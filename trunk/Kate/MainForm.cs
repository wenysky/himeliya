using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Himeliya.Controls;
using Natsuhime;
using System.Net;
using System.Text.RegularExpressions;

namespace Himeliya.Kate
{
    public partial class MainForm : Form
    {
        CookieContainer cookie = null;
        Dictionary<string, string> urlList = null;
        List<string> downloadUrlList;

        public MainForm()
        {
            InitializeComponent();
            cookie = new CookieContainer();
        }

        private void btnGetLinks_Click(object sender, EventArgs e)
        {
            Httper httper = new Httper();
            httper.Url = tbxUrl.Text.Trim();
            httper.Charset = httper.GetPageLanguageCode();
            httper.Cookie = cookie;
            string returnData = httper.HttpGet();

            MatchCollection mc = RegexUtility.GetMatchFull(returnData, "<span id=\"thread_[0-9]+\"><a href=\"(.*?)\".*>(.*)</a>");
            if (mc != null)
            {
                urlList = new Dictionary<string, string>();
                foreach (Match m in mc)
                {
                    urlList.Add(m.Groups[2].Value, m.Groups[1].Value);
                }

                foreach (string key in urlList.Keys)
                {
                    tbxMessage.Text += key + "\r\n" + tbxUrl.Text.Trim().Substring(0, tbxUrl.Text.Trim().LastIndexOf('/') + 1) + urlList[key] + "\r\n\r\n";
                }
            }
            else
            {
                tbxMessage.Text = "no regexed";
                return;
            }
        }

        private void btnGetPosts_Click(object sender, EventArgs e)
        {
            /*
            Httper httper = new Httper();
            httper.Url = tbxUrl.Text.Trim();
            httper.Charset = httper.GetPageLanguageCode();
            httper.Cookie = cookie;
            string returnData = httper.HttpGet();
             */
            /*
                        MatchCollection mc = RegexUtility.GetMatchFull(returnData, "<div class=\"postinfo\">(.*)<td class=\"postauthor\">");//<div id="postmessage_[0-9]+" class="t_msgfont">(.*)</div>
                        if (mc != null)
                        {
                            urlList = new Dictionary<string, string>();
                            foreach (Match m in mc)
                            {
                                urlList.Add(m.Groups[2].Value, m.Groups[1].Value);
                            }

                            foreach (string key in urlList.Keys)
                            {
                                tbxMessage.Text += key + "\r\n" + tbxUrl.Text.Trim().Substring(0, tbxUrl.Text.Trim().LastIndexOf('/') + 1) + urlList[key] + "\r\n\r\n";
                            }
                        }
                        else
                        {
                            tbxMessage.Text = "no regexed";
                            return;
                        }
                         */
            string ImageType = "jpg|jpeg|png|ico|bmp|gif";
            List<string> result = TextAnalyze.GetUrlListByUrl(tbxUrl.Text.Trim(), @"((http(s)?://)?)+(((/?)+[\w-.]+(/))*)+[\w-./]+\.+(" + ImageType + ")");

            downloadUrlList = result;

            foreach (string link in result)
            {
                tbxMessage.Text += link + "\r\n";
            }
            tbxMessage.Text += "OK";
        }

        private void btnStartDownload_Click(object sender, EventArgs e)
        {
            tbxMessage.Text = "" + downloadUrlList.Count.ToString() + "\r\n"; ;
            DownLoader dr = new DownLoader(downloadUrlList, @"G:\temptemp\");
            dr.DownloadChanged += new EventHandler<AsyncCompletedEventArgs>(dr_DownloadChanged);
            try
            {
                dr.Download();
            }
            catch(Exception ex)
            {
                tbxMessage.Text += ex.Message + "\r\n";
            }
        }

        void dr_DownloadChanged(object sender, AsyncCompletedEventArgs e)
        {
            tbxMessage.Text += "OK - " + e.UserState.ToString() + "\r\n";
        }
    }
}
