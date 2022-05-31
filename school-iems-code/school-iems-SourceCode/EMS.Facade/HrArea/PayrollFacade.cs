using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Net.Mail;
using EMS.Communication.EmailProxy;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Jsons.Custom.HR.Employee;
using Microsoft.Ajax.Utilities;

namespace EMS.Facade.HrArea
{
    public class PayrollFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;

        public PayrollFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
        }

        public bool UpdateMonthlyPayslipTotalValues(HR_MonthlyPayslip monthlyPayslip)
        {
            var isAnyAdditionValueAvailable = DbInstance.HR_MonthlyPayslipDetails.Any(x =>
                x.CategoryTypeEnumId >= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.BasicSalary &&
                x.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherAdjustment &&
                x.MonthlyPayslipId == monthlyPayslip.Id);
            if (isAnyAdditionValueAvailable)
            {
                monthlyPayslip.TotalAddition = DbInstance.HR_MonthlyPayslipDetails.Where(x => x.CategoryTypeEnumId >= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.BasicSalary &&
                    x.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherAdjustment && x.MonthlyPayslipId == monthlyPayslip.Id).Sum(x => x.Value);
            }
            else
            {
                monthlyPayslip.TotalAddition = 0;
            }

            var isAnyDeductionAvailable = DbInstance.HR_MonthlyPayslipDetails.Any(x =>
                !(x.CategoryTypeEnumId >= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.BasicSalary &&
                  x.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherAdjustment) &&
                x.MonthlyPayslipId == monthlyPayslip.Id);

            if (isAnyDeductionAvailable)
            {
                monthlyPayslip.TotalDeduction = DbInstance.HR_MonthlyPayslipDetails.Where(x => !(x.CategoryTypeEnumId >= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.BasicSalary &&
                    x.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherAdjustment) && x.MonthlyPayslipId == monthlyPayslip.Id).Sum(x => x.Value);
            }
            else
            {
                monthlyPayslip.TotalDeduction = 0;
            }

            return true;
        }
        public bool GenerateFirstSalarySettingsByEmployeeId(int employeeId,out string error)
        {
            error = string.Empty;

            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var salarySettings = HR_SalarySettings.GetNew();

                    salarySettings.Name = "First Salary Settings";
                    salarySettings.EmployeeId = employeeId;
                    salarySettings.IsCurrent = true;
                    salarySettings.EffectiveFrom = DateTime.Now;
                    salarySettings.CreateDate = DateTime.Now;
                    salarySettings.CreateById = HttpUtil.Profile.UserId;
                    salarySettings.LastChanged = DateTime.Now;
                    salarySettings.LastChangedById = HttpUtil.Profile.UserId;

                    DbInstance.HR_SalarySettings.Add(salarySettings);

                    DbInstance.SaveChanges();

                    if (!GenerateSalarySettingsDetailsFromSalaryTemplate(employeeId,salarySettings.Id,out error,scope))
                    {
                        return false;
                    }

                    DbInstance.SaveChanges();
                    scope.Commit();
                }
                catch (Exception e)
                {
                    error += e.GetBaseException().Message;
                    scope.Rollback();
                    return false;
                }
            }
            return true;
        }

        public bool GenerateSalarySettingsDetailsFromSalaryTemplate(int employeeId,int salarySettingsId, out string error, DbContextTransaction scope = null)
        {
            error = string.Empty;
            bool closeTransaction = false;
            if (scope == null)
            {
                closeTransaction = true;
                scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
            }
            try
            {
                var isAnySalarySettingsDetailsAvailable =
                    DbInstance.HR_SalarySettingsDetails.Any(x => x.SalarySettingsId == salarySettingsId);
                if (isAnySalarySettingsDetailsAvailable)
                {
                    return true;
                }

                var employeePositionId = DbInstance.User_Employee.Where(x => x.Id == employeeId).Select(x => x.PositionId).FirstOrDefault();
                if (employeePositionId == null || employeePositionId <= 0)
                {
                    error += "Employee Not found.";
                    return false;
                }

                var employeePosition = DbInstance.HR_Position.FirstOrDefault(x => x.Id == employeePositionId);

                if (employeePosition == null)
                {
                    error += "Employee Position Not found.";
                    return false;
                }

                if (employeePosition.SalaryTemplateId == null || employeePosition.SalaryTemplateId <= 0)
                {
                    error += "No Valid Salary Template is Set for Employees position. Please Set a Valid Salary Template From Position Management.";
                    return false;
                }

                var salaryTemplateDetailList = DbInstance.HR_SalaryTemplateDetails
                    .Where(x => x.SalaryTemplateId == employeePosition.SalaryTemplateId).ToList();

                if (salaryTemplateDetailList.Count <= 0)
                {
                    error += "No Salary Head found in the Salary Template. Please Add Salary Template Details First.";
                    return false;
                }

                var salarySettingsDetailList = new List<HR_SalarySettingsDetails>();

                foreach (var salaryTemplateDetail in salaryTemplateDetailList)
                {
                    salarySettingsDetailList.Add(new HR_SalarySettingsDetails()
                    {
                        SalarySettingsId = salarySettingsId,
                        Name = salaryTemplateDetail.Name,
                        SalaryTypeEnumId = salaryTemplateDetail.SalaryTypeEnumId,
                        CategoryTypeEnumId = salaryTemplateDetail.CategoryTypeEnumId,
                        OrderNo = salaryTemplateDetail.OrderNo,
                        Value = 0.0F,
                        CreateDate = DateTime.Now,
                        CreateById = HttpUtil.Profile.UserId,
                        LastChanged = DateTime.Now,
                        LastChangedById = HttpUtil.Profile.UserId
                    });
                }

                DbInstance.HR_SalarySettingsDetails.AddRange(salarySettingsDetailList);

                if (closeTransaction)
                {   //save changes to DB 
                    DbInstance.SaveChanges();
                    scope.Commit();
                }
            }
            catch (Exception ex)
            {
                error = ex.GetBaseException().Message;
                if (closeTransaction)
                    scope.Rollback();
                return false;
            }
            finally
            {   //dispose Transaction scope
                if (closeTransaction)
                {
                    scope.Dispose();
                }
            }
            return true;
        }

        private bool IsValidToSaveMonthlyPayslip(HR_MonthlyPayslip newObj, HR_SalarySettings salarySettings, out string error)
        {
            error = "";
            if (newObj.EmployeeId <= 0)
            {
                error += "Please select valid Employee.";
                return false;
            }
            if (newObj.PayrollId <= 0)
            {
                error += "Please select valid Payroll.";
                return false;
            }

            if (salarySettings == null)
            {
                error += "No Current Salary Settings Found.";
                return false;
            }

            newObj.SalarySettingsId = salarySettings.Id;
            if (newObj.SalarySettingsId <= 0)
            {
                error += "Please select valid Salary Settings.";
                return false;
            }
            //TODO write your custom validation here.
            var res = DbInstance.HR_MonthlyPayslip.Any(x => x.Id != newObj.Id && x.PayrollId == newObj.PayrollId && x.EmployeeId == newObj.EmployeeId && !x.IsDeleted);
            if (res)
            {
                error = "A Monthly Payslip for same Payroll already exists!";
                return false;
            }
            return true;
        }
        public bool SaveMonthlyPayslipLogic(HR_MonthlyPayslip newObj, ref HR_MonthlyPayslip dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Monthly Payslip to save can't be null!";
                return false;
            }

            var salarySettings =
                DbInstance.HR_SalarySettings.FirstOrDefault(x => x.IsCurrent && x.EmployeeId == newObj.EmployeeId);

            if (!IsValidToSaveMonthlyPayslip(newObj, salarySettings, out error))
                return false;

            HR_MonthlyPayslip objToSave = null;

            objToSave = DbInstance.HR_MonthlyPayslip.SingleOrDefault(x => x.PayrollId == newObj.PayrollId && x.EmployeeId == newObj.EmployeeId);
            bool isNewObject = false;
            
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_MonthlyPayslip.GetNew(newObj.Id);
                DbInstance.HR_MonthlyPayslip.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanAdd,
                    out error))
            {
                return false;
            }
            
            if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.EmployeeId = newObj.EmployeeId;
            if (objToSave.PayrollId != newObj.PayrollId && !isNewObject)
            {
                error = "You Cannot Change Payroll of a Payslip Manually.";
                return false;
            }
            objToSave.PayrollId = newObj.PayrollId;

            if (objToSave.SalarySettingsId != newObj.SalarySettingsId && !isNewObject)
            {
                error = "You Cannot Change Salary Settings Manually.";
                return false;
            }
            objToSave.SalarySettingsId = newObj.SalarySettingsId;

            if (objToSave.IsDraft != newObj.IsDraft && !isNewObject)
            {
                objToSave.History += $"{DateTime.Now} Is Drat is Set to {newObj.IsDraft}, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}<br>";
            }
            objToSave.IsDraft = newObj.IsDraft;

            objToSave.Remarks = newObj.Remarks;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }

        public bool GenerateMonthlyPayslipFromSalarySettings(int employeeId, HR_MonthlyPayslip monthlyPayslip, out string error, DbContextTransaction scope = null)
        {
            error = string.Empty;
            bool closeTransaction = false;
            if (scope == null)
            {
                closeTransaction = true;
                scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
            }
            try
            {
                if (monthlyPayslip.IsDeleted)
                {
                    var payslipPreviousDetails = DbInstance.HR_MonthlyPayslipDetails
                        .Where(x => x.MonthlyPayslipId == monthlyPayslip.Id).ToList();
                    DbInstance.HR_MonthlyPayslipDetails.RemoveRange(payslipPreviousDetails);
                    monthlyPayslip.IsDeleted = false;
                    DbInstance.SaveChanges();
                }
                else
                {
                    var isAnyPayslipDetailsAvailable = DbInstance.HR_MonthlyPayslipDetails.Any(x =>
                        x.MonthlyPayslipId == monthlyPayslip.Id && x.EmployeeId == employeeId);

                    if (isAnyPayslipDetailsAvailable)
                    {
                        return true;
                    }
                }

                var salarySettingsDetails = DbInstance.HR_SalarySettingsDetails
                    .Where(x => x.HR_SalarySettings.EmployeeId == employeeId && x.HR_SalarySettings.IsCurrent).ToList();

                if (salarySettingsDetails.Count <= 0)
                {
                    error += "No Current/Active Salary Settings found for the employee. Please Add Salary Settings First.";
                    return false;
                }

                var salarySettingsDetailList = new List<HR_MonthlyPayslipDetails>();

                foreach (var salaryTemplateDetail in salarySettingsDetails)
                {
                    salarySettingsDetailList.Add(new HR_MonthlyPayslipDetails()
                    {
                        MonthlyPayslipId = monthlyPayslip.Id,
                        Name = salaryTemplateDetail.Name,
                        EmployeeId = employeeId,
                        SalaryTypeEnumId = salaryTemplateDetail.SalaryTypeEnumId,
                        CategoryTypeEnumId = salaryTemplateDetail.CategoryTypeEnumId,
                        OrderNo = salaryTemplateDetail.OrderNo,
                        Value = salaryTemplateDetail.Value,
                        CreateDate = DateTime.Now,
                        CreateById = HttpUtil.Profile.UserId,
                        LastChanged = DateTime.Now,
                        LastChangedById = HttpUtil.Profile.UserId
                    });
                }

                monthlyPayslip.TotalAddition = salarySettingsDetailList.Where(x => x.CategoryTypeEnumId >= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.BasicSalary &&
                                                                     x.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherAdjustment).Sum(x => x.Value);
                monthlyPayslip.TotalDeduction = salarySettingsDetailList.Where(x => !(x.CategoryTypeEnumId >= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.BasicSalary &&
                                                                       x.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherAdjustment)).Sum(x => x.Value);

                DbInstance.HR_MonthlyPayslipDetails.AddRange(salarySettingsDetailList);

                if (closeTransaction)
                {   //save changes to DB 
                    DbInstance.SaveChanges();
                    scope.Commit();
                }
            }
            catch (Exception ex)
            {
                error = ex.GetBaseException().Message;
                if (closeTransaction)
                    scope.Rollback();
                return false;
            }
            finally
            {   //dispose Transaction scope
                if (closeTransaction)
                {
                    scope.Dispose();
                }
            }
            return true;
        }

        public string GetRateName(byte salaryTypeEnumId)
        {
            if (salaryTypeEnumId == (byte)HR_SalaryTemplateDetails.SalaryTypeEnum.Hourly)
            {
                return "Per Hour Rate";
            }

            if (salaryTypeEnumId == (byte)HR_SalaryTemplateDetails.SalaryTypeEnum.Weekly)
            {
                return "Per Week Rate";
            }

            if (salaryTypeEnumId == (byte)HR_SalaryTemplateDetails.SalaryTypeEnum.Daily)
            {
                return "Per Day Rate";
            }

            return "";
        }
        public string GetWorkingValueName(byte salaryTypeEnumId)
        {
            if (salaryTypeEnumId == (byte)HR_SalaryTemplateDetails.SalaryTypeEnum.Hourly)
            {
                return "Working Hour";
            }

            if (salaryTypeEnumId == (byte)HR_SalaryTemplateDetails.SalaryTypeEnum.Weekly)
            {
                return "Working Week";
            }

            if (salaryTypeEnumId == (byte)HR_SalaryTemplateDetails.SalaryTypeEnum.Daily)
            {
                return "Working Days";
            }

            return "";
        }
    }
}
