using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using hn.ArrowInterface.Entities;
using hn.ArrowInterface.RequestParams;
using hn.ArrowInterface.WebCommon;
using hn.Common_Arrow;
using Newtonsoft.Json;

namespace hn.Client.Service
{
    public partial class APIService
    {
        public bool SaleOrderUpload(List<string> billNos)
        {
            var token = CommonToken.GetToken();
            var http = new ArrowInterface.ArrowInterface();
            var Helper = new OracleDBHelper();
            string where = $" AND LHOUTSYSTEMOD IN ('{string.Join(",", billNos)}')";
            var bills = Helper.GetWithWhereStr<SaleOrderUploadParam>(where);
            List<string> errors = new List<string>();

            bills.ForEach(b =>
            {
                var conn = Helper.GetNewConnection();
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    var result = http.SaleOrderUpload(token.Token, b);

                    if (result.Success)
                    {
                        foreach (var row in result.Rows.AsParallel())
                        {
                            Helper.DeleteWithTran<Order>(row.KeyId(), tran);
                            Helper.InsertWithTransation(row, tran);
                            tran.Commit();
                        }
                    }
                    else
                    {
                        errors.Add($"单据【{b.lHOutSystemOd}】上传失败");
                    }
                }
                catch (OracleException e)
                {
                    tran.Rollback();
                    tran.Connection.Close();
                    var message = string.Format("销售订单上传结果插入失败：{0}", JsonConvert.SerializeObject(b));
                    Common_Arrow.LogHelper.Info(message);
                    Common_Arrow.LogHelper.Error(e);
                }
            });

            if (errors.Count > 0)
            {
                throw new Exception(string.Join("\r\n", errors));
            }

            return true;
        }

        public bool AcctOaStatus(AcctOAStatusParam pars)
        {
            var token = CommonToken.GetToken();
            var http = new ArrowInterface.ArrowInterface();

            var result = http.AcctOaStatus(token.Token, pars);

            var Helper = new OracleDBHelper();

            if (result.Success)
                foreach (var row in result.Rows.AsParallel())
                {
                    var conn = Helper.GetNewConnection();
                    conn.Open();
                    var tran = conn.BeginTransaction();
                    try
                    {
                        Helper.DeleteWithTran(row, tran);
                        Helper.DeleteWithTran<AcctOAStatusDetailed>(row.orderNo, tran);
                        Helper.InsertWithTransation(row, tran);
                        result.Rows.ForEach(p => Helper.InsertWithTransation(p, tran));
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        tran.Connection.Close();
                        var message = string.Format("OA同步结果插入失败：{0}", JsonConvert.SerializeObject(row));
                        Common_Arrow.LogHelper.Info(message);
                        Common_Arrow.LogHelper.Error(e);
                        return false;
                    }

                    tran.Commit();
                }


            return result.Success;
        }

        public bool obOrderUpload(List<string> billNos)
        {
            var token = CommonToken.GetToken();
            var http = new ArrowInterface.ArrowInterface();
            var Helper = new OracleDBHelper();

            List<string> errors = new List<string>();
            billNos.ForEach(no =>
            {
                try
                {
                    var where = new LH_OUTBOUNDORDER() { lhodoNo = no };
                    var bill = Helper.GetWhere(where).SingleOrDefault();
                    if (bill == null)
                    {
                        throw new Exception($"单据【{no}】不存在");
                    }
                    ObOrderUploadParam pars = new ObOrderUploadParam() { lhodoID = bill.lhodoID, lhplateNo = bill.FCarno };
                    var result = http.obOrderUpload(token.Token, pars);
                    if (result.Success)
                    {
                        bill.FStatus = 2;
                        Helper.Update(bill);
                    }
                    else
                    {
                        errors.Add($"单据【{no}】车牌同步失败");
                    }
                    
                }
                catch (OracleException e)
                {
                    var message = string.Format($"单据【{no}】车牌上传结果更新失败");
                    Common_Arrow.LogHelper.Info(message);
                    Common_Arrow.LogHelper.Error(e);
                }
            });

            if (errors.Count > 0)
            {
                throw new Exception(string.Join("\r\n", errors));
            }


            return true;
        }
    }
}