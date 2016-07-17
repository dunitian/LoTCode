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
    /// 文章说说的默认展图+访客默认（头像）管理
    /// </summary>
    public class DisPhotoController : Controller
    {
        IArticleDisPhotoService ArticleDisPhotoService { get; set; }
        IPeopleDisPhotoService PeopleDisPhotoService { get; set; }

        /// <summary>
        /// 文章说说的默认展图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 访客默认（头像）管理 ~ 目前只是个摆设
        /// </summary>
        /// <returns></returns>
        public ActionResult Peoples()
        {
            return View();
        }

        /// <summary>
        /// 添加展图
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加展图
        /// </summary>
        /// <param name="Title">标题名</param>
        /// <param name="PicUrl">图片路径</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(string Title, string PicUrl)
        {
            AjaxResponse<ArticleDisPhoto> obj = new AjaxResponse<ArticleDisPhoto>();

            if (string.IsNullOrEmpty(Title))
            {
                obj.ErrorMessage = "展图名称不能为空";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(PicUrl))
            {
                obj.ErrorMessage = "展图路径不能为空";
                return Json(obj);
            }
            if (Title.Length > 15)
            {
                obj.ErrorMessage = "展图名称不能超过15个字";
                return Json(obj);
            }

            ArticleDisPhoto ArticleDisPhoto = new ArticleDisPhoto { Title = Title, PicUrl = PicUrl, Status = StatusEnum.Normal };

            obj.IsSuccess = ArticleDisPhotoService.AddModel(ArticleDisPhoto);
            return Json(obj);
        }

        /// <summary>
        /// 修改默认展图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int Id = 0)
        {
            ArticleDisPhoto model = ArticleDisPhotoService.FindModel(Id);
            if (model == null)//查不到就让他添加（防恶意篡改ID）
            {
                return RedirectToAction("Add");
            }
            return View(model);
        }

        /// <summary>
        /// 更新默认展图
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Title"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(int Status, string Title, string PicUrl, int Id = 0)
        {
            AjaxResponse<ArticleDisPhoto> obj = new AjaxResponse<ArticleDisPhoto>();

            if (string.IsNullOrEmpty(Title))
            {
                obj.ErrorMessage = "展图名称不能为空";
                return Json(obj);
            }
            if (string.IsNullOrEmpty(PicUrl))
            {
                obj.ErrorMessage = "展图路径不能为空";
                return Json(obj);
            }
            if (Title.Length > 15)
            {
                obj.ErrorMessage = "展图名称不能超过15个字";
                return Json(obj);
            }

            ArticleDisPhoto ArticleDisPhoto = new ArticleDisPhoto { Id = Id, Title = Title, PicUrl = PicUrl, Status = Status != 99 ? StatusEnum.Normal : StatusEnum.Delete };

            obj.IsSuccess = ArticleDisPhotoService.UpdateModel(ArticleDisPhoto);

            return Json(obj);
        }

        /// <summary>
        /// 批量删除默认展图
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult DeletList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = ArticleDisPhotoService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = StatusEnum.Delete);
            AjaxResponse<ArticleDisPhoto> obj = new AjaxResponse<ArticleDisPhoto>();
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
        /// 批量恢复默认展图
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult RecoverList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = ArticleDisPhotoService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = StatusEnum.Normal);
            AjaxResponse<ArticleDisPhoto> obj = new AjaxResponse<ArticleDisPhoto>();
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
        /// 文章默认展图查询
        /// </summary>
        /// <param name="page">当前页（1开始）</param>
        /// <param name="rows">显示条数</param>
        /// <returns></returns>
        public JsonResult Query(int page = 1, int rows = 20, int photoType = 1)
        {
            int total = 0;
            if (photoType == 1)
            {
                IQueryable<ArticleDisPhoto> articleList = ArticleDisPhotoService.PageLoad(a => true, a => new { a.Id }, false, page, rows, out total);

                var obj = new AjaxResponse<ListData<ArticleDisPhoto>>();
                obj.IsSuccess = true;
                obj.Data = new ListData<ArticleDisPhoto>();
                obj.Data.rows = articleList;
                obj.Data.total = total;
                return Json(obj);
            }
            else
            {
                IQueryable<PeopleDisPhoto> articleList = PeopleDisPhotoService.PageLoad(a => true, a => new { a.Id }, false, page, rows, out total);

                var obj = new AjaxResponse<ListData<PeopleDisPhoto>>();
                obj.IsSuccess = true;
                obj.Data = new ListData<PeopleDisPhoto>();
                obj.Data.rows = articleList;
                obj.Data.total = total;
                return Json(obj);
            }
        }
    }
}
