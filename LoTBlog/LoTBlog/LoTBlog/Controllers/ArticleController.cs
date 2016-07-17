using LoT.Enums;
using LoT.Safe;
using LoTBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Controllers
{
    public class ArticleController : Controller
    {
        LoT.IService.IArticleService ArticleService { get; set; }
        LoT.IService.IArticleTypeService ArticleTypeService { get; set; }
        LoT.IService.IArticleTagService ArticleTagService { get; set; }

        #region 封装：分类
        /// <summary>
        /// 获取文章分类
        /// </summary>
        /// <param name="typeIds"></param>
        /// <returns></returns>
        private IList<LoT.Model.ArticleType> GetBottomArticleType(string typeIds = "")
        {
            IList<LoT.Model.ArticleType> articleList = new List<LoT.Model.ArticleType>();

            int[] ids = typeIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray();

            if (ids.Length == 0)
            {
                return articleList;
            }

            int index = ids[ids.Length - 1];

            //分类一
            var articleTypeOne = ArticleTypeService.FindModel(index);

            //分类不存在就滚蛋
            if (articleTypeOne == null || articleTypeOne.Status == StatusEnum.Delete)
            {
                return articleList;
            }
            articleList.Add(articleTypeOne);

            //没二级就滚蛋
            if (articleTypeOne.ParentType == null || articleTypeOne.ParentType.Status == StatusEnum.Delete)
            {
                return articleList;
            }

            //分类二
            var articleTypeTwo = articleTypeOne.ParentType;
            articleList.Add(articleTypeTwo);

            //没三级就滚蛋
            if (articleTypeTwo.ParentType == null || articleTypeTwo.ParentType.Status == StatusEnum.Delete)
            {
                return articleList;
            }

            //分类三
            articleList.Add(articleTypeTwo.ParentType);

            return articleList;
        }
        #endregion

        #region 检测分页是否正常
        /// <summary>
        /// 检测分页是否正常
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="ps"></param>
        private void CheckOutPages(ref int pi, ref int ps)
        {
            if (pi < 1)
            {
                pi = 1;
            }
            if (ps < 1)
            {
                ps = 9;
            }
        }
        #endregion

        /// <summary>
        /// 首页逆天推荐 ~ 带分页功能
        /// </summary>
        /// <param name="pi">index</param>
        /// <param name="ps">pageSize</param>
        /// <returns></returns>
        public ActionResult LTLikeArticle(int pi = 1, int ps = 9)
        {
            CheckOutPages(ref pi, ref ps);
            //加入a.Recommend == RecommendEnum.Top 置顶功能，a.Recommend 排序优先，top的是4最大
            int total;
            ViewBag.ArticleList = ArticleService.PageLoad(a => a.Status != ArticleStatusEnum.Delete && (a.Recommend == RecommendEnum.Dnt || a.Recommend == RecommendEnum.Top), a => new { a.Recommend, a.CreateTime, a.Sort }, true, pi, ps, out total).AsEnumerable().Select(a => new ArticleTemp { Id = a.Id, Name = a.Author, Count = a.HitCount, CreateTime = a.CreateTime.ToString("yyyy-MM-dd"), DisplayPic = a.DisplayPic, Title = a.Title, Author = a.Author, TContent = HtmlSafeHelper.GetChinese(HttpUtility.HtmlDecode(a.TContent)), ArticleTypeList = GetBottomArticleType(a.TypeIds) }).ToList();
            ViewBag.PageIndex = pi;
            ViewBag.PageSize = ps;
            ViewBag.Total = total;
            ViewBag.PageUrl = Url.Action("Index", "Index");
            return View();
        }



        /// <summary>
        /// 行业资讯页面 ~ 带分页功能
        /// </summary>
        /// <param name="pi">index</param>
        /// <param name="ps">pageSize</param>
        /// <returns></returns>
        public ActionResult NewsArticle(int pi = 1, int ps = 9)
        {
            CheckOutPages(ref pi, ref ps);

            int total;
            ViewBag.ArticleList = ArticleService.PageLoad(a => a.Status != ArticleStatusEnum.Delete && a.GroupType == GroupEnum.News, a => new { a.CreateTime, a.Sort }, true, pi, ps, out total).AsEnumerable().Select(a => new ArticleTemp { Id = a.Id, Name = a.Author, Count = a.HitCount, CreateTime = a.CreateTime.ToString("yyyy-MM-dd"), DisplayPic = a.DisplayPic, Title = a.Title, Author = a.Author, TContent = HtmlSafeHelper.GetChinese(HttpUtility.HtmlDecode(a.TContent)), ArticleTypeList = GetBottomArticleType(a.TypeIds) }).ToList();
            ViewBag.PageIndex = pi;
            ViewBag.PageSize = ps;
            ViewBag.Total = total;
            ViewBag.PageUrl = Url.Action("Index", "NewsInfo");
            return View("~/Views/Article/LTLikeArticle.cshtml");
        }

        /// <summary>
        /// 网络资源页面 ~ 带分页功能
        /// </summary>
        /// <param name="pi">index</param>
        /// <param name="ps">pageSize</param>
        /// <returns></returns>
        public ActionResult ResourceArticle(int pi = 1, int ps = 9)
        {
            CheckOutPages(ref pi, ref ps);

            int total;
            ViewBag.ArticleList = ArticleService.PageLoad(a => a.Status != ArticleStatusEnum.Delete && a.GroupType == GroupEnum.Net, a => new { a.Sort, a.CreateTime }, true, pi, ps, out total).AsEnumerable().Select(a => new ArticleTemp { Id = a.Id, Name = a.Author, Count = a.HitCount, CreateTime = a.CreateTime.ToString("yyyy-MM-dd"), DisplayPic = a.DisplayPic, Title = a.Title, Author = a.Author, TContent = HtmlSafeHelper.GetChinese(HttpUtility.HtmlDecode(a.TContent)), ArticleTypeList = GetBottomArticleType(a.TypeIds) }).ToList();
            ViewBag.PageIndex = pi;
            ViewBag.PageSize = ps;
            ViewBag.Total = total;
            ViewBag.PageUrl = Url.Action("Index", "Resource");
            return View("~/Views/Article/NotePadArticle.cshtml");
        }

        /// <summary>
        /// 逆天笔记页面 ~ 带分页功能
        /// </summary>
        /// <param name="pi">index</param>
        /// <param name="ps">pageSize</param>
        /// <returns></returns>
        public ActionResult NotePadArticle(int pi = 1, int ps = 9)
        {
            CheckOutPages(ref pi, ref ps);

            int total;
            ViewBag.ArticleList = ArticleService.PageLoad(a => a.Status != ArticleStatusEnum.Delete && a.GroupType == GroupEnum.Note, a => new { a.UpdateTime, a.Sort }, true, pi, ps, out total).AsEnumerable().Select(a => new ArticleTemp { Id = a.Id, Name = a.Author, Count = a.HitCount, CreateTime = a.CreateTime.ToString("yyyy-MM-dd"), DisplayPic = a.DisplayPic, Title = a.Title, Author = a.Author, TContent = HtmlSafeHelper.GetChinese(HttpUtility.HtmlDecode(a.TContent)), ArticleTypeList = GetBottomArticleType(a.TypeIds) }).ToList();
            ViewBag.PageIndex = pi;
            ViewBag.PageSize = ps;
            ViewBag.Total = total;
            ViewBag.PageUrl = Url.Action("Index", "Share");
            return View();
        }

        /// <summary>
        /// 文章详细页
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(int id = 0)
        {
            //前台传的所有数据都得好好过滤[后台敏感的东西适当过滤]
            if (id <= 0)
            {
                return RedirectToAction("Index", "Index");
            }

            LoT.Model.Article model = ArticleService.FindModel(id);

            //不存在或者已经被删掉的文章不显示
            if (model == null || model.Status == ArticleStatusEnum.Delete)
            {
                return RedirectToAction("Index", "Index");
            }

            #region 导航部分 首页 > NavTypeName > 详细页
            switch (model.GroupType)
            {
                case LoT.Enums.GroupEnum.News:
                    ViewBag.NavTypeName = "资讯";
                    ViewBag.NavTypeUrl = Url.Action("Index", "NewsInfo");
                    break;
                case LoT.Enums.GroupEnum.Note:
                    ViewBag.NavTypeName = "笔记";
                    ViewBag.NavTypeUrl = Url.Action("Index", "Share");
                    break;
                case LoT.Enums.GroupEnum.Net:
                    ViewBag.NavTypeName = "资源";
                    ViewBag.NavTypeUrl = Url.Action("Index", "Resource");
                    break;
                default:
                    ViewBag.NavTypeName = "文章";
                    ViewBag.NavTypeUrl = "#";
                    break;
            }
            #endregion

            #region 上一篇下一篇 根据GroupType来取紧挨着的2篇文章
            var articleOne = ArticleService.PageLoad(a => a.Id != model.Id && a.Id < model.Id && a.GroupType == model.GroupType && a.Status != ArticleStatusEnum.Delete).OrderByDescending(a => a.Id).FirstOrDefault();

            var articleTwo = ArticleService.PageLoad(a => a.Id != model.Id && a.Id > model.Id && a.GroupType == model.GroupType && a.Status != ArticleStatusEnum.Delete).OrderBy(a => a.Id).FirstOrDefault();

            if (articleOne != null)
            {
                ViewBag.ArticleLast = new Temp() { Id = articleOne.Id, Name = articleOne.Title };
            }
            if (articleTwo != null)
            {
                ViewBag.ArticleNext = new Temp() { Id = articleTwo.Id, Name = articleTwo.Title };
            }
            #endregion

            #region Tag显示
            IList<int> tagIds = model.TagIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => int.Parse(t)).ToList();
            ViewBag.ArticleTags = ArticleTagService.PageLoad(t => tagIds.Contains(t.Id) && t.Status != StatusEnum.Delete);
            #endregion

            ViewBag.ArticleTypeList = GetBottomArticleType(model.TypeIds);//获取分类List集合

            model.HitCount += 1;
            try
            {
                ArticleService.UpdateModel(model);
            }
            catch (Exception e)
            {
                LoT.LogSystem.LogHelper.WriteLog(string.Format("文章ID{0}浏览量+1的时侯报错", model.Id) + e);
            }

            return View(model);
        }


        /// <summary>
        /// 显示分类对应的文章
        /// </summary>
        /// <param name="id">分类ID，0为全部显示</param>
        /// <param name="pi">index</param>
        /// <param name="ps">pageSize</param>
        /// <returns></returns>
        public ActionResult Type(string id = "", int pi = 1, int ps = 9)
        {
            #region 检验id
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Index");
            }

            int navTypeId;
            int.TryParse(id, out navTypeId);
            if (navTypeId <= 0)
            {
                return RedirectToAction("Index", "Index");
            }
            #endregion
            ViewBag.NavTypeId = navTypeId;

            CheckOutPages(ref pi, ref ps);

            string typeId = string.Format(",{0},", id);//折中的方法

            int total;
            ViewBag.ArticleList = ArticleService.PageLoad(a => a.Status != ArticleStatusEnum.Delete && ("," + a.TypeIds + ",").Contains(typeId), a => new { a.UpdateTime }, true, pi, ps, out total).AsEnumerable().Select(a => new ArticleTemp { Id = a.Id, Name = a.Author, Count = a.HitCount, CreateTime = a.CreateTime.ToString("yyyy-MM-dd"), DisplayPic = a.DisplayPic, Title = a.Title, Author = a.Author, TContent = HtmlSafeHelper.GetChinese(HttpUtility.HtmlDecode(a.TContent)), ArticleTypeList = GetBottomArticleType(a.TypeIds) }).ToList();
            ViewBag.PageIndex = pi;
            ViewBag.PageSize = ps;
            ViewBag.Total = total;
            ViewBag.PageUrl = Url.Action("Type", "Article");
            return View();
        }


        /// <summary>
        /// 显示Tag对应的文章
        /// </summary>
        /// <param name="id">ID为tag的id，0为全部显示</param>
        /// <param name="pi">index</param>
        /// <param name="ps">pageSize</param>
        /// <returns></returns>
        public ActionResult Tag(string id = "", int pi = 1, int ps = 9)
        {
            #region 检验id
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Index");
            }

            int tagTypeId;
            int.TryParse(id, out tagTypeId);
            if (tagTypeId <= 0)
            {
                return RedirectToAction("Index", "Index");
            }
            #endregion

            CheckOutPages(ref pi, ref ps);

            string tagIds = string.Format(",{0},", id);//折中的方法

            int total;
            ViewBag.ArticleList = ArticleService.PageLoad(a => a.Status != ArticleStatusEnum.Delete && ("," + a.TagIds + ",").Contains(tagIds), a => new { a.UpdateTime }, true, pi, ps, out total).AsEnumerable().Select(a => new ArticleTemp { Id = a.Id, Name = a.Author, Count = a.HitCount, CreateTime = a.CreateTime.ToString("yyyy-MM-dd"), DisplayPic = a.DisplayPic, Title = a.Title, Author = a.Author, TContent = HtmlSafeHelper.GetChinese(HttpUtility.HtmlDecode(a.TContent)), ArticleTypeList = GetBottomArticleType(a.TypeIds) }).ToList();
            ViewBag.PageIndex = pi;
            ViewBag.PageSize = ps;
            ViewBag.Total = total;
            ViewBag.PageUrl = Url.Action("Tag", "Article");
            return View();
        }

    }
}
