using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hn.Common.Data;
using hn.Common.Provider;
using hn.DataAccess.model.Common;
using System.Data;

namespace hn.DataAccess.dal.Common
{
    /// <summary>
    /// 下拉选择器
    /// </summary>
    public class SelectDal: BaseRepository<SelectModel>
    {
        public static SelectDal Instance
        {
            get { return SingletonProvider<SelectDal>.Instance; }
        }
        /// <summary>
        /// 下拉选择器
        /// </summary>
        /// <param name="vSql"></param>
        /// <returns></returns>
        public List<SelectModel> Select_List()
        {
            return DbUtils.Query<SelectModel>(@"SELECT DISTINCT t.headid || '/' || t.policyname AS fid,t.headid AS fname FROM lh_policy t").ToList();
        }
        public DataTable Select_DataTable()
        {
            return DbUtils.Query("SELECT DISTINCT t.headid || '/' || t.policyname AS fid,t.headid AS fname FROM lh_policy t");
        } 
    }
}
