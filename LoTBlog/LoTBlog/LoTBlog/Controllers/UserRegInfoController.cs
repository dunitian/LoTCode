using LoT.Enums;
using LoT.IService;
using LoT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    public class UserRegInfoController : Controller
    {
        IUserRegInfoService UserRegInfoService { get; set; }

        /// <summary>
        /// 展示数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //throw new Exception("mmd");            
            int total;
            //ViewData.Model = UserRegInfoService.PageLoad(u => true);
            ViewData.Model = UserRegInfoService.PageLoad(u => true, u => u.Id, true, 1, 10, out total);
            return View();
        }

        /// <summary>
        /// 创建新数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(UserRegInfo userInfo)
        {
            if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email) && !string.IsNullOrEmpty(userInfo.Name) && !string.IsNullOrEmpty(userInfo.Pass))
            {
                if (UserRegInfoService.AddModel(userInfo))
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            //UserRegInfoService.DeleteModel(12); //~不推荐使用
            //以后更新用这个方法
            UserRegInfoService.UpdateModel(u => u.Id == id, u => { u.Status = StatusEnum.Normal; u.Name = u.Id + "mmd"; });
            return RedirectToAction("Index");
        }
    }
}