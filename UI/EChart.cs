﻿#define SIMULATION

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
		public string WriteCsvFilePath { get; set; } = "CollectionData/Data/temp.csv";

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
		private static List<List<RFIDData>> datas = new();

		private readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;
		private readonly string defaultCollectPath_ = $"CollectionData/Data/temp{DateTime.Now:yyyyMMddHHmmss}.csv";//识别数据存储路径
		private readonly string baseStandPath_ = "CollectionData/Base/baseStand.csv";
#if SIMULATION
		private static ManualResetEvent _threadOne = new ManualResetEvent(false);
		public static bool[] _isOpen = new bool[] { false };
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

		public delegate void SaveDataHandler(object sender, EventArgs e);
		public SaveDataHandler handler;
		public void AddSaveButton()
		{

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
							Debug.Assert(data.ProcessedPhase.HasValue);
						}
						// 限制数据量
						foreach (var item in datas)
						{
							if (item.Count >= 10000)
							{
								item.RemoveRange(0, item.Count - 5000);
							}
						}
						using var writer = new StreamWriter(WriteCsvFilePath, append: true);
						using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
						csv.WriteRecords(batch);
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
							tempTime.Add(data.Select(item => System.Convert.ToDouble(item.Time) / 1000).ToList());
							tempPhase.Add(data.Select(item => item.Phase).ToList());
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
								//Trace.WriteLine($"CheckBox Color: {checkBox.CheckBoxColor}");
								//Trace.WriteLine($"Series Color: {option.Series[$"val{i + 1}"].Color}");
								//option.Series[$"val{i + 1}"].ShowLine = true;
								//option.Series[$"val{i + 1}"].Color = checkBox.CheckBoxColor;
							}
							else
							{
								option.Series[$"val{i + 1}"].Visible = false;
								//option.Series[$"val{i + 1}"].ShowLine = false;
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
			if ((threadRead.ThreadState & System.Threading.ThreadState.Unstarted) == System.Threading.ThreadState.Unstarted)
			{
				threadRead.Start();
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

		private void btn_Save_Click(object sender, EventArgs e)
		{
			
		}
	}
}
