#define SIMULATION

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.sun.org.apache.xml.@internal.resolver.helpers;
using com.sun.tools.javac.util;
using CsvHelper;
using RFIDentify.Com;
using ScottPlot.Drawing.Colormaps;
using sun.swing;
using Sunny.UI;
using Timer = System.Windows.Forms.Timer;

namespace RFIDentify.UI
{
	public partial class EChart : UserControl
	{
		public Batcher<RFIDData> batcher;
		public Action? OnSave;
		public bool IsProcessed { get; set; } = true;

		private readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;
		private readonly string defaultCollectPath = $"CollectionData\\Data\\temp{DateTime.Now:yyyyMMddHHmmss}.csv";
		private readonly string defaultBaseStandPath = "CollectionData\\Base\\baseStand.csv";
		private string? baseStandPath; 
		// 设置时应该给绝对路径，而非相对根目录路径
		public string BaseStandPath { 			
			get => baseStandPath!;
			set => baseStandPath = value;
		}
		private string? writeCsvFilePath;
		// 设置时应该给绝对路径，而非相对根目录路径
		public string WriteCsvFilePath {
			get => writeCsvFilePath!; 
			set
			{
				if (string.IsNullOrEmpty(value)) return;
				FileInfo csvFile;
				try
				{
					csvFile = new FileInfo(value);
					DirectoryInfo parent = csvFile.Directory!;
					if (parent != null && !parent.Exists)
					{
						parent.Create();
					}
					if (!System.IO.File.Exists(csvFile.FullName))
					{
						using (csvFile.Create()) { }
						using var writer = new StreamWriter(System.IO.File.Open(csvFile.FullName!, FileMode.Append));
						using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
						csv.WriteHeader<RFIDData>();
						csv.NextRecord();
					}
				}
				catch (IOException e)
				{
					Console.WriteLine(e.ToString());
				}
				writeCsvFilePath = value;
			}
		}

		private UILineOption option = new();
		private Color[] colors =
		{
			Color.LightSalmon,
			Color.DarkGray,
			Color.OliveDrab,
			Color.OrangeRed,
			Color.PaleGreen,
			Color.DarkTurquoise,
			Color.LightPink
		};

		private static readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
		private readonly int timeout = 10000;
		private readonly int MaxMilliSecondRange = 6000;
		private Thread threadRead;
		//private static Dictionary<string, List<RFIDData>> datas = new Dictionary<string, List<RFIDData>>();
		private List<List<RFIDData>> datas = new();

#if SIMULATION
		private ManualResetEvent _threadOne = new ManualResetEvent(false);
		public bool[] _isOpen = new bool[] { false };
#else
		private static libltkjava? libltkjava;
		private readonly string readerPath = "Speedwayr-11-25-ab.local";//读写器连接路径
#endif

		private Timer chartRefreshTimer = new();

		public EChart()
		{
			InitializeComponent();
			batcher = new Batcher<RFIDData>(
				processor: Process, batchSize: 30, interval: TimeSpan.FromSeconds(5));
#if !SIMULATION
			if (libltkjava == null)
			{
				libltkjava = new libltkjava();
				libltkjava.ReadData += (arg) => { batcher.Add(arg); };
			}
#endif
			InitializeChart();
			InitializeTableLayoutPanel();

			BaseStandPath = basePath + defaultBaseStandPath;

			chartRefreshTimer.Interval = 50;
			chartRefreshTimer.Tick += new EventHandler(RefreshChart!);
			chartRefreshTimer.Start();
			// 初始化数据读取线程
			threadRead = new Thread(StartRead)
			{
				IsBackground = true
			};
		}

		private void InitializeTableLayoutPanel()
		{
			tableLayoutPanel_Tags.Controls.Clear();
			tableLayoutPanel_Tags.RowStyles.Clear();
			tableLayoutPanel_Tags.ColumnStyles.Clear();
			tableLayoutPanel_Tags.RowCount = DataProcess.Tags.Count - 1;
			tableLayoutPanel_Tags.ColumnCount = 1;
			tableLayoutPanel_Tags.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
			for (int i = 1; i < DataProcess.Tags.Count; i++)
			{
				tableLayoutPanel_Tags.RowStyles.Add(new RowStyle(SizeType.Percent, 1f / tableLayoutPanel_Tags.RowCount));
				UICheckBox uiCheckBox = new UICheckBox
				{
					Text = $"val{i}",
					Checked = true,
					CheckBoxColor = colors[i]
				};
				uiCheckBox.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
				tableLayoutPanel_Tags.Controls.Add(uiCheckBox);
			}
		}

		private void InitializeChart()
		{
			//option.ToolTip.Visible = true;
			option.Title = new UITitle();
			option.Title.Text = "RFID";
			option.Title.SubText = "PhaseLineChart";
			lineChart.SetOption(option);

			for (int i = 1; i < DataProcess.Tags.Count; i++)
			{
				var series = option.AddSeries(new UILineSeries("val" + i));
				series.Color = colors[i];
				series.CustomColor = true;
				datas.Add(new List<RFIDData>());
			}
		}

		private void UpdateTableLayoutPanel()
		{
			tableLayoutPanel_Tags.RowStyles.Clear();
			Point location = tableLayoutPanel_Tags.Location;
			int preHeight = tableLayoutPanel_Tags.Height;
			int totalHeight = tableLayoutPanel_Tags.RowCount * 40;
			location.Y = location.Y - (totalHeight - preHeight) / 2;
			tableLayoutPanel_Tags.Height = totalHeight;
			tableLayoutPanel_Tags.Location = location;
			for (int i = 0; i < tableLayoutPanel_Tags.RowCount; i++)
			{
				tableLayoutPanel_Tags.RowStyles.Add(new RowStyle(SizeType.Percent, 1f / tableLayoutPanel_Tags.RowCount));
			}
		}

		/// <summary>
		/// 读数据线程初始化函数
		/// </summary>
		private async void StartRead()
		{
#if SIMULATION
			long time = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
			Random random = new Random();
			for (int i = 0; i < 100000; i++)
			{
				if (!_isOpen[0])
				{
					_threadOne.Reset();
					_threadOne.WaitOne();
				}
				for (int j = 1; j <= 5; j++)
				{
					RFIDData arg = new RFIDData()
					{
						Time = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds() - time,
						Tag = DataProcess.Tags[j],
						Phase = 4096 * random.NextDouble(),
					};
					batcher.Add(arg);
				}
				await Task.Delay(10);
			}
#else
			this.libltkjava.startRead(null);
#endif
		}

		public void EnableSaveButton()
		{
			btn_Save.Visible = true;
			btn_Save.Click += btn_Save_Click;
			tableLayoutPanel_Btn.Location = new Point(310, 520);
			tableLayoutPanel_Btn.Size = new Size(510, 50);
			tableLayoutPanel_Btn.ColumnCount = 3;
			tableLayoutPanel_Btn.ColumnStyles.Clear();
			for (int i = 0; i < tableLayoutPanel_Btn.ColumnCount; i++)
			{
				tableLayoutPanel_Btn.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1f / 3));
			}
			tableLayoutPanel_Btn.Controls.Add(btn_Save);
		}

		/// <summary>
		/// Batcher 数据处理函数
		/// </summary>
		/// <param name="batch"></param>
		private void Process(Batch<RFIDData> batch)
		{
			using (batch)
			{
				if (rwLock.TryEnterWriteLock(timeout))
				{
					try
					{
						foreach (RFIDData data in batch)
						{
							string tag = data.Tag!;
							int index = DataProcess.Tags.IndexOf(tag);
							if (index != -1)
							{
								data.Index = index;
							}
							else
							{
								DataProcess.Tags.Add(tag);
								data.Index = DataProcess.Tags.Count - 1;
								datas.Add(new List<RFIDData>());
								// 图标添加折线
								var series = option.AddSeries(new UILineSeries("val" + data.Index.Value));
								series.Color = colors[data.Index.Value];
								series.CustomColor = true;
								// 添加Tag
								tableLayoutPanel_Tags.RowCount++;
								tableLayoutPanel_Tags.Controls.Add(new UICheckBox
								{
									Text = $"val{data.Index.Value}",
									Checked = true,
									CheckBoxColor = colors[data.Index.Value]
								});
								UpdateTableLayoutPanel();
							}
							if (data.Index.HasValue)
							{
								datas[data.Index.Value - 1].Add(data);
							}
							else
							{
								Console.WriteLine("Index is null");
							}
							DataProcess.Baseline(data); // 基准化
							System.Diagnostics.Debug.Assert(data.ProcessedPhase.HasValue);
						}
						// 限制数据量
						foreach (var item in datas)
						{
							if (item.Count >= 10000)
							{
								item.RemoveRange(0, item.Count - 5000);
							}
						}
						using var writer = new StreamWriter(File.Open(WriteCsvFilePath, FileMode.Append));
						using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
						foreach(var batchData in batch)
						{
							csv.WriteRecord(batchData);
							csv.NextRecord();
						}
						//csv.WriteRecords(batch);
					}
					catch (ArgumentException ex)
					{
						Console.WriteLine(ex.Message);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
					finally
					{
						rwLock.ExitWriteLock();
					}
				}
			}
		}

		/// <summary>
		/// 图表刷新函数
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RefreshChart(object sender, EventArgs e)
		{
			try
			{
				List<List<double>> tempTime = new();
				List<List<double>> tempPhase = new();
				if (rwLock.TryEnterReadLock(timeout))
				{
					try
					{
						foreach (var data in datas)
						{
							if (data.IsNullOrEmpty()) continue;
							tempTime.Add(data.Select(item => System.Convert.ToDouble(item.Time) / 1000).ToList());
							tempPhase.Add(data.Select(item => IsProcessed ? item.ProcessedPhase.GetValueOrDefault(item.Phase):item.Phase).ToList());
							//Trace.WriteLine($"{data.Count}:{IsProcessed}");
						}
					}
					finally
					{
						rwLock.ExitReadLock();
					}
				}
				foreach (UILineSeries value in option.Series.Values)
				{
					value.Clear();
				}
				double maxTime = MaxMilliSecondRange / 1000;
				for (int i = 0; i < tempTime.Count; i++)
				{
					int count = tempTime[i].Count;
					int j = (count - 1000 > 0) ? count - 1000 : 0;
					option.AddData($"val{i + 1}", tempTime[i].GetRange(j, count - j), tempPhase[i].GetRange(j, count - j));
					double temp = tempTime[i].Max();
					maxTime = (temp > maxTime ? temp : maxTime)!;
				}
				option.XAxis.SetRange(maxTime - MaxMilliSecondRange / 1000, maxTime);
				this.lineChart.Refresh();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// tag选择框改变事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBox_CheckedChanged(object? sender, EventArgs e)
		{
			if (rwLock.TryEnterWriteLock(timeout))
			{
				try
				{
					for (int i = 0; i < tableLayoutPanel_Tags.Controls.Count; i++)
					{
						if (tableLayoutPanel_Tags.Controls[i] is UICheckBox checkBox)
						{
							if (checkBox.Checked)
							{
								option.Series[$"val{i + 1}"].Visible = true;
							}
							else
							{
								option.Series[$"val{i + 1}"].Visible = false;
							}
						}
					}
					this.lineChart.Refresh();
				}
				finally
				{
					rwLock.ExitWriteLock();
				}
			}
		}

		private void btn_Start_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(baseStandPath))
			{
				MessageBox.Show("请设置基准文件路径");
				return;
			}
			if (string.IsNullOrEmpty(writeCsvFilePath))
			{
				WriteCsvFilePath = basePath + defaultCollectPath;
			}
			if ((threadRead.ThreadState & System.Threading.ThreadState.Unstarted) == System.Threading.ThreadState.Unstarted)
			{
				threadRead.Start();
			}
			else
			{
				MessageBox.Show("已经启动");
			}
			// 启动定时器
			chartRefreshTimer.Start();
#if SIMULATION
			_isOpen[0] = true;
			_threadOne.Set();
#endif
		}

		private void btn_Stop_Click(object sender, EventArgs e)
		{
#if SIMULATION
			_isOpen[0] = false;
#else
			libltkjava.PowerOff();
#endif
		}

		private void btn_Save_Click(object? sender, EventArgs e)
		{
			//MessageBox.Show("保存成功");
			foreach (var item in datas)
			{
				item.Clear();
			}
			OnSave?.Invoke();
		}

		private void EChart_Load(object sender, EventArgs e)
		{

		}
	}
}
