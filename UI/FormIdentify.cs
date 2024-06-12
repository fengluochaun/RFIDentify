#define SIMULATION

using RFIDentify.Com;
using Sunny.UI;
using System.Collections.Concurrent;
using Timer = System.Windows.Forms.Timer;

namespace RFIDentify.UI
{
	public partial class FormIdentify : UIPage
	{
		private FormMain parent;

		private readonly HttpHelper ChttpHelper = new();
		private readonly HttpHelper PhttpHelper = new("http://127.0.0.1:5000/");
		
		private static readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
		private static ConcurrentDictionary<string, List<RFIDData>> datas = new ConcurrentDictionary<string, List<RFIDData>>();

		private libltkjava libltkjava = new();
		public Batcher<RFIDData> batcher;
		
		private UILineOption option = new();
		private Color[] colors =
		{
			Color.LightSalmon,
			Color.LightYellow,
			Color.DarkGray,
			Color.OliveDrab,
			Color.OrangeRed,
			Color.PaleGreen,
			Color.DarkTurquoise,
			Color.LightPink
		};
		private Timer chartRefreshTimer;
		private Thread threadRead;

		private readonly int MaxMilliSecondRange = 6000;
		private readonly int timeout = 10000;

		private readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;
		private readonly string IdentificationPath_ = "CollectionData\\Identification\\temp.csv";//识别数据存储路径
		private readonly string baseStandPath_ = "CollectionData\\Base\\baseStand.csv";
#if SIMULATION
		private static ManualResetEvent _threadOne = new ManualResetEvent(false);
		public static bool[] _isOpen = new bool[] { false };
#else
		private readonly string readerPath = "Speedwayr-11-25-ab.local";//读写器连接路径
#endif
		public FormIdentify(FormMain parent)
		{
			InitializeComponent();
			this.parent = parent;
#if !SIMULATION
			libltkjava.ReadData += ReadDataFromEqu;
			libltkjava.powerON(readerPath, IdentificationPath_);
#endif
			batcher = new Batcher<RFIDData>(
				processor: Process,
				batchSize: 20,
				interval: TimeSpan.FromSeconds(5)
				);
			
			// 初始化 Chart
			option.ToolTip.Visible = true;
			option.Title = new UITitle();
			option.Title.Text = "RFID";
			option.Title.SubText = "PhaseLineChart";
			lineChart.SetOption(option);

			threadRead  = new Thread(StartRead)
			{
				IsBackground = true
			};

			// 初始化 Timer
			chartRefreshTimer = new Timer();
			chartRefreshTimer.Interval = 50; // 设置定时器间隔，单位为毫秒（这里设置为5秒）
			chartRefreshTimer.Tick += ChartRefreshTimer_Tick;

		}
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
				List<RFIDData> data = new List<RFIDData>();
				for (int j = 1; j <= 5; j++)
				{
					RFIDData arg = new RFIDData()
					{
						Time = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds() - time,
						Tag = "val" + j.ToString(),
						Phase = 4096 * random.NextDouble(),
					};
					batcher.Add(arg);
					data.Add(arg);
				}
				await Task.Delay(10);
			}
#else
			this.libltkjava.startRead(null);
#endif
		}

		/// <summary>
		/// Batcher 数据处理函数
		/// </summary>
		/// <param name="batch"></param>
		private void Process(Batch<RFIDData> batch)
		{
			using (batch)
			{
				try
				{
					rwLock.EnterWriteLock();
					foreach (RFIDData data in batch)
					{
						string tag = data.Tag!;
						if (!datas.ContainsKey(tag))
						{
							data.Tag = "val" + (datas.Count + 1);
							List<RFIDData> list = new List<RFIDData>();
							list.Add(data);
							datas.TryAdd(tag, list);
							var series = option.AddSeries(new UILineSeries(data.Tag!));
							series.Color = colors[datas.Count + 1];
						}
						//DataProcess.Baseline(data); // 基准化
						data.Tag = datas[tag][0].Tag;
						datas[tag].Add(data);

						// 限制数据量
						if (datas[tag].Count >= 10000)
						{
							datas[tag].RemoveRange(0, datas[tag].Count - 5000);
						}
					}
				}
				catch (ArgumentException ex)
				{
					Console.WriteLine(ex.Message);
				}
				catch (OverflowException ex)
				{
					Console.WriteLine(ex.Message);
				}
				finally
				{
					rwLock.ExitWriteLock();
				}
			}
		}

		private void UpdateChart()
		{
			if (rwLock.TryEnterReadLock(timeout))
			{
				try
				{
					foreach (UILineSeries value in option.Series.Values)
					{
						value.Clear();
					}
					int maxCount = 0;
					double maxTime = MaxMilliSecondRange;
					foreach (var data in datas)
					{
						int count = data.Value.Count;
						maxCount = count > maxCount ? count : maxCount;
						int i = (count - 1000 > 0) ? count - 1000 : 0;
						for (; i < count; i++)
						{
							option.AddData(data.Value[i].Tag, Convert.ToDouble(data.Value[i].Time) / 1000, data.Value[i].Phase);
							double temp = Convert.ToDouble(data.Value[i].Time);
							maxTime = (temp > maxTime ? temp : maxTime)!;
						}
					}
					option.XAxis.SetRange((maxTime - MaxMilliSecondRange) / 1000, maxTime / 1000);
					this.lineChart.Refresh();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				finally
				{
					rwLock.ExitReadLock();
				}
			}
			else
			{
				UpdateChart();
			}
		}

		/// <summary>
		/// 读取设备数据
		/// </summary>
		/// <param name="args"></param>
		private void ReadDataFromEqu(RFIDData data)
		{
			batcher.Add(data);
		}

		private void ChartRefreshTimer_Tick(object? sender, EventArgs e)
		{
			UpdateChart();
		}

		private void btn_Start_Click(object sender, EventArgs e)
		{
			if ((threadRead.ThreadState & ThreadState.Unstarted) == ThreadState.Unstarted)
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
			libltkjava.stop();
#endif
			// 上传识别数据
			var o = new
			{
				filePath = basePath + IdentificationPath_,
				baseStandPath = basePath + baseStandPath_
			};
			Task task = new(async () =>
			{
				var result = await PhttpHelper.PostAsync<object>("User/UserRecognition", o);
				MethodInvoker mi = new MethodInvoker(() =>
				{
					this.lbl_Identification.Text = "识别对象：" + result.ToString();
				});
				this.BeginInvoke(mi);
			});
			task.Start();
		}

		private void btn_AddUser_Click(object sender, EventArgs e)
		{
			parent.formRegister.UpdateByUserId();
			parent.SelectPage(1002);
		}

		private void lbl_Identifcation_Click(object sender, EventArgs e)
		{
			if (int.TryParse((lbl_Identification.Text.SplitLast("：")), out int id))
			{
				parent.formRegister.UpdateByUserId(id);
				parent.SelectPage(1002);
			}
		}
	}
}
