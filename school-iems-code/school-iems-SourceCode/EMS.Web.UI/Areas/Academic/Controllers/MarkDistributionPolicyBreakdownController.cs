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
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Academic.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    public class MarkDistributionPolicyBreakdownController : EmployeeBaseController
    {
    
        public ActionResult MarkDistributionPolicyBreakdownAddEdit()
        {
            return View();
        }
        public ActionResult MarkDistributionPolicyBreakdownList()
        {
            return View();
        }
	}
}
