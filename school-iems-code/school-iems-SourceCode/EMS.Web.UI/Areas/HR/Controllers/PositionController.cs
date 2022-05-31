//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Web.Framework.Bases;
using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.HR.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>

    public class PositionController : EmployeeBaseController
	{
    
        public ActionResult PositionAddEdit()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.PositionManager.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult PositionList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.PositionManager.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
	}
}
