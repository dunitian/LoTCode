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
    public class PartialViewController : Controller
    {
        IArticleService ArticleService { get; set; }
        IArticleTypeService ArticleTypeService { get; set; }
        IFriendLinkService FriendLinkService { get; set; }
        IImgFlashService ImgFlashService { get; set; }

        #region 多说系列 ~ 一个页面引用一次ds.js就够了
        /// <summary>
        /// 多说评论控件
        /// </summary>
        /// <param name="id">文章唯一ID</param>
        /// <param name="title">文章标题</param>
        /// <returns></returns>
        public ActionResult DuoShuo(int? id, string title)
        {
            ViewBag.YouNeedId = id == null ? 0 : id;
            ViewBag.YouNeedTitle = string.IsNullOrEmpty(title) ? "逆天为什么怎么逆天呢？" : title;
            ViewBag.YouNeedUrl = Request.Url.ToString();
            return View();
        }

        /// <summary>
        /// 多说热评文章（日，周，月）~ 样式太丑暂时先不用
        /// </summary>
        /// <param name="range">daily，weekly，monthly</param>
        /// <param name="items">显示最新文章的条数</param>
        /// <returns></returns>
        public ActionResult DSHot(string range = "monthly", int items = 5)
        {
            ViewBag.YouNeedRange = range;
            ViewBag.YouNeedItems = items;
            return View();
        }

        /// <summary>
        /// 多说最新评论~ 样式太丑暂时先不用
        /// </summary>
        /// <param name="items">显示最新评论的条数</param>
        /// <param name="avatars">显示头像(1为显示)</param>
        /// <param name="time">显示时间(1为显示)</param>
        /// <param name="title">显示标题(1为显示)</param>
        /// <param name="admin">显示管理员的评论(1为显示)</param>
        /// <param name="length">每条评论最多显示的汉字数</param>
        /// <returns></returns>
        public ActionResult DSComment(int items = 5, int avatars = 1, int time = 1, int title = 0, int admin = 0, int length = 15)
        {
            ViewBag.YouNeedItems = items;
            ViewBag.YouNeedAvatars = avatars;
            ViewBag.YouNeedTime = time;
            ViewBag.YouNeedTitle = title;
            ViewBag.YouNeedAdmin = admin;
            ViewBag.YouNeedLength = length;
            return View();
        }

        /// <summary>
        /// 获取当前文章评论数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DSCommentCount(int id = 1)
        {
            ViewBag.YouNeedId = id;
            return View();
        }

        /// <summary>
        /// 获取访问获取访客头像
        /// </summary>
        /// <param name="count">头像个数</param>
        /// <returns></returns>
        public ActionResult DSPeople(int count = 24)
        {
            ViewBag.YouNeedCount = count;
            return View();
        }
        #endregion

        /// <summary>
        /// 根据ID获取子分类，当子分类为0显示顶级分类
        /// </summary>
        /// <param name="id">分类id，0表示显示顶级菜单</param>
        /// <returns></returns>
        public ActionResult NavForType(int id = 0)
        {
            if (id <= 0)
            {
                ViewBag.MenuTypes = ArticleTypeService.PageLoad(t => t.Status != StatusEnum.Delete && (t.Pid == 0 || t.Pid == null)).ToList();
            }
            else
            {
                //本分类
                ViewBag.articleTypeMe = ArticleTypeService.PageLoad(t => t.Status != StatusEnum.Delete && (t.Id == id)).FirstOrDefault();

                var articleTypes = ArticleTypeService.PageLoad(t => t.Status != StatusEnum.Delete && (t.Pid == id)).ToList();
                if (articleTypes != null && articleTypes.Count > 0)
                {
                    ViewBag.MenuTypes = articleTypes;
                }

            }

            return View();
        }

        /// <summary>
        /// 相关文章
        /// </summary>
        /// <param name="id">1资讯 2笔记 3资源</param>
        /// <param name="count">显示数目</param>
        /// <param name="articleId">文章id</param>
        /// <returns></returns>
        public ActionResult RelatedArticle(GroupEnum id = 0, int count = 6, int articleId = 0)
        {
            int tempid;
            ViewBag.ArticleList = ArticleService.PageLoad(a => (a.GroupType == id || id == 0) && a.Id != articleId && a.Status != ArticleStatusEnum.Delete, a => new { a.CreateTime, a.HitCount }, true, 1, 6, out tempid).Select(a => new Temp { Id = a.Id, Name = a.Title });
            return View();
        }

        /// <summary>
        /// 点击排行 ~ 缓存机制
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 999)]
        public ActionResult HotArticle()
        {
            int tempid;
            ViewBag.ArticleList = ArticleService.PageLoad(a => a.Status != ArticleStatusEnum.Delete, a => new { a.HitCount, a.CreateTime }, true, 1, 9, out tempid).Select(a => new Temp { Id = a.Id, Name = a.Title }).ToList();
            return View();
        }

        /// <summary>
        /// 最新文章 ~ 缓存机制
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 999)]
        public ActionResult NewArticle()
        {
            int tempid;
            ViewBag.ArticleList = ArticleService.PageLoad(a => a.Status != ArticleStatusEnum.Delete, a => new { a.CreateTime, a.HitCount }, true, 1, 9, out tempid).Select(a => new Temp { Id = a.Id, Name = a.Title }).ToList();
            return View("~/Views/PartialView/HotArticle.cshtml");
        }

        /// <summary>
        /// 网友推荐 ~ 缓存机制
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 999)]
        public ActionResult RecommendedArticle()
        {
            int tempid;
            ViewBag.ArticleList = ArticleService.PageLoad(a => a.Status != ArticleStatusEnum.Delete && a.Recommend == RecommendEnum.Net, a => new { a.CreateTime }, true, 1, 9, out tempid).Select(a => new Temp { Id = a.Id, Name = a.Title }).ToList();
            return PartialView("~/Views/PartialView/HotArticle.cshtml");
        }

        /// <summary>
        /// 编辑推荐 ~ 缓存机制
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 999)]
        public ActionResult CompilerLikeArticle()
        {
            int tempid;
            ViewBag.ArticleList = ArticleService.PageLoad(a => a.Status != ArticleStatusEnum.Delete && a.Recommend == RecommendEnum.Compiler, a => new { a.CreateTime }, true, 1, 9, out tempid).Select(a => new Temp { Id = a.Id, Name = a.Title }).ToList();
            return PartialView("~/Views/PartialView/HotArticle.cshtml");
        }

        /// <summary>
        /// 逆天吐槽 [说说] ~ 缓存机制【功能尚且没有】
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 999)]
        public ActionResult TalkingLT()
        {
            return View();
        }

        /// <summary>
        /// 基本信息 ~ 缓存机制
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 999)]
        public ActionResult BlogBaseInfo()
        {
            return View();
        }

        /// <summary>
        /// 幻灯片
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 999)]
        public ActionResult BlogSWF()
        {
            var model = ImgFlashService.PageLoad(i => true).FirstOrDefault();
            //前台的所有东西都得谨慎
            if (model == null)
            {
                model = new LoT.Model.ImgFlash() { TopTitle = "Don't complain about things you are not willing to work hard to change.", BottomTitle = "如果你不愿意去努力的改变，那么就不要去抱怨。" };
            }
            return View(model);
        }

        /// <summary>
        /// 友情链接 == 为什么不用缓存==>我每个页面的友情链接都不一样
        /// </summary>
        /// <param name="pageType">页面类型[0-显示全部]</param>
        /// <returns></returns>
        public ActionResult FriendLink(int pageType = 0)
        {
            ViewData.Model = FriendLinkService.PageLoad(f => f.Status != StatusEnum.Delete && (f.LinkType == (LinkTypeEnum)pageType || pageType == 0)).OrderBy(f => f.Sort);
            return View();
        }

        /// <summary>
        /// 版权信息
        /// </summary>
        /// <returns></returns>
        //[OutputCache(Duration = 999)]
        public ActionResult CopyRight()
        {
            return View();
        }
    }
}
