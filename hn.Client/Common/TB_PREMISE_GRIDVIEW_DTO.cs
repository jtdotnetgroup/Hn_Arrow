using hn.Client.CustomAttribute;

namespace hn.Client
{
    public class TB_PREMISE_GRIDVIEW_DTO
    {
        [GridColumn("FID",false,100,0,false)]
        public string FID { get; set; }
        [GridColumn("经营场所编码")]
        public string FCODE { get; set; }
        [GridColumn("经营场所名称")]
        public string FNAME { get; set; }
    }
}