using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.CustomAttributes;


namespace hn.DataAccess.dal.LHModel
{
    [Table("MergeBill")]
    [TableName("MergeBill")]
    public class LH_MergeBill
    {
        /// <summary>
        /// 分货批号
        /// </summary>
        [Required]
        public string FBILLNO { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        [Required(ErrorMessage = "车牌不能为空")]
        public string FCARNO { get; set; }
        /// <summary>
        /// 经营单位
        /// </summary>
        public string FMCU { get; set; }
        /// <summary>
        /// 承运商
        /// </summary>
        [Required(ErrorMessage = "承运商不能为空")]
        public string FCARRIER { get; set; }

    }
}