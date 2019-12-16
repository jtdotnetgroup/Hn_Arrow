namespace hn.Client
{
    partial class FrmOutOrderList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.splCC = new DevExpress.XtraEditors.SplitContainerControl();
            this.palList = new DevExpress.XtraEditors.PanelControl();
            this.BillList = new DevExpress.XtraGrid.GridControl();
            this.BillGrid = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.PInfo = new System.Windows.Forms.Panel();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.dateStart = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnSetting = new System.Windows.Forms.Button();
            this.MaintoolStrip = new System.Windows.Forms.ToolStrip();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnSyncCar = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.palDetailed = new DevExpress.XtraEditors.PanelControl();
            this.palEnty = new DevExpress.XtraEditors.PanelControl();
            this.EntryList = new DevExpress.XtraGrid.GridControl();
            this.EntryGrid = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.dvgDetailed = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splCC)).BeginInit();
            this.splCC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.palList)).BeginInit();
            this.palList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BillList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.PInfo.SuspendLayout();
            this.MaintoolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.palDetailed)).BeginInit();
            this.palDetailed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.palEnty)).BeginInit();
            this.palEnty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EntryList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntryGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgDetailed)).BeginInit();
            this.SuspendLayout();
            // 
            // splCC
            // 
            this.splCC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splCC.Horizontal = false;
            this.splCC.Location = new System.Drawing.Point(0, 0);
            this.splCC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splCC.Name = "splCC";
            this.splCC.Panel1.Controls.Add(this.palList);
            this.splCC.Panel1.Text = "panZ";
            this.splCC.Panel2.Controls.Add(this.palDetailed);
            this.splCC.Panel2.Text = "palC";
            this.splCC.Size = new System.Drawing.Size(1061, 676);
            this.splCC.SplitterPosition = 441;
            this.splCC.TabIndex = 1;
            // 
            // palList
            // 
            this.palList.AutoSize = true;
            this.palList.Controls.Add(this.BillList);
            this.palList.Controls.Add(this.toolStrip2);
            this.palList.Controls.Add(this.PInfo);
            this.palList.Controls.Add(this.MaintoolStrip);
            this.palList.Controls.Add(this.dgvList);
            this.palList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palList.Location = new System.Drawing.Point(0, 0);
            this.palList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.palList.Name = "palList";
            this.palList.Size = new System.Drawing.Size(1061, 441);
            this.palList.TabIndex = 0;
            // 
            // BillList
            // 
            this.BillList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BillList.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            gridLevelNode1.RelationName = "Level1";
            this.BillList.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.BillList.Location = new System.Drawing.Point(2, 133);
            this.BillList.MainView = this.BillGrid;
            this.BillList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BillList.Name = "BillList";
            this.BillList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.BillList.Size = new System.Drawing.Size(1057, 306);
            this.BillList.TabIndex = 57;
            this.BillList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.BillGrid});
            this.BillList.Click += new System.EventHandler(this.BillList_Click);
            this.BillList.DoubleClick += new System.EventHandler(this.BillList_DoubleClick);
            // 
            // BillGrid
            // 
            this.BillGrid.DetailHeight = 297;
            this.BillGrid.GridControl = this.BillList;
            this.BillGrid.IndicatorWidth = 27;
            this.BillGrid.Name = "BillGrid";
            this.BillGrid.OptionsBehavior.AutoSelectAllInEditor = false;
            this.BillGrid.OptionsEditForm.ShowOnDoubleClick = DevExpress.Utils.DefaultBoolean.False;
            this.BillGrid.OptionsEditForm.ShowOnEnterKey = DevExpress.Utils.DefaultBoolean.False;
            this.BillGrid.OptionsEditForm.ShowOnF2Key = DevExpress.Utils.DefaultBoolean.False;
            this.BillGrid.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.False;
            this.BillGrid.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2});
            this.toolStrip2.Location = new System.Drawing.Point(2, 108);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1057, 25);
            this.toolStrip2.TabIndex = 56;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(56, 22);
            this.toolStripLabel2.Text = "出库列表";
            // 
            // PInfo
            // 
            this.PInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PInfo.Controls.Add(this.dateEnd);
            this.PInfo.Controls.Add(this.dateStart);
            this.PInfo.Controls.Add(this.label10);
            this.PInfo.Controls.Add(this.label7);
            this.PInfo.Controls.Add(this.textBox9);
            this.PInfo.Controls.Add(this.label9);
            this.PInfo.Controls.Add(this.textBox6);
            this.PInfo.Controls.Add(this.label6);
            this.PInfo.Controls.Add(this.textBox8);
            this.PInfo.Controls.Add(this.label8);
            this.PInfo.Controls.Add(this.textBox5);
            this.PInfo.Controls.Add(this.label5);
            this.PInfo.Controls.Add(this.textBox4);
            this.PInfo.Controls.Add(this.label4);
            this.PInfo.Controls.Add(this.textBox3);
            this.PInfo.Controls.Add(this.label3);
            this.PInfo.Controls.Add(this.textBox2);
            this.PInfo.Controls.Add(this.label2);
            this.PInfo.Controls.Add(this.textBox1);
            this.PInfo.Controls.Add(this.label1);
            this.PInfo.Controls.Add(this.BtnSetting);
            this.PInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PInfo.Location = new System.Drawing.Point(2, 29);
            this.PInfo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.PInfo.Name = "PInfo";
            this.PInfo.Size = new System.Drawing.Size(1057, 79);
            this.PInfo.TabIndex = 46;
            // 
            // dateEnd
            // 
            this.dateEnd.Location = new System.Drawing.Point(251, 11);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(134, 22);
            this.dateEnd.TabIndex = 51;
            // 
            // dateStart
            // 
            this.dateStart.Location = new System.Drawing.Point(92, 11);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(134, 22);
            this.dateStart.TabIndex = 50;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(229, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 14);
            this.label10.TabIndex = 48;
            this.label10.Text = "到";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 14);
            this.label7.TabIndex = 46;
            this.label7.Text = "发货日期：";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(859, 11);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 22);
            this.textBox9.TabIndex = 45;
            this.textBox9.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(786, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 14);
            this.label9.TabIndex = 44;
            this.label9.Text = "提 货 人：";
            this.label9.Visible = false;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(474, 42);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 22);
            this.textBox6.TabIndex = 43;
            this.textBox6.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(401, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 42;
            this.label6.Text = "同步车牌：";
            this.label6.Visible = false;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(286, 42);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 22);
            this.textBox8.TabIndex = 39;
            this.textBox8.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(213, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 14);
            this.label8.TabIndex = 38;
            this.label8.Text = "客户名称：";
            this.label8.Visible = false;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(666, 11);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 22);
            this.textBox5.TabIndex = 37;
            this.textBox5.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(593, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 14);
            this.label5.TabIndex = 36;
            this.label5.Text = "发货基地：";
            this.label5.Visible = false;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(474, 11);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 22);
            this.textBox4.TabIndex = 35;
            this.textBox4.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(401, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 34;
            this.label4.Text = "事 业 部：";
            this.label4.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(859, 42);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 22);
            this.textBox3.TabIndex = 33;
            this.textBox3.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(786, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 32;
            this.label3.Text = "订单类型：";
            this.label3.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(92, 42);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 31;
            this.textBox2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 30;
            this.label2.Text = "客户编号：";
            this.label2.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(666, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 29;
            this.textBox1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(593, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 28;
            this.label1.Text = "出库单号：";
            this.label1.Visible = false;
            // 
            // BtnSetting
            // 
            this.BtnSetting.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnSetting.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.BtnSetting.FlatAppearance.BorderSize = 0;
            this.BtnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSetting.Location = new System.Drawing.Point(1033, 0);
            this.BtnSetting.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BtnSetting.Name = "BtnSetting";
            this.BtnSetting.Size = new System.Drawing.Size(22, 77);
            this.BtnSetting.TabIndex = 27;
            this.BtnSetting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSetting.UseVisualStyleBackColor = true;
            // 
            // MaintoolStrip
            // 
            this.MaintoolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MaintoolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSearch,
            this.btnSyncCar,
            this.btnExit});
            this.MaintoolStrip.Location = new System.Drawing.Point(2, 2);
            this.MaintoolStrip.Name = "MaintoolStrip";
            this.MaintoolStrip.Size = new System.Drawing.Size(1057, 27);
            this.MaintoolStrip.TabIndex = 10;
            this.MaintoolStrip.Text = "toolStrip1";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::hn.Client.Properties.Resources.btnSearch;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 24);
            this.btnSearch.Text = "查询(&F)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnSyncCar
            // 
            this.btnSyncCar.Image = global::hn.Client.Properties.Resources.TitleNormal222;
            this.btnSyncCar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSyncCar.Name = "btnSyncCar";
            this.btnSyncCar.Size = new System.Drawing.Size(95, 24);
            this.btnSyncCar.Text = "同步车牌(&T)";
            this.btnSyncCar.Click += new System.EventHandler(this.btnSyncCar_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = global::hn.Client.Properties.Resources.btnClose;
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 24);
            this.btnExit.Text = "退出(&Q）";
            this.btnExit.ToolTipText = "退出(&X)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(2, 2);
            this.dgvList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(1057, 437);
            this.dgvList.TabIndex = 0;
            // 
            // palDetailed
            // 
            this.palDetailed.AutoSize = true;
            this.palDetailed.Controls.Add(this.palEnty);
            this.palDetailed.Controls.Add(this.toolStrip1);
            this.palDetailed.Controls.Add(this.dvgDetailed);
            this.palDetailed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palDetailed.Location = new System.Drawing.Point(0, 0);
            this.palDetailed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.palDetailed.Name = "palDetailed";
            this.palDetailed.Size = new System.Drawing.Size(1061, 230);
            this.palDetailed.TabIndex = 0;
            // 
            // palEnty
            // 
            this.palEnty.Controls.Add(this.EntryList);
            this.palEnty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palEnty.Location = new System.Drawing.Point(2, 27);
            this.palEnty.Name = "palEnty";
            this.palEnty.Size = new System.Drawing.Size(1057, 201);
            this.palEnty.TabIndex = 57;
            // 
            // EntryList
            // 
            this.EntryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntryList.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.EntryList.Location = new System.Drawing.Point(2, 2);
            this.EntryList.MainView = this.EntryGrid;
            this.EntryList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.EntryList.Name = "EntryList";
            this.EntryList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.EntryList.Size = new System.Drawing.Size(1053, 197);
            this.EntryList.TabIndex = 49;
            this.EntryList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.EntryGrid});
            // 
            // EntryGrid
            // 
            this.EntryGrid.DetailHeight = 297;
            this.EntryGrid.GridControl = this.EntryList;
            this.EntryGrid.IndicatorWidth = 27;
            this.EntryGrid.Name = "EntryGrid";
            this.EntryGrid.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(2, 2);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1057, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel1.Text = "出库单明细";
            // 
            // dvgDetailed
            // 
            this.dvgDetailed.AllowUserToAddRows = false;
            this.dvgDetailed.AllowUserToDeleteRows = false;
            this.dvgDetailed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgDetailed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgDetailed.Location = new System.Drawing.Point(2, 2);
            this.dvgDetailed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dvgDetailed.Name = "dvgDetailed";
            this.dvgDetailed.ReadOnly = true;
            this.dvgDetailed.RowTemplate.Height = 23;
            this.dvgDetailed.Size = new System.Drawing.Size(1057, 226);
            this.dvgDetailed.TabIndex = 0;
            // 
            // FrmOutOrderList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 676);
            this.Controls.Add(this.splCC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmOutOrderList";
            this.Text = "出库单";
            this.Load += new System.EventHandler(this.FrmOutOrderList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splCC)).EndInit();
            this.splCC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.palList)).EndInit();
            this.palList.ResumeLayout(false);
            this.palList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BillList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.PInfo.ResumeLayout(false);
            this.PInfo.PerformLayout();
            this.MaintoolStrip.ResumeLayout(false);
            this.MaintoolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.palDetailed)).EndInit();
            this.palDetailed.ResumeLayout(false);
            this.palDetailed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.palEnty)).EndInit();
            this.palEnty.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EntryList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EntryGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgDetailed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splCC;
        private DevExpress.XtraEditors.PanelControl palList;
        public System.Windows.Forms.Panel PInfo;
        public System.Windows.Forms.Button BtnSetting;
        public System.Windows.Forms.ToolStrip MaintoolStrip;
        public System.Windows.Forms.ToolStripButton btnSyncCar;
        private System.Windows.Forms.DataGridView dgvList;
        private DevExpress.XtraEditors.PanelControl palDetailed;
        private DevExpress.XtraEditors.PanelControl palEnty;
        public DevExpress.XtraGrid.GridControl EntryList;
        public DevExpress.XtraGrid.Views.Grid.GridView EntryGrid;
        public DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DataGridView dvgDetailed;
        public DevExpress.XtraGrid.GridControl BillList;
        public DevExpress.XtraGrid.Views.Grid.GridView BillGrid;
        public DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        public System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        public System.Windows.Forms.ToolStripButton btnExit;
        public System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.DateTimePicker dateStart;
        private System.Windows.Forms.Label label10;
    }
}