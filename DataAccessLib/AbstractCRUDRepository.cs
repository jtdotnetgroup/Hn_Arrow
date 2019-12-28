using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using Dapper;
using DataAccess.CustomEnums;

namespace DataAccess
{
    public abstract class AbstractCRUDRepository<T> where T : class, new()
    {

        public DBTypeEnums DbType { get; set; }

        public AbstractCRUDRepository(DBTypeEnums dbType)
        {
            this.DbType = dbType;
            SqlBuilder.DbType = dbType;
        }

        public virtual List<T> SelectWithWhere(object where, Dictionary<string, CompareEnum> compares, IDbTransaction tran = null)
        {
            string sql = SqlBuilder.GetSelectSql<T>(where, compares);

            return Query(sql, where, tran);
        }

        public virtual List<T> Select(string sql, object where, IDbTransaction tran = null)
        {
            return Query(sql, where, tran);
        }

        public virtual T GetSingle(object where, Dictionary<string, CompareEnum> compares)
        {
            string sql = SqlBuilder.GetSelectSql<T>(where, compares);
            using (IDbConnection conn = DBConnectionFactory.GetConnection(DbType))
            {
                var result = conn.QuerySingleOrDefault<T>(sql, where);

                return result;
            }
        }

        public virtual List<T> GetAll(IDbTransaction tran = null)
        {
            string sql = SqlBuilder.GetSelectSql<T>();

            return Query(sql, tran);
        }

        public virtual T GetSingle(string sql, object where)
        {
            using (IDbConnection conn = DBConnectionFactory.GetConnection(DbType))
            {
                var result = conn.QuerySingleOrDefault<T>(sql, where);

                return result;
            }
        }

        public virtual bool Insert(T entity, IDbTransaction tran = null)
        {
            string sql = SqlBuilder.GetInsertSql(entity);
            var result = Execute(sql, entity, tran);
            return result == 1;
        }

        public virtual bool InsertBulk(List<T> entities, IDbTransaction tran = null)
        {
            string sql = SqlBuilder.GetInsertBulkSql<T>();
            var conn = tran == null ? DBConnectionFactory.GetConnection(DbType) : tran.Connection;
            var result = tran == null ? conn.Execute(sql, entities) : conn.Execute(sql, entities, tran);

            if (tran == null)
            {
                conn.Close();

            }

            return result == entities.Count;

        }

        public virtual bool Update(T entity, object where, IDbTransaction tran = null)
        {
            string sql = SqlBuilder.GetUpdateSql(entity, where);
            var result = Execute(sql, entity, tran);
            return result == 1;
        }

        public virtual bool Update(object values, object where, IDbTransaction tran = null)
        {
            string sql = SqlBuilder.GetUpdateSql(values, where);
            var result = Execute(sql, values, where, tran);

            return result>0;
        }

        public virtual bool Delete(object where, IDbTransaction tran = null)
        {
            string sql = SqlBuilder.GetDeleteSql<T>(where);

            var conn = tran == null ? DBConnectionFactory.GetConnection(DbType) : tran.Connection;
            var result = tran == null ? conn.Execute(sql, where) : conn.Execute(sql, where, tran);

            if (tran == null)
            {
                conn.Close();
            }

            return result > 0;

        }

        public int Execute(string sql, object param, IDbTransaction tran)
        {
            var conn = tran == null ? DBConnectionFactory.GetConnection(DbType) : tran.Connection;

            var result = tran == null ? conn.Execute(sql, param) : conn.Execute(sql, param, tran);

            if (tran == null)
            {
                conn.Close();

            }

            return result;
        }

        private object MegreObject(object obj1, object obj2)
        {
            var param = new ExpandoObject();

            var t1 = obj1.GetType();
            var t2 = obj2.GetType();

            var pis1 = t1.GetProperties();
            var pis2 = t2.GetProperties();

            var pis1Names = pis1.Select(p => p.Name);

            var isSameName = pis2.Count(p => pis1Names.Contains(p.Name)) > 0;

            if (isSameName)
            {
                throw new ArgumentException("值与条件对象不能有相同名称属性");
            }

            foreach (var pi in pis1)
            {
                var value = pi.GetValue(obj1, null);
                ((ICollection<KeyValuePair<string, object>>) param).Add(new KeyValuePair<string, object>(pi.Name, value));
            }

            foreach (var pi in pis2)
            {
                var value = pi.GetValue(obj2, null);
                ((ICollection<KeyValuePair<string, object>>) param).Add(new KeyValuePair<string, object>(pi.Name, value));
            }

            return param;
        }

        public int Execute(string sql, object values, object where, IDbTransaction tran)
        {
            var conn = tran == null ? DBConnectionFactory.GetConnection(DbType) : tran.Connection;

            var param = MegreObject(values, where);
            var result = tran == null ? conn.Execute(sql, param) : conn.Execute(sql, param, tran);

            if (tran == null)
            {
                conn.Close();
            }

            return result;
        }

        public T ExecuteScalar(string sql, object param, IDbTransaction tran)
        {
            var conn = tran == null ? DBConnectionFactory.GetConnection(DbType) : tran.Connection;

            var result = tran == null ? conn.ExecuteScalar<T>(sql, param) : conn.ExecuteScalar<T>(sql, param, tran);

            if (tran == null)
            {
                conn.Close();
            }
            return result;
        }

        public T ExecuteScalar(string sql, IDbTransaction tran)
        {
            var conn = tran == null ? DBConnectionFactory.GetConnection(DbType) : tran.Connection;

            var result = tran == null ? conn.ExecuteScalar<T>(sql) : conn.ExecuteScalar<T>(sql, null, tran);

            if (tran == null)
            {
                conn.Close();
            }
            return result;
        }

        public List<T> Query(string sql, object param, IDbTransaction tran)
        {
            var conn = tran == null ? DBConnectionFactory.GetConnection(DbType) : tran.Connection;

            var result = tran == null ? conn.Query<T>(sql, param) : conn.Query<T>(sql, param, tran);

            if (tran == null)
            {
                conn.Close();
            }
            return result.ToList();
        }

        public List<T> Query(string sql, IDbTransaction tran)
        {
            var conn = tran == null ? DBConnectionFactory.GetConnection(DbType) : tran.Connection;

            var result = tran == null ? conn.Query<T>(sql) : conn.Query<T>(sql, null, tran);

            if (tran == null)
            {
                conn.Close();
            }
            return result.ToList();
        }




    }
}