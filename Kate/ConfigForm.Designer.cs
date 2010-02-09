namespace Himeliya.Kate
{
    partial class ConfigForm
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
            this.lblProxyAdd = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbxProxyAddress = new System.Windows.Forms.TextBox();
            this.tbxProxyPort = new System.Windows.Forms.TextBox();
            this.lblProxyPort = new System.Windows.Forms.Label();
            this.ckbxIsUserProxy = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbbxProjects = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxUrl = new System.Windows.Forms.TextBox();
            this.tbxTotalPageCount = new System.Windows.Forms.TextBox();
            this.tbxCurrentPageId = new System.Windows.Forms.TextBox();
            this.tbxCurrentId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnNewProject = new Himeliya.Controls.Button();
            this.btnSaveSelectProject = new Himeliya.Controls.Button();
            this.btnSaveNetwork = new Himeliya.Controls.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProxyAdd
            // 
            this.lblProxyAdd.AutoSize = true;
            this.lblProxyAdd.Location = new System.Drawing.Point(7, 23);
            this.lblProxyAdd.Name = "lblProxyAdd";
            this.lblProxyAdd.Size = new System.Drawing.Size(35, 12);
            this.lblProxyAdd.TabIndex = 1;
            this.lblProxyAdd.Text = "代理:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbxIsUserProxy);
            this.groupBox1.Controls.Add(this.btnSaveNetwork);
            this.groupBox1.Controls.Add(this.lblProxyPort);
            this.groupBox1.Controls.Add(this.tbxProxyPort);
            this.groupBox1.Controls.Add(this.tbxProxyAddress);
            this.groupBox1.Controls.Add(this.lblProxyAdd);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 208);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "网络";
            // 
            // tbxProxyAddress
            // 
            this.tbxProxyAddress.Location = new System.Drawing.Point(48, 20);
            this.tbxProxyAddress.Name = "tbxProxyAddress";
            this.tbxProxyAddress.Size = new System.Drawing.Size(136, 21);
            this.tbxProxyAddress.TabIndex = 2;
            // 
            // tbxProxyPort
            // 
            this.tbxProxyPort.Location = new System.Drawing.Point(241, 20);
            this.tbxProxyPort.Name = "tbxProxyPort";
            this.tbxProxyPort.Size = new System.Drawing.Size(41, 21);
            this.tbxProxyPort.TabIndex = 3;
            // 
            // lblProxyPort
            // 
            this.lblProxyPort.AutoSize = true;
            this.lblProxyPort.Location = new System.Drawing.Point(200, 23);
            this.lblProxyPort.Name = "lblProxyPort";
            this.lblProxyPort.Size = new System.Drawing.Size(35, 12);
            this.lblProxyPort.TabIndex = 4;
            this.lblProxyPort.Text = "端口:";
            // 
            // ckbxIsUserProxy
            // 
            this.ckbxIsUserProxy.AutoSize = true;
            this.ckbxIsUserProxy.Location = new System.Drawing.Point(112, 186);
            this.ckbxIsUserProxy.Name = "ckbxIsUserProxy";
            this.ckbxIsUserProxy.Size = new System.Drawing.Size(72, 16);
            this.ckbxIsUserProxy.TabIndex = 5;
            this.ckbxIsUserProxy.Text = "使用代理";
            this.ckbxIsUserProxy.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnNewProject);
            this.groupBox2.Controls.Add(this.btnSaveSelectProject);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbxCurrentId);
            this.groupBox2.Controls.Add(this.tbxCurrentPageId);
            this.groupBox2.Controls.Add(this.tbxTotalPageCount);
            this.groupBox2.Controls.Add(this.tbxUrl);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbbxProjects);
            this.groupBox2.Location = new System.Drawing.Point(306, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(242, 208);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "项目";
            // 
            // cbbxProjects
            // 
            this.cbbxProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbxProjects.FormattingEnabled = true;
            this.cbbxProjects.Location = new System.Drawing.Point(47, 20);
            this.cbbxProjects.Name = "cbbxProjects";
            this.cbbxProjects.Size = new System.Drawing.Size(189, 20);
            this.cbbxProjects.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "项目:";
            // 
            // tbxUrl
            // 
            this.tbxUrl.Location = new System.Drawing.Point(47, 71);
            this.tbxUrl.Name = "tbxUrl";
            this.tbxUrl.Size = new System.Drawing.Size(189, 21);
            this.tbxUrl.TabIndex = 6;
            // 
            // tbxTotalPageCount
            // 
            this.tbxTotalPageCount.Location = new System.Drawing.Point(47, 98);
            this.tbxTotalPageCount.Name = "tbxTotalPageCount";
            this.tbxTotalPageCount.Size = new System.Drawing.Size(189, 21);
            this.tbxTotalPageCount.TabIndex = 7;
            // 
            // tbxCurrentPageId
            // 
            this.tbxCurrentPageId.Location = new System.Drawing.Point(47, 125);
            this.tbxCurrentPageId.Name = "tbxCurrentPageId";
            this.tbxCurrentPageId.Size = new System.Drawing.Size(189, 21);
            this.tbxCurrentPageId.TabIndex = 8;
            // 
            // tbxCurrentId
            // 
            this.tbxCurrentId.Location = new System.Drawing.Point(47, 152);
            this.tbxCurrentId.Name = "tbxCurrentId";
            this.tbxCurrentId.Size = new System.Drawing.Size(189, 21);
            this.tbxCurrentId.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "地址:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "总页:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "此页:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "此id:";
            // 
            // btnNewProject
            // 
            this.btnNewProject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewProject.Location = new System.Drawing.Point(8, 181);
            this.btnNewProject.Name = "btnNewProject";
            this.btnNewProject.Size = new System.Drawing.Size(55, 23);
            this.btnNewProject.TabIndex = 17;
            this.btnNewProject.Text = "新建";
            this.btnNewProject.UseVisualStyleBackColor = true;
            // 
            // btnSaveSelectProject
            // 
            this.btnSaveSelectProject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveSelectProject.Location = new System.Drawing.Point(181, 179);
            this.btnSaveSelectProject.Name = "btnSaveSelectProject";
            this.btnSaveSelectProject.Size = new System.Drawing.Size(55, 23);
            this.btnSaveSelectProject.TabIndex = 16;
            this.btnSaveSelectProject.Text = "应用";
            this.btnSaveSelectProject.UseVisualStyleBackColor = true;
            // 
            // btnSaveNetwork
            // 
            this.btnSaveNetwork.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveNetwork.Location = new System.Drawing.Point(198, 179);
            this.btnSaveNetwork.Name = "btnSaveNetwork";
            this.btnSaveNetwork.Size = new System.Drawing.Size(84, 23);
            this.btnSaveNetwork.TabIndex = 0;
            this.btnSaveNetwork.Text = "应用";
            this.btnSaveNetwork.UseVisualStyleBackColor = true;
            this.btnSaveNetwork.Click += new System.EventHandler(this.btnSaveNetwork_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 232);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConfigForm";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Himeliya.Controls.Button btnSaveNetwork;
        private System.Windows.Forms.Label lblProxyAdd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckbxIsUserProxy;
        private System.Windows.Forms.Label lblProxyPort;
        private System.Windows.Forms.TextBox tbxProxyPort;
        private System.Windows.Forms.TextBox tbxProxyAddress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbxProjects;
        private Himeliya.Controls.Button btnNewProject;
        private Himeliya.Controls.Button btnSaveSelectProject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxCurrentId;
        private System.Windows.Forms.TextBox tbxCurrentPageId;
        private System.Windows.Forms.TextBox tbxTotalPageCount;
        private System.Windows.Forms.TextBox tbxUrl;
    }
}