﻿using CsvHelper;
using java.lang;
using org.llrp.ltk.types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Security.Policy;
using static ScottPlot.Generate;

namespace RFIDentify.Com
{
    public class DataProcess
    {
        public const int ChannelSize = 50;
        public static Dictionary<string, double[]>? BaseStand;
        public static string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
        public static string baseStandPath = "CollectionData/Base/baseStand.csv";
        public static void ReadBasePhase()
        {
            if(BaseStand != null)
            {
                return;
            }
            BaseStand = new Dictionary<string, double[]>();
            //using (var reader = new StreamReader(basePath + baseStandPath))
            //{
            //    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            //    {
            //        while (csv.Read())
            //        {
            //            var record = csv.GetRecord<double[]>();
            //        }
            //    }
            //}
            DataTable dt = CSVHelper.ReadCSV(basePath + baseStandPath, IndexColumn : 1);
            
            foreach(DataColumn dc in dt.Columns)
            {
                BaseStand.Add(dc.ColumnName.ToString(), new double[ChannelSize]);
            }
            for(int i = 1; i < dt.Rows.Count; i++) {
                foreach (KeyValuePair<string, double[]> kvp in BaseStand)
                {
                    BaseStand[kvp.Key][i-1] = Convert.ToDouble(dt.Rows[i][kvp.Key]);
                }
            }
        }
        public static RFIDData Baseline(RFIDData data)
        {
            if(BaseStand == null) ReadBasePhase();
            data.phase = Baseline(data.phase, data.channel, data.tag);
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
            foreach(RFIDData data in datas)
            {
                data.phase = Baseline(data.phase, data.channel, data.tag);
            }
            return datas;
        }

        //处理时间戳
        public static long ProcessTimestamp(string time)
        {
            time = time.Replace("+08:00", "").Replace(":", " ").Replace("T", " ").Replace("Z", "").Replace(".", " ");

            DateTimeOffset dateTime = DateTimeOffset.ParseExact(time, "yyyy-MM-dd HH mm ss fff", CultureInfo.InvariantCulture);

            long t = dateTime.ToUnixTimeMilliseconds() * 1000 + dateTime.Millisecond;

            return t / 1000000;
        }
    }
}
