using DevExpress.XtraGrid.Views.Grid;
using hn.Client.Views.Diglog;
using hn.Common;
using hn.DataAccess.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static hn.Client.FrmPleasePurchasePlan;

namespace hn.Client
{
    public partial class frm组柜计划 : Form
    {
        #region ■------------------ 字段相关

        private DataTable _table = new DataTable();

        private ApiService.APIServiceClient _service;

        private V_ICGROUPBILLMODEL[] _dataSrouce;

        private int _status = 0;

        #endregion

        #region ■------------------ 构造加载

        public frm组柜计划(int status = -1)
        {
            InitializeComponent();

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
            try
            {
                initComboBox();

                txt日期开始.Text = formatDateTime(DateTime.Now);
                txt日期结束.Text = formatDateTime(DateTime.Now);

                foreach (CodeValueClass item in cbo状态.Properties.Items)
                {
                    if (item.value == status.ToStr())
                    {
                        cbo状态.SelectedItem = item;
                    }
                }

                #region 销区列表
                var marketAreaList = _service.GetDics("101", "", true);
                treeList销区.DataSource = marketAreaList;
                #endregion
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void initComboBox()
        {
            //初始化品牌列表
            var list = _service.GetBrandList(Global.LoginUser);
            foreach (var item in list)
            {
                cbo品牌.Properties.Items.Add(item);

                string brandid = IniHelper.ReadString(Global.IniUrl, "CONFIG", "FBRANDID", "");
                if (item.FID == brandid)
                {
                    cbo品牌.SelectedItem = item;
                }
            }

            //初始化单据状态选择            
            cbo状态.Properties.Items.Add(new CodeValueClass("0", "全部"));
            cbo状态.Properties.Items.Add(new CodeValueClass("1", "草稿"));
            cbo状态.Properties.Items.Add(new CodeValueClass("2", "待审核"));
            cbo状态.Properties.Items.Add(new CodeValueClass("3", "审核通过"));
            cbo状态.Properties.Items.Add(new CodeValueClass("4", "审核不通过"));
            cbo状态.Properties.Items.Add(new CodeValueClass("5", "关闭"));
            cbo状态.Properties.Items.Add(new CodeValueClass("6", "完成"));

        }

        #endregion


        #region ■------------------ 运行日志

        private void LogError(Exception ex)
        {
            LogHelper.WriteLog(typeof(FrmSendGoodsPlan), ex);
        }

        private void LogError(string msg)
        {
            LogHelper.WriteLog(typeof(FrmSendGoodsPlan), msg);
        }

        #endregion

        #region ■------------------ 界面实现

        private void panel左_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //using (Pen pen = new Pen(Color.FromArgb(204, 206, 219)))
                //{
                //    e.Graphics.DrawLine(pen, new Point(panel左.Width - 1, 0), new Point(panel左.Width - 1, panel左.Height));
                //}
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void panel销区标题_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //using (Pen pen = new Pen(Color.FromArgb(204, 206, 219)))
                //{
                //    e.Graphics.DrawLine(pen, new Point(0, panel销区标题.Height - 1), new Point(panel销区标题.Width, panel销区标题.Height - 1));
                //}
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void pnl跑龙套1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                using (Pen pen = new Pen(Color.FromArgb(204, 206, 219)))
                {
                    e.Graphics.DrawLine(pen, new Point(0, pnl跑龙套1.Height - 1), new Point(pnl跑龙套1.Width, pnl跑龙套1.Height - 1));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }


        /// <summary>
        /// 调整行号宽度
        /// </summary>
        /// <param name="gridView"></param>
        private void RefreshDataGridHX(int rowsCount, GridView gridView)
        {
            try
            {
                //行号宽度
                int Num = rowsCount;
                if (Num <= 99)
                {
                    if (gridView.IndicatorWidth != 37)
                    {
                        gridView.IndicatorWidth = 37;
                        gridView.InvalidateRows();
                    }
                }
                else if (Num > 99 && Num <= 999)
                {
                    if (gridView.IndicatorWidth != 45)
                    {
                        gridView.IndicatorWidth = 45;
                        gridView.InvalidateRows();
                    }
                }
                else if (Num > 999)
                {
                    if (gridView.IndicatorWidth != 53)
                    {
                        gridView.IndicatorWidth = 53;
                        gridView.InvalidateRows();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void gridView发货计划列表_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            try
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void treeList销区_CustomDrawNodeImages(object sender, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs e)
        {
            e.SelectImageIndex = 18;
        }

        #endregion

        #region ■------------------ 按钮实现

        private void btn新增_Click(object sender, EventArgs e)
        {
            FrmSGPGroupCounter frm = new FrmSGPGroupCounter();
            frm.SaveAfter += new EventHandler(FrmPPPImmediateSendGoods_SaveAfter);
            frm.Show();
        }

        public void FrmPPPImmediateSendGoods_SaveAfter(object obj, EventArgs e)
        {
            this.onSearch();
        }

        private void Frm_SaveAfter(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btn编辑_Click(object sender, EventArgs e)
        {
            if (gridView发货计划列表.FocusedRowHandle > -1)
            {
                var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(gridView发货计划列表.FocusedRowHandle);
                var rowData = _dataSrouce[rowIndex];

                FrmSGPGroupCounter frm = new FrmSGPGroupCounter();
                frm.ICGROUPBILLModel = rowData;
                frm.SaveAfter += new EventHandler(FrmPPPImmediateSendGoods_SaveAfter);
                frm.Show();
            }
        }

        private void btn组柜_Click(object sender, EventArgs e)
        {
            FrmSGPGroupCounter frm = new FrmSGPGroupCounter();
            frm.SaveAfter += new EventHandler(FrmPPPImmediateSendGoods_SaveAfter);
            frm.Show();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    this.onSearch();
            //}
        }

        private void btn重新组柜_Click(object sender, EventArgs e)
        {
            if (gridView发货计划列表.FocusedRowHandle > -1)
            {
                var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(gridView发货计划列表.FocusedRowHandle);
                var rowData = _dataSrouce[rowIndex];

                //进入组柜编辑
                FrmSGPGroupCounter frm = new FrmSGPGroupCounter();
                frm.SaveAfter += new EventHandler(FrmPPPImmediateSendGoods_SaveAfter);
                frm.ICGROUPBILLModel = rowData;
                frm.Show();
                //if (frm.ShowDialog() == DialogResult.OK)
                //{
                //    this.onSearch();
                //}
            }
            else
            {
                MsgHelper.ShowInformation("请选择你要处理的数据！");
            }
        }

        private void btn标识批录_Click(object sender, EventArgs e)
        {
            string ids = "";
            List<string> list = new List<string>();

            for (int i = 0; i < gridView发货计划列表.RowCount; i++)
            {
                bool b = gridView发货计划列表.GetRowCellValue(i, "FCHECK").ToBool();
                if (b)
                {
                    var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(i);
                    var rowData = _dataSrouce[rowIndex];
                    list.Add(rowData.FID);
                }
            }

            if (list.Count > 0)
            {
                ids = string.Join(",", list.ToArray());
            }

            if (ids != "")
            {
                string centen = Interaction.InputBox("请是输入标识内容", "标题", "", -1, -1);
                if(_service.UpdateIdentification(ids, centen))
                {
                    MsgHelper.ShowError("标识保存成功！");
                    this.onSearch();
                }
            }
            else
            {
                MsgHelper.ShowError("请选择要处理的数据！");
            }
        }




        #endregion

        #region ■------------------ 数据筛选

        private void searchControl经营场所_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        #endregion


        #region ■------------------ 数据加载

        private void bgw加载数据_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                for (int i = 0; i < 31; i++)
                {
                    DataRow newRow = _table.NewRow();
                    newRow["组柜编号"] = "组柜编号" + i;
                    newRow["车辆信息"] = "车辆信息" + i;
                    newRow["品牌"] = "品牌" + i;
                    newRow["二级销区"] = "二级销区" + i;
                    newRow["单据编号"] = "单据编号" + i;
                    newRow["状态"] = i;
                    newRow["运输方式"] = "运输方式" + i;
                    newRow["车牌号"] = "车牌号" + i;
                    newRow["载重"] = "载重" + i;
                    newRow["工程名称"] = "工程名称" + i;
                    newRow["发货仓库"] = "发货仓库" + i;
                    newRow["同步状态"] = "同步状态" + i;
                    //_table.Rows.Add(newRow);
                    bgw加载数据.ReportProgress(1, newRow);
                }
                //RefreshDataGridHX(_table.Rows.Count, gridView发货计划列表);
                bgw加载数据.ReportProgress(2, null);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void bgw加载数据_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                switch (e.ProgressPercentage)
                {
                    case 1:
                        _table.Rows.Add(e.UserState as DataRow);
                        break;
                    case 2:
                        RefreshDataGridHX(_table.Rows.Count, gridView发货计划列表);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void bgw加载数据_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }


        #endregion

        private void gridView发货计划列表_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle > -1)
            {
                string fid = gridView发货计划列表.GetRowCellValue(e.FocusedRowHandle, "FID").ToString();

                gridControl发货计划明细.DataSource = _service.GetDeliveryEntryList(fid);
            }
        }

        private void btn查询_Click(object sender, EventArgs e)
        {
            this.onSearch();
        }

        private void btn重置_Click(object sender, EventArgs e)
        {
            txt销区.Text = "";
            cbo品牌.Text = "";
            cbo状态.Text = "";

            txt厂家账户.Text = "";
            txt厂家账户.Tag = null;
            txt承运公司.Text = "";
            txt承运公司.Tag = null;

            txt厂家单号.Text = "";
            txt车辆.Text = "";
            txt发货计划单号.Text = "";
            txt工程名称.Text = "";
            txt组柜单号.Text = "";
            txt日期开始.Text = formatDateTime(DateTime.Now);
            txt日期结束.Text = formatDateTime(DateTime.Now);

            var list = _service.GetGroupList(Global.LoginUser, "", "", "", 0, "", "", "", "", "", "", "", "",
                formatDateTime(txt日期开始.DateTime),
                formatDateTime(txt日期结束.DateTime), !chkClose.Checked);
            gridControl发货计划列表.DataSource = list;
        }

        private void treeList销区_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string text = treeList销区.FocusedNode.GetValue("FNAME").ToString();
            txt销区.Text = text;

            this.onSearch();
        }

        private void btn刷新_Click(object sender, EventArgs e)
        {
            this.onSearch();
        }

        private void onSearch()
        {
            string xq = txt销区.Text;
            string brand = "";
            int status = 0;
            string account = "";
            string expresscompany = "";
            if (cbo品牌.SelectedItem != null)
            {
                TB_BrandModel model = cbo品牌.SelectedItem as TB_BrandModel;
                if (model != null)
                {
                    brand = model.FID;
                    IniHelper.WriteString(Global.IniUrl, "CONFIG", "FBRANDID", model.FID);
                }
            }
            if (cbo状态.SelectedItem != null)
            {
                CodeValueClass model = cbo状态.SelectedItem as CodeValueClass;
                if (model != null)
                    status = model.value.ToInt();
            }

            if (txt厂家账户.Tag != null)
            {
                account = txt厂家账户.Tag.ToString();
            }
            if (txt承运公司.Tag != null)
            {
                expresscompany = txt承运公司.Tag.ToString();
            }

            string car = txt车辆.Text;

            string startdate = formatDateTime(txt日期开始.DateTime);
            string enddate = formatDateTime(txt日期结束.DateTime);


            gridControl发货计划明细.DataSource = null;

            _dataSrouce = _service.GetGroupList(
                Global.LoginUser,
                brand,
                xq == "全部" ? "" : xq,
                "",
                status,
                car,
                "",
                account,
                expresscompany,
                txt厂家单号.Text,
                txt发货计划单号.Text,
                txt组柜单号.Text,
                txt工程名称.Text,
                startdate == "0001/01/01" ? "" : startdate,
                enddate == "0001/01/01" ? "" : enddate,
                !chkClose.Checked);

            gridControl发货计划列表.DataSource = _dataSrouce;



        }

        private void btn退出_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn删除_Click(object sender, EventArgs e)
        {
            string ids = "";
            List<string> list = new List<string>();

            for (int i = 0; i < gridView发货计划列表.RowCount; i++)
            {
                bool b = gridView发货计划列表.GetRowCellValue(i, "FCHECK").ToBool();
                if (b)
                {
                    var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(i);
                    var rowData = _dataSrouce[rowIndex];
                    if (rowData.FSTATUS == 1 || rowData.FSTATUS == 2 || rowData.FSTATUS == 4 || rowData.FSTATUS == 6)
                    {
                        list.Add(rowData.FID);
                    }
                }
            }

            if (list.Count > 0)
            {
                ids = string.Join(",", list.ToArray());
            }

            if (ids != "")
            {
                if (MsgHelper.AskQuestion("确认要删除选中的单据，以及删除对应的其他关联信息吗?"))
                {
                    //调用发货计划删除接口
                    int res = _service.DeleteGroupByIDs(ids);
                    if (res > 0)
                    {
                        MsgHelper.ShowInformation("删除成功！");
                        this.onSearch();
                    }
                    else if (res == -1)
                    {
                        MsgHelper.ShowInformation("已经生成发货计划的组柜计划无法生成。请先删除对应的发货计划！");
                        this.onSearch();
                    }
                }
            }
            else
            {
                MsgHelper.ShowError("请选择要删除的数据，只能删除未审核的数据");
            }
        }

        private void btn审核_Click(object sender, EventArgs e)
        {
            string ids = "";
            List<string> list = new List<string>();

            for (int i = 0; i < gridView发货计划列表.RowCount; i++)
            {
                bool b = gridView发货计划列表.GetRowCellValue(i, "FCHECK").ToBool();
                if (b)
                {
                    var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(i);
                    var rowData = _dataSrouce[rowIndex];
                    if ((rowData.FSTATUS == 1 || rowData.FSTATUS == 2) && !string.IsNullOrEmpty(rowData.FBILLNO))
                    {
                        list.Add(rowData.FID);
                    }
                }
            }

            if (list.Count > 0)
            {
                ids = string.Join(",", list.ToArray());
            }

            if (ids != "")
            {
                FrmAuditDialog dialog = new FrmAuditDialog("审核", "请选择你要做的处理", "通过", "不通过");
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    //调用发货计划审核接口
                    int res = _service.AuditDeliveryByIDs(ids, 3, Global.LoginUser);
                    MsgHelper.ShowInformation("处理成功！");
                    this.onSearch();
                }
                else if (result == DialogResult.No)
                {
                    //调用发货计划审核接口
                    int res = _service.AuditGroupByIDs(ids, 4, Global.LoginUser);
                    MsgHelper.ShowInformation("处理成功！");
                    this.onSearch();
                }

                foreach(var model in _dataSrouce)
                {
                    foreach(string id in list)
                    {
                        if (model.FID == id)
                        {
                            model.FCHECK = true;
                        }
                    }
                }

            }
            else
            {
                MsgHelper.ShowError("审核只能针对待已审核和已生成单号的数据，请确认您选择数据是否正确！");
            }
        }

        private void btn反审_Click(object sender, EventArgs e)
        {
            string ids = "";
            List<string> list = new List<string>();

            for (int i = 0; i < gridView发货计划列表.RowCount; i++)
            {
                bool b = gridView发货计划列表.GetRowCellValue(i, "FCHECK").ToBool();
                if (b)
                {
                    var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(i);
                    var rowData = _dataSrouce[rowIndex];
                    if (rowData.FSYNCSTATUS < 1)
                    {
                        list.Add(rowData.FID);
                    }
                }
            }


            if (list.Count > 0)
            {
                ids = string.Join(",", list.ToArray());
            }

            if (ids != "")
            {
                if (MsgHelper.AskQuestion("点击确认退回到待审核状态，可修改资料！"))
                {
                    //调用发货计划删除接口
                    int res = _service.AuditAntiGroupByIDs(ids);
                    if (res > 0)
                    {
                        MsgHelper.ShowInformation("处理成功！");
                        this.onSearch();
                    }
                }
            }
            else
            {
                MsgHelper.ShowError("反审核只能针对已审核并且未同步到厂家的数据，请先勾选您要处理的数据！");
            }
        }

        private void btn导出_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出Excel";
            fileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                gridControl发货计划列表.ExportToXls(fileDialog.FileName);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    {
                        btn导出_Click(null, null);
                        break;
                    }
                case Keys.F3:
                    {
                        btn新增_Click(null, null);
                        break;
                    }
                case Keys.F4:
                    {
                        btn编辑_Click(null, null);
                        break;
                    }
                case Keys.F5:
                    {
                        onSearch();
                        break;
                    }
                case Keys.F6:
                    {
                        btn组柜_Click(null, null);
                        break;
                    }
                case Keys.F7:
                    {
                        btn重新组柜_Click(null, null);
                        break;
                    }
                case Keys.F8:
                    {
                        btn删除_Click(null, null);
                        break;
                    }
                case Keys.F9:
                    {
                        btn审核_Click(null, null);
                        break;
                    }
                case Keys.F10:
                    {
                        btn反审_Click(null, null);
                        break;
                    }
                 case Keys.F12:
                    {
                        btn标识批录_Click(null, null);
                        break;
                    }
                case Keys.Escape:
                    {
                        this.Close();
                        break;
                    }
            }
            base.OnKeyDown(e);
        }

        private void gridView发货计划列表_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle > -1)
            {
                string fid = gridView发货计划列表.GetRowCellValue(e.RowHandle, "FID").ToString();

                gridControl发货计划明细.DataSource = _service.GetGroupEntryList(fid);
            }
        }

        private void txt经营场所_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string brand = "";
            if (cbo品牌.SelectedItem != null)
            {
                TB_BrandModel model = cbo品牌.SelectedItem as TB_BrandModel;
                if (model != null)
                    brand = model.FID;
            }

            FrmQueryClientAccount frm = new FrmQueryClientAccount(brand);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txt厂家账户.Text = frm.SelectName;
                txt厂家账户.Tag = frm.SelectID;
            }
        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmQueryExpressCompany frm = new FrmQueryExpressCompany();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txt承运公司.Text = frm.SelectName;
                txt承运公司.Tag = frm.SelectID;
            }
        }

        private void gridView发货计划列表_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "FSYNCSTATUS")
            {
                switch (e.CellValue.ToInt())
                {
                    case 1:
                    case 2:
                        {
                            e.Appearance.ForeColor = Color.Blue;
                            break;
                        }
                    case -1:
                        {
                            e.Appearance.ForeColor = Color.Red;
                            break;
                        }
                    case 3:
                        {
                            e.Appearance.ForeColor = Color.Gray;
                            break;
                        }
                }
            }
        }

        private void gridView发货计划列表_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "FSYNCSTATUS")
            {
                switch (e.DisplayText)
                {
                    case "0":
                        e.DisplayText = "等待上传";
                        break;
                    case "1":
                        e.DisplayText = "已上传到厂家";
                        break;
                    case "-1":
                        e.DisplayText = "厂家检查不通过";
                        break;
                    case "2":
                        e.DisplayText = "厂家更新成功";
                        break;
                    case "3":
                        e.DisplayText = "上传完成";
                        break;
                }
            }
        }

        private void dropDownButton1_Click(object sender, EventArgs e)
        {
            if (!pnl日期.Visible)
            {
                pnl日期.Visible = true;
                pnl跑龙套2.Height = pnl跑龙套2.Height + 40;
            }
            else
            {
                pnl日期.Visible = false;
                pnl跑龙套2.Height = pnl跑龙套2.Height - 40;
            }
        }

        private string formatDateTime(DateTime date)
        {
            System.Globalization.DateTimeFormatInfo dtFormat = new System.Globalization.DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy/MM/dd";

            return date.ToString("yyyy/MM/dd", dtFormat);
        }

        private void chk全选_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var model in _dataSrouce)
            {
                model.FCHECK = chk全选.Checked;
            }
            gridControl发货计划列表.DataSource = _dataSrouce;
            gridControl发货计划列表.RefreshDataSource();
        }

        private void gridView发货计划明细_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (gridControl发货计划明细.DataSource != null && e.RowHandle > -1)
            {
                var list = gridControl发货计划明细.DataSource as V_ICSEOUTBILLENTRYMODEL[];
                var row = list[gridView发货计划明细.GetDataSourceRowIndex(e.RowHandle)];
                if (row.FGROUP_STATUS == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(211, 211, 211);
                }
            }
        }

        private void btn约车_Click(object sender, EventArgs e)
        {
            string ids = "";
            List<string> list = new List<string>();

            for (int i = 0; i < gridView发货计划列表.RowCount; i++)
            {
                bool b = gridView发货计划列表.GetRowCellValue(i, "FCHECK").ToBool();
                if (b)
                {
                    var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(i);
                    var rowData = _dataSrouce[rowIndex];

                    //发货计划单处于草稿状态，并且约车状态为待发布的时候用户即可勾选多条记录进行约车
                    //if (rowData.FSTATUS == 1 && rowData.FCAR_STATUS == 1)
                    if (rowData.FCAR_STATUS == 1)
                    {
                        list.Add(rowData.FID);
                    }
                }
            }

            if (list.Count > 0)
            {
                _service.GroupContractCar(list.ToArray());
                MsgHelper.ShowInformation("处理完成！");
                this.onSearch();
            }
            else
            {
                MsgHelper.ShowError("请选择要处理的数据！");
            }
        }

        private void btn约车变更_Click(object sender, EventArgs e)
        {
            if (gridView发货计划列表.FocusedRowHandle > -1)
            {
                var rowIndex = gridView发货计划列表.GetDataSourceRowIndex(gridView发货计划列表.FocusedRowHandle);
                var rowData = _dataSrouce[rowIndex];


                List<string> list = new List<string>();
                list.Add(rowData.FID);

                if (list.Count > 0)
                {
                    _service.GroupContractCar(list.ToArray());
                    MsgHelper.ShowInformation("处理完成！");
                    this.onSearch();
                }
                else
                {
                    MsgHelper.ShowError("请选择要处理的数据！");
                }
            }
        }

    }
}
