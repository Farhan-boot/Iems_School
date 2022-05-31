using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EMS.Communication.EmailProxy;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.Web.Framework.Bases.WebApi;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Jsons.Custom.HR;
using EMS.Web.Jsons.Custom.HR.Employee;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.HR.Controllers.WebApi
{
    public class EmployeeProfileApiController : EmployeeBaseApiController
    {

        public HttpResult<User_AccountJson> GetEmployeeAccountDataExtra()
        {
            string error = string.Empty;
            var result = new HttpResult<User_AccountJson>();
            try
            {
                result.DataExtra.EnrollmentStatusEnumList = EnumUtil.GetEnumList(typeof(User_Student.EnrollmentStatusEnum));
                result.DataExtra.EnrolmentTypeEnumList = EnumUtil.GetEnumList(typeof(User_Student.EnrollmentTypeEnum));

                result.DataExtra.UserTypeEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserTypeEnum));

                result.DataExtra.UserStatusEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserStatusEnum));
                result.DataExtra.UserContactPrivacyEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserContactPrivacyEnum));

                result.DataExtra.BloodGroupEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.BloodGroupEnum));
                result.DataExtra.GenderEnumList = EnumUtil.GetEnumList(typeof(User_Account.GenderEnum));
                result.DataExtra.MaritalStatusEnumList = EnumUtil.GetEnumList(typeof(User_Account.MaritalStatusEnum));

                result.DataExtra.ReligionEnumList = EnumUtil.GetEnumList(typeof(User_Account.ReligionEnum));

                result.DataExtra.EducationTypeEnumList = EnumUtil.GetEnumToUpperList(typeof(Adm_EducationBoard.BoardTypeEnum));
                result.DataExtra.DegreeEquivalentEnumList = EnumUtil.GetEnumToUpperList(typeof(User_Education.DegreeEquivalentEnum));

                result.DataExtra.IncrementMonthEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.MonthEnum));
                result.DataExtra.WorkingStatusEnumList = EnumUtil.GetEnumList(typeof(User_Employee.WorkingStatusEnum));
                result.DataExtra.JobClassEnumList = EnumUtil.GetEnumList(typeof(User_Employee.JobClassEnum));
                result.DataExtra.JobTypeEnumList = EnumUtil.GetEnumList(typeof(User_Employee.JobTypeEnum));
                result.DataExtra.EmployeeCategoryEnumList = EnumUtil.GetEnumList(typeof(User_Employee.EmployeeCategoryEnum));
                result.DataExtra.EmployeeTypeEnumList = EnumUtil.GetEnumList(typeof(User_Employee.EmployeeTypeEnum));




                var depts = DbInstance.HR_Department
                    .OrderBy(x => x.ParentDeptId)
                    .ThenBy(x => x.Priority)
                    .ToList();

                result.DataExtra.DepartmentList = depts.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name + " (" + t.ShortName + ")",
                        Type = t.Type.ToString().AddSpacesToSentence()
                    });

                result.DataExtra.CampusList = DbInstance.General_Campus.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList();

                var positions = DbInstance.HR_Position
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


                result.DataExtra.SemesterList = DbInstance.Aca_Semester.AsEnumerable()
                    .OrderByDescending(x => x.Year)
                    .ThenByDescending(x => x.TermId)
                    .Select(t => new
                    { Id = t.Id.ToString(), Name = t.Name }).ToList();


                result.DataExtra.CountryList = DbInstance.General_Country.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                        CallingCode = "(" + t.CallingCode + ") " + t.Name
                    });



                result.DataExtra.DistrictList = DbInstance.General_District.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id.ToString(),
                        Name = t.Name,
                        CountryId = t.CountryId,
                        DivisionId = t.DivisionId,


                    });

                result.DataExtra.PoliceStationList = DbInstance.General_PoliceStation.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                        DistrictId = t.DistrictId
                    });
                result.DataExtra.PostOfficeList = DbInstance.General_PostOffice.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name + "-" + t.PostCode,
                        PoliceStationId = t.PoliceStationId,
                        DistrictId = t.DistrictId
                    });


                result.DataExtra.RelationshipList = DbInstance.User_Relationship.AsEnumerable().OrderBy(x => x.OrderNo).Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();


                result.DataExtra.EmployeeList = DbInstance.User_Employee

                    .Include(x => x.User_Account)
                    .Where(x => x.User_Account.UserTypeEnumId == (byte)User_Account.UserTypeEnum.Employee && x.PositionId != null)
                    .OrderBy(x => x.User_Account.FullName)
                    .Include(x => x.HR_Position)
                    .Include(x => x.User_Account.HR_Department)
                    .AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id.ToString(),
                        Name = $"{t.User_Account.FullName} [{t.User_Account.UserName}] {t.User_Account.HR_Department?.ShortName} ({t.HR_Position?.Name})"
                    }).ToList();

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;

        }
        public HttpListResult<User_EducationJson> GetEducationDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_EducationJson>();
            try
            {
                //User_EducationDataService educationDataService =
                //    new User_EducationDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.DegreeEquivalentEnumList = EnumUtil.GetEnumList(typeof(User_Education.DegreeEquivalentEnum));
                result.DataExtra.DegreeGroupMajorEnumList = EnumUtil.GetEnumList(typeof(User_Education.DegreeGroupMajorEnum));
                result.DataExtra.ResultSystemEnumList = EnumUtil.GetEnumList(typeof(User_Education.ResultSystemEnum));

                //DropDown Option Tables

                result.DataExtra.DegreeCategoryList = DbInstance.Adm_DegreeCategory.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                        DegreeEquivalentEnumId = t.DegreeEquivalentEnumId,
                        BoardTypeEnumId = t.BoardTypeEnumId
                    })
                    .ToList();

                result.DataExtra.EducationBoardList = DbInstance.Adm_EducationBoard.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                        BoardTypeEnumId = t.BoardTypeEnumId,
                    })
                    .ToList();


                //result.DataExtra.AccountList = DbInstance.User_Account.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult<User_EducationJson> GetNewEducationByUserId(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<User_EducationJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Education.CanAdd, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            User_EducationJson json = new User_EducationJson();

            User_Education newEducation = User_Education.GetNew();
            newEducation.UserId = id;

            newEducation.DegreeEquivalent = User_Education.DegreeEquivalentEnum.BachelorEquivalent;
            //newEducation.DegreeTitle = "Higher Secondary Certificate";
            //newEducation.DegreeCategoryId = 602;
            //newEducation.YearOfPassing =DateTime.Now.Year;


            newEducation.Map(json);
            json.IsDisable = false; //IsDisable using for DegreeEquivalentEnumList Dropdown Disable or Enable.
            json.IsRemove = true;//IsRemove using for remove button Enable of new Education.

            result.Data = json;
            return result;
        }
        public HttpResult<UpdateEmployeeJson> GetEmployeeById(int userId = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<UpdateEmployeeJson>();
            try
            {

                if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanEdit, out error))
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return result;
                }

                if (userId <= 0)
                {
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add("Invalid Employee ID, Employee not found!");
                    return result;
                }
                User_Employee employeeEntity = null;
                employeeEntity = DbInstance.User_Employee.Include(x => x.User_Account).SingleOrDefault(x => x.UserId == userId);

                //if (id > 0)
                //{
                //     employeeEntity = DbInstance.User_Employee.Find(id);
                //}
                //else if (userId > 0)
                //{
                //    employeeEntity = DbInstance.User_Employee.SingleOrDefault(x=>x.UserId== userId);
                //}



                if (employeeEntity == null)
                {
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add("Invalid Employee ID, Employee not found!");
                    return result;
                }

                var employeeJson = new UpdateEmployeeJson
                {
                    ContactAddressJsonList = new List<User_ContactAddressJson>(),
                    EducationJsonList = new List<User_EducationJson>(),
                };

                employeeEntity.Map(employeeJson);




                List<User_ContactAddress> contactAddressList = new List<User_ContactAddress>();
                List<User_Education> educationList = new List<User_Education>();


                //User_Account start
                User_Account accountEntity;

                {
                    accountEntity = employeeEntity.User_Account;


                    if (accountEntity != null && accountEntity.PasswordSalt.IsValid())
                    {
                        accountEntity.Password = string.Empty;
                    }

                    if (accountEntity != null) accountEntity.PasswordSalt = string.Empty;
                }
                accountEntity.Map(employeeJson);


                if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee
                    .CanResetPassword))
                {
                    employeeJson.Password=String.Empty;
                }


                //User_Account End

                //Contact Address List Start

                contactAddressList = DbInstance.User_ContactAddress.OrderByDescending(x => x.ContactAddressTypeEnumId).Where(c => c.UserId == employeeEntity.UserId).ToList();

                User_ContactAddress.ValidedPresentPermanentAddress(employeeEntity.UserId, contactAddressList);

                var present =
                    contactAddressList.FirstOrDefault(
                        x => x.ContactAddressType == User_ContactAddress.ContactAddressTypeEnum.Present);
                var permanent =
                   contactAddressList.FirstOrDefault(
                       x => x.ContactAddressType == User_ContactAddress.ContactAddressTypeEnum.Permanent);


                present.Map(employeeJson.PresentAddressJson = new User_ContactAddressJson());
                permanent.Map(employeeJson.PermanentAddressJson = new User_ContactAddressJson());
                //Contact Address List end

                //Education Address List Start
                using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
                {

                    educationList = educationDataService.GetAllByUserId(employeeEntity.UserId)
                        .OrderBy(x => x.DegreeEquivalentEnumId).ToList();

                    educationList = educationList.OrderBy(x => x.DegreeEquivalentEnumId).ToList();
                    educationList.Map(employeeJson.EducationJsonList);
                }
                //Education Address List end

                result.Data = employeeJson;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }


            return result;
        }

        #region Save EmployeeAccount
        [System.Web.Mvc.HttpPost]


        public HttpResult<UpdateEmployeeJson> SaveEmployee(UpdateEmployeeJson employeeJson)
        {

            string error = string.Empty;
            var result = new HttpResult<UpdateEmployeeJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }


            long output = 0;
            long.TryParse(employeeJson.Id, out output);
            if (output <= 0)
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add("Invalid Employee ID, Employee not found!");
                return result;
            }
            if (output > 0)
            {
                var res = DbInstance.User_Account.Any(x => x.Id == output);
                if (!res)
                {
                    error = "Invalid Employee ID, Employee not found!";
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add(error);
                    return result;
                }
            }


            if (!IsValidToSaveAccountAndEmployee(employeeJson, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            //check validate address
            if (employeeJson.PresentAddressJson == null)
            {
                result.HasError = true;
                result.Errors.Add("Applicant Present Address can't be empty!");
                return result;
            }
            if (employeeJson.PermanentAddressJson == null)
            {
                result.HasError = true;
                result.Errors.Add("Applicant Permanent Address can't be empty!");
                return result;
            }
            if (employeeJson.PresentAddressJson.CountryId == 0)
            {
                employeeJson.PresentAddressJson.CountryId = 1;
            }
            if (!IsValidToSaveContactAddress(employeeJson.PresentAddressJson, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            if (employeeJson.PermanentAddressJson.CountryId == 0)
            {
                employeeJson.PermanentAddressJson.CountryId = 1;
            }
            if (!IsValidToSaveContactAddress(employeeJson.PermanentAddressJson, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            //check validate education

            /*            if (employeeJson.EducationJsonList == null)
                        {
                            result.HasError = true;
                            result.Errors.Add("Applicant Previous Education can't be empty!");
                            return result;
                        }*/

            /*            if (employeeJson.EducationJsonList.Count <= 0)
                        {
                            result.HasError = true;
                            result.Errors.Add("Please Add at Least Two Previous Education!");
                            return result;
                        }*/

            var entityReceivedEducationList = new List<User_Education>();

            foreach (var userEducation in employeeJson.EducationJsonList)
            {
                if (!IsValidToSaveEducation(userEducation, out error))
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return result;
                }

            }


            employeeJson.EducationJsonList.Map(entityReceivedEducationList);

            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    var userAccount = User_Account.GetNew();
                    if (!this.SaveAccountLogic(employeeJson, ref userAccount, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);

                        return result;
                    }
                    DbInstance.SaveChanges();
                    var userEmployee = new User_Employee();
                    if (!this.SaveEmployeeLogic(employeeJson, userAccount, ref userEmployee, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);

                        return result;
                    }
                    DbInstance.SaveChanges();

                    //var listContactAddress = new List<User_ContactAddress>();
                    //applicantJson.ContactAddressJsonList.Map(listContactAddress);
                    //userAccount.User_ContactAddress = listContactAddress;

                    //var listEducation = new List<User_Education>();
                    //applicantJson.EducationJsonList.Map(listEducation);
                    //userAccount.User_Education = listEducation;

                    userAccount.UserTypeEnumId = (byte)User_Account.UserTypeEnum.Employee;

                    if (!this.SaveContactAddressLogic(employeeJson.PermanentAddressJson, userAccount, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);

                        return result;
                    }
                    if (!this.SaveContactAddressLogic(employeeJson.PresentAddressJson, userAccount, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);

                        return result;
                    }
                    DbInstance.SaveChanges();
                    if (employeeJson.EducationJsonList != null && employeeJson.EducationJsonList.Count != 0)
                    {

                        foreach (var userEducation in entityReceivedEducationList)
                        {
                            if (!SaveEducationLogic(userEducation, userAccount, out error))
                            {
                                result.HasError = true;
                                result.Errors.Add(error);
                                return result;
                            }

                        }
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
            result.Data = employeeJson;
            return result;
        }
        private bool SaveAccountLogic(UpdateEmployeeJson jsonObj, ref User_Account dbAddedObj, out string error)
        {
            error = string.Empty;
            if (jsonObj == null)
            {
                error = "Account to save can't be null!";
                return false;
            }

            ////if (!IsValidToSaveAccount(newObj, out error))
            ////    return false;

            bool isNewObject = true;
            User_Account objToSave = null;
            long output;
            var id = long.TryParse(jsonObj.Id, out output) ? output : 0;

            if (id == 0)
            {
                var res = DbInstance.User_Account.Any(x => x.UserName.Equals(jsonObj.UserName.Trim()));
                if (res)
                {
                    error = "Given Employee ID already exists, try new Employee ID!";
                    return false;
                }
            }

            if (id != 0)
            {
                objToSave = DbInstance.User_Account.SingleOrDefault(x => x.Id == id);
                isNewObject = false;
            }

            if (objToSave == null)
            {   //new object
                isNewObject = true;
                var maxId = DbInstance.User_Account.OrderByDescending(item => item.Id).First().Id;

                objToSave = User_Account.GetNew(maxId + 1);
                DbInstance.User_Account.Add(objToSave);
                objToSave.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = DateTime.Now;




                objToSave.UserName = objToSave.Id.ToString();// newObj.UserName?.Trim() ?? newObj.UserName; //todo need logic should be unique.
                objToSave.Password = RandomPassword.GenerateOnlyNumber(4).ToString();
                objToSave.PasswordSalt = "";

                objToSave.Height = 0;
                //objToSave.Weight = 0;
                objToSave.IsLate = false;
                objToSave.BankAccountNo = "";
                objToSave.BankName = "";

                objToSave.IsMigrate = false;
                objToSave.MigrationId = null;
                objToSave.IsProfileCompleted = false;
                objToSave.IsApproved = true;



                objToSave.LastLoginDate = new DateTime(1753, 1, 1, 12, 0, 0, 0);
                objToSave.LastPasswordChangedDate = new DateTime(1753, 1, 1, 12, 0, 0, 0);
                objToSave.LastLockoutDate = new DateTime(1753, 1, 1, 12, 0, 0, 0);
                objToSave.FailedPasswordAttemptCount = 0;
                objToSave.FailedPasswordAttemptWindowStart = new DateTime(1753, 1, 1, 12, 0, 0, 0);
                objToSave.FailedPasswordAnswerAttemptCount = 0;
                objToSave.FailedPasswordAnswerAttemptWindowStart = new DateTime(1753, 1, 1, 12, 0, 0, 0);
            }

            //binding object
            if (objToSave.FullName.Trim() != jsonObj.FullName.Trim() && !isNewObject)
            {
                objToSave.History += $"Employee's <b>Name Changed</b> From {objToSave.FullName} To {jsonObj.FullName}, by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
            }

            objToSave.FullName = jsonObj.FullName?.Trim() ?? jsonObj.FullName;
            objToSave.BanglaName = jsonObj.BanglaName?.Trim() ?? jsonObj.BanglaName;

            objToSave.DateOfBirth = jsonObj.DateOfBirth;
            objToSave.PlaceOfBirthCity = jsonObj.PlaceOfBirthCity?.Trim() ?? jsonObj.PlaceOfBirthCity;
            objToSave.PlaceOfBirthCountryId = jsonObj.PlaceOfBirthCountryId;//long.TryParse(newObj.PlaceOfBirthCountryId, out output) ? output : (Int64?)null;
            objToSave.GenderEnumId = jsonObj.GenderEnumId;
            objToSave.ReligionEnumId = jsonObj.ReligionEnumId;
            objToSave.BloodGroupEnumId = jsonObj.BloodGroupEnumId;
            objToSave.MaritalStatusEnumId = jsonObj.MaritalStatusEnumId;
            objToSave.FatherName = jsonObj.FatherName?.Trim() ?? jsonObj.FatherName;
            objToSave.MotherName = jsonObj.MotherName?.Trim() ?? jsonObj.MotherName;
            objToSave.SpouseName = jsonObj.SpouseName?.Trim() ?? jsonObj.SpouseName;
            objToSave.UserMobile = jsonObj.UserMobile?.Trim() ?? jsonObj.UserMobile;
            objToSave.UserMobile2 = jsonObj.UserMobile2?.Trim() ?? jsonObj.UserMobile2;
            objToSave.UserMobilePrivacyEnumId = jsonObj.UserMobilePrivacyEnumId;//(int)User_Account.UserContactPrivacyEnum.AdminstrationOnly;
            objToSave.IsVerifiedUserMobile = jsonObj.IsVerifiedUserMobile;
            objToSave.UserEmail = jsonObj.UserEmail?.Trim() ?? jsonObj.UserEmail;
            objToSave.UserEmailPrivacyEnumId = jsonObj.UserEmailPrivacyEnumId;//(int)User_Account.UserContactPrivacyEnum.AllEmployees;
            objToSave.IsVerifiedUserEmaill = jsonObj.IsVerifiedUserEmaill;
            objToSave.EmergencyContactPersonName = jsonObj.EmergencyContactPersonName?.Trim() ?? jsonObj.EmergencyContactPersonName;
            objToSave.EmergencyMobile = jsonObj.EmergencyMobile?.Trim() ?? jsonObj.EmergencyMobile;
            objToSave.EmergencyContactAdress = jsonObj.EmergencyContactAdress?.Trim() ?? jsonObj.EmergencyContactAdress;
            objToSave.EmergencyContactRelationshipId = jsonObj.EmergencyContactRelationshipId;
            objToSave.Height = jsonObj.Height;

            objToSave.IsLate = jsonObj.IsLate;
            objToSave.CampusId = jsonObj.CampusId;// long.TryParse(newObj.CampusId, out output) ? output : 1;
            objToSave.Nationality = jsonObj.Nationality?.Trim() ?? jsonObj.Nationality;
            objToSave.NationalIdNumber = jsonObj.NationalIdNumber?.Trim() ?? jsonObj.NationalIdNumber;
            objToSave.BirthCertificateNumber = jsonObj.BirthCertificateNumber?.Trim() ?? jsonObj.BirthCertificateNumber;

            if (objToSave.BankName?.Trim() != jsonObj.BankName?.Trim() && !isNewObject)
            {
                objToSave.History += $"Employee's <b>Bank Name Changed</b> From {objToSave.BankName} To {jsonObj.BankName}, by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
            }
            objToSave.BankName = jsonObj.BankName;
            if (objToSave.BankAccountNo?.Trim() != jsonObj.BankAccountNo?.Trim() && !isNewObject)
            {
                objToSave.History += $"Employee's <b>Bank Account No Changed</b> From {objToSave.BankAccountNo} To {jsonObj.BankAccountNo}, by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
            }
            objToSave.BankAccountNo = jsonObj.BankAccountNo;
            objToSave.PassportNo = jsonObj.PassportNo;

            //todo rechek the enum values
            objToSave.UserTypeEnumId = (int)User_Account.UserTypeEnum.Employee;
            objToSave.UserStatusEnumId = jsonObj.UserStatusEnumId;
            objToSave.IsMigrate = objToSave.IsMigrate;
            objToSave.MigrationId = objToSave.MigrationId;
            objToSave.IsProfileCompleted = jsonObj.IsProfileCompleted;
            objToSave.IsApproved = jsonObj.IsApproved;


            objToSave.JoiningDate = jsonObj.JoiningDate;
            objToSave.DepartmentId = jsonObj.DepartmentId;
            objToSave.LeavingDate = jsonObj.LeavingDate;

            //new col
            objToSave.IsUserCanEditProfile = jsonObj.IsUserCanEditProfile;
            objToSave.IsDocumentPending = jsonObj.IsDocumentPending;
            //objToSave.IsLockedOut = false;

            objToSave.FailedPasswordAttemptCount = jsonObj.FailedPasswordAttemptCount;
            objToSave.FailedPasswordAttemptWindowStart = jsonObj.FailedPasswordAttemptWindowStart;
            objToSave.FailedPasswordAnswerAttemptCount = jsonObj.FailedPasswordAnswerAttemptCount;
            objToSave.FailedPasswordAnswerAttemptWindowStart = jsonObj.FailedPasswordAnswerAttemptWindowStart;
            objToSave.Remarks = jsonObj.Remarks?.Trim() ?? jsonObj.Remarks;
            objToSave.LockReason = jsonObj.LockReason;

            objToSave.JoiningSemesterId = null;
            if (long.TryParse(jsonObj.JoiningSemesterId, out output))
            {
                objToSave.JoiningSemesterId = output;
            }

            //Ems -4 update
            //objToSave.History = jsonObj.History;
            objToSave.ActiveRfidNo = jsonObj.ActiveRfidNo;
            objToSave.ProfilePictureImageUrl = jsonObj.ProfilePictureImageUrl;
            objToSave.SignatureImageUrl = jsonObj.SignatureImageUrl;
            objToSave.IsForceTwoFactorAuth = jsonObj.IsForceTwoFactorAuth;

            //objToSave.RankId = null;
            objToSave.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = DateTime.Now;

            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }

        //SaveEmployeeLogic
        private bool SaveEmployeeLogic(UpdateEmployeeJson jsonObj, User_Account objAccount, ref User_Employee dbAddedObj, out string error)
        {
            error = string.Empty;
            if (jsonObj == null)
            {
                error = "Employee to save can't be null!";
                return false;
            }

            //if (!IsValidToSaveEmployee(newObj, out error))
            //    return false;

            bool isNewObject = true;
            User_Employee objToSave = null;

            if (objAccount.Id != 0)
            {
                objToSave = DbInstance.User_Employee.SingleOrDefault(x => x.UserId == objAccount.Id);
                isNewObject = false;
            }
            //else
            //{
            //    jsonObj.Id = BigInt.NewBigInt();
            //}
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = User_Employee.GetNew(objAccount.Id);
                DbInstance.User_Employee.Add(objToSave);
                objToSave.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Employee.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Employee.CanEdit,
                   out error))
            {
                return false;
            }*/
            long output;
            //binding object  
            objToSave.UserId = jsonObj.UserId;
            objToSave.EmployeeCategoryEnumId = jsonObj.EmployeeCategoryEnumId;
            objToSave.EmployeeTypeEnumId = jsonObj.EmployeeTypeEnumId;
            objToSave.JobClassEnumId = jsonObj.JobClassEnumId;
            objToSave.JobTypeEnumId = jsonObj.JobTypeEnumId;
            objToSave.WorkingStatusEnumId = jsonObj.WorkingStatusEnumId;
            objToSave.TinNumber = jsonObj.TinNumber;
            objToSave.ImmediateSuperVisorId = long.TryParse(jsonObj.ImmediateSuperVisorId, out output) ? output : (Int64?)null;
            objToSave.IsAcademician = jsonObj.IsAcademician;
            objToSave.HasAdminAccess = jsonObj.HasAdminAccess;


            objToSave.IncrementMonthEnumId = jsonObj.IncrementMonthEnumId;
            objToSave.JoiningDepartmentId = jsonObj.JoiningDepartmentId;

            objToSave.PositionId = jsonObj.PositionId;// long.TryParse(, out output) ? output : (Int64?)null;

            objToSave.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = jsonObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }



        private bool IsValidToSaveAccountAndEmployee(UpdateEmployeeJson newObj, out string error)
        {
            error = string.Empty;
            //TODO Have to remove this check. (Temporary)
            //if (HttpUtil.Profile.UserName.Equals("psdeveloper") ||
            //    HttpUtil.DbContext.UAC_RoleUserMap.Any(x => x.UserId == HttpUtil.Profile.UserId))
            //{
            //    return true;
            //}
            if (newObj == null)
            {
                error = "Invalid Operation! Reload the page and try again later";
                return false;
            }


            long output;
            //=================Account
            //if (!newObj.IsAutoUserName && !newObj.UserName.IsValid())
            //{
            //    error += "User Name can't blank.";
            //    return false;
            //}
            //if (!newObj.UserName.IsValid())
            //{
            //    error += "User Name is not valid.";
            //    return false;
            //}
            if (newObj.UserName.Trim().Length > 50)
            {
                error += "User Name exceeded its maximum length 50.";
                return false;
            }
            if (!newObj.FullName.IsValid())
            {
                error += "Full Name is not valid.";
                return false;
            }
            newObj.FullName = newObj.FullName.ValidateName();
            if (newObj.FullName.Length > 128)
            {
                error += "Full Name exceeded its maximum length 128.";
                return false;
            }
            if (newObj.BanglaName.IsValid() && newObj.BanglaName.Length > 200)
            {
                error += "Bangla Name exceeded its maximum length 200.";
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
            if (newObj.PlaceOfBirthCity.IsValid() && newObj.PlaceOfBirthCity.Length > 128)
            {
                error += "Place Of Birth City exceeded its maximum length 128.";
                return false;
            }

            //if (!newObj.FatherName.IsValid())
            //{
            //    //newObj.FatherName = "";
            //    error += "Father Name is not valid.";
            //    return false;
            //}
            //newObj.FatherName = newObj.FatherName.ToTitleCase();
            //if (newObj.FatherName.Length > 128)
            //{
            //    error += "Father Name exceeded its maximum length 128.";
            //    return false;
            //}
            //if (!newObj.MotherName.IsValid())
            //{
            //    //newObj.MotherName = "";
            //    error += "Mother Name is not valid.";
            //    return false;
            //}
            //newObj.MotherName = newObj.MotherName.ToTitleCase();
            //if (newObj.MotherName.Length > 128)
            //{
            //    error += "Mother Name exceeded its maximum length 128.";
            //    return false;
            //}
            //if (newObj.SpouseName.IsValid() && newObj.SpouseName.Length > 128)
            //{
            //    error += "Spouse Name exceeded its maximum length 128.";
            //    return false;
            //}
            //custom validation todo should be mandatory 
            //if (!newObj.UserMobile.IsValid())
            //{
            //    error += "User Mobile cant be empty.";
            //    return false;
            //}
            if (newObj.UserMobile.IsValid())
                newObj.UserMobile = newObj.UserMobile.ValidateMobile();
            if (newObj.UserMobile.IsValid() && !newObj.UserMobile.IsValidMobileForBD())
            {
                error += "Invalid User Mobile. It should be only 11 digits number.";
                return false;
            }

            if (newObj.UserMobile2.IsValid())
                newObj.UserMobile2 = newObj.UserMobile2.ValidateMobile();
            if (newObj.UserMobile2.IsValid() && newObj.UserMobile2.Length > 15)
            {
                error += "User Mobile2 exceeded its maximum length 15.";
                return false;
            }
            if (newObj.UserMobilePrivacyEnumId == null)
            {
                error += "Please select valid User Mobile Privacy.";
                return false;
            }
            if (newObj.IsVerifiedUserMobile == null)
            {
                error += "Is Varified User Mobile required.";
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
            if (newObj.UserEmailPrivacyEnumId == null)
            {
                error += "Please select valid User Email Privacy.";
                return false;
            }
            if (newObj.IsVerifiedUserEmaill == null)
            {
                error += "Is Varified User Emaill required.";
                return false;
            }
            if (newObj.EmergencyContactPersonName.IsValid() && newObj.EmergencyContactPersonName.Length > 250)
            {
                error += "Emergency Contact Person Name exceeded its maximum length 250.";
                return false;
            }
            //custom todo shuld be mandatory 
            //if (!newObj.EmergencyMobile.IsValid())
            //{
            //    error += "User Emergency Mobile can't be empty.";
            //    return false;
            //}
            if (newObj.EmergencyMobile.IsValid())
                newObj.EmergencyMobile = newObj.EmergencyMobile.ValidateMobile();
            if (newObj.EmergencyMobile.IsValid() && !newObj.EmergencyMobile.IsValidMobileForBD())
            {
                error = "User Emergency Should be only 11 digits number!";
                return false;
            }
            //todo shuld be mandatory 
            //if (newObj.EmergencyMobile.IsValid() && newObj.UserMobile.Equals(newObj.EmergencyMobile))
            //{
            //    error = "Emergency Mobile Number should not be same with User's Mobile Number!";
            //    return false;
            //}



            //if (newObj.EmergencyMobile.IsValid() && newObj.EmergencyMobile.Length > 50)
            //{
            //    error += "Emergency Mobile exceeded its maximum length 50.";
            //    return false;
            //}
            if (newObj.EmergencyContactAdress.IsValid() && newObj.EmergencyContactAdress.Length > 500)
            {
                error += "Emergency Contact Adress exceeded its maximum length 500.";
                return false;
            }


            if (newObj.IsLate == null)
            {
                error += "Is Late required.";
                return false;
            }
            if (newObj.CampusId <= 0)
            {
                newObj.CampusId = 1;
                //error += "Please select valid Campus.";
                //return false;
            }
            if (!newObj.Nationality.IsValid())
            {
                newObj.Nationality = "";
                //error += "Nationality is not valid.";
                //return false;
            }
            if (newObj.Nationality.Length > 128)
            {
                error += "Nationality exceeded its maximum length 128.";
                return false;
            }
            if (newObj.NationalIdNumber.IsValid() && newObj.NationalIdNumber.Length > 50)
            {
                error += "National Id Number exceeded its maximum length 50.";
                return false;
            }
            if (newObj.BirthCertificateNumber.IsValid() && newObj.BirthCertificateNumber.Length > 50)
            {
                error += "Birth Certificate Number exceeded its maximum length 50.";
                return false;
            }
            if (newObj.BankAccountNo.IsValid() && newObj.BankAccountNo.Length > 50)
            {
                error += "Bank Account No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.BankName.IsValid() && newObj.BankName.Length > 250)
            {
                error += "Bank Name exceeded its maximum length 250.";
                return false;
            }
            if (newObj.PassportNo.IsValid() && newObj.PassportNo.Length > 128)
            {
                error += "Passport No exceeded its maximum length 128.";
                return false;
            }
            if (newObj.UserTypeEnumId == null)
            {
                error += "Please select valid User Type.";
                return false;
            }
            if (newObj.UserStatusEnumId == null)
            {
                error += "Please select valid User Status.";
                return false;
            }
            if (newObj.IsMigrate == null)
            {
                error += "Is Migrate required.";
                return false;
            }

            if (newObj.IsProfileCompleted == null)
            {
                error += "Is Profile Completed required.";
                return false;
            }
            if (newObj.IsApproved == null)
            {
                error += "Is Approved required.";
                return false;
            }
            //if (!newObj.Password.IsValid())
            //{
            //    error += "Password is not valid.";
            //    return false;
            //}
            //if (newObj.Password.Length > 128)
            //{
            //    error += "Password exceeded its maximum length 128.";
            //    return false;
            //}
            //if (!newObj.PasswordSalt.IsValid())
            //{
            //    error += "Password Salt is not valid.";
            //    return false;
            //}
            //if (newObj.PasswordSalt.Length > 128)
            //{
            //    error += "Password Salt exceeded its maximum length 128.";
            //    return false;
            //}
            //if (!newObj.LastLoginDate.IsValid())
            //{
            //    error += "Last Login Date is not valid.";
            //    return false;
            //}
            //if (!newObj.LastPasswordChangedDate.IsValid())
            //{
            //    error += "Last Password Changed Date is not valid.";
            //    return false;
            //}
            //if (!newObj.LastLockoutDate.IsValid())
            //{
            //    error += "Last Lockout Date is not valid.";
            //    return false;
            //}
            //if (newObj.FailedPasswordAttemptCount == null)
            //{
            //    error += "Failed Password Attempt Count required.";
            //    return false;
            //}
            //if (!newObj.FailedPasswordAttemptWindowStart.IsValid())
            //{
            //    error += "Failed Password Attempt Window Start is not valid.";
            //    return false;
            //}
            //if (newObj.FailedPasswordAnswerAttemptCount == null)
            //{
            //    error += "Failed Password Answer Attempt Count required.";
            //    return false;
            //}
            //if (!newObj.FailedPasswordAnswerAttemptWindowStart.IsValid())
            //{
            //    error += "Failed Password Answer Attempt Window Start is not valid.";
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

            //===========================================User Employee 
            if (newObj.UserId <= 0)
            {
                error += "Please select valid User.";
                return false;
            }
            if (newObj.EmployeeCategoryEnumId == null)
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
            if (newObj.TinNumber.IsValid() && newObj.TinNumber.Length > 50)
            {
                error += "Tin Number exceeded its maximum length 50.";
                return false;
            }

            //if (newObj.IsAcademician == null)
            //{
            //    error += "Is Academician required.";
            //    return false;
            //}
            //if (newObj.IsWorking == null)
            //{
            //    error += "Is Working required.";
            //    return false;
            //}
            //if (newObj.OldIdNo.IsValid() && newObj.OldIdNo.Length > 128)
            //{
            //    error += "Old Id No exceeded its maximum length 128.";
            //    return false;
            //}
            if (!newObj.JoiningDate.IsValid())
            {
                error += "Joining Date is not valid.";
                return false;
            }
            if (newObj.IncrementMonthEnumId == null)
            {
                error += "Please select valid Increment Month.";
                return false;
            }
            if (newObj.JoiningDepartmentId <= 0)
            {
                error += "Please select valid Joining Department.";
                return false;
            }
            if (newObj.DepartmentId <= 0)
            {
                error += "Please select valid Department.";
                return false;
            }

            if (newObj.PositionId <= 0)
            {
                error += "Please select valid Position.";
                return false;
            }

            if (newObj.LeavingDate != null && !newObj.LeavingDate.IsValid())
            {
                newObj.LeavingDate = null;//just put null,if its nullable and not valid.
                                          //error += "Leaving Date is not valid.";
                                          //return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.User_Employee.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A Employee already exists!";
            //return false;
            //}



            error = string.Empty;
            return true;
        }

        #endregion

        #region Save Address
        private bool SaveContactAddressLogic(User_ContactAddressJson newObj, User_Account objAccount, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Contact Address to save can't be null!";
                return false;
            }

            //if (!IsValidToSaveContactAddress(newObj, out error))
            //    return false;

            bool isNewObject = true;
            User_ContactAddress objToSave = null;

            //long output;
            //var id = long.TryParse(newObj.Id, out output) ? output : 0;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.User_ContactAddress.SingleOrDefault(x => x.Id == newObj.Id && x.UserId == objAccount.Id);
                isNewObject = false;
            }

            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = User_ContactAddress.GetNew();
                DbInstance.User_ContactAddress.Add(objToSave);
                objToSave.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = DateTime.Now;
            }


            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ContactAddressManager.ContactAddress.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ContactAddressManager.ContactAddress.CanEdit,
                   out error))
            {
                return false;
            }*/
            //binding object  
            objToSave.UserId = objAccount.Id;
            objToSave.Address1 = newObj.Address1;
            objToSave.Address2 = newObj.Address2;
            objToSave.PostOffice = newObj.PostOffice;
            objToSave.PoliceStation = newObj.PoliceStation;
            objToSave.District = newObj.District;
            objToSave.CountryId = newObj.CountryId;//long.TryParse(newObj.CountryId, out output) ? output : 1;
            objToSave.IsValid = newObj.IsValid;
            objToSave.ContactAddressTypeEnumId = newObj.ContactAddressTypeEnumId;
            objToSave.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;

            //here save any child table
            return true;
        }

        private bool IsValidToSaveContactAddress(User_ContactAddressJson newObj, out string error)
        {
            error = "";
            //if (newObj.UserId <= 0)
            //{
            //    error += "Please select valid User.";
            //    return false;
            //}
            if (newObj.Address1.IsValid() && newObj.Address1.Length > 500)
            {
                error += "Address 1 exceeded its maximum length 500.";
                return false;
            }
            if (newObj.Address2.IsValid() && newObj.Address2.Length > 500)
            {
                error += "Address 2 exceeded its maximum length 500.";
                return false;
            }
            if (newObj.PostOffice.IsValid() && newObj.PostOffice.Length > 250)
            {
                error += "Post Office exceeded its maximum length 250.";
                return false;
            }
            if (newObj.PoliceStation.IsValid() && newObj.PoliceStation.Length > 250)
            {
                error += "Police Station exceeded its maximum length 250.";
                return false;
            }
            if (newObj.District.IsValid() && newObj.District.Length > 250)
            {
                error += "District exceeded its maximum length 250.";
                return false;
            }

            if (newObj.IsValid == null)
            {
                error += "Is Valid required.";
                return false;
            }
            if (newObj.ContactAddressTypeEnumId == null)
            {
                error += "Please select valid Contact Address Type.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.User_ContactAddress.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A ContactAddress already exists!";
            //return false;
            //}
            return true;
        }
        #endregion

        #region  Education
        public HttpListResult<User_EducationJson> GetAllEducationByUserId(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_EducationJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Education.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Education.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Education.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    //todo check permission
                    if (id <= 0)
                    {
                        result.HasError = true;
                        result.Errors.Add("Invalid Employee ID, please refresh the page the try again!");
                        return result;
                    }
                    var educationList = DbInstance.User_Education
                        .OrderBy(x => x.DegreeEquivalentEnumId)
                        .Where(x => x.UserId == id).ToList();

                    var json = new List<User_EducationJson>();

                    educationList.Map(json);
                    result.Data = json;
                }
                catch (Exception e)
                {
                    result.HasError = true;
                    result.Errors.Add(e.Message.ToString());
                    return result;
                }
            }
            return result;
        }

        private bool SaveEducationLogic(User_Education newObj, User_Account objAccount, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Education to save can't be null!";
                return false;
            }

            //if (!IsValidToSaveEducation(newObj, out error))
            //    return false;

            bool isNewObject = true;
            User_Education objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.User_Education.SingleOrDefault(x => x.Id == newObj.Id && x.UserId == objAccount.Id);
                isNewObject = false;
            }
            //else
            //{
            //    newObj.Id = BigInt.NewBigInt();
            //}
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = User_Education.GetNew();
                DbInstance.User_Education.Add(objToSave);
                objToSave.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }


            //binding object  
            objToSave.UserId = objAccount.Id;
            objToSave.DegreeEquivalentEnumId = newObj.DegreeEquivalentEnumId;
            objToSave.DegreeCategoryId = newObj.DegreeCategoryId;
            objToSave.DegreeTitle = newObj.DegreeTitle;
            objToSave.DegreeGroupMajorEnumId = newObj.DegreeGroupMajorEnumId;
            objToSave.DegreeGroupMajorOther = newObj.DegreeGroupMajorOther;
            objToSave.InstitutionName = newObj.InstitutionName;
            objToSave.BoardId = newObj.BoardId;
            objToSave.CourseDuration = newObj.CourseDuration;
            objToSave.YearOfPassing = newObj.YearOfPassing;
            objToSave.RegistrationNo = newObj.RegistrationNo;
            objToSave.StudentRollOrIdNo = newObj.StudentRollOrIdNo;

            objToSave.ResultSystemEnumId = newObj.ResultSystemEnumId;
            objToSave.ObtainedGradeOrClass = newObj.ObtainedGradeOrClass;
            objToSave.ObtainedGpaOrMarks = newObj.ObtainedGpaOrMarks;

            objToSave.GpaNo4Sub = newObj.GpaNo4Sub;
            objToSave.FullMarksOrScale = newObj.FullMarksOrScale;
            objToSave.IsGolden = newObj.IsGolden;
            objToSave.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;


            //here save any child table
            return true;
        }

        private bool IsValidToSaveEducation(User_EducationJson newObj, out string error)
        {
            error = "";
            //if (newObj.UserId <= 0)
            //{
            //    error += "Please select valid User.";
            //    return false;
            //}

            if (newObj.DegreeEquivalentEnumId == null)
            {
                error += "Please select valid Degree Equivalent.";
                return false;
            }
            if (newObj.DegreeCategoryId <= 0)
            {
                error += "Please select valid Degree Category.";
                return false;
            }
            if (!newObj.DegreeTitle.IsValid())
            {
                newObj.DegreeTitle = "";
                //error += "Degree Title is not valid.";
                //return false;
            }
            if (newObj.DegreeTitle.Length > 250)
            {
                error += "Degree Title exceeded its maximum length 250.";
                return false;
            }
            if (newObj.DegreeGroupMajorEnumId == null)
            {
                error += "Please select valid Degree Group Major.";
                return false;
            }
            if (newObj.DegreeGroupMajorOther.IsValid() && newObj.DegreeGroupMajorOther.Length > 250)
            {
                error += "Degree Group Major Other exceeded its maximum length 250.";
                return false;
            }
            if (newObj.InstitutionName.IsValid() && newObj.InstitutionName.Length > 250)
            {
                error += "Institution Name exceeded its maximum length 250.";
                return false;
            }

            if (newObj.CourseDuration.IsValid() && newObj.CourseDuration.Length > 50)
            {
                error += "Course Duration exceeded its maximum length 50.";
                return false;
            }

            if (newObj.RegistrationNo.IsValid() && newObj.RegistrationNo.Length > 50)
            {
                error += "Registration No exceeded its maximum length 50.";
                return false;
            }

            if (newObj.ResultSystemEnumId == null)
            {
                error += "Please select valid Result System.";
                return false;
            }
            if (newObj.ObtainedGradeOrClass.IsValid() && newObj.ObtainedGradeOrClass.Length > 50)
            {
                error += "Grade Or Class exceeded its maximum length 50.";
                return false;
            }


            return true;
        }

        public HttpResult<bool> GetDeleteEducationById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<bool>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Education.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            result.Data = false;
            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    //todo check permission
                    if (id <= 0)
                    {
                        //result.HasError = true;
                        //result.Errors.Add("Save the Education first, then try again!");
                        result.Data = true;
                        return result;
                    }
                    var education = DbInstance.User_Education.FirstOrDefault(x => x.Id == id);
                    if (education == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("Invalid Education, please refresh and try again!");
                        return result;
                    }
                    //if (education.IsLocked)
                    //{
                    //    education.LockedBy = HttpUtil.Profile.UserId;
                    //    education.History += $"Unlocked by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}. ";
                    //}

                    DbInstance.User_Education.Remove(education);

                    DbInstance.SaveChanges();
                    scope.Commit();

                    result.Data = true;
                }
                catch (Exception e)
                {
                    result.HasError = true;
                    result.Errors.Add(e.Message.ToString());
                    return result;
                }
            }
            return result;
        }

        #endregion

        #region Profile Picture

        [System.Web.Http.HttpPost]
        public HttpResult<string> UploadProfilePicture(int userId)
        {
            string error = string.Empty;
            var result = new HttpResult<string>();
            
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanUploadProfilePicture, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            var Request = HttpContext.Current.Request;
            List<string> uploadFilePath = new List<string>();
            //check file exist, file size, file extensions
            try
            {
                User_Account user = DbInstance.User_Account.FirstOrDefault(x => x.Id == userId);
                if (!IsValidToUploadProfilePicture(user, Request.Files, out error))
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return result;
                }

                var extension = Path.GetExtension(Request.Files[0].FileName);
                var newFileId = BigInt.NewBigInt().ToString();
                var newFileName = newFileId + extension;
                var folder = user.DepartmentId.ToString();
                //Request.Files[file]

                string relativeFolderUrl = DiskStorageSettings.ItemFolderNames.EmployeeProfilePictureFolderName;

                relativeFolderUrl = Path.Combine(relativeFolderUrl, folder);

                var relativeFolderDiskPath = relativeFolderUrl.Replace("/", "\\");
                var fullDiskPathFolder = Path.Combine(DiskStorageSettings.RootStoragePathForProfilePictures, relativeFolderDiskPath);

                if (!Directory.Exists(fullDiskPathFolder))
                    Directory.CreateDirectory(fullDiskPathFolder);

                string fullFilePath = Path.Combine(fullDiskPathFolder, newFileName); //for save into file.//disk
                var relativeFileUrl = Path.Combine(relativeFolderUrl, newFileName); //for save in DB//web

                if (File.Exists(fullFilePath))
                    File.Delete(fullFilePath);

                Request.Files[0].SaveAs(fullFilePath);
                uploadFilePath.Add(fullFilePath);

                user.ProfilePictureImageUrl = relativeFileUrl;

                user.History += $"Employee's Profile picture uploaded by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
                user.LastChanged = DateTime.Now;
                DbInstance.SaveChanges();

                result.Data = relativeFileUrl;

                return result;
            }
            catch (Exception ex)
            {
                //deleting all files
                foreach (string fullFilePath in uploadFilePath)
                {
                    if (File.Exists(fullFilePath))
                        File.Delete(fullFilePath);

                }
                result.HasError = true;
                result.Errors.Add("Sorry some problem occurred while uploading Data, please try again later." /*+ ex.GetBaseException()*/);
                return result;
            }
        }
        private bool IsValidToUploadProfilePicture(User_Account user, HttpFileCollection files, out string error)
        {
            error = "";
            if (user == null)
            {
                error += "Student Profile Cannot be found.";
                return false;
            }

            if (files.Count <= 0)
            {
                error += "Please upload at least One jpg image for profile pic";
                return false;
            }
            if (files.Count > 1)
            {
                error += "Please Upload one photo as profile Picture.";
                return false;
            }

            try
            {

                if (!(Path.GetExtension(files[0].FileName).ToLower() == ".jpg" ||
                      Path.GetExtension(files[0].FileName).ToLower() == ".jpeg"))
                {
                    error += "Please upload jpg file only.";
                    return false;
                }

            }
            catch (Exception e)
            {
                error = e.Message;
                return false;
            }

            return true;

        }

        public HttpResult<bool> GetRemoveProfilePictureById(int userId)
        {
            string error = string.Empty;
            var result = new HttpResult<bool>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanRemoveProfilePicture, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            if (userId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Employee Id.");
                return result;
            }
            var user = DbInstance.User_Account.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Employee!");
                return result;
            }

            try
            {
                if (!user.ProfilePictureImageUrl.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("No Profile picture found.!");
                    return result;
                }

                var fullFilePath = Path.Combine(DiskStorageSettings.RootStoragePathForProfilePictures, user.ProfilePictureImageUrl);
                if (File.Exists(fullFilePath))
                    File.Delete(fullFilePath);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    user.ProfilePictureImageUrl = null;
                    user.History += $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} Profile Picture Removed, By {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}.<br>";

                    DbInstance.SaveChanges();
                    scope.Commit();
                }
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Errors.Add(e.GetBaseException().Message);
            }

            result.Data = true;
            return result;
        }

        #endregion


        [System.Web.Mvc.HttpPost]
        public HttpResult<HrEmployeeJson> SaveApproval([FromUri]long userId, bool isApproved, bool isSendEmailOnApproval, bool isForceToGenerateUsername)
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


                    using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                    {

                        if (isApproved)
                        {

                            if (userAccount.IsApproved)
                            {
                                result.HasError = true;
                                result.Errors.Add("This Employee Already Approved.");
                                return result;
                            }
                            userAccount.IsApproved = true;
                            userAccount.UserStatusEnumId = (byte)User_Account.UserStatusEnum.Active;
                            userAccount.IsProfileCompleted = true;
                        }
                        else
                        {
                            if (!userAccount.IsApproved)
                            {
                                result.HasError = true;
                                result.Errors.Add("This Employee Already Disapproved.");
                                return result;
                            }

                            userAccount.IsApproved = false;
                            userAccount.UserStatusEnumId = (byte)User_Account.UserStatusEnum.Inactive;
                            userAccount.IsProfileCompleted = false;
                        }


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
                            password = userAccount.Password;
                            //password = "[The password was given by you on registration. (This is not your password)]";
                        }

                        //TODO Check Email was sent or not
                        var userContactEmail = userAccount.UserEmail;
                        //DbInstance.User_ContactEmail
                        //    .SingleOrDefault(
                        //    x => x.UserId == userId
                        //    && x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail
                        //    );
                        if (!userContactEmail.IsValidEmail())
                        {
                            result.DataExtra.Message = "Can't find any valid Personal Email Address to send email. ";
                        }
                 
                        else
                        {
                            var isSenEmail = EmailTemplate.SendNewAccountEmail(
                                    userAccount.FullName,
                                    $"Congratulation! Your {SiteSettings.Instance.ProductBrandNameSolo} Online Account Approved by Admin",
                                    userContactEmail,
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


        //[System.Web.Mvc.HttpPost]
        //private HttpResult<HrEmployeeJson> SaveApprovalOld([FromUri]long userId, bool isApproved, bool isSendEmailOnApproval, bool isForceToGenerateUsername)
        //{
        //    string error = string.Empty;
        //    var result = new HttpResult<HrEmployeeJson>();
        //    if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanApproval, out error))
        //    {
        //        result.HasError = true;
        //        result.Errors.Add(error);
        //        return result;
        //    }
        //    //var json = new HrEmployeeJson();
        //    //json.Account = new User_AccountJson();
        //    try
        //    {
        //        var userAccount = DbInstance.User_Account.Find(userId);
        //        if (userAccount != null)
        //        {


        //            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.RepeatableRead))
        //            {
        //                if ((userAccount.UserName.Equals(userAccount.Id.ToString()) || isForceToGenerateUsername) && isApproved)
        //                {
        //                    // TODO test the method is working or not?
        //                    //var username = "";
        //                    //if (Facade.HrAreaFacade.EmployeeFacade.GenerateEmpUserName(userAccount, out username) && username != string.Empty)
        //                    //{
        //                    //    //log username change audit
        //                    //    Facade.HrAreaFacade.EmployeeFacade.SaveUserNameChangeAudit(userAccount.UserName,
        //                    //        username, userAccount.Id, scope, out error);
        //                    //    userAccount.UserName = username;//new user name

        //                    //}
        //                    //else
        //                    //{
        //                    //    result.HasError = true;
        //                    //    result.Errors.Add("User name creation failed. " + username);

        //                    //}
        //                }

        //                if (isApproved)
        //                {

        //                    if (userAccount.IsApproved)
        //                    {
        //                        result.HasError = true;
        //                        result.Errors.Add("This Employee Already Approved.");
        //                        return result;
        //                    }
        //                    userAccount.IsApproved = true;
        //                    userAccount.UserStatusEnumId = (byte)User_Account.UserStatusEnum.Active;
        //                    userAccount.IsProfileCompleted = true;
        //                }
        //                else
        //                {
        //                    if (!userAccount.IsApproved)
        //                    {
        //                        result.HasError = true;
        //                        result.Errors.Add("This Employee Already Disapproved.");
        //                        return result;
        //                    }

        //                    userAccount.IsApproved = false;
        //                    userAccount.UserStatusEnumId = (byte)User_Account.UserStatusEnum.Inactive;
        //                    userAccount.IsProfileCompleted = false;
        //                }


        //                result.DataExtra.UserName = userAccount.UserName;//output current user name
        //                if (Facade.HrAreaFacade.EmployeeFacade.SaveAccountOnly(userAccount, scope, out error))
        //                {
        //                    DbInstance.SaveChanges();
        //                    scope.Commit();
        //                }
        //                else
        //                {
        //                    scope.Rollback();
        //                    result.HasError = true;
        //                    result.Errors.Add(error);
        //                }
        //            }
        //            if (isApproved && isSendEmailOnApproval)
        //            {
        //                var username = userAccount.UserName;
        //                var password = "[You are already changed your first password. (This is not your password!)]";
        //                if (userAccount.PasswordSalt == string.Empty)
        //                {
        //                    //password = userAccount.Password;
        //                    password = "[The password was given by you on registration. (This is not your password)]";
        //                }

        //                //TODO Check Email was sent or not
        //                var userContactEmail = userAccount.UserEmail;
        //                //DbInstance.User_ContactEmail
        //                //    .SingleOrDefault(
        //                //    x => x.UserId == userId
        //                //    && x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail
        //                //    );
        //                if (!userContactEmail.IsValidEmail())
        //                {
        //                    result.DataExtra.Message = "Can't find any valid Personal Email Address to send email. ";
        //                }
                 
        //                else
        //                {
        //                    var isSenEmail =EmailTemplate. SendNewAccoutEmail(
        //                            userAccount.FullName,
        //                            "Congratulation! EMS Employee Registration has been approved",
        //                            userContactEmail,
        //                            username,
        //                            password,
        //                            out error);
        //                    if (!isSenEmail)
        //                    {
        //                        result.DataExtra.Message = "Email Can't send now for " + error;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            error = "User can't exist! Please register user first and try again later. ";
        //            result.HasError = true;
        //            result.Errors.Add(error);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        error = ex.ToString();
        //        result.HasError = true;
        //        result.Errors.Add(error);
        //    }
        //    return result;
        //}

     

        public HttpResult<HrEmployeeJson> SaveResetAndSendPassword([FromUri] int userId)
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
                string confirmPassword;
                var password = confirmPassword = RandomPassword.Generate(6, 8);

                if (!Facade.UserCredentialFacade.ResetPassword(userId, password, confirmPassword, out error, out msg))
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                }
                else
                {
                    result.DataExtra.NewPassword = password;
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


        #region User Log

        public HttpListResult<UAC_LoginAuditJson> GetPagedLoginAuditById(long userId, int? pageSize, int? pageNo)
        {
            string error = string.Empty;
            var result = new HttpListResult<UAC_LoginAuditJson>();
            int count = 0;
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanViewLog, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (UAC_LoginAuditDataService loginAuditDataService = new UAC_LoginAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<UAC_LoginAudit> query = DbInstance.UAC_LoginAudit
                        .Where(x => x.UserId == userId)
                        .OrderByDescending(x => x.Id);

                    //disallow super admin
                    if (!HttpUtil.IsSupperAdmin())
                    {
                        query = query.Where(x => x.UserId != HttpUtil.SuperAdminId || !x.UserName.Equals(HttpUtil.SuperAdminUserName, StringComparison.InvariantCultureIgnoreCase));
                    }

                    var policyEntities = loginAuditDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var policyJsons = new List<UAC_LoginAuditJson>();
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

        public HttpListResult<UAC_VerificationAuditJson> GetPagedChangeHistoryById(long userId, int? pageSize, int? pageNo)
        {
            string error = string.Empty;
            var result = new HttpListResult<UAC_VerificationAuditJson>();
            int count = 0;
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanViewLog, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (UAC_VerificationAuditDataService verficationAuditDataService = new UAC_VerificationAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<UAC_VerificationAudit> query = DbInstance.UAC_VerificationAudit
                        .Where(x => x.UserId == userId)
                        .OrderByDescending(x => x.Id);

                    var verficationAuditList = verficationAuditDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<UAC_VerificationAuditJson>();
                    verficationAuditList.Map(jsons);

                    foreach (var json in jsons)
                    {
                        json.Code = 0;
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

        public HttpListResult<UAC_PassResetJson> GetPagedPasswordResetHistoryById(long userId, int? pageSize, int? pageNo)
        {
            string error = string.Empty;
            var result = new HttpListResult<UAC_PassResetJson>();
            int count = 0;
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanViewLog, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (UAC_PassResetDataService passDataService = new UAC_PassResetDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<UAC_PassReset> query = DbInstance.UAC_PassReset
                        .Where(x => x.UserId == userId)
                        .OrderByDescending(x => x.Id);

                    var passResetList = passDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<UAC_PassResetJson>();
                    passResetList.Map(jsons);
                    foreach (var json in jsons)
                    {
                        json.Code = 0;
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

        #endregion
    }
}