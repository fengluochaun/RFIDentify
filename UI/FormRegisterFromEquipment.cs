using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using org.jdom;
using RFIDentify.Com;
using Sunny.UI;

namespace RFIDentify.UI
{
    public partial class FormRegisterFromEquipment : UIPage
    {
        private FormRegister? _formRegister;
        private FormMain _parent;


        private static ConcurrentDictionary<string, (string, int)> datas = new ConcurrentDictionary<string, (string, int)>();
        private libltkjava libltkjava = new libltkjava();
        private long currentTimeStamp;
        public Batcher<RFIDData>? batcher;
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


        public FormRegisterFromEquipment(FormMain? parent)
        {
            InitializeComponent();
            Initial();
            _parent = parent;
        }
        public FormRegisterFromEquipment(FormMain? parent, FormRegister formRegister)
        {
            InitializeComponent();
            _formRegister = formRegister;
            lbl_Id.Text = "编号：" + formRegister.user!.Id.ToString();
            Initial();
            _parent = parent;
        }

        public void Initial()
        {
            libltkjava.UpdateData += UpdateQueueValue;
            batcher = new Batcher<RFIDData>(
                processor: this.Process,
                batchSize: 20,
                interval: TimeSpan.FromSeconds(5)
                );
            option.ToolTip.Visible = true;
            option.Title = new UITitle();
            option.Title.Text = "RFID";
            option.Title.SubText = "PhaseLineChart";
            this.lineChart.SetOption(option);
        }

        public void UpdateByUser(FormRegister formRegister)
        {
            _formRegister = formRegister;
            lbl_Id.Text = "编号：" + formRegister.user!.Id.ToString();
        }


        private void btn_Save_Click(object sender, EventArgs e)
        {
            _parent!.SelectPage(1002);
        }

        private async void btn_CollectEnvir_Click(object sender, EventArgs e)
        {
            await libltkjava.powerON(args, @"CollectionData\Base\BaseStand1");
            StartReadShow();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            libltkjava.PowerOFF();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            //await libltkjava.powerON(args, @"CollectionData\Identification\l.csv");
            currentTimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            StartReadShow();
        }

        public void StartReadShow()
        {
            Thread t = new Thread(async () =>
            {
                //this.libltkjava.startRead();
                Random random = new Random();
                for (int i = 0; i < 100000; i++)
                {
                    List<RFIDData> data = new List<RFIDData>();
                    for (int j = 1; j <= 5; j++)
                    {
                        RFIDData arg = new RFIDData()
                        {
                            tag = "val" + j.ToString(),
                            phase = 4096 * random.NextDouble(),
                        };
                        batcher!.Add(arg);
                        data.Add(arg);
                    }
                    await Task.Delay(1);
                }
            })
            {
                IsBackground = true
            };
            t.Start();
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
                this.lineChart.SetOption(option);
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
                batcher!.Add(arg);
            }
        }
    }
}
