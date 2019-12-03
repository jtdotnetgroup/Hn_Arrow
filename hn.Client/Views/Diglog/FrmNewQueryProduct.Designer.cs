namespace hn.Client
{
    partial class FrmNewQueryProduct
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView名称代码 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.policyname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labCount = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.pnl跑龙套2 = new DevExpress.XtraEditors.PanelControl();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.txtFactoryModel = new System.Windows.Forms.TextBox();
            this.txtSpecifications = new System.Windows.Forms.TextBox();
            this.txtFactoryName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn重置 = new DevExpress.XtraEditors.SimpleButton();
            this.btn查询 = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView名称代码)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl跑龙套2)).BeginInit();
            this.pnl跑龙套2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridControl);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pnl跑龙套2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1127, 526);
            this.panel1.TabIndex = 2;
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            gridLevelNode1.RelationName = "Level1";
            this.gridControl.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl.Location = new System.Drawing.Point(0, 78);
            this.gridControl.LookAndFeel.SkinName = "Office 2010 Silver";
            this.gridControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl.MainView = this.gridView名称代码;
            this.gridControl.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(1127, 448);
            this.gridControl.TabIndex = 10;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView名称代码});
            this.gridControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridControl_KeyDown);
            // 
            // gridView名称代码
            // 
            this.gridView名称代码.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(158)))), ((int)(((byte)(216)))));
            this.gridView名称代码.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gridView名称代码.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView名称代码.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView名称代码.Appearance.FooterPanel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.gridView名称代码.Appearance.FooterPanel.Options.UseFont = true;
            this.gridView名称代码.Appearance.GroupButton.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gridView名称代码.Appearance.GroupButton.Options.UseFont = true;
            this.gridView名称代码.Appearance.GroupFooter.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.gridView名称代码.Appearance.GroupFooter.Options.UseFont = true;
            this.gridView名称代码.Appearance.GroupPanel.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gridView名称代码.Appearance.GroupPanel.Options.UseFont = true;
            this.gridView名称代码.Appearance.HeaderPanel.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gridView名称代码.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView名称代码.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView名称代码.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView名称代码.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView名称代码.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.gridView名称代码.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridView名称代码.Appearance.Row.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.gridView名称代码.Appearance.Row.Options.UseFont = true;
            this.gridView名称代码.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(158)))), ((int)(((byte)(216)))));
            this.gridView名称代码.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView名称代码.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.policyname,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11});
            this.gridView名称代码.DetailHeight = 280;
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = "离线";
            this.gridView名称代码.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gridView名称代码.GridControl = this.gridControl;
            this.gridView名称代码.IndicatorWidth = 45;
            this.gridView名称代码.Name = "gridView名称代码";
            this.gridView名称代码.OptionsBehavior.Editable = false;
            this.gridView名称代码.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView名称代码.OptionsMenu.EnableFooterMenu = false;
            this.gridView名称代码.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView名称代码.OptionsSelection.MultiSelect = true;
            this.gridView名称代码.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView名称代码.OptionsView.ColumnAutoWidth = false;
            this.gridView名称代码.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView名称代码.OptionsView.ShowGroupPanel = false;
            this.gridView名称代码.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView名称代码_CustomDrawRowIndicator);
            this.gridView名称代码.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView名称代码_MouseDown);
            // 
            // policyname
            // 
            this.policyname.Caption = "促销政策名称";
            this.policyname.FieldName = "POLICYNAME";
            this.policyname.MinWidth = 50;
            this.policyname.Name = "policyname";
            this.policyname.Visible = true;
            this.policyname.VisibleIndex = 1;
            this.policyname.Width = 100;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "促销政策行编码id";
            this.gridColumn1.FieldName = "ITEMID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 120;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "促销政策类型";
            this.gridColumn2.FieldName = "POLICYITEMTYPE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            this.gridColumn2.Width = 130;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "厂家产品代码";
            this.gridColumn3.FieldName = "PRODCODE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 100;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "厂家名称";
            this.gridColumn4.FieldName = "PRODNAME";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 130;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "厂家型号";
            this.gridColumn5.FieldName = "PRODMODEL";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            this.gridColumn5.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "厂家规格";
            this.gridColumn6.FieldName = "PRODSTANDARD";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            this.gridColumn6.Width = 80;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "产品大类";
            this.gridColumn7.FieldName = "LHPRODTYPE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 8;
            this.gridColumn7.Width = 80;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "政策起始量";
            this.gridColumn8.FieldName = "MINIMUMQUANTITY";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 9;
            this.gridColumn8.Width = 60;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "政策封顶量";
            this.gridColumn9.FieldNameSortGroup = "CAPPINGQUANTITY";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 10;
            this.gridColumn9.Width = 60;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "折扣率";
            this.gridColumn10.FieldName = "DISCOUNTRATE";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 11;
            this.gridColumn10.Width = 60;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "特价";
            this.gridColumn11.FieldName = "SPECIALOFFER";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 12;
            this.gridColumn11.Width = 60;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.panel2.Controls.Add(this.labCount);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.btnLast);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1127, 32);
            this.panel2.TabIndex = 11;
            // 
            // labCount
            // 
            this.labCount.AutoSize = true;
            this.labCount.Location = new System.Drawing.Point(175, 11);
            this.labCount.Name = "labCount";
            this.labCount.Size = new System.Drawing.Size(119, 12);
            this.labCount.TabIndex = 1;
            this.labCount.Text = "第【0】页/共【0】页";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(94, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(13, 6);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 23);
            this.btnLast.TabIndex = 0;
            this.btnLast.Text = "上一页";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.button1_Click);
            // 
            // pnl跑龙套2
            // 
            this.pnl跑龙套2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnl跑龙套2.Appearance.Options.UseBackColor = true;
            this.pnl跑龙套2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl跑龙套2.Controls.Add(this.btnReturn);
            this.pnl跑龙套2.Controls.Add(this.btnConfirm);
            this.pnl跑龙套2.Controls.Add(this.txtFactoryModel);
            this.pnl跑龙套2.Controls.Add(this.txtSpecifications);
            this.pnl跑龙套2.Controls.Add(this.txtFactoryName);
            this.pnl跑龙套2.Controls.Add(this.label4);
            this.pnl跑龙套2.Controls.Add(this.label3);
            this.pnl跑龙套2.Controls.Add(this.btn重置);
            this.pnl跑龙套2.Controls.Add(this.btn查询);
            this.pnl跑龙套2.Controls.Add(this.label1);
            this.pnl跑龙套2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl跑龙套2.Location = new System.Drawing.Point(0, 0);
            this.pnl跑龙套2.Margin = new System.Windows.Forms.Padding(2);
            this.pnl跑龙套2.Name = "pnl跑龙套2";
            this.pnl跑龙套2.Size = new System.Drawing.Size(1127, 46);
            this.pnl跑龙套2.TabIndex = 7;
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(1040, 12);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 23);
            this.btnReturn.TabIndex = 64;
            this.btnReturn.Text = "返回";
            this.btnReturn.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(959, 14);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 63;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // txtFactoryModel
            // 
            this.txtFactoryModel.Location = new System.Drawing.Point(451, 11);
            this.txtFactoryModel.Name = "txtFactoryModel";
            this.txtFactoryModel.Size = new System.Drawing.Size(100, 22);
            this.txtFactoryModel.TabIndex = 62;
            // 
            // txtSpecifications
            // 
            this.txtSpecifications.Location = new System.Drawing.Point(272, 10);
            this.txtSpecifications.Name = "txtSpecifications";
            this.txtSpecifications.Size = new System.Drawing.Size(100, 22);
            this.txtSpecifications.TabIndex = 60;
            // 
            // txtFactoryName
            // 
            this.txtFactoryName.Location = new System.Drawing.Point(90, 10);
            this.txtFactoryName.Name = "txtFactoryName";
            this.txtFactoryName.Size = new System.Drawing.Size(100, 22);
            this.txtFactoryName.TabIndex = 59;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(199, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 57;
            this.label4.Text = "厂家规格：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(378, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 56;
            this.label3.Text = "厂家型号：";
            // 
            // btn重置
            // 
            this.btn重置.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn重置.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn重置.Appearance.Options.UseFont = true;
            this.btn重置.Appearance.Options.UseForeColor = true;
            this.btn重置.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn重置.AppearanceDisabled.Options.UseFont = true;
            this.btn重置.AppearanceHovered.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn重置.AppearanceHovered.Options.UseFont = true;
            this.btn重置.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn重置.Location = new System.Drawing.Point(614, 11);
            this.btn重置.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.btn重置.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn重置.Margin = new System.Windows.Forms.Padding(2);
            this.btn重置.Name = "btn重置";
            this.btn重置.Size = new System.Drawing.Size(53, 22);
            this.btn重置.TabIndex = 54;
            this.btn重置.Text = "重置";
            this.btn重置.Click += new System.EventHandler(this.btn重置_Click);
            // 
            // btn查询
            // 
            this.btn查询.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn查询.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn查询.Appearance.Options.UseFont = true;
            this.btn查询.Appearance.Options.UseForeColor = true;
            this.btn查询.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn查询.AppearanceDisabled.Options.UseFont = true;
            this.btn查询.AppearanceHovered.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.btn查询.AppearanceHovered.Options.UseFont = true;
            this.btn查询.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn查询.Location = new System.Drawing.Point(556, 11);
            this.btn查询.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.btn查询.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn查询.Margin = new System.Windows.Forms.Padding(2);
            this.btn查询.Name = "btn查询";
            this.btn查询.Size = new System.Drawing.Size(53, 22);
            this.btn查询.TabIndex = 53;
            this.btn查询.Text = "查询";
            this.btn查询.Click += new System.EventHandler(this.btn查询_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "厂家名称：";
            // 
            // FrmNewQueryProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 526);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FrmNewQueryProduct";
            this.ShowInTaskbar = false;
            this.Text = "选择";
            this.Load += new System.EventHandler(this.FrmQueryMarketArea_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmQueryProduct_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView名称代码)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl跑龙套2)).EndInit();
            this.pnl跑龙套2.ResumeLayout(false);
            this.pnl跑龙套2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PanelControl pnl跑龙套2;
        private DevExpress.XtraEditors.SimpleButton btn重置;
        private DevExpress.XtraEditors.SimpleButton btn查询;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView名称代码;
        private DevExpress.XtraGrid.Columns.GridColumn policyname;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSpecifications;
        private System.Windows.Forms.TextBox txtFactoryName;
        private System.Windows.Forms.TextBox txtFactoryModel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labCount;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnConfirm;
    }
}