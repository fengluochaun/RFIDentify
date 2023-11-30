using RFIDentify.Com.CustAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RFIDentify.Com
{
    public static class AttributeHelper
    {
        /// <summary>
        /// 获取映射表名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTName(this Type type)
        {
            string tableName = "";
            TableAttribute attr = type.GetCustomAttribute<TableAttribute>();
            if (attr != null)
                tableName = attr.TableName;
            if (string.IsNullOrEmpty(tableName))
                tableName = type.Name;
            return tableName;
        }

        /// <summary>
        /// 获取映射列名
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetColName(this PropertyInfo property)
        {
            string colName = "";
            ColumnAttribute attr = property.GetCustomAttribute<ColumnAttribute>();
            if (attr != null)
                colName = attr.ColumnName;
            if (string.IsNullOrEmpty(colName))
                colName = property.Name;
            return colName;
        }

        /// <summary>
        /// 获取主键名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetPrimaryName(this Type type)
        {
            string priName = "";
            PrimaryKeyAttribute attr = type.GetCustomAttribute<PrimaryKeyAttribute>();
            if (attr != null)
                priName = string.Join(",", attr.KeyName);
            return priName;
        }

        /// <summary>
        /// 判断指定属性是否为主键
        /// </summary>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsPrimaryKey(this PropertyInfo property)
        {
            Type type = property.DeclaringType;
            string primaryKey = type.GetPrimaryName();//获取该类型的主键名
            string colName = property.GetColName();//获取该属性的映射列名
            return (primaryKey == colName);
        }

        /// <summary>
        /// 判断主键是否自增
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAutoIncrement(this Type type)
        {
            PrimaryKeyAttribute attr = type.GetCustomAttribute<PrimaryKeyAttribute>();
            if (attr != null)
                return attr.autoIncrement;
            return false;
        }
    }
}
