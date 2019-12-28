using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Office.Utils;
using DevExpress.XtraEditors;
using hn.Client.ApiService;
using hn.DataAccess;
using hn.DataAccess.dal.LHModel;

namespace hn.Client.Views.Diglog
{
    public partial class FrmBatchOutBoundOrder : DevExpress.XtraEditors.XtraForm
    {

        ApiService.APIServiceClient serviceClient=new APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);

        public LH_MergeBill Result { get; set; }
        public List<string> LHOBODNOS { get; set; }

        public FrmBatchOutBoundOrder()
        {
            InitializeComponent();
            LHOBODNOS=new List<string>();
        }

        
        private void FrmBatchOutBoundOrder_Load(object sender, EventArgs e)
        {
            if (this.Result == null)
            {
                var billNo = serviceClient.GetNewBillNo("FQ");
                txtBatchNo.Text = billNo;
                Result = new LH_MergeBill();
                Result.FBILLNO = billNo;
                Result.FCARNO = txtCarNo.Text;
            }

            txtBatchNo.Text = Result.FBILLNO;
            txtCarNo.Text = Result.FCARNO;

            SetMCUSelectList();
           SetCarriersSelectList();

        }

        private void SetMCUSelectList()
        {

            if (!string.IsNullOrEmpty(Result.FMCU))
            {
                var mcu = serviceClient.GetJYDW(Result.FMCU).FirstOrDefault();

                if (mcu != null)
                {
                    MCUSearchList.Text = mcu.FNAME;
                }
            }
        }

        private void SetCarriersSelectList()
        {
            if (!string.IsNullOrEmpty(Result.FCARRIER))
            {
                var carrier = serviceClient.GetExpressCompanyList(this.Result.FCARRIER);
                CarriersSelectList.Text = carrier.SingleOrDefault().FNAME;
            }
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            var mesage= DataValidation.Validata(Result);
            if (mesage == null)
            {
                try
                {
                   var result= serviceClient.SaveLH_MergeBill(this.Result, LHOBODNOS.ToArray());
                   var msg = result ? "保存成功" : "保存失败";
                   MsgHelper.ShowInformation(msg);
                   if (result)
                   {
                       this.Dispose();
                   }
                }
                catch (Exception exception)
                {
                    MsgHelper.ShowError(exception.Message);
                }
               
            }
            else
            {
                StringBuilder builder=new StringBuilder();
                foreach (var m in mesage)
                {
                    builder.Append($"{m}\r\n");
                }
                MsgHelper.ShowInformation(builder.ToString());
            }

        }

        private void CarriersSelectList_Properties_Click(object sender, EventArgs e)
        {
            FrmQueryExpressCompany frm=new FrmQueryExpressCompany();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Result.FCARRIER = frm.SelectID;
                CarriersSelectList.Text = frm.SelectName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void MCUSearchList_Properties_Click(object sender, EventArgs e)
        {
            FrmQueryMCU frm=new FrmQueryMCU();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Result.FMCU = frm.Result.FID;
                MCUSearchList.Text = frm.Result.FNAME;
            }
        }

        private void txtCarNo_EditValueChanged(object sender, EventArgs e)
        {
            this.Result.FCARNO = txtCarNo.Text;
        }

        private void CarriersSelectList_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CarriersSelectList.Text))
            {
                this.Result.FCARRIER = null;
            }
        }

        private void MCUSearchList_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MCUSearchList.Text))
            {
                this.Result.FMCU = null;
            }
        }
    }
}