﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace hn.ArrowInterface.Entities
{
    public class ICPOBILL
    {
        public string FID { get; set; }
        public string FBRANDID { get; set; }
        public string FTRANSTYPE { get; set; }
        public string FCLIENTID { get; set; }
        public string FBILLNO { get; set; }
        public DateTime FDATE { get; set; }
        public string FBILLER { get; set; }
        public string FTELEPHONE { get; set; }
        public DateTime FBILLDATE { get; set; }
        public decimal FSTATUS { get; set; }
        public DateTime FCHECKDATE { get; set; }
        public string FREMARK { get; set; }
        public decimal FSTATE { get; set; }
        public decimal FSYNCSTATUS { get; set; }
        public string FICSEBILLID { get; set; }
        public string FDESBILLNO { get; set; }
        public string FCOMPANY { get; set; }
        public string FPROJECTNO { get; set; }
        public string FACCOUNT { get; set; }
        public string FPOTYPE { get; set; }
        public string FPRICEPOLICY { get; set; }
        public string FNOTE { get; set; }
        public DateTime FTIMESTAMP { get; set; }

        //COMMENT ON COLUMN "PURCHASE"."ICPOBILL"."FSTATUS" IS '1:草稿  2:待审核  3:审核通过  4:审核不通过  5:关闭  6:完成  7:部分审核';
        //COMMENT ON COLUMN "PURCHASE"."ICPOBILL"."FSTATE" IS '0：禁用  1：正常    默认：1';
        //COMMENT ON COLUMN "PURCHASE"."ICPOBILL"."FSYNCSTATUS" IS '1：未同步  2：已同步  3：同步失败';
    }

    public class ICPOBILL_PolicyDTO
    {
        /// <summary>
        /// 所属公司名称
        /// </summary>
        [Required(ErrorMessage = "所属公司不能为空")]
        public string BrandName { get; set; }
        /// <summary>
        /// 厂家帐号
        /// </summary>
        ///
        [Required(ErrorMessage = "厂家帐号不能为空")]
        public string Account { get; set; }
        /// <summary>
        /// 渠道
        /// </summary>
        [Required(ErrorMessage = "渠道不能为空")]
        public string Channel { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        [Required(ErrorMessage = "订单类型不能为空")]
        public string OrderType { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        [Required(ErrorMessage = "业务类型不能为空")]
        public string OrderSubType { get; set; }


        public string HeadID { get; set; }

    }
}
