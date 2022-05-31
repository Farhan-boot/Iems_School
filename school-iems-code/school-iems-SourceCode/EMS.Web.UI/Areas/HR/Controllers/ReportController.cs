//File: UI Controller
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Web.Framework.Bases;
using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.Mvc;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.HR.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>

    public class ReportController : EmployeeBaseController
    {

        public ActionResult MonthlySalarySummary()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Reports.CanViewMonthlySalarySummaryReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult YearlySalarySummary()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Reports.CanViewYearlySalarySummaryReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult MonthlySalaryDetails()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Reports.CanViewMonthlySalaryDetailsReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

        public ActionResult MonthlySalaryReportWithBankDetails()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Reports.CanViewMonthlySalaryWithBankDetails))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }


        #region Monthly Salary Print Json

        public class MonthlySalaryPrintJson
        {
            public int EmployeeId { get; set; }
            public int UserId { get; set; }
            public string EmployeeName { get; set; }
            public string PayrollName { get; set; }
            public string Designation { get; set; }
            public string Remarks { get; set; }
            public List<HR_MonthlyPayslipDetailsJson> MonthlyPayslipDetailsList { get; set; }

            public MonthlySalaryPrintJson()
            {
                MonthlyPayslipDetailsList = new List<HR_MonthlyPayslipDetailsJson>();
            }
        }

        #endregion
        public ActionResult MonthlySalaryPrint(int id=0,int employeeId=0)
        {
            var result = new MvcModelResult<MonthlySalaryPrintJson>();
            if (id <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select a Valid Monthly Salary");
                return View(result);
            }

            if (employeeId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Select a Valid Employee");
                return View(result);
            }
            var employee = DbInstance.User_Employee
                .Include(x=>x.User_Account)
                .Include(x=>x.HR_Department)
                .Include(x=>x.HR_Position)
                .FirstOrDefault(x => x.Id == employeeId);
            if (employee == null)
            {
                result.HasError = true;
                result.Errors.Add("Employee Not Found.");
                return View(result);
            }
            result.Data = new MonthlySalaryPrintJson();

            result.Data.EmployeeName = employee.User_Account.FullName;
            result.Data.UserId = employee.UserId;
            result.Data.EmployeeId = employee.Id;
            result.Data.Designation = employee.HR_Position.Name + "(" +employee.HR_Department.ShortName+ ")";

            var monthlyPayslip = DbInstance.HR_MonthlyPayslip
                .Include(x=>x.HR_MonthlyPayslipDetails)
                .Include(x=>x.HR_Payroll)
                .FirstOrDefault(q => q.Id == id);

            if (monthlyPayslip == null)
            {
                result.HasError = true;
                result.Errors.Add("Monthly Payslip Not Found.");
                return View(result);
            }

            var payslipDetails = monthlyPayslip.HR_MonthlyPayslipDetails;
            var jsons = new List<HR_MonthlyPayslipDetailsJson>();
            payslipDetails.Map(jsons);

            result.Data.PayrollName = monthlyPayslip.HR_Payroll.Month.ToString() +", "+monthlyPayslip.HR_Payroll.Year;
            result.Data.Remarks = monthlyPayslip.Remarks;

            result.DataBag.TotalAddition = jsons.Where(x => x.IsAddition).Sum(x => x.Value);
            result.DataBag.TotalDeduction = jsons.Where(x => !x.IsAddition).Sum(x => x.Value);

            result.Data.MonthlyPayslipDetailsList = jsons;

            return View(result);

        }
    }
}
