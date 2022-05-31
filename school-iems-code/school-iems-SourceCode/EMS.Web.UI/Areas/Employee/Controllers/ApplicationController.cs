using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Employee.Controllers
{
    public class ApplicationController : EmployeeBaseController
    {
        // GET: Library/Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewIdCardDemandForm()
        {
            return View();
        }

    }
}