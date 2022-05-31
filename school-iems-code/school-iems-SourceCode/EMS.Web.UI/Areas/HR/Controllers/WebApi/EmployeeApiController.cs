//File: UI Controller

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web.Http;
using EMS.Communication.EmailProxy;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Facade;
using EMS.Facade.Public;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
using EMS.Web.Jsons.Custom.HR;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;
using Site = System.Security.Policy.Site;

namespace EMS.Web.UI.Areas.HR.Controllers.WebApi
{

    public class EmployeeApiController : EmployeeBaseApiController
    {
        public EmployeeApiController()
        { }

        public enum SalarySettingsStatusEnum
        {
            NoSalarySettingsAvailable = 1,
            AtLeastOneCurrentSalarySettingsAvailable =2
        }

        #region general Webapi
        #region for List

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="isApproved"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<User_EmployeeJson> GetPagedEmployee(
            string textkey
            ,string userName
            , int approval
            ,int? pageSize
            ,int? pageNo
            ,long deptId=-1
            ,int categoryEnumId = -1
            ,int jobClassEnumId = -1
            ,int employeeCategoryEnumId = -1
            ,int employeeTypeEnumId = -1
            ,int workingStatusEnumId = -1
            ,int jobTypeEnumId = -1//
            ,int academicLevelEnumId = -1
            ,long campusId = -1
            ,int salaryTemplateGroupId = -1
            ,int salarySettingsStatusEnumId = -1
        )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<User_EmployeeJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanEdit, out error))
            {
                result.HasError = true;
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
                        //.Include("HR_Department")
                        .Where(x => x.Id != 1)
                        .OrderBy(x => x.HR_Department.Name);
                        //.ThenBy(x => x.User_Account.User_Rank.CategoryEnumId)
                        //.ThenBy(x => x.User_Account.User_Rank.Priority)
                        //.ThenBy(x => x.User_Account.User_Rank.JobClassEnumId);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.User_Account.FullName.ToLower().Contains(textkey.ToLower()));
                    }
                    if (userName.IsValid())
                    {
                        query = query.Where(q => q.User_Account.UserName.ToLower().Contains(userName.ToLower()));
                    }
                    if (approval==0)
                    {
                        query = query.Where(q => q.User_Account.IsApproved==false);
                    }
                    if (approval==1)
                    {
                        query = query.Where(q => q.User_Account.IsApproved == true);
                    }
                    if (deptId>-1)
                    {
                        query = query.Where(q => q.User_Account.DepartmentId == deptId);
                    }
                    //if (categoryEnumId > -1)
                    //{
                    //    query = query.Where(q => q.User_Account.CategoryEnumId == (byte)categoryEnumId);
                    //}
                    if (jobClassEnumId > -1)
                    {
                        query = query.Where(q => q.JobClassEnumId == (byte)jobClassEnumId);
                    }
                    //if (academicLevelEnumId > -1)
                    //{
                    //    query = query.Where(q => q.User_Account.User_Rank.AcademicLevelEnumId == (byte)academicLevelEnumId);
                    //}
                    if (campusId > -1)
                    {
                        query = query.Where(q => q.User_Account.CampusId == campusId);
                    }

                    //
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

                    if (salaryTemplateGroupId > -1)
                    {
                        if (salaryTemplateGroupId == 0)
                        {
                            query = query.Where(q => q.HR_Position.SalaryTemplateId == null);
                        }
                        else
                        {
                            query = query.Where(q => q.HR_Position.SalaryTemplateId == salaryTemplateGroupId);
                        }
                    }

                    if (salarySettingsStatusEnumId == (byte)SalarySettingsStatusEnum.NoSalarySettingsAvailable)
                    {
                        query = query.Where(x => x.HR_SalarySettings.Count <= 0);
                    }
                    //else if (salarySettingsStatusEnumId == (byte)SalarySettingsStatusEnum.AtLeastOneSalarySettingsAvailable)
                    //{
                    //    query = query.Where(x => x.HR_SalarySettings.Count > 0);
                    //}
                    else if (salarySettingsStatusEnumId == (byte)SalarySettingsStatusEnum.AtLeastOneCurrentSalarySettingsAvailable)
                    {
                        query = query.Where(x => x.HR_SalarySettings.Any(q=>q.IsCurrent));
                    }
                    //else if (salarySettingsStatusEnumId ==
                    //         (byte)SalarySettingsStatusEnum.AtLeastOneCurrentSalarySettingsAvailable)
                    //{

                    //}

                    var policyEntities = employeeDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var policyJsons = new List<User_EmployeeJson>();
                    policyEntities.Map(policyJsons);

                    result.Data = policyJsons;
                    result.Count = count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }
        /// <summary>
        /// Caution: Very Dangerous to call When table have huge data!
        /// </summary>
        /// <returns></returns>
        private HttpListResult<User_EmployeeJson> GetAllEmployee()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_EmployeeJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanView, out error)
        && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanAdd, out error)
        && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_EmployeeDataService employeeDataService = new User_EmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJsons = new List<User_EmployeeJson>();
                    var policyEntities = employeeDataService.GetAll(out error);
                    policyEntities.Map(policyJsons);
                    result.Data = policyJsons;
                    result.Count = policyJsons.Count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }
        /// <summary>
        /// Todo pending to implement bulk save
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        private HttpListResult<User_EmployeeJson> SaveEmployeeList(IList<User_EmployeeJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_EmployeeJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (User_EmployeeDataService employeeDataService = new User_EmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<User_Employee>();
                    var dbAttachedListEntity = new List<User_Employee>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (employeeDataService.Save(entity, ref dbAttachedListEntity, out error))
                    //{
                    //    dbAttachedListEntity.Map(jsonList);
                    //    result.Data = jsonList;//return object
                    //}
                    //else
                    //{
                    //    result.HasError = true;
                    //    result.Errors.Add(error);
                    //}
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

        #region for Single Row
        public HttpResult<HrEmployeeJson> GetEmployeeById(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HrEmployeeJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var json = new HrEmployeeJson();
            json.Account = new User_AccountJson();
            json.Employee = new User_EmployeeJson();
            json.Image = new User_ImageJson();
            json.ContactAddressList = new List<User_ContactAddressJson>();
            json.ContactEmailList = new List<User_ContactEmailJson>();
            json.ContactNumberList = new List<User_ContactNumberJson>();
            json.EducationList = new List<User_EducationJson>();
            json.AppointmentHistoryList = new List<HR_AppointmentHistoryJson>();

            try
            {
                User_Employee employeeEntity;
                List<HR_AppointmentHistory> appointmentHistoryList = new List<HR_AppointmentHistory>();
                User_Account accountEntity;
                List<User_ContactNumber> contatNumberList = new List<User_ContactNumber>();
                List<User_ContactEmail> contatEmailList = new List<User_ContactEmail>();
                List<User_ContactAddress> contatAddressList = new List<User_ContactAddress>();
                using (User_EmployeeDataService employeeDataService = new User_EmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    if (id <= 0)
                    {
                        employeeEntity = User_Employee.GetNew();
                    }
                    else
                    {
                        employeeEntity = employeeDataService.GetById(id);
                    }
                    employeeEntity.Map(json.Employee);
                }
                using (HR_AppointmentHistoryDataService appointmentHistoryDataService = new HR_AppointmentHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    if (id <= 0)
                    {
                        HR_AppointmentHistory newAppointmentHistory = HR_AppointmentHistory.GetNew();
                        appointmentHistoryList.Add(newAppointmentHistory);
                    }
                    else
                    {
                        appointmentHistoryList = appointmentHistoryDataService.GetAllByEmployeeId(id);
                    }
                    appointmentHistoryList.OrderBy(x => x.StartDate);
                    appointmentHistoryList.Map(json.AppointmentHistoryList);
                }

                using (User_AccountDataService accountDataService = new User_AccountDataService(DbInstance, HttpUtil.Profile))
                {
                    if (id <= 0)
                    {
                        accountEntity = User_Account.GetNew();
                    }
                    else
                    {
                        accountEntity = accountDataService.GetById(employeeEntity.UserId);
                        accountEntity.Password = string.Empty;
                        accountEntity.PasswordSalt = string.Empty;
                    }
                    accountEntity.Map(json.Account);
                }
                using (User_ContactNumberDataService contactNumberDataService = new User_ContactNumberDataService(DbInstance, HttpUtil.Profile))
                {
                    if (id <= 0)
                    {
                        User_ContactNumber newContactNumber = User_ContactNumber.GetNew();
                        contatNumberList.Add(newContactNumber);
                    }
                    else
                    {
                        contatNumberList = contactNumberDataService.GetAllByUserId(employeeEntity.UserId);
                    }
                    contatNumberList.OrderBy(x => x.ContactNumberTypeEnumId);
                    contatNumberList.Map(json.ContactNumberList);
                }
                using (User_ContactEmailDataService contactEmailDataService = new User_ContactEmailDataService(DbInstance, HttpUtil.Profile))
                {
                    if (id <= 0)
                    {
                        User_ContactEmail newContactEmail = User_ContactEmail.GetNew();
                        contatEmailList.Add(newContactEmail);
                    }
                    else
                    {
                        contatEmailList = contactEmailDataService.GetAllByUserId(employeeEntity.UserId);
                    }
                    contatEmailList.OrderBy(x => x.ContactEmailTypeEnumId);
                    contatEmailList.Map(json.ContactEmailList);
                }
                using (User_ContactAddressDataService contactAddressDataService = new User_ContactAddressDataService(DbInstance, HttpUtil.Profile))
                {
                    if (id <= 0)
                    {
                        User_ContactAddress newContactAddress = User_ContactAddress.GetNew();
                        contatAddressList.Add(newContactAddress);
                    }
                    else
                    {
                        contatAddressList = contactAddressDataService.GetAllByUserId(employeeEntity.UserId);
                    }
                    contatAddressList.OrderByDescending(x => x.ContactAddressTypeEnumId);
                    contatAddressList.Map(json.ContactAddressList);
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }

            json.Account.ProfilePictureBase64 = HttpUtil.GetBase64String(json.Account.ProfilePictureImageUrl);

            result.Data = json;
            return result;
        }
        [System.Web.Mvc.HttpPost]
        public HttpResult<HrEmployeeJson> SaveEmployee(HrEmployeeJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HrEmployeeJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            DateTime dateTimeNow = DateTime.Now;
            //if (json.Account.DateOfBirth != null)
            //{
            //    DateTime.TryParseExact(
            //        json.Account.DateOfBirth.Date.ToString(),
            //        "dd/mm/yyyy"
            //        );
            //    json.Account.DateOfBirth.Date
            //}

            if (!IsValidToSaveEmployee(json, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            EmsDbContext emsDbContext = new EmsDbContext();
            //HR_EmployeeFacade Facade = new HR_EmployeeFacade(emsDbContext, HttpUtil.Profile);
            //User
            var listContactNumber = new List<User_ContactNumber>();
            json.ContactNumberList.Map(listContactNumber);

            var listContactEmail = new List<User_ContactEmail>();
            if (listContactEmail.Count!=0)
            {
                json.ContactEmailList.Map(listContactEmail);
            }
            

            var listContactAddress = new List<User_ContactAddress>();
            json.ContactAddressList.Map(listContactAddress);

            var userAccount = new User_Account();
            json.Account.Map(userAccount);
            userAccount.User_ContactNumber = new List<User_ContactNumber>();
            userAccount.User_ContactEmail = new List<User_ContactEmail>();
            userAccount.User_ContactAddress = new List<User_ContactAddress>();

            userAccount.UserTypeEnumId = (byte)User_Account.UserTypeEnum.Employee;
            //userAccount.IsMilitary = userAccount.CategoryEnumId != (byte)User_Account.UserCategoryEnum.Civil;

            userAccount.User_ContactNumber = listContactNumber;
            userAccount.User_ContactEmail = listContactEmail;
            userAccount.User_ContactAddress = listContactAddress;

            //Employee
            var listAppointmentHistory = new List<HR_AppointmentHistory>();
            json.AppointmentHistoryList.Map(listAppointmentHistory);
            var userEmployee = new User_Employee();
            json.Employee.Map(userEmployee);
            userEmployee.HR_AppointmentHistory = new List<HR_AppointmentHistory>();
            userEmployee.HR_AppointmentHistory = listAppointmentHistory;
            userEmployee.PositionId = listAppointmentHistory.Single(x => x.IsPrimary).PositionId;
            userEmployee.IsAcademician = userEmployee.EmployeeCategoryEnumId == (byte)User_Employee.EmployeeCategoryEnum.AdministrativeOfficer;

            //if (userEmployee.WorkingStatusEnumId == (byte)User_Employee.WorkingStatusEnum.Terminated ||
            //    userEmployee.WorkingStatusEnumId == (byte)User_Employee.WorkingStatusEnum.Resigned ||
            //    userEmployee.WorkingStatusEnumId == (byte)User_Employee.WorkingStatusEnum.Retired)
            //{
            //    userEmployee.IsWorking = false;
            //}
            //else
            //{
            //    userEmployee.IsWorking = true;
            //}

            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    if (Facade.HrAreaFacade.EmployeeFacade.SaveAccount(userAccount, out error, scope))
                    {
                        if (!Facade.HrAreaFacade.EmployeeFacade.SaveEmployee(userEmployee, out error, scope))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                            scope.Rollback();
                            return result;
                        }
                    }
                    else
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                        return result;
                    }

                    DbInstance.SaveChanges();
                    scope.Commit();
                }
                catch (Exception ex)
                {
                    //log error
                    result.HasError = true;
                    result.Errors.Add(ex.ToString());
                    scope.Rollback();
                }
            }
            result.Data = json;
            return result;
        }
        [System.Web.Mvc.HttpPost]
        private HttpResult<HrEmployeeJson> SaveApproval_Old([FromUri]long userId, bool isApproved, bool isSendEmailOnApproval, bool isForceToGenerateUsername)
        {
            string error = string.Empty;
            var result = new HttpResult<HrEmployeeJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanApproval, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            //var json = new HrEmployeeJson();
            //json.Account = new User_AccountJson();
            try
            {
                var userAccount = DbInstance.User_Account.Find(userId);
                if (userAccount != null)
                {
                    using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                    {
                        if ((userAccount.UserName.Equals(userAccount.Id.ToString()) || isForceToGenerateUsername) && isApproved)
                        {
                            //TODO test the method is working or not?
                            //var username = "";
                            //if (Facade.HrAreaFacade.EmployeeFacade.GenerateEmpUserName(userAccount, out username) && username != string.Empty)
                            //{
                            //    //log username change audit
                            //    Facade.HrAreaFacade.EmployeeFacade.SaveUserNameChangeAudit(userAccount.UserName,
                            //        username, userAccount.Id, scope, out error);
                            //    userAccount.UserName = username;//new user name

                            //}
                            //else
                            //{
                            //    result.HasError = true;
                            //    result.Errors.Add("User name creation failed. " + username);

                            //}
                        }
                        userAccount.IsApproved = isApproved;
                        result.DataExtra.UserName = userAccount.UserName;//output current user name
                        if (Facade.HrAreaFacade.EmployeeFacade.SaveAccountOnly(userAccount, scope, out error))
                        {
                            DbInstance.SaveChanges();
                            scope.Commit();
                        }
                        else
                        {
                            scope.Rollback();
                            result.HasError = true;
                            result.Errors.Add(error);
                        }
                    }
                    if (isApproved && isSendEmailOnApproval)
                    {
                        var username = userAccount.UserName;
                        var password = "[You are already changed your first password. (This is not your password!)]";
                        if (userAccount.PasswordSalt == string.Empty)
                        {
                            //password = userAccount.Password;
                            password = "[The password was given by you on registration. (This is not your password)]";
                        }

                        //TODO Check Email was sent or not
                        var userContact = userAccount.UserEmail;
                        //DbInstance.User_ContactEmail
                        //    .SingleOrDefault(
                        //    x => x.UserId == userId
                        //    && x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail
                        //    );

                 
                 
                        if (!userContact.IsValidEmail())
                        {
                            result.DataExtra.Message = "Can't find any valid Personal Email Address to send email. ";
                        }
                        else
                        {
                            var isSenEmail =EmailTemplate.SendNewAccountEmail(
                                    userAccount.FullName,
                                    $"Congratulation! Your {SiteSettings.Instance.ProductBrandNameSolo} Online Account Created and Approved By Admin",
                                    userContact,
                                    username,
                                    password,
                                    out error);
                            if (!isSenEmail)
                            {
                                result.DataExtra.Message = "Email Can't send now for " + error;
                            }
                        }
                    }
                }
                else
                {
                    error = "User can't exist! Please register user first and try again later. ";
                    result.HasError = true;
                    result.Errors.Add(error);
                }

            }
            catch (Exception ex)
            {
                error = ex.ToString();
                result.HasError = true;
                result.Errors.Add(error);
            }
            return result;
        }


        private bool IsValidToSaveEmployee(HrEmployeeJson json, out string error)
        {
            if (json == null ||
                json.Account == null ||
                json.Employee == null ||
                (json.AppointmentHistoryList == null || json.AppointmentHistoryList.Count == 0) ||
                (json.ContactNumberList == null || json.ContactNumberList.Count == 0) ||
                //(json.ContactEmailList == null || json.ContactEmailList.Count == 0) ||
                (json.ContactAddressList == null || json.ContactAddressList.Count == 0))
            {
                error = "Invalid Operation! Reload the page and try again later";
                return false;
            }
            if (!json.Account.FullName.IsValid())
            {
                error = "Full Name can't blank!";
                return false;
            }
            //if (!json.Account.BanglaName.IsValid())
            //{
            //    error = "Bangla Name can't blank!";
            //    return false;
            //}
            if (!json.Account.FatherName.IsValid())
            {
                error = "Father's Name can't blank!";
                return false;
            }
            if (!json.Account.MotherName.IsValid())
            {
                error = "Mother's Name can't blank!";
                return false;
            }
            if (!json.Account.DateOfBirthString.IsValid())
            {
                error = "Date of Birth can't blank!";
                return false;
            }
            var convertedDate = DateTimeHelper.ToDateTime(json.Account.DateOfBirthString);
            if (convertedDate == null)
            {
                error = "Invalid Date of Birth! Date should be dd/mm/yyyy format, ( ex: 14/04/1998 ) ";
                return false;
            }
            json.Account.DateOfBirth = (DateTime)convertedDate;
            //if (json.Account.DateOfBirth.ToString(CultureInfo.InvariantCulture).IsValid() 
            //    && ((DateTime.Now.Subtract(json.Account.DateOfBirth).Days) < (18*365)))
            //{
            //    error = "Date of Birth is greater than 18 years from now!";
            //    return false;
            //}
            if (!json.Account.Nationality.IsValid())
            {
                error = "Nationality can't blank!";
                return false;
            }

            long output;
            //if (!json.Account.NationalIdNumber.IsValid())
            //{
            //    error = "National ID can't blank!";
            //    return false;
            //}
            //long.TryParse(json.Account.PlaceOfBirthCountryId, out output);
            if (json.Account.NationalIdNumber.IsValid()
                && json.Account.NationalIdNumber.Length < 13
                && json.Account.PlaceOfBirthCountryId == 1)
            {
                error = "Bangladeshi National ID must be 13 to 17 digit!";
                return false;
            }
            //long.TryParse(json.Account.RankId, out output);
            //if (output <= 0)
            //{
            //    //error = "Invalid Rank!";
            //    //return false;
            //}
            //long.TryParse(json.Account.CampusId, out output);
            if (json.Account.CampusId <= 0)
            {
                json.Account.CampusId = 1;
                //error = "Invalid Campus!";
                //return false;
            }

            /***
            //Employee Checking
            ***/
            if (!json.Employee.JoiningDateString.IsValid())
            {
                error = "Joining Date can't blank!";
                return false;
            }
            convertedDate = DateTimeHelper.ToDateTime(json.Employee.JoiningDateString);
            if (convertedDate == null)
            {
                error = "Invalid Joining Date! Date should be dd/mm/yyyy format, ( ex: 14/04/1998 ) ";
                return false;
            }
            //json.Employee.JoiningDate = (DateTime)convertedDate;
            //if (json.Employee.JoiningDate.ToString(CultureInfo.InvariantCulture).IsValid() 
            //    && (json.Employee.JoiningDate >= DateTime.Now))
            //{
            //    error = "Invalid Joining Date!";
            //    return false;
            //}
            //if (json.Employee.LeavingDateString.IsValid())
            //{
            //    convertedDate = DateTimeHelper.ToDateTime(json.Employee.LeavingDateString);
            //    if (convertedDate == null)
            //    {
            //        error = "Invalid Leaving Date! Date should be dd/mm/yyyy format, ( ex: 14/04/1998 ) ";
            //        return false;
            //    }
            //    json.Employee.LeavingDate = (DateTime)convertedDate;
            //}
            ////long.TryParse(json.Employee.DepartmentId, out output);
            //if (json.Employee.DepartmentId <= 0)
            //{
            //    error = "Invalid Department!";
            //    return false;
            //}
            //long.TryParse(json.Employee.JoiningDepartmentId, out output);
            if (json.Employee.JoiningDepartmentId <= 0)
            {
                error = "Invalid Joining Department!";
                return false;
            }
            //long.TryParse(json.Employee.PositionId, out output);
            //if (output <= 0)
            //{
            //    error = "Invalid Primary Position!";
            //    return false;
            //}
            //if (!json.Employee.OldIdNo.IsValid())
            //{
            //    error = SiteSettings.Instance.InstituteShortName+" ID No can't blank!";
            //    return false;
            //}
            /***
            //Appointment Checking
            ***/
            foreach (var hrAppointmentHistoryJson in json.AppointmentHistoryList)
            {
                if (!hrAppointmentHistoryJson.StartDateString.IsValid())
                {
                    error = "Appointment Start Date can't blank!";
                    return false;
                }
                convertedDate = DateTimeHelper.ToDateTime(hrAppointmentHistoryJson.StartDateString);
                if (convertedDate == null)
                {
                    error = "Invalid Appointment Start Date! Date should be dd/mm/yyyy format, ( ex: 14/04/1998 ) ";
                    return false;
                }
                hrAppointmentHistoryJson.StartDate = (DateTime)convertedDate;

                if (hrAppointmentHistoryJson.EndDateString.IsValid())
                {
                    convertedDate = DateTimeHelper.ToDateTime(hrAppointmentHistoryJson.EndDateString);
                    if (convertedDate == null)
                    {
                        error = "Invalid Appointment End Date! Date should be dd/mm/yyyy format, ( ex: 14/04/1998 ) ";
                        return false;
                    }
                    hrAppointmentHistoryJson.EndDate = (DateTime)convertedDate;
                }
            }
            /***
            //Contact Number Checking
            ***/
            foreach (var contactNumber in json.ContactNumberList)
            {
                if (contactNumber.ContactNumber.IsValid() &&
                    (contactNumber.ContactNumber.Contains("+88")))
                {
                    error = "Invalid Contact Number!";
                    return false;
                }
            }
            //if (!json.ContactNumberList
            //    .Single(x => x.ContactNumberTypeEnumId == (byte)User_ContactNumber.ContactNumberTypeEnum.PersonalMobile)
            //    .ContactNumber.IsValid())
            //{
            //    error = "Personal Mobile can't blank!";
            //    return false;
            //}

            //var armyTel = json.ContactNumberList
            //    .Single(x => x.ContactNumberTypeEnumId ==
            //    (byte)User_ContactNumber.ContactNumberTypeEnum.ArmyTelephone)
            //    .ContactNumber;
            //if (!armyTel.IsValid() &&
            //    json.Account.CategoryEnumId != (byte)EnumCollection.Common.UserCategoryEnum.Civil &&
            //    json.Employee.JobClassEnumId == (byte)EnumCollection.Common.JobClassEnum.FirstClass)
            //{
            //    error = "Army Telephone can't blank!";
            //    return false;
            //}
            //if (armyTel.IsValid() && armyTel.Length < 4)
            //{
            //    error = "Army Telephone must be 4 digit!";
            //    return false;
            //}
            //var intercom = json.ContactNumberList
            //    .Single(x => x.ContactNumberTypeEnumId ==
            //    (byte)User_ContactNumber.ContactNumberTypeEnum.OfficeIntercom)
            //    .ContactNumber;
            //if (intercom.IsValid() && intercom.Length < 4)
            //{
            //    error = "Office Intercom must be 4 digit!";
            //    return false;
            //}

            /***
            //Contact Email Checking
            ***/
            //if (json.ContactEmailList.Count!=0 &&
            //    !json.ContactEmailList
            //    .Single(x => x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail)
            //    .ContactEmail.IsValid() &&
            //    (json.Employee.JobClassEnumId == (byte)EnumCollection.Common.JobClassEnum.FirstClass || json.Employee.JobClassEnumId == (byte)EnumCollection.Common.JobClassEnum.SecondClass))
            //{
            //    error = "Personal Email can't blank!";
            //    return false;
            //}
            //if (!json.ContactEmailList
            //    .Single(x => x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.OfficeEmail)
            //    .ContactEmail.IsValid() &&
            //    json.Employee.JobClassEnumId == (byte)EnumCollection.Common.JobClassEnum.FirstClass)
            //{
            //    error = "Office Email can't blank!";
            //    return false;
            //}


            //var mistEmail = json.ContactEmailList
            //    .SingleOrDefault(x => x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.OfficeEmail);
            //if (mistEmail != null && mistEmail.ContactEmail.IsValid() &&
            //    !mistEmail.ContactEmail.Contains(EMS.Framework.Settings.SiteSettings.Instance.InstituteDomain))
            //{
            //    //error = "Invalid Office Email!";
            //    //return false;
            //}
            if (!json.AppointmentHistoryList
                .Any(x => x.IsPrimary))
            {
                error = "You have to give only one Primary Appointment!";
                return false;
            }

            /***
            //Database Checking
            //***/
            //long.TryParse(json.Account.Id, out output);
            //if (DbInstance.User_Account.Any(x => x.Id == output))
            //{
            //    error = "You have already registered! " +
            //            "Your Username and Password will notified through email after verified by Admin. ";
            //    return false;
            //}
            //var personalMobile =
            //    json.ContactNumberList.Single(
            //        c => c.ContactNumberTypeEnumId == (byte)User_ContactNumber.ContactNumberTypeEnum.PersonalMobile)
            //        .ContactNumber;
            //var userExistList = DbInstance.User_ContactNumber
            //    .Where(x => x.ContactNumber == personalMobile)
            //    .Select(x => x.User_Account.UserTypeEnumId);
            //if (userExistList.Any(userTypeEnumId => userTypeEnumId == (byte)User_Account.UserTypeEnum.Employee))
            //{
            //    error = "You have already Registered with this Personal Mobile Number! " +
            //            "Your Username and Password will notified through email after verified by Admin. ";
            //    return false;
            //}
            //List<byte> userExistList = DbInstance.User_Account
            //    .Where(x => x.NationalIdNumber == json.Account.NationalIdNumber)
            //    .Select(x=>x.UserTypeEnumId)
            //    .ToList();
            //if (userExistList.Count!=0)
            //{
            //    if (userExistList.Any(userTypeEnumId => userTypeEnumId == (byte)User_Account.UserTypeEnum.Employee))
            //    {
            //        error = "You have already Registered with this National ID Number!";
            //        return false;
            //    }
            //}
            //exception

            error = string.Empty;
            return true;
        }
        [HttpPost]
        private  HttpResult<HrEmployeeJson> ResetPassword_Old([FromUri] int userId, string password, string confirmPassword)
        {
            string error = string.Empty;
            string msg = string.Empty;
            var result = new HttpResult<HrEmployeeJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanResetPassword, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                if (!Facade.UserCredentialFacade.ResetPassword(userId, password, confirmPassword, out error, out msg))
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                }
                if (msg != string.Empty)
                {
                    result.DataExtra.Message = msg;
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                result.HasError = true;
                result.Errors.Add(error);
            }

            return result;
        }
        public HttpResult<User_EmployeeJson> GetDeleteEmployeeById(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_EmployeeJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_EmployeeDataService employeeDataService = new User_EmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!employeeDataService.DeleteById(id, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
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
        #endregion
        #endregion

        #region Custom WebApi
        private HttpResult<User_EmployeeJson> GetEmployeeByIdWithChild(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_EmployeeJson>();
            try
            {
                using (User_EmployeeDataService employeeDataService = new User_EmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new User_EmployeeJson();
                    User_Employee policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = User_Employee.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement later
                        //includeTables.Add("User_Employee");
                        //includeTables.Add("User_Employee");

                        policyEntity = employeeDataService.GetById(id, includeTables.ToArray());
                    }
                    policyEntity.Map(policyJson);
                    result.Data = policyJson;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }
        public HttpResult<User_EmployeeJson> GetemployeeDataExtra()
        {
            string error = string.Empty;
            var result = new HttpResult<User_EmployeeJson>();
            try
            {
                result.DataExtra.EmployeeCategoryEnumList = EnumUtil.GetEnumList(typeof(User_Employee.EmployeeCategoryEnum));
                result.DataExtra.EmployeeTypeEnumList = EnumUtil.GetEnumList(typeof(User_Employee.EmployeeTypeEnum));
                result.DataExtra.WorkingStatusEnumList = EnumUtil.GetEnumList(typeof(User_Employee.WorkingStatusEnum));
                result.DataExtra.JobClassEnumList = EnumUtil.GetEnumList(typeof(User_Employee.JobClassEnum));
                result.DataExtra.JobTypeEnumList = EnumUtil.GetEnumList(typeof(User_Employee.JobTypeEnum));
                result.DataExtra.IncrementMonthEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.MonthEnum));

                result.DataExtra.UserTypeEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserTypeEnum));
                //result.DataExtra.UserCategoryEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserCategoryEnum));
                result.DataExtra.UserStatusEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserStatusEnum));
                result.DataExtra.BloodGroupEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.BloodGroupEnum));
                result.DataExtra.GenderEnumList = EnumUtil.GetEnumList(typeof(User_Account.GenderEnum));
                result.DataExtra.MaritalStatusEnumList = EnumUtil.GetEnumList(typeof(User_Account.MaritalStatusEnum));
                result.DataExtra.ReligionEnumList = EnumUtil.GetEnumList(typeof(User_Account.ReligionEnum));

                result.DataExtra.ContactAddressTypeEnumList = EnumUtil.GetEnumList(typeof(User_ContactAddress.ContactAddressTypeEnum));
                result.DataExtra.ContactEmailTypeEnumList = EnumUtil.GetEnumList(typeof(User_ContactEmail.ContactEmailTypeEnum));
                result.DataExtra.ContactNumberTypeEnumList = EnumUtil.GetEnumList(typeof(User_ContactNumber.ContactNumberTypeEnum));
                result.DataExtra.PrivacyEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.PrivacyEnum));

                var ranks = DbInstance.User_Rank
                    .OrderBy(x => x.CategoryEnumId)
                    .ThenBy(x => x.Priority)
                    .ToList();
                result.DataExtra.RankList = ranks.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id.ToString(),
                        Name = t.Name,
                        ShortName = t.ShortName,
                        CategoryEnumId = t.CategoryEnumId,
                        //Category = t.Category.ToString().AddSpacesToSentence()
                    });

                var banks = DbInstance.General_Bank.ToList();
                result.DataExtra.BankList = banks.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                    });

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

                var positions = DbInstance.HR_Position
                    .OrderBy(x => x.JobClassEnumId)
                    .ThenBy(x => x.Priority)
                    .ToList();
                result.DataExtra.PositionList = positions.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                        JobClass = t.JobClass.ToString().AddSpacesToSentence()
                    });

                result.DataExtra.JobClassList = EnumUtil.GetEnumList(typeof(User_Employee.JobClassEnum));

                var countries = DbInstance.General_Country.ToList();
                result.DataExtra.CountryList = countries.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                        CallingCode = "(" + t.CallingCode + ") " + t.Name
                    });

                var campuses = DbInstance.General_Campus.ToList();
                result.DataExtra.CampusList = campuses.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.ShortName
                    });
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;

        }
        public HttpResult<User_EmployeeJson> GetEmployeeListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpResult<User_EmployeeJson>();
            try
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

                result.DataExtra.SalarySettingsStatusEnumList = EnumUtil.GetEnumList(typeof(SalarySettingsStatusEnum));

                result.DataExtra.SalaryTemplateGroupList = DbInstance.HR_SalaryTemplate.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name
                    });

                result.DataExtra.CampusList = DbInstance.General_Campus.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id.ToString(),
                        Name = t.ShortName
                    });
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;

        }
        #endregion

    }
}
