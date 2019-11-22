using hn.DataAccess.dal.LHModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hn.Client
{
    public partial class FrmOutOrderDetailed : Form
    {
        #region 窗体
        public FrmOutOrderDetailed()
        {
            InitializeComponent(); 
        }
        private void FrmOutOrderList_Load(object sender, EventArgs e)
        {
            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
            Init();
        }
        #endregion

        #region 按钮事件 
        private void btnSave_Click(object sender, EventArgs e)
        {
            LH_OUTBOUNDORDERModel[] row = BillGrid.DataSource as LH_OUTBOUNDORDERModel[];
            if (row.Length == 0) { MessageBox.Show("程序异常!"); return; }
            if (_service.LH_OUTBOUNDORDER_Update_FCARNO(row[0].LHODONO, row[0].FCARNO))
            {
                MessageBox.Show("保存成功!");
                this.Close();
            }
            else {
                MessageBox.Show("保存失败");
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 点击列表
        /// </summary>
        private void BillList_Click(object sender, EventArgs e)
        {
            LaodDateiledData();
        }
        /// <summary>
        /// 双击列表
        /// </summary>
        private void BillList_DoubleClick(object sender, EventArgs e)
        {
            LH_OUTBOUNDORDERModel[] row = BillGrid.DataSource as LH_OUTBOUNDORDERModel[];
            if (row == null) { return; }
            if (row.Length < BillGrid.GetFocusedDataSourceRowIndex()) { return; }
            LH_OUTBOUNDORDERModel LH_OutOrderModel = row[BillGrid.GetFocusedDataSourceRowIndex()];

        }
        #endregion

        #region 公共变量
        ApiService.APIServiceClient _service;
        public List<LH_OUTBOUNDORDERModel> BillData = new List<LH_OUTBOUNDORDERModel>(); 
        List<ColStyle> BillStyle;
        List<ColStyle> EntryStyle;
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        void Init()
        {
            Style();
            LaodData();
            LaodDateiledData();
        } 
        /// <summary>
        /// 所有控件样式控制
        /// </summary>
        void Style()
        {
            BillGrid.OptionsBehavior.Editable = true;
            BillGrid.OptionsCustomization.AllowFilter = true; 
            BillStyle = new List<ColStyle>();
            BillStyle.AddRange(new ColStyle[]{
            new ColStyle
            {
                Caption = "出库单号",
                FieldName = "LHODONO",
                Visible = true,
                Width = 140,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "总数量",
                FieldName = "LHTOTALNUMBER",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "订单类型",
                FieldName = "LHODOTYPE",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "客户编号",
                FieldName = "ACCTCODE",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "客户名称",
                FieldName = "ACCTNAME",
                Visible = true,
                Width = 350,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "总金额",
                FieldName = "LHTOTALAMOUNT",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "总体积",
                FieldName = "LHTOTALVOLUME",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "发货基地",
                FieldName = "LHDELIVERYBASE",
                Visible = true,
                Width = 200,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "提货人",
                FieldName = "LHDELIVERYMAN",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "合同编号",
                FieldName = "LHCONTRACTNO",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "法人客户名称",
                FieldName = "BILLACCTNAME",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "法人客户编号",
                FieldName = "BILLACCTCODE",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "广告费点数",
                FieldName = "LHADVERTISINGPOINT",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "发货日期",
                FieldName = "LHDELIVERYTIME",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "车牌号",
                FieldName = "LHPLATENO",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "返点点数",
                FieldName = "LHREBATESPOINT",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "业务类型",
                FieldName = "LHBUTYPE",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "备注",
                FieldName = "LHMARK",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "事业部",
                FieldName = "LHORGCODE",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "开始时间",
                FieldName = "ATTR2",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "结束时间",
                FieldName = "ATTR3",
                Visible = true,
                Width = 100,
                ReadOnly = true
            },
            new ColStyle
            {
                Caption = "同步的车牌",
                FieldName = "FCARNO",
                Visible = true,
                ReadOnly = false,
                Width = 100,
            }
            });
            SetCol(BillGrid, BillStyle);

            EntryGrid.OptionsBehavior.Editable = false;
            EntryStyle = new List<ColStyle>();
            EntryStyle.AddRange(new ColStyle[] {
            new ColStyle
            {
                Caption = "产品编号",
                FieldName = "LHPRODCODE",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "产品名称",
                FieldName = "LHPRODSTANDARD",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "订单单号",
                FieldName = "LHORDERCODE",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "客户自编号",
                FieldName = "CUSTSELFNUM",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "包装件数",
                FieldName = "NUMOFPACKAGE",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "产品规格",
                FieldName = "LHPRODSTANDARD",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "颜色",
                FieldName = "LHPRODCOLOR",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "色号",
                FieldName = "COLORNUM",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "实发数量",
                FieldName = "LHSENTEDQTY",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "单位",
                FieldName = "LHPRODUNIT",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "标准单价",
                FieldName = "LHORIGINALPRICE",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "应用折扣率",
                FieldName = "LHDISCOUNTRATE",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "折后单价",
                FieldName = "LHDISCOUNTPRICE",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "金额",
                FieldName = "LHAMOUNT",
                Visible = true,
                Width = 100,
            },
            new ColStyle
            {
                Caption = "体积",
                FieldName = "LHVOLUME",
                Visible = true,
                Width = 100,
            }
            });
            SetCol(EntryGrid, EntryStyle);
        }
        /// <summary>
        /// 设置列样式
        /// </summary>
        /// <param name="Grid"></param>
        /// <param name="colStyles"></param>
        void SetCol(DevExpress.XtraGrid.Views.Grid.GridView Grid, List<ColStyle> colStyles)
        {
            foreach (var item in colStyles)
            {
                var DataCol = new DevExpress.XtraGrid.Columns.GridColumn();
                DataCol.Caption = item.Caption;
                DataCol.FieldName = item.FieldName;
                DataCol.Visible = item.Visible;
                DataCol.OptionsColumn.ReadOnly = item.ReadOnly;
                DataCol.OptionsColumn.AllowEdit = !item.ReadOnly;
                DataCol.Width = item.Width;
                DataCol.DisplayFormat.FormatType = item.FormatType;
                DataCol.VisibleIndex = item.VisibleIndex;
                Grid.Columns.Add(DataCol);
            }
        }
        /// <summary>
        /// 填充表格
        /// </summary>
        void LaodData() { 
            BillList.DataSource = _service.LH_OUTBOUNDORDER_LHODOID_List(BillData.Select(s=>s.LHODOID).ToArray());
        }
        /// <summary>
        /// 加载明细
        /// </summary>
        void LaodDateiledData()
        { 
            EntryList.DataSource = _service.LH_OUTBOUNDORDERDETAILED_List(BillData[0].LHODOID);
        }
        /// <summary>
        /// 表格样式
        /// </summary>
        public class ColStyle
        {
            public string Caption { get; set; }
            public string FieldName { get; set; }
            public bool Visible { get; set; }
            public int Width { get; set; }
            public bool ReadOnly { get; set; }
            public int VisibleIndex { get; set; }
            public DevExpress.Utils.FormatType FormatType { get; set; }
        }
    }
}
