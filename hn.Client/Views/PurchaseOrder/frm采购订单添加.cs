using hn.DataAccess.model;
using hn.DataAccess.model.Common;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using hn.Client.ApiService;
using hn.Common;

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
        public FrmPurchaseOrder(V_ICPOBILLMODEL pModel,bool bzf=false)
        {
            InitializeComponent();

            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);

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
                }
            }
            
            txtRemarks.Text = model.Fnote;
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

            var tmp  = _service.ICPOBILLENTRYMODEL_List(model.FID);
            foreach (var item in tmp) {
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
          

            foreach (var item in policyList)
            {
                var tmp = new SelectModel() { FID = item.Id, FName = $"{item.PolicyName}-{item.Id}" };
                cmbPromotionPolicy.Properties.Items.Add(tmp);
                if (item.Id == ICPOBILLMODELm.LH_PROMOTIONPOLICYID)
                {
                    cmbPromotionPolicy.SelectedItem = tmp;
                }
            }
        }


        private void initComboBox()
        {
            //初始化品牌列表
            var list = _service.GetBrandList(Global.LoginUser);

            list = list.Where(p => p.FNAME.Contains("箭牌") || p.FNAME.Contains("法恩")).ToArray();

            foreach (var item in list)
            {
             
                string brandid = IniHelper.ReadString(Global.IniUrl, "CONFIG", "FBRANDID", "");
            

                comBrand.Properties.Items.Add(item);               
                if (item.FID == brandid)
                {
                    comBrand.SelectedItem = item;
                }
                
            }
            model.LH_ORDERTYPE = model.LH_ORDERTYPE ?? "";
            foreach (var item in _service.GetDics("LH_ORDER_TYPE", "", true))
            {
                cmbOrderType.Properties.Items.Add(item); 
                if (item.FNAME == model.LH_ORDERTYPE ||item.FID == model.LH_ORDERTYPE)
                {
                    cmbOrderType.SelectedItem = item;
                }
            }
            model.LH_SALESCHANNEL = model.LH_SALESCHANNEL ?? "";
            foreach (var item in _service.GetDics("LH_PROD_CHANNEL", "", true))
            {
                cmbSaleChannel.Properties.Items.Add(item);
                if (item.FNAME == model.LH_SALESCHANNEL || item.FID == model.LH_SALESCHANNEL)
                {
                    cmbSaleChannel.SelectedItem = item;
                }
            }
            model.LH_BUTYPE = model.LH_BUTYPE ?? "";
            foreach (var item in _service.GetDics("LH_BU_TYPE", "", true))
            {
                cmbBusinessType.Properties.Items.Add(item);
                if (item.FNAME == model.LH_BUTYPE || item.FID == model.LH_BUTYPE)
                {
                    cmbBusinessType.SelectedItem = item;
                }
            }
            //政策下接列表
            SetPolicyItem();
            model.LH_ADVERTINGMONEYTYPE = model.LH_ADVERTINGMONEYTYPE ?? "";
            foreach (var item in _service.GetDics("LH_ADV_MONEY_TYPE", "", true))
            {
                cmbDeductionMethod.Properties.Items.Add(item);
                if (item.FNAME == model.LH_ADVERTINGMONEYTYPE || item.FID == model.LH_ADVERTINGMONEYTYPE)
                {
                    cmbDeductionMethod.SelectedItem = item;
                }
            }
            model.LH_ORDERPRODLINE = model.LH_ORDERPRODLINE ?? "";
            foreach (var item in _service.GetDics("LH_PROD_LINE", "", true))
            {
                cboLH_ORDERPRODLINE.Properties.Items.Add(item);
                if (item.FNAME == model.LH_ORDERPRODLINE || item.FID == model.LH_ORDERPRODLINE)
                {
                    cboLH_ORDERPRODLINE.SelectedItem = item;
                }
            }
            
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
            listCG.Clear();
            listCG = listCG.OrderBy(x => x.GG).ToList().OrderBy(x => x.GG).ToList();
            gridControl采购订单明细.DataSource = listCG;
            gridControl采购订单明细.RefreshDataSource();
            onCalcWeightTotal(); 
        } 
        List<V_ICPOBILLENTRYMODEL> vList = new List<V_ICPOBILLENTRYMODEL>();
        private void simpleButton5_Click(object sender, EventArgs e)
        {

            vList =  new List<V_ICPOBILLENTRYMODEL>();
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
        bool SaveCheck() {
            bool TF = true;
            var list = gridControl采购订单明细.DataSource as List<V_ICPOBILLENTRYMODEL>;
            if (list == null || list.Count == 0)
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
            if (!(cmbPromotionPolicy.SelectedItem is SelectModel))
            {
                System.Windows.Forms.MessageBox.Show("促销政策头ID不能为空！");
                return TF;
            }
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
            if (search价格策略.Tag != null)
            {
                tBill.Fpricepolicy = search价格策略.Tag.ToString();
            } 
            //新加 
            tBill.LH_ORDERTYPE = (cmbOrderType.SelectedItem as SYS_SUBDICSMODEL).FID;
            tBill.LH_SALESCHANNEL = (cmbSaleChannel.SelectedItem as SYS_SUBDICSMODEL).FID;
            tBill.LH_ORDERPRODLINE = (cboLH_ORDERPRODLINE.SelectedItem as SYS_SUBDICSMODEL).FID;
            tBill.LH_EXPECTEDARRIVEDDATE = dateDHRQ.DateTime;
            tBill.LH_PROMOTIONPOLICYID = (cmbPromotionPolicy.SelectedItem as SelectModel).FID;
            tBill.LH_OUTBOUNDORDER = txtLH_OUTBOUNDORDER.Text;
            tBill.LH_ADVERTINGMONEYTYPE = (cmbDeductionMethod.SelectedItem as SYS_SUBDICSMODEL).FID;
            tBill.LH_BUTYPE = (cmbBusinessType.SelectedItem as SYS_SUBDICSMODEL).FID;
            #endregion 

            #region 4、形成子表数据 
            foreach (var sub in list)
            {
                // 箭牌暂时不用写这个表的
                //if (sub.FPLANID == null)
                //{ 
                //    //插入一条icprentry记录 
                //    ICPRBILLENTRYMODEL tRModel = new ICPRBILLENTRYMODEL();
                //    tRModel.FID = Guid.NewGuid().ToStr();
                //    tRModel.FITEMID = sub.FITEMID;
                //    tRModel.FUNITID = sub.FUNITID ?? ""; 
                //    tRModel.FPLANID = tRModel.FID;
                //    tRModel.FASKAMOUNT = sub.Famount;
                //    tRModel.FASKQTY = sub.FAUDQTY; 
                //    tRModel.FSTOREHOUSE = sub.FstockNO;
                //    tRModel.FNEEDDATE = sub.FNEEDDATE;
                //    tRModel.FASKQTY = sub.FASKQTY;
                //    tRModel.FORDERUNITQTY = (int)sub.FSRCQTY; 
                //    // 保存
                //    sub.FPLANID = _service.Save_ICPREntry_List(tRModel);
                //}
                // 
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
                sub0.FITEMID = sub.FITEMID;
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
                weightTotal += model.Famount;
            }

            labAccount.Text = "金额合计：" + Math.Ceiling(weightTotal).ToStr();
        }

        private void itemButton商品代码_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmNewQueryProduct frm = new FrmNewQueryProduct();
            frm.itemid = (cmbPromotionPolicy.SelectedItem as SelectModel).FID;
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
                row.FSRCMODEL = frm.SelectData.LHPRODTYPE + "||"+frm.SelectData.PRODSTANDARD+ "||" + frm.SelectData.PRODMODEL; 
                row.FSRCCODE = frm.SelectData.PRODCODE;

                row.MINIMUMQUANTITY = frm.SelectData.MINIMUMQUANTITY;
                row.CAPPINGQUANTITY = frm.SelectData.CAPPINGQUANTITY;
                row.DISCOUNTRATE = frm.SelectData.DISCOUNTRATE;
                row.FPRICE = frm.SelectData.SPECIALOFFER;
                row.LH_DCTPOLICYITEMID = frm.SelectData.HEADID;

                list[gridView发货计划明细.GetDataSourceRowIndex(gridView发货计划明细.FocusedRowHandle)] = row;
                gridView发货计划明细.ActiveEditor.EditValue = frm.SelectData.PRODNAME;

                list = list.OrderBy(x => x.GG).ToList().OrderBy(x => x.GG).ToList();
                gridControl采购订单明细.DataSource = list;
                gridControl采购订单明细.RefreshDataSource();

                onCalcWeightTotal();
            }

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            var list = gridControl采购订单明细.DataSource as List<V_ICPOBILLENTRYMODEL>;
            if (list == null)
            {
                list = new List<V_ICPOBILLENTRYMODEL>();
                list.Add(new V_ICPOBILLENTRYMODEL() { FNEEDDATE = DateTime.Now, Flevel = "1" });
                gridControl采购订单明细.DataSource = list;
            }
            else
            {
                list.Add(new V_ICPOBILLENTRYMODEL() { FNEEDDATE = DateTime.Now,Flevel="1" });
            }
            gridControl采购订单明细.RefreshDataSource();
            cal();
            onCalcWeightTotal();
            SetcmbPromotionPolicyReadOnly();
        }
        void SetcmbPromotionPolicyReadOnly() {
            gridView发货计划明细.PostEditor();
            gridView发货计划明细.UpdateCurrentRow();
            cmbPromotionPolicy.ReadOnly = gridView发货计划明细.RowCount != 0;
        }
        private void repositoryItemTextEdit9_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void gridView发货计划明细_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
            if (gridView发货计划明细.FocusedRowHandle != -1 && (e.Column.FieldName == "FSRCQTY"|| e.Column.FieldName == "FPRICE"))
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
                var list1 = _service.GetStockListMN_2(getapi2,int.Parse(strDB));

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
            cmbPromotionPolicy.Properties.Items.Clear();
            cmbPromotionPolicy.SelectedItem = null;
            var policyList = cmbPromotionPolicy.Tag as LH_Policy[];
            var brandName = comBrand.SelectedItem.ToString();
            var orderType = cmbOrderType.SelectedItem as SYS_SUBDICSMODEL;
            if (policyList != null)
            {
                var selectList = policyList.Where(p =>
                        p.DeptName.Contains(brandName)&& p.OrderType.Contains(orderType==null?"":orderType.FNUMBER??""))
                    .Select(p => new SelectModel() {FID = p.Id, FName = $"{p.PolicyName}-{p.Id}"});

                cmbPromotionPolicy.Properties.Items.AddRange(selectList.ToArray());
            }
        }
    }
}
