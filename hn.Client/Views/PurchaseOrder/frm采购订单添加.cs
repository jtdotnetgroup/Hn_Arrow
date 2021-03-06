﻿using hn.ArrowInterface.Entities;
using hn.Client.Views.Diglog;
using hn.Common;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic;
using System.Windows.Forms;
using hn.Core;

namespace hn.Client
{
    public partial class FrmPurchaseOrder : FrmBase
    {
        #region ■------------------ 字段相关
        public event EventHandler SaveAfter;
        private DataTable _table = new DataTable();

        private DataTable _tableMarketArea;

        ApiService.APIServiceClient _service;
        V_ICPOBILLMODEL model = new V_ICPOBILLMODEL();
        #endregion

        #region ■------------------ 构造加载

        public FrmPurchaseOrder(int status = 3)
        {
            InitializeComponent();
            IniValue();
            txtCreater.Text = Global.LoginUser.UserName;
            txtCreater.Tag = Global.LoginUser.FID;
            txtCreater.Enabled = false;
            dateDatetime.DateTime = DateTime.Now;

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);

            txtBillNO.Text = _service.GetNewBillNo("PO");

            try
            {
                #region 请购计划列表

                initComboBox();

                #endregion

                #region 销区列表
                var marketAreaList = _service.GetDics("101", "", true);

                //treeList销区.DataSource = marketAreaList;

                #endregion

                bgw加载数据.RunWorkerAsync();


            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }


        bool bEdit = false;
        ICPOBILLMODEL ICPOBILLMODELm = new ICPOBILLMODEL();
        public FrmPurchaseOrder(V_ICPOBILLMODEL pModel, bool bzf = false)
        {
            InitializeComponent();

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);
            
            

            if (!pModel.FPREMISEID.Empty())
            {
                var mcu = _service.GetJYDW(pModel.FPREMISEID);
                txtMcu.Tag = mcu.First();
                txtMcu.Text = mcu.First().FNAME;
            }

            
            ICPOBILLMODELm = _service.GetSingleOrder(pModel.FID);
            IniValue();
            bEdit = true;
            model = pModel;
            txtCreater.Text = model.FBILLERNAME;
            txtCreater.Tag = model.FBILLER;
            txtCreater.Enabled = false;
            dateDatetime.DateTime = model.FBILLDATE;
            var listAccount = _service.GetClientAccountList(model.FBRANDID, "");
            txtBillNO.Text = model.FBILLNO;

            txtProjectNo.Text = ICPOBILLMODELm.FprojectNO;

            //
            foreach (var sub in listAccount)
            {
                if (sub.FID == model.FCLIENTID)
                {
                    txt厂家账户.Tag = sub.FID;
                    txt厂家账户.Text = sub.FACCOUNT;
                    txtFName.Text = sub.FNAME;
                    break;
                }
            }

            txtRemarks.Text = model.FREMARK;
            search价格策略.Text = model.Fpricepolicy;
            search价格策略.Tag = model.Fpricepolicy;
            searchDic105.Text = model.FPOtype;
            searchDic105.Tag = model.FPOtype;
            txtLH_OUTBOUNDORDER.Text = ICPOBILLMODELm.LH_OUTBOUNDORDER;
            dateDHRQ.Text = ICPOBILLMODELm.LH_EXPECTEDARRIVEDDATE.ToStr();
            //初始化品牌列表
            var list = _service.GetBrandList(Global.LoginUser);


            foreach (var item in list)
            {

                comBrand.Properties.Items.Add(item);
                if (item.FID == model.FBRANDID)
                {
                    comBrand.SelectedItem = item;
                }

            }

            listCG = _service.GetOrderEntryList(model.FID, null).ToList();

            var tmp = _service.ICPOBILLENTRYMODEL_List(model.FID);
            foreach (var item in tmp)
            {
                var mtiem = listCG.Where(w => w.FID.Equals(item.FID)).FirstOrDefault();
                mtiem.FERR_MESSAGE = item.FERR_MESSAGE;
                mtiem.FSRCMODEL = item.FSRCMODEL;
                mtiem.FORDERUNIT = item.Funit;
                mtiem.Funit = item.Funit;
                mtiem.Flevel = item.Flevel;
            }
            foreach (var sub in listCG)
            {
                ProductViewModel pro = _service.getProductView(sub.FITEMID);
                if (pro == null) continue;
                sub.Funit = pro.FUNITNAME;
                sub.FSRCMODEL = pro.FSRCMODEL;
                sub.FORDERUNIT = pro.FSRCUNIT;
                sub.FMODEL = pro.FMODEL;
                sub.FSRCCODE = pro.FSRCCODE;
            }

            listCG = listCG.OrderBy(x => x.GG).ToList().OrderBy(x => x.GG).ToList();

            gridControl采购订单明细.DataSource = listCG;



            if (bzf)
            {
                simpleButton3.Visible = false;
                simpleButton1.Visible = false;
                simpleButton6.Visible = false;
                simpleButton5.Visible = false;
                simpleButton2.Visible = false;

                btnZF.Visible = true;
                simpleButton7.Location = simpleButton1.Location;
                simpleButton4.Location = simpleButton6.Location;
            }
            else
            {
                simpleButton3.Visible = true;
                simpleButton1.Visible = true;
                simpleButton6.Visible = true;
                simpleButton5.Visible = true;
                simpleButton2.Visible = true;
                btnZF.Visible = false;


            }
            onCalcWeightTotal();
            initComboBox();
        }

        /// <summary>
        /// 设置政策选择列表
        /// </summary>
        private void SetPolicyItem()
        {
            var policyList = _service.Select_List();
            //把政策列表放入控件TAG属性中，供后续调用
            cmbPromotionPolicy.Tag = policyList;

            var selectItem = policyList.SingleOrDefault(p => p.Id == ICPOBILLMODELm.LH_PROMOTIONPOLICYID);

            foreach (var item in policyList)
            {
                cmbPromotionPolicy.Properties.Items.Add(item);
            }

            if (selectItem != null)
            {
                cmbPromotionPolicy.SelectedItem = selectItem;
            }
        }

        void setBrandCombox()
        {
            var list = _service.GetBrandList(Global.LoginUser);

            list = list.Where(p => p.FNAME.Contains("箭牌") || p.FNAME.Contains("法恩")).ToArray();
            string brandid = IniHelper.ReadString(Global.IniUrl, "CONFIG", "FBRANDID", "");
            comBrand.Properties.Items.AddRange(list);
            comBrand.SelectedIndex = 0;
            var selectBrand = list.SingleOrDefault(p => p.FID == brandid);

            if (selectBrand != null)
            {
                comBrand.SelectedItem = selectBrand;
            }
        }

        void setOrderTypeCombox()
        {
            model.LH_ORDERTYPE = model.LH_ORDERTYPE ?? "";
            var list = _service.GetDics("LH_ORDER_TYPE", "", false);
            var selectItem = list.SingleOrDefault(p => p.FNAME == model.LH_ORDERTYPE || p.FID == model.LH_ORDERTYPE);
            cmbOrderType.Properties.Items.AddRange(list);


            if (selectItem != null)
            {
                cmbOrderType.SelectedItem = selectItem;
            }
        }

        void setChannelCombox()
        {
            var list = _service.GetDics("LH_PROD_CHANNEL", "", false);
            cmbSaleChannel.Properties.Items.AddRange(list);
            cmbSaleChannel.SelectedIndex = 0;

            var selectItem = string.IsNullOrEmpty(model.LH_SALESCHANNEL)
                ? list.SingleOrDefault(p => p.FNAME.Equals("零售"))
                : list.SingleOrDefault(p => p.FID.Equals(model.LH_SALESCHANNEL));
             

            if (!string.IsNullOrEmpty(model.LH_SALESCHANNEL))
            {
                selectItem = list.SingleOrDefault(p => p.FNAME == model.LH_SALESCHANNEL || p.FID == model.LH_SALESCHANNEL);
            }

            cmbSaleChannel.SelectedItem = selectItem;
        }

        void setBusinessCombox()
        {
            model.LH_BUTYPE = model.LH_BUTYPE ?? "";
            var list = _service.GetDics("LH_BU_TYPE", "", false);
            var selectItem = list.SingleOrDefault(p => p.FID == model.LH_BUTYPE || p.FNAME == model.LH_BUTYPE);
            cmbBusinessType.Properties.Items.AddRange(list);
            cmbBusinessType.SelectedIndex = 0;
            if (selectItem != null)
            {
                cmbBusinessType.SelectedItem = selectItem;
            }

        }

        void setcmbDeductionMethodCombox()
        {

            var list = _service.GetDics("LH_ADV_MONEY_TYPE", "", false);
            var selectItem = list.SingleOrDefault(p => p.FNAME == "货款");
            cmbDeductionMethod.Properties.Items.AddRange(list);
            cmbDeductionMethod.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(model.LH_ADVERTINGMONEYTYPE))
            {
                selectItem = list.SingleOrDefault(p => p.FID == model.LH_ADVERTINGMONEYTYPE);
            }
            cmbDeductionMethod.SelectedItem = selectItem;
        }

        void setProdLine()
        {
            var list = _service.GetDics("LH_PROD_LINE", "", false);
            var selectItem = list.SingleOrDefault(p => p.FNAME == "卫浴");
            cboLH_ORDERPRODLINE.Properties.Items.AddRange(list);
            cboLH_ORDERPRODLINE.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(model.LH_ORDERPRODLINE))
            {
                selectItem = list.SingleOrDefault(p => p.FNAME == model.LH_ORDERPRODLINE || p.FID == model.LH_ORDERPRODLINE);
            }

            cboLH_ORDERPRODLINE.SelectedItem = selectItem;
        }


        private void initComboBox()
        {
            //初始化品牌列表
            setBrandCombox();
            //初始化订单类型
            setOrderTypeCombox();
            //初始化渠道
            setChannelCombox();
            //初始化业务类型
            setBusinessCombox();
            //政策下接列表
            SetPolicyItem();
            //初始化扣款方式
            setcmbDeductionMethodCombox();
            //初始化产品线
            setProdLine();
        }
        #endregion

        #region ■------------------ 运行日志

        private void LogError(Exception ex)
        {
            LogHelper.WriteLog(typeof(FrmPleasePurchasePlan), ex);
        }

        private void LogError(string msg)
        {
            LogHelper.WriteLog(typeof(FrmPleasePurchasePlan), msg);
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



        #endregion

        #region ■------------------ 数据加载

        private void bgw加载数据_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bgw加载数据_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

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




        private void onSearch()
        {

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
        }

        private void Frm_SaveAfter(object sender, EventArgs e)
        {
            onSearch();
        }

        List<V_ICPOBILLENTRYMODEL> listCG = new List<V_ICPOBILLENTRYMODEL>();

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            //var cblist = this.Controls.AsQueryable().Any(p =>
            //    p is DevExpress.XtraEditors.SearchControl || p is DevExpress.XtraEditors.ComboBoxEdit).ToList();

            listCG.Clear();
            listCG = listCG.OrderBy(x => x.GG).ToList().OrderBy(x => x.GG).ToList();
            gridControl采购订单明细.DataSource = listCG;
            gridControl采购订单明细.RefreshDataSource();
            onCalcWeightTotal();
        }
        List<V_ICPOBILLENTRYMODEL> vList = new List<V_ICPOBILLENTRYMODEL>();
        private void simpleButton5_Click(object sender, EventArgs e)
        {

            vList = new List<V_ICPOBILLENTRYMODEL>();
            int[] rownumber = this.gridView发货计划明细.GetSelectedRows();//获取选中行号；

            foreach (var i in rownumber)
            {
                vList.Add(this.gridView发货计划明细.GetRow(i) as V_ICPOBILLENTRYMODEL);
            }

            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync();
            }
            SetcmbPromotionPolicyReadOnly();
        }


        private void txt经营场所_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            string brand = "";
            if (comBrand.SelectedItem != null)
            {
                TB_BrandModel model = comBrand.SelectedItem as TB_BrandModel;
                if (model != null)
                    brand = model.FID;
            }

            FrmQueryClientAccount frm = new FrmQueryClientAccount(brand);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txt厂家账户.Text = frm.SelectAccount;
                txt厂家账户.Tag = frm.SelectID;
                txtFName.Text = frm.SelectName;
            }
        }
        public void IniValue()
        {
            search价格策略.Text = "销售-样板收费";
            search价格策略.Tag = "销售-样板收费";

            searchDic105.Text = "协议价";
            searchDic105.Tag = "协议价";
        }
        /// <summary>
        /// 保存校验
        /// </summary>
        /// <returns></returns>
        bool SaveCheck()
        {
            bool TF = true;
            var list = gridControl采购订单明细.DataSource as IEnumerable<object>;
            if (list == null || list.Count() == 0)
            {
                System.Windows.Forms.MessageBox.Show("明细记录数不可为空！");
                return TF;
            }
            TB_BrandModel bmodel = comBrand.SelectedItem as TB_BrandModel;
            if (bmodel != null)
            {
                IniHelper.WriteString(Global.IniUrl, "CONFIG", "FBRANDID", bmodel.FID);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("品牌不可为空！");
                return TF;
            }
            if (txt厂家账户.Tag == null)
            {
                System.Windows.Forms.MessageBox.Show("厂家账户不可为空！");
                return TF;
            }
            if ((cmbOrderType.SelectedItem as SYS_SUBDICSMODEL) == null)
            {
                System.Windows.Forms.MessageBox.Show("订单类型不能为空！");
                return TF;
            }
            if ((cmbSaleChannel.SelectedItem as SYS_SUBDICSMODEL) == null)
            {
                System.Windows.Forms.MessageBox.Show("销售渠道不能为空！");
                return TF;
            }
            if ((cmbSaleChannel.SelectedItem as SYS_SUBDICSMODEL) == null)
            {
                System.Windows.Forms.MessageBox.Show("产品线不能为空！");
                return TF;
            }
            if (string.IsNullOrWhiteSpace(dateDHRQ.Text))
            {
                System.Windows.Forms.MessageBox.Show("期望到货日期不能为空！");
                return TF;
            }
            //if (!(cmbPromotionPolicy.SelectedItem is LH_Policy))
            //{
            //    System.Windows.Forms.MessageBox.Show("促销政策头ID不能为空！");
            //    return TF;
            //}
            if ((cmbDeductionMethod.SelectedItem as SYS_SUBDICSMODEL) == null)
            {
                System.Windows.Forms.MessageBox.Show("扣款方式不能为空！");
                return TF;
            }
            if ((cmbBusinessType.SelectedItem as SYS_SUBDICSMODEL) == null)
            {
                System.Windows.Forms.MessageBox.Show("业务类型不能为空！");
                return TF;
            }
            return false;
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            #region 1、保存前检验
            if (SaveCheck()) return;
            #endregion

            #region 2、需要保存的数据定义
            List<ICPOBILLENTRYMODEL> listSub = new List<ICPOBILLENTRYMODEL>();
            List<V_ICPOBILLENTRYMODEL> list = gridControl采购订单明细.DataSource as List<V_ICPOBILLENTRYMODEL>;
            ICPOBILLMODEL tBill = _service.GetSingleOrder(model.FID) ?? new ICPOBILLMODEL();
            // 查询出来的没有FID 
            #endregion

            #region 3、形成主表数据 

            ///经营场所
            var mcu = labMCU.Tag as TB_PREMISE_GRIDVIEW_DTO;
            if (mcu != null)
            {
                tBill.FPREMISEID = mcu.FID;
            }

            tBill.FTRANSTYPE = "0";
            tBill.FBRANDID = (comBrand.SelectedItem as TB_BrandModel).FID;
            tBill.FCLIENTID = txt厂家账户.Tag.ToStr();
            tBill.FDATE = dateDatetime.DateTime;
            tBill.FBILLNO = txtBillNO.Text;
            tBill.FBILLERNAME = txtCreater.Text;
            tBill.FBILLER = txtCreater.Tag.ToStr();
            tBill.FSTATE = 1;//草稿
            tBill.Fnote = txtRemarks.Text;
            tBill.FprojectNO = txtProjectNo.Text;
            if (searchDic105.Tag != null)
            {
                tBill.FPOtype = searchDic105.Tag.ToString();
            }
            if (cmbPromotionPolicy.SelectedItem is LH_Policy)
            {
                tBill.Fpricepolicy = (cmbPromotionPolicy.SelectedItem as LH_Policy).Id;
            }
            //新加 
            tBill.LH_ORDERTYPE = (cmbOrderType.SelectedItem as SYS_SUBDICSMODEL).FID;
            tBill.LH_SALESCHANNEL = (cmbSaleChannel.SelectedItem as SYS_SUBDICSMODEL).FID;
            tBill.LH_ORDERPRODLINE = (cboLH_ORDERPRODLINE.SelectedItem as SYS_SUBDICSMODEL).FID;
            tBill.LH_EXPECTEDARRIVEDDATE = dateDHRQ.DateTime;
            tBill.LH_PROMOTIONPOLICYID = (cmbPromotionPolicy.SelectedItem is LH_Policy)?(cmbPromotionPolicy.SelectedItem as LH_Policy).Id:null;
            tBill.LH_OUTBOUNDORDER = txtLH_OUTBOUNDORDER.Text;
            tBill.LH_ADVERTINGMONEYTYPE = (cmbDeductionMethod.SelectedItem as SYS_SUBDICSMODEL).FID;
            tBill.LH_BUTYPE = (cmbBusinessType.SelectedItem as SYS_SUBDICSMODEL).FID;
            #endregion

            #region 4、形成子表数据 
            foreach (var sub in list)
            {

                sub.FENTRYID = listSub.Count + 1;
                ICPOBILLENTRYMODEL sub0 = new ICPOBILLENTRYMODEL();
                sub0.FID = sub.FID;
                sub0.FICPOBILLID = tBill.FID;
                sub0.FADVQTY = sub.FADVQTY;
                sub0.FBATCHNO = sub.FBATCHNO;
                sub0.FCOLORNO = sub.FCOLORNO;
                sub0.FENTRYID = sub.FENTRYID;
                sub0.FNEEDDATE = sub.FNEEDDATE;
                sub0.FPLANID = sub.FPLANID ?? "0";
                sub0.FPRICE = sub.FPRICE;
                sub0.FREMARK = sub.FREMARK;
                sub0.FSRCQTY = sub.FSRCQTY;
                sub0.FSRCCOST = sub0.FPRICE * sub0.FSRCQTY;
                //后面添加的字段
                sub0.FITEMID = sub.FSRCCODE;
                sub0.FSRCCODE = sub.FSRCCODE;
                sub0.FSRCNAME = sub.FSRCNAME;
                sub0.FSRCMODEL = sub.FSRCMODEL;
                sub0.Flevel = sub.Flevel;
                sub0.FstockNO = sub.FstockNO;
                sub0.FCOLORNO = sub.FCOLORNO;
                sub0.FcontractNO = sub.FcontractNO;
                sub0.Funit = sub.FORDERUNIT;
                sub0.FAUDQTY = sub.FAUDQTY;
                sub0.FPRICE = sub.FPRICE;
                sub0.Famount = sub.Famount;
                sub0.FREMARK = sub.FREMARK;
                sub0.FERR_MESSAGE = sub.FERR_MESSAGE;
                sub0.FNEEDDATE = DateTime.Now;
                sub0.FSRCQTY = sub.FSRCQTY;
                //
                sub0.LH_DCTPOLICYITEMID = sub.LH_DCTPOLICYITEMID;
                sub0.MINIMUMQUANTITY = sub.MINIMUMQUANTITY;
                sub0.CAPPINGQUANTITY = sub.CAPPINGQUANTITY;
                sub0.DISCOUNTRATE = sub.DISCOUNTRATE;
                sub0.LH_DCTPOLICYPRODNAME = sub.FPRODUCTNAME;
                sub0.LH_DCTPOLICYROWTYPE = sub.LH_DCTPOLICYROWTYPE;


                listSub.Add(sub0);
            }
            #endregion

            #region 5、开始保存
            try
            {

                string sResult = _service.SaveICPOBILL(tBill, listSub.ToArray());

                System.Windows.Forms.MessageBox.Show(sResult);
                if (this.SaveAfter != null)
                {
                    SaveAfter(null, null);
                }
                this.Close();
            }
            catch (Exception ee)
            {
                System.Windows.Forms.MessageBox.Show(ee.ToString());
            }
            #endregion
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            string brand = "";
            if (comBrand.SelectedItem != null)
            {
                TB_BrandModel model = comBrand.SelectedItem as TB_BrandModel;
                if (model != null)
                    brand = model.FID;
            }
            if (string.IsNullOrEmpty(brand))
            {
                System.Windows.Forms.MessageBox.Show("请选择品牌！");
                return;
            }
            var list11 = this.gridView发货计划明细.DataSource as List<V_ICPOBILLENTRYMODEL>;
            List<string> lEntryIDs = new List<string>();
            if (list11 != null)
            {
                foreach (var sub in list11)
                {
                    if (!string.IsNullOrEmpty(sub.ICPRBILLENTRYIDS))
                    {
                        string[] arr = sub.ICPRBILLENTRYIDS.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var subbb in arr)
                        {
                            lEntryIDs.Add(subbb);
                        }
                    }

                    if (!string.IsNullOrEmpty(sub.FPLANID))
                    {
                        lEntryIDs.Add(sub.FPLANID);
                    }
                }
            }

            try
            {
                FrmMainB MainForm = (FrmMainB)this.Parent.Parent;
                FrmPurchasePlanImport fImport = new FrmPurchasePlanImport(brand, lEntryIDs);
                fImport.showAfter += FImport_showAfter;
                MainForm.OpenChildForm(fImport);
            }
            catch
            { }
        }


        public void FPrice_A()
        {

            var list = gridControl采购订单明细.DataSource as List<V_ICPOBILLENTRYMODEL>;

            foreach (var sub in list)
            {
                ProductViewModel pro = _service.getProductView(sub.FITEMID);
                if (pro == null) continue;
                sub.FPRICE = pro.FPRICE_A;
            }

            gridControl采购订单明细.DataSource = list;
            gridControl采购订单明细.RefreshDataSource();
            onCalcWeightTotal();
        }

        private void FImport_showAfter(List<V_ICPOBILLENTRYMODEL> list)
        {
            listCG.AddRange(list.ToArray());
            foreach (var sub in listCG)
            {
                sub.Flevel = "1";

            }
            string strRemarks = "";

            foreach (var sub in listCG)
            {
                ProductViewModel pro = _service.getProductView(sub.FITEMID);
                if (pro == null) continue;
                sub.Funit = pro.FUNITNAME;
                sub.FSRCMODEL = pro.FSRCMODEL;
                sub.FORDERUNIT = pro.FSRCUNIT;
                sub.FMODEL = pro.FMODEL;
                sub.FSRCMODEL = pro.FSRCMODEL;
                sub.FSRCCODE = pro.FSRCCODE;
                sub.FPRICE = pro.FPRICE;

                if (strRemarks == "") strRemarks = sub.FREMARK;

            }

            if (strRemarks != "") txtRemarks.Text += strRemarks + "/";

            listCG = listCG.OrderBy(x => x.GG).ToList().ToList();
            gridControl采购订单明细.DataSource = listCG;


            gridControl采购订单明细.RefreshDataSource();

            //  .Text = "";
            cal();
            onCalcWeightTotal();
        }

        private void itemButton厂家代码_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridView发货计划明细.FocusedRowHandle != -1)
            {
                var list = gridControl采购订单明细.DataSource as List<V_ICPOBILLENTRYMODEL>;
                var row = list[gridView发货计划明细.GetDataSourceRowIndex(gridView发货计划明细.FocusedRowHandle)];

                FrmQuerySrc frm = new FrmQuerySrc(row.FITEMID);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //row.FSRCID = frm.SelectData.FID;


                    row.FSRCNAME = frm.SelectData.FSRCNAME;
                    row.FSRCCODE = frm.SelectData.FSRCCODE;
                    row.FSRCMODEL = frm.SelectData.FSRCMODEL;
                    //row.FSRCUNIT = frm.SelectData.FUNIT;

                    //row.FHNAMOUNT = 0;
                    /*
                   var askqty = row.FASKQTY;

                   if (frm.SelectData.FRATE != 0)
                   {
                       row.FCOMMITQTY = Math.Round((row.FCOMMITQTY * row.FRATE) / frm.SelectData.FRATE, 2);

                   }
                   */
                    // row.FWEIGHT = row.FCOMMITQTY * frm.SelectData.FWEIGHT;
                    //  row.FRATE = frm.SelectData.FRATE;
                    //  row.FLEFTAMOUNT = askqty;

                    gridView发货计划明细.ActiveEditor.EditValue = frm.SelectData.FSRCCODE;
                    list = list.OrderBy(x => x.GG).ToList().OrderBy(x => x.GG).ToList();
                    gridControl采购订单明细.DataSource = list;
                    gridControl采购订单明细.RefreshDataSource();

                    onCalcWeightTotal();
                }
            }
        }
        /// <summary>
        /// 金额合计
        /// </summary>
        private void onCalcWeightTotal()
        {

            var weightTotal = decimal.Zero;
            var list = gridControl采购订单明细.DataSource as List<V_ICPOBILLENTRYMODEL>;
            foreach (var model in list)
            {
                weightTotal += model.Famount.HasValue?model.Famount.Value:0;
            }

            labAccount.Text = "金额合计：" + Math.Ceiling(weightTotal).ToStr();
        }

        private void itemButton商品代码_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmNewQueryProduct frm = new FrmNewQueryProduct();
            frm.itemid = (cmbPromotionPolicy.SelectedItem as LH_Policy).Id;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var list = gridControl采购订单明细.DataSource as List<V_ICPOBILLENTRYMODEL>;
                var row = list[gridView发货计划明细.GetDataSourceRowIndex(gridView发货计划明细.FocusedRowHandle)];

                row.FITEMID = frm.SelectData.ITEMID;
                row.FPRODUCTNAME = frm.SelectData.PRODNAME;
                row.FPRODUCTTYPE = frm.SelectData.LHPRODTYPE;
                row.FPRODUCTCODE = frm.SelectData.PRODCODE;
                row.FSRCQTY = 0;

                row.Famount = row.FPRICE * row.FSRCQTY;

                row.FSRCNAME = "";
                row.FSRCMODEL = frm.SelectData.LHPRODTYPE + "||" + frm.SelectData.PRODSTANDARD + "||" + frm.SelectData.PRODMODEL;
                row.FSRCCODE = frm.SelectData.PRODCODE;

                row.MINIMUMQUANTITY = frm.SelectData.MINIMUMQUANTITY;
                row.CAPPINGQUANTITY = frm.SelectData.CAPPINGQUANTITY;
                row.DISCOUNTRATE = frm.SelectData.DISCOUNTRATE;
                if (frm.SelectData.SPECIALOFFER != null) row.FPRICE = frm.SelectData.SPECIALOFFER.Value;
                row.LH_DCTPOLICYITEMID = frm.SelectData.HEADID;

                list[gridView发货计划明细.GetDataSourceRowIndex(gridView发货计划明细.FocusedRowHandle)] = row;
                gridView发货计划明细.ActiveEditor.EditValue = frm.SelectData.PRODNAME;

                list = list.OrderBy(x => x.GG).ToList().OrderBy(x => x.GG).ToList();
                gridControl采购订单明细.DataSource = list;
                gridControl采购订单明细.RefreshDataSource();

                onCalcWeightTotal();
            }

        }

        private V_ICPOBILLENTRYMODEL PolicyProductToICPOBillEntry(v_lhproducts_policyModel data)
        {
            V_ICPOBILLENTRYMODEL result = new V_ICPOBILLENTRYMODEL();
            result.FITEMID = data.PRODCODE;
            result.FPRODUCTNAME = data.PRODNAME;
            result.FPRODUCTTYPE = data.LHPRODTYPE;
            result.FPRODUCTCODE = data.PRODCODE;
            result.FSRCQTY = 0;

            result.Famount = result.FPRICE * result.FSRCQTY;

            result.FSRCNAME = "";
            result.FSRCMODEL = data.LHPRODTYPE + "||" + data.PRODSTANDARD + "||" + data.PRODMODEL;
            result.FSRCCODE = data.PRODCODE;

            result.MINIMUMQUANTITY = data.MINIMUMQUANTITY;
            result.CAPPINGQUANTITY = data.CAPPINGQUANTITY;
            result.DISCOUNTRATE = data.DISCOUNTRATE;

            result.LH_DCTPOLICYROWTYPE = data.POLICYITEMTYPE;
            result.LH_DCTPOLICYPRODNAME = data.PRODNAME;

            if (data.SPECIALOFFER != null) result.FPRICE = data.SPECIALOFFER.Value;
            result.LH_DCTPOLICYITEMID = data.ITEMID;

            return result;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            var header = new ICPOBILL_PolicyDTO();


            header.Account = txt厂家账户.Text;
            header.BrandName = comBrand.SelectedItem is TB_BrandModel
                ? ((TB_BrandModel)comBrand.SelectedItem).FNAME
                : "";
            header.OrderType = cmbOrderType.SelectedItem is SYS_SUBDICSMODEL
                ? ((SYS_SUBDICSMODEL)cmbOrderType.SelectedItem).FNUMBER
                : "";

            header.OrderSubType = cmbBusinessType.SelectedItem is SYS_SUBDICSMODEL
                ? ((SYS_SUBDICSMODEL)cmbBusinessType.SelectedItem).FNUMBER
                : "";
            header.Channel = cmbSaleChannel.SelectedItem is SYS_SUBDICSMODEL
                ? ((SYS_SUBDICSMODEL)cmbSaleChannel.SelectedItem).FNUMBER
                : "";
            header.HeadID = cmbPromotionPolicy.SelectedItem is LH_Policy
                ? ((LH_Policy)cmbPromotionPolicy.SelectedItem).Id : "";

            FrmNewQueryProduct queryFrm = new FrmNewQueryProduct();
            queryFrm.header = header;
            if (queryFrm.ShowDialog() == DialogResult.OK)
            {
                var selectRows = queryFrm.SelectRows;

                List<V_ICPOBILLENTRYMODEL> datasource = gridView发货计划明细.DataSource as List<V_ICPOBILLENTRYMODEL>;
                if (datasource == null)
                {
                    datasource = new List<V_ICPOBILLENTRYMODEL>();

                }

                selectRows = selectRows.Where(r => !datasource.Exists(p => p.FSRCCODE == r.PRODCODE)).ToList();
                foreach (var row in selectRows)
                {
                    datasource.Add(PolicyProductToICPOBillEntry(row));
                }

                if (gridControl采购订单明细 != null) gridControl采购订单明细.DataSource = datasource;
                gridView发货计划明细.RefreshData();
            }

            queryFrm.Dispose();

        }
        void SetcmbPromotionPolicyReadOnly()
        {
            gridView发货计划明细.PostEditor();
            gridView发货计划明细.UpdateCurrentRow();
            cmbPromotionPolicy.ReadOnly = gridView发货计划明细.RowCount != 0;
        }

        private void repositoryItemTextEdit9_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridView发货计划明细_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (gridView发货计划明细.FocusedRowHandle != -1 && (e.Column.FieldName == "FSRCQTY" || e.Column.FieldName == "FPRICE"))
            {
                var list = gridControl采购订单明细.DataSource as List<V_ICPOBILLENTRYMODEL>;
                var row = list[gridView发货计划明细.GetDataSourceRowIndex(gridView发货计划明细.FocusedRowHandle)];

                // gridView发货计划明细.SetRowCellValue(gridView发货计划明细.FocusedRowHandle, "", "");
                row.Famount = row.FSRCQTY * row.FPRICE;
                // row.FSRCCOST = row.Famount;
                onCalcWeightTotal();
            }

        }


        public void cal()
        {
            var list = gridControl采购订单明细.DataSource as List<V_ICPOBILLENTRYMODEL>;

            foreach (var row in list)
            {
                // gridView发货计划明细.SetRowCellValue(gridView发货计划明细.FocusedRowHandle, "", "");
                row.Famount = row.FSRCQTY * row.FPRICE;
            }
        }


        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (model != null)
            {
                FrmQueryDictionary frmdiction = new FrmQueryDictionary("116");

                if (frmdiction.ShowDialog() == DialogResult.OK)
                {
                    search价格策略.Text = frmdiction.SelectName;
                    search价格策略.Tag = frmdiction.SelectName;
                }
            }

        }

        private void searchControl1_Properties_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmQueryDictionary frmDic = new FrmQueryDictionary("115");
            if (frmDic.ShowDialog() == DialogResult.OK)
            {
                searchDic105.Text = frmDic.SelectName;
                searchDic105.Tag = frmDic.SelectName;
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            frm库存查询 frmQuery = new frm库存查询();
            frmQuery.FormBorderStyle = FormBorderStyle.FixedSingle;
            frmQuery.Width = 900;
            frmQuery.Height = 300;
            frmQuery.ShowIcon = false;

            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width - frmQuery.Width;
            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - frmQuery.Height;
            Point p = new Point(x, y);
            frmQuery.PointToScreen(p);
            frmQuery.Location = p;
            frmQuery.TopMost = true;
            frmQuery.Show();


            // frmQuery.StartPosition = FormStartPosition.CenterParent;
            // frmQuery.ShowDialog();
        }

        private void search价格策略_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gridView发货计划明细_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void btnZF_Click(object sender, EventArgs e)
        {

            var list = new List<V_ICPOBILLENTRYMODEL>();
            int[] rownumber = this.gridView发货计划明细.GetSelectedRows();//获取选中行号；

            foreach (var i in rownumber)
            {
                list.Add(this.gridView发货计划明细.GetRow(i) as V_ICPOBILLENTRYMODEL);
            }
            foreach (var sub in list)
            {
                if (_service.ZFICPOBILL(sub.FID).Contains("成功"))
                    listCG.Remove(sub);

            }
            listCG = listCG.OrderBy(x => x.GG).ToList().OrderBy(x => x.GG).ToList();
            gridControl采购订单明细.DataSource = listCG;
            gridControl采购订单明细.RefreshDataSource();
            onCalcWeightTotal();

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var sub in vList)
            {
                if (bEdit)
                {
                    _service.ZFICPRBILLEntry(sub.FID);
                    //if (_service.ZFICPRBILLEntry(sub.FID) == "1")
                    {
                        listCG.Remove(sub);
                    }
                }
                else
                {
                    listCG.Remove(sub);
                }
            }


        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listCG = listCG.OrderBy(x => x.GG).ToList().OrderBy(x => x.GG).ToList();
            gridControl采购订单明细.DataSource = listCG;
            gridControl采购订单明细.RefreshDataSource();
            cal();
            onCalcWeightTotal();

            vList = new List<V_ICPOBILLENTRYMODEL>();
            int[] rownumber = this.gridView发货计划明细.GetSelectedRows();//获取选中行号；
            this.gridView发货计划明细.ClearSelection();


        }
        string strDB = "";
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            simpleButton8.Enabled = false;
            simpleButton8.Text = "厂家库存检测中";

            string tAccount = txt厂家账户.Text;
            strDB = "100";
            if (tAccount.Contains("FDK"))
            {
                strDB = "10";
            }
            else if (tAccount.Contains("MN"))
            {
                strDB = "2";
            }
            else if (tAccount.Contains("GW"))
            {
                strDB = "3";
            }

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<V_ICPOBILLENTRYMODEL> listTemp = gridControl采购订单明细.DataSource as List<V_ICPOBILLENTRYMODEL>;

            foreach (var sub in listTemp)
            {
                //FSRCQTY

                MApiModel.api2.Rootobject getapi2 = new MApiModel.api2.Rootobject();
                getapi2.cpgg = sub.GG;
                getapi2.cpxh = sub.XH;
                getapi2.pageSize = 200;
                getapi2.pageIndex = 1;
                var list1 = _service.GetStockListMN_2(getapi2, int.Parse(strDB));

                if (list1.resultInfo.Length == 0)
                {
                    sub.cjkcs = 0;
                }
                else
                {
                    int iCount = 0;
                    foreach (var sss in list1.resultInfo)
                    {

                        iCount += sss.bysl;
                    }


                    sub.cjkcs = iCount;
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            simpleButton8.Enabled = true;
            simpleButton8.Text = "厂家库存检查";
            // gridControl采购订单明细.DataSource = listTemp;
            gridControl采购订单明细.RefreshDataSource();


        }

        private void gridView发货计划明细_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            DevExpress.Utils.AppearanceDefault appRed = new DevExpress.Utils.AppearanceDefault
            (Color.Black, Color.Red, Color.Empty, Color.Red, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            DevExpress.Utils.AppearanceDefault appYellow = new DevExpress.Utils.AppearanceDefault
                (Color.Black, Color.Yellow, Color.Empty, Color.Yellow, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            DevExpress.Utils.AppearanceDefault appGreen = new DevExpress.Utils.AppearanceDefault
                (Color.Black, Color.Green, Color.Empty, Color.Green, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            if (e.Column.FieldName == "cjkcs")//指定列
            {
                try
                {


                    /*
                      case "10":
                            strTempAccount += "FDK";
                            break;
                        case "2":
                            strTempAccount += "MN";
                            break;
                        case "3":
                            strTempAccount += "GW";
                            break;
                     * */


                    string strDDS = gridView发货计划明细.GetRowCellDisplayText(e.RowHandle, "FSRCQTY").ToString().Trim().Replace(",", "");

                    string strCJS = gridView发货计划明细.GetRowCellDisplayText(e.RowHandle, "cjkcs").ToString().Trim().Replace(",", "");

                    int iDDs = int.Parse(strDDS);
                    int iCJS = int.Parse(strCJS);

                    if (iCJS > 0)
                    {
                        if (iCJS < iDDs)
                        {
                            DevExpress.Utils.AppearanceHelper.Apply(e.Appearance, appRed);
                        }
                        else
                        {
                            // DevExpress.Utils.AppearanceHelper.Apply(e.Appearance, appGreen);
                        }
                    }
                    else
                    {
                        DevExpress.Utils.AppearanceHelper.Apply(e.Appearance, appRed);
                    }
                }
                catch
                { }


            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            FPrice_A();
        }

        /// <summary>
        /// 所属公司或订单类型改变时过滤可选政策
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////1、选择订单类型为常规订单的，如果用户要选择促销政策头ID的话则需要同时判断：订单所属公司（事业部）、厂家账号（经销商账号）、销售渠道、业务类型、五项头字段信息来取促销政策头ID信息
            //cmbPromotionPolicy.Properties.Items.Clear();
            //cmbPromotionPolicy.SelectedItem = null;
            //var policyList = cmbPromotionPolicy.Tag as LH_Policy[];
            ////订单所属公司（事业部）
            //var brandName = comBrand.SelectedItem.ToString();
            ////订单类型
            //var orderType = cmbOrderType.SelectedItem as SYS_SUBDICSMODEL;
            //if (policyList != null)
            //{
            //    var selectList = policyList.Where(p =>
            //            p.DeptName.Contains(brandName)&& p.OrderType.Contains(orderType==null?"":orderType.FNUMBER??""))
            //        .Select(p => new SelectModel() {FID = p.Id, FName = $"{p.PolicyName}-{p.Id}"});

            //    cmbPromotionPolicy.Properties.Items.AddRange(selectList.ToArray());
            //}
        }

        private void cmbPromotionPolicy_MouseClick(object sender, MouseEventArgs e)
        {
            var header = new ICPOBILL_PolicyDTO();


            header.Account = txt厂家账户.Text;
            header.BrandName = comBrand.SelectedItem is TB_BrandModel
                ? ((TB_BrandModel)comBrand.SelectedItem).FNAME
                : "";

            header.OrderType = cmbOrderType.SelectedItem is SYS_SUBDICSMODEL
                ? ((SYS_SUBDICSMODEL)cmbOrderType.SelectedItem).FNUMBER
                : "";

            header.OrderSubType = cmbBusinessType.SelectedItem is SYS_SUBDICSMODEL
                ? ((SYS_SUBDICSMODEL)cmbBusinessType.SelectedItem).FNUMBER
                : "";

            header.Channel = cmbSaleChannel.SelectedItem is SYS_SUBDICSMODEL
                ? ((SYS_SUBDICSMODEL)cmbSaleChannel.SelectedItem).FNUMBER
                : "";


            try
            {
                var policies = _service.GetPolicies(header);
                cmbPromotionPolicy.Text = "";
                cmbPromotionPolicy.Properties.Items.Clear();
                cmbPromotionPolicy.Properties.Items.AddRange(policies);

                cmbPromotionPolicy.ShowPopup();
            }
            catch (Exception exception)
            {
                MsgHelper.ShowInformation(exception.Message);
            }

        }

        private void gridView发货计划明细_RowCountChanged(object sender, EventArgs e)
        {
            cmbPromotionPolicy.ReadOnly = gridView发货计划明细.RowCount != 0;
        }

        private void searchControl1_Properties_ButtonClick_2(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //经营场所选择
            FrmQueryMCU frm=new FrmQueryMCU();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var result = frm.Result;
                this.txtMcu.Text = result.FNAME;
                labMCU.Tag = result;
                frm.Dispose();
            }

        }

        private void cmbPromotionPolicy_SelectedIndexChanged(object sender, EventArgs e)
        {
            var policy = cmbPromotionPolicy.SelectedItem as LH_Policy;
            if (policy != null)
            {
                txtPolicyID.Text = policy.Id;
            }
        }
    }
}
