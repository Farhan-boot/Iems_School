//File: UI Controller

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Permissions;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
using EMS.Web.Jsons.Custom.Academic;
using EMS.Web.Jsons.Custom.HR;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{

    public class StudentApiController : EmployeeBaseApiController
    {
        public StudentApiController()
        {  }

        #region general Webapi
        #region for List
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        //This WeApi is used for both StudentList and StudentListEdit
        public HttpListResult<User_StudentJson> GetPagedStudent(
            string textkey
            ,string classRoll
            ,string userName
            ,string ugcUniqueId
            , int? pageSize
            ,int? pageNo
            ,int statusId = -1
            ,long admissionStatusEnumId = -1
            ,long deptId = -1
            , long programId = -1//new
            , long batchId = -1//new
            , long levelTermId = -1
            , long bloodId = -1//new
             , long genderId = -1//new
            , bool isShowTrashedStudent=false
            ,int religionId=-1
            ,long admissionSemesterId=-1
            ,int studentQuotaId = -1
            , string studentMobileNo = ""
            , string studentEmail = ""
            , string presentDistrict = ""
            , string birthCertificateNo = ""
            , string nationalIdNo = ""
            , string passportNo = ""
            , string permanentDistrict = ""
            , string fatherName = ""
            , int parentsJobTypeId = -1
            ,string motherName = ""
            ,bool isNeverLoggedIn = false
            ,bool isShowPassword = false
            ,string admissionStartDate = ""
            ,string admissionEndDate = ""
            ,int searchByGenderEnumId = -1
            ,int searchByEnrolmentEnumId = -1
            ,string studentsBirthDate = ""
            ,bool isIncludeContactAddress = false
            )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<User_StudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                
                using (User_StudentDataService studentDataService = new User_StudentDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<User_Student> query = DbInstance.User_Student
                        .Include(x=>x.User_Account)
                        //.Include(x=>x.User_Account.User_Rank)
                        //.Include(x=>x.User_Account.User_ContactNumber)
                        //.Include(x=>x.User_Account.User_ContactEmail)
                         //.Include(x=>x.HR_Department)
                        .Include(x=>x.Aca_Program)
                        .Include(x=>x.Aca_DeptBatch)
                        .Include(x=>x.Aca_StudyLevelTerm)
                        .Include(x=>x.Aca_ClassSection)
                        //.Include(x=>x.Aca_StudyTerm)
                        .Where(x=>x.IsDeleted== isShowTrashedStudent) 
                        .OrderBy(x => x.ProgramId)
                        .ThenByDescending(x => x.RegistrationSession)
                        .ThenBy(x => x.ClassRollNo);

                    if (isIncludeContactAddress)
                    {
                        query = query.Include(x => x.User_Account.User_ContactAddress);
                    }
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.User_Account.FullName.ToLower().Contains(textkey.ToLower()));
                    }
                    if (userName.IsValid())
                    {
                        query = query.Where(q => q.User_Account.UserName.ToLower().Contains(userName.ToLower()));
                    }
                    if(ugcUniqueId.IsValid())
                    {
                        query = query.Where(q => q.UgcUniqueId.ToLower().Contains(ugcUniqueId.ToLower()));
                    }
                    if (classRoll.IsValid())
                    {
                        int roll = Convert.ToInt32(classRoll);
                        query = query.Where(q => q.ClassRollNo.Equals(roll));
                    }
                    if (deptId > -1)
                    {
                        query = query.Where(q => q.User_Account.DepartmentId == deptId);
                    }
                    if (programId > -1)
                    {
                        query = query.Where(q => q.ProgramId == programId);
                    }
                    if (levelTermId > 0)
                    {
                        query = query.Where(q => q.LevelTermId == levelTermId);
                    }
                    //if (termId > 0)
                    //{
                    //    query = query.Where(q => q.TermId == termId);
                    //}
                    if (batchId > 0 )
                    {
                        query = query.Where(q => q.ContinuingBatchId == batchId);
                    }
                   
                    if (admissionStatusEnumId > -1)
                    {
                        query = query.Where(q => q.AdmissionStatusEnumId == admissionStatusEnumId);
                    }
                    if (statusId > -1)
                    {
                        query = query.Where(q => q.EnrollmentStatusEnumId == statusId);
                    }
                    if (bloodId > -1)
                    {
                        query = query.Where(q => q.User_Account.BloodGroupEnumId == bloodId);
                    }
                    if (genderId > -1)
                    {
                        query = query.Where(q => q.User_Account.GenderEnumId == genderId);
                    }
                    if (religionId > -1)
                    {
                        query = query.Where(q => q.User_Account.ReligionEnumId == religionId);
                    }
                    if (admissionSemesterId > -1)
                    {
                        query = query.Where(q => q.User_Account.JoiningSemesterId == admissionSemesterId);
                    }

                    if (studentQuotaId > -1)
                    {
                        query = query.Where(q => q.StudentQuotaNameId == studentQuotaId);
                    }

                    if (searchByGenderEnumId > -1)
                    {
                        query = query.Where(q => q.User_Account.GenderEnumId == searchByGenderEnumId);
                    }

                    if (searchByEnrolmentEnumId > -1)
                    {
                        query = query.Where(q => q.EnrollmentTypeEnumId == searchByEnrolmentEnumId);
                    }

                    if (studentMobileNo.IsValid())
                    {
                        query = query.Where(q => q.User_Account.UserMobile.ToLower().Contains(studentMobileNo.ToLower()) || q.User_Account.UserMobile2.ToLower().Contains(studentMobileNo.ToLower()));
                    }

                    if (studentEmail.IsValid())
                    {
                        query = query.Where(q => q.User_Account.UserEmail.ToLower().Contains(studentEmail.ToLower()));
                    }

                    if (presentDistrict.IsValid())
                    {
                        query = query.Where(q => q.User_Account.PlaceOfBirthCity.ToLower().Contains(presentDistrict.ToLower()));
                    }
                    if (birthCertificateNo.IsValid())
                    {
                        query = query.Where(q => q.User_Account.BirthCertificateNumber.ToLower().Contains(birthCertificateNo.ToLower()));
                    }
                    if (passportNo.IsValid())
                    {
                        query = query.Where(q => q.User_Account.PassportNo.ToLower().Contains(passportNo.ToLower()));
                    }
                    if (nationalIdNo.IsValid())
                    {
                        query = query.Where(q => q.User_Account.NationalIdNumber.ToLower().Contains(nationalIdNo.ToLower()));
                    }
                    //if (permanentDistrict.IsValid())
                    //{
                    //    query = query.Where(q => q.User_Account.UserEmail.ToLower().Contains(studentEmail.ToLower()));
                    //}
                    if (fatherName.IsValid())
                    {
                        query = query.Where(q => q.User_Account.FatherName.ToLower().Contains(fatherName.ToLower()));
                    }
                    if (motherName.IsValid())
                    {
                        query = query.Where(q => q.User_Account.MotherName.ToLower().Contains(motherName.ToLower()));
                    }

                    //todo not working.
                    if (parentsJobTypeId > -1)
                    {
                        query = query.Where(q => q.ParentsPrimaryJobTypeId== parentsJobTypeId);
                    }
                    if (isNeverLoggedIn)
                    {
                        query = query.Where(q => q.User_Account.LastLoginDate == DateTimeHelper.SqlMinDateTime || q.User_Account.LastLoginDate == null);
                    }

                    #region DateTimeChecking.

                    DateTime admissionStartDateOnlyDate = Convert.ToDateTime(admissionStartDate).ToOnlyDate();
                    DateTime admissionEndDateOnlyDate = Convert.ToDateTime(admissionEndDate).ToOnlyDate();
                    DateTime birthOnlyDate = Convert.ToDateTime(studentsBirthDate).ToOnlyDate();

                    if (admissionStartDate.IsValid() && admissionStartDate.IsDateTime() && admissionEndDate.IsValid() && admissionEndDate.IsDateTime())
                    {
                        if (admissionEndDateOnlyDate < admissionStartDateOnlyDate)
                        {
                            result.HasError = true;
                            result.Errors.Add("Start Date cannot be less than End date.");
                            return result;
                        }
                        query = query.Where(q => EntityFunctions.TruncateTime(q.CreateDate) >= admissionStartDateOnlyDate && EntityFunctions.TruncateTime(q.CreateDate) <= admissionEndDateOnlyDate);
                    }
                    else
                    {
                        if (admissionStartDate.IsValid() && admissionStartDate.IsDateTime())
                        {
                            query = query.Where(q => EntityFunctions.TruncateTime(q.CreateDate) == admissionStartDateOnlyDate);
                        }
                    }

                    if (studentsBirthDate.IsValid() && studentsBirthDate.IsDateTime())
                    {
                        query = query.Where(q => EntityFunctions.TruncateTime(q.User_Account.DateOfBirth) == birthOnlyDate);
                    }
                    #endregion

                    if (pageSize <= 0)
                    {
                        pageSize = null;
                    }
                    if (pageNo <= 0)
                    {
                        pageNo = null;
                    }

                    var entities = studentDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<User_StudentJson>();
                    entities.Map(jsons);

                    if (PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanViewPassword, out error) && isShowPassword)
                    {
                        int listLength = entities.Count;
                        for (int i = 0; i < listLength; i++)
                        {
                            if (jsons[i].Id == entities[i].Id && !entities[i].User_Account.PasswordSalt.IsValid())
                            {

                                jsons[i].Password = entities[i].User_Account.Password;

                            }
                        }
                    }

                    result.Data = jsons;
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
        private HttpListResult<User_StudentJson> GetAllStudent()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_StudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_StudentDataService studentDataService = new User_StudentDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<User_StudentJson>();
                    var entities = studentDataService.GetAll(out error);
                    entities.Map(jsons);
                    result.Data = jsons;
                    result.Count = jsons.Count;
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
        [HttpPost]
        public HttpListResult<User_StudentJson> SaveStudentList(IList<User_StudentJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_StudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_StudentDataService studentDataService = new User_StudentDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<User_Student>();
                    var dbAttachedListEntity = new List<User_Student>();
                    jsonList.Map(entityListReceived);

                    foreach (var entity in entityListReceived)
                    {
                        //if (studentDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<AcademicStudentJson> GetStudentById(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<AcademicStudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var json = new AcademicStudentJson();
            json.Account = new User_AccountJson();
            json.Student = new User_StudentJson();
            json.Image = new User_ImageJson();
            json.ContactAddressList = new List<User_ContactAddressJson>();
            json.ContactEmailList = new List<User_ContactEmailJson>();
            json.ContactNumberList = new List<User_ContactNumberJson>();
            json.EducationList = new List<User_EducationJson>();

            try
            {
                User_Student studentEntity;
                User_Account accountEntity;
                List<User_ContactNumber> contatNumberList = new List<User_ContactNumber>();
                List<User_ContactEmail> contatEmailList = new List<User_ContactEmail>();
                List<User_ContactAddress> contatAddressList = new List<User_ContactAddress>();
                List<User_Education> educationList = new List<User_Education>();
                using (User_StudentDataService studentDataService = new User_StudentDataService(DbInstance, HttpUtil.Profile))
                {
                    if (id <= 0)
                    {
                        studentEntity = User_Student.GetNew();
                    }
                    else
                    {
                        studentEntity = studentDataService.GetById(id);
                    }
                    studentEntity.Map(json.Student);
                }

                using (User_AccountDataService accountDataService = new User_AccountDataService(DbInstance, HttpUtil.Profile))
                {
                    if (id <= 0)
                    {
                        accountEntity = User_Account.GetNew();
                    }
                    else
                    {
                        accountEntity = accountDataService.GetById(studentEntity.UserId);
                        if (accountEntity.PasswordSalt.IsValid())
                        {
                          accountEntity.Password = string.Empty;
                        }

                        
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
                        contatNumberList = contactNumberDataService.GetAllByUserId(studentEntity.UserId)
                            .OrderBy(x => x.ContactNumberTypeEnumId).ToList();
                    }
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
                        contatEmailList = contactEmailDataService.GetAllByUserId(studentEntity.UserId)
                            .OrderBy(x => x.ContactEmailTypeEnumId).ToList();
                    }
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
                        contatAddressList = contactAddressDataService.GetAllByUserId(studentEntity.UserId)
                            .OrderByDescending(x => x.ContactAddressTypeEnumId).ToList();
                    }
                    contatAddressList.Map(json.ContactAddressList);
                }
                using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
                {
                    if (id <= 0)
                    {
                        User_Education newEducations = User_Education.GetNew();
                        educationList.Add(newEducations);
                    }
                    else
                    {
                        educationList = educationDataService.GetAllByUserId(studentEntity.UserId)
                            .OrderByDescending(x => x.DegreeEquivalentEnumId).ToList();
                    }
                    educationList.Map(json.EducationList);
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
        
        [HttpPost]
        public HttpResult<AcademicStudentJson> SaveStudent(AcademicStudentJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<AcademicStudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            if (!IsValidToSaveStudent(json, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            //User
            var listContactNumber = new List<User_ContactNumber>();
            json.ContactNumberList.Map(listContactNumber);

            var listContactEmail = new List<User_ContactEmail>();
            json.ContactEmailList.Map(listContactEmail);

            var listContactAddress = new List<User_ContactAddress>();
            json.ContactAddressList.Map(listContactAddress);

            var listEducation = new List<User_Education>();
            json.EducationList.Map(listEducation);

            var userAccount = new User_Account();
            json.Account.Map(userAccount);
            userAccount.User_ContactNumber = new List<User_ContactNumber>();
            userAccount.User_ContactEmail = new List<User_ContactEmail>();
            userAccount.User_ContactAddress = new List<User_ContactAddress>();
            userAccount.User_Education = new List<User_Education>();

            userAccount.UserTypeEnumId = (byte)User_Account.UserTypeEnum.Student;
            //userAccount.IsMilitary = userAccount.CategoryEnumId != (byte)User_Account.UserCategoryEnum.Civil;

            userAccount.User_ContactNumber = listContactNumber;
            userAccount.User_ContactEmail = listContactEmail;
            userAccount.User_ContactAddress = listContactAddress;
            userAccount.User_Education = listEducation;

            //Student
            var userStudent = new User_Student();
            json.Student.Map(userStudent);

            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    if (Facade.AcademicFacade.StudentFacade.SaveAccount(userAccount, out error, scope))
                    {
                        if (!Facade.AcademicFacade.StudentFacade.SaveStudent(userStudent, out error, scope))
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
                    result.HasError = true;
                    result.Errors.Add(ex.ToString());
                    scope.Rollback();
                }
            }
            result.Data = json;
            return result;
        }

        private bool IsValidToSaveStudent(AcademicStudentJson json, out string error)
        {
            error = string.Empty;
            //TODO Have to remove this check. (Temporary)
            if (HttpUtil.Profile.UserName.Equals("psdeveloper") ||
                HttpUtil.DbContext.UAC_RoleUserMap.Any(x => x.UserId == HttpUtil.Profile.UserId))
            {
                return true;
            }
            if (json == null 
                || json.Account == null 
                ||json.Student == null
                )
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
            //if (json.Account.NationalIdNumber.IsValid()
            //    && json.Account.NationalIdNumber.Length < 13
            //    && output == 1)
            //{
            //    error = "Bangladeshi National ID must be 13 to 17 digit!";
            //    return false;
            //}
            //long.TryParse(json.Account.RankId, out output);
            //if (output <= 0)
            //{
            //    error = "Invalid Rank!";
            //    return false;
            //}
            //long.TryParse(json.Account.CampusId, out output);
            if (json.Account.CampusId <= 0)
            {
                error = "Invalid Campus!";
                return false;
            }

            /***
            //Student Checking
            ***/
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
            if (!json.ContactNumberList
                .Single(x => x.ContactNumberTypeEnumId == (byte)User_ContactNumber.ContactNumberTypeEnum.PersonalMobile)
                .ContactNumber.IsValid())
            {
                error = "Personal Mobile can't blank!";
                return false;
            }
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
            if (!json.ContactEmailList
                .Single(x => x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail)
                .ContactEmail.IsValid())
            {
                error = "Personal Email can't blank!";
                return false;
            }
            //if (!json.ContactEmailList
            //    .Single(x => x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.OfficeEmail)
            //    .ContactEmail.IsValid() &&
            //    json.Employee.JobClassEnumId == (byte)EnumCollection.Common.JobClassEnum.FirstClass)
            //{
            //    error = "Office Email can't blank!";
            //    return false;
            //}
            var mistEmail = json.ContactEmailList
                .SingleOrDefault(x => x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.OfficeEmail);
            if (mistEmail != null && mistEmail.ContactEmail.IsValid() &&
                !mistEmail.ContactEmail.Contains(SiteSettings.Instance.InstituteDomain))
            {
                error = "Invalid "+ SiteSettings.Instance.InstituteShortName + " Email!";
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
        private HttpResult<User_StudentJson> GetDeleteStudentById(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_StudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_StudentDataService studentDataService = new User_StudentDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!studentDataService.DeleteById(id, out error))
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

        public HttpResult<User_StudentJson> GetTrashStudentById(int stdId=0,bool isPutInTrash=false)
        {
            string error = string.Empty;
            var result = new HttpResult<User_StudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanTrash, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                if (stdId<=0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Student ID!");
                    return result;
                }

                var student = DbInstance.User_Student
                    .Include(x => x.User_Account)
                    .FirstOrDefault(x => x.Id == stdId);

                if (student==null)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Student ID!");
                    return result;
                }

                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (isPutInTrash)
                    {
                        if (student.IsDeleted)
                        {
                            result.HasError = true;
                            result.Errors.Add("This Student Already Deleted.");
                            return result;
                        }


                        // Check Registration History
                        var isClassTaken = DbInstance.Aca_ClassTakenByStudent
                            .Any(x => x.StudentId == student.Id
                                      && x.RegistrationStatusEnumId != (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.Cancelled);

                        if (isClassTaken)
                        {
                            result.HasError = true;
                            result.Errors.Add("This Student has registered courses. First delete those. Then try again.");
                            return result;
                        }



                        // Check Accounts History
                        /*var isHasAccountCredit = DbInstance.Acnt_StdTransaction.Any(x => x.StudentId == student.Id
                                                                                         && !x.IsDebited
                                                                                         && !x.IsDeleted);
                        if (isHasAccountCredit)
                        {
                            result.HasError = true;
                            result.Errors.Add("This Student have same payment transaction history. First delete those. Then try again.");
                            return result;
                        }*/

                        string trashUserName=String.Empty;
                        if (student.ClassRollNo.IndexOf('-') != 0)
                        {
                            trashUserName = $"-{student.ClassRollNo}";
                        }

                        var isAlreadyHasTrashList = DbInstance.User_Student
                            .Include(x => x.User_Account)
                            .Any(x => x.ClassRollNo == trashUserName
                                      || x.User_Account.UserName == trashUserName
                            );

                        if (isAlreadyHasTrashList)
                        {
                            result.HasError = true;
                            result.Errors.Add("This Student ID already in trash list. First change that ID. Then try again.");
                            return result;
                        }


                        if (student.ClassRollNo.IndexOf('-') != 0)
                        {
                            student.ClassRollNo = $"-{student.ClassRollNo}";
                        }

                        if (student.User_Account.UserName.IndexOf('-') != 0)
                        {
                            student.User_Account.UserName = $"-{student.User_Account.UserName}";
                        }

                      



                        student.IsDeleted = true;
                        student.User_Account.IsDeleted = true;
                        student.User_Account.UserStatusEnumId = (byte) User_Account.UserStatusEnum.Inactive;
                    }
                    else
                    {
                        if (!student.IsDeleted)
                        {
                            result.HasError = true;
                            result.Errors.Add("This Student Already UnDeleted.");
                            return result;
                        }

                        string originalUserName = student.User_Account.UserName;

                        // Remove - from UserName
                        for (int i=0; i<originalUserName.Length; i++)
                        {
                            if (originalUserName.IndexOf('-') == 0)
                            {
                                originalUserName = originalUserName.Remove(0, 1);
                            }
                            else
                            {
                                break;
                            }
                        }

                        var isExistUserName = DbInstance.User_Student
                            .Include(x => x.User_Account)
                            .Any(x => x.ClassRollNo == originalUserName
                                      || x.User_Account.UserName == originalUserName
                            );

                        if (isExistUserName)
                        {
                            result.HasError = true;
                            result.Errors.Add($"You can't restore this User, Because Student ID:{originalUserName} Already Exist.");
                            return result;
                        }

                        student.ClassRollNo = originalUserName;
                        student.User_Account.UserName = originalUserName;


                        // Old Logic
                        /*if (student.User_Account.UserName.IndexOf('-') == 0)
                        {
                            string originalUserName = student.User_Account.UserName.Remove(0, 1);


                            var isExistUserName = DbInstance.User_Student
                                .Include(x=>x.User_Account)
                                .Any(x => x.ClassRollNo== originalUserName
                                          ||x.User_Account.UserName == originalUserName
                                );

                            if (isExistUserName)
                            {
                                result.HasError = true;
                                result.Errors.Add($"You can't restore this User, Because Student ID:{originalUserName} Already Exist.");
                                return result;
                            }

                        }

                        if (student.ClassRollNo.IndexOf('-') == 0)
                        {
                            student.ClassRollNo = student.ClassRollNo.Remove(0, 1);
                        }
                        if (student.User_Account.UserName.IndexOf('-') == 0)
                        {
                            student.User_Account.UserName = student.User_Account.UserName.Remove(0, 1);
                        }*/

                      
                        student.IsDeleted = false;
                        student.User_Account.IsDeleted = false;


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

        #region Custom WebApi
        private HttpResult<User_StudentJson> GetStudentByIdWithChild(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_StudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_StudentDataService studentDataService = new User_StudentDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new User_StudentJson();
                    User_Student entity;
                    if (id <= 0)
                    {
                        entity = User_Student.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("User_Student");
                        //includeTables.Add("User_Student");

                        entity = studentDataService.GetById(id, includeTables.ToArray());
                    }
                    entity.Map(json);
                    result.Data = json;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }
        public HttpListResult<User_StudentJson> GetstudentDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_StudentJson>();
            try
            {
                //User_StudentDataService studentDataService =
                //new User_StudentDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //result.DataExtra.StudentQuotaEnumList = EnumUtil.GetEnumList(typeof(User_Student.StudentQuotaEnum));
                result.DataExtra.EnrolmentStatusEnumList = EnumUtil.GetEnumList(typeof(User_Student.EnrollmentStatusEnum));
                result.DataExtra.EnrolmentTypeEnumList = EnumUtil.GetEnumList(typeof(User_Student.EnrollmentTypeEnum));
                //result.DataExtra.ParentsPrimaryJobTypeEnumList = EnumUtil.GetEnumList(typeof(User_Student.ParentsPrimaryJobTypeEnum));
                //result.DataExtra.GradeTypeEnumList = EnumUtil.GetEnumList(typeof(User_Student.GradeTypeEnum));

                result.DataExtra.UserTypeEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserTypeEnum));
                result.DataExtra.UserCategoryEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserCategoryEnum));
                result.DataExtra.UserStatusEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserStatusEnum));
                result.DataExtra.BloodGroupEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.BloodGroupEnum));
                result.DataExtra.GenderEnumList = EnumUtil.GetEnumList(typeof(User_Account.GenderEnum));
                result.DataExtra.MaritalStatusEnumList = EnumUtil.GetEnumList(typeof(User_Account.MaritalStatusEnum));
                result.DataExtra.ReligionEnumList = EnumUtil.GetEnumList(typeof(User_Account.ReligionEnum));

                result.DataExtra.ContactAddressTypeEnumList = EnumUtil.GetEnumList(typeof(User_ContactAddress.ContactAddressTypeEnum));
                result.DataExtra.ContactEmailTypeEnumList = EnumUtil.GetEnumList(typeof(User_ContactEmail.ContactEmailTypeEnum));
                result.DataExtra.ContactNumberTypeEnumList = EnumUtil.GetEnumList(typeof(User_ContactNumber.ContactNumberTypeEnum));
                result.DataExtra.PrivacyEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.PrivacyEnum));

                result.DataExtra.EducationTypeEnumList = EnumUtil.GetEnumToUpperList(typeof(Adm_EducationBoard.BoardTypeEnum));
                result.DataExtra.DegreeEquivalentEnumList = EnumUtil.GetEnumToUpperList(typeof(User_Education.DegreeEquivalentEnum));
                //DropDown Option Tables
                //TODO NEED TO FIX  data extra name 
                result.DataExtra.StudentQuotaEnumList = DbInstance.Adm_StudentQuotaName.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                result.DataExtra.ParentsPrimaryJobTypeEnumList = DbInstance.Adm_ParentsPrimaryJobType.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                result.DataExtra.GradeTypeEnumList = DbInstance.Aca_GradingPolicy.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.StudyLevelList = DbInstance.Aca_StudyLevel.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                result.DataExtra.StudyTermList = DbInstance.Aca_StudyTerm.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                result.DataExtra.ClassSectionList = DbInstance.Aca_ClassSection.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                result.DataExtra.CurriculumList = DbInstance.Aca_Curriculum.AsEnumerable()
                    .OrderBy(x => x.CurriculumNo)
                    .ThenByDescending(x => x.Year)
                    .Select(t => new
                { Id = t.Id.ToString(), Name = t.ShortName }).ToList();
                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.ShortTitle }).ToList();
                result.DataExtra.DeptBatchList = DbInstance.Aca_DeptBatch.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).OrderBy(x=>x.Name).ToList();

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
                        Category = t.Category.ToString().AddSpacesToSentence()
                    });
                
                result.DataExtra.BankList = DbInstance.General_Bank.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                    });
                result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable()
                    .OrderBy(x => x.ShortName)
                    .Where(x => x.TypeEnumId == 2)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name, ShortName = t.ShortName })
                    .ToList();
                
                result.DataExtra.CountryList = DbInstance.General_Country.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                        CallingCode = "(" + t.CallingCode + ") " + t.Name
                    });
                
                result.DataExtra.CampusList = DbInstance.General_Campus.AsEnumerable()
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
        //This WeApi is used for both StudentList and StudentListEdit
        public HttpResult<User_StudentJson> GetstudentListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpResult<User_StudentJson>();
            try
            {
                result.DataExtra.EnrolmentStatusEnumList = EnumUtil.GetEnumList(typeof(User_Student.EnrollmentStatusEnum));
                result.DataExtra.EnrolmentTypeEnumList = EnumUtil.GetEnumList(typeof(User_Student.EnrollmentTypeEnum));
                result.DataExtra.AdmissionStatusEnumList = EnumUtil.GetEnumList(typeof(User_Student.AdmissionStatusEnum));
                result.DataExtra.BloodGroupEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.BloodGroupEnum));
                result.DataExtra.ReligionEnumList = EnumUtil.GetEnumList(typeof(User_Account.ReligionEnum));
                result.DataExtra.GenderEnumList = EnumUtil.GetEnumList(typeof(User_Account.GenderEnum));
                //result.DataExtra.CampusList = DbInstance.General_Campus.AsEnumerable()
                //    .Select(t => new
                //    {
                //        Id = t.Id.ToString(),
                //        Name = t.ShortName
                //    });
                result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable()
                    .OrderBy(x => x.ShortName)
                    .Where(x => x.TypeEnumId == 2 && !x.IsDeleted)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name, ShortName = t.ShortName })
                    .ToList();
                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable()
                   .OrderBy(x => x.ShortName)
                   .Where(x => !x.IsDeleted)
                   .Select(t => new
                   { Id = t.Id, Name = t.Name, ShortName = t.ShortName, ShortTitle = t.ShortTitle })
                   .ToList();

                result.DataExtra.SemesterList = Aca_Semester.GetDropdownList(DbInstance);
                result.DataExtra.QuotaList = DbInstance.Adm_StudentQuotaName.AsEnumerable()
                    .OrderByDescending(x => x.Id)
                    .Select(t => new
                        { Id = t.Id, Name = t.Name })
                    .ToList();
                result.DataExtra.ParentsPrimaryJobTypeEnumList = DbInstance.Adm_ParentsPrimaryJobType.AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.Name }).ToList();
                /*result.DataExtra.LevelList = DbInstance.Aca_StudyLevel.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name
                });
                result.DataExtra.TermList = DbInstance.Aca_StudyTerm.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name
                });*/
                result.DataExtra.LevelTermList = DbInstance.Aca_StudyLevelTerm.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name
                });
                result.DataExtra.ClassSectionList = DbInstance.Aca_ClassSection.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name
                });
                result.DataExtra.DeptBatchList = DbInstance.Aca_DeptBatch.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name
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
