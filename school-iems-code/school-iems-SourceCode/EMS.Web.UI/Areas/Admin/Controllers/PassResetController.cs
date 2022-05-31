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

namespace EMS.Web.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    [EmsMvcAuthorize(User_Account.UserTypeEnum.Employee)]
    public class PassResetController : BaseController
	{
        public ActionResult PassResetList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.AuditManager.CanViewPasswordChangeAudit))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
	}
}
