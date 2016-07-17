using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Back.Controllers
{
    /// <summary>
    /// 热词热搜统计
    /// </summary>
    public class HotWordController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
