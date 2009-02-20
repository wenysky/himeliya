using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Natsuhime;

namespace Himeliya.Kate
{
    class TextAnalyze
    {
        /// <summary>
        /// 分析文本,返回匹配列表
        /// </summary>
        /// <param name="sourcetext">源文本</param>
        /// <param name="regexstring">正则表达式</param>
        /// <returns>文本列表</returns>
        public static List<string> GetList(string sourcetext, string regexstring)
        {
            List<string> list = new List<string>();
            Match match = Regex.Match(sourcetext, regexstring, RegexOptions.IgnoreCase);

            while (match.Success)
            {
                string item = match.Value;
                ////短路径处理
                //if (item.IndexOf("http://") == -1 && item.IndexOf("https://") == -1)
                //    item = (item[0] == '/' ? domainName : dnDir) + item;
                //过滤掉完全一样
                if (!list.Contains(item))
                    list.Add(item);
                match = match.NextMatch();
            }
            return list;
        }


        public static List<string> GetUrlList(string sourcetext, string regexstring, string domain)
        {
            List<string> list = new List<string>();
            Match match = Regex.Match(sourcetext, regexstring, RegexOptions.IgnoreCase);

            while (match.Success)
            {
                string item = match.Value;
                ////短路径处理
                //if (item.IndexOf("http://") == -1 && item.IndexOf("https://") == -1)
                //    item = (item[0] == '/' ? domainName : dnDir) + item;
                if (item.IndexOf("http://") == -1 && item.IndexOf("https://") == -1)
                {
                    if (item[0] == '.')
                    {
                        item = domain + item.TrimStart('.').TrimStart('/');
                    }
                    if (item[0] == '/')
                    {
                        item = domain + item.TrimStart('/');
                    }

                    //www.asdfsadf.com/1.jpg可以跳过,images/1.jpg需要添加
                    if (item.Split('.').Length < 3)
                    {
                        item = domain + item;
                    }
                }
                //过滤掉完全一样
                if (!list.Contains(item))
                    list.Add(item);
                match = match.NextMatch();
            }
            return list;
        }
        public static List<string> GetUrlListByUrl(string url, string regexstring)
        {
            Httper httper = new Httper();
            httper.Url = url;
            httper.Charset = httper.GetPageLanguageCode();

            return GetUrlList(httper.HttpGet(), regexstring, url.Substring(0, url.LastIndexOf('/') + 1));
        }
    }
}
