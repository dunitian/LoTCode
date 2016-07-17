using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Back.Controllers
{
    /// <summary>
    /// 敏感记录~管理员操作记录
    /// </summary>
    public class AdminRecordController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
