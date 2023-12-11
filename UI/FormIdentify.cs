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

namespace RFIDentify.UI
{
    public partial class FormIdentify : UIPage
    {
        private FormMain _parent;

        private static ConcurrentDictionary<string, (string, int)> datas = new ConcurrentDictionary<string, (string, int)>();
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
            this.lineChart.SetOption(option);
            Thread thread = new Thread(StartReadShow)
            {
                IsBackground = true
            };
            thread.Start();
        }

        private async void btn_Start_Click(object sender, EventArgs e)
        {
            //await libltkjava.powerON(args, @"Collection\Identification\l.csv");
            currentTimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            //StartReadShow();
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

        private int calculateTime()
        {
            long nowTimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            return (int)(nowTimeStamp - currentTimeStamp);
        }

        public static bool[] _isOpen = new bool[] { false };
        public async void StartReadShow()
        {           
            //this.libltkjava.powerON(args, @"Collection\Identification\l.csv");
            
            //this.libltkjava.startRead();
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
                        tag = "val" + j.ToString(),
                        phase = 4096 * random.NextDouble(),
                    };
                    batcher.Add(arg);
                    data.Add(arg);
                }
                await Task.Delay(1);
            }            
        }

        /// <summary>
        /// 更新 Chart
        /// </summary>
        delegate void UpdateChartCallBack();
        private void UpdateChart()
        {
            if (this.lineChart.InvokeRequired)
            {
                UpdateChartCallBack updateChartCallBack = new UpdateChartCallBack(UpdateChart);
                this.Invoke(updateChartCallBack);
            }
            else
            {
                //this.lineChart.SetOption(option);
                this.lineChart.Refresh();
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
                    foreach (RFIDData data in batch)
                    {
                        if (!datas.ContainsKey(data.tag!))
                        {
                            string tag = "val" + (datas.Count + 1);
                            datas.TryAdd(data.tag!, (tag, 0));
                            var series = option.AddSeries(new UILineSeries(tag));
                            series.SetMaxCount(1000);
                            series.Color = colors[datas.Count + 1];
                        }
                        var tagValue = datas[data.tag!];
                        tagValue.Item2++;
                        datas[data.tag!] = tagValue;
                        data.tag = tagValue.Item1;
                        this.lineChart.Option.AddData(tagValue.Item1, tagValue.Item2++, DataProcess.Baseline(data).phase);
                    }
                    UpdateChart();
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine(ex.Message);
                }

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
