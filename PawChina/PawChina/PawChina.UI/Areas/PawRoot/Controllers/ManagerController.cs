using PawChina.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PawChina.UI.Areas.PawRoot.Controllers
{
    public class ManagerController : BaseController
    {
        #region 登录页面
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> LoginOn(ChineseInfo userInfo)
        {
            var data = new AjaxOption<ChineseInfo>();

            #region 验证系列
            if (userInfo == null || userInfo.PawName.IsNullOrWhiteSpace() || userInfo.PawPass.IsNullOrWhiteSpace())
            {
                data.Msg = "用户名或密码错误！";
                return Json(data);
            }

            var list = await DapperDataAsync.QueryAsync<ChineseInfo>("select top 1 PawGid,PawName,PawPass,PawCreateTime,PawEmail from ChineseInfo where PawName=@PawName and PawDataStatus=1", new { PawName = userInfo.PawName });
            if (!list.ExistsData())
            {
                data.Msg = "用户名不存在！";
                return Json(data);
            }

            var model = list.FirstOrDefault();
            userInfo.PawPass = SafeHelper.GetShaOne(userInfo.PawPass);
            if (userInfo.PawPass != model.PawPass)
            {
                data.Msg = "密码错误！";
                return Json(data);
            }

            Session["PawChina"] = model;//设置Session
            #endregion

            data.Status = true;
            return Json(data);
        }
        /// <summary>
        /// 登出操作
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            Session["PawChina"] = null;
            return RedirectToAction("Login", "Manager");
        }
        #endregion

        /// <summary>
        /// 后台管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 统计页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Total()
        {
            return View();
        }
    }
}