using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Controllers
{
    //探索
    //学习心得、笔记、工具
    public class ShareController : Controller
    {
        /// <summary>
        /// 笔记+网络资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id = 0, int pi = 1, int ps = 9)
        {
            ViewBag.PageIndex = pi;
            ViewBag.PageSize = ps;
            return View();
        }

    }
}
