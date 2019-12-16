using hn.Common;
using hn.Common.Provider;
using hn.Core;
using hn.DataAccess.Dal;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using hn.Common_Arrow;
using LogHelper = hn.Common.LogHelper;

namespace hn.DataAccess.Bll
{
    public class ICPOBILLBLL
    {
        public static ICPOBILLBLL Instance
        {
            get { return SingletonProvider<ICPOBILLBLL>.Instance; }
        }

        public string GetEasyUIJson(int page = 1, int rows = 15, string FOrgID = null, string FBrandID = null, string keywords = null, string sort = "FID", string order = "asc")
        {
            return ICPOBILLDAL.Instance.GetJson(page, rows, FOrgID, FBrandID, keywords, sort, order);
        }

        public string Save(ICPOBILLMODEL ICPOBILL, IEnumerable<ICPOBILLENTRYMODEL> ICPOBILLENTRYList)
        {
            string FID = null;

            #region 执行前检测
            if (!ICPOBILL.FID.IsGuid())
            {
                ICPOBILLMODEL temp = ICPOBILLDAL.Instance.GetWhere(new { FBILLNO = ICPOBILL.FBILLNO }).FirstOrDefault();

                if (temp != null && temp.FID != ICPOBILL.FID)
                {
                    return "采购订单编号重复！";
                }
            }

            //foreach (var item in ICPOBILLENTRYList.GroupBy(i => i.FITEMID + i.FENTRYID))
            //{
            //    if (item.Count() > 1)
            //    {
            //        return "采购订单明细商品信息重复！";
            //    }
            //}

            //foreach (var item in ICPOPOLICYList.GroupBy(i => i.FITEMID + i.FSRCENTRYID))
            //{
            //    if (item.Count() > 1)
            //    {
            //        return "采购订单价格政策商品信息重复！";
            //    }
            //}

            #endregion

            #region 保存主表

            if (!ICPOBILL.FID.IsGuid())
            {
                ICPOBILL.FSTATUS = 1;
                ICPOBILL.FSTATE = 1;
                ICPOBILL.FBILLER = SysVisitor.Instance.UserId;
                ICPOBILL.FBILLERNAME = SysVisitor.Instance.UserName;
                ICPOBILL.FBILLDATE = DateTime.Now;

                FID = ICPOBILLDAL.Instance.Insert(ICPOBILL);

                if (!FID.IsGuid())
                {
                    return "保存采购订单失败！";
                }
            }
            else
            {
                FID = ICPOBILL.FID;

                var model =  ICPOBILLDAL.Instance.Get(FID);
                model.FBRANDID = ICPOBILL.FBRANDID;
                model.FCLIENTID = ICPOBILL.FCLIENTID;
                model.FREMARK = ICPOBILL.FREMARK;
                model.FDATE = ICPOBILL.FDATE;
                model.FTELEPHONE = ICPOBILL.FTELEPHONE;
                ICPOBILLDAL.Instance.Update(model);
                //if (ICPOBILLDAL.Instance.UpdateWhatWhere(new
                //{
                //    FBILLER = SysVisitor.Instance.UserId,
                //    FBILLERNAME = SysVisitor.Instance.UserName,
                //    //FSRCPRID=ICPOBILL.FSRCPRID,
                //    FTRANSTYPE = ICPOBILL.FTRANSTYPE
                //}, new
                //{ 
                //    FID = FID
                //}) <= 0)
                //{
                //    return "保存采购订单失败";
                //}
            }

            #endregion

            #region 保存子表

            //删除明细
            ICPOBILLENTRYBLL.Instance.DeleteByICPOBILLID(FID);

            //删除价格政策
            //ICPOPOLICYBLL.Instance.DeleteByICPOBILLID(FID);

            //保存明细
            foreach (var item in ICPOBILLENTRYList)
            {
                item.FICPOBILLID = FID;
                ICPOBILLENTRYDAL.Instance.Insert(item);

                //更新请购计划明细里面的采购订单编号
                ICPRBILLENTRYDAL.Instance.UpdateWhatWhere(new { FICPOBILLNO = ICPOBILL.FBILLNO }, new { FID = item.FPLANID });
            }

            ////保存价格政策
            //foreach (var item in ICPOPOLICYList)
            //{
            //    ICPOPOLICYBLL.Instance.Add(item, FID);
            //}

            #endregion

            return null;
        }


        public string SaveClient(ICPOBILLMODEL ICPOBILL, IEnumerable<ICPOBILLENTRYMODEL> ICPOBILLENTRYList)
        {
            OracleDBHelper helper = new OracleDBHelper();
            var conn = helper.GetNewConnection();
            conn.Open();
            var tran = conn.BeginTransaction();
            try
            {
               

                LogHelper.Info("ICPOBILLENTRYList=" + JSONhelper.ToJson(ICPOBILLENTRYList));

            string FID = null;

            #region 执行前检测
            if (!ICPOBILL.FID.IsGuid())
            {
                ICPOBILLMODEL temp = ICPOBILLDAL.Instance.GetWhere(new { FBILLNO = ICPOBILL.FBILLNO }).FirstOrDefault();

                if (temp != null && temp.FID != ICPOBILL.FID)
                {
                    return "采购订单编号重复！";
                }
            }

            #endregion

            #region 保存主表

            if (!ICPOBILL.FID.IsGuid())
            {
                FID = Guid.NewGuid().ToString();
                ICPOBILL.FSTATUS = 1;
                ICPOBILL.FSTATE = 1;
                ICPOBILL.FBILLER = ICPOBILL.FBILLER;
                ICPOBILL.FBILLERNAME = ICPOBILL.FBILLER;
                ICPOBILL.FBILLDATE = DateTime.Now;
                ICPOBILL.Fpricepolicy = ICPOBILL.Fpricepolicy;
                ICPOBILL.FPOtype = ICPOBILL.FPOtype;
                ICPOBILL.Fnote = ICPOBILL.Fnote;
                ICPOBILL.FID = FID;

                helper.InsertWithTransation(ICPOBILL, tran);
            }
            else
            {
                FID = ICPOBILL.FID;

                var model = ICPOBILLDAL.Instance.Get(FID);            
                model.FCLIENTID = ICPOBILL.FCLIENTID;
                model.FREMARK = ICPOBILL.FREMARK;
                model.FDATE = ICPOBILL.FDATE;
                model.FTELEPHONE = ICPOBILL.FTELEPHONE;
                model.Fpricepolicy = ICPOBILL.Fpricepolicy;
                model.FPOtype = ICPOBILL.FPOtype;
                model.Fnote = ICPOBILL.Fnote;


                //int iResult= ICPOBILLDAL.Instance.Update(model);
                var iResult = helper.UpdateWithTransation(model,tran);

            }

            #endregion

            #region 保存子表

            //删除明细
            //ICPOBILLENTRYBLL.Instance.DeleteByICPOBILLID(FID);
            helper.DeleteWithTran<ICPOBILLENTRYMODEL>($"AND  FICPOBILLID='{FID}'", tran);

            //保存明细
            foreach (var item in ICPOBILLENTRYList)
            {
                item.FICPOBILLID = FID;
                item.FID = Guid.NewGuid().ToString();

                helper.InsertWithTransation(item, tran);

            }

            #endregion
            tran.Commit();
            conn.Dispose();
            return null;
            }
            catch (Exception e)
            {
                tran.Rollback();
                LogHelper.Info("e.Message=" + e.Message);
                throw;
            }

        }

        /// <summary>
        /// 获取审核状态
        /// </summary>
        /// <returns></returns>
        public int GetStatus(string FID)
        {
            return ICPOBILLDAL.Instance.GetStatus(FID);
        }

        /// <summary>
        /// 审核OA状态
        /// </summary>
        /// <param name="idStrings"></param>
        /// <returns></returns>
        public bool OA_Status(string[] idStrings)
        {
            return ICPOBILLDAL.Instance.ExecuteNonQuery(@"Update ICPOBILL SET OASTATUS = 1 WHERE FDESBILLNO IN('"+string.Join("','",idStrings) + "')") > 0;
        }
    }
}