//using hn.Client.Service.AutoSync.Model;
using hn.Client.Service.Common;
using hn.DataAccess.dal.Common;
using hn.DataAccess.model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using hn.ArrowInterface.Entities;
using hn.Common_Arrow;

namespace hn.Client.Service
{
    /// <summary>
    /// 通用接口管理
    /// </summary>
    public partial class APIService
    {
        public APIService() {}
        DBHelper db;
        string vSQL;
        DataTable dt;
        /// <summary>
        /// 返回下拉列表
        /// </summary>
        /// <param name="vSql"></param>
        /// <returns></returns>
        public List<LH_Policy> Select_List()
        {
            var helper = new OracleDBHelper();
        
            var list = helper.Select<LH_Policy>(@"SELECT DISTINCT HEADID,POLICYNAME,ORDERTYPE,ORDERSUBTYPE,PRODCHANNEL,DEPTNAME from LH_POLICY");

            return list;

        }
        public DataTable Select_DataTable()
        {
            return SelectDal.Instance.Select_DataTable();
        }
    }
}