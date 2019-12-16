namespace hn.Client
{
    partial class FrmOutOrderDetailed
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.splCC = new DevExpress.XtraEditors.SplitContainerControl();
            this.palList = new DevExpress.XtraEditors.PanelControl();
            this.BillList = new DevExpress.XtraGrid.GridControl();
            this.BillGrid = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.MaintoolStrip = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
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
            gridLevelNode2.RelationName = "Level1";
            this.BillList.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.BillList.Location = new System.Drawing.Point(2, 29);
            this.BillList.MainView = this.BillGrid;
            this.BillList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BillList.Name = "BillList";
            this.BillList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.BillList.Size = new System.Drawing.Size(1057, 410);
            this.BillList.TabIndex = 55;
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
            // MaintoolStrip
            // 
            this.MaintoolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MaintoolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnExit});
            this.MaintoolStrip.Location = new System.Drawing.Point(2, 2);
            this.MaintoolStrip.Name = "MaintoolStrip";
            this.MaintoolStrip.Size = new System.Drawing.Size(1057, 27);
            this.MaintoolStrip.TabIndex = 10;
            this.MaintoolStrip.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::hn.Client.Properties.Resources.btnSave;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(71, 24);
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.palDetailed.Size = new System.Drawing.Size(1061, 223);
            this.palDetailed.TabIndex = 0;
            // 
            // palEnty
            // 
            this.palEnty.Controls.Add(this.EntryList);
            this.palEnty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.palEnty.Location = new System.Drawing.Point(2, 27);
            this.palEnty.Name = "palEnty";
            this.palEnty.Size = new System.Drawing.Size(1057, 194);
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
            this.EntryList.Size = new System.Drawing.Size(1053, 190);
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
            this.dvgDetailed.Size = new System.Drawing.Size(1057, 219);
            this.dvgDetailed.TabIndex = 0;
            // 
            // FrmOutOrderDetailed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 676);
            this.Controls.Add(this.splCC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "FrmOutOrderDetailed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出库单明细";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmOutOrderList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splCC)).EndInit();
            this.splCC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.palList)).EndInit();
            this.palList.ResumeLayout(false);
            this.palList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BillList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
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
        public DevExpress.XtraGrid.GridControl BillList;
        public DevExpress.XtraGrid.Views.Grid.GridView BillGrid;
        public DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        public System.Windows.Forms.ToolStrip MaintoolStrip;
        public System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.DataGridView dgvList;
        private DevExpress.XtraEditors.PanelControl palDetailed;
        private DevExpress.XtraEditors.PanelControl palEnty;
        public DevExpress.XtraGrid.GridControl EntryList;
        public DevExpress.XtraGrid.Views.Grid.GridView EntryGrid;
        public DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DataGridView dvgDetailed;
        public System.Windows.Forms.ToolStripButton btnSave;
    }
}