using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hn.Common.Data;
using hn.DataAccess.Model;

namespace hn.DataAccess.dal.LHModel
{
    [Table("ICPOBILL")]
    [TableName("ICPOBILL")]
    [Description("采购订单")]
    public class LH_ICPOBILLMODEL:ICPOBILLMODEL
    {
        [NotMapped]
        [DbField(false)]
        private List<LH_ICPOBILLENTRYMODEL> Entities { get; set; }
    }
}