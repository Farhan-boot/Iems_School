//File: UI Controller
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.CoreLibrary.Helpers;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;


namespace EMS.Web.UI.Areas.HR.Controllers.WebApi
{

    public class MonthlyPayslipManagerApiController : EmployeeBaseApiController
	{
        public MonthlyPayslipManagerApiController()
        {  }


        public class DraftUnDraftJson
        {
            public bool IsDraft { get; set; }
            public int PayrollId { get; set; }
            public List<EmployeeMonthlyPayslipJson> PayslipList { get; set; }

            public DraftUnDraftJson()
            {
                PayslipList = new List<EmployeeMonthlyPayslipJson>();
            }
        }

        public class EmployeeMonthlyPayslipJson
        {
            public int PayrollId { get; set; }
            public int MonthlyPayslipId { get; set; }
            public int EmployeeId { get; set; }
            public int UserId { get; set; }
            public string Name { get; set; }
            public float TotalAddition { get; set; }
            public float TotalDeduction { get; set; }
            public bool IsDraft { get; set; }
            public bool IsSelected { get; set; }
            public string Remarks { get; set; }
        }

        public class GenerateMonthlyPayslipJson
        {
            public int PayrollId { get; set; }
            public bool IsDraft { get; set; }
            public bool IsContinueOnError { get; set; }
            public List<User_EmployeeJson> EmployeeList { get; set; }

            public GenerateMonthlyPayslipJson()
            {
                EmployeeList = new List<User_EmployeeJson>();
            }
        }
        #region MonthlyPayslip 
        #region Get Api
       
        public HttpListResult<EmployeeMonthlyPayslipJson> GetMonthlyPayslipByPayrollId(Int32 payrollId)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<EmployeeMonthlyPayslipJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                IQueryable<HR_MonthlyPayslip> query = DbInstance.HR_MonthlyPayslip
                    .Include(x=>x.User_Employee.User_Account)
                    .Where(x => x.PayrollId == payrollId && !x.IsDeleted).OrderBy(x => x.User_Employee.HR_Position.Priority);

                var entities = query.Select(x=>new EmployeeMonthlyPayslipJson
                {
                    PayrollId = x.PayrollId,
                    MonthlyPayslipId = x.Id,
                    EmployeeId = x.EmployeeId,
                    UserId = x.User_Employee.UserId,
                    Name = x.User_Employee.User_Account.FullName,
                    TotalAddition = x.TotalAddition,
                    TotalDeduction = x.TotalDeduction,
                    IsDraft =x.IsDraft,
                    Remarks = x.Remarks
                }).ToList();

                if (entities.Count <= 0)
                {
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add("There is No available Monthly Payslip found.");
                    return result;
                }

                result.Data = entities;
                result.Count = count;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult<HR_MonthlyPayslipJson> GetMonthlyPayslipById(Int32 id = 0,int employeeId=0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_MonthlyPayslipJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_MonthlyPayslipDataService monthlyPayslipDataService = new HR_MonthlyPayslipDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_MonthlyPayslipJson();
                    HR_MonthlyPayslip entity;
                    if (id <= 0)
                    {
                        entity = HR_MonthlyPayslip.GetNew(id, employeeId);
                    }
                    else
                    {
                        entity = monthlyPayslipDataService.GetById(id);
                    }
                    entity.Map(json);
                    result.Data = json;

                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
       
        public HttpListResult<HR_MonthlyPayslipJson> GetMonthlyPayslipDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_MonthlyPayslipJson>();
            try
            {
                //HR_MonthlyPayslipDataService monthlyPayslipDataService =
                //    new HR_MonthlyPayslipDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //DropDown Option Tables

                var payrollList = DbInstance.HR_Payroll.Where(x => !x.IsDeleted).OrderByDescending(x => x.Year).ThenByDescending(x => x.MonthEnumId).AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.Month.ToString() + ", " + t.Year, IsCurrent = t.IsCurrent }).ToList();
                result.DataExtra.PayrollList = payrollList;

                result.DataExtra.SelectedPayrollId = payrollList.FirstOrDefault(x => x.IsCurrent)?.Id;

                if (result.DataExtra.PayrollList.Count > 0)
                {
                    var depts = DbInstance.HR_Department
                        .OrderBy(x => x.Name)
                        //.ThenBy(x => x.Priority)
                        .ToList();
                    result.DataExtra.DepartmentList = depts.AsEnumerable()
                        .Select(t => new
                        {
                            Id = t.Id,
                            Name = t.Name,// + " (" + t.ShortName + ")",
                            Type = t.Type.ToString().AddSpacesToSentence()
                        });

                    result.DataExtra.CategoryEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserCategoryEnum));
                    result.DataExtra.JobClassEnumList = EnumUtil.GetEnumList(typeof(User_Employee.JobClassEnum));
                    result.DataExtra.AcademicLevelEnumList = EnumUtil.GetEnumList(typeof(User_Rank.AcademicLevelEnum));
                    result.DataExtra.EmployeeCategoryEnumList = EnumUtil.GetEnumList(typeof(User_Employee.EmployeeCategoryEnum));
                    result.DataExtra.EmployeeTypeEnumList = EnumUtil.GetEnumList(typeof(User_Employee.EmployeeTypeEnum));
                    result.DataExtra.WorkingStatusEnumList = EnumUtil.GetEnumList(typeof(User_Employee.WorkingStatusEnum));
                    result.DataExtra.JobTypeEnumList = EnumUtil.GetEnumList(typeof(User_Employee.JobTypeEnum));
                    result.DataExtra.SalaryTypeEnumList = EnumUtil.GetEnumList(typeof(HR_SalaryTemplateDetails.SalaryTypeEnum));
                    result.DataExtra.CategoryTypeEnumList = EnumUtil.GetEnumList(typeof(HR_SalaryTemplateDetails.CategoryTypeEnum));

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

                //result.DataExtra.SalarySettingsList = DbInstance.HR_SalarySettings.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.EmployeeList = DbInstance.User_Employee.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult<GenerateMonthlyPayslipJson> GetPagedEmployeeForSalaryGenerate(
            string textkey
            , string userName
            , int payrollId
            , long deptId = -1
            , int categoryEnumId = -1
            , int jobClassEnumId = -1
            , int employeeCategoryEnumId = -1
            , int employeeTypeEnumId = -1
            , int workingStatusEnumId = -1
            , int jobTypeEnumId = -1//
            , int academicLevelEnumId = -1
            , long salaryTemplateId = -1
        )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpResult<GenerateMonthlyPayslipJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_EmployeeDataService employeeDataService = new User_EmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<User_Employee> query = DbInstance.User_Employee
                        .Include(x => x.User_Account)
                        .Include(x => x.User_Account.HR_Department)
                        .Include(x => x.HR_Position)
                        .Where(x => x.Id != 1 && x.HR_Position.SalaryTemplateId != null 
                                              && x.HR_SalarySettings.Any(q=>q.IsCurrent && !q.IsExcludedFromAutoGeneration))
                        .OrderBy(x => x.HR_Department.Name);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.User_Account.FullName.ToLower().Contains(textkey.ToLower()));
                    }
                    if (userName.IsValid())
                    {
                        query = query.Where(q => q.User_Account.UserName.ToLower().Contains(userName.ToLower()));
                    }
                    if (deptId > -1)
                    {
                        query = query.Where(q => q.User_Account.DepartmentId == deptId);
                    }
                    
                    if (jobClassEnumId > -1)
                    {
                        query = query.Where(q => q.JobClassEnumId == (byte)jobClassEnumId);
                    }
                    
                    if (salaryTemplateId > -1)
                    {
                        query = query.Where(q => q.HR_Position.SalaryTemplateId == salaryTemplateId);
                    }

                    if (employeeCategoryEnumId > -1)
                    {
                        query = query.Where(q => q.EmployeeCategoryEnumId == employeeCategoryEnumId);
                    }
                    if (jobTypeEnumId > -1)
                    {
                        query = query.Where(q => q.JobTypeEnumId == jobTypeEnumId);
                    }
                    if (workingStatusEnumId > -1)
                    {
                        query = query.Where(q => q.WorkingStatusEnumId == workingStatusEnumId);
                    }

                    if (employeeTypeEnumId > -1)
                    {
                        query = query.Where(q => q.EmployeeTypeEnumId == employeeTypeEnumId);
                    }

                    var generatedEmployeesIds = DbInstance.HR_MonthlyPayslip.Where(x => x.PayrollId == payrollId && !x.IsDeleted)
                        .Select(x => x.EmployeeId).ToList();

                    query = query.Where(x => !generatedEmployeesIds.Contains(x.Id));

                    var policyEntities = query.ToList();
                    var policyJsons = new List<User_EmployeeJson>();
                    policyEntities.Map(policyJsons);

                    var generatePayslipJson = new GenerateMonthlyPayslipJson()
                    {
                        PayrollId = payrollId
                    };

                    generatePayslipJson.EmployeeList = policyJsons;

                    if (policyEntities.Count <= 0)
                    {
                        result.HasError = true;
                        result.Errors.Add("No Salary Generate Able Employee Found with Current Search Parameter.");
                        return result;
                    }
                    result.Data = generatePayslipJson;
                    result.DataExtra.Count = policyEntities.Count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }

        [HttpPost]
        public HttpResult GenerateMonthlyPayslip(GenerateMonthlyPayslipJson generateMonthlyPayslipJson)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpResult();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            var generateAbleEmployeeList = generateMonthlyPayslipJson.EmployeeList.Where(x => x.IsSelected).ToList();

            try
            {
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    foreach (var monthlyPayslip in generateAbleEmployeeList)
                    {
                        var entityReceived = HR_MonthlyPayslip.GetNew(0,monthlyPayslip.Id);
                        var dbAttachedEntity = new HR_MonthlyPayslip();
                        #region Generating 

                        entityReceived.PayrollId = generateMonthlyPayslipJson.PayrollId;
                        entityReceived.IsDraft = generateMonthlyPayslipJson.IsDraft;
                        entityReceived.CreateDate =DateTime.Now;
                        entityReceived.CreateById = HttpUtil.Profile.UserId;
                        entityReceived.LastChanged = DateTime.Now;
                        entityReceived.LastChangedById = HttpUtil.Profile.UserId;

                        #endregion

                        if (Facade.HrAreaFacade.PayrollFacade.SaveMonthlyPayslipLogic(entityReceived, ref dbAttachedEntity, out error))
                        {
                            DbInstance.SaveChanges();

                            if (!Facade.HrAreaFacade.PayrollFacade.GenerateMonthlyPayslipFromSalarySettings(dbAttachedEntity.EmployeeId, dbAttachedEntity, out error, scope))
                            {
                                result.HasError = true;
                                result.Errors.Add($"Error Occured For {monthlyPayslip.FullName}, Reason:{error}");
                                return result;
                            }

                            DbInstance.SaveChanges();
                        }
                        else
                        {
                            result.HasError = true;
                            result.Errors.Add($"Error Occured For {monthlyPayslip.FullName}, Reason:{error}");
                            return result;
                        }
                    }

                    if (!result.HasError)
                    {
                        scope.Commit();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }

        [HttpPost]
        public HttpListResult<EmployeeMonthlyPayslipJson> DraftOrUnDraftPayslip(DraftUnDraftJson draftUnDraftJson)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<EmployeeMonthlyPayslipJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            var draftUnDraftAbleEmployeeIdList = draftUnDraftJson.PayslipList.Where(x => x.IsSelected).Select(x=>x.EmployeeId).ToList();

            try
            {
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    var payslipList = DbInstance.HR_MonthlyPayslip.Where(x =>
                        x.PayrollId == draftUnDraftJson.PayrollId &&
                        draftUnDraftAbleEmployeeIdList.Contains(x.EmployeeId)).ToList();

                    foreach (var monthlyPayslip in payslipList)
                    {
                        monthlyPayslip.IsDraft = draftUnDraftJson.IsDraft;
                        monthlyPayslip.History += $"{DateTime.Now} Is Drat is Set to {draftUnDraftJson.IsDraft}, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}<br>";
                    }

                    DbInstance.SaveChanges();
                    scope.Commit();

                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }


        #endregion

        #endregion

    }
}
