using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PawChina.UI.Areas.PawRoot.Controllers
{
    public class NoteController : Controller //BaseController
    {
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
            var model = await GetNoteService().GetMode(id);
            var backModel = model.MapTo<NoteBackModel>();
            backModel.SeoInfo = await GetSEOService().GetMode(model.NSeoId);
            if (backModel.SeoInfo == null)
            {
                backModel.SeoInfo = new SeoTKD() { SeoKeywords = "", Sedescription = "" };
            }
            return View(backModel);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(NoteBackModel model)
        {
            await GetNoteService().UpdateMode(null);
            await GetSEOService().UpdateMode(model.SeoInfo);
            return Json("");
        }

        /// <summary>
        /// 查询笔记页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Query(QueryModel model)
        {
            //起始时间大于结束时间 或者 开始时间大于现在
            if (DateTime.Compare(model.StartTime, model.EndTime) > 0 || DateTime.Compare(model.StartTime, DateTime.Now) > 0)
            {
                var obj = new AjaxOption<object>() { Msg = "请检查开始或结束时间！" };
                return Json(obj);
            }
            return Json(await GetNoteService().PageLoadAsync(model));
        }
    }
}