﻿namespace hn.Client
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNewQueryProduct));
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
            this.pnl跑龙套2 = new DevExpress.XtraEditors.PanelControl();
            this.btn重置 = new DevExpress.XtraEditors.SimpleButton();
            this.btn查询 = new DevExpress.XtraEditors.SimpleButton();
            this.txt关键字 = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigator总记录数 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigator每页多少记录 = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigator状态 = new System.Windows.Forms.ToolStripLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView名称代码)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl跑龙套2)).BeginInit();
            this.pnl跑龙套2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt关键字.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridControl);
            this.panel1.Controls.Add(this.pnl跑龙套2);
            this.panel1.Controls.Add(this.bindingNavigator1);
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
            this.gridControl.Location = new System.Drawing.Point(0, 38);
            this.gridControl.LookAndFeel.SkinName = "Office 2010 Silver";
            this.gridControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl.MainView = this.gridView名称代码;
            this.gridControl.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(1127, 488);
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
            this.policyname.VisibleIndex = 0;
            this.policyname.Width = 100;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "促销政策行编码id";
            this.gridColumn1.FieldName = "ITEMID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 120;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "促销政策类型";
            this.gridColumn2.FieldName = "POLICYITEMTYPE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 130;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "厂家产品代码";
            this.gridColumn3.FieldName = "PRODCODE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 100;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "厂家名称";
            this.gridColumn4.FieldName = "PRODNAME";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 130;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "厂家型号";
            this.gridColumn5.FieldName = "PRODMODEL";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "厂家规格";
            this.gridColumn6.FieldName = "PRODSTANDARD";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 80;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "产品大类";
            this.gridColumn7.FieldName = "LHPRODTYPE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 80;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "政策起始量";
            this.gridColumn8.FieldName = "MINIMUMQUANTITY";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 60;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "政策封顶量";
            this.gridColumn9.FieldNameSortGroup = "CAPPINGQUANTITY";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 9;
            this.gridColumn9.Width = 60;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "折扣率";
            this.gridColumn10.FieldName = "DISCOUNTRATE";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 10;
            this.gridColumn10.Width = 60;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "特价";
            this.gridColumn11.FieldName = "SPECIALOFFER";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 11;
            this.gridColumn11.Width = 60;
            // 
            // pnl跑龙套2
            // 
            this.pnl跑龙套2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnl跑龙套2.Appearance.Options.UseBackColor = true;
            this.pnl跑龙套2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl跑龙套2.Controls.Add(this.btn重置);
            this.pnl跑龙套2.Controls.Add(this.btn查询);
            this.pnl跑龙套2.Controls.Add(this.txt关键字);
            this.pnl跑龙套2.Controls.Add(this.label1);
            this.pnl跑龙套2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl跑龙套2.Location = new System.Drawing.Point(0, 0);
            this.pnl跑龙套2.Margin = new System.Windows.Forms.Padding(2);
            this.pnl跑龙套2.Name = "pnl跑龙套2";
            this.pnl跑龙套2.Size = new System.Drawing.Size(1127, 38);
            this.pnl跑龙套2.TabIndex = 7;
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
            this.btn重置.Location = new System.Drawing.Point(258, 8);
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
            this.btn查询.Location = new System.Drawing.Point(200, 8);
            this.btn查询.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.btn查询.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn查询.Margin = new System.Windows.Forms.Padding(2);
            this.btn查询.Name = "btn查询";
            this.btn查询.Size = new System.Drawing.Size(53, 22);
            this.btn查询.TabIndex = 53;
            this.btn查询.Text = "查询";
            this.btn查询.Click += new System.EventHandler(this.btn查询_Click);
            // 
            // txt关键字
            // 
            this.txt关键字.Location = new System.Drawing.Point(38, 8);
            this.txt关键字.Margin = new System.Windows.Forms.Padding(2);
            this.txt关键字.Name = "txt关键字";
            this.txt关键字.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.txt关键字.Properties.Appearance.Options.UseFont = true;
            this.txt关键字.Size = new System.Drawing.Size(145, 26);
            this.txt关键字.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "查询:";
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bindingNavigator1.AutoSize = false;
            this.bindingNavigator1.BackColor = System.Drawing.Color.Transparent;
            this.bindingNavigator1.CountItem = null;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigator总记录数,
            this.toolStripLabel1,
            this.bindingNavigator每页多少记录,
            this.bindingNavigator状态});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 506);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = null;
            this.bindingNavigator1.MoveNextItem = null;
            this.bindingNavigator1.MovePreviousItem = null;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = null;
            this.bindingNavigator1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.bindingNavigator1.Size = new System.Drawing.Size(1127, 20);
            this.bindingNavigator1.TabIndex = 9;
            this.bindingNavigator1.Text = "bindingNavigator1";
            this.bindingNavigator1.Visible = false;
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(24, 17);
            this.bindingNavigatorMoveFirstItem.Text = "首页";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(24, 17);
            this.bindingNavigatorMovePreviousItem.Text = "上一页";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 20);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(38, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "当前页";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(32, 17);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总页数";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 20);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(24, 17);
            this.bindingNavigatorMoveNextItem.Text = "下一页";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(24, 17);
            this.bindingNavigatorMoveLastItem.Text = "尾页";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 20);
            // 
            // bindingNavigator总记录数
            // 
            this.bindingNavigator总记录数.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bindingNavigator总记录数.Enabled = false;
            this.bindingNavigator总记录数.Name = "bindingNavigator总记录数";
            this.bindingNavigator总记录数.RightToLeftAutoMirrorImage = true;
            this.bindingNavigator总记录数.Size = new System.Drawing.Size(75, 17);
            this.bindingNavigator总记录数.Text = "共 0 条记录";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(6, 20);
            // 
            // bindingNavigator每页多少记录
            // 
            this.bindingNavigator每页多少记录.Enabled = false;
            this.bindingNavigator每页多少记录.Name = "bindingNavigator每页多少记录";
            this.bindingNavigator每页多少记录.Size = new System.Drawing.Size(83, 17);
            this.bindingNavigator每页多少记录.Text = "每页 0 条记录";
            // 
            // bindingNavigator状态
            // 
            this.bindingNavigator状态.Enabled = false;
            this.bindingNavigator状态.Name = "bindingNavigator状态";
            this.bindingNavigator状态.Size = new System.Drawing.Size(0, 17);
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
            ((System.ComponentModel.ISupportInitialize)(this.pnl跑龙套2)).EndInit();
            this.pnl跑龙套2.ResumeLayout(false);
            this.pnl跑龙套2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt关键字.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PanelControl pnl跑龙套2;
        private DevExpress.XtraEditors.SimpleButton btn重置;
        private DevExpress.XtraEditors.SimpleButton btn查询;
        private DevExpress.XtraEditors.TextEdit txt关键字;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView名称代码;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton bindingNavigator总记录数;
        private System.Windows.Forms.ToolStripSeparator toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel bindingNavigator每页多少记录;
        private System.Windows.Forms.ToolStripLabel bindingNavigator状态;
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
    }
}