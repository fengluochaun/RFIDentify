using com.sun.org.omg.SendingContext.CodeBasePackage;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using RFIDentify.Com;

namespace RFIDentify.DAO
{
    public class SQLiteHelper
    {
        private string dataSource = "User.db";
        private static SQLiteConnection connection;
        public SQLiteHelper(string fileName) : this()
        {
            dataSource = fileName;
        }

        public SQLiteHelper()
        {
            CreateDBFile();
            dbConnection();
        }

        /// <summary>
        /// 获取单例
        /// </summary>
        private static object syncObj = new object();
        private static SQLiteHelper instance = null;
        public static SQLiteHelper GetInstance()
        {
            if (instance == null)
            {
                lock (syncObj)
                {
                    if (instance == null)
                    {
                        instance = new SQLiteHelper();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// 创建数据库文件
        /// </summary>
        public void CreateDBFile()
        {
            string path = Environment.CurrentDirectory + @"/Data/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string databaseFileName = path + dataSource;
            if (!File.Exists(databaseFileName))
            {
                SQLiteConnection.CreateFile(databaseFileName);
            }
        }
        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="fileName">文件名</param>
        public void DeleteDBFile(string fileName)
        {
            string path = Environment.CurrentDirectory + @"/Data/";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// 生成连接字符串
        /// </summary>
        /// <returns></returns>
        private string CreateConnectionString()
        {
            SQLiteConnectionStringBuilder connectionString = new SQLiteConnectionStringBuilder();
            connectionString.DataSource = @"data/" + dataSource;//此处文件名可以使用变量表示
            string conStr = connectionString.ToString();
            return conStr;
        }
        /// <summary>
        /// 建立数据库连接
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection dbConnection()
        {
            connection = new SQLiteConnection(CreateConnectionString());
            connection.Open();
            return connection;
        }

        #region 事务控制

        /// <summary>
        /// 事务
        /// </summary>
        public SQLiteTransaction trans = null;

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            trans = connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            trans.Rollback();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            trans.Commit();
        }

        #endregion

        /// <summary>
        /// 不带参数，SQL执行语句
        /// update、delete、insert
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <returns></returns>
        public async Task<int> Execute(string sql)
        {
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    if (connection.State != System.Data.ConnectionState.Open) connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("执行出错:" + sql + "\r\n" + ex.Message);
                return 0;
            }
            finally
            {
                closeConn();
            }
        }
        /// <summary>
        /// 带参数，执行脚本
        /// insert,update,delete
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">可变参数，目的是省略了手动构造数组的过程，直接指定对象，编译器会帮助我们构造数组，并将对象加入数组中，传递过来</param>
        /// <returns></returns>
        public async Task<int> Execute(string sql, params SQLiteParameter[] parameters)
        {
            try
            {
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    if (connection.State != System.Data.ConnectionState.Open) connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandText = sql;
                    if (parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("执行出错:" + sql + "\r\n" + ex.Message);
                return 0;
            }
            finally
            {
                closeConn();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="TableName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<List<T>> Query<T>(string TableName, string where = "") where T : new()
        {
            try
            {
                List<T> datas = new List<T>();
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    if (connection.State != System.Data.ConnectionState.Open) connection.Open();
                    cmd.Connection = connection;
                    cmd.CommandText = "select * from " + TableName + " " + where;
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                    if (properties.Length <= 0)
                    {
                        throw new Exception("类属性长度为零");
                    }
                    foreach (DataRow dd in dt.Rows)
                    {
                        int i = 0;
                        var model = new T();
                        foreach (System.Reflection.PropertyInfo item in properties)
                        {
                            var value = dd[i++];
                            if (value is DBNull)
                            {
                                item.SetValue(model, null, null);
                            }
                            else
                            {
                                if(item.PropertyType == typeof(System.Drawing.Image))
                                {
                                    var ds = Convert.ChangeType(ImgUtil.Byte2Image(value), item.PropertyType);
                                    item.SetValue(model, ds, null);
                                }
                                else
                                {
                                    var ds = Convert.ChangeType(value, item.PropertyType);
                                    item.SetValue(model, ds, null);
                                }
                                
                            }
                        }
                        datas.Add(model);
                    }
                }
                return datas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("查询出错:" + TableName + where + "\r\n" + ex.Message);
                //Logger.Error("查询出错:" + TableName + where + "\r\n" + ex.Message);
                return new List<T>();
            }
            finally
            {
                closeConn();
            }
        }
        /// <summary>
        /// 查询，完整的sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<List<T>> Query<T>(string sql) where T : new()
        {
            try
            {
                List<T> datas = null;
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    if (connection.State != System.Data.ConnectionState.Open) connection.Open();
                    cmd.CommandText = sql;
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                    if (properties.Length <= 0)
                    {
                        throw new Exception("类属性长度为零");
                    }
                    foreach (DataRow dd in dt.Rows)
                    {
                        int i = 0;
                        var model = new T();
                        foreach (System.Reflection.PropertyInfo item in properties)
                        {
                            var value = dd[i++];
                            if (value is DBNull)
                            {
                                item.SetValue(model, 0M, null);
                            }
                            else
                            {
                                var ds = Convert.ChangeType(value, item.PropertyType);
                                item.SetValue(model, ds, null);
                            }
                        }
                        datas.Add(model);
                    }
                }
                return datas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("查询出错:" + sql + "\r\n" + ex.Message);
                //Logger.Error("查询出错:" + TableName + where + "\r\n" + ex.Message);
                return new List<T>();
            }
            finally
            {
                closeConn();
            }
        }
        /// <summary>
        /// 查询 带参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<List<T>> Query<T>(string sql, params SQLiteParameter[] parameters) where T : new()
        {
            try
            {
                List<T> datas = null;
                using (SQLiteCommand cmd = new SQLiteCommand())
                {
                    if (connection.State != System.Data.ConnectionState.Open) connection.Open();
                    cmd.CommandText = sql;
                    if (parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                    if (properties.Length <= 0)
                    {
                        throw new Exception("类属性长度为零");
                    }
                    foreach (DataRow dd in dt.Rows)
                    {
                        int i = 0;
                        var model = new T();
                        foreach (System.Reflection.PropertyInfo item in properties)
                        {
                            var value = dd[i++];
                            if (value is DBNull)
                            {
                                item.SetValue(model, 0M, null);
                            }
                            else
                            {
                                var ds = Convert.ChangeType(value, item.PropertyType);
                                item.SetValue(model, ds, null);
                            }
                        }
                        datas.Add(model);
                    }
                }
                return datas;
            }
            catch (Exception ex)
            {
                Console.WriteLine("查询出错:" + sql + "\r\n" + ex.Message);
                //Logger.Error("查询出错:" + TableName + where + "\r\n" + ex.Message);
                return new List<T>();
            }
            finally
            {
                closeConn();
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void closeConn()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                else if (connection.State == ConnectionState.Broken)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("closeConnErr:" + ex);
            }
        }
    }
}
