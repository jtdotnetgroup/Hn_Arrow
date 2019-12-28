using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataAccess.CustomEnums;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace DataAccess
{
    public class DBConnectionFactory
    {

        public static string ConnectionName = "DefaultDB";

        public static IDbConnection GetConnection(DBTypeEnums dbType)
        {
            var conStr = AppDomain.CurrentDomain.GetData(ConnectionName);
            if (conStr == null)
            {
                conStr = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
                AppDomain.CurrentDomain.SetData(ConnectionName, conStr);
            }

            switch (dbType)
            {
                case DBTypeEnums.MYSQL:
                {
                    return new MySqlConnection(conStr.ToString());
                }

                case DBTypeEnums.SQLSERVER:
                {
                    return new SqlConnection(conStr.ToString());
                }

                case DBTypeEnums.ORACLE:
                {
                    return new OracleConnection(conStr.ToString());
                }

                default:
                {
                    throw new ArgumentException("不支持的数据库类型");
                }
            }
        }
    }
}