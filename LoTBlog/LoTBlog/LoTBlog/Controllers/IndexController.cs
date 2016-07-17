using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Controllers
{
    //散客
    //主页，网站的一些最新动态
    public class IndexController : Controller
    {
        LoT.IService.IAdvertisementService AdvertisementService { get; set; }
        LoT.IService.IQQModelService QQModelService { get; set; }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="pi">pageindex</param>
        /// <param name="ps">pagesize</param>
        /// <returns></returns>
        public ActionResult Index(int pi = 1, int ps = 9)
        {
            ViewBag.PageIndex = pi;
            ViewBag.PageSize = ps;
            return View();
        }

        /// <summary>
        /// QQ登录验证+处理(前端就这一个地方对数据库有写入操作，好好把关[v2 的时候进行安全记录])
        /// </summary>
        /// <returns></returns>
        public JsonResult QQCheck(LoT.Model.QQModel qqinfo)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(qqinfo.Name) && !string.IsNullOrEmpty(qqinfo.OpenId) && !string.IsNullOrEmpty(qqinfo.AccessToken))
            {
                //openId 唯一凭证（一会改代码）
                LoT.Model.QQModel model = new LoT.Model.QQModel();
                model.Name = LoT.Safe.HtmlSafeHelper.NoHTML(qqinfo.Name);
                model.OpenId = LoT.Safe.HtmlSafeHelper.NoHTML(qqinfo.OpenId);
                model.AccessToken = LoT.Safe.HtmlSafeHelper.NoHTML(qqinfo.AccessToken);
                model.Figureurl = LoT.Safe.HtmlSafeHelper.NoHTML(qqinfo.Figureurl);
                model.Status = LoT.Enums.AdminEnum.Temp;
                model.EndDataTime = DateTime.Now;
                model.Count = 1;

                //todo: 登录后存数据库

                //openid唯一
                var qqModel = QQModelService.PageLoad(q => q.OpenId == model.OpenId).FirstOrDefault();
                //修改qqinfo
                if (qqModel != null)
                {
                    double interval = new TimeSpan(model.EndDataTime.Ticks - qqModel.EndDataTime.Ticks).TotalMinutes;
                    //如果时间间隔太短就直接忽略【QQToken短时间不会失效】（防DDos）
                    if (interval < 2)
                    {
                        return Json(result);
                    }
                    qqModel.Name = model.Name;
                    qqModel.OpenId = model.OpenId;
                    qqModel.AccessToken = model.AccessToken;
                    qqModel.Figureurl = model.Figureurl;
                    qqModel.EndDataTime = model.EndDataTime;
                    qqModel.Count += 1;
                    result = QQModelService.UpdateModel(qqModel);
                }
                else//添加qqinfo
                {
                    result = QQModelService.AddModel(model);
                }
            }

            return Json(result);
        }

        /// <summary>
        /// 右下角广告
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 999)]
        public ActionResult BottomMsg()
        {
            var model = AdvertisementService.PageLoad(i => i.Map.Contains("右下角")).FirstOrDefault();
            //前台的所有东西都得谨慎
            if (model == null || string.IsNullOrEmpty(model.AContext))
            {
                ViewBag.AContext = "&amp;nbsp;&lt;a href=&quot;http://tieba.baidu.com/p/3783667386; target=&quot;_blank&quot;&gt;.NET学习资料-逆天整理-精华无密版~~上万资料免费赠送哦~&lt;/a&gt;";
            }
            else
            {
                ViewBag.AContext = model.AContext;
            }
            return View();
        }
    }
}
