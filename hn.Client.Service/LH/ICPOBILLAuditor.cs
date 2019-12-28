using System;
using System.Collections.Generic;
using DataAccess.CustomEnums;
using hn.Client.Service.Respository;
using hn.DataAccess.dal.LHModel;
using hn.DataAccess.Model;

namespace hn.Client.Service.LH
{
    public class ICPOBILLAuditor:IAuditor<ICPOBILLMODEL>
    {
        public string UserID { get; set; }

        private DefaultRepository<ICPOBILLMODEL> repository =new DefaultRepository<ICPOBILLMODEL>(DBTypeEnums.ORACLE);

        /// <summary>
        /// 单据状态对应可进行的操作，一个状态可能会允许多个操作
        /// </summary>
        private Dictionary<ICPOBILLStatus, HashSet<AuditEnums>> TypesAuditions { get; set; }

        /// <summary>
        /// 每个操作过后对应的状态
        /// </summary>
        private Dictionary<AuditEnums, ICPOBILLStatus> AuditionTypeDictionary { get; set; }

        public ICPOBILLAuditor(string userId,AuditEnums auditType)
        {
            InitAuditionTypeDictionary();
            InitTypesAuditions();

            this.UserID = userId;
            
        }

        private void InitTypesAuditions()
        {
            TypesAuditions=new Dictionary<ICPOBILLStatus, HashSet<AuditEnums>>();
            TypesAuditions.Add(ICPOBILLStatus.草稿,new HashSet<AuditEnums>(){AuditEnums.提交,AuditEnums.审核通过,AuditEnums.审核不通过});
            TypesAuditions.Add(ICPOBILLStatus.待审核,new HashSet<AuditEnums>(){AuditEnums.审核通过,AuditEnums.审核不通过});
            TypesAuditions.Add(ICPOBILLStatus.审核通过,new HashSet<AuditEnums>(){AuditEnums.反审核,AuditEnums.确认});
            TypesAuditions.Add(ICPOBILLStatus.审核不通过,new HashSet<AuditEnums>());
            TypesAuditions.Add(ICPOBILLStatus.确认,new HashSet<AuditEnums>(){AuditEnums.反确认,AuditEnums.提交同步});
            TypesAuditions.Add(ICPOBILLStatus.关闭,new HashSet<AuditEnums>());
        }

        private void InitAuditionTypeDictionary()
        {
            AuditionTypeDictionary=new Dictionary<AuditEnums, ICPOBILLStatus>();
            AuditionTypeDictionary.Add(AuditEnums.提交, ICPOBILLStatus.待审核);
            AuditionTypeDictionary.Add(AuditEnums.审核通过, ICPOBILLStatus.审核通过);
            AuditionTypeDictionary.Add(AuditEnums.审核不通过, ICPOBILLStatus.审核不通过);
            AuditionTypeDictionary.Add(AuditEnums.反审核, ICPOBILLStatus.草稿);
            AuditionTypeDictionary.Add(AuditEnums.确认, ICPOBILLStatus.确认);
            AuditionTypeDictionary.Add(AuditEnums.反确认, ICPOBILLStatus.审核通过);
            AuditionTypeDictionary.Add(AuditEnums.提交同步, ICPOBILLStatus.关闭);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill">单据体</param>
        /// <param name="auditType">操作</param>
        /// <returns></returns>
        public string AuditBILL(ICPOBILLMODEL bill,AuditEnums auditType)
        {
            ICPOBILLStatus billStatus;
            Enum.TryParse(bill.FSTATUS.ToString(),out billStatus);
            string msg = "";

            if (CheckOption(billStatus, auditType))
            {
                bill.FCHECKDATE = DateTime.Now;
                bill.FCHECKERID = this.UserID;
                bill.FSTATUS = (int) AuditionTypeDictionary[auditType];


                repository.Update(bill, new {FID = bill.FID});

                msg = $"【{auditType}】操作成功\r\n";
            }
            else
            {
                msg = $"【{billStatus}】状态的单据【{bill.FBILLNO}】不能进行【{auditType}】操作\r\n";
            }

            return msg;
        }

        private bool CheckOption(ICPOBILLStatus billStatus,AuditEnums auditType)
        {
            var options = TypesAuditions[billStatus];
            return options.Contains(auditType);
        }

        public string CheckOption(List<string> billNos,AuditEnums auditType)
        {
            var compare=new Dictionary<string,CompareEnum>();
            compare.Add("FBILLNO",CompareEnum.In);

            var bills = repository.SelectWithWhere(new {FBILLNO = billNos}, compare);
            string msg = "";
            if (bills != null && bills.Count > 0)
            {
                ICPOBILLStatus billStatus;
                bills.ForEach(p =>
                {
                    Enum.TryParse(p.FSTATUS.ToString(), out billStatus);
                    if (!CheckOption(billStatus, auditType))
                    {
                        msg += $"【{billStatus}】状态的单据【{p.FBILLNO}】不能进行【{auditType}】操作";
                    }
                });
            }

            return msg;
        }

    }
}