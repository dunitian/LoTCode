using LoT.IService;
using LoT.Model;
using LoTBlog.Back.Models;
using Spring.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Back.Controllers
{
    /// <summary>
    /// 固定信息管理~Logo、头像；标题、简介
    /// </summary>
    public class BaseInfoController : Controller
    {
        IBaseInfoService BaseInfoService { get; set; }

        /// <summary>
        /// 显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var baseInfoModel = BaseInfoService.PageLoad(b => true).FirstOrDefault();
            if (baseInfoModel == null)//Model不能为空，不然前台会报错
            {
                baseInfoModel = new BaseInfo() { QQ = 1054186320 };
            }
            return View(baseInfoModel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <returns></returns>
        public JsonResult Update(BaseInfo baseInfo)
        {
            AjaxResponse<object> obj = new AjaxResponse<object>();

            #region 一系列验证
            if (baseInfo == null)
            {
                obj.ErrorMessage = "更新失败";
                return Json(obj);
            }
            //顶部标题
            if (string.IsNullOrEmpty(baseInfo.TopTitle))
            {
                obj.ErrorMessage = "顶部标题不能为空";
                return Json(obj);
            }
            if (baseInfo.TopTitle.Length > 29)
            {
                obj.ErrorMessage = "顶部标题不能超过29个字";
                return Json(obj);
            }
            //头部简介
            if (string.IsNullOrEmpty(baseInfo.TopText))
            {
                obj.ErrorMessage = "头部简介不能为空";
                return Json(obj);
            }
            if (baseInfo.TopText.Length > 100)
            {
                obj.ErrorMessage = "头部简介不能超过100个字";
                return Json(obj);
            }
            //Logo正面
            if (string.IsNullOrEmpty(baseInfo.TopLogoOne) || baseInfo.TopLogoOne.Length < 1)
            {
                obj.ErrorMessage = "Logo正面不能为空";
                return Json(obj);
            }
            //Logo背面
            if (string.IsNullOrEmpty(baseInfo.TopLogoTwo) || baseInfo.TopLogoTwo.Length < 1)
            {
                obj.ErrorMessage = "Logo背面不能为空";
                return Json(obj);
            }
            //右侧头像
            if (string.IsNullOrEmpty(baseInfo.RightImg) || baseInfo.RightImg.Length < 1)
            {
                obj.ErrorMessage = "右侧头像不能为空";
                return Json(obj);
            }
            //右侧标题
            if (string.IsNullOrEmpty(baseInfo.RightTitle))
            {
                obj.ErrorMessage = "右侧标题不能为空";
                return Json(obj);
            }
            if (baseInfo.RightTitle.Length > 29)
            {
                obj.ErrorMessage = "右侧标题不能超过29个字";
                return Json(obj);
            }
            //右侧宣言
            if (string.IsNullOrEmpty(baseInfo.Manifesto))
            {
                obj.ErrorMessage = "右侧宣言不能为空";
                return Json(obj);
            }
            if (baseInfo.Manifesto.Length > 29)
            {
                obj.ErrorMessage = "右侧宣言不能超过29个字";
                return Json(obj);
            }
            //网名昵称
            if (string.IsNullOrEmpty(baseInfo.Nickname))
            {
                obj.ErrorMessage = "网名昵称不能为空";
                return Json(obj);
            }
            if (baseInfo.Nickname.Length > 29)
            {
                obj.ErrorMessage = "网名昵称不能超过29个字";
                return Json(obj);
            }
            //奋斗目标
            if (string.IsNullOrEmpty(baseInfo.Goal))
            {
                obj.ErrorMessage = "奋斗目标不能为空";
                return Json(obj);
            }
            if (baseInfo.Goal.Length > 29)
            {
                obj.ErrorMessage = "奋斗目标不能超过29个字";
                return Json(obj);
            }
            //你的梦想
            if (string.IsNullOrEmpty(baseInfo.Dream))
            {
                obj.ErrorMessage = "你的梦想不能为空";
                return Json(obj);
            }
            if (baseInfo.Dream.Length > 29)
            {
                obj.ErrorMessage = "你的梦想不能超过29个字";
                return Json(obj);
            }
            //ＱＱ号
            if (baseInfo.QQ < 0)
            {
                obj.ErrorMessage = "ＱＱ号不能为空";
                return Json(obj);
            }
            //邮箱号码
            if (string.IsNullOrEmpty(baseInfo.Email) || baseInfo.Email.Length < 1)
            {
                obj.ErrorMessage = "邮箱号码不能为空";
                return Json(obj);
            }
            #endregion

            if (baseInfo.Id > 0)
            {
                obj.IsSuccess = BaseInfoService.UpdateModel(baseInfo);
            }
            else
            {
                obj.IsSuccess = BaseInfoService.AddModel(baseInfo);
            }
            return Json(obj);
        }

    }
}
