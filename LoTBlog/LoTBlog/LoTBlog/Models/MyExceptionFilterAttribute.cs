using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Models
{
    public class MyExceptionFilterAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //记录处理错误消息
            LoT.LogSystem.LogHelper.WriteLog(filterContext.Exception.ToString());

            //页面跳转到错误页面
            filterContext.HttpContext.Response.Redirect("/Error.html");
        }
    }
}