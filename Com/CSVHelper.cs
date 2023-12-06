using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDentify.Com
{
    internal class CSVHelper
    {
        /// <summary>
         /// 创建CSV文件并写入内容
         /// </summary>
         /// <param name="dt">DataTable</param>
         /// <param name="fileName">文件全名</param>
         /// <returns>是否写入成功</returns>
         public static Boolean SaveCSV(DataTable dt, string fullFileName)
         {
             Boolean r = false;
             FileStream fs = new FileStream(fullFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
             StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
             string data = "";
 
             //写出列名称
             for (int i = 0; i < dt.Columns.Count; i++)
             {
                 data += dt.Columns[i].ColumnName.ToString();
                 if (i < dt.Columns.Count - 1)
                 {
                     data += ",";
                 }
             }
             sw.WriteLine(data);
 
             //写出各行数据
             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 data = "";
                 for (int j = 0; j < dt.Columns.Count; j++)
                 {
                     data += dt.Rows[i][j].ToString();
                     if (j < dt.Columns.Count - 1)
                     {
                         data += ",";
                     }
                 }
                 sw.WriteLine(data);
             }
 
             sw.Close();
             fs.Close();
 
             r = true;
             return r;
         }
 
         /// <summary>
         /// 读CSV 文件
         /// </summary>
         /// <param name="fileName">文件全名</param>
         /// <returns>DataTable</returns>
         public static DataTable ReadCSV(string fullFileName)
         {
             return ReadCSV(fullFileName, 0, 0, 0, 0, true);
         }
 
         /// <summary>
         /// 读CSV 文件
         /// </summary>
         /// <param name="fileName">文件全名</param>
         /// <param name="firstRow">开始行</param>
         /// <param name="firstColumn">开始列</param>
         /// <param name="getRows">获取多少行</param>
         /// <param name="getColumns">获取多少列</param>
         /// <param name="haveTitleRow">是有标题行</param>
         /// <returns>DataTable</returns>
         public static DataTable ReadCSV(string fullFileName, Int16 firstRow = 0, Int16 firstColumn = 0, Int16 getRows = 0, Int16 getColumns = 0, bool haveTitleRow = true, int IndexColumn = 0)
         {
             DataTable dt = new DataTable();
             FileStream fs = new FileStream(fullFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
             StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
             try
             {
                 string strLine = "";//记录每次读取的一行记录
                 string[] aryLine;//记录每行记录中的各字段内容
                 int columnCount = 0; //标示列数
                 bool bCreateTableColumns = false;//是否已建立了表的字段
                 int iRow = 1;//第几行
 
                 if (firstRow > 0) //去除无用行
                 {
                     for (int i = 1; i < firstRow; i++)
                     {
                         sr.ReadLine();
                     }
                 }
                 string[] separators = { "," };// { ",", ".", "!", "?", ";", ":", " " };
                 while ((strLine = sr.ReadLine()) != null)//逐行读取CSV中的数据
                 {
                     strLine = strLine.Trim();
                     aryLine = strLine.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
 
                     if (bCreateTableColumns == false)
                     {
                         bCreateTableColumns = true;
                         columnCount = aryLine.Length;
                         //创建列
                         for (int i = firstColumn; i < (getColumns == 0 ? columnCount : firstColumn + getColumns); i++)
                         {
                             DataColumn dc = new DataColumn(haveTitleRow == true ? aryLine[i] : "COL" + i.ToString());
                             dt.Columns.Add(dc);
                         }
 
                         bCreateTableColumns = true;
 
                         if (haveTitleRow == true)
                         {
                             continue;
                         }
                     }
 
                     DataRow dr = dt.NewRow();
                     for (int j = firstColumn; j < (getColumns == 0 ? columnCount : firstColumn + getColumns); j++)
                     {
                         dr[j - firstColumn] = aryLine[j + IndexColumn];
                     }
                     dt.Rows.Add(dr);
 
                     iRow = iRow + 1;
                     if (getRows > 0)
                     {
                         if (iRow > getRows)
                         {
                             break;
                         }
                     }
                 }
             }
             catch (Exception)
             {
                 //异常处理
             }
             finally
             {
                 sr.Close();
                 fs.Close();
             }
             return dt;
         }
 
    }
}
