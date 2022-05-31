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
using EMS.Framework.Utils;

namespace EMS.Web.UI.Areas.Academic.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    ///[EmsAuthorize(User_Account.UserAuthorizeTypeEnum.Employee)]
    public class CurriculumController : EmployeeBaseController
	{
    
        public ActionResult CurriculumAddEdit()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult CurriculumList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult CurriculumPrint()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
	}
}
