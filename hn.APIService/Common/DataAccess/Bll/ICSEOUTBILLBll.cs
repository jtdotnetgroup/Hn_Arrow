﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using hn.APIService.Data.Provider;
using hn.APIService.DataAccess.Model;
using hn.APIService.DataAccess.Dal;

namespace zt.DataAccess.Bll
{
    public class ICSEOUTBILLBll
    {

        public static ICSEOUTBILLBll Instance
        {
            get { return SingletonProvider<ICSEOUTBILLBll>.Instance; }
        }

        public int Update(ICSEOUTBILLModel model)
        {
            return ICSEOUTBILLDal.Instance.Update(model);
        }

        public int Delete(int FID)
        {
            return ICSEOUTBILLDal.Instance.Delete(FID);
        }

        public string GetJson(int pageindex, int pagesize, string filterJson, string sort = "FID", string order = "asc")
        {
            return ICSEOUTBILLDal.Instance.GetJson(pageindex, pagesize, filterJson, sort, order);
        }
    }
}
