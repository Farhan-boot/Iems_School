using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;

namespace EMS.Web.UI.Areas.Admin.Controllers
{
    [EmsMvcAuthorize(User_Account.UserTypeEnum.Employee)]
    public class LoginAuditController : BaseController
    {
        // GET: Admin/UserAccessControl
        public ActionResult LoginAuditList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.AuditManager.CanViewLoginAudit))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}