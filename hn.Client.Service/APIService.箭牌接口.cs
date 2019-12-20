using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using hn.ArrowInterface.Entities;
using hn.ArrowInterface.RequestParams;
using hn.ArrowInterface.WebCommon;
using hn.Common_Arrow;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using hn.DataAccess.dal;
using hn.DataAccess.model.Common;

namespace hn.Client.Service
{
    public partial class APIService
    {
        private ARROW_SyncMethods methods = new ARROW_SyncMethods();

        public bool SaleOrderUpload(List<string> billNos)
        {
            return methods.SaleOrderUpload(billNos);
        }

        public bool AcctOaStatus(AcctOAStatusParam pars)
        {
            return methods.AcctOaStatus(pars);
        }

        public bool obOrderUpload(List<string> billNos)
        {
            return methods.obOrderUpload(billNos);
        }

        public List<LH_Policy> GetPolicies(ICPOBILL_PolicyDTO header)
        {
            return methods.GetPolicies(header);

        }

        public PageResult<v_lhproducts_policyModel> GetPolicyProducts(ICPOBILL_PolicyDTO header,
            v_lhproducts_policyModel where, int index = 1, int size = 35)
        {
            return methods.GetPolicyProducts(header, where, index, size);
        }

    }
}