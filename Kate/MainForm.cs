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
using Natsuhime.Text;
using Natsuhime.Web;
using Himeliya.Kate.Analyze;

namespace Himeliya.Kate
{
    public partial class MainForm : Form
    {
        List<string> downloadUrlList;

        NewHttper httper = null;

        public MainForm()
        {
            InitializeComponent();

            this.httper = new NewHttper();
            //if (true)
            //{
            //    this.httper.Proxy = WebRequest.GetSystemWebProxy() as WebProxy;
            //}
            this.httper.Cookie = new CookieContainer();
            this.httper.RequestStringCompleted += new NewHttper.RequestStringCompleteEventHandler(nhttper_RequestStringCompleted);
        }

        #region 处理过程
        void nhttper_RequestStringCompleted(object sender, RequestStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string baseUrl = tbxUrl.Text.Trim().Substring(0, tbxUrl.Text.Trim().LastIndexOf('/') + 1);

                if (e.UserState.ToString() == "GetTitleUrls")
                {
                    GetTitleUrlsComplete(e.ResponseString, baseUrl);
                }
                else if (e.UserState.ToString() == "GetImageUrls")
                {
                    GetImageUrlsComplete(e.ResponseString, baseUrl);
                }
            }
            else
            {
                MessageBox.Show(e.Error.Message);
            }
        }

        void GetImageUrlsComplete(string sourceHtml, string baseUrl)
        {
            string regexString = RegexStringLib.GetFileUrlRegexString("jpg|jpeg|png|ico|bmp|gif");
            List<string> imageUrls = TextAnalyze.GetUrlList(sourceHtml, regexString, baseUrl);

            if (imageUrls.Count > 0)
            {
                this.downloadUrlList = new List<string>();
                foreach (string url in imageUrls)
                {
                    string fullUrl = Utils.CompleteRelativeUrl(baseUrl, url);
                    this.downloadUrlList.Add(fullUrl);

                    tbxMessage.Text += string.Format("{1}{0}", Environment.NewLine, fullUrl);
                }
                MessageBox.Show(string.Format("{0} images got!", imageUrls.Count));
            }
            else
            {
                tbxMessage.Text = "no regexed";
            }
        }

        void GetTitleUrlsComplete(string sourceHtml, string baseUrl)
        {
            Dictionary<string, string> urlList = Natsuhime.Web.Plugin.Discuz.TextAnalyze.GetThreadsInBoard(sourceHtml);
            if (urlList.Count > 0)
            {
                foreach (string key in urlList.Keys)
                {
                    string fullUrl = Utils.CompleteRelativeUrl(baseUrl, key);

                    tbxMessage.Text += string.Format("{1}{0}{2}{0}{0}", Environment.NewLine, urlList[key], fullUrl);
                }
                MessageBox.Show(string.Format("{0} threads got!", urlList.Count));
            }
            else
            {
                tbxMessage.Text = "no regexed";
            }

            int pageCount = Natsuhime.Web.Plugin.Discuz.TextAnalyze.GetBoardPageCount(sourceHtml);
            if (pageCount >= 0)
            {
                MessageBox.Show(string.Format("{0} total pages got!", pageCount));
            }
            else
            {
                MessageBox.Show("no pagecount regexed");
            }
        }
        #endregion


        private void btnGetThreadLinks_Click(object sender, EventArgs e)
        {
            this.httper.Url = tbxUrl.Text.Trim();
            this.httper.Charset = httper.GetPageLanguageCode();
            this.httper.RequestStringAsync(EnumRequestMethod.GET, "GetTitleUrls");
        }

        private void btnGetPosts_Click(object sender, EventArgs e)
        {
            #region 保留的注释
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
            #endregion
            this.httper.Url = tbxUrl.Text.Trim();
            this.httper.Charset = httper.GetPageLanguageCode();
            this.httper.RequestStringAsync(EnumRequestMethod.GET, "GetImageUrls");
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
            catch (Exception ex)
            {
                tbxMessage.Text += ex.Message + "\r\n";
            }
        }

        void dr_DownloadChanged(object sender, AsyncCompletedEventArgs e)
        {
            tbxMessage.Text += "OK - " + e.UserState.ToString() + "\r\n";
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm();
            form.ShowDialog(this);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            TitleListFetcher abc = new TitleListFetcher();
            abc.FetchCompleted += new EventHandler<Natsuhime.Events.ReturnCompletedEventArgs>(abc_FetchCompleted);
            abc.Cookie = new CookieContainer();
            abc.Charset = "big5";
            abc.Url = "http://www.welovephoto.com/discuz/forumdisplay.php?fid=1";
            abc.FetchListAnsy();
        }

        void abc_FetchCompleted(object sender, Natsuhime.Events.ReturnCompletedEventArgs e)
        {
            
        }
    }
}
