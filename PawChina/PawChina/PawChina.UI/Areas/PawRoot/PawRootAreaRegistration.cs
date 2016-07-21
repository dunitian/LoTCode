using System.Web.Mvc;

namespace PawChina.UI.Areas.PawRoot
{
    public class PawRootAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PawRoot";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PawRoot_default",
                "PawRoot/{controller}/{action}/{id}",
                new { action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}