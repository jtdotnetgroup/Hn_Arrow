using hn.Common.Data; 
using System.ComponentModel.DataAnnotations.Schema; 
namespace hn.DataAccess.dal.LHModel
{
    /// <summary>
    /// 出库单明细
    /// </summary>
    [Table("LH_OUTBOUNDORDERDETAILED")]
    [TableName("LH_OUTBOUNDORDERDETAILED")] 
    public class LH_OUTBOUNDORDERDETAILEDModel
    {
        public string LHODOID { get; set; }
        public string LHODOROWID { get; set; }
        public string LHPRODCODE { get; set; }
        public string LHPRODSTANDARD { get; set; }
        public string LHORDERCODE { get; set; }
        public string CUSTSELFNUM { get; set; }
        public string NUMOFPACKAGE { get; set; }
        public string LHPRODCOLOR { get; set; }
        public string COLORNUM { get; set; }
        public decimal LHSENTEDQTY { get; set; }
        public string LHPRODUNIT { get; set; }
        public decimal LHORIGINALPRICE { get; set; }
        public decimal LHDISCOUNTRATE { get; set; }
        public decimal LHDISCOUNTPRICE { get; set; }
        public decimal LHAMOUNT { get; set; }
        public decimal LHVOLUME { get; set; } 
    }
}
