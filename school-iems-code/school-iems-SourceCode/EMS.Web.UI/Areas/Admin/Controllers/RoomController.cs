//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using EMS.Web.Framework.Bases.Mvc;
using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Admin.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    ///[EmsAuthorize(User_Account.UserAuthorizeTypeEnum.Employee)]
    public class RoomController : EmployeeBaseController
	{

        public ActionResult RoomAddEdit()
        {
            string error = "";
            var modelResult = new MvcModelResult();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanView, out error)
              && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanAdd, out error)
             && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanEdit, out error))
            {
                modelResult.HasError = true;
                modelResult.HasViewPermission = false;
                modelResult.Errors.Add(error);
           
            }
            return View(modelResult);
        }
        public ActionResult RoomList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult RoomStatus()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
	}
}
