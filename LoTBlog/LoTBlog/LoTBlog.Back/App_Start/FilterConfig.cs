using System.Web;
using System.Web.Mvc;

namespace LoTBlog.Back
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LoTBlog.Back.Models.MyExceptionFilterAttribute());
        }
    }
}