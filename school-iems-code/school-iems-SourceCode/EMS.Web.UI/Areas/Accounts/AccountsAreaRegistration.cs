using System.Web.Mvc;

namespace EMS.Web.UI.Areas.Accounts
{
    public class AccountsAreaRegistration : AreaRegistration 
    {
        //public override Student Accounting
        public override string AreaName 
        {
            get 
            {
                return "Accounts";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
                "Accounts_default",
                "Accounts/{controller}/{action}/{id}",
                new { controller = "DashBoard", action = "Index", id = UrlParameter.Optional },
                new[] { "EMS.Web.UI.Areas.Accounts.Controllers" }
                );

        }
    }
}