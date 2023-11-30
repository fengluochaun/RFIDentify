using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDentify.Com.CustAttributes
{
    /// <summary>
    /// 列名特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        public string ColumnName { get; protected set; }
        public ColumnAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }
    }
}
