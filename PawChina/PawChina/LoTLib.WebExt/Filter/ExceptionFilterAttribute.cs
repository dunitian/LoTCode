namespace PawChina.Common
{
    public class ExceptionFilterAttribute : System.Web.Mvc.HandleErrorAttribute
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //记录处理错误消息
            //LogHelper.WriteLog(filterContext.Exception.ToString());

            //页面跳转到错误页面
            filterContext.HttpContext.Response.Redirect("/Error.html", true);
        }
    }
}