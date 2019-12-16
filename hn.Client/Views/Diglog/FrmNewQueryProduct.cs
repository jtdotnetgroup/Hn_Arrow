using hn.DataAccess.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using hn.ArrowInterface.Entities;
using hn.Client.ApiService;
using hn.DataAccess.model.Common;

namespace hn.Client
{
    public partial class FrmNewQueryProduct : FrmBase
    {
        ApiService.APIServiceClient _service;

        public List<v_lhproducts_policyModel> SelectRows;

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
        public ICPOBILL_PolicyDTO header { get; set; }
        private int page = 1;//当前面
        private int size=50;//每页显示量
        private int total = 0;//总条数
        private int count = 0;//总页数
        

        private void FrmQueryMarketArea_Load(object sender, EventArgs e)
        {
            onSearch(GetWhere());
        }

        private void onSearch(v_lhproducts_policyModel where)
        {

            var action = new Action( () =>
            {
                PageResult<v_lhproducts_policyModel> result = null;
                string countStr = $"第【{page}】页/共【{count}】页";

                try
                {
                   
                    btnNext.Enabled = btnLast.Enabled = btn查询.Enabled = btn重置.Enabled=btnConfirm.Enabled=btnReturn.Enabled = false;
                    result = _service.GetPolicyProducts(header, where, page, size);

                    total = result.Total;

                    count = total / size;
                    count += total % size > 0 ? 1 : 0;

                    countStr = $"第【{page}】页/共【{count}】页";

                    labCount.Text = countStr;
                    gridControl.DataSource = result.Result;

                    btnNext.Enabled = btnLast.Enabled =
                        btn查询.Enabled = btn重置.Enabled = btnConfirm.Enabled = btnReturn.Enabled = true;


                }
                catch (Exception e)
                {
                    MsgHelper.ShowInformation(e.Message);
                }

            });

            var t1 = new ThreadStart(action);

            t1.Invoke();
        }

        private void btn查询_Click(object sender, EventArgs e)
        {
           
            onSearch(GetWhere());
        }

        private void btn重置_Click(object sender, EventArgs e)
        {
            onSearch(GetWhere());
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
            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hInfo = gridView名称代码.CalcHitInfo(new Point(e.X, e.Y));
            //if (e.Button == MouseButtons.Left && e.Clicks == 2)
            //{
            //    //判断光标是否在行范围内 
            //    if (hInfo.InRow)
            //    {
            //        var list = new List<v_lhproducts_policyModel>(gridControl.DataSource as v_lhproducts_policyModel[]);
            //        this.SelectData = list[gridView名称代码.GetDataSourceRowIndex(gridView名称代码.FocusedRowHandle)];
            //        this.SelectID = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "HEADID").ToString();
            //        this.SelectName = gridView名称代码.GetRowCellValue(gridView名称代码.FocusedRowHandle, "POLICYNAME").ToString();
            //        this.DialogResult = DialogResult.OK;
            //        this.Close();
            //    }
            //}
        }

        private void FrmQueryProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                onSearch(GetWhere());
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

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            page = 1;
            onSearch(GetWhere());
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            page = count;
            onSearch(GetWhere());
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            page = page - 1 < 1 ? 1 : page - 1;
            onSearch(GetWhere());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            page = page + 1 > count ? count : page + 1;
            onSearch(GetWhere());
        }

        private v_lhproducts_policyModel GetWhere()
        {
            var where = new v_lhproducts_policyModel();
            where.PRODNAME = txtFactoryName.Text;
            where.PRODMODEL = txtFactoryModel.Text;
            where.PRODSTANDARD = txtSpecifications.Text;
            return where;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            
            var selectedIndexs = gridView名称代码.GetSelectedRows();

            this.SelectRows=new  List<v_lhproducts_policyModel>();

            foreach (var i in selectedIndexs)
            {
                var row = gridView名称代码.GetRow(i) as v_lhproducts_policyModel;
                SelectRows.Add(row);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
