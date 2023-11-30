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

namespace RFIDentify.UI
{
    public partial class FormIdentify : Form
    {
        private static Dictionary<string, Queue<double>> datas = new Dictionary<string, Queue<double>>();
        private static List<double[]> DisDatas
        {
            get
            {
                return datas.Values.Select(x => x.ToArray()).ToList();
            }
        }
        private libltkjava libltkjava = new libltkjava();
        private long currentTimeStamp;
        public Batcher<RFIDData> batcher;
        private string args = "Speedwayr-11-25-ab.local";//读写器连接路径
        private ScottPlot.Plottable.DataLogger Logger;
        public FormIdentify()
        {
            InitializeComponent();
            libltkjava.UpdateData += UpdateQueueValue;
            batcher = new Batcher<RFIDData>(
                processor: this.Process,
                batchSize: 20,
                interval: TimeSpan.FromSeconds(5)
                );
        }

        private async void btn_Start_Click(object sender, EventArgs e)
        {
            //await libltkjava.powerON(args, @"ss\l.csv");
            currentTimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            InitChart();
            StartReadShow();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {

        }

        private void btn_AddUser_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Identifcation_Click(object sender, EventArgs e)
        {

        }

        private int calculateTime()
        {
            long nowTimeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            return (int)(nowTimeStamp - currentTimeStamp);
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
                    for (int j = 0; j < 5; j++)
                    {
                        RFIDData arg = new RFIDData()
                        {
                            tag = j.ToString(),
                            phase = 4096 * random.NextDouble(),
                        };
                        batcher.Add(arg);
                        data.Add(arg);
                    }
                    //UpdateQueueValue(data);                    
                    await Task.Delay(1);
                }
            })
            {
                IsBackground = true
            };
            t.Start();
        }

        private void InitChart()
        {
            //var count = DisDatas.Count;
            //for(int i = 0; i < count; i++)
            //{
            //    this.plot.Plot.AddDataLogger(label: "Tag"+i.ToString());
            //}
            Logger = this.plot.Plot.AddDataLogger(label: "Tag1");
            Logger.ViewFull();
        }

        /// <summary>
        /// 更新 Chart
        /// </summary>
        delegate void UpdateChartCallBack();
        private void UpdateChart()
        {
            if (this.plot.InvokeRequired)
            {
                UpdateChartCallBack updateChartCallBack = new UpdateChartCallBack(UpdateChart);
                this.Invoke(updateChartCallBack);
            }
            else
            {
                if (Logger.Count == Logger.CountOnLastRender) return;
                if (Logger.Count >= 1000 && Logger.Count <= 1040)
                {
                    Logger.ViewSlide();
                }
                //this.plot.Render();
                this.plot.Refresh();
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
                foreach (RFIDData data in batch)
                {
                    //if (!datas.ContainsKey(data.tag!))
                    //{
                    //    if (datas.Count >= 5)
                    //    {                            
                    //        continue;
                    //    }
                    //    datas.Add(data.tag!, new Queue<double>());
                    //    datas[data.tag!].Enqueue(data.phase);
                    //    return;
                    //}
                    //else if (datas[data.tag!].Count > 600)
                    //{
                    //    datas[data.tag!].Dequeue();
                    //}
                    //datas[data.tag!].Enqueue(data.phase);
                    Logger.Add(calculateTime() * 100, data.phase);
                }
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
            //if (datas.Count == 0)
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        datas[i.ToString()] = new Queue<double>(600);
            //    }
            //}
            //Random r = new Random();
            //for (int i = 0; i < 5; i++)
            //{
            //    string tag = i.ToString();
            //    if (datas[tag].Count > 600)
            //    {
            //        for (int j = 0; j < num; j++)
            //        {
            //            datas[tag].Dequeue();
            //        }
            //    }
            //    for (int j = 0; j < num; j++)
            //    {
            //        datas[tag].Enqueue(r.NextDouble() * 4096);
            //    }
            //}
        }
    }
}
