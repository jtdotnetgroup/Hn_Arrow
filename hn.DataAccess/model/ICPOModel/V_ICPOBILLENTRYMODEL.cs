using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using hn.Common.Data;
using hn.Common;
using hn.Core.Dal;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using hn.Core.Model;
using hn.DataAccess.Dal;
namespace hn.DataAccess.Model
{
    [TableName("V_LH_ICPOBILLENTRY")]
    [Description("采购订单明细")]
    public class V_ICPOBILLENTRYMODEL : ICPOBILLENTRYMODEL
    {

        /// <summary>
        /// 产品系列
        /// </summary>
        public string FPRODUCTTYPE { get; set; }

        /// <summary>
        /// 产品代码
        /// </summary>
        public string FPRODUCTCODE { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string FPRODUCTNAME { get; set; }


        /// <summary>
        /// 产品型号
        /// </summary>
        public string FMODEL { get; set; }

        /// <summary>
        /// 基本单位
        /// </summary>
        public string FUNITID { get; set; }

        /// <summary>
        /// 采购单位名称
        /// </summary>
        public string FUNITNAME { get; set; }

        public string FORDERUNIT { get; set; }

        public string PRODSTANDARD { get; set; }
        public string PRODMODEL { get; set; }

        /// <summary>
        /// 基本单位名称
        /// </summary>
        public decimal FASKQTY { get; set; }

        /// <summary>
        /// 厂家订单单价
        /// </summary>
        public decimal? LHDISCOUNTPRICE { get; set; }

        [DbField(false)]
        public int cjkcs { get; set; }
        public string FNEEDDATESTR { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [DbField(false)]
        public string FSTATUSNAME
        {
            get
            {
                return Enum.GetName(typeof(Constant.BILL_FSTATUS), FSTATUS);
            }
        }

        /// <summary>
        /// 启用状态
        /// </summary>
        [DbField(false)]
        public string FSTATENAME
        {
            get
            {
                return FSTATE == 1 ? "启用" : "禁用";
            }
        }


        public string PZ
        {
            get
            {
                string result = "";

                if (!string.IsNullOrEmpty(this.FSRCMODEL))
                {
                    result = FSRCMODEL.Split(new[] { "||" }, StringSplitOptions.None)[0];
                }

                return result;
            }

        }

        public string XH
        {
            get
            {
                string result = "";

                if (!string.IsNullOrEmpty(this.FSRCMODEL))
                {
                    result = FSRCMODEL.Split(new[] { "||" }, StringSplitOptions.None)[1];
                }

                return result;
            }
        }

        public string GG
        {
            get
            {
                string result = "";

                if (!string.IsNullOrEmpty(this.FSRCMODEL))
                {
                    result = FSRCMODEL.Split(new []{"||"}, StringSplitOptions.None)[2];
                }

                return result;
            }
        }
    }
}
