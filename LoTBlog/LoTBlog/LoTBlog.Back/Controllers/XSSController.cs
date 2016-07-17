using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Back.Controllers
{
    /// <summary>
    /// XSS 统计
    /// </summary>
    public class XSSController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
