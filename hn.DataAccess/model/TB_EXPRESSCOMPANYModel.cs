using hn.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DataAccess.CustomAttributes;


namespace hn.DataAccess.Model
{

    [TableName("TB_EXPRESSCOMPANY")]
    [Description("物流承运商")]
    public class TB_EXPRESSCOMPANYModel
    {
        //内码ID
        public string FID { get; set; }
        public string FCODE { get; set; }
        public string FNAME { get; set; }
        public string FCONTACT { get; set; }
        public string FPHONE { get; set; }
        public string FEMAIL { get; set; }
        public string FREMARK { get; set; }
    }

    [TableName("TB_EXPRESSCOMPANY")]
    public class ExpressCompanyModel : TB_EXPRESSCOMPANYModel
    {

    }
}
