using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.CustomAttributes;
using hn.ArrowInterface.WebCommon;

namespace hn.ArrowInterface.RequestParams
{
    [TableName("V_LH_OUTBOUNDORDER_CARNO")]
    public class ObOrderUploadParam: AbstractRequestParams
{
        public string lhodoID { get; set; }
        
        public string lhplateNo { get; set; }

        
        
    }

    public class ObOrderUploadPrarams 
    {
        public List<ObOrderUploadParam> Rows { get; set; }
    }
}