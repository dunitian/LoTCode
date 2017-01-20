using PawChina.Model;
using System.Web.Mvc;

namespace PawChina.UI.Areas.PawRoot.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前登录用户（页面初始化的时候会自动赋值）
        /// </summary>
        public ChineseInfo CurrentUser
        {
            get
            {
                try { return Session["PawChina"] as ChineseInfo; } catch (System.Exception) { return null; }
            }
        }
        /// <summary>
        /// 控制器执行前调用方法
        /// </summary>
        /// <param name="filterContext">当前请求行为上下文对象</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //LoginOn LoginOut Login 不用登录就可以访问
            if (CurrentUser == null && !filterContext.ActionDescriptor.ActionName.Contains("Login"))
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    var data = new AjaxOption<object>() { Msg = "登录超时" };
                    filterContext.Result = Json(data);
                }
                else
                {
                    //filterContext.Result = RedirectToAction("Login", "Manager");
                    //todo:加一个非本域名的判断，非本域名则跳转到主页（防跨站攻击）
                    filterContext.Result = RedirectToAction("Login", "Manager", new { returnUrl = filterContext.HttpContext.Request.Url });
                }
            }
        }
    }
}