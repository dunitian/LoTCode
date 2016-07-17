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
    /// 友情链接管理
    /// </summary>
    public class FriendLinkController : Controller
    {
        IFriendLinkService FriendLinkService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 添加友情链接页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加友情链接
        /// </summary>
        /// <param name="Name">友情链接名字</param>
        /// <param name="LinkType">友情链接所在页面</param>
        /// <param name="Sort">排序</param>
        /// <param name="WebUrl">网站地址</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(string Name, int LinkType, int Sort, string WebUrl)
        {
            AjaxResponse<FriendLink> obj = new AjaxResponse<FriendLink>();

            if (string.IsNullOrEmpty(Name))
            {
                obj.ErrorMessage = "标题不能为空";
                return Json(obj);
            }

            if (Name.Length > 15)
            {
                obj.ErrorMessage = "标题不能超过15个字";
                return Json(obj);
            }

            //保证Name唯一，先查询一下是不是有这个Name
            var temp = FriendLinkService.PageLoad(a => a.LinkName == Name).FirstOrDefault();
            if (temp != null)
            {
                obj.ErrorMessage = "该标题已经存在！";
                return Json(obj);
            }

            FriendLink FriendLink = new FriendLink { LinkName = Name, LinkType = (LinkTypeEnum)LinkType, Sort = Sort, WebUrl = WebUrl, Status = StatusEnum.Normal };

            obj.IsSuccess = FriendLinkService.AddModel(FriendLink);
            return Json(obj);
        }

        /// <summary>
        /// 批量删除友情链接
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult DeletList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = FriendLinkService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = StatusEnum.Delete);
            AjaxResponse<FriendLink> obj = new AjaxResponse<FriendLink>();
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
        /// 批量恢复友情链接
        /// </summary>
        /// <param name="ids">ID集合信息（逗号分隔）</param>
        /// <returns></returns>
        public JsonResult RecoverList(string ids = "")
        {
            IList<int> idList = ids.Trim(',').Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s)).ToList();
            int i = FriendLinkService.UpdateModel(at => idList.Contains(at.Id), at => at.Status = StatusEnum.Normal);
            AjaxResponse<FriendLink> obj = new AjaxResponse<FriendLink>();
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
        /// 修改友情链接页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int Id = 0)
        {
            FriendLink model = FriendLinkService.FindModel(Id);
            if (model == null)//查不到就让他添加（防恶意篡改ID）
            {
                return RedirectToAction("Add");
            }
            return View(model);
        }

        /// <summary>
        /// 更新友情链接
        /// </summary>
        /// <param name="Name">友情链接名字</param>
        /// <param name="LinkType">友情链接所在页面</param>
        /// <param name="Sort">排序</param>
        /// <param name="WebUrl">网站地址</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(string Name, int LinkType, int Sort, string WebUrl, int Status, int Id = 0)
        {
            AjaxResponse<FriendLink> obj = new AjaxResponse<FriendLink>();

            if (string.IsNullOrEmpty(Name))
            {
                obj.ErrorMessage = "标题不能为空";
                return Json(obj);
            }

            if (Name.Length > 15)
            {
                obj.ErrorMessage = "标题不能超过15个字";
                return Json(obj);
            }

            FriendLink FriendLink = new FriendLink { Id = Id, LinkName = Name, LinkType = (LinkTypeEnum)LinkType, Sort = Sort, WebUrl = WebUrl, Status = Status != 99 ? StatusEnum.Normal : StatusEnum.Delete };

            obj.IsSuccess = FriendLinkService.UpdateModel(FriendLink);

            return Json(obj);
        }

        /// <summary>
        /// 友情链接查询
        /// </summary>
        /// <param name="page">当前页（1开始）</param>
        /// <param name="rows">显示条数</param>
        /// <param name="Name">标题（最多15个字）</param>
        /// <param name="Pid">所属分类</param>
        /// <returns></returns>
        public JsonResult Query(int page = 1, int rows = 20, string Name = "", int LinkType = -1)
        {
            int total = 0;
            Expression<Func<FriendLink, bool>> whereLambada = at => (string.IsNullOrEmpty(Name) || at.LinkName.Contains(Name)) && (LinkType == -1 || at.LinkType == (LinkTypeEnum)LinkType);

            IQueryable<FriendLink> articleList = FriendLinkService.PageLoad(whereLambada, a => new { a.Sort, a.Id }, false, page, rows, out total);

            var obj = new AjaxResponse<ListData<FriendLink>>();
            obj.IsSuccess = true;
            obj.Data = new ListData<FriendLink>();
            obj.Data.rows = articleList;
            obj.Data.total = total;
            return Json(obj);
        }
    }
}
