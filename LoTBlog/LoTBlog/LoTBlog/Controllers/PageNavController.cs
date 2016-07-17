using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Controllers
{
    public class PageNavController : Controller
    {
        /// <summary>
        /// 分页控件
        /// </summary>
        /// <param name="pi">pageindex</param>
        /// <param name="ps">pagesize</param>
        /// <param name="total">总共有多少条数据</param>
        /// <param name="url">当前页面url</param>
        /// <returns></returns>
        public ActionResult Show(int pi = 1, int ps = 9, int total = 9, string url = "#")
        {
            if (pi < 1)
            {
                pi = 1;
            }
            if (ps < 1)
            {
                ps = 9;
            }
            if (total <= 0)
            {
                total = 9;
            }
            ViewBag.PageIndex = pi;
            ViewBag.PageSize = ps;
            ViewBag.Total = total;
            ViewBag.Count = Convert.ToInt32(Math.Ceiling(total * 1.0 / ps));
            ViewBag.Url = url;
            return View();
        }

    }
}
