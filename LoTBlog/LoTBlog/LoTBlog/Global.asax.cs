using Spring.Web.Mvc;
using System.Web.Mvc;
using System.Web.Routing;

namespace LoTBlog
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : SpringMvcApplication//System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //配置log4net日志
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}