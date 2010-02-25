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

        /// <summary>
        /// 开始入口
        /// </summary>
        internal void Start()
        {
            GetActivateProjects();
            Fetch();
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
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

        /// <summary>
        /// 初始化抓取
        /// </summary>
        void Fetch()
        {
            if (projects.Count > 0)
            {
                cookie = new CookieContainer();
                FetchPosts();
            }
        }

        /// <summary>
        /// 抓取主题
        /// </summary>
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
                        //获取页数出错,重试
                        this.tlf.FetchListAnsy("init" + Guid.NewGuid().ToString());
                        this.PostFetchInfoChanged(
                            "初次获取页数出错",
                            "初次获取页数出错,重试ing",
                            "",
                            null
                            );

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
                            string message = "重复Url获得 : " + pi.Url;
                            this.PostFetchInfoChanged(message, message, "", null);
                            System.Diagnostics.Debug.WriteLine(message);
                        }
                    }

                    //发送事件
                    this.PostFetchPostsProgressChanged(
                        projects[0].TotalPageCount - projects[0].CurrentPageId + 1,
                        projects[0].TotalPageCount
                        );

                    //计算下次获取页数
                    if (e.TotalPageCount > 0)
                    {
                        //如果页数不变,继续递减pageid
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
                        //获取页数出错,重试
                        this.tlf.FetchListAnsy("continue" + projects[0].CurrentPageId.ToString());
                        this.PostFetchInfoChanged(
                            "获取页数出错",
                            "获取页数出错,重试ing:" + projects[0].CurrentPageId.ToString(),
                            "",
                            null
                            );

                        return;
                    }
                }


                //获取全部完成?
                if (projects[0].CurrentPageId == 0)
                {
                    this.PostFetchInfoChanged(
                        "获取Post完成",
                        string.Format("获取{0}Post完成", projects[0].PostList.Count),
                        "",
                        null
                        );

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
                this.PostFetchPostsAndFilesComplted(this.projects, e.Error);
            }
        }

        /// <summary>
        /// 抓取主题中的文件连接
        /// </summary>
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
                    this.PostFetchPostsAndFilesComplted(
                        this.projects, 
                        new Exception("PostInfo居然为空了!")
                        );
                }

                int currentPostIndex = projects[0].PostList.IndexOf(pi);
                int nextPostIndex = currentPostIndex + 1;

                //发送进度时间
                this.PostFetchFilesInPostProgressChanged(
                    currentPostIndex + 1, 
                    projects[0].PostList.Count
                    );


                //是否完成
                if (nextPostIndex < projects[0].PostList.Count)
                {
                    this.flf.Url = projects[0].PostList[nextPostIndex].Url;
                    this.flf.FetchListAnsy(projects[0].PostList[nextPostIndex]);
                }
                else
                {
                    this.PostFetchPostsAndFilesComplted(this.projects, null);
                }
            }
            else
            {
                this.PostFetchPostsAndFilesComplted(this.projects, e.Error);
            }
        }



        #region 事件声明和触发
        void PostFetchInfoChanged(string title, string message, string extMessage, object userState)
        {
            if (this.FetchInfoChanged != null)
            {
                this.FetchInfoChanged(
                    this,
                    new MessageEventArgs(title, message, extMessage, userState)
                    );
            }
        }
        void PostFetchPostsProgressChanged(int currentPageId, int totalPageCount)
        {
            if (this.FetchPostsProgressChanged != null)
            {
                this.FetchPostsProgressChanged(
                    this,
                    new ProgressChangedEventArgs(currentPageId, totalPageCount, null)
                    );
            }
        }
        void PostFetchFilesInPostProgressChanged(int currentPostIndex, int totalPostCount)
        {
            if (this.FetchFilesInPostProgressChanged != null)
            {
                this.FetchFilesInPostProgressChanged(
                    this,
                    new ProgressChangedEventArgs(currentPostIndex, totalPostCount, null)
                    );
            }
        }
        void PostFetchPostsAndFilesComplted(List<ProjectInfo> projects, Exception e)
        {
            if (this.FetchPostsAndFilesComplted != null)
            {
                this.FetchPostsAndFilesComplted(
                    this,
                    new FetchPostsAndFilesCompletedEventArgs(projects, e, false, null)
                    );
            }
        }

        public event EventHandler<MessageEventArgs> FetchInfoChanged;
        public event EventHandler<ProgressChangedEventArgs> FetchPostsProgressChanged;
        public event EventHandler<ProgressChangedEventArgs> FetchFilesInPostProgressChanged;
        public event EventHandler<FetchPostsAndFilesCompletedEventArgs> FetchPostsAndFilesComplted;
        #endregion
    }
}
