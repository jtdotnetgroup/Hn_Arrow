﻿using hn.ArrowInterface.Entities;
using hn.ArrowInterface.RequestParams;
using hn.ArrowInterface.WebCommon;
using hn.Common_Arrow;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace hn.ArrowInterface.Jobs
{
    [Obsolete("不需要定时同步，实时调用的")]
    public class SyncSaleOrderUploadResultJob : AbsJob
    {
        public override bool Sync()
        {
            var token = GetToken(); 
            var pars = new SaleOrderUploadParam(); 
            var result = Interface.SaleOrderUpload(token.Token, pars);

            if (result.Success)
            {
                var tmp = result.item;
                if (tmp != null)
                {
                    var conn = Helper.GetNewConnection();
                    conn.Open();
                    var tran = conn.BeginTransaction();
                    try
                    {
                        foreach (var row in tmp.AsParallel())
                        {
                            Helper.DeleteWithTran<Order>(row.KeyId(),tran);
                            Helper.InsertWithTransation(row,tran);
                        }
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        conn.Close();

                        string message = string.Format("销售订单上传结果：{0}", JsonConvert.SerializeObject(tmp));
                        LogHelper.Info(message);
                        LogHelper.Error(e);
                        return false;
                    }
                }
                //同步完成，更新请求记录
                UpdateSyncRecord(pars);
                return true;
            }

            return false;
        }

        protected override AbstractRequestParams GetParams()
        {
            var jobRecord = Helper.GetWhere<SyncJob_Definition>(new SyncJob_Definition() { JobClassName = this.JobName }).FirstOrDefault();
             
            var pars = new SaleOrderUploadParam
            {
                orderType = "常规订单",
                acctCode = "AW04298",
                tradeCompanyName = "广东乐华智能卫浴有限公司",
                billIdName = "测试法人",
                salesChannel = "零售",
                lHbuType = "常规",
                contractWay = "经销",
                orderProdLine = "卫浴",
                balanceName = "测试-箭牌卫浴事业部-卫浴-零售",
                lHexpectedArrivedDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"),
                lHdepositOrNot = "N",
                lHdiscountType = "底价不变",
                lHorgName = "箭牌卫浴事业部",
                submissionDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                source = "华耐系统",
                lHOutSystemID = "testorder001",
                lHOutSystemOd = "dsdd-9999901",
                lHpromotionPolicyID = "",
                consignee = "1",
                lHoutboundOrder = "",
                lHAdvertingMoneyType = "PayForGoods",
                remarks = "",
                saleOrderItemList = new[]
                {
                    new SaleOrderUploadDetailedParam
                    {
                        prodCode = "17103103036416",
                        qTY = 10,
                        lHrowSource = "华耐系统",
                        lHOutSystemID = "testorder001",
                        lHOutSystemLineID = "testorderitem001",
                        lHcomments = "",
                        lHDctpolicyItemId = "W-57ASBU2SD"
                    }
                }
            };

            return pars;
        }
    }
}
