using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;
using Microsoft.Ajax.Utilities;
using MvcSiteMapProvider.Linq;
using System.Diagnostics;

namespace EMS.Web.UI.Areas.HR.Controllers.WebApi
{
    public class MonthlySalaryReportApiController : EmployeeBaseApiController
    {
        #region Monthly Salary Details Report
        //MonthlySalaryDetailsReport
        public HttpResult GetMonthlySalaryDetailsReportDataExtra()
        {
            string error = string.Empty;
            var result = new HttpResult();
            try
            {
                var payrollList = DbInstance.HR_Payroll.Where(x => !x.IsDeleted).OrderByDescending(x => x.Year).ThenByDescending(x => x.MonthEnumId).AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.Month.ToString() + ", " + t.Year, IsCurrent = t.IsCurrent }).ToList();
                result.DataExtra.PayrollList = payrollList;

                result.DataExtra.SelectedPayrollId = payrollList.FirstOrDefault(x => x.IsCurrent)?.Id;

                if (result.DataExtra.PayrollList.Count > 0)
                {
                    result.DataExtra.SalaryTemplateList = DbInstance.HR_SalaryTemplate.AsEnumerable()
                        .Select(t => new
                        {
                            Id = t.Id,
                            Name = t.Name
                        }).ToList();
                }
                else
                {
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add("No Payroll Found. Please Create proper payroll first.");
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public class MonthlySalaryDetailReportJson
        {
            public List<string> SalaryHeadList { get; set; }
            public List<double> TotalList { get; set; }
            public List<EmployeeSalaryJson> EmployeeList { get; set; }

            public string NetTotalInWords { get; set; }
            public MonthlySalaryDetailReportJson()
            {
                SalaryHeadList = new List<string>();
                TotalList = new List<double>();
                EmployeeList = new List<EmployeeSalaryJson>();
            }
        }

        public class EmployeeSalaryJson
        {
            public int EmployeeId { get; set; }
            public int UserId { get; set; }
            public string EmployeeName { get; set; }
            public DateTime JoiningDate { get; set; }
            public string LengthOfService { get; set; }
            public DateTime? NextPromotionDate { get; set; }
            public string Designation { get; set; }
            public List<double> SalaryValueList { get; set; }
            public EmployeeSalaryJson()
            {
                EmployeeId = 0;
                UserId = 0;
                EmployeeName = "";
                Designation = "";
                LengthOfService = "";
                SalaryValueList = new List<double>();
            }
        }

        public HttpResult<MonthlySalaryDetailReportJson> GetMonthlySalaryDetailsReport(
            int payrollId = -1,
            int salaryTemplateId = -1
        )
        {
            var result = new HttpResult<MonthlySalaryDetailReportJson>();
            string error = String.Empty;

            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Reports.CanViewMonthlySalaryDetailsReport, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            if (salaryTemplateId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select a Valid Salary Group.");
                return result;
            }

            if (payrollId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select a Valid Payroll.");
                return result;
            }
            try
            {
                
                IQueryable<HR_MonthlyPayslipDetails> query;

                query = DbInstance.HR_MonthlyPayslipDetails
                    .Include(x=>x.HR_MonthlyPayslip.HR_SalarySettings)
                    .Include(x=>x.User_Employee.HR_Position)
                    .Include(x=>x.User_Employee.HR_Department)
                    .Include(x=>x.User_Employee.User_Account)
                    .Where(x => !x.IsDeleted && !x.HR_MonthlyPayslip.IsDraft && x.HR_MonthlyPayslip.PayrollId == payrollId);
                
                if (salaryTemplateId > 0)
                {
                    query = query.Where(x => x.User_Employee.HR_Position.SalaryTemplateId == salaryTemplateId);
                }

                #region Creating Neccessary list

                List<HR_MonthlyPayslipDetails> monthlySalaryDetailList = query.OrderBy(x => x.OrderNo).ThenBy(x => x.CategoryTypeEnumId).ToList();

                if (monthlySalaryDetailList.Count <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("No Salary Found with your search.");
                    return result;
                }
                var distinctSalaryCategoryTypeList = monthlySalaryDetailList.DistinctBy(x => x.Name.Trim().ToLower())
                    .Select(x => new
                    {
                        SalaryHeadName = x.Name,
                        CategoryTypeEnumId = x.CategoryTypeEnumId
                    }).ToList();

                var additionTypeHeadNameList = distinctSalaryCategoryTypeList.Where(x => x.CategoryTypeEnumId >= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.BasicSalary 
                    && x.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherAdjustment
                    && x.CategoryTypeEnumId != (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.PreviousAdjustment
                    ).Select(x=>x.SalaryHeadName).ToList();

                var previousAdjustmentHeadNameList = distinctSalaryCategoryTypeList
                    .Where(x => x.CategoryTypeEnumId == (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.PreviousAdjustment)
                    .Select(x => x.SalaryHeadName).ToList();

                var deductionTypeHeListList = distinctSalaryCategoryTypeList.Where(x => x.CategoryTypeEnumId >= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.TaxDeduction &&
                    x.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherDeduction).Select(x => x.SalaryHeadName).ToList();

                var distinctEmployeeIdList = monthlySalaryDetailList.OrderBy(x => x.User_Employee.HR_Position.Priority)
                    .ThenBy(x=>x.User_Employee.HR_Department.DepartmentNo)
                    .ThenBy(x=>x.User_Employee.User_Account.JoiningDate).DistinctBy(x => x.EmployeeId)
                    .Select(x => x.EmployeeId).ToList();

                #endregion


                var monthlySalaryDetailJson = new MonthlySalaryDetailReportJson();
                var totalSalaryHead = distinctSalaryCategoryTypeList.Count + 2;
                for (int i = 0; i < totalSalaryHead; i++)
                {
                    monthlySalaryDetailJson.TotalList.Add(0.0f);
                }
                
                #region Creating Employee List with Data.

                bool isSalaryHeadAdded = false;
                
                foreach (var employeeId in distinctEmployeeIdList)
                {
                    var employeeSalaryJson = new EmployeeSalaryJson();
                    var totalSalaryIndex = 0;

                    #region adding Basic Information

                    employeeSalaryJson.EmployeeId = employeeId;
                    employeeSalaryJson.UserId = monthlySalaryDetailList.Where(x => x.EmployeeId == employeeId)
                        .Select(x => x.User_Employee.UserId).FirstOrDefault();
                    employeeSalaryJson.EmployeeName = monthlySalaryDetailList.Where(x => x.EmployeeId == employeeId)
                        .Select(x => x.User_Employee.User_Account.FullName).FirstOrDefault();

                    var designationWithDept = monthlySalaryDetailList.Where(x => x.EmployeeId == employeeId)
                        .Select(x => x.User_Employee.HR_Position.Name).FirstOrDefault() + "(" + monthlySalaryDetailList.Where(x => x.EmployeeId == employeeId)
                        .Select(x => x.User_Employee.HR_Department.ShortName).FirstOrDefault() + ")";

                    employeeSalaryJson.Designation = designationWithDept;

                    employeeSalaryJson.JoiningDate = monthlySalaryDetailList.Where(x => x.EmployeeId == employeeId)
                        .Select(x => x.User_Employee.User_Account.JoiningDate).FirstOrDefault();

                    employeeSalaryJson.NextPromotionDate = monthlySalaryDetailList
                         .Where(x => x.EmployeeId == employeeId)
                         .Select(x => x.HR_MonthlyPayslip.HR_SalarySettings.NextPromotionDate).FirstOrDefault();

                    var totalMonths = ((DateTime.Now.Year - employeeSalaryJson.JoiningDate.Year)*12) + DateTime.Now.Month - employeeSalaryJson.JoiningDate.Month;

                    if (totalMonths > 0)
                    {
                        var totalYears = totalMonths / 12;

                        if (totalYears > 0)
                        {
                            employeeSalaryJson.LengthOfService += totalYears + " Year ";
                        }

                        totalMonths %= 12;

                        if (totalMonths > 0)
                        {
                            employeeSalaryJson.LengthOfService += totalMonths + " Months ";
                        }
                    }
                    else
                    {
                        employeeSalaryJson.LengthOfService += "Joined This Month";
                    }


                    #endregion

                    #region Adding Addition Values

                    var grossSalary = 0.0f;
                    foreach (var additionTypeHeadName in additionTypeHeadNameList)
                    {
                        var salaryDetailList = monthlySalaryDetailList.Where(x =>
                            x.Name.ToLower().Trim() == additionTypeHeadName.ToLower().Trim() && x.EmployeeId == employeeId).ToList();
                        if (!isSalaryHeadAdded)
                        {
                            monthlySalaryDetailJson.SalaryHeadList.Add(additionTypeHeadName);
                        }
                        if (salaryDetailList.Count > 0)
                        {
                            var categoryWiseTotalAmount = 0.0f;
                            foreach (var salaryDetail in salaryDetailList)
                            {
                                categoryWiseTotalAmount += salaryDetail.Value;
                            }
                            employeeSalaryJson.SalaryValueList.Add(categoryWiseTotalAmount);

                            grossSalary += categoryWiseTotalAmount;
                        }
                        else
                        {
                            employeeSalaryJson.SalaryValueList.Add(0);
                        }

                        monthlySalaryDetailJson.TotalList[totalSalaryIndex] += employeeSalaryJson.SalaryValueList[totalSalaryIndex];

                        totalSalaryIndex++;
                    }

                    if (!isSalaryHeadAdded)
                    {
                        monthlySalaryDetailJson.SalaryHeadList.Add("Gross Salary & Allowance");
                    }

                    employeeSalaryJson.SalaryValueList.Add(grossSalary);

                    monthlySalaryDetailJson.TotalList[totalSalaryIndex] += employeeSalaryJson.SalaryValueList[totalSalaryIndex];

                    totalSalaryIndex++;

                    #endregion

                    #region Previous Adjustments

                    var previousAdjustment = 0.0f;
                    foreach (var previousAdjustmentHeadName in previousAdjustmentHeadNameList)
                    {
                        var salaryDetailList = monthlySalaryDetailList.Where(x =>
                            x.Name.ToLower().Trim() == previousAdjustmentHeadName.ToLower().Trim() && x.EmployeeId == employeeId).ToList();
                        if (!isSalaryHeadAdded)
                        {
                            monthlySalaryDetailJson.SalaryHeadList.Add(previousAdjustmentHeadName);
                        }
                        if (salaryDetailList.Count > 0)
                        {
                            foreach (var salaryDetail in salaryDetailList)
                            {
                                previousAdjustment += salaryDetail.Value;
                            }
                            employeeSalaryJson.SalaryValueList.Add(previousAdjustment);
                        }
                        else
                        {
                            employeeSalaryJson.SalaryValueList.Add(0);
                        }

                        monthlySalaryDetailJson.TotalList[totalSalaryIndex] += employeeSalaryJson.SalaryValueList[totalSalaryIndex];

                        totalSalaryIndex++;
                    }

                    #endregion

                    #region Adding Deduction Values

                    var grossDeduction = 0.0f;
                    foreach (var deductionTypeHeadName in deductionTypeHeListList)
                    {
                        var salaryDetailList = monthlySalaryDetailList.Where(x =>
                            x.Name.ToLower().Trim() == deductionTypeHeadName.ToLower().Trim() && x.EmployeeId == employeeId).ToList();
                        
                        if (!isSalaryHeadAdded)
                        {
                            monthlySalaryDetailJson.SalaryHeadList.Add(deductionTypeHeadName);
                        }

                        if (salaryDetailList.Count > 0)
                        {
                            var categoryWiseTotalAmount = 0.0f;
                            foreach (var salaryDetail in salaryDetailList)
                            {
                                categoryWiseTotalAmount += salaryDetail.Value;
                            }
                            employeeSalaryJson.SalaryValueList.Add(categoryWiseTotalAmount);
                            grossDeduction += categoryWiseTotalAmount;
                        }
                        else
                        {
                            employeeSalaryJson.SalaryValueList.Add(0);
                        }
                        monthlySalaryDetailJson.TotalList[totalSalaryIndex] += employeeSalaryJson.SalaryValueList[totalSalaryIndex];

                        totalSalaryIndex++;
                    }

                    if (!isSalaryHeadAdded)
                    {
                        monthlySalaryDetailJson.SalaryHeadList.Add("Net Salary and Allowance");
                    }

                    employeeSalaryJson.SalaryValueList.Add(grossSalary + previousAdjustment - grossDeduction);

                    monthlySalaryDetailJson.TotalList[totalSalaryIndex] += employeeSalaryJson.SalaryValueList[totalSalaryIndex];

                    #endregion

                    monthlySalaryDetailJson.EmployeeList.Add(employeeSalaryJson);

                    isSalaryHeadAdded = true;
                }

                #endregion

                monthlySalaryDetailJson.NetTotalInWords = StringHelper.ToWordsInBDT(monthlySalaryDetailJson.TotalList[totalSalaryHead - 1]);

                result.Data = monthlySalaryDetailJson;

                return result;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().ToString());
                return result;
            }
        }

        /*
        private List<ParticularWiseCollectionSummaryJson> GetParticularWiseCollectionSummaryData(List<Acnt_StdTransaction> transactionList, int particularId)
        {
            List<Acnt_StdTransactionDetail> transDtlList = new List<Acnt_StdTransactionDetail>();

            foreach (var trans in transactionList)
            {
                transDtlList.AddRange(trans.Acnt_StdTransactionDetail);
            }

            // TransactionDetail Distinct
            var distincTransDtlList = transDtlList?.DistinctBy(x => x.ParticularId).ToList();

            //Only Selected Particular Name
            if (particularId > 0)
            {
                distincTransDtlList = distincTransDtlList.Where(x => x.ParticularId == particularId).ToList();
            }

            var summaryDataList = new List<ParticularWiseCollectionSummaryJson>();

            foreach (var transDtl in distincTransDtlList)
            {
                var summaryData = ParticularWiseCollectionSummaryJson.GetNew();

                summaryData.ParticularName = transDtl.Name;
                summaryData.TotalAmount = (decimal)transDtlList.Where(x => x.ParticularId == transDtl.ParticularId)
                    .Sum(x => x.Amount);

                summaryDataList.Add(summaryData);

                //Debug.WriteLine(summaryData.TotalAmount);
            }

            return summaryDataList;
        }
*/

        #endregion


        #region Monthly Salary Summary Report
        //MonthlySalarySummaryReport
        public HttpResult GetMonthlySalarySummaryReportDataExtra()
        {
            string error = string.Empty;
            var result = new HttpResult();
            try
            {
                var payrollList = DbInstance.HR_Payroll.Where(x => !x.IsDeleted).OrderByDescending(x => x.Year).ThenByDescending(x => x.MonthEnumId).AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Month.ToString() + ", " + t.Year, IsCurrent = t.IsCurrent }).ToList();
                result.DataExtra.PayrollList = payrollList;

                result.DataExtra.SelectedPayrollId = payrollList.FirstOrDefault(x => x.IsCurrent)?.Id;

                if (result.DataExtra.PayrollList.Count <= 0)
                {
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add("No Payroll Found. Please Create proper payroll first.");
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public class MonthlySalarySummaryReportJson
        {
            public int PayrollId { get; set; }
            public double TotalGrossSalary { get; set; }
            public double TotalTaxDeduction { get; set; }
            public double TotalArrearSalary { get; set; }
            public double TotalDeduction { get; set; }
            public double TotalRevenueStamp { get; set; }
            public double TotalNetSalary { get; set; }

            public string TotalNetSalaryInWords
            {
                get
                {
                    return StringHelper.ToWordsInBDT(TotalNetSalary);
                }
            }

            public List<SalaryGroupJson> SalaryList { get; set; }

            public MonthlySalarySummaryReportJson()
            {
                SalaryList = new List<SalaryGroupJson>();
            }

        }

        public class SalaryGroupJson
        {
            public string SalaryGroupName { get; set; }
            public double GrossTotalSalary { get; set; }
            public double TaxDeductionAtSource { get; set; }
            public double ArrearSalary { get; set; }
            public double Deduction { get; set; }
            public double RevenueStamp { get; set; }
            public double NetSalary { get; set; }
        }


        public HttpResult<MonthlySalarySummaryReportJson> GetMonthlySalarySummaryReport(
            int payrollId = -1
        )
        {
            var result = new HttpResult<MonthlySalarySummaryReportJson>();
            string error = String.Empty;

            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Reports.CanViewMonthlySalarySummaryReport, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            if (payrollId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select a Valid Payroll.");
                return result;
            }
            try
            {

                IQueryable<HR_MonthlyPayslipDetails> query;

                query = DbInstance.HR_MonthlyPayslipDetails
                    .Include(x=>x.User_Employee.HR_Position)
                    .Where(x => !x.IsDeleted && !x.HR_MonthlyPayslip.IsDraft && x.HR_MonthlyPayslip.PayrollId == payrollId);

                #region Creating Neccessary list

                List<HR_MonthlyPayslipDetails> monthlySalarySummaryList = query.ToList();
                
                if (monthlySalarySummaryList.Count <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("No Salary Found with your search.");
                    return result;
                }
                var salaryGroupList = DbInstance.HR_SalaryTemplate.Where(x=>!x.IsDeleted && x.IsEnable).OrderBy(x=>x.OrderNo).ToList();

                #endregion
                
                var monthlySalarySummaryJson = new MonthlySalarySummaryReportJson();

                monthlySalarySummaryJson.PayrollId = payrollId;

                foreach (var salaryGroup in salaryGroupList)
                {
                    var salaryGroupJson = new SalaryGroupJson();
                    
                    salaryGroupJson.SalaryGroupName = salaryGroup.Name;

                    salaryGroupJson.GrossTotalSalary = monthlySalarySummaryList.Where(x=> x.User_Employee.HR_Position.SalaryTemplateId == salaryGroup.Id && x.CategoryTypeEnumId >= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.BasicSalary &&
                        x.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherAdjustment 
                        && x.CategoryTypeEnumId !=(byte) HR_SalaryTemplateDetails.CategoryTypeEnum.PreviousAdjustment).Sum(x=>x.Value);


                    salaryGroupJson.TaxDeductionAtSource = monthlySalarySummaryList.Where(x => x.User_Employee.HR_Position.SalaryTemplateId == salaryGroup.Id
                        && x.CategoryTypeEnumId == (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.TaxDeduction).Sum(x => x.Value); ;

                    salaryGroupJson.ArrearSalary = monthlySalarySummaryList.Where(x => x.User_Employee.HR_Position.SalaryTemplateId == salaryGroup.Id
                        && x.CategoryTypeEnumId == (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.PreviousAdjustment).Sum(x => x.Value);

                    salaryGroupJson.Deduction = monthlySalarySummaryList.Where(x => x.User_Employee.HR_Position.SalaryTemplateId == salaryGroup.Id
                        && x.CategoryTypeEnumId > (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.TaxDeduction &&
                                                x.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherDeduction
                                                && x.CategoryTypeEnumId != (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.TaxDeduction
                                                && x.CategoryTypeEnumId != (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.PreviousAdjustment
                                                && x.CategoryTypeEnumId != (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.RevenueStamp
                        ).Sum(x => x.Value);

                    salaryGroupJson.RevenueStamp = monthlySalarySummaryList.Where(x => x.User_Employee.HR_Position.SalaryTemplateId == salaryGroup.Id
                        && x.CategoryTypeEnumId == (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.RevenueStamp).Sum(x => x.Value);


                    salaryGroupJson.NetSalary = (salaryGroupJson.GrossTotalSalary + salaryGroupJson.ArrearSalary) - (salaryGroupJson.TaxDeductionAtSource+ salaryGroupJson.Deduction+ salaryGroupJson.RevenueStamp);

                    monthlySalarySummaryJson.SalaryList.Add(salaryGroupJson);
                }

                monthlySalarySummaryJson.TotalGrossSalary = monthlySalarySummaryJson.SalaryList.Sum(x=>x.GrossTotalSalary);
                monthlySalarySummaryJson.TotalTaxDeduction = monthlySalarySummaryJson.SalaryList.Sum(x => x.TaxDeductionAtSource);
                monthlySalarySummaryJson.TotalArrearSalary = monthlySalarySummaryJson.SalaryList.Sum(x => x.ArrearSalary);
                monthlySalarySummaryJson.TotalDeduction = monthlySalarySummaryJson.SalaryList.Sum(x => x.Deduction);
                monthlySalarySummaryJson.TotalRevenueStamp = monthlySalarySummaryJson.SalaryList.Sum(x => x.RevenueStamp);
                monthlySalarySummaryJson.TotalNetSalary = monthlySalarySummaryJson.SalaryList.Sum(x => x.NetSalary);

                result.Data = monthlySalarySummaryJson;

                return result;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().ToString());
                return result;
            }
        }

        #endregion


        #region Monthly Salary Report With Bank Details Json
        public class MonthlySalaryReportWithBankDetailsJson
        {
            public int EmployeeId { get; set; }
            public int UserId { get; set; }
            public string FullName { get; set; }
            public string BankAccountNo { get; set; }
            public double Amount { get; set; }
        }

        public HttpListResult<MonthlySalaryReportWithBankDetailsJson> GetMonthlySalaryReportWithBankDetails(
            int payrollId = -1
        )
        {
            var result = new HttpListResult<MonthlySalaryReportWithBankDetailsJson>();
            string error = String.Empty;

            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Reports.CanViewMonthlySalaryWithBankDetails, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            if (payrollId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select a Valid Payroll.");
                return result;
            }
            try
            {
                var monthlySalaryReportWithBankDetailList = DbInstance.HR_MonthlyPayslip
                    .Include(x=>x.User_Employee.User_Account)
                    .Where(x => x.PayrollId == payrollId 
                                && !x.IsDeleted && x.User_Employee.User_Account.BankAccountNo !=null && x.User_Employee.User_Account.BankAccountNo.Trim() != "")
                    .OrderBy(x=>x.User_Employee.HR_Position.Priority)
                    .ThenBy(x => x.User_Employee.HR_Department.DepartmentNo)
                    .ThenBy(x=>x.User_Employee.User_Account.JoiningDate)
                    .Select(x =>
                        new MonthlySalaryReportWithBankDetailsJson
                        {
                            EmployeeId = x.EmployeeId,
                            UserId = x.User_Employee.User_Account.Id,
                            FullName = x.User_Employee.User_Account.FullName,
                            BankAccountNo = x.User_Employee.User_Account.BankAccountNo,
                            Amount = (x.TotalAddition-x.TotalDeduction)
                        }).ToList();

                if (monthlySalaryReportWithBankDetailList.Count <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("No Salary Found with your selected Payroll.");
                    return result;
                }

                result.Data = monthlySalaryReportWithBankDetailList;

                result.DataExtra.GrandTotal = monthlySalaryReportWithBankDetailList.Sum(x => x.Amount);
                result.DataExtra.GrandTotalInWords = StringHelper.ToWordsInBDT(result.DataExtra.GrandTotal);

                return result;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().ToString());
                return result;
            }
        }

        #endregion

        #region Yearly Salary Summary Report
        public class YearlySalarySummaryReportJson
        {
            public List<string> PayrollNameList { get; set; }
            public List<YearlySalaryGroupJson> SalaryGroupList { get; set; }
            public List<double> GrossSalaryList { get; set; }
            public List<double> ArrearSalaryList { get; set; }
            public List<double> DeductionList { get; set; }
            public List<double> RevenueStampList { get; set; }
            public List<double> TaxList { get; set; }
            public List<double> NetTotalList { get; set; }
            public YearlySalarySummaryReportJson()
            {
                NetTotalList = new List<double>();
                GrossSalaryList = new List<double>();
                ArrearSalaryList = new List<double>();
                DeductionList = new List<double>();
                RevenueStampList = new List<double>();
                TaxList = new List<double>();

                PayrollNameList = new List<string>();

                SalaryGroupList = new List<YearlySalaryGroupJson>();
            }

        }
        public class YearlySalaryGroupJson
        {
            public string SalaryGroupName { get; set; }
            public List<double> PayrollWiseAmountList { get; set; }
            public double NetTotalSalary { get; set; }

            public YearlySalaryGroupJson()
            {
                PayrollWiseAmountList = new List<double>();
                NetTotalSalary = 0;
            }
        }

        public HttpResult<YearlySalarySummaryReportJson> GetYearlySalarySummaryReport(
            int startPayrollId = -1,
            int endPayrollId = -1
        )
        {
            var result = new HttpResult<YearlySalarySummaryReportJson>();
            string error = String.Empty;

            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Reports.CanViewYearlySalarySummaryReport, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            if (startPayrollId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select a Start Payroll.");
                return result;
            }
            if (endPayrollId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select a End Payroll.");
                return result;
            }
            try
            {
                var startPayroll = DbInstance.HR_Payroll.FirstOrDefault(x => x.Id == startPayrollId);

                if (startPayroll == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Please Select a Start Payroll.");
                    return result;
                }
                var startPayrollMonth = new DateTime(startPayroll.Year, startPayroll.MonthEnumId, 1);

                var endPayroll = DbInstance.HR_Payroll.FirstOrDefault(x => x.Id == endPayrollId);
                if (endPayroll == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Please Select a End Payroll.");
                    return result;
                }
                var endPayrollMonth = new DateTime(endPayroll.Year, endPayroll.MonthEnumId, 1);

                if ((startPayroll.MonthEnumId > endPayroll.MonthEnumId && startPayroll.Year == endPayroll.Year) || startPayroll.Year>endPayroll.Year)
                {
                    result.HasError = true;
                    result.Errors.Add("Start Payroll Cannot be Greater than End Payroll.");
                    return result;
                }

                var allPayrollList = DbInstance.HR_Payroll.Where(x=>!x.IsDeleted).ToList();


                var selectedPayrollList = allPayrollList
                    .Where(x => x.PayrollMonthYear >= startPayrollMonth && x.PayrollMonthYear <= endPayrollMonth)
                    .OrderBy(x => x.Year)
                    .ThenBy(x => x.MonthEnumId)
                    .ToList();

                var payrollIdsList = selectedPayrollList.Select(x => x.Id).ToList();

                if (selectedPayrollList.Count <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("No Published Payroll Found.");
                    return result;
                }

                var allMonthlyPayslipDetails = DbInstance.HR_MonthlyPayslipDetails
                    .Include(x => x.User_Employee.HR_Position)
                    .Include(x => x.HR_MonthlyPayslip)
                    .Where(x => payrollIdsList.Contains(x.HR_MonthlyPayslip.HR_Payroll.Id) && !x.IsDeleted && !x.HR_MonthlyPayslip.IsDraft)
                    .ToList();

                if (allMonthlyPayslipDetails.Count <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("No Salary Found with your search.");
                    return result;
                }

                var yearlySalaryJson = new YearlySalarySummaryReportJson();

                var salaryGroupList = DbInstance.HR_SalaryTemplate.Where(x => !x.IsDeleted && x.IsEnable).OrderBy(x => x.OrderNo).ToList();

                bool isHeaderAndOtherValueAdded = false;
                foreach (var salaryGroup in salaryGroupList)
                {
                    var salaryGroupJson = new YearlySalaryGroupJson();

                    salaryGroupJson.SalaryGroupName = salaryGroup.Name;
                    foreach (var payroll in selectedPayrollList)
                    {
                        var monthlyPayslipDetails = allMonthlyPayslipDetails
                            .Where(x => x.HR_MonthlyPayslip.PayrollId == payroll.Id).ToList();

                        var grossSalary = monthlyPayslipDetails
                            .Where(x => x.User_Employee.HR_Position.SalaryTemplateId == salaryGroup.Id
                                        && x.CategoryTypeEnumId >= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.BasicSalary &&
                            x.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherAdjustment
                            && x.CategoryTypeEnumId != (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.PreviousAdjustment).Sum(x => x.Value);

                        salaryGroupJson.PayrollWiseAmountList.Add(grossSalary);

                        if (!isHeaderAndOtherValueAdded)
                        {
                            yearlySalaryJson.PayrollNameList.Add($"{payroll.Month.ToString()}-{payroll.Year}");

                            var totalGrossSalary = monthlyPayslipDetails.Where(x =>
                                x.CategoryTypeEnumId >= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.BasicSalary &&
                                x.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherAdjustment
                                && x.CategoryTypeEnumId !=
                                (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.PreviousAdjustment).Sum(x => x.Value);
                            yearlySalaryJson.GrossSalaryList.Add(totalGrossSalary);


                            var totalTax = monthlyPayslipDetails.Where(x =>
                                    x.CategoryTypeEnumId ==
                                    (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.TaxDeduction)
                                .Sum(x => x.Value);
                            yearlySalaryJson.TaxList.Add(totalTax);


                            var pyarollWisetotalArrearSalary = monthlyPayslipDetails.Where(x =>
                                x.CategoryTypeEnumId ==
                                (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.PreviousAdjustment).Sum(x => x.Value);
                            yearlySalaryJson.ArrearSalaryList.Add(pyarollWisetotalArrearSalary);


                            var totalDeduction = monthlyPayslipDetails.Where(x =>
                                    x.CategoryTypeEnumId ==
                                    (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherDeduction)
                                .Sum(x => x.Value);
                            yearlySalaryJson.DeductionList.Add(totalDeduction);


                            var pyarollWisetotalRevenueStamp = monthlyPayslipDetails.Where(x =>
                                    x.CategoryTypeEnumId ==
                                    (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.RevenueStamp)
                                .Sum(x => x.Value);
                            yearlySalaryJson.RevenueStampList.Add(pyarollWisetotalRevenueStamp);

                            var netTotal = (totalGrossSalary + pyarollWisetotalArrearSalary) - (totalTax+ totalDeduction+ pyarollWisetotalRevenueStamp);
                            yearlySalaryJson.NetTotalList.Add(netTotal);
                        }

                    }

                    salaryGroupJson.NetTotalSalary = salaryGroupJson.PayrollWiseAmountList.Sum();
                    yearlySalaryJson.SalaryGroupList.Add(salaryGroupJson);

                    isHeaderAndOtherValueAdded = true;
                }

                var totalGrossSalaryAmount = yearlySalaryJson.GrossSalaryList.Sum();
                yearlySalaryJson.GrossSalaryList.Add(totalGrossSalaryAmount);

                var totalTaxAmount = yearlySalaryJson.TaxList.Sum();
                yearlySalaryJson.TaxList.Add(totalTaxAmount);

                var totalDeductionAmount = yearlySalaryJson.DeductionList.Sum();
                yearlySalaryJson.DeductionList.Add(totalDeductionAmount);

                var totalRevenueStamp = yearlySalaryJson.RevenueStampList.Sum();
                yearlySalaryJson.RevenueStampList.Add(totalRevenueStamp);

                var totalArrearSalary = yearlySalaryJson.ArrearSalaryList.Sum();
                yearlySalaryJson.ArrearSalaryList.Add(totalArrearSalary);


                yearlySalaryJson.NetTotalList.Add((totalGrossSalaryAmount + totalArrearSalary)-(totalTaxAmount+totalDeductionAmount+totalRevenueStamp));

                result.Data = yearlySalaryJson;
                return result;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().ToString());
                return result;
            }
        }

        #endregion
    }
}