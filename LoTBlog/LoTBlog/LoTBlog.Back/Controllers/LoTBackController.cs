using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Back.Controllers
{
    public class LoTBackController : Controller
    {
        /// <summary>
        /// 后台主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "欢迎进入 LoTBlog 后台";
            ViewBag.ManagerName = "逆天";
            return View("~/Views/Shared/BackIndex.cshtml");
        }
    }
}
