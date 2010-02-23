using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Himeliya.Kate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                this, 
                "确定开始分析并下载么?", 
                "询问 - Kate", 
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );

            if (dr == DialogResult.Yes)
            {
                ProjectManager pm = new ProjectManager();
                pm.Start();
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm();
            form.ShowDialog(this);
        }
    }
}
