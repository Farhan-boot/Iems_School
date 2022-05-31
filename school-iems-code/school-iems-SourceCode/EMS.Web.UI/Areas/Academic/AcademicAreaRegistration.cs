using System.Web.Mvc;

namespace EMS.Web.UI.Areas.Academic
{
    public class AcademicAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Academic";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Academic_default",
                "Academic/{controller}/{action}/{id}",
                new { controller = "DashBoard", action = "Index", id = UrlParameter.Optional },
                new[] { "EMS.Web.UI.Areas.Academic.Controllers" }
            );
        }
    }
}