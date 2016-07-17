using LoT.Enums;
using LoT.IService;
using LoT.Model;
using LoTBlog.Back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Back.Controllers
{
    /// <summary>
    /// Tag标签专栏
    /// </summary>
    public class ArticleTagController : Controller
    {
        public IArticleTagService ArticleTagService { get; set; }// 文章分类

        /// <summary>
        /// 展示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加文章Tag页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加文章Tag
        /// </summary>
        /// <param name="Name">Tag名字</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(string Name)
        {
            AjaxResponse<ArticleTag> obj = new AjaxResponse<ArticleTag>();

            if (string.IsNullOrEmpty(Name))
            {
                obj.ErrorMessage = "分类名称不能为空";
                return Json(obj);
            }

            if (Name.Length > 15)
            {
                obj.ErrorMessage = "分类名称不能超过15个字";
                return Json(obj);
            }

            //保证Name唯一，先查询一下是不是有这个Name
            var temp = ArticleTagService.PageLoad(a => a.Name == Name).FirstOrDefault();
            if (temp != null)
            {
                obj.ErrorMessage = "该分类已经存在！";
                return Json(obj);
            }

            ArticleTag ArticleTag = new ArticleTag { Name = Name, Status = StatusEnum.Normal };

            obj.IsSuccess = ArticleTagService.AddModel(ArticleTag);
            return Json(obj);
        }

        /// <summary>
        /// 批量删除文章Tag
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult DeletList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = ArticleTagService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = StatusEnum.Delete);
            AjaxResponse<ArticleTag> obj = new AjaxResponse<ArticleTag>();
            if (i > 0)
            {
                obj.IsSuccess = true;
                obj.OtherData = "成功删除" + i + "条记录";
            }
            else
            {
                obj.ErrorMessage = "操作失败！";
            }
            return Json(obj);
        }

        /// <summary>
        /// 批量恢复文章Tag
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult RecoverList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = ArticleTagService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = StatusEnum.Normal);
            AjaxResponse<ArticleTag> obj = new AjaxResponse<ArticleTag>();
            if (i > 0)
            {
                obj.IsSuccess = true;
                obj.OtherData = "成功恢复" + i + "条记录";
            }
            else
            {
                obj.ErrorMessage = "操作失败！";
            }
            return Json(obj);
        }

        /// <summary>
        /// 修改文章Tag页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int Id = 0)
        {
            ArticleTag model = ArticleTagService.FindModel(Id);
            if (model == null)//查不到就让他添加（防恶意篡改ID）
            {
                return RedirectToAction("Add");
            }
            return View(model);
        }

        /// <summary>
        /// 更新文章Tag
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(string Name, int Status, int Id = 0)
        {
            AjaxResponse<ArticleTag> obj = new AjaxResponse<ArticleTag>();

            if (string.IsNullOrEmpty(Name))
            {
                obj.ErrorMessage = "分类名称不能为空";
                return Json(obj);
            }

            if (Name.Length > 15)
            {
                obj.ErrorMessage = "分类名称不能超过15个字";
                return Json(obj);
            }

            ArticleTag ArticleTag = new ArticleTag { Id = Id, Name = Name, Status = Status != 99 ? StatusEnum.Normal : StatusEnum.Delete };

            obj.IsSuccess = ArticleTagService.UpdateModel(ArticleTag);

            return Json(obj);
        }

        /// <summary>
        /// 文章Tag查询
        /// </summary>
        /// <param name="page">当前页（1开始）</param>
        /// <param name="rows">显示条数</param>
        /// <param name="Name">分类名称（最多15个字）</param>
        /// <param name="Pid">所属分类</param>
        /// <returns></returns>
        public JsonResult Query(int page = 1, int rows = 20, string Name = "", int Status = -1)
        {
            int total = 0;
            Expression<Func<ArticleTag, bool>> whereLambada = at => (string.IsNullOrEmpty(Name) || at.Name.Contains(Name)) && (Status == -1 || at.Status == (Status == 99 ? StatusEnum.Delete : StatusEnum.Normal));

            IQueryable<ArticleTag> articleList = ArticleTagService.PageLoad(whereLambada, a => new { a.Id }, false, page, rows, out total);

            var obj = new AjaxResponse<ListData<ArticleTag>>();
            obj.IsSuccess = true;
            obj.Data = new ListData<ArticleTag>();
            obj.Data.rows = articleList;
            obj.Data.total = total;
            return Json(obj);
        }
    }
}
