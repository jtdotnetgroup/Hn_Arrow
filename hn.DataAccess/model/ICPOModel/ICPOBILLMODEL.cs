﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using hn.Common.Data;
using hn.Common;
using hn.Core.Dal;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.CustomAttributes;
using hn.Core.Model;
using hn.DataAccess.Dal;
using hn.DataAccess.dal.LHModel;

namespace hn.DataAccess.Model
{
    [Table("ICPOBILL")]
    [Common.Data.TableName("ICPOBILL")]
    [Description("采购订单")]
    [JIT_Table("ICPOBILL")]
    public class ICPOBILLMODEL
    {

        /// <summary>
        /// 品牌ID
        /// </summary>
        public string FBRANDID { get; set; }

        /// <summary>
        /// 业务类型ID
        /// </summary>
        public string FTRANSTYPE { get; set; }

        /// <summary>
        /// 厂家账号
        /// </summary>
        public string FCLIENTID { get; set; }

        /// <summary>
        /// 订单日期
        /// </summary>
        public DateTime FDATE { get; set; }

        /// <summary>
        /// 制单人ID
        /// </summary>
        public string FBILLER { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string FTELEPHONE { get; set; }

        /// <summary>
        /// 制单日期
        /// </summary>
        public DateTime FBILLDATE { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? FCHECKDATE { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FREMARK { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        public int FSTATE { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string LH_ORDERTYPE { get; set; }
        /// <summary>
        /// 销售渠道
        /// </summary>
        public string LH_SALESCHANNEL { get; set; }
        /// <summary>
        /// 产品线
        /// </summary>
        public string LH_ORDERPRODLINE { get; set; }
        /// <summary>
        /// 期望到货日期
        /// </summary>
        public DateTime LH_EXPECTEDARRIVEDDATE { get; set; }
        /// <summary>
        /// 促销政策头ID
        /// </summary>
        public string LH_PROMOTIONPOLICYID { get; set; }
        /// <summary>
        /// 出库单号
        /// </summary>
        public string LH_OUTBOUNDORDER { get; set; }
        /// <summary>
        /// 扣款方式
        /// </summary>
        public string LH_ADVERTINGMONEYTYPE { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string LH_BUTYPE { get; set; }


        #region 蒙厂的字段


        /// <summary>
        /// 同步状态
        /// </summary>
        public int FSYNCSTATUS { get; set; }

        /// <summary>
        /// 内码ID
        /// </summary>
        [Key]
        public string FID { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string FBILLNO { get; set; }

        /// <summary>
        /// 厂家订单编号
        /// </summary>
        public string FDesBillNo { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int FSTATUS { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        public string Fcompany { get; set; }

        /// <summary>
        /// 项目审批号码
        /// </summary>
        public string FprojectNO { get; set; }

        /// <summary>
        /// 厂家账户代码（客户代号）
        /// </summary>
        public string FACCOUNT { get; set; }

        /// <summary>
        /// 订单类别
        /// </summary>
        public string FPOtype { get; set; }

        /// <summary>
        /// 价格政策
        /// </summary>
        public string Fpricepolicy { get; set; }
        /// <summary>
        /// 订单描述
        /// </summary>
        public string Fnote { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>

        public string FTIMESTAMP { get; set; }

        /// <summary>
        /// OA状态
        /// </summary>
        public int? OASTATUS { get; set; }

        public string FPREMISEID { get; set; }

        public string FCHECKERID { get; set; }

        #endregion


        /// <summary>
        /// 启用状态
        /// </summary>
        ///
        [NotMapped]
        [JIT_NotMapped]
        [DbField(false)]
        public string FSTATENAME
        {
            get
            {
                return FSTATE == 1 ? "启用" : "禁用";
            }
        }

        [NotMapped]
        [JIT_NotMapped]
        [DbField(false)]
        public string FSYNCSTATUSNAME
        {
            get
            {
                ICPOBILLSyneStatus result;
                 Enum.TryParse(this.FSYNCSTATUS.ToString(), out result);
                 return result.ToString();
            }
        }

       

        /// <summary>
        /// 制单人名称
        /// </summary>
        [NotMapped]
        [JIT_NotMapped]
        [DbField(false)]
        public string FBILLERNAME { get; set; }
        
        [NotMapped]
        [JIT_NotMapped]
        [DbField(false)]
        public string FDATESTR { get; set; }
    }
}
