using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Student.Controllers
{
    public class MessageController : StudentBaseController
    {
        // GET: Student/Message
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VeiwMessage()
        {
            return View();
        }
        public ActionResult ComposeMessage()
        {
            return View();
        }
    }
}