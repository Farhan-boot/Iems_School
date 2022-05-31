using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;

using EMS.Web.Jsons.Models;
using EntityMapper = EMS.Web.Jsons.Models.EntityMapper;
using System.Data.Entity.Infrastructure;
using EMS.Web.Jsons.Custom.HR.Employee;

//AddNewApplicantApiController.cs
namespace EMS.Web.UI.Areas.HR.Controllers.WebApi
{
    public class AddNewEmployeeApiController : EmployeeBaseApiController
    {

        public HttpResult<AddNewEmployeeStep1Json> GetNewEmployee(long id = 0)
        {

            string error = string.Empty;
            var employeeResult = new HttpResult<AddNewEmployeeStep1Json>();

            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanAdd, out error))
            {
                employeeResult.HasError = true;
                employeeResult.HasViewPermission = false;
                employeeResult.Errors.Add(error);
                return employeeResult;
            }

            var applicantJson = new AddNewEmployeeStep1Json
            {
                Id = "0",
                UserName = "",
                FullName = "",
                UserEmail = "",
                UserMobile = "",
                DateOfBirth=DateTime.Now,
                GenderEnumId = (byte)User_Account.GenderEnum.Unknown,
                DepartmentId = 0,
                PositionId = 0,//"0",
                EmployeeCategoryEnumId = 0,
                JobTypeEnumId = (byte)User_Employee.JobTypeEnum.FullTime,
                EmployeeTypeEnumId = (byte)User_Employee.EmployeeTypeEnum.Permanent,
                JobClassEnumId = (byte)User_Employee.JobClassEnum.FirstGrade,
                WorkingStatusEnumId = (byte)User_Employee.WorkingStatusEnum.Active,
                JoiningSemesterId = 0,
                JoiningDate = DateTime.Now,
                IsAutoUserName = true,
                //AdmissionExamId = null,//List with active selected
                //ProgramId = "",//List
                //EnrollmentTypeEnumId = 0,
                //FeeCodeId = 0,//list with active by programId
                //IsAutoUserName = false,
                //ContinuingBatchId = 0,
                //IdPrefix="XXXXXX",
                //DepartmentName = 0,//list
                //DepartmentCode = 0,
                //AdmissionYear = 0,
                //SemesterTerm = 0,
                //ClassTimingGroupEnumId = 0
            };

            try
            {
                if (!GetDataExtraForCreateEmployee(employeeResult, out error))
                {
                    return employeeResult;
                }
                //setting current exam ID

            }
            catch (Exception ex)
            {
                employeeResult.HasError = true;
                employeeResult.Errors.Add(ex.ToString());
            }

            employeeResult.Data = applicantJson;
            return employeeResult;
        }
        private bool GetDataExtraForCreateEmployee(HttpResult<AddNewEmployeeStep1Json> result, out string error)
        {
            error = string.Empty;

            if (result == null)
            {
                error = "";
                return false;
            }

            result.DataExtra.EmployeeCategoryEnumList = EnumUtil.GetEnumList(typeof(User_Employee.EmployeeCategoryEnum));
            result.DataExtra.EmployeeTypeEnumList = EnumUtil.GetEnumList(typeof(User_Employee.EmployeeTypeEnum));
            result.DataExtra.WorkingStatusEnumList = EnumUtil.GetEnumList(typeof(User_Employee.WorkingStatusEnum));
            result.DataExtra.JobClassEnumList = EnumUtil.GetEnumList(typeof(User_Employee.JobClassEnum));
            result.DataExtra.JobTypeEnumList = EnumUtil.GetEnumList(typeof(User_Employee.JobTypeEnum));
            //result.DataExtra.IncrementMonthEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.MonthEnum));

            var depts = DbInstance.HR_Department
                .Where(x => !x.IsDeleted && x.StatusEnumId == (byte)HR_Department.StatusEnum.Active)
                   .OrderBy(x => x.ParentDeptId)
                   .ThenBy(x => x.Priority)
                   .ToList();

            result.DataExtra.DepartmentList = depts.AsEnumerable()
                .Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,// + " (" + t.ShortName + ")",
                    Type = t.Type.ToString().AddSpacesToSentence()
                });


            var positions = DbInstance.HR_Position
                .Where(x => !x.IsDeleted && x.StatusEnumId == (byte)HR_Department.StatusEnum.Active)
                .OrderBy(x => x.Name)
                //.ThenBy(x => x.Priority)
                .ToList();

            result.DataExtra.PositionList = positions.AsEnumerable()
            .Select(t => new
            {
                Id = t.Id,
                Name = t.Name,
                JobClass = t.JobClass.ToString().AddSpacesToSentence()
            });


            result.DataExtra.SemesterList = DbInstance.Aca_Semester.AsEnumerable()
            .OrderByDescending(x => x.Year)
            .ThenByDescending(x => x.TermId)
            .Select(t => new
            { Id = t.Id.ToString(), Name = t.Name }).ToList();

            //result.DataExtra.CampusList = DbInstance.General_Campus.AsEnumerable().Select(t => new
            //{
            //    Id = t.Id,
            //    Name = t.Name
            //}).ToList();

            return true;
        }
   
        [HttpPost]
        public HttpResult<AddNewEmployeeStep1Json> SaveEmployee(AddNewEmployeeStep1Json employeeJson)
        {

            string error = string.Empty;
            var result = new HttpResult<AddNewEmployeeStep1Json>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                //checking json is correct
                if (!IsValidToSaveNewEmployee(employeeJson, out error))
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return result;
                }

                //var username = $"{(admExam.Aca_Semester.Year % 100).ToStringPadLeft(2, '0')}{admExam.Aca_Semester.TermId.ToStringPadLeft(2, '0')}{selectedProgram.HR_Department.DepartmentNo.ToStringPadLeft(2, '0')}{applicantJson.UserName.Trim()}";
                //var username =  $"{admExam.Trim()}{selectedProgram.Code.ToInt32().ToStringPadLeft(2, '0')}{employeeJson.UserName.Trim().ToInt32().ToStringPadLeft(SiteSettings.Instance.StudentIdSerialDigitCount, '0')}";

                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.RepeatableRead))
                {

                    //getting username
                    //validate userName/classroll/registrationNumber

                    var username = "";

                    if (!Facade.HrAreaFacade.EmployeeFacade.GenerateEmpUserName(employeeJson, out username))
                    {
                        result.HasError = true;
                        result.Errors.Add("Auto User Name Generation Error, please try again!");
                        return result;
                    }
                    //employeeJson.Id = username;//identity column
                    employeeJson.UserName = username;


                    var userAccount = GetNewUser_Account(employeeJson);

                    User_AccountDataService accountService = new User_AccountDataService(DbInstance, HttpUtil.Profile);
                    User_Account dbAddedObj = new User_Account();
                    if (!accountService.Save(userAccount, ref dbAddedObj, out error, scope))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                        return result;
                    }
                    DbInstance.SaveChanges();
                    employeeJson.Id = dbAddedObj.Id.ToString();
                    User_EmployeeDataService studentService = new User_EmployeeDataService(DbInstance, HttpUtil.Profile);
                    var userStudent = GetNewUser_Employee(employeeJson, dbAddedObj);
                    if (!studentService.Save(userStudent, out error, scope))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                        return result;
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


            result.Data = employeeJson;
            return result;
        }

        private bool IsValidToSaveNewEmployee(AddNewEmployeeStep1Json newObj, out string error)
        {
            error = string.Empty;

            if (newObj == null)
            {
                error = "Invalid Operation! Reload the page and try again later";
                return false;
            }
            long output;
            //Id = "0",//done
            //UserName = "", /done
            //FullName = "",/done
            //AdmissionExamId = null, done
            //ProgramId = "",done//List
            //EnrollmentTypeEnumId = 0 done,
            //FeeCodeId = 0,//done
            //GenderEnumId=1,//done
            //if (!newObj.IsAutoUserName && !newObj.UserName.IsValid())
            //{
            //    error += $"Please type only last {SiteSettings.Instance.StudentIdSerialDigitCount}-digit of the Student ID.";
            //    return false;
            //}
            //if (!newObj.IsAutoUserName && !newObj.UserName.IsInt32())
            //{
            //    error += $"Please type only number for last {SiteSettings.Instance.StudentIdSerialDigitCount}-digit of the Student ID.";
            //    return false;
            //}
            //if (!newObj.IsAutoUserName && newObj.UserName.Trim().Length != 3)
            //{
            //    error += $"Please type only last {SiteSettings.Instance.StudentIdSerialDigitCount}-Digit of Student ID.";
            //    return false;
            //}
            //if (!newObj.IsAutoUserName && !newObj.UserName.Trim().IsInt32())
            //{
            //    error += "Inputed Student ID is not number.";
            //    return false;
            //}
            if (!newObj.FullName.IsValid())
            {
                error += "Full Name is not valid.";
                return false;
            }
            if (newObj.FullName.Length < 4)
            {
                error += "Full Name must be at least 4 Character.";
                return false;
            }
            if (newObj.FullName.Length > 128)
            {
                error += "Full Name exceeded its maximum length 128.";
                return false;
            }
            if (newObj.UserEmail.IsValid() && newObj.UserEmail.Length > 250)
            {
                error += "User Email exceeded its maximum length 250.";
                return false;
            }

            if (newObj.UserEmail.IsValid() && !newObj.UserEmail.IsValidEmail())
            {
                error += "User Email Address is not Valid!";
                return false;
            }
            if (newObj.UserMobile.IsValid())
                newObj.UserMobile = newObj.UserMobile.ValidateMobile();

            if (newObj.UserMobile.IsValid() && !newObj.UserMobile.IsValidMobileForBD())
            {
                error += "Invalid User Mobile. It should be only 11 digits number.";
                return false;
            }
            if (newObj.GenderEnumId == (byte)User_Account.GenderEnum.Unknown)
            {
                error += "Please select valid Gender.";
                return false;
            }
            if (!newObj.DateOfBirth.IsValid())
            {
                error += "Date Of Birth is not valid.";
                return false;
            }
            if ((DateTime.Today.Year - newObj.DateOfBirth.Year) < 16)
            {
                error = "Invalid Date of Birth, Employee's age should be grater then 16y!";
                return false;
            }

            if (!newObj.JoiningDate.IsValid())
            {
                error += "Joining Date is not valid.";
                return false;
            }
            if (newObj.JoiningSemesterId <= 0)
            {
                error += "Please select valid Joining Semester.";
                return false;
            }
            if (newObj.DepartmentId <= 0)
            {
                error += "Please select valid Department.";
                return false;
            }
            //long id = 0;
            //Int64.TryParse(newObj.PositionId,out id);
            if (newObj.PositionId <= 0)
            {
                error += "Please select valid Position.";
                return false;
            }

            if (newObj.EmployeeCategoryEnumId == null || newObj.EmployeeCategoryEnumId==0)
            {
                error += "Please select valid Employee Category.";
                return false;
            }
            if (newObj.EmployeeTypeEnumId == null)
            {
                error += "Please select valid Employee Type.";
                return false;
            }
            if (newObj.JobClassEnumId == null)
            {
                error += "Please select valid Job Class.";
                return false;
            }
            if (newObj.JobTypeEnumId == null)
            {
                error += "Please select valid Job Type.";
                return false;
            }
            if (newObj.WorkingStatusEnumId == null)
            {
                error += "Please select valid Working Status.";
                return false;
            }
         
         
      
            //if (newObj.EnrollmentTypeEnumId == null)
            //{
            //    error += "Please select valid Enrollment Type.";
            //    return false;
            //}
            //if (newObj.ContinuingBatchId == null || newObj.ContinuingBatchId <= 0)
            //{
            //    error += "Please Select Valid Batch.";
            //    return false;
            //}

            error = string.Empty;
            return true;
        }

        private static User_Account GetNewUser_Account(AddNewEmployeeStep1Json employeeJson)
        {
            User_Account obj = new User_Account();
            //obj.Id = 0; //identity auto increment 
            obj.UserName = employeeJson.UserName;
            obj.FullName = employeeJson.FullName;
            obj.BanglaName = "";
            obj.DateOfBirth = employeeJson.DateOfBirth;
            obj.PlaceOfBirthCity = null;
            obj.PlaceOfBirthCountryId = 1;
            obj.GenderEnumId = employeeJson.GenderEnumId;
            //obj.Gender = User_Account.GenderEnum.Unknown;
            //obj.ReligionEnumId = null;
            obj.Religion = User_Account.ReligionEnum.Unknown;
            //obj.BloodGroupEnumId = null;
            obj.BloodGroup = EnumCollection.Common.BloodGroupEnum.Unknown;
            //obj.MaritalStatusEnumId = null;
            obj.MaritalStatus = User_Account.MaritalStatusEnum.Unknown;
            obj.FatherName = string.Empty;
            obj.MotherName = string.Empty;
            obj.SpouseName = null;
            obj.UserMobile = employeeJson.UserMobile;
            obj.UserMobilePrivacyEnumId = 0;
            obj.UserMobilePrivacy = User_Account.UserContactPrivacyEnum.AdminstrationOnly;
            obj.IsVerifiedUserMobile = false;
            obj.UserEmail = employeeJson.UserEmail;
            obj.UserEmailPrivacyEnumId = (byte)User_Account.UserContactPrivacyEnum.AdminstrationOnly; ;
            obj.IsVerifiedUserEmaill = false;
            obj.EmergencyContactPersonName = null;
            obj.EmergencyMobile = null;
            obj.EmergencyContactAdress = null;
            obj.EmergencyContactRelationshipId = null;
            obj.Height = null;
            obj.IsLate = false;
            obj.CampusId = 1;//
            obj.Nationality = "Bangladeshi";//
            obj.NationalIdNumber = null;
            obj.BirthCertificateNumber = null;
            obj.BankAccountNo = null;
            obj.BankName = null;
            obj.PassportNo = null;
            //obj.UserTypeEnumId = 0;
            obj.UserType = User_Account.UserTypeEnum.Employee;
            // obj.UserStatusEnumId = 0;
            obj.UserStatus = User_Account.UserStatusEnum.Inactive;//todo update in 2nd step//Active=admission fee paid,
            obj.IsApproved = false;//todo update in 2nd step
            obj.IsProfileCompleted = false;//todo update in 2nd step

            obj.IsMigrate = false;
            obj.MigrationId = null;
            
            obj.IsUserCanEditProfile = false;
            obj.IsDocumentPending = true;//updated
           
            obj.Password = RandomPassword.GenerateOnlyNumber(5).ToString(); ;
            obj.PasswordSalt = string.Empty;
            obj.LastLoginDate = null;//make it null
            obj.LastPasswordChangedDate = null;//make it null
            obj.LastLockoutDate = null;//make it null
            obj.FailedPasswordAttemptCount = 0;
            obj.FailedPasswordAttemptWindowStart = null;//make it null
            obj.FailedPasswordAnswerAttemptCount = 0;
            obj.FailedPasswordAnswerAttemptWindowStart = null;//make it null
            obj.Remarks = null;
            obj.LockReason = null;
            obj.JoiningSemesterId = employeeJson.JoiningSemesterId;

            obj.JoiningDate = employeeJson.JoiningDate;
            obj.LeavingDate = null;
            obj.DepartmentId = employeeJson.DepartmentId;

            obj.CreateDate = DateTime.Now;
            obj.CreateById = HttpUtil.Profile.UserId;
            obj.LastChanged = DateTime.Now;
            obj.LastChangedById = HttpUtil.Profile.UserId;
            //obj.RankId = null;
            return obj;
        }

        private User_Employee GetNewUser_Employee(AddNewEmployeeStep1Json employeeJson, User_Account account)
        {

            User_Employee obj = User_Employee.GetNew();
            obj.Id = account.Id;// auto increment
            obj.UserId = account.Id;//

            obj.EmployeeCategoryEnumId = (byte)employeeJson.EmployeeCategoryEnumId;
            obj.EmployeeTypeEnumId = (byte)employeeJson.EmployeeTypeEnumId;
            obj.JobClassEnumId = (byte)employeeJson.JobClassEnumId;
            obj.JobTypeEnumId = (byte)employeeJson.JobTypeEnumId;
            obj.WorkingStatusEnumId = (byte)employeeJson.WorkingStatusEnumId;
            obj.TinNumber = "";
            obj.ImmediateSuperVisorId = null;
            obj.IsAcademician = false || obj.EmployeeCategory==User_Employee.EmployeeCategoryEnum.FacultyMember;

            obj.IncrementMonthEnumId = employeeJson.JoiningDate.ToString("MM").ToInt32(); 
            obj.JoiningDepartmentId = employeeJson.DepartmentId;
            //int id = 0;
            //Int32.TryParse(employeeJson.PositionId, out id);
            obj.PositionId = employeeJson.PositionId;
            obj.HasAdminAccess = false;


            obj.CreateDate = DateTime.Now;
            obj.CreateById = HttpUtil.Profile.UserId;

            obj.LastChanged = DateTime.Now;
            obj.LastChangedById = HttpUtil.Profile.UserId;

            return obj;
        }


    }

}