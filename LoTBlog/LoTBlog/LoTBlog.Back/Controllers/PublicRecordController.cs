using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Back.Controllers
{
    /// <summary>
    /// 访客统计
    /// </summary>
    public class PublicRecordController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
