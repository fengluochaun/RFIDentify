using RFIDentify.Com;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using org.llrp.ltk.types;
using System.Globalization;
using ScottPlot;
using Sunny.UI;
using System.Collections.Concurrent;
using javax.xml.crypto;
using com.sun.org.apache.bcel.@internal.generic;
using Timer = System.Windows.Forms.Timer;
using javax.management;

namespace RFIDentify.UI
{
    public partial class FormIdentify : UIPage
    {
        private FormMain _parent;
        public readonly int MaxMilliSecondRange = 6000;

        private int timeout = 10000;
		private static readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
		private static ConcurrentDictionary<string, List<RFIDData>> datas = new ConcurrentDictionary<string, List<RFIDData>>();
        
        private libltkjava libltkjava = new libltkjava();
        private long currentTimeStamp;
        public Batcher<RFIDData> batcher;
        private string args = "Speedwayr-11-25-ab.local";//读写器连接路径
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

		private static ManualResetEvent _threadOne = new ManualResetEvent(false);

        public FormIdentify(FormMain parent)
        {
            InitializeComponent();
			libltkjava.UpdateData += UpdateQueueValue;
            batcher = new Batcher<RFIDData>(
                processor: this.Process,
                batchSize: 20,
                interval: TimeSpan.FromSeconds(5)
                );
            this._parent = parent;
            option.ToolTip.Visible = true;
            option.Title = new UITitle();
            option.Title.Text = "RFID";
            option.Title.SubText = "PhaseLineChart";
            //option.XAxisType = UIAxisType.DateTime;
            //option.XAxis.AxisLabel.DateTimeFormat = "mm:ss";
            this.lineChart.SetOption(option);
            

			// 初始化 Timer
			chartRefreshTimer = new Timer();
			chartRefreshTimer.Interval = 50; // 设置定时器间隔，单位为毫秒（这里设置为5秒）
			chartRefreshTimer.Tick += ChartRefreshTimer_Tick;

			// 启动定时器
			chartRefreshTimer.Start();
		}
		private void ChartRefreshTimer_Tick(object? sender, EventArgs e)
		{
            UpdateChart();
		}

		private void btn_Start_Click(object sender, EventArgs e)
        {
            //await libltkjava.powerON(args, @"Collection\Identification\l.csv");
            currentTimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
			Thread thread = new Thread(StartReadShow)
			{
				IsBackground = true
			};
			thread.Start();
			_isOpen[0] = true;
            _threadOne.Set();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            //libltkjava.PowerOFF();
            string filepath = @"D:\feng\Documents\Tencent\1744665475\FileRecv\实验代码\test\RFIDentify_Backend\ss\l.csv";
            _isOpen[0] = false;
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("file", filepath);
            Task task = new Task(async () =>
            {
                var result = await WebUtils.InvokeWebapi("http://127.0.0.1:5000", "user_recognition", "post", param);
                MethodInvoker mi = new MethodInvoker(() =>
                {
                    this.lbl_Identification.Text = "识别对象：" + result.Result.ToString();
                });
                this.BeginInvoke(mi);
            });
            task.Start();           
            
        }

        private void btn_AddUser_Click(object sender, EventArgs e)
        {
            _parent.formRegister.UpdateByUserId();
            _parent.SelectPage(1002);
        }

        private void lbl_Identifcation_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse((lbl_Identification.Text.SplitLast("：")), out id))
            {
                _parent.formRegister.UpdateByUserId(id);
                _parent.SelectPage(1002);
            }
        }

        public static bool[] _isOpen = new bool[] { false };
        public async void StartReadShow()
        {
            //this.libltkjava.powerON(args, @"Collection\Identification\l.csv");            
            //this.libltkjava.startRead();
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
                        time = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds() - time,
                        tag = "val" + j.ToString(),
                        phase = 4096 * random.NextDouble(),
                    };
                    batcher.Add(arg);
                    data.Add(arg);
                }
                await Task.Delay(10);
            }            
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
						string tag = data.tag!;						
						if (!datas.ContainsKey(tag))
                        {
							data.tag = "val" + (datas.Count + 1);
							List<RFIDData> list = new List<RFIDData>();
                            list.Add(data);
							datas.TryAdd(tag, list);
                            var series = option.AddSeries(new UILineSeries(data.tag!));
                            series.Color = colors[datas.Count + 1];
                        }
						DataProcess.Baseline(data); // 基准化
						data.tag = datas[tag][0].tag;
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
                        for(; i < count; i++)
                        {
							option.AddData(data.Value[i].tag, Convert.ToDouble(data.Value[i].time)/1000, data.Value[i].phase);
						    double temp = Convert.ToDouble(data.Value[i].time);
                            maxTime = (temp > maxTime ? temp : maxTime)!;
						}
					}
                    option.XAxis.SetRange((maxTime - MaxMilliSecondRange) / 1000, maxTime / 1000);
					this.lineChart.Refresh();
				}
				catch(Exception ex)
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
        /// 更新队列值
        /// </summary>
        /// <param name="args"></param>
        private void UpdateQueueValue(List<RFIDData> args)
        {
            if (args.Count == 0) return;
            foreach (var arg in args)
            {
                batcher.Add(arg);
            }
        }
    }
}
