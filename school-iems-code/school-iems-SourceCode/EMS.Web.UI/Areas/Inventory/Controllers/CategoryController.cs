//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Web.Framework.Bases.Mvc;
using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;

namespace EMS.Web.UI.Areas.Inventory.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    ///[EmsAuthorize(User_Account.UserAuthorizeTypeEnum.Employee)]
    public class CategoryController : EmployeeBaseController
	{
    
        public ActionResult CategoryAddEdit()
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            return View();
        }
        public ActionResult CategoryList()
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            return View();
        }
	}
}
