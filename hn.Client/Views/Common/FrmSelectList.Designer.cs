namespace hn.Client.Views.Common
{
    partial class FrmSelectList<T>
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
            this.gridSelectList = new DevExpress.XtraGrid.GridControl();
            this.gridViewSelectList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridSelectList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSelectList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridSelectList
            // 
            this.gridSelectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSelectList.Location = new System.Drawing.Point(0, 0);
            this.gridSelectList.MainView = this.gridViewSelectList;
            this.gridSelectList.Name = "gridSelectList";
            this.gridSelectList.Size = new System.Drawing.Size(600, 254);
            this.gridSelectList.TabIndex = 0;
            this.gridSelectList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSelectList});
            // 
            // gridViewSelectList
            // 
            this.gridViewSelectList.GridControl = this.gridSelectList;
            this.gridViewSelectList.Name = "gridViewSelectList";
            this.gridViewSelectList.OptionsSelection.CheckBoxSelectorColumnWidth = 80;
            this.gridViewSelectList.OptionsSelection.MultiSelect = true;
            this.gridViewSelectList.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewSelectList.OptionsView.ShowGroupPanel = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnRemove);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridSelectList);
            this.splitContainer1.Size = new System.Drawing.Size(600, 298);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnRemove
            // 
            this.btnRemove.ImageOptions.Image = global::hn.Client.Properties.Resources.TitleClose20;
            this.btnRemove.Location = new System.Drawing.Point(12, 8);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 0;
            this.btnRemove.Text = "移除";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // FrmSelectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 298);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmSelectList";
            this.Text = "FrmSelectList";
            ((System.ComponentModel.ISupportInitialize)(this.gridSelectList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSelectList)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridSelectList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSelectList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
    }
}