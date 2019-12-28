using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hn.Common.Data;
using hn.DataAccess.Model;

namespace hn.DataAccess.dal.LHModel
{
    [Table("ICPOBILLENTRY")]
    [TableName("ICPOBILLENTRY")]
    [Description("采购订单明细")]
    public class LH_ICPOBILLENTRYMODEL: ICPOBILLENTRYMODEL
    {
       
    }
}