using System;
using System.Collections.Generic;
using System.Text;
using Natsuhime;
using System.Net;
using Natsuhime.Web;
using Natsuhime.Events;

namespace Himeliya.Kate.Analyze
{
    class TitleListFetcher : BaseFetcher
    {
        public TitleListFetcher()
        {
            base.httper = new NewHttper();
            base.httper.RequestStringCompleted += new NewHttper.RequestStringCompleteEventHandler(httper_StringCompleted);
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
            string baseUrl = base.httper.Url.Substring(0, base.httper.Url.LastIndexOf('/') + 1);
            Dictionary<string, string> urlList = Natsuhime.Web.Plugin.Discuz.TextAnalyze.GetThreadsInBoard(
                sourceHtml,
                baseUrl
                );
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
    }
}
