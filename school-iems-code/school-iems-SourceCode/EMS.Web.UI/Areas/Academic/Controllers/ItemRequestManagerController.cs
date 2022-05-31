using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;
using EMS.Web.UI.Areas.Academic.Models;
using Microsoft.Ajax.Utilities;

namespace EMS.Web.UI.Areas.Academic.Controllers
{
    public class ItemRequestManagerController : EmployeeBaseController
    {
        // GET: Academic/ItemRequestManager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ItemRequestAddEdit()
        {
            return View();
        }


        public ActionResult ItemRequestList()
        {
            return View();
        }


        public ActionResult AcceptedItemList()
        {
            return View();
        }


        public ActionResult AssginToParsonList()
        {
            return View();
        }



    }
}