using System;
using System.Collections.Generic;
using System.Text;

using Himeliya.Kate.Entity;
using Natsuhime.Data;
using System.Data;
using Himeliya.Kate.Analyze;
using System.Net;
using Natsuhime.Events;
using Himeliya.Kate.EventArg;

namespace Himeliya.Kate
{
    class ProjectManager
    {
        CookieContainer cookie = null;
        List<ProjectInfo> projects = null;
        TitleListFetcher tlf = null;
        FileListFetcher flf = null;


        internal void Start()
        {
            GetActivateProjects();
            Fetch();
        }


        void GetActivateProjects()
        {
            string sql = "SELECT * FROM projects WHERE `is_activate`=1";
            IDataReader dr = DbHelper.ExecuteReader(System.Data.CommandType.Text, sql);
            projects = new List<ProjectInfo>();

            while (dr.Read())
            {
                ProjectInfo pi = new ProjectInfo();
                pi.Id = Convert.ToInt32(dr["id"]);
                pi.Name = dr["name"].ToString();
                pi.Url = dr["fetch_url"].ToString();
                pi.Charset = dr["charset"].ToString();
                pi.TotalPageCount = Convert.ToInt32(dr["total_page_count"]);
                pi.CurrentPageId = Convert.ToInt32(dr["current_page_id"]);
                pi.CurrentPostId = Convert.ToInt32(dr["current_post_id"]);
                pi.IsActivate = Convert.ToInt32(dr["is_activate"]);

                projects.Add(pi);
            }
            dr.Close();
        }

        void Fetch()
        {
            if (projects.Count > 0)
            {
                cookie = new CookieContainer();
                FetchPosts();
            }
        }

        void FetchPosts()
        {
            this.tlf = new TitleListFetcher();
            this.tlf.FetchPostCompleted += new EventHandler<FetchTitleCompletedEventArgs>(tlf_FetchPostCompleted);
            this.tlf.Cookie = this.cookie;
            this.tlf.Charset = projects[0].Charset;
            this.tlf.Url = projects[0].Url.Replace("[*]", "1");
            this.tlf.FetchListAnsy("init");
        }

        void tlf_FetchPostCompleted(object sender, FetchTitleCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                //如果是1.说明是第一次请求,用于初始化总页数
                if (e.UserState.ToString().IndexOf("init") > -1)
                {
                    if (e.TotalPageCount > 0)
                    {
                        projects[0].TotalPageCount = e.TotalPageCount;
                        projects[0].CurrentPageId = e.TotalPageCount;
                        projects[0].PostList = new List<PostInfo>();
                    }
                    else
                    {
                        //throw new Exception("获取页数出错");
                        this.tlf.FetchListAnsy("init" + Guid.NewGuid().ToString());//重试
                        return;
                    }
                }
                else
                {
                    //添加到project对象的属性中
                    foreach (PostInfo pi in e.Posts)
                    {
                        //todo : 需要用lb表达式判断,contains是无法获得结果的
                        if (!projects[0].PostList.Contains(pi))
                        {
                            projects[0].PostList.Add(pi);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("重复Url获得 : " + pi.Url);
                        }
                    }

                    //计算下次获取页数(由于获取页数的正则问题,当前页数在最后9页左右的时候,都获取不到,特例排除吧)
                    if (e.TotalPageCount > 0 || projects[0].TotalPageCount - projects[0].CurrentPageId < 10)
                    {
                        //如果页数不变,继续递减pageid,否则再次向后翻X页重新获取
                        if (e.TotalPageCount > projects[0].TotalPageCount)
                        {
                            projects[0].CurrentPageId += e.TotalPageCount - projects[0].TotalPageCount;
                            projects[0].TotalPageCount = e.TotalPageCount;
                        }
                        else
                        {
                            projects[0].CurrentPageId--;
                        }

                    }
                    else
                    {
                        //throw new Exception("获取页数出错");
                        this.tlf.FetchListAnsy("continue" + projects[0].CurrentPageId.ToString());//重试
                        return;
                    }
                }


                //获取全部完成?
                if (projects[0].CurrentPageId == 0)
                {
                    this.FetchFiles();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("===> fetch:" + projects[0].CurrentPageId.ToString());
                    this.tlf.Url = projects[0].Url.Replace("[*]", projects[0].CurrentPageId.ToString());
                    this.tlf.FetchListAnsy("continue" + projects[0].CurrentPageId.ToString() + Guid.NewGuid().ToString());
                }
            }
            else
            {
                throw e.Error;
            }
        }


        void FetchFiles()
        {
            this.flf = new FileListFetcher();
            this.flf.FetchCompleted += new EventHandler<ReturnCompletedEventArgs>(FileList_FetchCompleted);
            this.flf.Cookie = this.cookie;
            this.flf.Charset = projects[0].Charset;
            this.flf.Url = projects[0].PostList[0].Url;
            this.flf.FetchListAnsy(projects[0].PostList[0]);
        }

        void FileList_FetchCompleted(object sender, ReturnCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                PostInfo pi = e.UserState as PostInfo;
                List<string> files = e.ReturnObject as List<string>;

                if (pi != null)
                {
                    pi.FileList = new List<string>();

                    foreach (string fileUrl in files)
                    {
                        pi.FileList.Add(fileUrl);
                    }
                }
                else
                {
                    throw new Exception("PostInfo居然为空了!");
                }


                int nextPostIndex = projects[0].PostList.IndexOf(pi) + 1;

                //是否完成
                if (nextPostIndex < projects[0].PostList.Count)
                {
                    this.flf.Url = projects[0].PostList[nextPostIndex].Url;
                    this.flf.FetchListAnsy(projects[0].PostList[nextPostIndex]);
                }
                else
                {
                }
            }
            else
            {
                throw e.Error;
            }
        }

    }
}
