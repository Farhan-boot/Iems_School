using System.Web.Http;
using System.Web.Mvc;

namespace EMS.Web.UI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Admin"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "DashBoard", action = "Index", id = UrlParameter.Optional },
                new[] { "EMS.Web.UI.Areas.Admin.Controllers" }
                );
        }
    }
}