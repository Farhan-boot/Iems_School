using System.Web.Mvc;

namespace EMS.Web.UI.Areas.BasicAccounting
{
    public class CompanyAccountingAreaRegistration : AreaRegistration 
    {
        
        public override string AreaName 
        {
            get 
            {
                return "BasicAccounting";
            }
        }
        //public override void RegisterArea(AreaRegistrationContext context)
        //{

        //    context.MapRoute(
        //        "Accounts_default",
        //        "Accounts/{controller}/{action}/{id}",
        //        new { controller = "DashBoard", action = "Index", id = UrlParameter.Optional },
        //        new[] { "EMS.Web.UI.Areas.Accounts.Controllers" }
        //        );

        //}
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BasicAccounting_default",
                "BasicAccounting/{controller}/{action}/{id}",
                //new { action = "Index", id = UrlParameter.Optional },
                new { controller = "DashBoard", action = "Index", id = UrlParameter.Optional },
                new[] { "EMS.Web.UI.Areas.BasicAccounting.Controllers" }
            );
        }
    }
}