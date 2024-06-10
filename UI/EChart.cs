#define SIMULATION

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
using RFIDentify.Com;
using Sunny.UI;
using Timer = System.Windows.Forms.Timer;

namespace RFIDentify.UI
{
	public partial class EChart : UserControl
	{
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

		private static readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
		//private static Dictionary<string, List<RFIDData>> datas = new Dictionary<string, List<RFIDData>>();
		private static List<List<RFIDData>> datas = new();

		private readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;
		private readonly string defaultCollectPath_ = $"CollectionData/Data/temp{DateTime.Now:yyyyMMddHHmmss}.csv";//识别数据存储路径
		private readonly string baseStandPath_ = "CollectionData/Base/baseStand.csv";
#if SIMULATION
		private static ManualResetEvent _threadOne = new ManualResetEvent(false);
		public static bool[] _isOpen = new bool[] { false };
#else
		private static libltkjava libltkjava;
		private readonly string readerPath = "Speedwayr-11-25-ab.local";//读写器连接路径
#endif

		public EChart()
		{
			InitializeComponent();
#if !SIMULATION
			if (libltkjava == null)
			{
				libltkjava = new libltkjava();
				libltkjava.powerON(readerPath, basePath);
				libltkjava.ReadData += ReadDataFromEqu;
			}
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

		/// <summary>
		/// 读取设备数据
		/// </summary>
		/// <param name="args"></param>
		private void ReadDataFromEqu(List<RFIDData> datas)
		{
			if (datas.Count == 0) return;
			foreach (var arg in datas)
			{
				batcher.Add(arg);
			}
		}
		private void btn_Start_Click(object sender, EventArgs e)
		{

		}

		private void btn_Stop_Click(object sender, EventArgs e)
		{

		}

		private void btn_Save_Click(object sender, EventArgs e)
		{

		}
	}
}
