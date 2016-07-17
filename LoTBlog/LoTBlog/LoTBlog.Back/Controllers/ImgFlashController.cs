using LoT.IService;
using LoT.Model;
using LoTBlog.Back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Back.Controllers
{
    /// <summary>
    /// 幻灯片专栏
    /// </summary>
    public class ImgFlashController : Controller
    {
        IImgFlashService ImgFlashService { get; set; }
        public ActionResult Index()
        {
            var imgFlashModel = ImgFlashService.PageLoad(i => true).FirstOrDefault();

            if (imgFlashModel == null)//如果赋值为null的话会报错滴
            {
                imgFlashModel = new ImgFlash();
            }

            ViewData.Model = imgFlashModel;

            return View();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="TopTitle">上标题</param>
        /// <param name="BottomTitle">下标题</param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(string TopTitle, string BottomTitle, string BackImg, int Id = 0)
        {
            AjaxResponse<ImgFlash> obj = new AjaxResponse<ImgFlash>();

            if (string.IsNullOrEmpty(TopTitle))
            {
                obj.ErrorMessage = "上标题不能为空";
                return Json(obj);
            }

            if (TopTitle.Length > 100)
            {
                obj.ErrorMessage = "上标题不能超过100个字";
                return Json(obj);
            }

            if (string.IsNullOrEmpty(BottomTitle))
            {
                obj.ErrorMessage = "下标题不能为空";
                return Json(obj);
            }

            if (BottomTitle.Length > 100)
            {
                obj.ErrorMessage = "下标题不能超过100个字";
                return Json(obj);
            }

            if (string.IsNullOrEmpty(BackImg))
            {
                obj.ErrorMessage = "背景图为空的话会很丑的！";
                return Json(obj);
            }

            ImgFlash ImgFlash = new ImgFlash { Id = Id, TopTitle = TopTitle, BottomTitle = BottomTitle, BackImg = BackImg, Status = LoT.Enums.StatusEnum.Normal };

            if (Id > 0)
            {
                obj.IsSuccess = ImgFlashService.UpdateModel(ImgFlash);
            }
            else
            {
                obj.IsSuccess = ImgFlashService.AddModel(ImgFlash);
            }

            return Json(obj);
        }

    }
}
