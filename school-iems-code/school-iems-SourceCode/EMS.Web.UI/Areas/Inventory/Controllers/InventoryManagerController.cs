using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using EMS.Web.Framework.Bases.Mvc;


namespace EMS.Web.UI.Areas.Inventory.Controllers
{
    public class InventoryManagerController : EmployeeBaseController
    {
        // GET: Inventory/ProductCardManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InventoryAddEdit()
        {
            return View();
        }
        public ActionResult InventoryList()
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            return View();
        }
        public ActionResult InventoryDetail()
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            return View();
        }

        




    }
}