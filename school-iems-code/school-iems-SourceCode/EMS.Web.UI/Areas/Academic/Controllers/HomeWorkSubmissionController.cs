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

namespace EMS.Web.UI.Areas.Academic.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    ///[EmsAuthorize(User_Account.UserAuthorizeTypeEnum.Employee)]
    public class HomeWorkSubmissionController : EmployeeBaseController
	{
    
        public ActionResult HomeWorkSubmissionAddEdit()
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            return View();
        }
        public ActionResult HomeWorkSubmissionList()
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            return View();
        }
	}
}
