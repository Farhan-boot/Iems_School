using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Student.Controllers
{
    public class AgreementController : StudentBaseController
    {
        // GET: Student/Agreement
        public ActionResult Index()
        {
            return View();
        }
    }
}