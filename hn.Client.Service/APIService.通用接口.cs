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
        public List<SelectModel> Select_List()
        {
            return SelectDal.Instance.Select_List();
        }
        public DataTable Select_DataTable()
        {
            return SelectDal.Instance.Select_DataTable();
        }
    }
}