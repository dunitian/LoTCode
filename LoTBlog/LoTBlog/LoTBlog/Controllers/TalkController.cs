using LoT.Enums;
using LoT.IService;
using LoTBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Controllers
{
    //点滴
    //居家、闲言碎语，生活感触
    public class TalkController : Controller
    {
        ITalkingService TalkingService { get; set; }

        /// <summary>
        /// 点滴
        /// </summary>
        /// <param name="pi">pageindex</param>
        /// <param name="ps">pagesize</param>
        /// <returns></returns>
        public ActionResult Index(int pi = 1, int ps = 9)
        {
            #region 检测分页
            if (pi < 1)
            {
                pi = 1;
            }
            if (ps < 1)
            {
                ps = 9;
            } 
            #endregion

            int total;
            ViewBag.TalkingList = TalkingService.PageLoad(a => a.Status != ArticleStatusEnum.Delete, a => new { a.UpdateTime }, true, pi, ps, out total).AsEnumerable().Select(a => new TalkingTemp { Id = a.Id, Title = string.IsNullOrEmpty(a.Title) ? "生活" : a.Title, Say = a.Say, HitCount = a.HitCount, CreateTime = a.CreateTime.ToString("yyyy-MM-dd"), DisplayPic = a.DisplayPic }).ToList();
            ViewBag.PageIndex = pi;
            ViewBag.PageSize = ps;
            ViewBag.Total = total;
            ViewBag.PageUrl = Url.Action("Index", "Talk");
            return View();
        }

        /// <summary>
        /// 根据id获取内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetSayById(int id = 0)
        {
            if (id <= 0)
            {
                return "false";
            }
            var model = TalkingService.FindModel(id);
            if (model == null)
            {
                return "false";
            }

            //更新浏览次数
            model.HitCount += 1;
            try
            {
                TalkingService.UpdateModel(model);
            }
            catch (Exception e)
            {
                LoT.LogSystem.LogHelper.WriteLog(string.Format("说说ID{0}浏览量+1的时侯报错", model.Id) + e);
            }

            return model.Say;
        }
    }
}
