using PawChina.Model;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace PawChina.UI.Areas.PawRoot.Controllers
{
    public abstract class AdminController<T> : BaseController
    {
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        public abstract ActionResult Index();

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <returns></returns>
        public abstract ActionResult Add();

        [HttpPost]
        public abstract Task<JsonResult> Add(T model);


        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <returns></returns>
        public abstract Task<ActionResult> Edit(int id = 0);

        [HttpPost]
        public abstract Task<JsonResult> Edit(T model);

        /// <summary>
        /// 批量更新~恢复或者删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public abstract Task<JsonResult> UpdateList(string ids, StatusEnum status);

        /// <summary>
        /// 列表查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public abstract Task<ActionResult> Query(QueryModel model);

    }
}