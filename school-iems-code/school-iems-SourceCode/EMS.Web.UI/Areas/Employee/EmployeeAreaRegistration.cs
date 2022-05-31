using System.Web.Http;
using System.Web.Mvc;

namespace EMS.Web.UI.Areas.Employee
{
    public class EmployeeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Employee"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Employee_default",
                "Employee/{controller}/{action}/{id}",
                new { controller = "DashBoard", action = "Index", id = UrlParameter.Optional },
                new[] { "EMS.Web.UI.Areas.Employee.Controllers" }
            );
        }
    }
}