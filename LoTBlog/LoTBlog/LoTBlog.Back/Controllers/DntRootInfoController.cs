using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoTBlog.Back.Models;
using LoT.Model;

namespace LoTBlog.Back.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class DntRootInfoController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 管理员登录（未登录状态码【ErrorCode】是38）
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            //RecordSafeAttack ~登录验证专用
            return View();
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="pwd">密  码</param>
        /// <returns></returns>
        public JsonResult LoginOn(string name, string pwd)
        {
            AjaxResponse<DntRootInfo> obj = new AjaxResponse<DntRootInfo>();
            obj.IsSuccess = true;
            return Json(obj);
        }
    }
}
