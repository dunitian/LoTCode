using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Controllers
{
    //时代
    //行业资讯（专业+安全）
    public class NewsInfoController : Controller
    {
        /// <summary>
        /// 行业资讯（专业+安全）
        /// </summary>
        /// <param name="pi">pageindex</param>
        /// <param name="ps">pagesize</param>
        /// <returns></returns>
        public ActionResult Index(int pi = 1, int ps = 9)
        {
            ViewBag.PageIndex = pi;
            ViewBag.PageSize = ps;
            return View();
        }

    }
}
