using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Back.Controllers
{
    /// <summary>
    /// 站点数据搜集~文章统计
    /// </summary>
    public class CensusController : Controller
    {
        /// <summary>
        /// 文章统计
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 站点数据搜集
        /// </summary>
        /// <returns></returns>
        public ActionResult Compute()
        {
            //新增会员
            ViewBag.UserCount = 1;

            //新增笔记
            ViewBag.SCToday = 1;
            ViewBag.SCMonth = 9;
            ViewBag.SCAll = 11;

            //新增资讯
            ViewBag.Newstoday = 0;
            ViewBag.NewsMonth = 19;
            ViewBag.NewsAll = 27;

            //新增资源
            ViewBag.ArticleToday = 2;
            ViewBag.ArticleMonth = 7;
            ViewBag.ArticleAll = 9;

            return View();
        }
    }
}
