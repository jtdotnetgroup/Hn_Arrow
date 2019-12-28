namespace hn.Client.Views.Diglog
{
    partial class FrmBatchOutBoundOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBatchOutBoundOrder));
            this.txtCarNo = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.MCUSearchList = new DevExpress.XtraEditors.SearchControl();
            this.label2 = new System.Windows.Forms.Label();
            this.CarriersSelectList = new DevExpress.XtraEditors.SearchControl();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBatchNo = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCUSearchList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarriersSelectList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBatchNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCarNo
            // 
            this.txtCarNo.Location = new System.Drawing.Point(91, 10);
            this.txtCarNo.Name = "txtCarNo";
            this.txtCarNo.Size = new System.Drawing.Size(119, 20);
            this.txtCarNo.TabIndex = 0;
            this.txtCarNo.EditValueChanged += new System.EventHandler(this.txtCarNo_EditValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "车牌号：";
            // 
            // MCUSearchList
            // 
            this.MCUSearchList.Location = new System.Drawing.Point(289, 10);
            this.MCUSearchList.Name = "MCUSearchList";
            this.MCUSearchList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.MCUSearchList.Properties.Click += new System.EventHandler(this.MCUSearchList_Properties_Click);
            this.MCUSearchList.Size = new System.Drawing.Size(198, 20);
            this.MCUSearchList.TabIndex = 3;
            this.MCUSearchList.EditValueChanged += new System.EventHandler(this.MCUSearchList_EditValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "经营场所：";
            // 
            // CarriersSelectList
            // 
            this.CarriersSelectList.Location = new System.Drawing.Point(289, 46);
            this.CarriersSelectList.Name = "CarriersSelectList";
            this.CarriersSelectList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.CarriersSelectList.Properties.Click += new System.EventHandler(this.CarriersSelectList_Properties_Click);
            this.CarriersSelectList.Size = new System.Drawing.Size(198, 20);
            this.CarriersSelectList.TabIndex = 3;
            this.CarriersSelectList.EditValueChanged += new System.EventHandler(this.CarriersSelectList_EditValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "承运公司：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "分货批号：";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(91, 46);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Properties.ReadOnly = true;
            this.txtBatchNo.Size = new System.Drawing.Size(119, 20);
            this.txtBatchNo.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(135, 100);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(289, 103);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmBatchOutBoundOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 135);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CarriersSelectList);
            this.Controls.Add(this.MCUSearchList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBatchNo);
            this.Controls.Add(this.txtCarNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmBatchOutBoundOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "快速分货";
            this.Load += new System.EventHandler(this.FrmBatchOutBoundOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCarNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MCUSearchList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarriersSelectList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBatchNo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtCarNo;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchControl MCUSearchList;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SearchControl CarriersSelectList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txtBatchNo;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}