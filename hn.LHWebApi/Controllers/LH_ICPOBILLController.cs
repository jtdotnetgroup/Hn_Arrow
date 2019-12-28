using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using hn.Client.Service;
using hn.DataAccess.dal.LHModel;
using hn.DataAccess.Model;

namespace hn.LHWebApi.Controllers
{
    public class LH_ICPOBILLController : ApiController
    {
        ARROW_SyncMethods methods=new ARROW_SyncMethods();

        public string SaveICPOBILL(LH_ICPOBILLMODEL input)
        {
            return "";
        }
    }
}
