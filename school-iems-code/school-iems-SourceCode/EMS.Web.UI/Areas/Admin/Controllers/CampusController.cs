﻿//File: UI Controller
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

namespace EMS.Web.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    ///[EmsAuthorize(User_Account.UserAuthorizeTypeEnum.Employee)]
    public class CampusController : EmployeeBaseController
	{
    
        public ActionResult CampusAddEdit()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult CampusList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
	}
}
