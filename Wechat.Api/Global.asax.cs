﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Wechat.Api.Filters;
using Wechat.Api.Helper;
using Wechat.Util;
using Wechat.Util.TaskServer;

namespace Wechat.Api
{
    /// <summary>
    /// 启动文件
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        protected void Application_Start()
        {
 
            //webapi 配置
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //注册错误异常
            GlobalConfiguration.Configuration.Filters.Add(new ExceptionAttribute());
            //注册参数验证
            GlobalConfiguration.Configuration.Filters.Add(new ValidParameterAttribute());
            //注册认证
            //GlobalConfiguration.Configuration.Filters.Add(new AuthenticationAttribute());

            //上传文件服务
            //QueueHelper<UploadFileObj>.Register(UploadFileAction.UploadFile);
            //QueueHelper<UploadFileObj>.Start();

            QuartzHelper.CreateScheduler().GetAwaiter().GetResult();
            QuartzHelper.RegisterJob<SyncMessageJob>("/5 * * ? * *", "wechat").GetAwaiter().GetResult();

        }
    }
}
