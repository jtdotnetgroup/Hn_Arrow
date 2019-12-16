using hn.Common.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace hn.DataAccess.model
{
    [Table("v_lhproducts_policy")]
    [TableName("v_lhproducts_policy")]
    [Description("v_lhproducts_policy")]
    public class v_lhproducts_policyModel
    {
        public string HEADID { get; set; }
        /// <summary>
        /// 促销政策名称
        /// </summary>
        public string POLICYNAME { get; set; }  
        public string ORDERTYPE { get; set; }
        public string ORDERSUBTYPE { get; set; }
        public string PRODCHANNEL { get; set; }
        public string DEPTNAME { get; set; }
        /// <summary>
        /// 促销政策行编码id
        /// </summary>
        public string ITEMID { get; set; }
        /// <summary>
        /// 促销政策类型
        /// </summary>
        public string POLICYITEMTYPE { get; set; }
        /// <summary>
        /// 政策起始量
        /// </summary>
        public decimal? MINIMUMQUANTITY { get; set; }
        /// <summary>
        /// 政策封顶量
        /// </summary>
        public decimal? CAPPINGQUANTITY { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal? DISCOUNTRATE { get; set; }
        /// <summary>
        /// 特价
        /// </summary>
        public decimal? SPECIALOFFER { get; set; }
        /// <summary>
        /// 厂家产品代码
        /// </summary>
        public string PRODCODE { get; set; }
        /// <summary>
        /// 厂家名称
        /// </summary>
        public string PRODNAME { get; set; }
        public string LHPRODLINE { get; set; }
        public string LHPRODCHANNEL { get; set; }
        /// <summary>
        /// 厂家规格
        /// </summary>
        public string PRODSTANDARD { get; set; }
        /// <summary>
        /// 产品大类
        /// </summary>
        public string LHPRODTYPE { get; set; }
        public string LHPRODTYPE1 { get; set; }
        public string LHPRODTYPE2 { get; set; }
        /// <summary>
        /// 厂家型号
        /// </summary>
        public string PRODMODEL { get; set; }
        public decimal? PRODVOLUME { get; set; }
        public string PRODSTATUS { get; set; }
        public string LHPRODSIGN { get; set; }
        public DateTime? LASTUPDATE { get; set; }
        public string FID { get; set; }
        /// <summary>
        /// 应用厂家帐号
        /// </summary>
        public string ACCTCODES { get; set; }
    }

    [Table("V_LHPRODUCTS_UNPOLICYHEADID")]
    public class V_LHPRODUCTS_UNPOLICYHEADID:v_lhproducts_policyModel
    {

    }

}
