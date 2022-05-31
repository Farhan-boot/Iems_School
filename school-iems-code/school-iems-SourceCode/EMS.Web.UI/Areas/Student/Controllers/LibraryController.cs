using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Student.Controllers
{
    public class LibraryController : StudentBaseController
    {
        // GET: Student/Library
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CurrentBorrows()
        {
            return View();
        }
        public ActionResult BorrowHistory()
        {
            return View();
        }
        public ActionResult Financials()
        {
            return View();
        }
        public ActionResult Reservation()
        {
            return View();
        }
    }
}