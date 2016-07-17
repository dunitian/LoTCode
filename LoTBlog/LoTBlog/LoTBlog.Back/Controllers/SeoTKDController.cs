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
    /// SEO信息表管理
    /// </summary>
    public class SeoTKDController : Controller
    {
        ISeoTKDService SeoTKDService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加SeoTKD页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加SeoTKD
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="SeoKeywords"></param>
        /// <param name="Sedescription"></param>
        /// <returns></returns>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(string SeoKeywords, string Sedescription)
        {
            AjaxResponse<SeoTKD> obj = new AjaxResponse<SeoTKD>();

            if (string.IsNullOrEmpty(SeoKeywords))
            {
                obj.ErrorMessage = "关键字不能为空";
                return Json(obj);
            }

            if (SeoKeywords.Length > 149)
            {
                obj.ErrorMessage = "关键字不能超过149个字";
                return Json(obj);
            }

            if (string.IsNullOrEmpty(Sedescription))
            {
                obj.ErrorMessage = "描述不能为空";
                return Json(obj);
            }

            if (Sedescription.Length > 249)
            {
                obj.ErrorMessage = "描述不能超过249个字";
                return Json(obj);
            }

            SeoTKD SeoTKD = new SeoTKD { SeoKeywords = SeoKeywords, Sedescription = Sedescription, Status = StatusEnum.Normal };

            obj.IsSuccess = SeoTKDService.AddModel(SeoTKD);
            return Json(obj);
        }

        /// <summary>
        /// 批量删除SeoTKD
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult DeletList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = SeoTKDService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = StatusEnum.Delete);
            AjaxResponse<SeoTKD> obj = new AjaxResponse<SeoTKD>();
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
        /// 批量恢复SeoTKD
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult RecoverList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = SeoTKDService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = StatusEnum.Normal);
            AjaxResponse<SeoTKD> obj = new AjaxResponse<SeoTKD>();
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
        /// 修改SeoTKD页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int Id = 0)
        {
            SeoTKD model = SeoTKDService.FindModel(Id);
            if (model == null)//查不到就让他添加（防恶意篡改ID）
            {
                return RedirectToAction("Add");
            }
            return View(model);
        }

        /// <summary>
        /// 更新SeoTKD
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="SeoKeywords"></param>
        /// <param name="Sedescription"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(string SeoKeywords, string Sedescription, int Status, int Id = 0)
        {
            AjaxResponse<SeoTKD> obj = new AjaxResponse<SeoTKD>();

            if (string.IsNullOrEmpty(SeoKeywords))
            {
                obj.ErrorMessage = "关键字不能为空";
                return Json(obj);
            }

            if (SeoKeywords.Length > 149)
            {
                obj.ErrorMessage = "关键字不能超过149个字";
                return Json(obj);
            }

            if (string.IsNullOrEmpty(Sedescription))
            {
                obj.ErrorMessage = "描述不能为空";
                return Json(obj);
            }

            if (Sedescription.Length > 249)
            {
                obj.ErrorMessage = "描述不能超过249个字";
                return Json(obj);
            }

            SeoTKD SeoTKD = new SeoTKD { Id = Id, SeoKeywords = SeoKeywords, Sedescription = Sedescription, Status = Status != 99 ? StatusEnum.Normal : StatusEnum.Delete };

            obj.IsSuccess = SeoTKDService.UpdateModel(SeoTKD);

            return Json(obj);
        }

        /// <summary>
        /// SeoTKD查询
        /// </summary>
        /// <param name="page">当前页（1开始）</param>
        /// <param name="rows">显示条数</param>
        /// <param name="Name">名称</param>
        /// <param name="Pid">所属分类</param>
        /// <returns></returns>
        public JsonResult Query(int page = 1, int rows = 20, string Name = "", int Status = -1)
        {
            int total = 0;
            Expression<Func<SeoTKD, bool>> whereLambada = at => (string.IsNullOrEmpty(Name) || at.SeoKeywords.Contains(Name)) && (Status == -1 || at.Status == (Status == 99 ? StatusEnum.Delete : StatusEnum.Normal));

            IQueryable<SeoTKD> articleList = SeoTKDService.PageLoad(whereLambada, a => new { a.Id }, true, page, rows, out total);

            var obj = new AjaxResponse<ListData<SeoTKD>>();
            obj.IsSuccess = true;
            obj.Data = new ListData<SeoTKD>();
            obj.Data.rows = articleList;
            obj.Data.total = total;
            return Json(obj);
        }
    }
}
