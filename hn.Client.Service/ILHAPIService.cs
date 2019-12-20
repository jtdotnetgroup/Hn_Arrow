using System.Collections.Generic;
using System.ServiceModel;
using hn.ArrowInterface.Entities;
using hn.ArrowInterface.RequestParams;
using hn.DataAccess.dal.LHModel;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using hn.DataAccess.model.Common;

namespace hn.Client.Service
{
   
    public interface ILHAPIService
    {
        bool SaleOrderUpload(List<string> billNos);

        bool AcctOaStatus(AcctOAStatusParam pars);


        bool obOrderUpload(List<string> billNos);


        List<LH_Policy> GetPolicies(ICPOBILL_PolicyDTO header);


        PageResult<v_lhproducts_policyModel> GetPolicyProducts(ICPOBILL_PolicyDTO header,
            v_lhproducts_policyModel where, int index = 1, int size = 35);

        string SaveICPOBILL(string header,string entriees);


    }
}