using hn.Common.Data;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace hn.DataAccess.dal.LHModel
{
    /// <summary>
    /// 出库单表头
    /// </summary>
    [Table("LH_OUTBOUNDORDER")]
    [TableName("LH_OUTBOUNDORDER")]
    public class LH_OUTBOUNDORDERModel
    {
        public string LHODOID { get; set; }
        public string LHODONO { get; set; }
        public decimal LHTOTALint { get; set; }
        public string LHODOTYPE { get; set; }
        public string ACCTCODE { get; set; }
        public string ACCTNAME { get; set; }
        public decimal LHTOTALAMOUNT { get; set; }
        public decimal LHTOTALVOLUME { get; set; }
        public string LHDELIVERYBASE { get; set; }
        public string LHDELIVERYMAN { get; set; }
        public string LHCONTRACTNO { get; set; }
        public string BILLACCTNAME { get; set; }
        public string BILLACCTCODE { get; set; }
        public decimal LHADVERTISINGPOINT { get; set; }
        public DateTime LHDELIVERYTIME { get; set; }
        public string LHPLATENO { get; set; }
        public decimal LHREBATESPOINT { get; set; }
        public string LHBUTYPE { get; set; }
        public string LHMARK { get; set; }
        public string LHORGCODE { get; set; }
        public DateTime ATTR2 { get; set; }
        public DateTime ATTR3 { get; set; }
        public decimal? FSTATUS { get; set; }
        public string FCARNO { get; set; }
        public string FMegreBillNo { get; set; }

        [NotMapped]
        [DbField(false)]
        public string FSTATUS_SHOW
        {
            get
            {
                if (FSTATUS == 2)
                {
                    return "已同步";
                }

                return "未同步";
            }
        }

    }
}
