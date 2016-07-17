using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Controllers
{
    //回忆
    //个人相册，照片墙
    public class PhotoController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }

    }
}
