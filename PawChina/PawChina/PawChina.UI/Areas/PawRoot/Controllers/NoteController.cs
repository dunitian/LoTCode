using System;
using PawChina.IBLL;
using PawChina.IOC;
using PawChina.Model;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace PawChina.UI.Areas.PawRoot.Controllers
{
    public class NoteController : Controller //: BaseController
    {

        public static ISeoTKDBLL SeoTKDBLL = Container.Resolve<ISeoTKDBLL>();
        public static INoteInfoBLL NoteInfoBLL = Container.Resolve<INoteInfoBLL>();

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Edit(string id)
        {
            if (!id.IsNumber())
            {
                return RedirectToAction("Add");
            }
            var model = await NoteInfoBLL.GetAsync(id);
            model.SeoInfo = await SeoTKDBLL.GetAsync(model.NSeoId);
            if (model.SeoInfo == null)
            {
                model.SeoInfo = new SeoTKD() { SeoKeywords = "", Sedescription = "" };
            }
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(NoteInfo model)
        {
            AjaxOption<object> obj = new AjaxOption<object>();

            #region 验证系列
            if (model == null)
            {
                obj.Msg = "参数不能为空";
                return Json(obj);
            }
            if (model.NId <= 0)
            {
                obj.Msg = "笔记编号必须大于0";
                return Json(obj);
            }
            if (model.NTitle.IsNullOrWhiteSpace() || model.NTitle.Length > 100)
            {
                obj.Msg = "标题不能为空且不能大于100个字符";
                return Json(obj);
            }
            if (model.NAuthor.IsNullOrWhiteSpace() || model.NAuthor.Length > 50)
            {
                obj.Msg = "标题不能为空且不能大于50个字符";
                return Json(obj);
            }
            if (model.NContent.IsNullOrWhiteSpace())
            {
                obj.Msg = "内容不能为空";
                return Json(obj);
            }
            #endregion

            //必须保证存在数据库里面的文字是安全的
            model.NContent = model.NContent.ToUrlDecode().ToHtmlEncode();
            model.NUpdateTime = DateTime.Now;
            var noteInfo = await NoteInfoBLL.UpdateAsync(model);
            if (model.SeoInfo != null)
                await SeoTKDBLL.UpdateAsync(model.SeoInfo);
            if (noteInfo != null)
            {
                obj.Status = true;
                obj.Msg = "更新成功";
            }
            return Json(obj);
        }

        /// <summary>
        /// 查询笔记页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Query(QueryModel model)
        {
            var obj = new AjaxOption<object>();
            if (model == null)
            {
                obj.Msg = "参数不能为空";
                return Json(obj);
            }
            //起始时间大于结束时间 或者 开始时间大于现在
            if (DateTime.Compare(model.StartTime, model.EndTime) > 0 || DateTime.Compare(model.StartTime, DateTime.Now) > 0)
            {
                obj.Msg = "请检查开始或结束时间！";
                return Json(obj);
            }
            return Content(await NoteInfoBLL.QueryAsync(model));
        }
    }
}