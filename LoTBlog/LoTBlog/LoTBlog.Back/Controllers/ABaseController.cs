using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Back.Controllers
{
    public class ABaseController : Controller
    {
        /// <summary>
        /// 检查是否登录
        /// </summary>
        /// <returns></returns>
        public string CheckLogin()
        {
            string msg = "false";
            return msg;
        }

        /// <summary>
        /// 记录敏感操作
        /// </summary>
        public void RecordAction(string strInfo)
        {

        }

    }
}
