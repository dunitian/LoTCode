using System.Linq;
using PawChina.IOC;
using PawChina.IBLL;
using System.Web.Mvc;
using PawChina.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PawChina.UI.Areas.PawRoot.Controllers
{
    public class ProTypeController : AdminController<ProTypeInfo>
    {
        public static IProTypeInfoBLL ProTypeInfoBLL = Container.Resolve<IProTypeInfoBLL>();

        /// <summary>
        /// 验证错误
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetErrorMsg(ProTypeInfo model)
        {
            string msg = string.Empty;
            #region 验证系列
            if (model == null)
            {
                return "参数不能为空";
            }
            if (model.TName.IsNullOrWhiteSpace())
            {
                return "分类名称不能为空";
            }
            if (model.TGroupType != ProductEnum.IsProduct && model.TGroupType != ProductEnum.IsProductPart)
            {
                return "请选择分类类别（商品分类 | 配件分类）";
            }
            if (model.TFloor != 1 && model.TFloor != 2 && model.TFloor != 3)
            {
                return "请选择该分类所属级别（1，2，3）";
            }
            if (model.TPid < 0)
            {
                return "请选择正确的所属分类";
            }
            if (model.TSort > short.MaxValue)
            {
                return "不能超过32767";
            }
            if (model.TDisplayPic.IsNullOrWhiteSpace())
            {
                return "上传图片不能为空";
            }
            if (model.TContent.IsNullOrWhiteSpace())
            {
                return "分类简介不能为空";
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
        public override async Task<JsonResult> Add(ProTypeInfo model)
        {
            AjaxOption<object> obj = new AjaxOption<object>();
            #region 验证相关
            obj.Msg = GetErrorMsg(model);
            //有错误信息
            if (!obj.Msg.IsNullOrWhiteSpace())
            {
                return Json(obj);
            }
            #endregion
            model.TDataStatus = StatusEnum.Normal;

            var modelId = await ProTypeInfoBLL.InsertAsync(model);
            if (modelId > 0)
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
        public override async Task<ActionResult> Edit(int id = 0)
        {
            if (id <= 0)
            {
                return RedirectToAction("Add");
            }
            var model = await ProTypeInfoBLL.GetAsync(id);
            if (model == null)
            {
                return RedirectToAction("Add");
            }
            return View(model);
        }
        [HttpPost]
        public override async Task<JsonResult> Edit(ProTypeInfo model)
        {
            AjaxOption<object> obj = new AjaxOption<object>();
            #region 验证相关
            //if (model.DId <= 0)
            //{
            //    obj.Msg = "编号必须大于0";
            //    return Json(obj);
            //}
            //if (model.DataStatus == StatusEnum.Init)
            //{
            //    model.DataStatus = StatusEnum.Normal;
            //}
            //obj.Msg = GetErrorMsg(model);
            ////有错误信息
            //if (!obj.Msg.IsNullOrWhiteSpace())
            //{
            //    return Json(obj);
            //}
            #endregion
            //var ProTypeInfo = await ProTypeInfoBLL.UpdateAsync(model);
            //if (ProTypeInfo != null)
            //{
            //    obj.Status = true;
            //    obj.Msg = "更新成功";
            //}
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
            //if (ids.IsNullOrWhiteSpace())
            //{
            //    obj.Msg = "选中项不能为空";
            //    return Json(obj);
            //}
            //int i = await ProTypeInfoBLL.ExecuteAsync("update ProTypeInfo set DataStatus=@DataStatus where DId in @DIds", new
            //{
            //    DataStatus = status,
            //    DIds = ids.SplitToIntList()
            //});
            //obj.Status = true;
            //obj.Msg = string.Format("更新了 {0} 条数据", i);
            return Json(obj);
        }

        /// <summary>
        /// 查询笔记页面
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
            return Content(await ProTypeInfoBLL.QueryAsync(model));
        }

        /// <summary>
        /// 加载商品分类或者配件分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetProTypes(int id)
        {
            AjaxOption<object> obj = new AjaxOption<object>();
            if (id != 1 && id != 2)
            {
                obj.Msg = "分类级别不正确";
                return Json(obj);
            }
            else
            {
                var list = new List<IEnumerable<ProTypeInfo>>();
                var data = await ProTypeInfoBLL.QueryAsync("select TId,TName,TFloor from ProTypeInfo where TGroupType=@TGroupType and TDataStatus<>99 and TFloor<3", new { TGroupType = id });
                if (data.ExistsData())
                {
                    list.Add(data.Where(t => t.TFloor == 1));
                    list.Add(data.Where(t => t.TFloor == 2));
                }
                obj.Status = true;
                obj.Data = list;
                return Json(obj);
            }
        }
    }
}