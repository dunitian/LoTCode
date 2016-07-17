using System.Web;
using System.Web.Mvc;

namespace LoTBlog
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LoTBlog.Models.MyExceptionFilterAttribute());
        }
    }
}