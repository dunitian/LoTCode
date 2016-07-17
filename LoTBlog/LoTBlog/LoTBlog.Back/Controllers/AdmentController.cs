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
    /// 固定广告专栏
    /// </summary>
    public class AdmentController : Controller
    {
        IAdvertisementService AdvertisementService { get; set; }
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加Advertisement页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加Advertisement
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Map"></param>
        /// <param name="AContext"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(string Map, string AContext)
        {
            AjaxResponse<Advertisement> obj = new AjaxResponse<Advertisement>();

            if (string.IsNullOrEmpty(Map))
            {
                obj.ErrorMessage = "位置不能为空";
                return Json(obj);
            }

            if (Map.Length > 49)
            {
                obj.ErrorMessage = "位置不能超过49个字";
                return Json(obj);
            }

            if (string.IsNullOrEmpty(AContext))
            {
                obj.ErrorMessage = "描述不能为空";
                return Json(obj);
            }

            if (AContext.Length > 500)
            {
                obj.ErrorMessage = "描述不能超过500个字";
                return Json(obj);
            }

            AContext = HttpUtility.UrlDecode(AContext);
            //必须保证存在数据库里面的文字是安全的
            AContext = HttpUtility.HtmlEncode(AContext);

            Advertisement Advertisement = new Advertisement { Map = Map, AContext = AContext, Status = StatusEnum.Normal };

            obj.IsSuccess = AdvertisementService.AddModel(Advertisement);
            return Json(obj);
        }

        /// <summary>
        /// 批量删除Advertisement
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult DeletList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = AdvertisementService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = StatusEnum.Delete);
            AjaxResponse<Advertisement> obj = new AjaxResponse<Advertisement>();
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
        /// 批量恢复Advertisement
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult RecoverList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = AdvertisementService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = StatusEnum.Normal);
            AjaxResponse<Advertisement> obj = new AjaxResponse<Advertisement>();
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
        /// 修改Advertisement页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int Id = 0)
        {
            Advertisement model = AdvertisementService.FindModel(Id);
            if (model == null)//查不到就让他添加（防恶意篡改ID）
            {
                return RedirectToAction("Add");
            }
            return View(model);
        }

        /// <summary>
        /// 更新Advertisement
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Map"></param>
        /// <param name="Sedescription"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(string Map, string AContext, int Status, int Id = 0)
        {
            AjaxResponse<Advertisement> obj = new AjaxResponse<Advertisement>();

            if (string.IsNullOrEmpty(Map))
            {
                obj.ErrorMessage = "位置不能为空";
                return Json(obj);
            }

            if (Map.Length > 49)
            {
                obj.ErrorMessage = "位置不能超过49个字";
                return Json(obj);
            }

            if (string.IsNullOrEmpty(AContext))
            {
                obj.ErrorMessage = "描述不能为空";
                return Json(obj);
            }

            if (AContext.Length > 500)
            {
                obj.ErrorMessage = "描述不能超过500个字";
                return Json(obj);
            }

            AContext = HttpUtility.UrlDecode(AContext);
            //必须保证存在数据库里面的文字是安全的
            AContext = HttpUtility.HtmlEncode(AContext);

            Advertisement Advertisement = new Advertisement { Id = Id, Map = Map, AContext = AContext, Status = Status != 99 ? StatusEnum.Normal : StatusEnum.Delete };

            obj.IsSuccess = AdvertisementService.UpdateModel(Advertisement);

            return Json(obj);
        }

        /// <summary>
        /// Advertisement查询
        /// </summary>
        /// <param name="page">当前页（1开始）</param>
        /// <param name="rows">显示条数</param>
        /// <returns></returns>
        public JsonResult Query(int page = 1, int rows = 20)
        {
            int total = 0;

            IQueryable<Advertisement> advertisementList = AdvertisementService.PageLoad(a => true, a => new { a.Id }, false, page, rows, out total);

            var obj = new AjaxResponse<ListData<Advertisement>>();
            obj.IsSuccess = true;
            obj.Data = new ListData<Advertisement>();
            obj.Data.rows = advertisementList;
            obj.Data.total = total;
            return Json(obj);
        }
    }
}
