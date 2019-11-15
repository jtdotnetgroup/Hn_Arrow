using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraTreeList;
using hn.Client.Core;
using hn.Common;
using hn.DataAccess.model;
using hn.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using hn.ArrowInterfac.ArrowLog;
using static hn.Client.FrmPleasePurchasePlan;

namespace hn.Client
{
    public partial class FrmSGPGroupCounter : Form
    {
        #region ■------------------ 字段相关
        ApiService.APIServiceClient _service;

        public V_ICSEOUTBILLMODEL IcseoutbillModel;

        public event EventHandler SaveAfter;
        #endregion

        #region ■------------------ 构造加载

        public FrmSGPGroupCounter()
        {
            InitializeComponent();
            try
            {
                numericUpDown1.Value = decimal.Parse(IniHelper.ReadString(Global.IniUrl, "CONFIG", "cph", "40"));
            }
            catch
            { }
            _service = new ApiService.APIServiceClient("BasicHttpBinding_IAPIService", Global.WcfUrl);

            initData();



            dateEdit1.DateTime = DateTime.Now.AddDays(-30);
            dateEdit2.DateTime = DateTime.Now;



        }

        private void initData()
        {

            treeList销区.OptionsBehavior.EnableFiltering = true;
            treeList销区.OptionsView.ShowAutoFilterRow = true;
            treeList销区.OptionsFilter.FilterMode = FilterMode.Smart;
            //var marketAreaList = _service.GetDics("101", "", false);

            /*
            List<SYS_SUBDICSMODEL> marketAreaList = new List<SYS_SUBDICSMODEL>();
            if (checkBox1.Checked)
            {
                marketAreaList = _service.GetDics_Area("101", "", false).ToList();
            }
            else
            {
                marketAreaList = _service.GetDics("101", "", false).ToList();
            }


          
            SYS_SUBDICSMODEL model = new SYS_SUBDICSMODEL();
            model.FID = "0";
            model.FNAME = "无匹配关系";
            marketAreaList.Add(model);




            treeList销区.DataSource = marketAreaList;
            */


            //初始化品牌列表
            var listBrand = _service.GetBrandList(Global.LoginUser);
            foreach (var item in listBrand)
            {
                cbo品牌.Properties.Items.Add(item);

                string brandid = IniHelper.ReadString(Global.IniUrl, "CONFIG", "FBRANDID", "");
                if (item.FID == brandid)
                {
                    cbo品牌.SelectedItem = item;
                }
            }

            //初始化运输方式
            var dicList1 = _service.GetDics("106", "", false);


            /*海柜
铁路
零坦
汽运
自提/*/

            List<SYS_SUBDICSMODEL> listDic = dicList1.ToList();
            foreach (var item in dicList1)
            {
                if (item.FNAME.Contains("海柜"))
                {
                    listDic[0] = item;
                }
                if (item.FNAME.Contains("铁路"))
                {
                    listDic[1] = item;

                }
                if (item.FNAME.Contains("零坦"))
                {
                    listDic[2] = item;

                }
                if (item.FNAME.Contains("汽运"))
                {
                    listDic[3] = item;

                }
                if (item.FNAME.Contains("自提"))
                {
                    listDic[4] = item;

                }
            }

            if (listDic.Count > 5) listDic = listDic.Take(5).ToList();

            foreach (var item in listDic)
            {
                cbo运输方式.Properties.Items.Add(item);
            }


            //初始化发货方式
            var dicList2 = _service.GetDics("113", "", false);
            foreach (var item in dicList2)
            {
                cbo发货方式.Properties.Items.Add(item);
            }

            cbo发货方式.SelectedIndex = 0;

            var dicList114 = _service.GetDics("114", "", false);
            if (dicList114 != null)
            {
                foreach (var item in dicList114)
                {
                    cbo运输计价.Properties.Items.Add(item);
                }
            }
            //cbo运输计价.Properties.Items.AddRange(_service.GetDics("114", "", false));

            cbo开单类型.Properties.Items.Add(new CodeValueClass("1", "普通开单"));
            cbo开单类型.Properties.Items.Add(new CodeValueClass("2", "托管库开单"));
            cbo开单类型.SelectedIndex = 0;

            //var dicList3 = _service.GetDics("105", "", true);
            //foreach (var item in dicList3)
            //{
            //    cbo计划类型.Properties.Items.Add(item);
            //}
            //cbo计划类型.SelectedIndex = 3;


            txt发货日期.DateTime = DateTime.Now;

            //加载请购计划明细列表数据
            //var list = _service.GetPurchasePlanEntryByDeliveryList("", "", "", "", "", 7);
            //foreach (var model in list)
            //{
            //    model.FCOMMITQTY = model.FORDERUNITQTY;
            //}
            //gridControl请购计划明细.DataSource = list;
            TB_EBPLModel[] lProvince = _service.GetProvince();
            cbo省.Properties.Items.AddRange(lProvince);
        }

        private void onClear()
        {

            bLoad = true;
        }

        private void setFormData()
        {
            if (IcseoutbillModel != null)
            {
                if (IcseoutbillModel.FSYNCSTATUS == 0)
                {
                    btn取消作废.Visible = false;
                    btn反作废.Visible = false;
                    btn变更.Visible = false;
                }

                txt发货计划号.Text = IcseoutbillModel.FGROUP_NO;
                foreach (var item in cbo品牌.Properties.Items)
                {
                    if (((TB_BrandModel)item).FID == IcseoutbillModel.FBRANDID)
                    {
                        cbo品牌.SelectedItem = item;
                        break;
                    }
                }
                //sr厂家账户.Text = IcseoutbillModel.FCLIENTNAME;
                //sr厂家账户.Tag = IcseoutbillModel.FCLIENTID;
                sr二级销区.Text = IcseoutbillModel.FCLASSAREA2NAME;
                sr二级销区.Tag = IcseoutbillModel.FPREMISEID;

                searchJYDW.Text = IcseoutbillModel.JYDWNAME;
                searchJYDW.Tag = IcseoutbillModel.JYDWID;

                foreach (var item in cbo运输方式.Properties.Items)
                {
                    if (((SYS_SUBDICSMODEL)item).FID == IcseoutbillModel.FTRANSID)
                    {
                        cbo运输方式.SelectedItem = item;
                        break;
                    }
                }

                txt车牌号.Text = IcseoutbillModel.FCARNUMBER;
                txt车型载重.Text = IcseoutbillModel.FLOADCAPACITY;
                txt提货人.Text = IcseoutbillModel.FDELIVERER;
                txt提货人电话.Text = IcseoutbillModel.FDELIVERERTEL;
                txt收货方地址.Text = IcseoutbillModel.FRECEIVERADDR;
                txt汇总重量.Text = IcseoutbillModel.FALLWEIGHT.ToStr();
                txt汇总体积.Text = IcseoutbillModel.FALLVOLUME.ToStr();
                txt中心仓.Text = IcseoutbillModel.FCENTER_WAREHOUSE;
                txt委托人.Text = IcseoutbillModel.FDELIVERERADDR;
                txt委托人电话.Text = IcseoutbillModel.FCLIENTELE_PHONE;
                txt采购订单.Text = IcseoutbillModel.FPURCHASE_NO;
                txt描述.Text = IcseoutbillModel.FPLANDESC;
                txt工程名称.Text = IcseoutbillModel.FPROJECTNAME;
                txt其他.Text = IcseoutbillModel.FREMARK;
                txt发货日期.DateTime = IcseoutbillModel.FDELIVERDATE;
                txt计划信息.Text = IcseoutbillModel.FPLAN_INFO;

                sr厂家发货基地.Text = IcseoutbillModel.FBASEA_NAME;
                sr厂家发货基地.Tag = IcseoutbillModel.FDELIVER_BASE_ID;

                foreach (var item in cbo发货方式.Properties.Items)
                {
                    if (((SYS_SUBDICSMODEL)item).FID == IcseoutbillModel.FDELIVERY_METHOD)
                    {
                        cbo发货方式.SelectedItem = item;
                        break;
                    }
                }

                sr承运公司.Text = IcseoutbillModel.FEXPRESSCOMPANYNAME;
                sr承运公司.Tag = IcseoutbillModel.FEXPRESSCOMPANYID;

                foreach (var item in cbo开单类型.Properties.Items)
                {
                    if (((CodeValueClass)item).value.ToDecimal() == IcseoutbillModel.FBILLING_TYPE)
                    {
                        cbo开单类型.SelectedItem = item;
                        break;
                    }
                }

                chk是否托管.Checked = IcseoutbillModel.FIS_CONSIGN == 1;

                mbProvinceid = false;
                foreach (var item in cbo省.Properties.Items)
                {
                    if (((TB_EBPLModel)item).EBPL_CODE == IcseoutbillModel.FPROVINCEID)
                    {
                        cbo省.SelectedItem = item;
                        break;
                    }
                }

                cbo市.Properties.Items.AddRange(_service.GetCity(IcseoutbillModel.FPROVINCEID));
                foreach (var item in cbo市.Properties.Items)
                {
                    TB_EBPLModel model = item as TB_EBPLModel;
                    if (model.EBPL_CODE == IcseoutbillModel.FCITYID)
                    {
                        cbo市.SelectedItem = item;
                    }
                }
                cbo区县.Properties.Items.AddRange(_service.GetCity(IcseoutbillModel.FCITYID));
                foreach (var item in cbo区县.Properties.Items)
                {
                    TB_EBPLModel model = item as TB_EBPLModel;
                    if (model.EBPL_CODE == IcseoutbillModel.FDISTRICTID)
                    {
                        cbo区县.SelectedItem = item;
                    }
                }

                mbProvinceid = true;

                txt收货人.Text = IcseoutbillModel.FCONSIGNEE;
                txt收货人电话.Text = IcseoutbillModel.FCONSIGNEE_TEL;
                txt要求到货日期.EditValue = IcseoutbillModel.FREQUEST_DELIVERY_DATE;
                txt预计发货日期.EditValue = IcseoutbillModel.FESTIMATED_DELIVERY_DATE;
                chk是否签回单.Checked = IcseoutbillModel.FIS_SIGN_BACK == 1;
                txt发货类型.Text = IcseoutbillModel.FDELIVERY_TYPE;

                foreach (var item in cbo运输计价.Properties.Items)
                {
                    if (((SYS_SUBDICSMODEL)item).FID == IcseoutbillModel.FTRANSPORT_PRICE_TYPE)
                    {
                        cbo运输计价.SelectedItem = item;
                        break;
                    }
                }

                var listTemp = new List<V_ICSEOUTBILLENTRYMODEL>(_service.GetDeliveryEntryByGroupNo(IcseoutbillModel.FGROUP_NO));

                foreach (var subT in listTemp)
                {
                    v_thdModel v = _service.getTHD(subT.thdbm);
                    subT.dw = v.dw;
                    subT.dj = v.dj;
                    subT.pz = v.cppz;
                    subT.xh = v.cpxh;
                    subT.gg = v.cpgg;
                    subT.khhm = v.khhm;
                    subT.khmc = v.khmc;
                    subT.cpdj = v.cpdj;
                    subT.pzhm = v.pzhm;
                    subT.kdrq = v.rq.ToString("yyyy-MM-dd");
                    subT.cpcm = v.cpcm;
                    subT.cpsh = v.cpsh;
                }

                gridControl组柜明细.DataSource = listTemp;

                //gridControl发货计划.DataSource = new List<V_ICSEOUTBILLMODEL>(_service.GetDeliveryByGroupNo(IcseoutbillModel.FGROUP_NO));



                /*
                        dw=selectTHD.dw,
                        dj=selectTHD.dj,
                        pz=selectTHD.cppz,
                        xh=selectTHD.cpxh,
                        gg=selectTHD.cpgg,
                        khhm=selectTHD.khhm,
                        khmc=selectTHD.khmc,
                        cpdj=selectTHD.cpdj
                */

            }
            else
            {
                //生成新的组柜单号
                txt发货计划号.Text = _service.GetNewBillNo("ZG");
            }

            string orderNames = "天津、成都、安庆、芜湖、青岛、哈尔滨、太原、济南、昆明、无锡、石家庄、唐山、邢台、沧州";

            var list = _service.GetDics("101", "", false).Select(p => new
            {
                FID = p.FID,
                名称 = p.FNAME,
                order = orderNames.IndexOf(p.FNAME.Replace("市", "")) ==-1?999 : orderNames.IndexOf(p.FNAME.Replace("市",""))
            }).OrderBy(p => p.order).ToList();

            GridColumn column = new GridColumn();
            column.Caption = "FID";
            column.FieldName = "FID";
            column.Visible = false;
            searchLookUpEdit1.Properties.View.Columns.Add(column);
            column = new GridColumn();
            column.Caption = "名称";
            column.FieldName = "名称";
            column.Visible = true;
            searchLookUpEdit1.Properties.View.Columns.Add(column);
            column = new GridColumn();
            column.Caption = "order";
            column.FieldName = "order";
            column.Visible = false;
            searchLookUpEdit1.Properties.View.Columns.Add(column);

            searchLookUpEdit1.Properties.ValueMember = "FID";
            searchLookUpEdit1.Properties.DisplayMember = "名称";
            searchLookUpEdit1.Properties.DataSource = list;

        }
        #endregion

        #region ■------------------ 运行日志

        private void LogError(Exception ex)
        {
            LogHelper.WriteLog(typeof(FrmSGPGroupCounter), ex);
        }

        private void LogError(string msg)
        {
            LogHelper.WriteLog(typeof(FrmSGPGroupCounter), msg);
        }

        #endregion

        #region ■------------------ 界面实现

        private void pnl跑龙套3_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                using (Pen pen = new Pen(Color.FromArgb(165, 172, 181)))
                {
                    e.Graphics.DrawLine(pen, new Point(0, 0), new Point(pnl跑龙套3.Width, 0));
                    e.Graphics.DrawLine(pen, new Point(0, pnl跑龙套3.Height - 1), new Point(pnl跑龙套3.Width, pnl跑龙套3.Height - 1));

                    //e.Graphics.DrawLine(pen, new Point(0, 0), new Point(0, pnl跑龙套3.Height));
                    //e.Graphics.DrawLine(pen, new Point(pnl跑龙套3.Width - 1, 0), new Point(pnl跑龙套3.Width - 1, pnl跑龙套3.Height));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }



        #endregion

        #region ■------------------ 按钮实现

        private void btnAPPClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }




        #endregion

        private void searchControl1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmQueryMarketArea frm = new FrmQueryMarketArea();
            frm.ShowDialog();
        }

        private void searchControl2_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmQueryMarketArea frm = new FrmQueryMarketArea();
            frm.ShowDialog();
        }

        private void sr厂家账户_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
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
                    //sr厂家账户.Text = frm.SelectName;
                    //sr厂家账户.Tag = frm.SelectID;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void sr承运公司_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                FrmQueryExpressCompany frm = new FrmQueryExpressCompany();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    sr承运公司.Text = frm.SelectName;
                    sr承运公司.Tag = frm.SelectID;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }


        private void gridView请购计划列表_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

        private bool mbProvinceid = false;

        public List<string> listICPRBILLID = new List<string>();
        public List<string> listTHD = new List<string>();


        int index = 1;



        private void btn添加到组柜_Click(object sender, EventArgs e)
        {



            var listTHD = gridControl库存查询.DataSource as v_thdModel[];


            foreach (var sub in listTHD)
            {
                if (sub.FCHECK)
                {
                    v_thdModel selectTHD = new v_thdModel();
                    selectTHD = sub;


                    if (string.IsNullOrEmpty(selectTHD.cpdj))
                    {
                        System.Windows.Forms.MessageBox.Show("请选择提货单!");
                        return;
                    }




                    List<V_ICSEOUTBILLENTRYMODEL> list = new List<V_ICSEOUTBILLENTRYMODEL>();
                    if (gridControl组柜明细.DataSource != null)
                    {
                        list = gridControl组柜明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
                    }

                    //var commitqty = Math.Round(hnamount / rate, 2);
                    var commitqty = selectTHD.LEFTNUM1 > (decimal.Parse(selectTHD.sl) - selectTHD.USENUM) ? (decimal.Parse(selectTHD.sl) - selectTHD.USENUM) : selectTHD.LEFTNUM1;
                    //bhd sl

                    string strTempAccount = selectTHD.khhm;
                    switch (selectTHD.DB)
                    {
                        case "10":
                            strTempAccount += "FDK";
                            break;
                        case "2":
                            strTempAccount += "MN";
                            break;
                        case "3":
                            strTempAccount += "GW";
                            break;
                    }



                    V_CLIENTACCOUNTModel v_Client = _service.GetClientByAccount(strTempAccount);
                    string tClientID = v_Client.FID;
                    string tCLientName = v_Client.FNAME;

                    string fsrcid = "000";
                    string fitemid = "";

                    SRCModel src = _service.getSrc(selectTHD.cppz, selectTHD.cpgg, selectTHD.cpxh);
                    if (src != null)
                    {
                        fsrcid = src.FID;
                        fitemid = src.FPRODUCTID;
                    }



                    list.Add(new V_ICSEOUTBILLENTRYMODEL()
                    {
                        FSRCID = fsrcid,
                        FITEMID = fitemid,
                        FBRAND = "01",
                        FDESCRIPTION = selectTHD.bz,
                        FCLIENTID = tClientID,
                        FCLIENTNAME = tCLientName,
                        FENTRYID = index,
                        FLEVEL = "AA",
                        FORDERUNIT = "",
                        FCOMMITQTY = commitqty,
                        FPRICEPOLICYENTRYID = "1",
                        thdbm = selectTHD.AUTOID.ToStr(),
                        LEFTNUM = selectTHD.LEFTNUM1,
                        dw = selectTHD.dw,
                        dj = selectTHD.dj,
                        pz = selectTHD.cppz,
                        xh = selectTHD.cpxh,
                        gg = selectTHD.cpgg,
                        khhm = selectTHD.khhm,
                        khmc = selectTHD.khmc,
                        cpdj = selectTHD.cpdj,
                        pzhm = selectTHD.pzhm,
                        kdrq = selectTHD.rq.ToString("yyyy-MM-dd"),
                        cpcm = selectTHD.cpcm,
                        cpsh = selectTHD.cpsh,
                        perWeight = string.IsNullOrEmpty(selectTHD.gg) ? "0" : selectTHD.gg,
                        tCount = selectTHD.ks,
                        FWEIGHT = decimal.Parse(string.IsNullOrEmpty(selectTHD.gg) ? "0" : selectTHD.gg) * decimal.Parse(selectTHD.ks) * commitqty,
                        FICPRID = selectTHD.icprbillentryid,
                    });



                    index++;


                    gridControl组柜明细.DataSource = list;
                    gridControl组柜明细.RefreshDataSource();


                    txt汇总重量.Text = this.gridView组柜明细.Columns["FWEIGHT"].SummaryItem.SummaryValue.ToStr();
                    // txt汇总体积.Text = this.gridView组柜明细.Columns["FVOLUME"].SummaryItem.SummaryValue.ToStr();

                }
            }

        }

        private void btn查询_Click(object sender, EventArgs e)
        {


        }


        public bool onCheck()
        {
            var datasource = gridControl组柜明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
            var datalist = datasource.OrderBy(m => m.FBRAND).ThenBy(m => m.FCLIENTID).ThenBy(m => m.FTRUSTEESHIP).ToList();

            //品牌部
            List<string> listResult = new List<string>();

            foreach (V_ICSEOUTBILLENTRYMODEL entry in datalist)
            {
                string subid = "";
                if (!string.IsNullOrEmpty(entry.FID))
                {
                    subid = entry.FID;
                }

                int iLocalSum = _service.GetLocalSum(entry.thdbm, subid);
                v_thdModel vThd = _service.getTHD(entry.thdbm);
                int iNew = (int)entry.FCOMMITQTY;

                if (iLocalSum + iNew > int.Parse(vThd.sl))
                {
                    listResult.Add(vThd.cpgg + " " + vThd.cpxh + " 提货单数量" + vThd.sl + " 保存后数量" + Convert.ToString(iLocalSum + iNew));
                }
                //qqqqqqqqqqqqqqqqqqqqq
                else if (IcseoutbillModel == null && (int)vThd.USENUM + iNew > int.Parse(vThd.sl))
                {
                    listResult.Add(vThd.cpgg + " " + vThd.cpxh + " 提货单数量" + vThd.sl + " 保存后数量" + Convert.ToString(vThd.USENUM + iNew));

                }


            }
            if (listResult.Count > 0)
            {
                string strMes = "";
                foreach (var sub in listResult)
                {
                    strMes += sub + "\r\n";
                }

                System.Windows.Forms.MessageBox.Show(strMes);

                return false;
            }
            else
            {
                return true;
            }


        }

        private bool onSave(bool delivery = false)
        {
            if (delivery)
            {
                if (cbo品牌.SelectedItem == null)
                {
                    MsgHelper.ShowError("请选择品牌/厂家！");
                    return false;
                }

                //if (string.IsNullOrEmpty(sr厂家账户.Text))
                //{
                //    MsgHelper.ShowError("请选择厂家账户！");
                //    return false;
                //}

                if (string.IsNullOrEmpty(sr二级销区.Text))
                {
                    MsgHelper.ShowError("请选择二级销区！");
                    return false;
                }

                if (string.IsNullOrEmpty(txt车牌号.Text))
                {
                    MsgHelper.ShowError("请输入车牌号！");
                    return false;
                }
            }

            ICSEOUTBILLMODEL model = new ICSEOUTBILLMODEL();
            if (IcseoutbillModel != null)
            {
                //model.FID = IcseoutbillModel.FID;
                model.JYDWNAME = IcseoutbillModel.JYDWNAME;
                model.JYDWID = IcseoutbillModel.JYDWID;

                model.FPREMISEID = IcseoutbillModel.FPREMISEID;
                model.FCLIENTID = IcseoutbillModel.FCLIENTID;
                model.FBRANDID = IcseoutbillModel.FBRANDID;
                model.FBILLNO = IcseoutbillModel.FBILLNO;
                model.FCARNUMBER = IcseoutbillModel.FCARNUMBER;
                model.FLOADCAPACITY = IcseoutbillModel.FLOADCAPACITY;
                model.FDELIVERER = IcseoutbillModel.FDELIVERER;
                model.FDELIVERERTEL = IcseoutbillModel.FDELIVERERTEL;
                model.FDELIVERERIDNO = IcseoutbillModel.FDELIVERERIDNO;
                model.FDELIVERERADDR = IcseoutbillModel.FDELIVERERADDR;
                model.FRECEIVER = IcseoutbillModel.FRECEIVER;
                model.FRECEIVERTEL = IcseoutbillModel.FRECEIVERTEL;
                model.FRECEIVERADDR = IcseoutbillModel.FRECEIVERADDR;
                model.FALLWEIGHT = IcseoutbillModel.FALLWEIGHT;
                model.FALLVOLUME = IcseoutbillModel.FALLVOLUME;
                model.FBILLERID = IcseoutbillModel.FBILLERID;
                model.FBILLDATE = IcseoutbillModel.FBILLDATE;
                model.FSTATUS = IcseoutbillModel.FSTATUS;
                model.FCHECKERID = IcseoutbillModel.FCHECKERID;
                model.FCHECKDATE = IcseoutbillModel.FCHECKDATE;
                model.FREMARK = IcseoutbillModel.FREMARK;
                model.FTRANSTYPE = IcseoutbillModel.FTRANSTYPE;
                model.FTRANSID = IcseoutbillModel.FTRANSID;
                model.FDELIVERDATE = IcseoutbillModel.FDELIVERDATE;
                model.FFACTORYSTATUS = IcseoutbillModel.FFACTORYSTATUS;
                model.FSYNCSTATUS = IcseoutbillModel.FSYNCSTATUS;
                model.FCENTER_WAREHOUSE = IcseoutbillModel.FCENTER_WAREHOUSE;
                model.FIS_CONSIGN = IcseoutbillModel.FIS_CONSIGN;
                model.FDELIVERY_METHOD = IcseoutbillModel.FDELIVERY_METHOD;
                model.FEXPRESSCOMPANYID = IcseoutbillModel.FEXPRESSCOMPANYID;
                model.FPROJECTNAME = IcseoutbillModel.FPROJECTNAME;
                model.FPURCHASE_NO = IcseoutbillModel.FPURCHASE_NO;
                model.FPLANDESC = IcseoutbillModel.FPLANDESC;
                model.FSRCBILLNO = IcseoutbillModel.FSRCBILLNO;
                model.FBILLING_TYPE = IcseoutbillModel.FBILLING_TYPE;
                model.FGROUP_NO = IcseoutbillModel.FGROUP_NO;
                model.FSETTLE_ORG = IcseoutbillModel.FSETTLE_ORG;
                model.FDELIVERY_REQUIRE = IcseoutbillModel.FDELIVERY_REQUIRE;
                model.FBRAND_DEPART = IcseoutbillModel.FBRAND_DEPART;
                model.FPLAN_INFO = IcseoutbillModel.FPLAN_INFO;

                _service.DeleteGroupBill(model.FGROUP_NO);
            }

            model.FGROUP_NO = txt发货计划号.Text;
            if (cbo品牌.SelectedItem != null)
            {
                model.FBRANDID = ((TB_BrandModel)cbo品牌.SelectedItem).FID;

                IniHelper.WriteString(Global.IniUrl, "CONFIG", "FBRANDID", model.FBRANDID);
            }

            //if (sr厂家账户.Tag != null)
            //{
            //    model.FCLIENTID = sr厂家账户.Tag.ToStr();
            //}

            if (sr二级销区.Tag != null)
            {
                model.FPREMISEID = sr二级销区.Tag.ToStr();
            }

            if (searchJYDW.Tag != null)
            {
                model.JYDWID = searchJYDW.Tag.ToStr();
                model.JYDWNAME = searchJYDW.Text.ToStr();
            }

            if (cbo运输方式.SelectedItem != null)
            {
                model.FTRANSID = ((SYS_SUBDICSMODEL)cbo运输方式.SelectedItem).FID;
            }

            if (searchJYDW.Tag != null)
            {
                model.JYDWNAME = searchJYDW.Text;
                model.JYDWID = searchJYDW.Tag.ToStr();
            }


            model.FCARNUMBER = txt车牌号.Text;
            model.FLOADCAPACITY = txt车型载重.Text;
            model.FDELIVERDATE = txt发货日期.DateTime;
            model.FDELIVERER = txt提货人.Text;
            model.FDELIVERERTEL = txt提货人电话.Text;

            model.FALLWEIGHT = txt汇总重量.Text.ToDecimal();
            model.FALLVOLUME = txt汇总体积.Text.ToDecimal();
            model.FCENTER_WAREHOUSE = txt中心仓.Text;
            if (cbo发货方式.SelectedItem != null)
            {
                model.FDELIVERY_METHOD = ((SYS_SUBDICSMODEL)cbo发货方式.SelectedItem).FID;
            }
            model.FDELIVERERADDR = txt委托人.Text;
            //model.FPURCHASE_NO = txt采购订单.Text;
            //model.FEETOTAL = txt标准运费.Text;
            //model.FPLANDESC = txt描述.Text;
            if (sr承运公司.Tag != null)
            {
                model.FEXPRESSCOMPANYID = sr承运公司.Tag.ToStr();
            }

            model.FPLAN_INFO = txt计划信息.Text;
            //model.FPROJECTNAME = txt工程名称.Text;
            //model.FREMARK = txt其他.Text; 
            if (cbo开单类型.SelectedItem != null)
            {
                model.FBILLING_TYPE = ((CodeValueClass)cbo开单类型.SelectedItem).value.ToInt();
            }
            model.FIS_CONSIGN = chk是否托管.Checked ? 1 : 0;
            model.FBILLERID = Global.LoginUser.FID;
            model.FBILLDATE = DateTime.Now;
            if (cbo省.SelectedItem != null)
            {
                model.FPROVINCEID = ((TB_EBPLModel)cbo省.SelectedItem).EBPL_CODE;
            }
            if (cbo市.SelectedItem != null)
            {
                model.FCITYID = ((TB_EBPLModel)cbo市.SelectedItem).EBPL_CODE;
            }
            if (cbo区县.SelectedItem != null)
            {
                model.FDISTRICTID = ((TB_EBPLModel)cbo区县.SelectedItem).EBPL_CODE;
            }

            if (cbo运输计价.SelectedItem != null)
            {
                model.FTRANSPORT_PRICE_TYPE = ((SYS_SUBDICSMODEL)cbo运输计价.SelectedItem).FID;
            }

            model.FRECEIVERADDR = txt收货方地址.Text;
            model.FCONSIGNEE = txt收货人.Text;
            model.FCONSIGNEE_TEL = txt收货人电话.Text;
            model.FCOMPANY = txt公司主体.Text;
            model.FISTICKET = chk是否开票.Checked ? 1 : 0;

            if (sr厂家发货基地.Tag != null)
            {
                model.FDELIVER_BASE_ID = sr厂家发货基地.Tag.ToStr();
            }

            model.FREQUEST_DELIVERY_DATE = txt要求到货日期.DateTime;
            model.FESTIMATED_DELIVERY_DATE = txt预计发货日期.DateTime;
            model.FIS_SIGN_BACK = chk是否签回单.Checked ? 1 : 0;
            model.FDELIVERY_TYPE = txt发货类型.Text;

            if (cbo区县.SelectedItem != null)
            {
                model.FDELIVERY_TYPE = ((TB_EBPLModel)cbo区县.SelectedItem).EBPL_CODE;
            }


            List<ICSEOUTBILLENTRYMODEL> entrys = new List<ICSEOUTBILLENTRYMODEL>();
            var datasource = gridControl组柜明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;

            var datalist = datasource.OrderBy(m => m.FBRAND).ThenBy(m => m.FCLIENTID).ThenBy(m => m.FTRUSTEESHIP).ToList();
            int index = 1;
            //品牌部
            string brand = "";
            foreach (V_ICSEOUTBILLENTRYMODEL entry in datalist)
            {

                if (entry.FTRUSTEESHIP == "Y")
                {
                    model.FBILLING_TYPE = 2;
                }

                if ((brand == "") || (brand == (entry.FBRAND + entry.FCLIENTID + entry.FTRUSTEESHIP + entry.FPRICENUMBER)))
                {
                    entrys.Add(new ICSEOUTBILLENTRYMODEL()
                    {

                        FID = entry.FID,
                        FICSEOUTID = entry.FICSEOUTID,
                        FSRCID = entry.FSRCID,
                        FPRICEPOLICYENTRYID = entry.FPRICEPOLICYENTRYID,
                        FICPRID = entry.FICPRID,
                        FENTRYID = index,
                        FCOMMITQTY = entry.FCOMMITQTY,
                        FHNAMOUNT = entry.FHNAMOUNT,
                        FSTOCKNUMBER = entry.FSTOCKNUMBER,
                        FSTOCK = entry.FSTOCK,
                        FSTOCKPLACE = entry.FSTOCKPLACE,
                        FWEIGHT = entry.FWEIGHT,
                        FVOLUME = entry.FVOLUME,
                        FREMARK = entry.FREMARK,
                        FBATCHNO = entry.FBATCHNO,
                        FCOLORNO = entry.FCOLORNO,
                        FLEVEL = entry.FLEVEL,
                        FWDR = entry.FWDR,
                        FITEMID = entry.FITEMID,
                        FPRICE = entry.FPRICE,
                        FAMOUNT = entry.FAMOUNT,
                        FERR_MESSAGE = entry.FERR_MESSAGE,
                        FNEEDDATE = entry.FNEEDDATE,
                        FORDERREMARK1 = entry.FORDERREMARK1,
                        FORDERREMARK2 = entry.FORDERREMARK2,
                        FDESCRIPTION = entry.FDESCRIPTION,
                        FBRAND = entry.FBRAND,
                        FCLIENTID = entry.FCLIENTID,
                        FTRUSTEESHIP = entry.FTRUSTEESHIP,
                        FLOCATION_NO = entry.FLOCATION_NO,
                        FPRICENUMBER = entry.FPRICENUMBER,
                        thdbm = entry.thdbm


                    });
                    model.FCLIENTID = entry.FCLIENTID;
                    model.FBRAND_DEPART = entry.FBRAND;
                    model.FPLANDESC = entry.FPRICENUMBER;


                    brand = entry.FBRAND + entry.FCLIENTID + entry.FTRUSTEESHIP + entry.FPRICENUMBER;
                }
                else
                {
                    if (delivery)
                    {
                        model.FBILLNO = _service.GetNewBillNo("DP");
                    }
                    else
                    {
                        model.FBILLNO = "";
                    }

                    //发货计划保存
                    _service.DeliveryBillSave(model, entrys.ToArray(), false);

                    entrys = new List<ICSEOUTBILLENTRYMODEL>();
                    index = 1;
                    entrys.Add(new ICSEOUTBILLENTRYMODEL()
                    {
                        FID = entry.FID,
                        FICSEOUTID = entry.FICSEOUTID,
                        FSRCID = entry.FSRCID,
                        FPRICEPOLICYENTRYID = entry.FPRICEPOLICYENTRYID,
                        FICPRID = entry.FICPRID,
                        FENTRYID = index,
                        FCOMMITQTY = entry.FCOMMITQTY,
                        FHNAMOUNT = entry.FHNAMOUNT,
                        FSTOCKNUMBER = entry.FSTOCKNUMBER,
                        FSTOCK = entry.FSTOCK,
                        FSTOCKPLACE = entry.FSTOCKPLACE,
                        FWEIGHT = entry.FWEIGHT,
                        FVOLUME = entry.FVOLUME,
                        FREMARK = entry.FREMARK,
                        FBATCHNO = entry.FBATCHNO,
                        FCOLORNO = entry.FCOLORNO,
                        FLEVEL = entry.FLEVEL,
                        FWDR = entry.FWDR,
                        FITEMID = entry.FITEMID,
                        FPRICE = entry.FPRICE,
                        FAMOUNT = entry.FAMOUNT,
                        FERR_MESSAGE = entry.FERR_MESSAGE,
                        FNEEDDATE = entry.FNEEDDATE,
                        FORDERREMARK1 = entry.FORDERREMARK1,
                        FORDERREMARK2 = entry.FORDERREMARK2,
                        FDESCRIPTION = entry.FDESCRIPTION,
                        FBRAND = entry.FBRAND,
                        FCLIENTID = entry.FCLIENTID,
                        FTRUSTEESHIP = entry.FTRUSTEESHIP,
                        FLOCATION_NO = entry.FLOCATION_NO,
                        FPRICENUMBER = entry.FPRICENUMBER,
                        thdbm = entry.thdbm
                    });

                    brand = entry.FBRAND + entry.FCLIENTID + entry.FTRUSTEESHIP + entry.FPRICENUMBER;
                    model.FCLIENTID = entry.FCLIENTID;
                    model.FBRAND_DEPART = entry.FBRAND;
                    model.FPLANDESC = entry.FPRICENUMBER;

                }

                index++;
            }

            if (delivery)
            {
                model.FBILLNO = _service.GetNewBillNo("DP");
            }
            else
            {
                model.FBILLNO = "";
            }
            //发货计划保存
            return _service.DeliveryBillSave(model, entrys.ToArray(), false);


        }

        private void btn保存_Click(object sender, EventArgs e)
        {
            if (onCheck() == false)
            {
                return;
            }
            else if (onSave())
            {
                MsgHelper.ShowInformation("保存成功！");
                if (SaveAfter != null)
                {
                    SaveAfter(null, null);
                }

                this.DialogResult = DialogResult.OK;
                if (IcseoutbillModel == null)
                {
                    this.onClear();
                }
                else
                {
                    this.Close();
                }
            }

        }



        private void btn关闭_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn删除行_Click(object sender, EventArgs e)
        {
            if (gridView组柜明细.FocusedRowHandle > -1)
            {
                gridView组柜明细.DeleteRow(gridView组柜明细.FocusedRowHandle);
            }
        }

        private void btn生成发货计划_Click(object sender, EventArgs e)
        {
            if (onSave(true))
            {
                MsgHelper.ShowInformation("处理完成！");
                if (SaveAfter != null)
                {
                    SaveAfter(null, null);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void FrmSGPGroupCounter_Load(object sender, EventArgs e)
        {
            setFormData();
        }




        bool bLoad = false;
        private void gridView请购计划明细_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            /*
             if (e.FocusedRowHandle > -1&&bLoad)
             {
                 string GG = gridView请购计划明细.GetRowCellValue(e.FocusedRowHandle, "GG").ToString();
                 string XH = gridView请购计划明细.GetRowCellValue(e.FocusedRowHandle, "XH").ToString();
                 string pjhm = gridView请购计划明细.GetRowCellValue(e.FocusedRowHandle, "FID").ToString();

                 txtGG.Text = GG;
                 txtXH.Text = XH;
                 txtJHDH.Text = pjhm;

                 btn库存查询_Click(null, null);
             }
             */
        }

        private void gridView请购计划明细_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void gridView组柜明细_CustomDrawRowFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {

        }

        private void gridView组柜明细_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

        }

        private void repositoryItemButtonEdit厂家代码_Click(object sender, EventArgs e)
        {

        }

        private void repositoryItemButtonEdit厂家代码_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridView组柜明细.FocusedRowHandle != -1)
            {
                var list = gridControl组柜明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
                var row = list[gridView组柜明细.GetDataSourceRowIndex(gridView组柜明细.FocusedRowHandle)];

                FrmQuerySrc frm = new FrmQuerySrc(row.FITEMID);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    row.FSRCID = frm.SelectData.FID;
                    row.FSRCNAME = frm.SelectData.FSRCNAME;
                    row.FSRCCODE = frm.SelectData.FSRCCODE;
                    row.FSRCMODEL = frm.SelectData.FSRCMODEL;
                    row.FSRCUNIT = frm.SelectData.FUNIT;
                    //row.FHNAMOUNT = 0;

                    var askqty = row.FLEFTAMOUNT;
                    if (frm.SelectData.FRATE != 0)
                    {
                        row.FORDERUNITQTY = Math.Ceiling(askqty / frm.SelectData.FRATE);

                        row.FCOMMITQTY = Math.Round(askqty / frm.SelectData.FRATE, 1);
                        //row.FWEIGHT = Math.Ceiling(row.FCOMMITQTY * frm.SelectData.FWEIGHT);
                    }

                    try
                    {
                        row.FWEIGHT = decimal.Parse(row.perWeight) * decimal.Parse(row.tCount) * row.FCOMMITQTY;
                    }
                    catch
                    {
                        row.FWEIGHT = decimal.Parse("0.0");
                    }


                    row.FLEFTAMOUNT = askqty;

                    gridControl组柜明细.DataSource = list;
                    gridControl组柜明细.RefreshDataSource();
                }
            }
        }


        private void repositoryItemCheckEdit2_CheckedChanged(object sender, EventArgs e)
        {
            var list = gridControl库存查询.DataSource as thdModel[];
            foreach (var row in list)
            {
                row.FCHECK = false;
            }

            try
            {
                int iKD = list.Sum(x => int.Parse(x.sl));
                int iCC = list.Sum(x => x.XZSL);
                int iSY = iKD - iCC;
                labSum.Text = "合计：开单" + iKD + " 出仓" + iCC + " 剩余" + iSY;
            }
            catch
            {
                labSum.Text = "";
            }

            gridControl库存查询.DataSource = list;
            gridControl库存查询.RefreshDataSource();
        }


        public List<v_thdModel> getTHD()
        {



            MApiModel.api8.Rootobject getapi8 = new MApiModel.api8.Rootobject();
            getapi8.pzhm = "";
            getapi8.rq1 = dateEdit1.DateTime.ToString("2017-01-01");
            getapi8.rq2 = DateTime.Now.ToString("yyyy-MM-dd");
            string cpgg = "";
            string cpxh = "";
            string thdh = "";

            int iPP = 0;

            var list = _service.GeICPO_BOLListMN_db(getapi8, cpxh, cpgg, "", thdh, txtArea.Text.Trim(), iPP);
            return list.ToList();
        }

        private void btn库存查询_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;

            MApiModel.api8.Rootobject getapi8 = new MApiModel.api8.Rootobject();
            getapi8.pzhm = "";
            getapi8.rq1 = dateEdit1.DateTime.ToString("yyyy-MM-dd");
            getapi8.rq2 = dateEdit2.DateTime.ToString("yyyy-MM-dd");
            string cpgg = txtGG.Text;
            string cpxh = txtXH.Text;
            string thdh = textTHD.Text;

            int iPP = 0;
            if (txtArea.Text == "无匹配关系") iPP = 1;

            var list = _service.GeICPO_BOLListMN_db(getapi8, cpxh, cpgg, "", thdh, txtArea.Text.Trim(), iPP);
            if (list != null)
            {


                //    各型号
                //    托板
                //    捆绑器 


                // list = list.Where(x =>x.LEFTNUM1>0).ToArray();

                try
                {
                    int iKD = list.Sum(x => int.Parse(x.sl));
                    int iCC = list.Sum(x => x.XZSL);
                    int iSY = iKD - iCC;
                    labSum.Text = "合计：开单" + iKD + " 出仓" + iCC + " 剩余" + iSY;
                }
                catch
                {
                    labSum.Text = "";
                }

                gridControl库存查询.DataSource = list;
                this.Cursor = Cursors.Default;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(list.ToString());
            }


        }

        private void gridView组柜明细_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (gridView组柜明细.FocusedRowHandle != -1 && e.Column.FieldName == "FCOMMITQTY")
            {
                var list = gridControl组柜明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
                var row = list[gridView组柜明细.GetDataSourceRowIndex(gridView组柜明细.FocusedRowHandle)];
                var listSrc = _service.GetSrcList(row.FITEMID, "");
                foreach (var srcModel in listSrc)
                {
                    if (srcModel.FID == row.FSRCID)
                    {
                        // row.FWEIGHT = Math.Ceiling(e.Value.ToDecimal() * srcModel.FWEIGHT);
                        try
                        {
                            row.FWEIGHT = decimal.Parse(row.perWeight) * decimal.Parse(row.tCount) * row.FCOMMITQTY;
                        }
                        catch
                        {
                            row.FWEIGHT = decimal.Parse("0.0");
                        }


                        gridControl组柜明细.DataSource = list;
                        gridControl组柜明细.RefreshDataSource();
                    }
                }
            }
        }


        private void btn行复制_Click(object sender, EventArgs e)
        {
            if (gridView组柜明细.FocusedRowHandle > -1)
            {
                var list = gridControl组柜明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
                var row = list[gridView组柜明细.GetDataSourceRowIndex(gridView组柜明细.FocusedRowHandle)];
                list.Add(new V_ICSEOUTBILLENTRYMODEL()
                {
                    FID = list.Count() + 1.ToStr(),
                    FMODEL = row.FMODEL,
                    FENTRYID = list.Count() + 1,
                    FCATEGORYNAME = row.FCATEGORYNAME,
                    FBRAND = row.FBRAND,
                    FPRODUCTNAME = row.FPRODUCTNAME,
                    FPRODUCTTYPE = row.FPRODUCTTYPE,
                    FPRODUCTCODE = row.FPRODUCTCODE,
                    FUNITNAME = row.FUNITNAME,
                    FBATCHNO = row.FBATCHNO,
                    FCOLORNO = row.FCOLORNO,
                    FREMARK = row.FREMARK,
                    FSRCID = row.FSRCID,
                    FSRCNAME = row.FSRCNAME,
                    FSRCCODE = row.FSRCCODE,
                    FSRCMODEL = row.FSRCMODEL,
                    FSRCUNIT = row.FSRCUNIT,
                    FORDERUNIT = row.FSRCUNIT,
                    FRATE = row.FRATE,
                    FCOMMITQTY = row.FCOMMITQTY,
                    FNEEDDATE = row.FNEEDDATE,
                    FPRICEPOLICYENTRYID = row.FPRICEPOLICYENTRYID,
                    FICPRID = row.FICPRID,
                    FWEIGHT = row.FWEIGHT,
                    FVOLUME = row.FVOLUME,
                    FITEMID = row.FITEMID,
                    ICPRBILLNO = row.ICPRBILLNO,
                    FICPRENTRYID = row.FICPRENTRYID,
                    FLEVEL = row.FLEVEL,
                    FPREMISENAME = row.FPREMISENAME,
                    FASKQTY = row.FASKQTY,
                    FORDERUNITQTY = row.FORDERUNITQTY,
                    FLEFTAMOUNT = 0,
                    FORDERREMARK1 = row.FORDERREMARK1,
                    FORDERREMARK2 = row.FORDERREMARK2,
                    FFACTORYNO = row.FFACTORYNO,
                    JDE = row.JDE,
                    FHNAMOUNT = row.FHNAMOUNT,
                    FSTOCK = row.FSTOCK,
                    FSTOCKPLACE = row.FSTOCKPLACE,
                    FWDR = row.FWDR,
                    FTRUSTEESHIP = row.FTRUSTEESHIP,
                    FLOCATION_NO = row.FLOCATION_NO,
                    FPRICENUMBER = row.FPRICENUMBER,
                });

                gridControl组柜明细.DataSource = list;
                gridControl组柜明细.RefreshDataSource();
            }
        }

        private void repositoryItemButtonEdit厂家账户_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridView组柜明细.FocusedRowHandle != -1)
            {
                var list = gridControl组柜明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
                var row = list[gridView组柜明细.GetDataSourceRowIndex(gridView组柜明细.FocusedRowHandle)];

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
                    row.FCLIENTID = frm.SelectID;
                    row.FCLIENTNAME = frm.SelectName;

                    gridView组柜明细.ActiveEditor.EditValue = frm.SelectName;
                    gridControl组柜明细.DataSource = list;
                    gridControl组柜明细.RefreshDataSource();
                }
            }
        }

        private void btn厂家账户批量填充_Click(object sender, EventArgs e)
        {
            if (!checkHaveChecked())
            {
                MsgHelper.ShowError("请先勾选您要填充的数据！");
                return;
            }

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
                var list = gridControl组柜明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
                List<string> ids = new List<string>();

                for (int i = 0; i < gridView组柜明细.RowCount; i++)
                {
                    bool b = gridView组柜明细.GetRowCellValue(i, "FCHECK").ToBool();
                    if (b)
                    {
                        var row = list[gridView组柜明细.GetDataSourceRowIndex(i)];
                        row.FCLIENTID = frm.SelectID;
                        row.FCLIENTNAME = frm.SelectName;
                    }
                }

                gridControl组柜明细.DataSource = list;
                gridControl组柜明细.RefreshDataSource();
            }
        }


        private bool checkHaveChecked()
        {
            List<string> ids = new List<string>();

            for (int i = 0; i < gridView组柜明细.RowCount; i++)
            {
                bool b = gridView组柜明细.GetRowCellValue(i, "FCHECK").ToBool();
                if (b)
                {
                    ids.Add(gridView组柜明细.GetRowCellValue(i, "FID").ToStr());
                }
            }

            return ids.Count > 0;
        }

        private void Frm_SaveAfter(object sender, EventArgs e)
        {
            btn查询_Click(null, null);
        }

        private void btn取消作废_Click(object sender, EventArgs e)
        {
            List<string> ids = new List<string>();

            for (int i = 0; i < gridView组柜明细.RowCount; i++)
            {
                bool b = gridView组柜明细.GetRowCellValue(i, "FCHECK").ToBool();
                if (b)
                {
                    ids.Add(gridView组柜明细.GetRowCellValue(i, "FID").ToStr());
                }
            }

            if (ids.Count > 0)
            {
                _service.UpdateDeliveryGroupStatus(string.Join(",", ids.ToArray()), 1);


                gridControl组柜明细.DataSource = new List<V_ICSEOUTBILLENTRYMODEL>(_service.GetDeliveryEntryByGroupNo(IcseoutbillModel.FGROUP_NO));

                MsgHelper.ShowInformation("处理完成！");
            }
            else
            {
                MsgHelper.ShowError("请勾选要处理的数据！");
            }
        }

        private void btn反作废_Click(object sender, EventArgs e)
        {
            List<string> ids = new List<string>();

            for (int i = 0; i < gridView组柜明细.RowCount; i++)
            {
                bool b = gridView组柜明细.GetRowCellValue(i, "FCHECK").ToBool();
                if (b)
                {
                    ids.Add(gridView组柜明细.GetRowCellValue(i, "FID").ToStr());
                }
            }

            if (ids.Count > 0)
            {
                _service.UpdateDeliveryGroupStatus(string.Join(",", ids.ToArray()), 0);


                gridControl组柜明细.DataSource = new List<V_ICSEOUTBILLENTRYMODEL>(_service.GetDeliveryEntryByGroupNo(IcseoutbillModel.FGROUP_NO));

                MsgHelper.ShowInformation("处理完成！");
            }
            else
            {
                MsgHelper.ShowError("请勾选要处理的数据！");
            }
        }

        private void btn变更_Click(object sender, EventArgs e)
        {

            if (OnChange())
            {
                MsgHelper.ShowInformation("保存成功！");
                if (SaveAfter != null)
                {
                    SaveAfter(null, null);
                }

                this.DialogResult = DialogResult.OK;
                if (IcseoutbillModel == null)
                {
                    this.onClear();
                }
                else
                {
                    this.Close();
                }
            }

        }

        private bool OnChange()
        {
            ICSEOUTBILLMODEL model = new ICSEOUTBILLMODEL();
            if (IcseoutbillModel != null)
            {
                model.FID = null;
                model.JYDWNAME = IcseoutbillModel.JYDWNAME;
                model.JYDWID = IcseoutbillModel.JYDWID;

                model.FPREMISEID = IcseoutbillModel.FPREMISEID;
                model.FCLIENTID = IcseoutbillModel.FCLIENTID;
                model.FBRANDID = IcseoutbillModel.FBRANDID;
                //model.FBILLNO = _service.GetNewBillNo("DP");
                model.FCARNUMBER = IcseoutbillModel.FCARNUMBER;
                model.FLOADCAPACITY = IcseoutbillModel.FLOADCAPACITY;
                model.FDELIVERER = IcseoutbillModel.FDELIVERER;
                model.FDELIVERERTEL = IcseoutbillModel.FDELIVERERTEL;
                model.FDELIVERERIDNO = IcseoutbillModel.FDELIVERERIDNO;
                model.FDELIVERERADDR = IcseoutbillModel.FDELIVERERADDR;
                model.FRECEIVER = IcseoutbillModel.FRECEIVER;
                model.FRECEIVERTEL = IcseoutbillModel.FRECEIVERTEL;
                model.FRECEIVERADDR = IcseoutbillModel.FRECEIVERADDR;
                model.FALLWEIGHT = IcseoutbillModel.FALLWEIGHT;
                model.FALLVOLUME = IcseoutbillModel.FALLVOLUME;
                model.FBILLERID = IcseoutbillModel.FBILLERID;
                model.FBILLDATE = IcseoutbillModel.FBILLDATE;
                model.FSTATUS = IcseoutbillModel.FSTATUS;
                model.FCHECKERID = IcseoutbillModel.FCHECKERID;
                model.FCHECKDATE = IcseoutbillModel.FCHECKDATE;
                model.FREMARK = IcseoutbillModel.FREMARK;
                model.FTRANSTYPE = IcseoutbillModel.FTRANSTYPE;
                model.FTRANSID = IcseoutbillModel.FTRANSID;
                model.FDELIVERDATE = IcseoutbillModel.FDELIVERDATE;
                model.FFACTORYSTATUS = IcseoutbillModel.FFACTORYSTATUS;
                model.FSYNCSTATUS = IcseoutbillModel.FSYNCSTATUS;
                model.FCENTER_WAREHOUSE = IcseoutbillModel.FCENTER_WAREHOUSE;
                model.FIS_CONSIGN = IcseoutbillModel.FIS_CONSIGN;
                model.FDELIVERY_METHOD = IcseoutbillModel.FDELIVERY_METHOD;
                model.FEXPRESSCOMPANYID = IcseoutbillModel.FEXPRESSCOMPANYID;
                model.FPROJECTNAME = IcseoutbillModel.FPROJECTNAME;
                model.FPURCHASE_NO = IcseoutbillModel.FPURCHASE_NO;
                model.FPLANDESC = IcseoutbillModel.FPLANDESC;
                model.FSRCBILLNO = IcseoutbillModel.FSRCBILLNO;
                model.FBILLING_TYPE = IcseoutbillModel.FBILLING_TYPE;
                model.FGROUP_NO = IcseoutbillModel.FGROUP_NO;
                model.FSETTLE_ORG = IcseoutbillModel.FSETTLE_ORG;
                model.FDELIVERY_REQUIRE = IcseoutbillModel.FDELIVERY_REQUIRE;
                model.FBRAND_DEPART = IcseoutbillModel.FBRAND_DEPART;
                model.FPLAN_INFO = IcseoutbillModel.FPLAN_INFO;

            }

            List<ICSEOUTBILLENTRYMODEL> entrys = new List<ICSEOUTBILLENTRYMODEL>();
            var datasource = gridControl组柜明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;

            var datalist = datasource.OrderBy(m => m.FBRAND).ThenBy(m => m.FCLIENTID).ThenBy(m => m.FTRUSTEESHIP).ToList();
            int index = 1;
            int count = 0;
            //品牌部
            string brand = "";
            foreach (V_ICSEOUTBILLENTRYMODEL entry in datalist)
            {
                if (string.IsNullOrEmpty(entry.FICSEOUTID))
                {
                    count++;
                    if ((brand == "") || (brand == (entry.FBRAND + entry.FCLIENTID + entry.FTRUSTEESHIP + entry.FPRICENUMBER)))
                    {
                        entrys.Add(new ICSEOUTBILLENTRYMODEL()
                        {
                            FID = entry.FID,
                            FICSEOUTID = entry.FICSEOUTID,
                            FSRCID = entry.FSRCID,
                            FPRICEPOLICYENTRYID = entry.FPRICEPOLICYENTRYID,
                            FICPRID = entry.FICPRID,
                            FENTRYID = index,
                            FCOMMITQTY = entry.FCOMMITQTY,
                            FHNAMOUNT = entry.FHNAMOUNT,
                            FSTOCKNUMBER = entry.FSTOCKNUMBER,
                            FSTOCK = entry.FSTOCK,
                            FSTOCKPLACE = entry.FSTOCKPLACE,
                            FWEIGHT = entry.FWEIGHT,
                            FVOLUME = entry.FVOLUME,
                            FREMARK = entry.FREMARK,
                            FBATCHNO = entry.FBATCHNO,
                            FCOLORNO = entry.FCOLORNO,
                            FLEVEL = entry.FLEVEL,
                            FWDR = entry.FWDR,
                            FITEMID = entry.FITEMID,
                            FPRICE = entry.FPRICE,
                            FAMOUNT = entry.FAMOUNT,
                            FERR_MESSAGE = entry.FERR_MESSAGE,
                            FNEEDDATE = entry.FNEEDDATE,
                            FORDERREMARK1 = entry.FORDERREMARK1,
                            FORDERREMARK2 = entry.FORDERREMARK2,
                            FDESCRIPTION = entry.FDESCRIPTION,
                            FBRAND = entry.FBRAND,
                            FCLIENTID = entry.FCLIENTID,
                            FTRUSTEESHIP = entry.FTRUSTEESHIP,
                            FLOCATION_NO = entry.FLOCATION_NO
                        });
                        model.FCLIENTID = entry.FCLIENTID;
                        model.FBRAND_DEPART = entry.FBRAND;
                        model.FPLANDESC = entry.FPRICENUMBER;

                        brand = entry.FBRAND + entry.FCLIENTID + entry.FTRUSTEESHIP + entry.FPRICENUMBER;
                    }
                    else
                    {
                        model.FBILLNO = _service.GetNewBillNo("DP");

                        //发货计划保存
                        _service.DeliveryBillSave(model, entrys.ToArray(), false);

                        entrys = new List<ICSEOUTBILLENTRYMODEL>();
                        index = 1;
                        entrys.Add(new ICSEOUTBILLENTRYMODEL()
                        {
                            FID = entry.FID,
                            FICSEOUTID = entry.FICSEOUTID,
                            FSRCID = entry.FSRCID,
                            FPRICEPOLICYENTRYID = entry.FPRICEPOLICYENTRYID,
                            FICPRID = entry.FICPRID,
                            FENTRYID = index,
                            FCOMMITQTY = entry.FCOMMITQTY,
                            FHNAMOUNT = entry.FHNAMOUNT,
                            FSTOCKNUMBER = entry.FSTOCKNUMBER,
                            FSTOCK = entry.FSTOCK,
                            FSTOCKPLACE = entry.FSTOCKPLACE,
                            FWEIGHT = entry.FWEIGHT,
                            FVOLUME = entry.FVOLUME,
                            FREMARK = entry.FREMARK,
                            FBATCHNO = entry.FBATCHNO,
                            FCOLORNO = entry.FCOLORNO,
                            FLEVEL = entry.FLEVEL,
                            FWDR = entry.FWDR,
                            FITEMID = entry.FITEMID,
                            FPRICE = entry.FPRICE,
                            FAMOUNT = entry.FAMOUNT,
                            FERR_MESSAGE = entry.FERR_MESSAGE,
                            FNEEDDATE = entry.FNEEDDATE,
                            FORDERREMARK1 = entry.FORDERREMARK1,
                            FORDERREMARK2 = entry.FORDERREMARK2,
                            FDESCRIPTION = entry.FDESCRIPTION,
                            FBRAND = entry.FBRAND,
                            FCLIENTID = entry.FCLIENTID,
                            FTRUSTEESHIP = entry.FTRUSTEESHIP,
                            FLOCATION_NO = entry.FLOCATION_NO,
                        });

                        brand = entry.FBRAND + entry.FCLIENTID + entry.FTRUSTEESHIP + entry.FPRICENUMBER;
                        model.FCLIENTID = entry.FCLIENTID;
                        model.FBRAND_DEPART = entry.FBRAND;
                        model.FPLANDESC = entry.FPRICENUMBER;
                    }

                    index++;
                }
            }

            if (count > 0)
            {
                model.FBILLNO = _service.GetNewBillNo("DP");
                //发货计划保存
                _service.DeliveryBillSave(model, entrys.ToArray(), false);

                return true;
            }
            else
            {
                MsgHelper.ShowInformation("没有需要变更的数据");
                return false;
            }


        }

        private void gridView组柜明细_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (gridControl组柜明细.DataSource != null && e.RowHandle > -1)
            {
                var list = gridControl组柜明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
                var row = list[gridView组柜明细.GetDataSourceRowIndex(e.RowHandle)];
                if (row.FGROUP_STATUS == 1)
                {
                    e.Appearance.BackColor = Color.FromArgb(211, 211, 211);
                }
            }

        }

        private void chk全选_CheckedChanged(object sender, EventArgs e)
        {
            var list = gridControl组柜明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;
            foreach (var model in list)
            {
                model.FCHECK = chk全选.Checked;
            }
            gridControl组柜明细.DataSource = list;
            gridControl组柜明细.RefreshDataSource();
        }


        private void cbo省_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mbProvinceid)
            {
                var item = cbo省.SelectedItem as TB_EBPLModel;
                if (item != null)
                {
                    cbo市.Properties.Items.AddRange(_service.GetCity(item.EBPL_CODE));
                }
            }
        }

        private void cbo市_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mbProvinceid)
            {
                var item = cbo市.SelectedItem as TB_EBPLModel;
                if (item != null)
                {
                    cbo区县.Properties.Items.AddRange(_service.GetDistrict(item.EBPL_CODE));
                }
            }
        }

        private void sr厂家发货基地_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                string brand = "";
                if (cbo品牌.SelectedItem != null)
                {
                    TB_BrandModel model = cbo品牌.SelectedItem as TB_BrandModel;
                    if (model != null)
                        brand = model.FID;
                }


                FrmQueryDeliverBase frm = new FrmQueryDeliverBase(brand);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    sr厂家发货基地.Text = frm.SelectName;
                    sr厂家发货基地.Tag = frm.SelectID;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            initData();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtArea.Text = "";
            txtJHDH.Text = "";
            txtGG.Text = "";
            txtXH.Text = "";

        }


        string result = "";
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            simpleButton2.Text = "提货单同步中";
            simpleButton2.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                #region 旧代码，弃用
                //List<DateTime> listDatetime = new List<DateTime>();
                //string strStartDate = _service.GetStartDate_syn();

                //DateTime dtStartTime = DateTime.Parse(strStartDate);



                ////test
                //dtStartTime = DateTime.Now.AddDays(-3);

                //TimeSpan iTimeSpan = DateTime.Now - dtStartTime;

                //backgroundWorker1.ReportProgress(0, "正在递增查询...");

                //for (int i = 0; i <= iTimeSpan.Days; i++)
                //{
                //    try
                //    {
                //        DateTime theTime = dtStartTime.AddDays(i);

                //        backgroundWorker1.ReportProgress(0, "正在递增查询" + theTime.ToString("yyyy-MM-dd") + "数据");
                //        listDatetime.Add(theTime);
                //        result = _service.GeICPO_BOLListMN_syn(theTime);
                //        backgroundWorker1.ReportProgress(0, theTime.ToString("yyyy-MM-dd") + "补充结果：" + result);

                //    }
                //    catch (Exception ee)
                //    {
                //        result = ee.ToStr();
                //        backgroundWorker1.ReportProgress(0, result);
                //    }

                //}


                //backgroundWorker1.ReportProgress(0, "出仓数量查询...");
                //strStartDate = _service.GetStartDate_syn_cc();
                //dtStartTime = DateTime.Parse(strStartDate);
                //iTimeSpan = DateTime.Now - dtStartTime;
                //for (int i = 0; i <= iTimeSpan.Days; i++)
                //{
                //    try
                //    {
                //        DateTime theTime = dtStartTime.AddDays(i);

                //        if (!listDatetime.Any(x => x.Date == theTime.Date))
                //        {
                //            backgroundWorker1.ReportProgress(0, "查询" + theTime.ToString("yyyy-MM-dd") + "出仓数量");

                //            result = _service.GeICPO_BOLListMN_syn_cc(theTime);
                //        }

                //    }
                //    catch (Exception ee)
                //    {
                //        result = ee.ToStr();
                //        backgroundWorker1.ReportProgress(0, result);
                //    }

                //}
                #endregion

                string rq1, rq2;

                rq1 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                rq2 = rq1;

                _service.Sync_Today_THD(rq1, rq2);

            }
            catch (Exception ee)
            {
                backgroundWorker1.ReportProgress(0, ee.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            simpleButton2.Text = "同步提货单";
            simpleButton2.Enabled = true;
            labInfo.Text = "同步完成!";
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null) labInfo.Text = e.UserState.ToString();
        }

        private void SelectICPR_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmPurchasePlanImport fImport = new FrmPurchasePlanImport("", new List<string>());
            if (fImport.ShowDialog() == DialogResult.OK)
            {
                if (fImport.listCG != null && fImport.listCG.Count > 0)
                {
                    var list = gridControl组柜明细.DataSource as List<V_ICSEOUTBILLENTRYMODEL>;

                    var row = list[gridView组柜明细.GetDataSourceRowIndex(gridView组柜明细.FocusedRowHandle)];

                    row.FICPRID = fImport.listCG[0].FID;
                    gridControl组柜明细.DataSource = list;
                    gridControl组柜明细.RefreshDataSource();
                }
            }

        }


        private void sr二级销区_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                FrmQueryDictionary frm = new FrmQueryDictionary("101");
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    sr二级销区.Text = frm.SelectName;
                    sr二级销区.Tag = frm.SelectID;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void searchControl1_Properties_ButtonClick_1(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FrmQueryJYDW frm = new FrmQueryJYDW();
            DialogResult d = frm.ShowDialog();
            if (d == DialogResult.OK)
            {
                searchJYDW.Text = frm.result.FNAME;
                searchJYDW.Tag = frm.result.FID;

                if (sr二级销区.Tag == null || sr二级销区.Tag.ToStr() != frm.result.FCLASSAREA2)
                {
                    SYS_SUBDICSMODEL sys = _service.GetArea2(frm.result.FCLASSAREA2);
                    sr二级销区.Text = sys.FNAME;
                    sr二级销区.Tag = sys.FID;

                }

            }
            else
            {

            }
        }
        /// 获取GridView过滤或排序后的数据集
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public System.Collections.IList GetGridViewFilteredAndSortedData(DevExpress.XtraGrid.Views.Grid.GridView view)
        {
            return view.DataController.GetAllFilteredAndSortedRows();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

            System.Collections.IList list = GetGridViewFilteredAndSortedData(gridView库存查询);
            List<string> listAutoID = new List<string>();

            Regex regID = new Regex("\"AUTOID\":(\\d+)");
            foreach (var sub in list)
            {
                if (regID.IsMatch(sub.ToStr()))
                {
                    listAutoID.Add(regID.Match(sub.ToStr()).Groups[1].Value.ToStr());
                }
            }




            var listTHD = gridControl库存查询.DataSource as v_thdModel[];
            foreach (var sub in listTHD)
            {

                sub.FCHECK = false;
            }

            foreach (var sub in listTHD)
            {
                if (listAutoID.Any(x => x == sub.AUTOID.ToStr()))
                    sub.FCHECK = checkBox1.Checked;
            }




            gridControl库存查询.DataSource = listTHD;
            gridControl库存查询.RefreshDataSource();
        }

        private void textTHD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                btn库存查询_Click(null, null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            IniHelper.WriteString(Global.IniUrl, "CONFIG", "cph", ((int)numericUpDown1.Value).ToStr());
            if (textBox1.Text != "")
            {
                string[] arr = textBox1.Text.Split(new string[] { " ", "\r\n", "、" }, StringSplitOptions.RemoveEmptyEntries);

                if (arr.Length == 1)
                {
                    txt提货人电话.Text = arr[0].Replace("\r", "").Replace("\n", "").Replace("?", "");
                }
                else if (arr.Length >= 2)
                {
                    txt提货人电话.Text = arr[0].Replace("\r", "").Replace("\n", "").Replace("?", "");
                    txt车牌号.Text = textBox1.Text.Replace(arr[0] + " ", "").Replace("\r", "").Replace("\n", "").Replace("?", "");

                    if (txt车牌号.Text != "")
                    {
                        try
                        {
                            while (txt车牌号.Text.StartsWith(" "))
                            {
                                if (txt车牌号.Text == "") break;
                                txt车牌号.Text = txt车牌号.Text.Substring(1, txt车牌号.Text.Length - 1);

                                if (txt车牌号.Text.Length > (int)numericUpDown1.Value) txt车牌号.Text = txt车牌号.Text.Substring(0, (int)numericUpDown1.Value - 1);
                            }
                        }
                        catch
                        { }
                    }
                }



            }
        }

        private void treeList销区_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            string text = treeList销区.FocusedNode.GetValue("FNAME").ToStr();
            if (!string.IsNullOrEmpty(text))
            {
                txtArea.Text = text;
                btn库存查询_Click(null, null);
            }


        }

        List<v_thdModel> listTHDV = new List<v_thdModel>();
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            listTHDV = getTHD();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<SYS_SUBDICSMODEL> marketAreaList = new List<SYS_SUBDICSMODEL>();
            if (checkBox1.Checked)
            {
                marketAreaList = _service.GetDics_Area("101", "", false).ToList();
            }
            else
            {
                marketAreaList = _service.GetDics("101", "", false).ToList();
            }


            marketAreaList.RemoveAll(x => !listTHDV.Any(y => y.fclassarea2 == x.FID));

            SYS_SUBDICSMODEL model = new SYS_SUBDICSMODEL();
            model.FID = "0";
            model.FNAME = "无匹配关系";
            marketAreaList.Add(model);

            treeList销区.DataSource = marketAreaList;

            simpleButton3.Enabled = true;
            simpleButton3.Text = "加载";
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            simpleButton3.Enabled = false;
            simpleButton3.Text = "加载中";
            backgroundWorker2.RunWorkerAsync();
        }

        private void sr二级销区_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            sr二级销区.Tag = searchLookUpEdit1.EditValue;
            sr二级销区.Text = searchLookUpEdit1.Text;
        }

        private void searchLookUpEdit1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmSGPGroupCounter_Shown(object sender, EventArgs e)
        {

        }

        private void searchLookUpEdit1_PropertiesChanged(object sender, EventArgs e)
        {

        }
    }
}
