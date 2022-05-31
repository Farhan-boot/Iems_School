using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
using EMS.Framework.Settings;
using EMS.Web.Jsons.Custom.Academic;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Student.Controllers.WebApi
{
    public class ProfileApiController : StudentBaseApiController
    {

        public HttpResult<AcademicStudentJson> GetStudentById(long id = 0)
        {
            string error = string.Empty;
            id = HttpUtil.Profile.UserId;

            var result = new HttpResult<AcademicStudentJson>();

            if (!SiteSettings.Instance.Student.Profile.CanEdit)
            {
                result.HasError = true;
                result.Errors.Add("Sorry, you are not permitted to do this task.");
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

                if (id <= 0)
                {
                    studentEntity = User_Student.GetNew();
                }
                else
                {
                    studentEntity = DbInstance.User_Student.SingleOrDefault(c => c.UserId == HttpUtil.Profile.UserId);//studentDataService.GetById(id);
                }
                if (studentEntity == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Student information not found!");
                    return result;
                }
                studentEntity.Map(json.Student);

                using (User_AccountDataService accountDataService = new User_AccountDataService(DbInstance, HttpUtil.Profile))
                {
                    if (id <= 0)
                    {
                        accountEntity = User_Account.GetNew();
                    }
                    else
                    {
                        accountEntity = accountDataService.GetById(studentEntity.UserId);
                        accountEntity.Password = string.Empty;
                        accountEntity.PasswordSalt = string.Empty;
                    }
                    accountEntity.Map(json.Account);
                }
                using (User_ContactNumberDataService contactNumberDataService = new User_ContactNumberDataService(DbInstance, HttpUtil.Profile))
                {

                    contatNumberList = contactNumberDataService.GetAllByUserId(studentEntity.UserId)
                        .OrderBy(x => x.ContactNumberTypeEnumId).ToList();
                    if (contatNumberList.Count == 0 || contatNumberList.All(x=>x.ContactNumberTypeEnumId != (byte)User_ContactNumber.ContactNumberTypeEnum.PersonalMobile))
                    {
                        User_ContactNumber newContactNumber = User_ContactNumber.GetNew(BigInt.NewBigInt());
                        newContactNumber.UserId = studentEntity.UserId;
                        newContactNumber.ContactNumberTypeEnumId = (byte)User_ContactNumber.ContactNumberTypeEnum.PersonalMobile;
                        contatNumberList.Add(newContactNumber);
                    }
                   

                    contatNumberList.Map(json.ContactNumberList);
                }
                using (User_ContactEmailDataService contactEmailDataService = new User_ContactEmailDataService(DbInstance, HttpUtil.Profile))
                {

                    contatEmailList = contactEmailDataService.GetAllByUserId(studentEntity.UserId)
                        .OrderBy(x => x.ContactEmailTypeEnumId).ToList();

                    if (contatEmailList.All(x => x.ContactEmailType != User_ContactEmail.ContactEmailTypeEnum.PersonalEmail) )
                    {
                        User_ContactEmail newContactEmail = User_ContactEmail.GetNew(BigInt.NewBigInt());
                        newContactEmail.UserId = studentEntity.UserId;
                        newContactEmail.ContactEmailTypeEnumId = (byte)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail;
                        contatEmailList.Add(newContactEmail);
                    }
                    contatEmailList.Map(json.ContactEmailList);
                }
                using (User_ContactAddressDataService contactAddressDataService = new User_ContactAddressDataService(DbInstance, HttpUtil.Profile))
                {

                    contatAddressList = contactAddressDataService.GetAllByUserId(studentEntity.UserId)
                        .OrderByDescending(x => x.ContactAddressTypeEnumId).ToList();
                    //if (contatAddressList.Count == 0)//if no address found
                    //{
                    //    User_ContactAddress newContactAddressPresent = User_ContactAddress.GetNew();
                    //    newContactAddressPresent.ContactAddressTypeEnumId = (byte)User_ContactAddress.ContactAddressTypeEnum.Present;
                    //    contatAddressList.Add(newContactAddressPresent);

                    //    User_ContactAddress newContactAddressPermanent = User_ContactAddress.GetNew();
                    //    newContactAddressPermanent.ContactAddressTypeEnumId = (byte)User_ContactAddress.ContactAddressTypeEnum.Permanent;
                    //    contatAddressList.Add(newContactAddressPermanent);
                    //}
                    if (contatAddressList.All(x => x.ContactAddressType != User_ContactAddress.ContactAddressTypeEnum.Present))//if no present address
                    {
                        User_ContactAddress newContactAddressPresent = User_ContactAddress.GetNew();
                        newContactAddressPresent.UserId = studentEntity.UserId;
                        newContactAddressPresent.ContactAddressTypeEnumId = (byte)User_ContactAddress.ContactAddressTypeEnum.Present;
                        contatAddressList.Add(newContactAddressPresent);
                    }
                    if (contatAddressList.All(x => x.ContactAddressType != User_ContactAddress.ContactAddressTypeEnum.Permanent))//if no permanent address
                    {
                        User_ContactAddress newContactAddressPermanent = User_ContactAddress.GetNew();
                        newContactAddressPermanent.UserId = studentEntity.UserId;
                        newContactAddressPermanent.ContactAddressTypeEnumId = (byte)User_ContactAddress.ContactAddressTypeEnum.Permanent;
                        contatAddressList.Add(newContactAddressPermanent);
                    }

                    contatAddressList.Map(json.ContactAddressList);
                }
                using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
                {

                    educationList = educationDataService.GetAllByUserId(studentEntity.UserId)
                        .OrderByDescending(x => x.DegreeEquivalentEnumId).ToList();
                    //if (educationList.Count == 0)
                    //{
                    //    User_Education newEducationsHsc = User_Education.GetNew();
                    //    newEducationsHsc.DegreeEquivalent = User_Education.DegreeEquivalentEnum.HscEquivalent;
                    //    educationList.Add(newEducationsHsc);
                    //    User_Education newEducationsSsc = User_Education.GetNew();
                    //    newEducationsSsc.DegreeEquivalent = User_Education.DegreeEquivalentEnum.SscEquivalent;
                    //    educationList.Add(newEducationsSsc);

                    //}
                    if (educationList.All(x => x.DegreeEquivalent != User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent))
                    {
                        User_Education newEducationsHsc = User_Education.GetNew();
                        newEducationsHsc.UserId = studentEntity.UserId;
                        //newEducationsHsc.EducationType=User_Education.EducationTypeEnum.General;
                        newEducationsHsc.DegreeEquivalent = User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent;
                        newEducationsHsc.DegreeTitle = "Higher Secondary Certificate";
                        educationList.Add(newEducationsHsc);
                    }
                    if (educationList.All(x => x.DegreeEquivalent != User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent))
                    {
                        User_Education newEducationsSsc = User_Education.GetNew();
                        newEducationsSsc.UserId = studentEntity.UserId;
                        //newEducationsSsc.EducationType = User_Education.EducationTypeEnum.General;
                        newEducationsSsc.DegreeTitle = "Secondary School Certificate";
                        newEducationsSsc.DegreeEquivalent = User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent;
                        educationList.Add(newEducationsSsc);
                    }

                    educationList.Map(json.EducationList);
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }

            result.Data = json;
            return result;
        }
        public HttpListResult<User_StudentJson> GetstudentDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_StudentJson>();

            if (!SiteSettings.Instance.Student.Profile.CanEdit)
            {
                result.HasError = true;
                result.Errors.Add("Sorry, you are not permitted to do this task.");
                return result;
            }
            try
            {
                //User_StudentDataService studentDataService =
                //    new User_StudentDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

               
                result.DataExtra.EnrolmentStatusEnumList = EnumUtil.GetEnumList(typeof(User_Student.EnrollmentStatusEnum));
                result.DataExtra.EnrolmentTypeEnumList = EnumUtil.GetEnumList(typeof(User_Student.EnrollmentTypeEnum));


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
                result.DataExtra.CurriculumList = DbInstance.Aca_Curriculum.AsEnumerable().Select(t => new
                { Id = t.Id.ToString(), Name = t.ShortName }).ToList();

                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.ShortTitle }).ToList();
                result.DataExtra.DeptBatchList = DbInstance.Aca_DeptBatch.AsEnumerable().OrderBy(x => x.Name).Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

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
                var depts = DbInstance.HR_Department
                    .Where(x => x.TypeEnumId == 2)
                    .OrderBy(x => x.DepartmentNo)
                    .ToList();
                result.DataExtra.DepartmentList = depts.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.ShortName
                    });

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

        [HttpPost]
        public HttpResult<AcademicStudentJson> SaveStudent(AcademicStudentJson json)
        {



            string error = string.Empty;
            var result = new HttpResult<AcademicStudentJson>();

            if (!SiteSettings.Instance.Student.Profile.CanEdit)
            {
                result.HasError = true;
                result.Errors.Add("Sorry, you are not permitted to do this task.");
                return result;
            }


            if (HttpUtil.Profile.UserTypeId != (byte)User_Account.UserTypeEnum.Student || json.Account.Id != HttpUtil.Profile.UserId)
            {
                result.HasError = true;
                result.Errors.Add("Sorry, you are not permitted to save this profile!");
                return result;
            }

            var std = DbInstance.User_Account.SingleOrDefault(x => x.Id == HttpUtil.Profile.UserId && x.IsMigrate); //&& x.IsMigrate
            if (std == null)
            {
                result.HasError = true;
                result.Errors.Add("Sorry, you are not permitted to save your profile!");
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
                        else
                        {
                            //profile saved successfully
                            std.IsMigrate = false;
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
                || json.Student == null
                )
            {
                error = "Invalid Operation! Reload the page and try again later";
                return false;
            }
            //education
            if (json.EducationList==null || json.EducationList.Count <2)
            {
                error = "Please input your Education Information!";
                return false;
            }
            if (json.ContactAddressList == null || json.ContactAddressList.Count <2)
            {
                error = "Please input your Contact Address Information!";
                return false;
            }


            if (!json.Account.FullName.IsValid())
            {
                error = "Full Name can't blank!";
                return false;
            }
            if (!json.Account.BanglaName.IsValid())
            {
                error = "Bangla Name can't blank!";
                return false;
            }
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
            if ((DateTime.Today.Year-convertedDate.Value.Year)<15)
            {
                error = "Invalid Date of Birth, your age should be grater then 15y!";
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
            var student = json.Student;
            if (student == null)
            {
                error = "Invalid Student Information!";
                return false;
            }

            if (!student.RegistrationNo.IsValid())
            {
                student.RegistrationNo = "";
                //error = "Invalid Registration No!";
                //return false;
            }
            if (!student.RegistrationSession.IsValid() || !student.RegistrationSession.Contains("-"))
            {
                error = "Invalid Registration No!";
                return false;
            }

            /***
            //Student Checking
            ***/
            /***
           //father Mother Mobile Checking
           ***/
            if (!student.FatherMobile.IsValid())
            {
                error = "Father Mobile Number is not valid!";
                return false;
            }
            student.FatherMobile = student.FatherMobile.Trim().Replace("-", "").Replace(" ","").Replace("+88", "").Replace("+", "");
            if (!student.FatherMobile.IsValidMobileForBD())
            {
                error = "Father Mobile number should be only number and 11 digits!";
                return false;
            }
            //if (student.FatherMobile.Contains("+") )
            //{
            //    error = "Please exclude country code from Father Mobile Number!";
            //    return false;
            //}
            if (!student.MotherMobile.IsValid())
            {
                error = "Mother Mobile Number is not valid!";
                return false;
            }
           
            student.MotherMobile = student.MotherMobile.Trim().Replace("-", "").Replace(" ", "").Replace("+88", "").Replace("+", "");
            if (!student.MotherMobile.IsValidMobileForBD())
            {
                error = "Mother Mobile number should be only number and 11 digits!";
                return false;
            }
            if (student.MotherMobile.Equals(student.FatherMobile))
            {
                error = "Father's and Mother's Mobile Number should not be same!";
                return false;
            }
            /***
            //Contact Number Checking
            ***/
            foreach (var contactNumber in json.ContactNumberList)
            {
                bool isPersonal = contactNumber.ContactNumberTypeEnumId==(byte)User_ContactNumber.ContactNumberTypeEnum.PersonalMobile;
                if (contactNumber.ContactNumberTypeEnumId !=
                    (byte) User_ContactNumber.ContactNumberTypeEnum.PersonalMobile)
                {
                    continue;
                }

                if ((!contactNumber.ContactNumber.IsValid() || contactNumber.ContactNumber.Length < 4))
                {
                    error = "Contact Number is too short or invalid!";
                    return false;
                }

                contactNumber.ContactNumber = contactNumber.ContactNumber.Trim().Replace("-", "").Replace(" ", "").Replace("+88", "").Replace("+", "");
               
                if (contactNumber.ContactNumber.Equals(student.FatherMobile) || contactNumber.ContactNumber.Equals(student.MotherMobile))
                {
                    error = "Your Contact Number should not be same with Father/Mother's Mobile Number!";
                    return false;
                }
            }
            if (json.ContactNumberList.All(x => x.ContactNumberTypeEnumId != (byte) User_ContactNumber.ContactNumberTypeEnum.PersonalMobile))
            {
                error = "Personal Mobile can't blank!";
                return false;
            }
            /***
            //Contact Email Checking
            ***/
          
            var personalEmail = json.ContactEmailList
                .FirstOrDefault(
                    x => x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail);
            if (personalEmail == null)
            {
                //.ContactEmail.IsValid()
                error = "Personal Email can't blank!";
                return false;
            }
            else if (!personalEmail.ContactEmail.Trim().IsValidEmail())
            {
                //.ContactEmail.IsValid()
                error = "Personal Email invalid!";
                return false;
            }
           



            error = string.Empty;
            return true;
        }

        #region SaveStudent
        private bool IsValidToSaveAccount(User_Account newObj, out string error)
        {
            error = "";
            if (!newObj.UserName.IsValid())
            {
                error += "User Name is not valid.";
                return false;
            }
            if (newObj.UserName.Length > 128)
            {
                error += "User Name exceeded its maximum length 128.";
                return false;
            }
            if (!newObj.FullName.IsValid())
            {
                error += "Full Name is not valid.";
                return false;
            }
            if (newObj.FullName.Length > 128)
            {
                error += "Full Name exceeded its maximum length 128.";
                return false;
            }
            if (!newObj.BanglaName.IsValid())
            {
                error += "Bangla Name is not valid.";
                return false;
            }
            if (newObj.BanglaName.Length > 200)
            {
                error += "Bangla Name exceeded its maximum length 200.";
                return false;
            }
            //if (!newObj.Prefix.IsValid())
            //{
            //    error += "Prefix is not valid.";
            //    return false;
            //}
            //if (newObj.Prefix.Length > 50)
            //{
            //    error += "Prefix exceeded its maximum length 50.";
            //    return false;
            //}
            //if (!newObj.Suffix.IsValid())
            //{
            //    error += "Suffix is not valid.";
            //    return false;
            //}
            //if (newObj.Suffix.Length > 50)
            //{
            //    error += "Suffix exceeded its maximum length 50.";
            //    return false;
            //}
            if (!newObj.DateOfBirth.IsValid())
            {
                error += "Date Of Birth is not valid.";
                return false;
            }
            //if (newObj.PlaceOfBirth.IsValid() && newObj.PlaceOfBirth.Length > 50)
            //{
            //    error += "Place Of Birth exceeded its maximum length 50.";
            //    return false;
            //}
            //if (newObj.PlaceOfBirthCity.IsValid() && newObj.PlaceOfBirthCity.Length > 50)
            //{
            //    error += "Place Of Birth City exceeded its maximum length 50.";
            //    return false;
            //}

            if (newObj.GenderEnumId == null)
            {
                error += "Please select valid Gender.";
                return false;
            }
            if (newObj.ReligionEnumId == null)
            {
                error += "Please select valid Religion.";
                return false;
            }
            if (newObj.MaritalStatusEnumId == null)
            {
                error += "Please select valid Marital Status.";
                return false;
            }
            if (newObj.BloodGroupEnumId == null)
            {
                error += "Please select valid Blood Group.";
                return false;
            }
            if (newObj.FatherName.IsValid() && newObj.FatherName.Length > 128)
            {
                error += "Father Name exceeded its maximum length 128.";
                return false;
            }
            if (newObj.MotherName.IsValid() && newObj.MotherName.Length > 128)
            {
                error += "Mother Name exceeded its maximum length 128.";
                return false;
            }
            if (newObj.SpouseName.IsValid() && newObj.SpouseName.Length > 128)
            {
                error += "Spouse Name exceeded its maximum length 128.";
                return false;
            }


            if (newObj.IsLate == null)
            {
                error += "Is Late required.";
                return false;
            }
            //if (newObj.IsMilitary == null)
            //{
            //    error += "Is Military required.";
            //    return false;
            //}
            if (!newObj.Nationality.IsValid())
            {
                error += "Nationality is not valid.";
                return false;
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
            if (!newObj.Password.IsValid())
            {
                error += "Password is not valid.";
                return false;
            }
            if (newObj.Password.Length > 128)
            {
                error += "Password exceeded its maximum length 128.";
                return false;
            }
            if (!newObj.PasswordSalt.IsValid())
            {
                error += "Password Salt is not valid.";
                return false;
            }
            if (newObj.PasswordSalt.Length > 128)
            {
                error += "Password Salt exceeded its maximum length 128.";
                return false;
            }
            //if (newObj.IsApproved == null)
            //{
            //    error += "Is Approved required.";
            //    return false;
            //}
            //if (newObj.IsLockedOut == null)
            //{
            //    error += "Is Locked Out required.";
            //    return false;
            //}
            if (!newObj.LastLoginDate.IsValid())
            {
                error += "Last Login Date is not valid.";
                return false;
            }
            if (!newObj.LastPasswordChangedDate.IsValid())
            {
                error += "Last Password Changed Date is not valid.";
                return false;
            }
            if (!newObj.LastLockoutDate.IsValid())
            {
                error += "Last Lockout Date is not valid.";
                return false;
            }
            if (newObj.FailedPasswordAttemptCount == null)
            {
                error += "Failed Password Attempt Count required.";
                return false;
            }
            if (!newObj.FailedPasswordAttemptWindowStart.IsValid())
            {
                error += "Failed Password Attempt Window Start is not valid.";
                return false;
            }
            if (newObj.FailedPasswordAnswerAttemptCount == null)
            {
                error += "Failed Password Answer Attempt Count required.";
                return false;
            }
            if (!newObj.FailedPasswordAnswerAttemptWindowStart.IsValid())
            {
                error += "Failed Password Answer Attempt Window Start is not valid.";
                return false;
            }
            if (newObj.DepartmentId <= 0)
            {
                error += "Please select valid Department.";
                return false;
            }

            if (!newObj.JoiningDate.IsValid())
            {
                error += "Date Admitted is not valid.";
                return false;
            }
            //if (newObj.PersonalNo.IsValid() && newObj.PersonalNo.Length > 50)
            //{
            //    error += "Personal No exceeded its maximum length 50.";
            //    return false;
            //}
            if (newObj.IsMigrate == null)
            {
                error += "Is Migrate required.";
                return false;
            }


            if (newObj.BankAccountNo.IsValid() && newObj.BankAccountNo.Length > 50)
            {
                error += "Bank Account No exceeded its maximum length 50.";
                return false;
            }

            //if (newObj.CategoryEnumId == null)
            //{
            //    error += "Please select valid Category.";
            //    return false;
            //}
            if (newObj.CampusId <= 0)
            {
                error += "Please select valid Campus.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.User_Account.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A Account already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveAccountLogic(User_Account newObj, ref User_Account dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Account to save can't be null!";
                return false;
            }

            if (!IsValidToSaveAccount(newObj, out error))
                return false;

            bool isNewObject = true;
            User_Account objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.User_Account.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {
                var maxId = DbInstance.User_Account.OrderByDescending(item => item.Id).First().Id;
                newObj.Id = maxId;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = User_Account.GetNew(newObj.Id);
                DbInstance.User_Account.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.AccountManager.Account.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.AccountManager.Account.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.UserName = newObj.UserName;
            objToSave.FullName = newObj.FullName;
            objToSave.BanglaName = newObj.BanglaName;
            //objToSave.Prefix = newObj.Prefix;
            //objToSave.Suffix = newObj.Suffix;
            objToSave.DateOfBirth = newObj.DateOfBirth;
            //objToSave.PlaceOfBirth = newObj.PlaceOfBirth;
            //objToSave.PlaceOfBirthCity = newObj.PlaceOfBirthCity;
            objToSave.PlaceOfBirthCountryId = newObj.PlaceOfBirthCountryId;
            objToSave.GenderEnumId = newObj.GenderEnumId;
            objToSave.ReligionEnumId = newObj.ReligionEnumId;
            objToSave.MaritalStatusEnumId = newObj.MaritalStatusEnumId;
            objToSave.BloodGroupEnumId = newObj.BloodGroupEnumId;
            objToSave.FatherName = newObj.FatherName;
            objToSave.MotherName = newObj.MotherName;
            objToSave.SpouseName = newObj.SpouseName;
            objToSave.Height = newObj.Height;
            //objToSave.Weight = newObj.Weight;
            objToSave.IsLate = newObj.IsLate;
            //objToSave.IsMilitary = newObj.IsMilitary;
            objToSave.Nationality = newObj.Nationality;
            objToSave.NationalIdNumber = newObj.NationalIdNumber;
            objToSave.BirthCertificateNumber = newObj.BirthCertificateNumber;
            objToSave.UserTypeEnumId = newObj.UserTypeEnumId;
            objToSave.UserStatusEnumId = newObj.UserStatusEnumId;
            objToSave.Password = newObj.Password;
            objToSave.PasswordSalt = newObj.PasswordSalt;
            //objToSave.IsApproved = newObj.IsApproved;
            //objToSave.IsLockedOut = newObj.IsLockedOut;
            objToSave.LastLoginDate = newObj.LastLoginDate;
            objToSave.LastPasswordChangedDate = newObj.LastPasswordChangedDate;
            objToSave.LastLockoutDate = newObj.LastLockoutDate;
            objToSave.FailedPasswordAttemptCount = newObj.FailedPasswordAttemptCount;
            objToSave.FailedPasswordAttemptWindowStart = newObj.FailedPasswordAttemptWindowStart;
            objToSave.FailedPasswordAnswerAttemptCount = newObj.FailedPasswordAnswerAttemptCount;
            objToSave.FailedPasswordAnswerAttemptWindowStart = newObj.FailedPasswordAnswerAttemptWindowStart;
            objToSave.Remarks = newObj.Remarks;
            //objToSave.PersonalNo = newObj.PersonalNo;
            objToSave.IsMigrate = newObj.IsMigrate;
            objToSave.MigrationId = newObj.MigrationId;
            objToSave.BankName = newObj.BankName;
            objToSave.BankAccountNo = newObj.BankAccountNo;

            objToSave.JoiningDate = newObj.JoiningDate;
            objToSave.LeavingDate = newObj.LeavingDate;
            objToSave.DepartmentId = newObj.DepartmentId;


            //objToSave.RankId = newObj.RankId;
            //objToSave.CategoryEnumId = newObj.CategoryEnumId;
            objToSave.CampusId = newObj.CampusId;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }

        private bool IsValidToSaveStudent(User_Student newObj, out string error)
        {
            error = "";
            if (newObj.UserId <= 0)
            {
                error += "Please select valid User.";
                return false;
            }
            if (!newObj.RegistrationNo.IsValid())
            {
                error += "Registration No is not valid.";
                return false;
            }
            if (newObj.RegistrationNo.Length > 50)
            {
                error += "Registration No exceeded its maximum length 50.";
                return false;
            }
            if (!newObj.RegistrationSession.IsValid())
            {
                error += "Registration Session is not valid.";
                return false;
            }
            if (newObj.RegistrationSession.Length > 50)
            {
                error += "Registration Session exceeded its maximum length 50.";
                return false;
            }
            if (newObj.ClassRollNo == null)
            {
                error += "Class Roll No required.";
                return false;
            }
            if (newObj.AdmissionTestRollNo == null)
            {
                error += "Admission Test Roll No required.";
                return false;
            }
            if (newObj.AdmissionExamId == null)
            {
                error += "Admission Exam Setting Id required.";
                return false;
            }
          

            if (newObj.ProgramId <= 0)
            {
                error += "Please select valid Program.";
                return false;
            }
            if (newObj.ContinuingBatchId <= 0)
            {
                error += "Please select valid Dept Batch.";
                return false;
            }
            if (newObj.FeeCodeId == null)
            {
                error += "Fee Code Id required.";
                return false;
            }
            if (newObj.CurriculumId <= 0)
            {
                error += "Please select valid Curriculum.";
                return false;
            }

            if (newObj.StudentQuotaNameId == null)
            {
                error += "Please select valid Student Quota.";
                return false;
            }
            if (newObj.EnrollmentStatusEnumId == null)
            {
                error += "Please select valid Enrollment Status.";
                return false;
            }
            if (newObj.EnrollmentTypeEnumId == null)
            {
                error += "Please select valid Enrollment Type.";
                return false;
            }
            if (newObj.ParentsPrimaryJobTypeId == null)
            {
                error += "Please select valid Parents Primary Job Type.";
                return false;
            }
            if (newObj.LevelTermId <= 0)
            {
                error += "Please select valid Level.";
                return false;
            }
            //if (newObj.TermId <= 0)
            //{
            //    error += "Please select valid Term.";
            //    return false;
            //}
            if (newObj.CGPA == null)
            {
                error += "C G P A required.";
                return false;
            }
            if (newObj.CreditCompleted == null)
            {
                error += "Credit Completed required.";
                return false;
            }
       
            //if (newObj.DateGraduated != null && !newObj.DateGraduated.IsValid())
            //{
            //    newObj.DateGraduated = null;//just put null,if its nullable and not valid.
            //                                //error += "Date Graduated is not valid.";
            //                                //return false;
            //}
            if (newObj.GradingPolicyId == null)
            {
                error += "Please select valid Grade Type.";
                return false;
            }
            if (newObj.CourseCompleted == null)
            {
                error += "Course Completed required.";
                return false;
            }
            if (newObj.IncompleteCredits == null)
            {
                error += "Incomplete Credits required.";
                return false;
            }



            //TODO write your custom validation here.
            //var res =  DbInstance.User_Student.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A Student already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveStudentLogic(User_Student newObj, ref User_Student dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Student to save can't be null!";
                return false;
            }

            if (!IsValidToSaveStudent(newObj, out error))
                return false;

            bool isNewObject = true;
            User_Student objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.User_Student.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {
                var maxId = DbInstance.User_Account.OrderByDescending(item => item.Id).First().Id;
                newObj.Id = maxId; //BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = User_Student.GetNew(newObj.Id);
                DbInstance.User_Student.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.StudentManager.Student.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.StudentManager.Student.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.UserId = newObj.UserId;
            objToSave.RegistrationNo = newObj.RegistrationNo;
            objToSave.RegistrationSession = newObj.RegistrationSession;
            objToSave.ClassRollNo = newObj.ClassRollNo;
            objToSave.AdmissionTestRollNo = newObj.AdmissionTestRollNo;
            objToSave.AdmissionExamId = newObj.AdmissionExamId;
           
            objToSave.ProgramId = newObj.ProgramId;
            objToSave.ContinuingBatchId = newObj.ContinuingBatchId;
            objToSave.FeeCodeId = newObj.FeeCodeId;
            objToSave.CurriculumId = newObj.CurriculumId;
            objToSave.ElectiveCurriculumId = newObj.ElectiveCurriculumId;
            objToSave.StudentQuotaNameId = newObj.StudentQuotaNameId;
            objToSave.EnrollmentStatusEnumId = newObj.EnrollmentStatusEnumId;
            objToSave.EnrollmentTypeEnumId = newObj.EnrollmentTypeEnumId;
            objToSave.ParentsPrimaryJobTypeId = newObj.ParentsPrimaryJobTypeId;
            objToSave.LevelTermId = newObj.LevelTermId;
            //objToSave.TermId = newObj.TermId;
            objToSave.CGPA = newObj.CGPA;
            objToSave.CreditCompleted = newObj.CreditCompleted;
       

            objToSave.GradingPolicyId = newObj.GradingPolicyId;
            objToSave.CourseCompleted = newObj.CourseCompleted;
            objToSave.IncompleteCredits = newObj.IncompleteCredits;
            //objToSave.FatherId = newObj.FatherId;
            //objToSave.MotherId = newObj.MotherId;
            //objToSave.OtherParentId = newObj.OtherParentId;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }


        private bool IsValidToSaveContactAddress(User_ContactAddress newObj, out string error)
        {
            error = "";
            if (newObj.UserId <= 0)
            {
                error += "Please select valid User.";
                return false;
            }

            if (newObj.ContactAddressTypeEnumId == null)
            {
                error += "Please select valid Contact Address Type.";
                return false;
            }

            
            if (!newObj.Address1.IsValid())
            {
                error += "Address Line 1 required.";
                return false;
            }
            if (newObj.Address1.Length > 500)
            {
                error += "Address Line-1 exceeded its maximum length 500.";
                return false;
            }
            if (newObj.Address2.IsValid() && newObj.Address2.Length > 50)
            {
                error += "Address Line-2 exceeded its maximum length 500.";
                return false;
            }
            if (!newObj.PostOffice.IsValid())
            {
                error += "Post Office is not valid.";
                return false;
            }
            if (newObj.PostOffice.Length > 250)
            {
                error += "Post Office exceeded its maximum length 250.";
                return false;
            }
            if (!newObj.PoliceStation.IsValid())
            {
                error += "Police Station is not valid.";
                return false;
            }
            if (newObj.PoliceStation.Length > 250)
            {
                error += "Police Station exceeded its maximum length 250.";
                return false;
            }
            if (!newObj.District.IsValid())
            {
                error += "District is not valid.";
                return false;
            }
            if (newObj.District.Length > 250)
            {
                error += "District exceeded its maximum length 250.";
                return false;
            }

            if (newObj.CountryId <= 0)
            {
                error += "Please select valid Country.";
                return false;
            }
            
            if (newObj.IsValid == null)
            {
                error += "Is Valid required.";
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
        private bool SaveContactAddressLogic(User_ContactAddress newObj, ref User_ContactAddress dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Contact Address to save can't be null!";
                return false;
            }

            if (!IsValidToSaveContactAddress(newObj, out error))
                return false;

            bool isNewObject = true;
            User_ContactAddress objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.User_ContactAddress.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            //else
            //{
            //    newObj.Id = BigInt.NewBigInt();
            //}
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = User_ContactAddress.GetNew(newObj.Id);
                DbInstance.User_ContactAddress.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
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
            objToSave.UserId = newObj.UserId;
            //objToSave.CareOfPersonName = newObj.CareOfPersonName;
            objToSave.ContactAddressTypeEnumId = newObj.ContactAddressTypeEnumId;
            //objToSave.AppartmentNo = newObj.AppartmentNo;
            //objToSave.HouseNo = newObj.HouseNo;
            //objToSave.RoadNo = newObj.RoadNo;
            //objToSave.AreaInfo = newObj.AreaInfo;
            objToSave.PostOffice = newObj.PostOffice;
            objToSave.PoliceStation = newObj.PoliceStation;
            objToSave.District = newObj.District;
            //objToSave.PostCode = newObj.PostCode;
            objToSave.CountryId = newObj.CountryId;
            //objToSave.IsMailingContact = newObj.IsMailingContact;
            objToSave.IsValid = newObj.IsValid;
            //objToSave.PrivacyEnumId = newObj.PrivacyEnumId;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }


        private bool IsValidToSaveContactEmail(User_ContactEmail newObj, out string error)
        {
            error = "";
            if (newObj.UserId <= 0)
            {
                error += "Please select valid User.";
                return false;
            }
            if (newObj.ContactEmailTypeEnumId == null)
            {
                error += "Please select valid Contact Email Type.";
                return false;
            }
            if (!newObj.ContactEmail.IsValid())
            {
                error += "Contact Email is not valid.";
                return false;
            }
            if (newObj.ContactEmail.Length > 100)
            {
                error += "Contact Email exceeded its maximum length 100.";
                return false;
            }
            //if (newObj.IsPrimary == null)
            //{
            //    error += "Is Primary required.";
            //    return false;
            //}
            //if (newObj.IsActive == null)
            //{
            //    error += "Is Active required.";
            //    return false;
            //}
            if (newObj.IsValid == null)
            {
                error += "Is Valid required.";
                return false;
            }
            //if (newObj.IsMailingContact == null)
            //{
            //    error += "Is Mailing Contact required.";
            //    return false;
            //}
            if (newObj.PrivacyEnumId == null)
            {
                error += "Please select valid Privacy.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.User_ContactEmail.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A ContactEmail already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveContactEmailLogic(User_ContactEmail newObj, ref User_ContactEmail dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Contact Email to save can't be null!";
                return false;
            }

            if (!IsValidToSaveContactEmail(newObj, out error))
                return false;

            bool isNewObject = true;
            User_ContactEmail objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.User_ContactEmail.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = User_ContactEmail.GetNew(newObj.Id);
                DbInstance.User_ContactEmail.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ContactEmailManager.ContactEmail.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ContactEmailManager.ContactEmail.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.UserId = newObj.UserId;
            objToSave.ContactEmailTypeEnumId = newObj.ContactEmailTypeEnumId;
            objToSave.ContactEmail = newObj.ContactEmail;
            //objToSave.IsPrimary = newObj.IsPrimary;
            //objToSave.IsActive = newObj.IsActive;
            objToSave.IsValid = newObj.IsValid;
            //objToSave.IsMailingContact = newObj.IsMailingContact;
            objToSave.PrivacyEnumId = newObj.PrivacyEnumId;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }


        private bool IsValidToSaveEducation(User_Education newObj, out string error)
        {
            error = "";
            if (newObj.UserId <= 0)
            {
                error += "Please select valid User.";
                return false;
            }
   
            if (newObj.DegreeEquivalentEnumId == null)
            {
                error += "Please select valid Degree Equivalent.";
                return false;
            }
            if (!newObj.DegreeTitle.IsValid())
            {
                error += "Degree Title is not valid.";
                return false;
            }
            if (newObj.DegreeTitle.Length > 250)
            {
                error += "Degree Title exceeded its maximum length 250.";
                return false;
            }

            if (!newObj.InstitutionName.IsValid())
            {
                error += "Institution Name is not valid.";
                return false;
            }
            if (newObj.InstitutionName.Length > 250)
            {
                error += "Institution Name exceeded its maximum length 250.";
                return false;
            }


            if (newObj.CourseDuration.IsValid() && newObj.CourseDuration.Length > 250)
            {
                error += "Duration exceeded its maximum length 250.";
                return false;
            }
            if (newObj.YearOfPassing == null)
            {
                error += "Year Of Passing required.";
                return false;
            }
        

            if (newObj.UserId <= 0)
            {
                error += "Please select valid User.";
                return false;
            }
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
                error += "Degree Title is not valid.";
                return false;
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
                error += "Degree Group Major exceeded its maximum length 250.";
                return false;
            }
            if (!newObj.InstitutionName.IsValid())
            {
                error += "Institution Name is not valid.";
                return false;
            }
            if (newObj.InstitutionName.Length > 250)
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
            if (newObj.StudentRollOrIdNo.IsValid() && newObj.StudentRollOrIdNo.Length > 50)
            {
                error += "Student Roll Or Id No exceeded its maximum length 50.";
                return false;
            }
            //if (!newObj.GradeOrClass.IsValid())
            //{
            //    error += "Grade Or Class is not valid.";
            //    return false;
            //}
            //if (newObj.GradeOrClass.Length > 50)
            //{
            //    error += "Grade Or Class exceeded its maximum length 50.";
            //    return false;
            //}

            //TODO write your custom validation here.
            //var res =  DbInstance.User_Education.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A Education already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveEducationLogic(User_Education newObj, ref User_Education dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Education to save can't be null!";
                return false;
            }

            if (!IsValidToSaveEducation(newObj, out error))
                return false;

            bool isNewObject = true;
            User_Education objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.User_Education.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            //else
            //{
            //    newObj.Id = BigInt.NewBigInt();
            //}
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = User_Education.GetNew(newObj.Id);
                DbInstance.User_Education.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.EducationManager.Education.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.EducationManager.Education.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.UserId = newObj.UserId;

            objToSave.DegreeEquivalentEnumId = newObj.DegreeEquivalentEnumId;
            objToSave.DegreeCategoryId = newObj.DegreeCategoryId;
            objToSave.DegreeTitle = newObj.DegreeTitle;
            objToSave.DegreeGroupMajor = newObj.DegreeGroupMajor;
            objToSave.InstitutionName = newObj.InstitutionName;
            objToSave.BoardId = newObj.BoardId;
            objToSave.CourseDuration = newObj.CourseDuration;
            objToSave.YearOfPassing = newObj.YearOfPassing;
            objToSave.RegistrationNo = newObj.RegistrationNo;
            objToSave.StudentRollOrIdNo = newObj.StudentRollOrIdNo;
            objToSave.ObtainedGpaOrMarks = newObj.ObtainedGpaOrMarks;
            objToSave.ObtainedGradeOrClass = newObj.ObtainedGradeOrClass;
            objToSave.GpaNo4Sub = newObj.GpaNo4Sub;
            objToSave.FullMarksOrScale = newObj.FullMarksOrScale;
            objToSave.IsGolden = newObj.IsGolden;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }
        #endregion

    }
}
