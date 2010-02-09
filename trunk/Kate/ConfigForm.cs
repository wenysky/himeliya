using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Natsuhime.Common;
using System.IO;

namespace Himeliya.Kate
{
    public partial class ConfigForm : Form
    {
        string configPath;
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
            Dictionary<string, object> config = Config.LoadConfig(this.configPath);
            if (config.ContainsKey("UseProxy"))
            {
                this.ckbxIsUserProxy.Checked = bool.Parse(config["UseProxy"].ToString());
            }
            if (config.ContainsKey("ProxyAddress"))
            {
                this.tbxProxyAddress.Text = config["ProxyAddress"].ToString();
            }
            if (config.ContainsKey("ProxyPort"))
            {
                this.tbxProxyPort.Text = config["ProxyPort"].ToString();
            }
        }
    }
}
