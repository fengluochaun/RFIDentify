using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFIDentify.Models;
using RFIDentify.Com;
using System.Data.SQLite;
using System.Data;
using com.sun.tools.corba.se.idl;
using System.Reflection;

namespace RFIDentify.DAO
{
    public class UserDao
    {
        private SQLiteHelper SQLiteHelper = SQLiteHelper.GetInstance();
        public async Task<int> AddUser(User user)
        {
            string tableName = user.GetType().GetTName();
            byte[] picData = null;
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@name", user.Name),
                new SQLiteParameter("@age", user.Age),
                new SQLiteParameter("@description", user.Description),
                new SQLiteParameter("@telephone", user.Telephone)
            };
            if (user.Picture != null)
            {
                picData = ImgUtil.ImageToByte(user.Picture);
                SQLiteParameter sqliteParameter = new SQLiteParameter("@picData", DbType.Object, picData!.Length);
                sqliteParameter.Value = picData;
                parameters.Append(sqliteParameter);
            }            
            
            //string sql = $"insert into [{tableName} ('name','age','description','telephone','picture') " +
            //    $"values ('@name','@age','@description','@telephone','@picture')]";
            string sql = $"insert into [{tableName}] ('name','age','description','telephone') " +
                $"values (@name,@age,@description,@telephone)";
            return await this.SQLiteHelper.Execute(sql, parameters) ;
        }

        public async Task<List<User>> GetUserById(int id)
        {
            string where = $"where 1=1 and id = {id}";
            return await this.SQLiteHelper.Query<User>("User", where);
        }

        public async Task<List<User>> GetAllUser()
        {
            return await this.SQLiteHelper.Query<User>("User", "where 1=1");
        }

        public async Task<int> UpdateUser(User user)
        {
            byte[] picData = null;
            if (user.Picture != null)
            {
                picData = ImgUtil.ImageToByte(user.Picture);
            }
            SQLiteParameter sQLiteParameter = new SQLiteParameter("@picData", DbType.Object, picData.Length);
            sQLiteParameter.Value = picData;
            SQLiteParameter[] parameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@name", user.Name),
                new SQLiteParameter("@age", user.Age),
                new SQLiteParameter("@description", user.Description),
                new SQLiteParameter("@telephone", user.Telephone),
                sQLiteParameter
            };
            string where = $"id = {user.Id}";
            PropertyInfo[] properties = typeof(User).GetProperties();
            string strCols = string.Join(",", properties.Where(p => "Id".Contains(p.GetColName())).Select(p => string.Format("{0}=@{0}", p.GetColName())));
            string sql = $"update [User] set {strCols} where {where} ";
            return await this.SQLiteHelper.Execute(sql, parameters);
        }

        public async Task<int> GetUserNum()
        {
            string sql = "select COUNT(*) from User where 1=1";
            int a = await this.SQLiteHelper.ExecuteScalar(sql);
            return a ;
        }
    }
}
