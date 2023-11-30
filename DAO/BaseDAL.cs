using com.sun.org.omg.SendingContext.CodeBasePackage;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using RFIDentify.Com;

namespace RFIDentify.DAO
{
    public class BaseDAL<T> where T : class
    {
        public SQLiteHelper sqliteHelper = SQLiteHelper.GetInstance();
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> Add(T model)
        {
            if (model == null)
                return 0;
            string cols = string.Empty;
            Type type = model.GetType();
            string primaryKey = type.GetPrimaryName();
            string tableName = type.GetTName();
            PropertyInfo[] properties = null;
            if (type.IsAutoIncrement())
            {
                if (cols.Contains(primaryKey))
                {
                    cols = RemoveStrItem(cols, primaryKey);
                }
            }
            else
            {
                if (!cols.Contains(primaryKey))
                {
                    //抛异常      
                    throw new Exception("主键列不能为空！");
                }
            }
            properties = type.GetProperties();
            string strCols = string.Join(",", properties.Select(pr => pr.GetColName()));
            string paraCols = string.Join(",", properties.Select(pr => $"@{pr.Name}"));
            SQLiteParameter p = new SQLiteParameter();
            string sql = $"insert into [{tableName}] ({strCols}) values ({paraCols})";
            return await sqliteHelper.Execute(sql, properties.Select(pr => new SQLiteParameter("@" + pr.Name, pr.GetValue(model) ?? DBNull.Value)).ToArray());
        }

        public async Task<int> Delete(T model, string strWhere)
        {
            string sql = "";
            Type type = typeof(T);
            string tableName = type.GetTName();

            sql = $"delete from {tableName} where 1=1";
            if (!string.IsNullOrEmpty(strWhere))
                sql += " and " + strWhere;
            return await sqliteHelper.Execute(sql);
        }

        public async Task<int> Update(T model, string strWhere)
        {
            if (model == null) return 0;
            Type type = typeof(T);
            string tableName = type.GetTName();
            string primaryKey = type.GetPrimaryName();//主键名
            PropertyInfo[] properties = null;

            properties = type.GetProperties();
            string strCols = string.Join(",", properties.Where(p => primaryKey.Contains(p.GetColName())).Select(p => string.Format("{0}=@{0}", p.GetColName())));

            if (string.IsNullOrEmpty(strWhere))
                strWhere = $"{primaryKey}=@{primaryKey}";

            string sql = $"update [{tableName}] set {strCols} where {strWhere} ";
            //参数数组生成
            SQLiteParameter[] paras = properties.Select(p => new SQLiteParameter("@" + p.GetColName(), p.GetValue(model) ?? DBNull.Value)).ToArray();
            return await sqliteHelper.Execute(sql, paras);
        }

        /// <summary>
        /// 移除指定的子串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="reStr"></param>
        /// <returns></returns>
        public static string RemoveStrItem(string str, string reStr)
        {
            List<string> arrStr = GetStrList(str, ',', false);
            arrStr.Remove(reStr);
            return GetListStrToString(arrStr, ",");
        }

        public static List<string> GetStrList(string str, char speater, bool toLower)
        {
            List<string> list = new List<string>();
            string[] ss = str.Split(speater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != speater.ToString())
                {
                    string strVal = s.Trim();
                    if (toLower)
                    {
                        strVal = s.ToLower();
                    }
                    list.Add(strVal);
                }
            }
            return list;
        }

        /// <summary>
        /// 把 List<string> 按照分隔符组装成 string
        /// </summary>
        /// <param name="list"></param>
        /// <param name="speater"></param>
        /// <returns></returns>
        public static string GetListStrToString(List<string> list, string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }
    }
}
