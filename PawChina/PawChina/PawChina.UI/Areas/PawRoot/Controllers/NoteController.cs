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
        /// NoteInfo验证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetErrorMsg(NoteInfo model)
        {
            string msg = string.Empty;
            #region 验证系列
            if (model == null)
            {
                return "参数不能为空";
            }
            if (model.NTitle.IsNullOrWhiteSpace() || model.NTitle.Length > 100)
            {
                return "标题不能为空且不能，且大于100个字符";
            }
            if (model.NAuthor.IsNullOrWhiteSpace() || model.NAuthor.Length > 50)
            {
                return "作者名不能为空且，且不能大于50个字符";
            }
            if (model.NContent.IsNullOrWhiteSpace())
            {
                return "内容不能为空";
            }

            #region SEO相关
            //理论上是不可能出现SeoInfo==null的现象的，防止黑客手动传null
            if (model.SeoInfo == null)
            {
                //todo:一次危险记录
                return "SEO信息不能为空";
            }
            if (model.SeoInfo.SeoKeywords.IsNullOrWhiteSpace() || model.SeoInfo.SeoKeywords.Length > 149)
            {
                return "SEO关键词不能为空，且不能大于149个字符";
            }
            if (model.SeoInfo.Sedescription.IsNullOrWhiteSpace() || model.SeoInfo.Sedescription.Length > 249)
            {
                return "SEO描述不能为空，且不能大于249个字符";
            }
            #endregion

            #endregion
            return msg;
        }

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

        [HttpPost]
        public async Task<JsonResult> Add(NoteInfo model)
        {
            AjaxOption<object> obj = new AjaxOption<object>();

            //验证相关
            obj.Msg = GetErrorMsg(model);
            //有错误信息
            if (!obj.Msg.IsNullOrWhiteSpace())
            {
                return Json(obj);
            }

            model.NDataStatus = StatusEnum.Normal;
            model.SeoInfo.DataStatus = StatusEnum.Normal;
            model.NContent = model.NContent.ToUrlDecode();
            //todo:默认展图
            if (model.NDisplayPic.IsNullOrWhiteSpace())
            {
                model.NDisplayPic = "";
            }

            //纯文本内容为空则手动赋值（为前端准备）
            if (model.NContentText.IsNullOrWhiteSpace())
            {
                model.NContentText = model.NContent.GetChinese();
            }

            if (model.SeoInfo != null && !model.SeoInfo.SeoKeywords.IsNullOrWhiteSpace() && !model.SeoInfo.Sedescription.IsNullOrWhiteSpace())
            {
                model.NSeoId = await SeoTKDBLL.InsertAsync(model.SeoInfo);
            }

            var noteId = await NoteInfoBLL.InsertAsync(model);

            if (noteId > 0)
            {
                obj.Status = true;
                obj.Msg = "添加成功";
            }
            return Json(obj);
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Edit(int id = 0)
        {
            if (id <= 0)
            {
                return RedirectToAction("Add");
            }
            var model = await NoteInfoBLL.GetAsync(id);
            model.SeoInfo = await SeoTKDBLL.GetAsync(model.NSeoId);
            //防止编辑页面出错
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
            if (model.NId <= 0)
            {
                obj.Msg = "笔记编号必须大于0";
                return Json(obj);
            }
            obj.Msg = GetErrorMsg(model);
            //有错误信息
            if (!obj.Msg.IsNullOrWhiteSpace())
            {
                return Json(obj);
            }
            #endregion

            model.NContent = model.NContent.ToUrlDecode();
            //todo:默认展图
            if (model.NDisplayPic.IsNullOrWhiteSpace())
            {
                model.NDisplayPic = "";
            }

            //纯文本内容为空则手动赋值（为前端准备）
            if (model.NContentText.IsNullOrWhiteSpace())
            {
                model.NContentText = model.NContent.GetChinese();
            }

            if (model.SeoInfo != null && !model.SeoInfo.SeoKeywords.IsNullOrWhiteSpace() && !model.SeoInfo.Sedescription.IsNullOrWhiteSpace())
            {
                var seoInfo = await SeoTKDBLL.UpdateAsync(model.SeoInfo);
                if (seoInfo != null)
                    model.NSeoId = seoInfo.Id;
            }

            var noteInfo = await NoteInfoBLL.UpdateAsync(model);

            if (noteInfo != null)
            {
                obj.Status = true;
                obj.Msg = "更新成功";
            }
            return Json(obj);
        }

        /// <summary>
        /// 批量更新~恢复或者删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<JsonResult> UpdateList(string ids, StatusEnum status)
        {
            AjaxOption<object> obj = new AjaxOption<object>();
            if (ids.IsNullOrWhiteSpace())
            {
                obj.Msg = "选中项不能为空";
                return Json(obj);
            }
            int i = await NoteInfoBLL.ExecuteAsync("update NoteInfo set NDataStatus=@NDataStatus where NId in @NIds", new
            {
                NDataStatus = status,
                NIds = ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => { int n; int.TryParse(s, out n); return n; }).Distinct()
            });
            obj.Status = true;
            obj.Msg = string.Format("更新了 {0} 条数据", i);
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
            //起始时间大于结束时间
            if (model.StartTime > model.EndTime)
            {
                obj.Msg = "请检查开始或结束时间！";
                return Json(obj);
            }
            return Content(await NoteInfoBLL.QueryAsync(model));
        }
    }
}