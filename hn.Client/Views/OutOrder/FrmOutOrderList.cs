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
    public partial class FrmOutOrderList : Form
    {
        #region 窗体
        public FrmOutOrderList()
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
        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {

        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {

        }

        private void btnReturnGoods_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 公共变量
        ApiService.APIServiceClient _service;
        #endregion
        /// <summary>
        /// 初始化
        /// </summary>
        void Init() {
            LaodData();
        }
        /// <summary>
        /// 填充表格
        /// </summary>
        void LaodData() {
            //_service.
        }
    }
}
