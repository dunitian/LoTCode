using PawChina.IOC;
using PawChina.IBLL;
using System.Web.Mvc;
using PawChina.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PawChina.UI.Areas.PawRoot.Controllers
{
    public class DisPhotoController : AdminController<NoteDisPlayImg>
    {
        public static INoteDisPlayImgBLL NoteDisPlayImgBLL = Container.Resolve<INoteDisPlayImgBLL>();

        /// <summary>
        /// 验证错误
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetErrorMsg(NoteDisPlayImg model)
        {
            string msg = string.Empty;
            #region 验证系列
            if (model == null)
            {
                return "参数不能为空";
            }
            if (model.DPicUrl.IsNullOrWhiteSpace())
            {
                return "上传图片不能为空";
            }
            #endregion
            return msg;
        }

        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public override ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <returns></returns>
        public override ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public override async Task<JsonResult> Add(NoteDisPlayImg model)
        {
            AjaxOption<object> obj = new AjaxOption<object>();
            #region 验证相关
            obj.Msg = GetErrorMsg(model);
            //有错误信息
            if (!obj.Msg.IsNullOrWhiteSpace())
            {
                return Json(obj);
            }

            if (model.DTitle.IsNullOrWhiteSpace())
            {
                model.DTitle = "笔记默认展图";
            }
            #endregion
            model.DataStatus = StatusEnum.Normal;
            var modelId = await NoteDisPlayImgBLL.InsertAsync(model);
            if (modelId > 0)
            {
                obj.Status = true;
                obj.Msg = "添加成功";
            }
            return Json(obj);
        }

        /// <summary>
        /// 批量上传
        /// </summary>
        /// <returns></returns>
        public ActionResult AddList()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> AddList(string title, string[] imgs)
        {
            AjaxOption<object> obj = new AjaxOption<object>();
            if (imgs == null)
            {
                obj.Msg = "文件列表为空";
                return Json(obj);
            }
            if (title.IsNullOrWhiteSpace())
            {
                title = "笔记默认展图";
            }
            var list = new List<NoteDisPlayImg>();
            System.Array.ForEach(imgs, (imgsrc) =>
            {
                if (!imgsrc.IsNullOrWhiteSpace() && imgsrc.Length > 5)
                {
                    list.Add(new NoteDisPlayImg()
                    {
                        DTitle = title,
                        DPicUrl = imgsrc,
                        DataStatus = StatusEnum.Normal
                    });
                }
            });
            var count = await NoteDisPlayImgBLL.ExecuteAsync("insert into NoteDisPlayImg(DTitle,DPicUrl,DataStatus) values(@DTitle,@DPicUrl,@DataStatus)", list);//.InsertAsync(list);
            if (count > 0)
            {
                obj.Status = true;
                obj.Msg = string.Format("成功添加了 {0} 个图片", count);
            }
            else
            {
                obj.Msg = "操作失败，请重试";
            }
            return Json(obj);
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <returns></returns>
        public override async Task<ActionResult> Edit(int id = 0)
        {
            if (id <= 0)
            {
                return RedirectToAction("Add");
            }
            var model = await NoteDisPlayImgBLL.GetAsync(id);
            if (model == null)
            {
                return RedirectToAction("Add");
            }
            return View(model);
        }
        [HttpPost]
        public override async Task<JsonResult> Edit(NoteDisPlayImg model)
        {
            AjaxOption<object> obj = new AjaxOption<object>();
            #region 验证相关
            if (model.DId <= 0)
            {
                obj.Msg = "编号必须大于0";
                return Json(obj);
            }
            if (model.DataStatus == StatusEnum.Init)
            {
                model.DataStatus = StatusEnum.Normal;
            }
            obj.Msg = GetErrorMsg(model);
            //有错误信息
            if (!obj.Msg.IsNullOrWhiteSpace())
            {
                return Json(obj);
            }
            #endregion
            var noteDisPlayImg = await NoteDisPlayImgBLL.UpdateAsync(model);
            if (noteDisPlayImg != null)
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
        public override async Task<JsonResult> UpdateList(string ids, StatusEnum status)
        {
            AjaxOption<object> obj = new AjaxOption<object>();
            if (ids.IsNullOrWhiteSpace())
            {
                obj.Msg = "选中项不能为空";
                return Json(obj);
            }
            int i = await NoteDisPlayImgBLL.ExecuteAsync("update NoteDisPlayImg set DataStatus=@DataStatus where DId in @DIds", new
            {
                DataStatus = status,
                DIds = ids.SplitToIntList()
            });
            obj.Status = true;
            obj.Msg = string.Format("更新了 {0} 条数据", i);
            return Json(obj);
        }

        /// <summary>
        /// 模型查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<ActionResult> Query(QueryModel model)
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
            return Content(await NoteDisPlayImgBLL.QueryAsync(model));
        }
    }
}