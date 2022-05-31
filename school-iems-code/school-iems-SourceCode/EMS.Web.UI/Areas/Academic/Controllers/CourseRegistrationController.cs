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
using EMS.Framework.Permissions;
using EMS.Framework.Settings;
using EMS.Framework.Utils;

namespace EMS.Web.UI.Areas.Academic.Controllers
{
	/// <summary>
	/// You can add your custom code here.
	/// </summary>
	///[EmsAuthorize(User_Account.UserAuthorizeTypeEnum.Employee)]
	public class CourseRegistrationController : EmployeeBaseController
	{
	
		public ActionResult BulkCourseRegistration()
		{
			if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAddFromBulkRegistration))
			{
				return RedirectToAction("PermissionDenied", "Home", new { area = "" });
			}
			return View();
		}

        public ActionResult RepeaterCourseRegistration()
        {
            if (SiteSettings.GetInstituteKeyEnum != SiteSettings.InstituteKeyEnum.baust && !HttpUtil.IsSupperAdmin())
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

    }
}
