using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using EMS.Web.Framework.Bases.Mvc;


namespace EMS.Web.UI.Areas.Inventory.Controllers
{
    public class ProductCategoryManagerController : EmployeeBaseController
    {
        // GET: Inventory/ProductCardManager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductCategoryAddEdit()
        {
            return View();
        }
        public ActionResult ProductCategoryList()
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            return View();
        }
        public ActionResult ProductCategoryDetail()
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            return View();
        }

        




    }
}