using com.sun.corba.se.spi.orb;
using CsvHelper;
using java.lang;
using org.llrp.ltk.types;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Security.Policy;
using static ScottPlot.Generate;
using DateTime = System.DateTime;

namespace RFIDentify.Com
{
    public class DataProcess
    {
        private const int ChannelSize = 50;
        private static Dictionary<string, double[]>? BaseStand;
        private static string[] tags =
            {
                "",
                "E2000016811401862090C0A0",
                "E2000016811401862090C0A1",
				"E2000016811401862090C0A2",
				"E2000016811401862090C0A3",
				"E2000016811401862090C0A4"
            }; // 记录所有的标签
        private static string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
        private static string baseStandPath = "CollectionData/Base/baseStand.csv";
        public static void ReadBasePhase()
        {
            if (BaseStand != null)
            {
                return;
            }
            BaseStand = new Dictionary<string, double[]>();
            DataTable dt = CSVHelper.ReadCSV(basePath + baseStandPath, IndexColumn: 1);

            foreach (DataColumn dc in dt.Columns)
            {
                int idx = Convert.ToInt32(dc.ColumnName);
                double[] values = new double[ChannelSize];
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    values[i - 1] = Convert.ToDouble(dt.Rows[i][dc.ColumnName]);
                }
                BaseStand.Add(tags[idx], values);
            }
        }
        public static RFIDData Baseline(RFIDData data)
        {
            if (BaseStand == null) ReadBasePhase();
            data.Phase = Baseline(data.Phase, data.Channel, data.Tag);
            int index = Array.IndexOf(tags, data.Tag);
			data.Index = index == -1 ? null : index;
            return data;
        }

        public static double Baseline(double phase, int? channel, string? tag)
        {
            if (BaseStand == null) ReadBasePhase();
            int _channel = Convert.ToInt32(channel);
            double diff = phase - BaseStand![tag!][_channel];
            if (diff < -1000)
            {
                diff += 4096;
            }
            return diff;
        }

        public static List<RFIDData> Baseline(ref List<RFIDData> datas) {
            foreach (RFIDData data in datas)
            {
                data.Phase = Baseline(data.Phase, data.Channel, data.Tag);
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
