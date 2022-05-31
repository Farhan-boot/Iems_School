using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using EMS.Web.Framework.Bases.Mvc;


namespace EMS.Web.UI.Areas.Inventory.Controllers
{
    public class RequisitionManagerController : EmployeeBaseController
    {
        // GET: Requisition/RequisitionManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RequisitionAddEdit()
        {
            return View();
        }
        public ActionResult RequisitionList()
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            return View();
        }
        public ActionResult RequisitionDetail()
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            return View();
        }

        




    }
}