using System.Web.Http;
using System.Web.Mvc;

namespace EMS.Web.UI.Areas.Admission
{
    public class AdmissionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get {return "Admission";}
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admission_default",
                "Admission/{controller}/{action}/{id}",
                new { controller = "DashBoard", action = "Index", id = UrlParameter.Optional },
                new[] { "EMS.Web.UI.Areas.Admission.Controllers" }
            );
        }
    }
}