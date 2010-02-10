using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Natsuhime.Common;
using System.IO;
using Himeliya.Kate.Entity;
using Natsuhime.Data;

namespace Himeliya.Kate
{
    public partial class ConfigForm : Form
    {
        string configPath;
        Dictionary<string, object> config = null;

        public ConfigForm()
        {
            InitializeComponent();
            this.configPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "config" + Path.DirectorySeparatorChar + "main.dat"
                );
        }

        private void btnSaveNetwork_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> config = new Dictionary<string, object>();
            config.Add("UseProxy", this.ckbxIsUserProxy.Checked);
            config.Add("ProxyAddress", this.tbxProxyAddress.Text.Trim());
            config.Add("ProxyPort", this.tbxProxyPort.Text.Trim());

            Config.SaveConfig(this.configPath, config);
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            BindProjectList();


            //this.config = Config.LoadConfig(this.configPath);
            //if (config.ContainsKey("UseProxy"))
            //{
            //    this.ckbxIsUserProxy.Checked = bool.Parse(this.config["UseProxy"].ToString());
            //}
            //if (config.ContainsKey("ProxyAddress"))
            //{
            //    this.tbxProxyAddress.Text = this.config["ProxyAddress"].ToString();
            //}
            //if (config.ContainsKey("ProxyPort"))
            //{
            //    this.tbxProxyPort.Text = this.config["ProxyPort"].ToString();
            //}


            //if (config.ContainsKey("Projects"))
            //{
            //    List<ProjectInfo> projects = this.config["Projects"] as List<ProjectInfo>;
            //    this.cbbxProjects.DataSource = projects;
            //    //this.cbbxProjects.ValueMember = "Key";
            //    this.cbbxProjects.DisplayMember = "Name";
            //}
        }

        private void BindProjectList()
        {
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM projects");
            List<ProjectInfo> projects = new List<ProjectInfo>();
            while (dr.Read())
            {
                ProjectInfo pi = new ProjectInfo();
                pi.Id = Convert.ToInt32(dr["id"]);
                pi.Name = dr["name"].ToString();
                pi.Url = dr["fetch_url"].ToString();
                pi.TotalPageCount = Convert.ToInt32(dr["total_page_count"]);
                pi.CurrentPageId = Convert.ToInt32(dr["current_page_id"]);
                pi.CurrentPostId = Convert.ToInt32(dr["current_post_id"]);

                projects.Add(pi);
            }
            dr.Close();
            this.cbbxProjects.DataSource = projects;
            this.cbbxProjects.DisplayMember = "Name";
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            ChangeInputStatus(false);
        }

        void ChangeInputStatus(bool isReadOnly)
        {
            this.tbxCurrentPostId.ReadOnly =
                this.tbxCurrentPageId.ReadOnly =
                this.tbxTotalPageCount.ReadOnly =
                this.tbxProjectName.ReadOnly =
                this.tbxUrl.ReadOnly = isReadOnly;
        }

        private void btnSaveProjectInfo_Click(object sender, EventArgs e)
        {
            if (this.ckbxEditProject.Checked)
            {
            }
            else
            {
                string sql = string.Format(
                    "INSERT INTO projects(`name`,`fetch_url`) VALUES('{0}','{1}')",
                    this.tbxProjectName.Text.Trim(),
                    this.tbxUrl.Text.Trim()
                    );
                try
                {
                    DbHelper.ExecuteNonQuery(CommandType.Text, sql);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            this.BindProjectList();

            //List<ProjectInfo> projects = this.config.ContainsKey("Projects") ? this.config["Projects"] as List<ProjectInfo> : null;

            //if (projects == null)
            //{
            //    projects = new List<ProjectInfo>();
            //}

            //ProjectInfo pi = new ProjectInfo();
            //pi.Name = this.tbxProjectName.Text.Trim();
            //projects.Add(pi);

            //if (this.config.ContainsKey("Projects"))
            //{
            //    this.config["Projects"] = projects;
            //}
            //else
            //{
            //    this.config.Add("Projects", projects);
            //}
            //Config.SaveConfig(this.configPath, config);
        }

        private void ckbxEditProject_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeInputStatus(!this.ckbxEditProject.Checked);
        }

        private void cbbxProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectInfo pi = this.cbbxProjects.SelectedItem as ProjectInfo;
            if (pi != null)
            {
                this.tbxProjectName.Text = pi.Name;
                this.tbxUrl.Text = pi.Url;
                this.tbxTotalPageCount.Text = pi.TotalPageCount.ToString();
                this.tbxCurrentPageId.Text = pi.CurrentPageId.ToString();
                this.tbxCurrentPostId.Text = pi.CurrentPostId.ToString();
            }
        }
    }
}
