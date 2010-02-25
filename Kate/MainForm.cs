using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Natsuhime.Events;
using Himeliya.Kate.EventArg;

namespace Himeliya.Kate
{
    public partial class MainForm : Form
    {
        ProjectManager pm;
        public MainForm()
        {
            InitializeComponent();
            pm = new ProjectManager();
            pm.FetchInfoChanged += new EventHandler<MessageEventArgs>(pm_FetchInfoChanged);
            pm.FetchPostsAndFilesComplted += new EventHandler<FetchPostsAndFilesCompletedEventArgs>(pm_FetchPostsAndFilesComplted);

            pm.FetchPostsProgressChanged += new EventHandler<Natsuhime.Events.ProgressChangedEventArgs>(pm_FetchPostsProgressChanged);
            pm.FetchFilesInPostProgressChanged += new EventHandler<Natsuhime.Events.ProgressChangedEventArgs>(pm_FetchFilesInPostProgressChanged);
        }

        void SendMessage(string message)
        {
            this.tbxMessage.Text += message + Environment.NewLine;
            this.tbxMessage.Focus();//让文本框获取焦点
            this.tbxMessage.Select(this.tbxMessage.TextLength, 0);//设置光标的位置到文本尾
            this.tbxMessage.ScrollToCaret();//滚动到控件光标处
        }


        void pm_FetchPostsProgressChanged(object sender, Natsuhime.Events.ProgressChangedEventArgs e)
        {
            if (e.CurrentPercent > e.TotalPercent)
            {
                SendMessage(string.Format("异常!!!!!==>{0}/{1}", e.CurrentPercent, e.TotalPercent));
            }
            else
            {
                this.pgbPageCount.Maximum = e.TotalPercent;
                this.pgbPageCount.Value = e.CurrentPercent;

                this.lblCurrentPageId.Text = e.CurrentPercent.ToString();
                this.lblTotalPageCount.Text = e.TotalPercent.ToString();
            }
        }

        void pm_FetchFilesInPostProgressChanged(object sender, Natsuhime.Events.ProgressChangedEventArgs e)
        {
            if (e.CurrentPercent > e.TotalPercent)
            {
                SendMessage(string.Format("异常!!!!!==>{0}/{1}", e.CurrentPercent, e.TotalPercent));
            }
            else
            {
                this.pgbPosts.Maximum = e.TotalPercent;
                this.pgbPosts.Value = e.CurrentPercent;

                this.lblCurrentPost.Text = e.CurrentPercent.ToString();
                this.lblTotalPost.Text = e.TotalPercent.ToString();
            }
        }

        void pm_FetchInfoChanged(object sender, MessageEventArgs e)
        {
            string message = string.Format("[{0}]{1}", e.Title, e.Message);

            SendMessage(message);
        }

        void pm_FetchPostsAndFilesComplted(object sender, FetchPostsAndFilesCompletedEventArgs e)
        {
            MessageBox.Show("全部获取完毕");

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
