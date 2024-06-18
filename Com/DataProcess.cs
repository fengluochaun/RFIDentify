using com.sun.corba.se.spi.orb;
using CsvHelper;
using java.lang;
using java.nio.file;
using org.llrp.ltk.types;
using ScottPlot;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Security.Policy;
using static ScottPlot.Generate;
using DateTime = System.DateTime;

namespace RFIDentify.Com
{
    public class DataProcess
    {
        private const int ChannelSize = 50;
        //private static Dictionary<string, double[]>? BaseStand;
        private static List<List<double>>? BaseStand;
        private static List<string> tags = new()
            {
                "",
				"e200001d7018006209601feb",
				"e200001d701800950960402b",
				"e200001d70180069096028ca",
				"e200001d701800630960219f",
				"e200001d7018008209603605"
			}; // 记录所有的标签
        public static List<string> Tags { get => tags; }
        private static string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
        private static string baseStandPath = basePath + "CollectionData\\Base\\baseStand.csv";
        public static long BeginTime { get; set; }
        public static void ReadBasePhase()
        {
            if (BaseStand != null)
            {
                return;
            }
            BaseStand = new List<List<double>>() { new List<double>()};
            try
            {
				DataTable dt = CSVHelper.ReadCSV(baseStandPath, IndexColumn: 1);

				foreach (DataColumn dc in dt.Columns)
				{
					int idx = Convert.ToInt32(dc.ColumnName);
					List<double> values = new()
					{
						0
					};
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						values.Add(Convert.ToDouble(dt.Rows[i][dc.ColumnName]));
					}
					BaseStand.Add(values);
				}
            }
            catch (FileNotFoundException e)
            {
                Trace.WriteLine(e.Message);
            }
            
        }

        public static void UpdateBaseStand(string? path)
        {
			if (string.IsNullOrEmpty(path))
            {
                path = ConfigManager.GetStringFromConfig("CurrentBaseStandPath", baseStandPath);
            }

            if (baseStandPath != path)
            {
                BaseStand = null;
                baseStandPath = path;
                ReadBasePhase();
            }
		}

        public static RFIDData Baseline(RFIDData data)
        {
            if (BaseStand == null) ReadBasePhase();
            data.ProcessedPhase = Baseline(data.Index!.Value, data.Phase, data.Channel, data.Tag);
            return data;
        }

        public static double Baseline(int index, double phase, int? channel, string? tag)
        {
            if (BaseStand == null) ReadBasePhase();
            int _channel = Convert.ToInt32(channel);
            double diff = phase - BaseStand![index][_channel];
            if (diff < -1000)
            {
                diff += 4096;
            }
            return diff;
        }

        public static List<RFIDData> Baseline(ref List<RFIDData> datas) {
            foreach (RFIDData data in datas)
            {
                data.Phase = Baseline(data.Index!.Value, data.Phase, data.Channel, data.Tag);
            }
            return datas;
        }

        //处理时间戳
        public static long String2Timestamp(string time)
        {
            time = time.Replace("+08:00", "").Replace(":", " ").Replace("T", " ").Replace("Z", "").Replace(".", " ");

            DateTimeOffset dateTime = DateTimeOffset.ParseExact(time, "yyyy-MM-dd HH mm ss fff", CultureInfo.InvariantCulture);

            long t = dateTime.ToUnixTimeMilliseconds() * 1000 + dateTime.Millisecond;

            return t / 1000000;
        }
        
        public static void SaveFormatDataToCSVByTag(List<RFIDData> datas)
        {
			DataTable dt = new DataTable();
			dt.Columns.Add("Time", typeof(string));
			dt.Columns.Add("Tag", typeof(string));
			dt.Columns.Add("Phase", typeof(double));
			dt.Columns.Add("Channel", typeof(int));
			foreach (RFIDData data in datas)
            {
				DataRow dr = dt.NewRow();
                dr["Time"] = data.Time;
                dr["Tag"] = data.Tag;
                dr["Phase"] = data.Phase;
                dr["Channel"] = data.Channel;
				dt.Rows.Add(dr);
			}
			//CSVHelper.SaveCSV(dt, path);
		}
    }
}
