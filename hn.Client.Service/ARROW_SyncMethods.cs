using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DataAccess;
using DataAccess.CustomEnums;
using hn.ArrowInterface.Entities;
using hn.ArrowInterface.RequestParams;
using hn.ArrowInterface.WebCommon;
using hn.Client.Service.LH;
using hn.Client.Service.Respository;
using hn.Common_Arrow;
using hn.DataAccess.Bll;
using hn.DataAccess.dal.LHModel;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using hn.DataAccess.model.Common;
using Newtonsoft.Json;

namespace hn.Client.Service
{
    public class ARROW_SyncMethods
    {

        public ARROW_SyncMethods()
        {
            DBConnectionFactory.ConnectionName = "DbConnection";
        }

        public bool SaleOrderUpload(List<string> billNos)
        {

            var auditType = AuditEnums.提交同步;
            ICPOBILLAuditor auditor=new ICPOBILLAuditor("System", auditType);
            var err= auditor.CheckOption(billNos, auditType);

            

            if (!err.Equals(""))
            {
                throw new ArgumentException(err);
            }

            var token = CommonToken.GetToken();
            var http = new ArrowInterface.ArrowInterface();
            var Helper = new OracleDBHelper();
            string where = $" AND LHOUTSYSTEMOD IN ('{string.Join("','", billNos)}')";
            var bills = Helper.GetWithWhereStr<SaleOrderUploadParam>(where);

            bills.ForEach(b =>
            {
                var details = Helper.GetWhere(new SaleOrderUploadDetailedParam()
                { lHOutSystemID = b.lHOutSystemID }).ToArray();

                b.saleOrderItemList = details;
            });

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

                        var saleorderRepository=new DefaultRepository<Order>(DBTypeEnums.ORACLE);


                        ///返写箭牌销售单号到本地采购订单表ICPOBILL的FDesBillNo字段
                        var whereStr = $" AND FBILLNO='{b.lHOutSystemOd}'";
                        var icpobill = Helper.GetWithTranWithWhereStr<ICPOBILLMODEL>(whereStr, tran)
                            .SingleOrDefault();

                        saleorderRepository.Delete(new { lHOutSystemID=b.lHOutSystemID }, tran);

                        foreach (var row in result.item.AsParallel())
                        {
                            Helper.InsertWithTransation(row, tran);

                            icpobill.FDesBillNo = row.orderNo;
                        }

                        //更新本地采购订单表ICPOBILL
                        icpobill.FSTATUS = (int)ICPOBILLStatus.关闭;
                        icpobill.FSYNCSTATUS = (int)ICPOBILLSyneStatus.已同步;
                        Helper.UpdateWithTransation(icpobill, tran);
                        

                    }
                    else
                    {
                        errors.Add($"单据【{b.lHOutSystemOd}】上传失败:{result.Message}");
                    }

                    tran.Commit();
                    conn.Close();
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    conn.Close();
                    var message = $"销售订单【{b.lHOutSystemOd}】上传失败：{e.Message}";
                    LogHelper.Info(message);
                    LogHelper.Error(e);
                    throw;
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
            pars.acctCode = ConfigurationManager.AppSettings["dealerCode"];
            var result = http.AcctOaStatus(token.Token, pars);

            var Helper = new OracleDBHelper();

            if (result.Success)
            {
                foreach (var row in result.Rows.AsParallel())
                {
                    var conn = Helper.GetNewConnection();
                    conn.Open();
                    var tran = conn.BeginTransaction();
                    try
                    {
                        Helper.DeleteWithTran(row, tran);
                        Helper.DeleteWithTran<AcctOAStatusDetailed>($" AND ORDERNO='{row.orderNo}'", tran);
                        Helper.InsertWithTransation(row, tran);
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        conn.Close();
                        var message = $"OA同步结果插入失败：{JsonConvert.SerializeObject(row)}";
                        LogHelper.Info(message);
                        LogHelper.Error(e);
                        return false;
                    }

                    tran.Commit();
                }
            }


            return result.Success;
        }

        public bool obOrderUpload(List<string> billNos)
        {
            var token = CommonToken.GetToken();
            var http = new ArrowInterface.ArrowInterface();
            var Helper = new OracleDBHelper();

            var uploadParamRepository=new DefaultRepository<ObOrderUploadParam>(DBTypeEnums.ORACLE);
            var lhobounorderRepository=new DefaultRepository<LH_OUTBOUNDORDER>(DBTypeEnums.ORACLE);

            Dictionary<string,CompareEnum> compare=new Dictionary<string, CompareEnum>();
            compare.Add("LHODONO",CompareEnum.In);

            var pars= uploadParamRepository.SelectWithWhere(new {LHODONO = billNos}, compare, null);

            List<string> errors = new List<string>();
            pars.ForEach(p =>
            {
                try
                {
                    var result = http.obOrderUpload(token.Token, p);
                    if (result.Success)
                    {
                        var sql = "UPDATE LH_OUTBOUNDORDER SET FSTATUS=:FSTATUS WHERE lhodoID=:lhodoID";
                        lhobounorderRepository.Execute(sql, new {FSTATUS = 7, lhodoID = p.lhodoID }, null);
                    }
                    else
                    {
                        errors.Add($"单据【{p.lhodoID}】车牌同步失败");
                    }

                }
                catch (OracleException e)
                {
                    var message = string.Format($"单据【{p.lhodoID}】车牌上传结果更新失败");
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

        public List<LH_Policy> GetPolicies(ICPOBILL_PolicyDTO header)
        {
            //检查传入参数是否合法
            CheckNull(header);
            var helper = new OracleDBHelper();
            var sql =
                @"SELECT DISTINCT HEADID,POLICYNAME,ORDERTYPE,ORDERSUBTYPE,PRODCHANNEL,DEPTNAME from LH_POLICY WHERE 1=1 ";

            //选择订单类型为常规订单的，如果用户要选择促销政策头ID的话则需要同时判断：订单所属公司（事业部）、厂家账号（经销商账号）、销售渠道、业务类型、五项头字段信息来取促销政策头ID信息
            string where = $@"AND ORDERTYPE='{header.OrderType}' AND DEPTNAME LIKE '%{header.BrandName}%' 
                                    AND ORDERSUBTYPE='{header.OrderSubType}' AND PRODCHANNEL='{header.Channel}' AND ACCTCODES LIKE '%{header.Account}%'";

            sql += where;

            var policies = helper.Select<LH_Policy>(sql);

            return policies;

        }

        public PageResult<v_lhproducts_policyModel> GetPolicyProducts(ICPOBILL_PolicyDTO header,
            v_lhproducts_policyModel where, int index = 1, int size = 35)
        {
            where = ComputeWhere(header, where);
            var helper = new OracleDBHelper();
            var total = 0;

            string whereStr = helper.GetWhereStr(where);

            List<v_lhproducts_policyModel> resultList = new List<v_lhproducts_policyModel>();

            if (string.IsNullOrEmpty(header.HeadID))
            {
                List<V_LHPRODUCTS_UNPOLICYHEADID> data =
                    helper.GetWithWhereStrByPage<V_LHPRODUCTS_UNPOLICYHEADID>(whereStr, where, index, size);

                total = helper.Count<V_LHPRODUCTS_UNPOLICYHEADID>(whereStr);

                var t = typeof(V_LHPRODUCTS_UNPOLICYHEADID);

                var pis = t.GetProperties().ToList();

                data.ForEach(p =>
                {
                    var item = new v_lhproducts_policyModel();
                    pis.ForEach(pi =>
                    {
                        var value = pi.GetValue(p, null);
                        pi.SetValue(item, value);
                    });
                    resultList.Add(item);
                });
            }
            else
            {
                List<v_lhproducts_policyModel> data =
                    helper.GetWithWhereStrByPage<v_lhproducts_policyModel>(whereStr, where, index, size);

                total = helper.Count<v_lhproducts_policyModel>(whereStr);
                resultList = data;
            }

            PageResult<v_lhproducts_policyModel> result = new PageResult<v_lhproducts_policyModel>()
            { Total = total, Result = resultList };

            return result;
        }

        private bool CheckNull(ICPOBILL_PolicyDTO header)
        {
            var t = typeof(ICPOBILL_PolicyDTO);
            var pis = t.GetProperties().ToList();
            //检查传入参数值是否为空
            pis.ForEach(p =>
            {
                var attr = p.GetCustomAttributes(true)
                    .SingleOrDefault(c => c is RequiredAttribute) as RequiredAttribute;
                var value = p.GetValue(header);
                if ((value == null || string.IsNullOrEmpty(value.ToString())) && attr != null)
                {
                    throw new ValidationException(attr.ErrorMessage);
                }
            });

            return true;
        }

        private v_lhproducts_policyModel ComputeWhere(ICPOBILL_PolicyDTO header, v_lhproducts_policyModel where)
        {
            CheckNull(header);

            where.LHPRODTYPE = header.BrandName;

            switch (header.OrderType)
            {
                //常规订单
                case "Common":
                    {
                        where.LHPRODSIGN = "成品";
                        break;
                    }

                //配件订单
                case "Parts":
                    {
                        where.LHPRODSIGN = "配件";
                        break;
                    }

                //广告物料申请单
                case "Advertise":
                    {
                        where.LHPRODSIGN = "广告物料";
                        where.LHPRODTYPE = "广告用品";
                        break;
                    }
            }

            where.HEADID = string.IsNullOrEmpty(header.HeadID) ? null : header.HeadID;

            if (!string.IsNullOrEmpty(header.HeadID))
            {
                where.DEPTNAME = header.BrandName.Contains("事业部") ? header.BrandName : $"{header.BrandName}事业部";
                where.ORDERTYPE = header.OrderType;
                where.ORDERSUBTYPE = header.OrderSubType;
                where.ACCTCODES = header.Account;
            }

            where.LHPRODCHANNEL = header.Channel;

            return where;

        }

        public string SaveICPOBILL(ICPOBILLMODEL ICPOBILL, List<ICPOBILLENTRYMODEL> ICPOBILLENTRYList)
        {
            string result = ICPOBILLBLL.Instance.SaveClient(ICPOBILL, ICPOBILLENTRYList);
            if (result.IsNullOrEmpty())
            {
                return "保存完成！" + result;
            }
            else
            {
                return result;
            }
        }

        public bool SaveMergeBill(LH_MergeBill bill, List<string> LHOBODNOS)
        {
            var conn = DBConnectionFactory.GetConnection(DBTypeEnums.ORACLE);
            conn.Open();
            var tran = conn.BeginTransaction();
            try
            {
                bool result = false;
                LH_MergeBillRepository repository = new LH_MergeBillRepository();
                var lhoutorderRepository = new DefaultRepository<LH_OUTBOUNDORDER>(DBTypeEnums.ORACLE);

                var sql = "SELECT * FROM MERGEBILL WHERE FBILLNO=:FBILLNO";

                var temp = repository.GetSingle(sql, bill);
                if (temp == null)
                {

                    result = repository.Insert(bill, tran);

                }
                else
                {
                    result = repository.Update(bill, new { FBILLNO = bill.FBILLNO });
                }

                sql = $"UPDATE  LH_OUTBOUNDORDER SET FMEGREBILLNO=:FBILLNO,FCARNO=:CARNO WHERE LHODONO IN :LHODONOS";

                result = lhoutorderRepository.Execute(sql, new { FBILLNO = bill.FBILLNO, LHODONOS = LHOBODNOS, CARNO = bill.FCARNO }, tran) == LHOBODNOS.Count;

                tran.Commit();
                conn.Close();

                return result;

            }
            catch (Exception e)
            {
                tran.Rollback();
                conn.Close();
                LogHelper.Error(e);
                throw;
            }

        }

        public List<LH_MergeBill> GetMergeBills(List<string> LHOBODNOS)
        {

            var repository = new DefaultRepository<LH_MergeBill>(DBTypeEnums.ORACLE);
            var sql = "SELECT DISTINCT MB.* FROM MergeBill MB INNER JOIN LH_OUTBOUNDORDER OB ON OB.FMEGREBILLNO=MB.FBILLNO WHERE OB.LHODONO IN :LHODONOS";

            try
            {
                return repository.Query(sql, new { LHODONOS = LHOBODNOS }, null);
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                throw;
            }


        }

        public string AuditionICPOBILL(string userId,List<string> billnos,AuditEnums auditType)
        {
            ICPOBILLAuditor auditor = new ICPOBILLAuditor(userId, auditType);
            
            var repository=new DefaultRepository<ICPOBILLMODEL>(DBTypeEnums.ORACLE);

            var compare=new Dictionary<string,CompareEnum>();
            compare.Add("FBILLNO",CompareEnum.In);

            var bills = repository.SelectWithWhere(new {FBILLNO = billnos}, compare);

            var result = new StringBuilder();

            foreach (var b in bills)
            {
                var msg = auditor.AuditBILL(b, auditType);
                result.Append(msg);
            }

            return result.ToString();

        }
    }
}