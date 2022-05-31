//File: UI Controller

using System.Web.Mvc;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;
using EMS.Web.Framework.Bases;

namespace EMS.Web.UI.Areas.HR.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    [EmsMvcAuthorize(User_Account.UserTypeEnum.Employee)]
    public class EmployeeController : BaseController
	{
    
        public ActionResult EmployeeAddEdit()
        {
            return View();
        }
        
	}
}
