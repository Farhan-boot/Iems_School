using System.Web.Mvc;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Employee.Controllers
{
    public class ProfileController : EmployeeBaseController
    {
        // GET: Student/Profile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginHistory()
        {
            return View();
        }
    }
}