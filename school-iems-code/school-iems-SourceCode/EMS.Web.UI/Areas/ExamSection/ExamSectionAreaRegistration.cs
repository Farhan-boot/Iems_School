using System.Web.Mvc;

namespace EMS.Web.UI.Areas.ExamSection
{
    public class ExamSectionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ExamSection";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ExamSection_default",
                "ExamSection/{controller}/{action}/{id}",
                  //new { action = "Index", id = UrlParameter.Optional }
                  new { controller = "DashBoard", action = "Index", id = UrlParameter.Optional },
                  new[] { "EMS.Web.UI.Areas.ExamSection.Controllers" }
            );
        }
    }
}