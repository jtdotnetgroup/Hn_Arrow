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
using DataAccess.CustomAttributes;
using hn.Core.Model;
using hn.DataAccess.Dal;
using hn.DataAccess.dal.LHModel;

namespace hn.DataAccess.Model
{
    [Table("V_LH_ICPOBILL")]
    [Common.Data.TableName("V_LH_ICPOBILL")]
    [Description("采购订单")]
    public class V_ICPOBILLMODEL : ICPOBILLMODEL
    {
        public string FICSEBILLNAME { get; set; }
        public string FBRANDNAME { get; set; }

        public string FSRCCODE { get; set; }

        public string LH_ORDERTYPENAME { get; set; }

        [NotMapped]
        [JIT_NotMapped]
        [DbField(false)]
        public string LHREVIWESTATUS { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [NotMapped]
        [JIT_NotMapped]
        [DbField(false)]
        public string FSTATUSNAME
        {
            get
            {
                ICPOBILLStatus result;
                Enum.TryParse(this.FSTATUS.ToString(), out result);
                return result.ToString();
            }
        }
    }
}
