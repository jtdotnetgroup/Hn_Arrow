using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CustomEnums;

namespace DataAccess
{
    public partial class SqlBuilder
    {
        public static DBTypeEnums DbType = DBTypeEnums.Default;

        public static string GetSelectSql<T>(object where = null,Dictionary<string,CompareEnum> compareOptions=null)
        {
            var tableName = GetTableName<T>();
            var fieldNames = GetFieldNames<T>();
            var whereString = GetWhereString(where, compareOptions);

            var fieldBuilder=new StringBuilder();

            foreach (var key in fieldNames.Keys)
            {
                fieldBuilder.Append(fieldNames[key]);
                if (!fieldNames.Keys.Last().Equals(key))
                {
                    fieldBuilder.Append(",");
                }
            }


            string result = $"SELECT {fieldBuilder} FROM {tableName} WHERE 1=1 {whereString}";
            return result;
        }

        public static string GetUpdateSql<T>(T entity, object where)
        {
            var tableName = GetTableName<T>();
            var whereString = GetWhereString(where,null);
            var fieldsBuilder=new StringBuilder();
            var fieldStrList = GetFieldNames(entity);
            foreach (var key in fieldStrList.Keys)
            {
                    fieldsBuilder.Append($"{fieldStrList[key]}={ParameterPrefix}{key}");
                    if (fieldStrList.Keys.Last() !=key)
                    {
                        fieldsBuilder.Append(",");
                    }
            }
            

            string result = $"UPDATE {tableName} SET {fieldsBuilder} WHERE 1=1 {whereString}";
            return result;
        }

        public static string GetUpdateSql<T>(object values, object where,Dictionary<string,CompareEnum> compareOptions=null)
        {
            var tableName = GetTableName<T>();
            var whereString = GetWhereString(where, compareOptions);
            var fieldsBuilder=new StringBuilder();
            var fieldStrList = GetFieldNames(values);
            foreach (var key in fieldStrList.Keys)
            {
                    fieldsBuilder.Append($"{fieldStrList[key]}={ParameterPrefix}{key}");
                    if (fieldStrList.Keys.Last() !=key)
                    {
                        fieldsBuilder.Append(",");
                    }
            }
            

            string result = $"UPDATE {tableName} SET {fieldsBuilder} WHERE 1=1 {whereString}";
            return result;
        }

        public static string GetDeleteSql<T>(object where,Dictionary<string,CompareEnum> compareOptions=null)
        {
            if (where == null)
            {
                throw new ArgumentException("删除条件不能为空");
            }

            var tableName = GetTableName<T>();
            var whereStr = GetWhereString(where,compareOptions);

            string result = $"DELETE FROM {tableName} WHERE 1=1 {whereStr}";
            return result;
        }

        public static string GetInsertSql<T>(T entity)
        {
            var tableName = GetTableName<T>();
            var fieldStrList = GetFieldNames(entity);
            var fieldsBuilder=new StringBuilder();
            var valuesBuilder=new StringBuilder();

            foreach (var key in fieldStrList.Keys)
            {
                fieldsBuilder.Append(fieldStrList[key]);
                valuesBuilder.Append($"{ParameterPrefix}{key}");

                if (fieldStrList.Keys.Last() != key)
                {
                    fieldsBuilder.Append(",");
                    valuesBuilder.Append(",");
                }
            }

            string result = $"INSERT INTO {tableName} ({fieldsBuilder}) values ({valuesBuilder})";
            return result;
        }

        public static string GetInsertBulkSql<T>()
        {
            var tableName = GetTableName<T>();
            var fieldStrList = GetFieldNames<T>();
            var fieldsBuilder = new StringBuilder();
            var valuesBuilder = new StringBuilder();

            foreach (var key in fieldStrList.Keys)
            {
                fieldsBuilder.Append(fieldStrList[key]);
                valuesBuilder.Append($"{ParameterPrefix}{key}");

                if (fieldStrList.Keys.Last() != key)
                {
                    fieldsBuilder.Append(",");
                    valuesBuilder.Append(",");
                }
            }
            string result = $"INSERT INTO {tableName} ({fieldsBuilder}) values ({valuesBuilder})";
            return result;
        }

    }
}
