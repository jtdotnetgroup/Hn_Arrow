using System.Linq;
using System.Windows.Forms;
using hn.Client.ApiService;
using hn.DataAccess.model;

namespace hn.Client.Views.Diglog
{
    public partial class FrmQueryMCU : DevExpress.XtraEditors.XtraForm
    {
        private APIServiceClient _service = new APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);

        public TB_PREMISE_GRIDVIEW_DTO Result { get; set; }

        public FrmQueryMCU()
        {
            InitializeComponent();
            InitGridView();
            GetData();
        }

        private void GetData()
        {
            var data = _service.GetJYDW(txtName.Text);

            gridMCU.DataSource = data;

        }

        private void InitGridView()
        {
            var t = typeof(TB_PREMISE_GRIDVIEW_DTO);
            var pis = t.GetProperties().ToList();

            pis.ForEach(p =>
            {
                var col = GridColumnBuilder.BuildColumn(p);
                gridViewMCU.Columns.Add(col);
            });
        }

        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            GetData();
        }

        private void gridViewMCU_DoubleClick(object sender, System.EventArgs e)
        {
            var row = gridViewMCU.GetFocusedRow() as TB_PREMISEModel;

            if (row != null)
            {
                Result=new TB_PREMISE_GRIDVIEW_DTO()
                {
                    FID = row.FID,FCODE = row.FCODE,FNAME = row.FNAME
                };
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
        }

        private void gridViewMCU_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                gridViewMCU_DoubleClick(sender,e);
            }
        }

        private void gridViewMCU_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks==2)
            {
                gridViewMCU_DoubleClick(sender,e);
            }
        }
    }
}