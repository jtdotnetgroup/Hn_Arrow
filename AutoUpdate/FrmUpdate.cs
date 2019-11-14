using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace AutoUpdate
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class FrmUpdate : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ColumnHeader chFileName;
		private System.Windows.Forms.ColumnHeader chVersion;
		private System.Windows.Forms.ColumnHeader chProgress;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListView lvUpdateList;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lbState;
		private System.Windows.Forms.ProgressBar pbDownFile;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
        private BackgroundWorker backgroundWorker1;
        private BackgroundWorker backgroundWorker2;
        private Label labFirst;

        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;


		public FrmUpdate()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú������κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdate));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labFirst = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvUpdateList = new System.Windows.Forms.ListView();
            this.chFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbDownFile = new System.Windows.Forms.ProgressBar();
            this.lbState = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 240);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labFirst);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.lvUpdateList);
            this.panel1.Controls.Add(this.pbDownFile);
            this.panel1.Controls.Add(this.lbState);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(120, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 240);
            this.panel1.TabIndex = 2;
            // 
            // labFirst
            // 
            this.labFirst.AutoSize = true;
            this.labFirst.Location = new System.Drawing.Point(38, 91);
            this.labFirst.Name = "labFirst";
            this.labFirst.Size = new System.Drawing.Size(131, 12);
            this.labFirst.TabIndex = 10;
            this.labFirst.Text = "�Ժ����ڼ�����...";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "����Ϊ�����ļ��б�";
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 238);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 2);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // lvUpdateList
            // 
            this.lvUpdateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFileName,
            this.chVersion,
            this.chProgress});
            this.lvUpdateList.Location = new System.Drawing.Point(3, 48);
            this.lvUpdateList.Name = "lvUpdateList";
            this.lvUpdateList.Size = new System.Drawing.Size(272, 120);
            this.lvUpdateList.TabIndex = 6;
            this.lvUpdateList.UseCompatibleStateImageBehavior = false;
            this.lvUpdateList.View = System.Windows.Forms.View.Details;
            // 
            // chFileName
            // 
            this.chFileName.Text = "�����";
            this.chFileName.Width = 100;
            // 
            // chVersion
            // 
            this.chVersion.Text = "�汾��";
            this.chVersion.Width = 50;
            // 
            // chProgress
            // 
            this.chProgress.Text = "����";
            this.chProgress.Width = 100;
            // 
            // pbDownFile
            // 
            this.pbDownFile.Location = new System.Drawing.Point(3, 207);
            this.pbDownFile.Name = "pbDownFile";
            this.pbDownFile.Size = new System.Drawing.Size(274, 17);
            this.pbDownFile.TabIndex = 5;
            // 
            // lbState
            // 
            this.lbState.Location = new System.Drawing.Point(3, 176);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(240, 16);
            this.lbState.TabIndex = 4;
            this.lbState.Text = "�������һ������ʼ�����ļ�";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(0, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 8);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(224, 264);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(80, 24);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "��һ��(&N)>";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(312, 264);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "ȡ��(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Location = new System.Drawing.Point(8, 266);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(122, 22);
            this.panel2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "��ӭ�Ժ������ע���ǵĲ�Ʒ��";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(24, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 48);
            this.label2.TabIndex = 10;
            this.label2.Text = "     ����������,�����������ڼ䱻�ر�,���\"���\"�Զ����³�����Զ���������ϵͳ��";
            // 
            // groupBox3
            // 
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(122, 2);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox2";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(0, 32);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(280, 8);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(14, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "��лʹ����������";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // FrmUpdate
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(424, 301);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "�Զ�����";
            this.Load += new System.EventHandler(this.FrmUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private string updateUrl = string.Empty;
		private string tempUpdatePath = string.Empty;
		XmlFiles updaterXmlFiles = null;
		private int availableUpdate = 0;
		bool isRun = false;
		string mainAppExe = "";

		/// <summary>
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrmUpdate());
		}

		private void FrmUpdate_Load(object sender, System.EventArgs e)
		{
			
			panel2.Visible = false;

            labFirst.Visible = true;
            btnNext.Enabled = false;
            backgroundWorker2.RunWorkerAsync();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
			Application.ExitThread();
			Application.Exit();
		}

        bool bHaveUpdate = false;
		private void btnNext_Click(object sender, System.EventArgs e)
		{
			if (availableUpdate > 0)
			{
                bHaveUpdate = false;
                listFileName.Clear();

                foreach (ListViewItem sub in lvUpdateList.Items)
                {
                    listFileName.Add(sub.Text);
                }
                btnNext.Enabled = false;
                btnNext.Text = "������...";
                pbDownFile.Value = 0;
                backgroundWorker1.RunWorkerAsync();
			}
			else
			{
                mainAppExe = updaterXmlFiles.GetNodeValue("//EntryPoint");
                btnFinish_Click(sender, e);
				//MessageBox.Show("û�п��õĸ���!","�Զ�����",MessageBoxButtons.OK,MessageBoxIcon.Information);
				//return;
			}

		}

        bool bErr = false;
		private void DownUpdateFile()
		{
			
			mainAppExe = updaterXmlFiles.GetNodeValue("//EntryPoint");
			Process [] allProcess = Process.GetProcesses();
			foreach(Process p in allProcess)
			{
				
				if (p.ProcessName.ToLower() + ".exe" == mainAppExe.ToLower() )
				{
					for(int i=0;i<p.Threads.Count;i++)
						p.Threads[i].Dispose();
					p.Kill();
					isRun = true;
					//break;
				}
			}
			WebClient wcClient = new WebClient();
			for(int i = 0;i < listFileName.Count;i++)
			{
                try
                {


                    //����
                    backgroundWorker1.ReportProgress(i * 100 / listFileName.Count);

                    string UpdateFile = listFileName[i];
                    string updateFileUrl = updateUrl+"/" + listFileName[i].Trim();
                    long fileLength = 0;

                    WebRequest webReq = WebRequest.Create(updateFileUrl);
                    WebResponse webRes = webReq.GetResponse();
                    fileLength = webRes.ContentLength;

                    //lbState.Text = "�������ظ����ļ�,���Ժ�...";


                    try
                    {
                        Stream srm = webRes.GetResponseStream();
                        StreamReader srmReader = new StreamReader(srm);
                        byte[] bufferbyte = new byte[fileLength];
                        int allByte = (int)bufferbyte.Length;
                        int startByte = 0;
                        while (fileLength > 0)
                        {
                            Application.DoEvents();
                            int downByte = srm.Read(bufferbyte, startByte, allByte);
                            if (downByte == 0) { break; };
                            startByte += downByte;
                            allByte -= downByte;
                            //pbDownFile.Value += downByte;


                            float part = (float)startByte / 1024;
                            float total = (float)bufferbyte.Length / 1024;
                            int percent = Convert.ToInt32((part / total) * 100);

                            //this.lvUpdateList.Items[i].SubItems[2].Text = percent.ToString() + "%";
                            backgroundWorker1.ReportProgress(999, new lvEntity() { text = UpdateFile, percent = percent.ToString() + "%" });
                        }




                        string tempPath = tempUpdatePath + UpdateFile;
                        CreateDirtory(tempUpdatePath);
                        FileStream fs = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(bufferbyte, 0, bufferbyte.Length);
                        srm.Close();
                        srmReader.Close();
                        fs.Close();


                    }
                    catch (WebException ex)
                    {
                        bErr = true;
                        MessageBox.Show("�����ļ�����ʧ�ܣ�" + ex.Message.ToString(), "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {

                }
			}
			
			
		}
        //����Ŀ¼

      

		private void CreateDirtory(string path)
		{
			if(!File.Exists(path))
			{
				string [] dirArray = path.Split('\\'); 
				string temp = string.Empty;
				for(int i = 0;i<dirArray.Length - 1;i++)
				{
					temp += dirArray[i].Trim() + "\\";
					if(!Directory.Exists(temp))
						Directory.CreateDirectory(temp);
				}
			}
		}

		//�����ļ�;
		public void CopyFile(string sourcePath,string objPath)
		{
//			char[] split = @"\".ToCharArray();
			if(!Directory.Exists(objPath))
			{
				Directory.CreateDirectory(objPath);
			}
			string[] files = Directory.GetFiles(sourcePath);
			for(int i=0;i<files.Length;i++)
			{
				string[] childfile = files[i].Split('\\');
				File.Copy(files[i],objPath + @"\" + childfile[childfile.Length-1],true);
			}
			string[] dirs = Directory.GetDirectories(sourcePath);
			for(int i=0;i<dirs.Length;i++)
			{
				string[] childdir = dirs[i].Split('\\');
				CopyFile(dirs[i],objPath + @"\" + childdir[childdir.Length-1]);
			}
		} 

		//�����ɸ��Ƹ����ļ���Ӧ�ó���Ŀ¼
		private void btnFinish_Click(object sender, System.EventArgs e)
		{
			
			this.Close();
			this.Dispose();
			try
			{
				CopyFile(tempUpdatePath,Directory.GetCurrentDirectory());
				System.IO.Directory.Delete(tempUpdatePath,true);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
            //if(true == this.isRun) Process.Start(mainAppExe,"fromautoupdater");
            Process[] allProcess = Process.GetProcesses();
            foreach (Process p in allProcess)
            {

                if (p.ProcessName.ToLower() + ".exe" == mainAppExe.ToLower())
                {
                    for (int i = 0; i < p.Threads.Count; i++)
                        p.Threads[i].Dispose();
                    p.Kill();
                    isRun = true;
                    //break;
                }
            }
            isRun = true;
            if (true == this.isRun) Process.Start(mainAppExe, "fromautoupdater");
        }
		
		//���»��ƴ��岿�ֿؼ�����
		private void InvalidateControl()
		{
			panel2.Location = panel1.Location;
			panel2.Size = panel1.Size;
			panel1.Visible = false;
			panel2.Visible = true;

			btnNext.Visible = false;
			btnCancel.Visible = false;
			
		}
		//�ж���Ӧ�ó����Ƿ���������
		private bool IsMainAppRun()
		{
			string mainAppExe = updaterXmlFiles.GetNodeValue("//EntryPoint");
			bool isRun = false;
			Process [] allProcess = Process.GetProcesses();
			foreach(Process p in allProcess)
			{
				
				if (p.ProcessName.ToLower() + ".exe" == mainAppExe.ToLower() )
				{
					isRun = true;
					//break;
				}
			}
			return isRun;
		}

        List<string> listFileName = new List<string>();

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {           

            DownUpdateFile();
            bHaveUpdate = true;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 999)
            {

                lvEntity lvitem = (lvEntity)e.UserState;
                foreach (ListViewItem lv in lvUpdateList.Items)
                {
                    if (lv.Text == lv.Text)
                    {
                        lv.SubItems[2].Text = lvitem.percent;
                        break;
                    }
                }
            }
            else
            {
                pbDownFile.Value = e.ProgressPercentage > 100 ? 100:e.ProgressPercentage;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InvalidateControl();
            this.Cursor = Cursors.Default;
            if (bHaveUpdate == true&&bErr==false)
            {
                mainAppExe = updaterXmlFiles.GetNodeValue("//EntryPoint");
                btnFinish_Click(null, null);
            }
            else
            {
                MessageBox.Show("����ʧ�ܣ�");
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

            string localXmlFile = Application.StartupPath + "\\UpdateList.xml";
            string serverXmlFile = string.Empty;


            try
            {
                //�ӱ��ض�ȡ���������ļ���Ϣ
                updaterXmlFiles = new XmlFiles(localXmlFile);
            }
            catch (Exception ee)
            {
                MessageBox.Show("�����ļ�����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            //��ȡ��������ַ
            updateUrl = updaterXmlFiles.GetNodeValue("//Url");

            AppUpdater appUpdater = new AppUpdater();
            appUpdater.UpdaterUrl = updateUrl + "/UpdateList.xml";

            //�����������,���ظ��������ļ�
            try
            {
                tempUpdatePath = Environment.GetEnvironmentVariable("Temp") + "\\" + "_" + updaterXmlFiles.FindNode("//Application").Attributes["applicationId"].Value + "_" + "y" + "_" + "x" + "_" + "m" + "_" + "\\";
                appUpdater.DownAutoUpdateFile(tempUpdatePath);
            }
            catch
            {
                MessageBox.Show("�����������ʧ��,������ʱ!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;

            }

            //��ȡ�����ļ��б�
            htUpdateFile = new Hashtable();

            serverXmlFile = tempUpdatePath + "\\UpdateList.xml";
            if (!File.Exists(serverXmlFile))
            {
                MessageBox.Show("���µ�ַ����");
                Application.Exit();
                return;
            }

            availableUpdate = appUpdater.CheckForUpdate(serverXmlFile, localXmlFile, out htUpdateFile);
         
        }
        Hashtable htUpdateFile = new Hashtable();
        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (availableUpdate > 0)
            {
                for (int i = 0; i < htUpdateFile.Count; i++)
                {
                    string[] fileArray = (string[])htUpdateFile[i];
                    lvUpdateList.Items.Add(new ListViewItem(fileArray));
                }

                btnNext.Enabled = true;
                labFirst.Visible = false;
            }
            else
            {
                mainAppExe = updaterXmlFiles.GetNodeValue("//EntryPoint");
                btnFinish_Click(null, null);

            }
          
        }
    }

    class lvEntity
    {
        public string text { set; get; }
        public string percent { set; get; }
    }
}