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
    /// 相册分类管理
    /// </summary>
    public class PhotoTypeController : Controller
    {
        public IArticleTypeService ArticleTypeService { get; set; }// 文章分类

        /// <summary>
        /// 展示页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewData.Model = ArticleTypeService.PageLoad(at => at.Pid == 0 || at.Pid == null);
            return View();
        }

        /// <summary>
        /// 添加文章类型页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewData.Model = ArticleTypeService.PageLoad(at => at.Status != StatusEnum.Delete && (at.Pid == 0 || at.Pid == null));
            return View();
        }

        /// <summary>
        /// 添加文章类型
        /// </summary>
        /// <param name="Name">文章名字</param>
        /// <param name="Pid">所属分类</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(string Name, string Introduction)
        {
            AjaxResponse<object> obj = new AjaxResponse<object>();

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
            var temp = ArticleTypeService.PageLoad(a => a.Name == Name).FirstOrDefault();
            if (temp != null)
            {
                obj.ErrorMessage = "该分类已经存在！";
                return Json(obj);
            }

            ArticleType articleType = new ArticleType { Name = Name, Status = StatusEnum.Normal };

            obj.IsSuccess = ArticleTypeService.AddModel(articleType);
            return Json(obj);
        }

        /// <summary>
        /// 批量删除文章类型
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult DeletList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = ArticleTypeService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = StatusEnum.Delete);
            AjaxResponse<ArticleType> obj = new AjaxResponse<ArticleType>();
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
        /// 批量恢复文章类型
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult RecoverList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = ArticleTypeService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = StatusEnum.Normal);
            AjaxResponse<ArticleType> obj = new AjaxResponse<ArticleType>();
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
        /// 修改文章类型页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int Id = 0)
        {
            ArticleType model = ArticleTypeService.FindModel(Id);
            if (model == null)//查不到就让他添加（防恶意篡改ID）
            {
                return RedirectToAction("Add");
            }

            //一级分类
            ViewBag.ArticleList = ArticleTypeService.PageLoad(at => at.Pid == 0 || at.Pid == null);

            //一级分类
            if (model.ParentType == null)
            {
                return View(model);
            }

            //二级或者三级
            ArticleType pTemp = model.ParentType.ParentType;
            if (pTemp != null)//三级
            {
                ViewBag.ChildTypes = ArticleTypeService.PageLoad(at => at.Pid == model.ParentType.Pid);
            }

            return View(model);
        }

        /// <summary>
        /// 更新文章分类
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Pid"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(string Name, int? Pid, int Status, int Id = 0)
        {
            AjaxResponse<ArticleType> obj = new AjaxResponse<ArticleType>();

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

            if (Id == Pid)
            {
                obj.ErrorMessage = "分类并没有变化";
                return Json(obj);
            }


            ArticleType articleType = new ArticleType { Id = Id, Name = Name, Pid = Pid == 0 ? null : Pid, Status = Status != 99 ? StatusEnum.Normal : StatusEnum.Delete };

            obj.IsSuccess = ArticleTypeService.UpdateModel(articleType);
            return Json(obj);
        }

        /// <summary>
        /// 文章类型查询
        /// </summary>
        /// <param name="page">当前页（1开始）</param>
        /// <param name="rows">显示条数</param>
        /// <param name="Name">分类名称（最多15个字）</param>
        /// <param name="Pid">所属分类</param>
        /// <returns></returns>
        public JsonResult Query(int page = 1, int rows = 20, string Name = "", int Pid = 0)
        {
            int total = 0;
            Expression<Func<ArticleType, bool>> whereLambada = at => (string.IsNullOrEmpty(Name) || at.Name.Contains(Name)) && (Pid == 0 || Pid == null || at.Pid == Pid);

            IQueryable<ArticleType> articleList = ArticleTypeService.PageLoad(whereLambada, a => new { a.Id }, false, page, rows, out total);

            var obj = new AjaxResponse<ListData<ArticleType>>();
            obj.IsSuccess = true;
            obj.Data = new ListData<ArticleType>();
            obj.Data.rows = articleList;
            obj.Data.total = total;
            return Json(obj);
        }

        /// <summary>
        /// 根据当前id获取子分类【通用方法】
        /// </summary>
        /// <param name="Pid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetChildType(int Pid = 0)
        {
            var obj = new AjaxResponse<IQueryable<Temp>>();

            if (Pid == 0)//没有获取到数据
            {
                obj.ErrorMessage = "请选择父分类";
                return Json(obj);
            }

            obj.IsSuccess = true;
            obj.Data = ArticleTypeService.PageLoad(at => at.Pid == Pid && at.Status != StatusEnum.Delete).Select(at => new Temp { Id = at.Id, Name = at.Name });
            return Json(obj);
        }
    }
}
