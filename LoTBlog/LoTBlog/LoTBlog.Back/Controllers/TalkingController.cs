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
    /// 生活点滴专栏
    /// </summary>
    public class TalkingController : Controller
    {
        ITalkingService TalkingService { get; set; }
        IArticleDisPhotoService ArticleDisPhotoService { get; set; }
        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// 说说添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 说说添加页面
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Say">内容</param>
        /// <param name="HitCount">浏览次数</param>
        /// <param name="Status">状态（0,所有人可见，1,好友可见，2,仅自己可见,99删除）</param>
        /// <param name="DisplayPic">文章默认展图</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(string Title = "", string Say = "", int HitCount = 9, int Status = 0, string DisplayPic = "")
        {
            AjaxResponse<object> obj = new AjaxResponse<object>();

            #region 一系列验证
            if (!string.IsNullOrEmpty(Title))
            {
                if (Title.Length > 25)
                {
                    obj.ErrorMessage = "标题25个字以内！";
                    return Json(obj);
                }
            }

            if (string.IsNullOrEmpty(Say))
            {
                obj.ErrorMessage = "内容不能为空！";
                return Json(obj);
            }

            if (Say.Length > 500)
            {
                obj.ErrorMessage = "内容500个字以内！";
                return Json(obj);
            }
            #endregion

            //如果没有上传默认展图，就随机展示一个默认展图
            if (string.IsNullOrEmpty(DisplayPic))
            {
                IList<ArticleDisPhoto> disPics = ArticleDisPhotoService.PageLoad(p => p.Status != StatusEnum.Delete).ToList();
                int count = disPics.Count;
                if (count > 0)
                {
                    Random random = new Random();
                    int index = random.Next(disPics.Count);
                    DisplayPic = disPics[index].PicUrl;
                }
                else//实在没有的话就给一个默认值
                {
                    DisplayPic = LoT.Common.ConfigHelper.GetValueForConfigAppKey("ArticleTypeDisPlayPic");
                }
            }

            Talking talking = new Talking()
            {
                Title = string.IsNullOrEmpty(Title) ? "" : Title,
                Say = Say,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                HitCount = HitCount,
                Status = (ArticleStatusEnum)Status,
                DisplayPic = DisplayPic
            };

            obj.IsSuccess = TalkingService.AddModel(talking);

            return Json(obj);
        }

        /// <summary>
        /// 说说修改页面
        /// </summary>
        /// <param name="GroupType"></param>
        /// <param name="ArticleId"></param>
        /// <returns></returns>
        public ActionResult Update(int GroupType = 1, int ArticleId = 0)
        {
            Talking talking = TalkingService.FindModel(ArticleId);
            //不存在就跳转到添加页面
            if (talking == null)
            {
                return RedirectToAction("Add");
            }

            return View(talking);
        }

        /// <summary>
        /// 说说修改页面
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Say">内容</param>
        /// <param name="HitCount">浏览次数</param>
        /// <param name="Status">状态（0,所有人可见，1,好友可见，2,仅自己可见,99删除）</param>
        /// <param name="DisplayPic">文章默认展图</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(string Title = "", string Say = "", int HitCount = 9, int Status = 0, string DisplayPic = "", int Id=0)
        {
            AjaxResponse<object> obj = new AjaxResponse<object>();

            #region 一系列验证
            if (Id <= 0)
            {
                obj.ErrorMessage = "说说不存在！";
                return Json(obj);
            }

            if (!string.IsNullOrEmpty(Title))
            {
                if (Title.Length > 25)
                {
                    obj.ErrorMessage = "标题25个字以内！";
                    return Json(obj);
                }
            }

            if (string.IsNullOrEmpty(Say))
            {
                obj.ErrorMessage = "内容不能为空！";
                return Json(obj);
            }

            if (Say.Length > 500)
            {
                obj.ErrorMessage = "内容500个字以内！";
                return Json(obj);
            }
            #endregion

            //如果没有上传默认展图，就随机展示一个默认展图
            if (string.IsNullOrEmpty(DisplayPic))
            {
                IList<ArticleDisPhoto> disPics = ArticleDisPhotoService.PageLoad(p => p.Status != StatusEnum.Delete).ToList();
                int count = disPics.Count;
                if (count > 0)
                {
                    Random random = new Random();
                    int index = random.Next(disPics.Count);
                    DisplayPic = disPics[index].PicUrl;
                }
                else//实在没有的话就给一个默认值
                {
                    DisplayPic = LoT.Common.ConfigHelper.GetValueForConfigAppKey("ArticleTypeDisPlayPic");
                }
            }

            Talking talking = new Talking()
            {
                Id=Id,
                Title = string.IsNullOrEmpty(Title) ? "" : Title,
                Say = Say,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                HitCount = HitCount,
                Status = (ArticleStatusEnum)Status,
                DisplayPic = DisplayPic
            };

            obj.IsSuccess = TalkingService.UpdateModel(talking);

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
            int i = TalkingService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = ArticleStatusEnum.Delete);
            AjaxResponse<Talking> obj = new AjaxResponse<Talking>();
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
            int i = TalkingService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = ArticleStatusEnum.All);
            AjaxResponse<Talking> obj = new AjaxResponse<Talking>();
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
        /// 查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="Say"></param>
        /// <param name="startime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public JsonResult Query(int page = 1, int rows = 20, string Say = "", string startime = "", string endtime = "")
        {
            DateTime regStartTime;
            DateTime regEndTime;
            if (!DateTime.TryParse(startime, out regStartTime))
            {
                regStartTime = DateTime.MinValue;
            }

            if (!DateTime.TryParse(endtime, out regEndTime))
            {
                regEndTime = DateTime.MaxValue;
            }

            int total = 0;
            Expression<Func<Talking, bool>> whereLambada =
                a => (string.IsNullOrEmpty(Say) || a.Say.Contains(Say))
                    && (startime == string.Empty || a.CreateTime >= regStartTime)
                    && (endtime == string.Empty || a.CreateTime <= regEndTime);

            var TalkingList = TalkingService.PageLoad(whereLambada, a => new { a.CreateTime }, true, page, rows, out total).ToList().Select(a => new TalkingTemp
            {
                Id = a.Id,
                Say = a.Say,
                CreateTime = a.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                UpdateTime = a.UpdateTime.ToString("yyyy-MM-dd HH:mm"),
                Status = a.Status
            });

            var obj = new AjaxResponse<ListData<TalkingTemp>>();
            obj.IsSuccess = true;
            obj.Data = new ListData<TalkingTemp>();
            obj.Data.rows = TalkingList;
            obj.Data.total = total;
            return Json(obj);
        }
    }
}
