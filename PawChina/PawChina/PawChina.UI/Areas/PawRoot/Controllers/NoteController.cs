using System;
using System.Linq;
using PawChina.IBLL;
using PawChina.IOC;
using PawChina.Model;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace PawChina.UI.Areas.PawRoot.Controllers
{
    public class NoteController : BaseController
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
            //todo:验证
            await NoteInfoBLL.UpdateAsync(model);
            await SeoTKDBLL.UpdateAsync(model.SeoInfo);
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
            return Json(await NoteInfoBLL.QueryAsync(model));
        }
    }
}