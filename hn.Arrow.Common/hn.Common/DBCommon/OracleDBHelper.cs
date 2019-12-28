using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;

[assembly: AssemblyVersion("1.0")]
namespace hn.Common_Arrow
{
    public class OracleDBHelper
    {
        private static readonly OracleClientFactory factory = new OracleClientFactory();

        private string conStr { get; set; }

        public OracleDBHelper(string conStr)
        {
            this.conStr = conStr;
        }

        public OracleDBHelper()
        {
            this.conStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        }

        public DbConnection GetNewConnection()
        {
            var con = factory.CreateConnection();
            con.ConnectionString = this.conStr;
            return con;
        }

        //public DbConnection conn { get; set; }

        /// <summary>
        /// 用于执行没有返回数据的SQL语句，如UPDATE或INSERT、DELETE类
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, Dictionary<string, object> pars)
        {
            try
            {
                var conn = GetNewConnection();
                conn.Open();

                var cmd = factory.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                foreach (var key in pars.Keys) cmd.Parameters.Add(new OracleParameter(key, pars[key]));


                var result= cmd.ExecuteNonQuery();

                conn.Close();
                return result;
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                LogHelper.Info("SQL:" + sql);
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语并返回首行首列值，例如：SELECT COUNT()/MAX()/MIN()等
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, Dictionary<string, object> pars)
        {
            var conn = GetNewConnection();
            conn.Open();
            try
            {
              

                var cmd = factory.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                foreach (var key in pars.Keys) cmd.Parameters.Add(new OracleParameter(key, pars[key]));

                var result= cmd.ExecuteScalar();
                conn.Close();
                return result;
            }
            catch (Exception e)
            {
                conn.Close();
                LogHelper.Error(e);
                LogHelper.Info("SQL:" + sql);
                throw;
            }
        }

        /// <summary>
        /// 根据实体对象获取插入SQL语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string GetInsertSql<T>()
        {
            var t = typeof(T);

            var pis = t.GetProperties().Where(p =>
                p.GetCustomAttributes(true).Count(pi => pi.GetType() == typeof(NotMappedAttribute)) == 0).ToList();

            var tableAttr =
                t.GetCustomAttributes(true)
                    .FirstOrDefault(p => p.GetType() == typeof(TableAttribute)) as TableAttribute;
            var tableName = tableAttr.Name;

            var strbuilder = new StringBuilder();
            strbuilder.AppendFormat("INSERT INTO {0} ", tableName);

            var fields = "(";
            var values = " VALUES (";

            pis.ForEach(p =>
            {
                var fieldName = p.Name;

                if (p.GetCustomAttributes(true).SingleOrDefault(o => o.GetType() == typeof(ColumnAttribute)) is
                    ColumnAttribute column)
                    fieldName = column.Name;

                fields += fieldName;

                values += ":" + p.Name;

                if (p == pis.Last())
                {
                    fields += ")";
                    values += ")";
                }
                else
                {
                    fields += ",";
                    values += ",";
                }
            });

            strbuilder.Append(fields);
            strbuilder.Append(values);

            return strbuilder.ToString();
        }

        public string GetInsertSql<T>(T entity)
        {
            var t = typeof(T);

            var pis = t.GetProperties().Where(p =>
                p.GetCustomAttributes(true).Count(pi => pi.GetType() == typeof(NotMappedAttribute)) == 0).ToList();

            var tableAttr =
                t.GetCustomAttributes(true)
                    .FirstOrDefault(p => p.GetType() == typeof(TableAttribute)) as TableAttribute;
            var tableName = tableAttr.Name;

            var strbuilder = new StringBuilder();
            strbuilder.AppendFormat("INSERT INTO {0} ", tableName);

            var fields = "(";
            var values = " VALUES (";

            pis.ForEach(p =>
            {
                var fieldName = p.Name;

                var value = p.GetValue(entity, null);

                if (value != null)
                {
                    if (p.GetCustomAttributes(true).SingleOrDefault(o => o.GetType() == typeof(ColumnAttribute)) is
                        ColumnAttribute column)
                        fieldName = column.Name;

                    fields += fieldName;
                    values += $"'{value}'";

                    if (p != pis.Last())
                    {
                        fields += ",";
                        values += ",";
                    }
                }

                if (p == pis.Last())
                {

                    if (fields.Last() == ',')
                    {
                        fields = fields.Substring(0, fields.Length - 1);
                    }
                    if (values.Last() == ',')
                    {
                        values = values.Substring(0, values.Length - 1);
                    }

                    fields += ")";
                    values += ")";
                }
            });

            strbuilder.Append(fields);
            strbuilder.Append(values);

            return strbuilder.ToString();
        }

        /// <summary>
        /// 根据实体对像获取UPDATE语句
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="where">更新条件</param>
        /// <returns></returns>
        public string GetUpdateSql<T>(string where)
        {
            var t = typeof(T);

            var pis = t.GetProperties().Where(p =>
                p.GetCustomAttributes(true).Count(pi => pi.GetType() == typeof(NotMappedAttribute)) == 0).ToList();

            var tableAttr =
                t.GetCustomAttributes(true)
                    .FirstOrDefault(p => p.GetType() == typeof(TableAttribute)) as TableAttribute;
            var tableName = tableAttr.Name;

            var strbuilder = new StringBuilder();
            strbuilder.AppendFormat("UPDATE {0} SET ", tableName);

            var fields = "";

            pis.ForEach(p =>
            {
                var fieldName = p.Name;

                if (p.GetCustomAttributes(true).SingleOrDefault(o => o.GetType() == typeof(ColumnAttribute)) is
                    ColumnAttribute column)
                    fieldName = column.Name;

                fields += fieldName + "=:" + p.Name;

                if (p == pis.Last())
                    fields += " WHERE 1=1 ";
                else
                    fields += ",";
            });

            strbuilder.Append(fields);
            strbuilder.Append(where);
            return strbuilder.ToString();
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">实体数据</param>
        /// <param name="where">更新条件</param>
        /// <returns></returns>
        public bool Update<T>(T obj, string where)
        {
            var sql = GetUpdateSql<T>(where);
            var cmd = GetCommand(sql, obj);
            var conn = GetNewConnection();
            conn.Open();
            try
            {
               
                cmd.Connection = conn;

                var result= cmd.ExecuteNonQuery() > 0;
                conn.Close();
                return result;
            }
            catch (Exception e)
            {
                conn.Close();
                LogHelper.Error(e);
                LogHelper.Info("SQL:" + sql);
                throw;
            }
        }

        /// <summary>
        /// 更新实体数据，更新条件为实体类型的Key特性字段
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">实体数据</param>
        /// <returns></returns>
        public bool Update<T>(T obj)
        {
            var t = typeof(T);
            var pis = t.GetProperties();

            var keyFieldName = "";
            PropertyInfo keyPropertyInfo = null;

            foreach (var pi in pis)
            {
                var keyAttr = pi.GetCustomAttributes(true).Count(p => p is KeyAttribute) == 1;
                if (keyAttr)
                {
                    keyFieldName = pi.Name;

                    //如果属性中有Column特性，则把Column.Name作为键字段名
                    if (pi.GetCustomAttributes(true).SingleOrDefault(o => o.GetType() == typeof(ColumnAttribute)) is
                        ColumnAttribute column)
                        keyFieldName = column.Name;

                    keyPropertyInfo = pi;

                    break;
                }
            }

            if (string.IsNullOrEmpty(keyFieldName)) throw new ArgumentException(string.Format("{0}类未指定Key字段", t.Name));

            var where = string.Format(" AND {0}=:{1}", keyFieldName, keyPropertyInfo.Name);

            var sql = GetUpdateSql<T>(where);
            var cmd = GetCommand(sql, obj);
            var conn = GetNewConnection();
            conn.Open();

            try
            {
                
                cmd.Connection = conn;
                var result= cmd.ExecuteNonQuery() > 0;

                conn.Close();
                return result;
            }
            catch (Exception e)
            {
                conn.Close();
                
                LogHelper.Error(e);
                LogHelper.Info("SQL:" + sql);
                throw;
            }
        }

        public bool UpdateWithTransation<T>(T obj, DbTransaction tran)
        {
            var t = typeof(T);
            var pis = t.GetProperties();

            var keyFieldName = "";
            PropertyInfo keyPropertyInfo = null;

            foreach (var pi in pis)
            {
                var keyAttr = pi.GetCustomAttributes(true).Count(p => p is KeyAttribute) == 1;
                if (keyAttr)
                {
                    keyFieldName = pi.Name;

                    //如果属性中有Column特性，则把Column.Name作为键字段名
                    if (pi.GetCustomAttributes(true).SingleOrDefault(o => o.GetType() == typeof(ColumnAttribute)) is
                        ColumnAttribute column)
                        keyFieldName = column.Name;

                    keyPropertyInfo = pi;

                    break;
                }
            }

            if (string.IsNullOrEmpty(keyFieldName)) throw new ArgumentException(string.Format("{0}类未指定Key字段", t.Name));

            var where = string.Format(" AND {0}=:{1}", keyFieldName, keyPropertyInfo.Name);

            var sql = GetUpdateSql<T>(where);

            var cmd = GetCommand(sql, obj);
            cmd.Connection = tran.Connection;
            cmd.Transaction = tran;

            try
            {
                var result= cmd.ExecuteNonQuery() > 0;

                return result;
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                LogHelper.Info("SQL:" + sql);
                throw;
            }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">插入的实体数据</param>
        /// <returns></returns>
        public bool Insert<T>(T obj)
        {
            var sql = GetInsertSql<T>();
            var cmd = GetCommand(sql, obj);
            var conn = GetNewConnection();
            conn.Open();

            try
            {
                
                cmd.Connection = conn;
                var result = cmd.ExecuteNonQuery();
                conn.Close();

                return result > 0; ;
            }
            catch (Exception e)
            {
                conn.Close();
                
                LogHelper.Error(e);
                LogHelper.Info($"数据：{JsonConvert.SerializeObject(obj)}");
                LogHelper.Info("SQL:" + sql);
                throw;
            }
        }


        /// <summary>
        /// 插入数据，带事务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public bool InsertWithTransation<T>(T obj, DbTransaction tran)
        {
            var sql = GetInsertSql<T>();
            var cmd = GetCommand(sql, obj);
            cmd.Connection = tran.Connection;
            cmd.Transaction = tran;

            try
            {
                var result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                LogHelper.Error($"数据：{JsonConvert.SerializeObject(obj)}");
                LogHelper.Error($"实体类型：{typeof(T).Name}");
                LogHelper.Error("SQL:" + sql);
                throw;
            }
        }

        /// <summary>
        /// 根据实体类型获取DBCommand
        /// </summary>
        /// <typeparam name="T">实体数据类型</typeparam>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="par">替换进SQL的实际数据</param>
        /// <returns></returns>
        private DbCommand GetCommand<T>(string sql, T par)
        {
            var cmd = factory.CreateCommand();
            cmd.CommandText = sql;
            

            var t = typeof(T);
            var pis = t.GetProperties().Where(p =>
                p.GetCustomAttributes(true).Count(pi => pi.GetType() == typeof(NotMappedAttribute)) == 0).ToList();
            LogHelper.Debug($"实体类：{t.Name}");
            LogHelper.Debug($"实体数据：{JsonConvert.SerializeObject(par)}");
            LogHelper.Debug($"SQL:{sql}");
            pis.ForEach(p =>
            {
                var value = p.GetValue(par) ?? "";
                LogHelper.Debug($"SQL参数：{p.Name}：{value}");
                cmd.Parameters.Add(new OracleParameter($":{p.Name}" , value));
            });

            LogHelper.Debug("===============================分割线========================================================");

            return cmd;
        }

        /// <summary>
        /// 根据实体类型获取DBCommand
        /// </summary>
        /// <typeparam name="T">实体数据类型</typeparam>
        /// <param name="sql">要执行的SQL语句</param>
        /// <param name="par">替换进SQL的实际数据</param>
        /// <returns></returns>
        private DbCommand GetCommand<T>(string sql, List<T> pars)
        {
            var cmd = factory.CreateCommand();
            cmd.CommandText = sql;
            

            var t = typeof(T);
            var pis = t.GetProperties().Where(p => p.GetCustomAttributes(true).Count(c => c is NotMappedAttribute) == 0).ToList();

            pars.ForEach(par =>
            {
                pis.ForEach(p =>
                {
                    var value = p.GetValue(par, null);
                    if (value == null)
                        value = "";

                    cmd.Parameters.Add(new OracleParameter(":" + p.Name, value));
                });
            });


            return cmd;
        }

        public int Delete<T>(string where)
        {
            var t = typeof(T);

            var pis = t.GetProperties().ToList();

            var tableAttr =
                t.GetCustomAttributes(true)
                    .FirstOrDefault(p => p.GetType() == typeof(TableAttribute)) as TableAttribute;
            var tableName = tableAttr.Name;

            var sql = string.Format("DELETE FROM {0} WHERE 1=1 {1}", tableName, where);

            var cmd = factory.CreateCommand();
            cmd.CommandText = sql;
            var conn = GetNewConnection();
            conn.Open();

            try
            {
              
                cmd.Connection = conn;
                var result= cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
            catch (Exception e)
            {
                conn.Close();
                LogHelper.Error(e);
                LogHelper.Info("SQL:" + sql);
                throw;
            }
        }

        public int DeleteWithTran<T>(string where, DbTransaction tran)
        {
            var t = typeof(T);

            var tableAttr =
                t.GetCustomAttributes(true)
                    .FirstOrDefault(p => p.GetType() == typeof(TableAttribute)) as TableAttribute;
            var tableName = tableAttr.Name;

            var sql = string.Format("DELETE FROM {0} WHERE 1=1 {1}", tableName, where);

            var cmd = tran.Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.Transaction = tran;

            LogHelper.Debug($"SQL:{sql}");

            try
            {
               
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                LogHelper.Info("SQL:" + sql);
                throw;
            }
        }

        public int DeleteWithTran<T>(T obj, DbTransaction tran)
        {
            var t = typeof(T);
            var pis = t.GetProperties().ToList();
            var tableAttr =
                t.GetCustomAttributes(true)
                    .FirstOrDefault(p => p.GetType() == typeof(TableAttribute)) as TableAttribute;
            var tableName = tableAttr.Name;

            PropertyInfo keyPropertyInfo = pis.SingleOrDefault(p => p.GetCustomAttributes(true).Count(c => c is KeyAttribute) == 1);
            if (keyPropertyInfo == null)
            {
                throw new ArgumentException(string.Format("{0}类未指定Key字段", t.Name));
            }

            string keyFieldName = "";
            var keyValue = keyPropertyInfo.GetValue(obj, null);
            if (keyValue == null)
            {
                throw  new ArgumentException($"主键【{keyFieldName}】值不能为空");
            }

            var par = factory.CreateParameter();
            par.ParameterName = $":{keyFieldName}";
            par.Value = keyValue;
            keyFieldName = keyPropertyInfo.Name;

            if (keyPropertyInfo.GetCustomAttributes(true).SingleOrDefault(o => o.GetType() == typeof(ColumnAttribute)) is
                ColumnAttribute column)
                keyFieldName = column.Name;

            var sql = $"DELETE FROM {tableName} WHERE {keyFieldName}=:{keyPropertyInfo.Name}";

            var cmd = tran.Connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add(par);

            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                LogHelper.Info("SQL:" + sql);
                throw;
            }
        }


        public DataTable Select(string sql)
        {
            var conn = GetNewConnection();
            conn.Open();
            var cmd = factory.CreateCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
            var da = factory.CreateDataAdapter();
            da.SelectCommand = cmd;
            var table = new DataTable();
            try
            {
                da.Fill(table);
            }
            catch (Exception e)
            {
                conn.Close();
                
                LogHelper.Info($"SQL:{sql}\n异常：{e.Message}");
                throw;
            }

            conn.Close();
            

            return table;
        }

        private List<T> DataTableToList<T>(DataTable table) where T : new()
        {
            //反射获得泛型类信息
            var t = typeof(T);
            //获得泛型类所有公共字段
            var pisInfos = t.GetProperties().ToList();
            //最终返回的对象列表
            var result = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                var item = new T();
                foreach (DataColumn col in table.Columns)
                {
                    //这里假设字段名和数据库列名是一 一对应
                    //可能通过字段名与列名进行比较，相同则进行取值赋值操作
                    var pi = pisInfos.FirstOrDefault(p =>
                    {
                        if (p.Name.ToUpper() == col.ColumnName.ToUpper()) return true;
                        //如果列名与字段不是一一对应的，则反射字段Column特性，获取Column的Name值与列名进行比较

                        return p.GetCustomAttributes(true).FirstOrDefault(f => f.GetType() == typeof(ColumnAttribute)) is ColumnAttribute attr && attr.Name.ToUpper() == col.ColumnName.ToUpper();
                    });
                    if (pi != null)
                    {
                        var value = row[col.ColumnName];

                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {

                            pi.SetValue(item, value, null);

                        }
                        else
                        {
                            if (pi.PropertyType.Name == typeof(string).Name)
                            {
                                pi.SetValue(item,"");
                            }
                        }
                    }
                }

                result.Add(item);
            }

            return result;
        }

        public List<T> Select<T>(string sql) where T : new()
        {
            using (var conn = GetNewConnection())
            {
                conn.Open();
                var cmd = factory.CreateCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                var da = factory.CreateDataAdapter();
                da.SelectCommand = cmd;
                var table = new DataTable();
                da.Fill(table);

                var result = DataTableToList<T>(table);

                return result;
            } ;
           
        }

        public T Get<T>(string sql) where T : new()
        {
            var conn = GetNewConnection();
            conn.Open();
            var cmd = factory.CreateCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
            var da = factory.CreateDataAdapter();
            da.SelectCommand = cmd;
            var table = new DataTable();
            da.Fill(table);
            try
            {
                var result = DataTableToList<T>(table).SingleOrDefault();
                conn.Close();
                return result;
            }
            catch (InvalidOperationException e)
            {
                LogHelper.Error(e);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool BatchInsert<T>(List<T> data, DbTransaction tran = null)
        {
            var start = DateTime.Now;
            if (data == null || data.Count == 0) return false;
            var builder = new StringBuilder();
            builder.Append("INSERT ALL \n");
            var str = GetInsertSql<T>().Replace("INSERT", "");

            data.ForEach(p =>
            {
                builder.Append($"{str}\n");
            });

            builder.Append(" SELECT 1 FROM DUAL");

            var sql = builder.ToString();
            var cmd = GetCommand(sql, data);
            var conn = GetNewConnection();
            conn.Open();

            if (tran != null)
            {
                cmd.Connection = tran.Connection;
                cmd.Transaction = tran;
            }

            try
            {
                //LogHelper.Info($"批量插入数据【{typeof(T).Name}】【{data.Count}】条");


                var result = cmd.ExecuteNonQuery();
                conn.Close();
                var timespan = DateTime.Now - start;
                //LogHelper.Info($"批量插入完成，耗时【{timespan.TotalMilliseconds}】毫秒");
                return result > 0;
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                LogHelper.Info($"插入失败数据");
                return false;
            }
            finally
            {
                conn.Close();
            }

        }

        public bool BatchUpdate<T>(List<T> data, string where)
        {
            if (data == null || data.Count == 0) return false;
            var now = DateTime.Now;
            var sql = GetUpdateSql<T>(where);
            var conn = GetNewConnection();
            conn.Open();
            try
            {
                data.ForEach(row =>
                {
                    var cmd = GetCommand(sql, row);
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                });
                var timespan = DateTime.Now - now;
                LogHelper.Info($"批量更新完成耗时：{timespan.Hours}时{timespan.Minutes}分{timespan.Seconds}秒，共更新{data.Count}条数据");
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                LogHelper.Info($"批量更新失败\n异常：{e.Message}");
                LogHelper.Info("SQL:" + sql);
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public string GetSelectSql<T>()
        {
            var t = typeof(T);
            var tableAttr =
                t.GetCustomAttributes(true)
                    .FirstOrDefault(p => p.GetType() == typeof(TableAttribute)) as TableAttribute;
            var tableName = tableAttr == null ? t.Name : tableAttr.Name;

            var builder = new StringBuilder();
            builder.Append("SELECT * FROM ");
            builder.Append(tableName);
            builder.Append(" Where 1=1");

            return builder.ToString();
        }

        public string GetSelectSqlWithDistinct<T>()
        {
            var t = typeof(T);
            var tableAttr =
                t.GetCustomAttributes(true)
                    .FirstOrDefault(p => p.GetType() == typeof(TableAttribute)) as TableAttribute;
            var tableName = tableAttr == null ? t.Name : tableAttr.Name;

            var builder = new StringBuilder();
            builder.Append("SELECT Distinct * FROM ");
            builder.Append(tableName);
            builder.Append(" Where 1=1");

            return builder.ToString();
        }

        public T Get<T>(object id) where T : new()
        {
            var conn = GetNewConnection();
            conn.Open();
            var sql = GetSelectSql<T>();
            var t = typeof(T);
            var pis = t.GetProperties();

            var keyFieldName = "";

            foreach (var pi in pis)
            {
                var keyAttr = pi.GetCustomAttributes(true).FirstOrDefault(p => p.GetType() == typeof(KeyAttribute));

                if (keyAttr != null)
                {
                    keyFieldName = pi.Name;
                    break;
                }
            }

            if (string.IsNullOrEmpty(keyFieldName)) throw new ArgumentException(string.Format("{0}类未指定Key字段", t.Name));

            sql += string.Format(" AND {0}=", keyFieldName) + id;

            var result = Select<T>(sql).FirstOrDefault();

            conn.Close();

            return result;
        }

        public List<T> GetAll<T>() where T : new()
        {
            var sql = GetSelectSql<T>();
            return Select<T>(sql);
        }


        public List<T> GetListWithDisdinct<T>() where T : new()
        {
            var sql = GetSelectSqlWithDistinct<T>();
            return Select<T>(sql);
        }

        public int Count<T>(string where = "")
        {
            var t = typeof(T);
            var tableAttr =
                t.GetCustomAttributes(true)
                    .FirstOrDefault(p => p.GetType() == typeof(TableAttribute)) as TableAttribute;
            var tableName = tableAttr == null ? t.Name : tableAttr.Name;

            var sql = $"SELECT COUNT(*) FROM {tableName} WHERE 1=1 {where}";

            var conn = GetNewConnection();
            conn.Open();

            try
            {
                var cmd = factory.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                int result = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                return result;
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                throw;
            }
            finally
            {
                conn.Close();
            }

           
        }

        public string GetWhereStr<T>(T where)
        {
            var sql = "";
            var builder = new StringBuilder();
            builder.Append(sql);

            var t = where.GetType();
            var pis = t.GetProperties().Where(p =>
                p.GetCustomAttributes(true).Count(pi => pi.GetType() == typeof(NotMappedAttribute)) == 0);
            //var cmd = factory.CreateCommand();

            foreach (var pi in pis)
            {
                var value = pi.GetValue(where, null);
                if (value != null&&!string.IsNullOrEmpty(value.ToString()))
                {
                    var fieldName = pi.Name;

                    if (pi.GetCustomAttributes(true).FirstOrDefault(p => p.GetType() == typeof(ColumnAttribute)) is
                        ColumnAttribute attr) fieldName = attr.Name;

                    //cmd.Parameters.Add(new OracleParameter(pi.Name, value));

                    builder.Append(" AND ");
                    builder.Append(fieldName);

                    var compare = pi.PropertyType == typeof(string)?$" LIKE '%{value}%'":$"={value}";

                    builder.Append(compare);
                }
            }

            sql = builder.ToString();
            return sql;
        }

        private DbCommand GetCommand<T>(T where)
        {
            var sql = GetSelectSql<T>();
            var cmd = factory.CreateCommand();
            sql += GetWhereStr(where);
            cmd.CommandText = sql;
            return cmd;
        }

        public string GetSelectSql<T>(object wherer)
        {
            var sql = GetSelectSql<T>();
            var builder = new StringBuilder();
            builder.Append(sql);
            builder.Append(" WHERE 1=1 ");

            var t = wherer.GetType();
            var pis = t.GetProperties();

            foreach (var pi in pis)
            {
                var value = pi.GetValue(wherer, null);
                if (value != null)
                {
                    var fieldName = pi.Name;

                    if (pi.GetCustomAttributes(true).FirstOrDefault(p => p.GetType() == typeof(ColumnAttribute)) is
                        ColumnAttribute attr) fieldName = attr.Name;

                    builder.Append(" AND ");
                    builder.Append(fieldName);
                    builder.Append("=:");
                    builder.Append(pi.Name);
                }
            }

            return builder.ToString();
        }

        public List<T> GetWhere<T>(T condiction) where T : new()
        {
            var conn = GetNewConnection();
            conn.Open();

            var cmd = GetCommand(condiction);
            cmd.Connection = conn;
            var da = factory.CreateDataAdapter();
            da.SelectCommand = cmd;


            var data = new DataTable();
            da.Fill(data);

            var result = DataTableToList<T>(data);

            conn.Close();

            return result;
        }

        public List<T> GetWhere<T>(T condiction,int index,int size) where T : new()
        {
            var conn = GetNewConnection();
            conn.Open();

            var cmd = GetCommand(condiction);
            cmd.Connection = conn;
            cmd.CommandText += $" AND ROWNUM>{(index-1)*size} AND ROWNUM<={index*size}";
            var da = factory.CreateDataAdapter();
            da.SelectCommand = cmd;

            var data = new DataTable();
            da.Fill(data);

            var result = DataTableToList<T>(data);
            conn.Close();

            return result;
        }

        private DbCommand SetCommandParam<T>(T condition,DbCommand cmd)
        {
            if (condition != null)
            {
                var t = typeof(T);
                var pis = t.GetProperties().ToList();
                pis.ForEach(p =>
                {
                    var value = p.GetValue(condition, null);
                    if (value != null)
                    {
                        var fieldName = p.Name;
                        if (p.GetCustomAttributes(true).FirstOrDefault(att => att.GetType() == typeof(ColumnAttribute)) is
                            ColumnAttribute attr) fieldName = attr.Name;

                        cmd.Parameters.Add(new OracleParameter(fieldName, value));
                    }
                });
            }
            return cmd;
        }


        public List<T> GetWithWhereStr<T>(string @where,T condition=default(T)) where T : new()
        {
            string sql = GetSelectSql<T>();
            sql += where;
            var conn = GetNewConnection();
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            cmd = SetCommandParam(condition,cmd);
            
            using (var da = factory.CreateDataAdapter())
            {
                da.SelectCommand = cmd;
                var datatable = new DataTable();
                da.Fill(datatable);
                var result = DataTableToList<T>(datatable);

                conn.Close();
                return result;
            }
        }

        public List<T> GetWithWhereStrByPage<T>(string @where,int index =1,int size=50) where T : new()
        {
            string sql = GetSelectSql<T>();
            sql += where;
            sql+= $" AND ROWNUM>{(index - 1) * size} AND ROWNUM<={index * size}";
            var conn = GetNewConnection();
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            using (var da = factory.CreateDataAdapter())
            {
                da.SelectCommand = cmd;
                var datatable = new DataTable();
                da.Fill(datatable);
                var result = DataTableToList<T>(datatable);

                conn.Close();
                return result;
            }
        }

        public List<T> GetWithWhereStrByPage<T>(string @where, object condition,int index = 1, int size = 50) where T : new()
        {
            string sql = GetSelectSql<T>();
            sql += where;
            //sql += $" AND ROWNUM>{(index - 1) * size} AND ROWNUM<={index * size}";
            var conn = GetNewConnection();
            conn.Open();

            sql = $"SELECT t.*,ROWNUM AS ROWNO FROM ({sql}) t ";
            sql = $"SELECT * FROM ({sql}) WHERE ROWNO>{(index - 1) * size} AND ROWNO<={index * size}";

            var cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            LogHelper.Info(sql);
            using (var da = factory.CreateDataAdapter())
            {
                da.SelectCommand = cmd;
                var datatable = new DataTable();
                da.Fill(datatable);
                var result = DataTableToList<T>(datatable);

                conn.Close();
                return result;
            }
        }

        public List<T> GetWithTranWithWhereStr<T>(string where,DbTransaction tran) where T : new()
        {
            string sql = GetSelectSql<T>();
            sql += where;
            if (tran.Connection.State == ConnectionState.Closed) tran.Connection.Open();

            var cmd = tran.Connection.CreateCommand();
            cmd.CommandText = sql;
            using (var da = factory.CreateDataAdapter())
            {
                da.SelectCommand = cmd;
                var datatable = new DataTable();
                da.Fill(datatable);
                var result = DataTableToList<T>(datatable);
                
                return result;
            }

        }
    }
}