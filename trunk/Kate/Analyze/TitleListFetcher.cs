using System;
using System.Collections.Generic;
using System.Text;
using Natsuhime;
using System.Net;
using Natsuhime.Web;
using Natsuhime.Events;

namespace Himeliya.Kate.Analyze
{
    class TitleListFetcher
    {
        NewHttper httper = null;
        public CookieContainer Cookie
        {
            get
            {
                return this.httper.Cookie;
            }
            set
            {
                this.httper.Cookie = value;
            }
        }
        public WebProxy WebProxy
        {
            get
            {
                return this.httper.Proxy;
            }
            set
            {
                this.httper.Proxy = value;
            }
        }
        public string Url
        {
            get
            {
                return this.httper.Url;
            }
            set
            {
                this.httper.Url = value;
            }
        }
        public string Charset
        {
            get
            {
                return this.httper.Charset;
            }
            set
            {
                this.httper.Charset = value;
            }
        }


        public TitleListFetcher()
        {
            this.httper = new NewHttper();
            this.httper.RequestStringCompleted += new NewHttper.RequestStringCompleteEventHandler(httper_StringCompleted);
        }

        void httper_StringCompleted(object sender, RequestStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                this.GetTitleUrlsComplete(e.ResponseString, e.UserState, e.Cancelled);
            }
            else
            {
                OnCompleted(new ReturnCompletedEventArgs(null, e.Error, e.Cancelled, e.UserState));
            }
        }

        void GetTitleUrlsComplete(string sourceHtml, object userstate, bool cancelled)
        {
            string baseUrl = this.httper.Url.Substring(0, this.httper.Url.LastIndexOf('/') + 1);
            Dictionary<string, string> urlList = Natsuhime.Web.Plugin.Discuz.TextAnalyze.GetThreadsInBoard(sourceHtml);
            OnCompleted(new ReturnCompletedEventArgs(urlList, null, cancelled, userstate));
            return;

            if (urlList.Count > 0)
            {
                foreach (string key in urlList.Keys)
                {
                    string fullUrl = Utils.CompleteRelativeUrl(baseUrl, key);

                    //tbxMessage.Text += string.Format("{1}{0}{2}{0}{0}", Environment.NewLine, urlList[key], fullUrl);
                }
                //MessageBox.Show(string.Format("{0} threads got!", urlList.Count));
            }
            else
            {
                //tbxMessage.Text = "no regexed";
            }

            int pageCount = Natsuhime.Web.Plugin.Discuz.TextAnalyze.GetBoardPageCount(sourceHtml);
            if (pageCount >= 0)
            {
                //MessageBox.Show(string.Format("{0} total pages got!", pageCount));
            }
            else
            {
                //MessageBox.Show("no pagecount regexed");
            }
        }

        public void FetchListAnsy()
        {
            this.httper.RequestStringAsync(EnumRequestMethod.GET);
        }



        void OnCompleted(ReturnCompletedEventArgs e)
        {
            if (this.FetchCompleted != null)
            {
                this.FetchCompleted(this, e);
            }
        }
        public event EventHandler<ReturnCompletedEventArgs> FetchCompleted;
    }
}
