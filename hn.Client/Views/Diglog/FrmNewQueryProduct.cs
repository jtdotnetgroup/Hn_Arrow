using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hn.Client
{
    public partial class FrmNewQueryProduct : FrmBase
    {
        ApiService.APIServiceClient _service;

        static object lockObject = new object();
        static FrmNewQueryProduct instance;

        public FrmNewQueryProduct()
        {
            //instance = new FrmNewQueryProduct();
            InitializeComponent();
            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
        }

        public static FrmNewQueryProduct GetInstance()
        {
            lock (lockObject)
            { 
                return instance;
            }
        }


        public string itemid;
        public string SelectID;
        public string SelectName;

        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, v_lhproducts_policyModel[]> dicDataSource = new Dictionary<string, v_lhproducts_policyModel[]>(); 
        public v_lhproducts_policyModel SelectData;

        

        private void FrmQueryMarketArea_Load(object sender, EventArgs e)
        {
            onSearch("");
        }

        private void onSearch(string keyword)
        {
            this.Cursor = Cursors.WaitCursor;
            if (!dicDataSource.ContainsKey(itemid))
            {
                dicDataSource.Add(itemid, _service.v_lhproducts_policy_List(itemid, ""));
            } 
            gridControl.DataSource = dicDataSource[itemid];
            this.Cursor = Cursors.Default;

            gridControl.Focus();
        }

        private void btn查询_Click(object sender, EventArgs e)
        {
            onSearch(txt关键字.Text);
        }

        private void btn重置_Click(object sender, EventArgs e)
        {
            txt关键字.Text = "";
            onSearch("");
        }

        private void gridView名称代码_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView名称代码_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView名称代码.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内 
                if (hInfo.InRow)
                {
                    var list = new List<v_lhproducts_policyModel>(gridControl.DataSource as v_lhproducts_policyModel[]);
                    this.SelectData = list[gridView名称代码.GetDataSourceRowIndex(gridView名称代码.FocusedRowHandle)];
                    this.SelectID = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "HEADID").ToString();
                    this.SelectName = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "POLICYNAME").ToString();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void FrmQueryProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.onSearch(txt关键字.Text);
            }
        }

        private void gridControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var list = new List<v_lhproducts_policyModel>(gridControl.DataSource as v_lhproducts_policyModel[]);
                this.SelectData = list[gridView名称代码.GetDataSourceRowIndex(gridView名称代码.FocusedRowHandle)];
                this.SelectID = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "HEADID").ToString();
                this.SelectName = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "POLICYNAME").ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
