using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using AutoMapper;
using AutoMapper.Configuration;
using hn.ArrowInterface.Entities;
using hn.ArrowInterface.RequestParams;
using hn.ArrowInterface.WebCommon;
using hn.Common_Arrow;
using hn.DataAccess.dal.LHModel;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using hn.DataAccess.model.Common;
using Newtonsoft.Json;

namespace hn.Client.Service
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class LHAPIService: ILHAPIService
    {
        private ARROW_SyncMethods methods = new ARROW_SyncMethods();

        // 在此处添加更多操作并使用 [OperationContract] 标记它们
        [OperationContract]
        public bool SaleOrderUpload(List<string> billNos)
        {
            return methods.SaleOrderUpload(billNos);
        }

        [OperationContract]
        public bool AcctOaStatus(AcctOAStatusParam pars)
        {
            return methods.AcctOaStatus(pars);
        }

        [OperationContract]
        public bool obOrderUpload(List<string> billNos)
        {
            return methods.obOrderUpload(billNos);
        }

        [OperationContract]
        public List<LH_Policy> GetPolicies(ICPOBILL_PolicyDTO header)
        {
            return methods.GetPolicies(header);

        }

        [OperationContract]
        public PageResult<v_lhproducts_policyModel> GetPolicyProducts(ICPOBILL_PolicyDTO header,
            v_lhproducts_policyModel where, int index = 1, int size = 35)
        {
            return methods.GetPolicyProducts(header, where, index, size);
        }

        [OperationContract]
        public string SaveICPOBILL(string header, string entriees)
        {
            LogHelper.Info($"表头数据：{header}");
            LogHelper.Info(($"明细数据：{entriees}"));

            var headerObj = JsonConvert.DeserializeObject<ICPOBILLMODEL>(header);
            var entriesObj = JsonConvert.DeserializeObject<List<ICPOBILLENTRYMODEL>>(entriees);

            Mapper.Initialize(new MapperConfigurationExpression());
            var saveHeader = Mapper.Instance.Map<ICPOBILLMODEL>(headerObj);
            var list = new List<ICPOBILLENTRYMODEL>();

            saveHeader = new ICPOBILLMODEL()
            {
                FID = headerObj.FID,
                FPREMISEID = headerObj.FPREMISEID,
                FDesBillNo = headerObj.FDesBillNo,
                FSTATUS = headerObj.FSTATUS,
                FBILLER = headerObj.FBILLER,
                FBILLDATE = headerObj.FBILLDATE,
                FBILLNO = headerObj.FBILLNO,
                FACCOUNT = headerObj.FACCOUNT,
                FBILLERNAME = headerObj.FBILLERNAME,
                FBRANDID = headerObj.FBRANDID,
                FCHECKDATE = headerObj.FCHECKDATE,
                FCLIENTID = headerObj.FCLIENTID,
                FDATE = headerObj.FDATE,
                FDATESTR = headerObj.FDATESTR,
                FPOtype = headerObj.FPOtype,
                FREMARK = headerObj.FREMARK,
                FSTATE = headerObj.FSTATE,
                FSYNCSTATUS = headerObj.FSYNCSTATUS,
                FTELEPHONE = headerObj.FTELEPHONE,
                FTIMESTAMP = headerObj.FTIMESTAMP,
                FTRANSTYPE = headerObj.FTRANSTYPE,
                Fcompany = headerObj.Fcompany,
                Fnote = headerObj.Fnote,
                Fpricepolicy = headerObj.Fpricepolicy,
                FprojectNO = headerObj.FprojectNO,
                OASTATUS = headerObj.OASTATUS,
                LH_ADVERTINGMONEYTYPE = headerObj.LH_ADVERTINGMONEYTYPE,
                LH_ORDERTYPE = headerObj.LH_ORDERTYPE,
                LH_SALESCHANNEL = headerObj.LH_SALESCHANNEL,
                LH_ORDERPRODLINE = headerObj.LH_ORDERPRODLINE,
                LH_BUTYPE = headerObj.LH_BUTYPE,
                LH_PROMOTIONPOLICYID = headerObj.LH_PROMOTIONPOLICYID,
                LH_EXPECTEDARRIVEDDATE = headerObj.LH_EXPECTEDARRIVEDDATE,
                LH_OUTBOUNDORDER = headerObj.LH_OUTBOUNDORDER
            };

            foreach (var e in entriesObj)
            {
                var item = Mapper.Map<ICPOBILLENTRYMODEL>(e);
                list.Add(item);
            }

            
            return methods.SaveICPOBILL(saveHeader, list);
        }

       
    }
}
