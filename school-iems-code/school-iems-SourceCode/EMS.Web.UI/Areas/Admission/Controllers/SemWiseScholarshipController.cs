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
using EMS.Framework.Utils;

namespace EMS.Web.UI.Areas.Admission.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    ///[EmsAuthorize(User_Account.UserAuthorizeTypeEnum.Employee)]
    public class SemWiseScholarshipController : EmployeeBaseController
	{

        public ActionResult IndividualStudentSettingsList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.SemesterWiseScholarship.CanViewFirstSemesterScholarship))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

        public ActionResult IndividualStudentSettingsList2()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.SemesterWiseScholarship.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

        public ActionResult ScholarshipEligibilityManager()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.SemesterWiseScholarship.CanCheckScholarshipEligibility))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

        


    }
}
