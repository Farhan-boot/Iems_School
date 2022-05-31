using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Custom.Academic;
using EMS.Web.Jsons.Custom.Admission.Applicant;
using EMS.Web.Jsons.Models;
using EntityMapper = EMS.Web.Jsons.Models.EntityMapper;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using EMS.Communication.EmailProxy;
using EMS.Jsons.Models;
using Microsoft.Ajax.Utilities;
using OfficeOpenXml.FormulaParsing.Utilities;

//AddNewApplicantApiController.cs
namespace EMS.Web.UI.Areas.Admission.Controllers.WebApi
{
    public class StudentProfileApiController : EmployeeBaseApiController
    {
        #region Get Student Data For Update
        public HttpResult<UpdateStudentJson> GetStudentById(int id = 0)
        {
            //201701241753321453

            string error = string.Empty;
            var result = new HttpResult<UpdateStudentJson>();
            try
            {

                if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanEdit, out error))
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return result;
                }

                if (id <= 0)
                {
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add("Invalid Student ID, Student not found!");
                    return result;
                }

                User_Student studentEntity;
                //if (id <= 0)
                //{
                //    studentEntity = User_Student.GetNew();

                //}
                //else
                //{
                studentEntity = DbInstance.User_Student.SingleOrDefault(x => x.UserId == id);
                if (studentEntity == null)
                {
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add("Invalid Student ID, Student not found!");
                    return result;
                }
                //}

                var applicantJson = new UpdateStudentJson
                {
                    ContactAddressJsonList = new List<User_ContactAddressJson>(),
                    EducationJsonList = new List<User_EducationJson>(),
                };
                //applicantJson.IsAutoUserName = false;
                //applicantJson.DateAdmitted = DateTime.Now;
                studentEntity.Map(applicantJson);




                List<User_ContactAddress> contatAddressList = new List<User_ContactAddress>();
                List<User_Education> educationList = new List<User_Education>();
                //List<Acnt_StdScholarship> scholarshipList = new List<Acnt_StdScholarship>();


                //User_Account start
                User_Account accountEntity;
                if (id <= 0)
                {
                    accountEntity = User_Account.GetNew();
                }
                else
                {
                    accountEntity = DbInstance.User_Account.Find(studentEntity.UserId);
                    if (accountEntity==null)
                    {
                        result.HasError = true;
                        result.HasViewPermission = false;
                        result.Errors.Add("Invalid Student ID, Student not found!");
                        return result;
                    }
                    if (accountEntity.PasswordSalt.IsValid())
                    {
                        accountEntity.Password = string.Empty;
                    }


                    accountEntity.PasswordSalt = string.Empty;
                }
                accountEntity.Map(applicantJson);


                //Get Create And Update User data
                List<long> createAndUpdateUserIds = new List<long>();
                createAndUpdateUserIds.Add(accountEntity.CreateById);
                createAndUpdateUserIds.Add(accountEntity.LastChangedById);
                createAndUpdateUserIds = createAndUpdateUserIds.Distinct().ToList();

                var createAndUpdateUserList = DbInstance.User_Account
                    .Join(createAndUpdateUserIds, all => all.Id, req => req, (all, req) => all)
                    .ToList();

                var createBy = createAndUpdateUserList.SingleOrDefault(x => x.Id == accountEntity.CreateById);
                if (createBy!=null)
                {
                    applicantJson.CreateByFullName = createBy.FullName;
                    applicantJson.CreateByUserName = createBy.UserName;
                }
                var updateBy = createAndUpdateUserList.SingleOrDefault(x => x.Id == accountEntity.LastChangedById);
                if (updateBy != null)
                {
                    applicantJson.LastChangedByFullName = updateBy.FullName;
                    applicantJson.LastChangedByUserName = updateBy.UserName;
                }





                //User_Account end

                //contatAddressList start
                //if (id <= 0)
                //{
                //    User_ContactAddress newContactAddress = User_ContactAddress.GetNew();
                //    contatAddressList.Add(newContactAddress);
                //}
                if (id > 0)
                {


                    contatAddressList = DbInstance.User_ContactAddress.OrderByDescending(x => x.ContactAddressTypeEnumId).Where(c => c.UserId == studentEntity.UserId).ToList();
                }
                User_ContactAddress.ValidedPresentPermanentAddress(studentEntity.UserId, contatAddressList);

                var present =
                    contatAddressList.FirstOrDefault(
                        x => x.ContactAddressType == User_ContactAddress.ContactAddressTypeEnum.Present);
                var permanent =
                   contatAddressList.FirstOrDefault(
                       x => x.ContactAddressType == User_ContactAddress.ContactAddressTypeEnum.Permanent);

                //contatAddressList.Map(applicantJson.ContactAddressJsonList);

                present.Map(applicantJson.PresentAddressJson = new User_ContactAddressJson());
                permanent.Map(applicantJson.PermanentAddressJson = new User_ContactAddressJson());
                //contatAddressList end
                using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
                {
                    //if (id <= 0)
                    //{
                    //    User_Education newEducations = User_Education.GetNew();
                    //    educationList.Add(newEducations);
                    //}
                    if (id > 0)
                    {
                        educationList = educationDataService.GetAllByUserId(studentEntity.UserId)
                            .OrderBy(x => x.DegreeEquivalentEnumId).ToList();
                    }
                    //validate at least two education
                    //if (educationList.Count==0)
                    //{
                    //    User_Education.ValidedEducationList(studentEntity.UserId, educationList);
                    //}

                    educationList = educationList.OrderBy(x => x.DegreeEquivalentEnumId).ToList();
                    educationList.Map(applicantJson.EducationJsonList);

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

                result.DataExtra.EnrollmentStatusEnumList = EnumUtil.GetEnumList(typeof(User_Student.EnrollmentStatusEnum));
                result.DataExtra.EnrolmentTypeEnumList = EnumUtil.GetEnumList(typeof(User_Student.EnrollmentTypeEnum));



                result.DataExtra.UserTypeEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserTypeEnum));
                //result.DataExtra.UserCategoryEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserCategoryEnum));
                result.DataExtra.UserStatusEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserStatusEnum));
                result.DataExtra.UserContactPrivacyEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserContactPrivacyEnum));

                result.DataExtra.BloodGroupEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.BloodGroupEnum));
                result.DataExtra.GenderEnumList = EnumUtil.GetEnumList(typeof(User_Account.GenderEnum));
                result.DataExtra.MaritalStatusEnumList = EnumUtil.GetEnumList(typeof(User_Account.MaritalStatusEnum));
                result.DataExtra.ReligionEnumList = EnumUtil.GetEnumList(typeof(User_Account.ReligionEnum));

                //result.DataExtra.ContactAddressTypeEnumList = EnumUtil.GetEnumList(typeof(User_ContactAddress.ContactAddressTypeEnum));
                //result.DataExtra.ContactEmailTypeEnumList = EnumUtil.GetEnumList(typeof(User_ContactEmail.ContactEmailTypeEnum));
                //result.DataExtra.ContactNumberTypeEnumList = EnumUtil.GetEnumList(typeof(User_ContactNumber.ContactNumberTypeEnum));
                ////result.DataExtra.PrivacyEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.PrivacyEnum));

                result.DataExtra.EducationTypeEnumList = EnumUtil.GetEnumToUpperList(typeof(Adm_EducationBoard.BoardTypeEnum));
                result.DataExtra.DegreeEquivalentEnumList = EnumUtil.GetEnumToUpperList(typeof(User_Education.DegreeEquivalentEnum));
                result.DataExtra.AdmissionStatusEnumList = EnumUtil.GetEnumList(typeof(User_Student.AdmissionStatusEnum));



                result.DataExtra.ClassTimingGroupEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ClassTimingGroupEnum));


                result.DataExtra.CancelAdmissionReasonEnumList = GetCancelAdmissionReasonEnumList();

                

                //DropDown Option Tables User_Relationship
                //result.DataExtra.GradeTypeEnumList = EnumUtil.GetEnumList(typeof(User_Student.GradeTypeEnum));
                //result.DataExtra.StudentQuotaEnumList = EnumUtil.GetEnumList(typeof(User_Student.StudentQuotaEnum));

                result.DataExtra.ParentsPrimaryJobTypeList = DbInstance.Adm_ParentsPrimaryJobType.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                    });
                result.DataExtra.StudentQuotaNameList = DbInstance.Adm_StudentQuotaName.OrderBy(x => x.OrderNo).AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                result.DataExtra.ParentsPrimaryJobTypeList = DbInstance.Adm_ParentsPrimaryJobType.OrderBy(x => x.OrderNo).AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                result.DataExtra.GradingPolicyList = DbInstance.Aca_GradingPolicy.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.DeptBatchList = DbInstance.Aca_DeptBatch.OrderBy(x => x.BatchNo).AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList();



                result.DataExtra.StudyLevelTermList = DbInstance.Aca_StudyLevelTerm.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

                //result.DataExtra.StudyLevelList = DbInstance.Aca_StudyLevel.AsEnumerable().Select(t => new
                //{ Id = t.Id, Name = t.Name }).ToList();

                //result.DataExtra.StudyTermList = DbInstance.Aca_StudyTerm.AsEnumerable().Select(t => new
                //{ Id = t.Id, Name = t.Name }).ToList();

                //result.DataExtra.ClassSectionList = DbInstance.Aca_ClassSection.AsEnumerable().Select(t => new
                //{ Id = t.Id, Name = t.Name }).ToList();


                result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.ShortName
                }).ToList();

                result.DataExtra.ProgramList = DbInstance.Aca_Program
                        
                    .AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.ShortTitle,
                       
                        ProgramTypeEnumId = t.ProgramTypeEnumId,
                        ClassTimingGroupEnumId = t.ClassTimingGroupEnumId,
                        ClassTimingGroup = t.ClassTimingGroup.ToString(),
                        OfficialCertificateTitle = t.OfficialCertificateTitle,

                    })
                    .OrderBy(x => x.Name)
                    .ToList();
                result.DataExtra.SemesterList = Aca_Semester.GetDropdownList(DbInstance);

                //ToDo Valid admission exam query and order
                result.DataExtra.AdmissionExamList = DbInstance.Adm_AdmissionExam
                    .Include(x => x.Aca_Semester)
                    .AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                        SemesterId = t.Aca_Semester.Id.ToString(),
                        SemesterName = t.Aca_Semester.Name,

                    });

                //var ranks = DbInstance.User_Rank
                //    .OrderBy(x => x.CategoryEnumId)
                //    .ThenBy(x => x.Priority)
                //    .ToList();
                //result.DataExtra.RankList = ranks.AsEnumerable()
                //    .Select(t => new
                //    {
                //        Id = t.Id.ToString(),
                //        Name = t.Name,
                //        ShortName = t.ShortName,
                //        CategoryEnumId = t.CategoryEnumId,
                //        Category = t.Category.ToString().AddSpacesToSentence()
                //    });

                //result.DataExtra.BankList = DbInstance.General_Bank.AsEnumerable()
                //    .Select(t => new
                //    {
                //        Id = t.Id,
                //        Name = t.Name,
                //    });


                //result.DataExtra.CityList = DbInstance.General_City.AsEnumerable()
                //    .Select(t => new
                //    {
                //        Id=t.Id.ToString(),
                //        Name=t.Name
                //    });
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



                result.DataExtra.ClassSectionList = DbInstance.Aca_ClassSection.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                    })
                    .OrderBy(x => x.Id)
                    .ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;

        }

        private IList<EnumUtil.EnumInfo> GetCancelAdmissionReasonEnumList()
        {
            var cancelAdmissionReasonEnumList = EnumUtil.GetEnumList(typeof(User_Student.EnrollmentStatusEnum));

            cancelAdmissionReasonEnumList = cancelAdmissionReasonEnumList
                .Where(x => x.Id == (byte) User_Student.EnrollmentStatusEnum.AdmissionCancelled
                || x.Id == (byte)User_Student.EnrollmentStatusEnum.DropOut
                || x.Id == (byte)User_Student.EnrollmentStatusEnum.Terminated
                || x.Id==(byte)User_Student.EnrollmentStatusEnum.DepartmentChanged
                ).ToList();
            return cancelAdmissionReasonEnumList;

        }

       

        public HttpResult<Aca_CurriculumJson> GetDataExtraOnChangeProgram(long id = 0, Int32 stdId = 0)
        {
            var result = new HttpResult<Aca_CurriculumJson>();

            try
            {
                var curriculumList = DbInstance.Aca_Curriculum.AsEnumerable()
                    .Where(x => x.ProgramId == id
                    // && x.IsValid == true
                    // && x.IsOffering == true
                    )
                    .OrderByDescending(x => x.Year)
                    .Select(t => new
                    {
                        Id = t.Id.ToString(),
                        //Name=t.Name,
                        ShortName = t.ShortName,
                        Year = t.Year
                    })
                    .ToList();

                if (curriculumList.Count <= 0)
                {
                    //result.HasError = true;
                    result.Errors.Add("There is no Core Curriculum for this Program in the System.");

                    //return result;
                }

                result.DataExtra.CurriculumList = curriculumList;

                var curriculumElectiveGroupList = DbInstance.Aca_CurriculumElectiveGroup.AsEnumerable()
                    .Where(x => x.ProgramId == id)
                    .Select(t => new
                    {
                        Id = t.Id.ToString(),
                        Name = t.Name
                    }).ToList();
                result.DataExtra.CurriculumElectiveGroupList = curriculumElectiveGroupList;

              
            }
            catch (Exception e)
            {
                //result.HasError = true;
                result.Errors.Add(e.Message.ToString());
                return result;
            }


            return result;
        }


        public HttpResult<bool> GetConfirmOrCancelAdmissionByUserId(int id = 0, bool isConfirmAdmission = false, byte enrollmentStatusEnumId = 0,string admissionRemark="",bool isSendEmail=false)
        {
            string error = String.Empty;
            var result = new HttpResult<bool>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanConfirmAdmission, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            if (id <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Student ID!");
                return result;
            }


            var student = DbInstance.User_Student
                    .Include(x => x.User_Account)
                    //.Include(x=>x.Acnt_FeeCode) // Use For Add Admission Fee
                    //.Include(x => x.Acnt_FeeCode.Acnt_FeeCodePolicy) // Use For Add Admission Fee
                    //.Include(x => x.Aca_Program.Acnt_OfficialBank) // Use For Add Admission Fee
                    .FirstOrDefault(x => x.UserId == id);

            if (student == null)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Student ID!");
                return result;
            }

            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    if (isConfirmAdmission)
                    {

                        if (student.AdmissionStatusEnumId == (byte)User_Student.AdmissionStatusEnum.ProfileAndAdmissionCompleted)
                        {
                            result.HasError = true;
                            result.Errors.Add("This Student Already Admission Completed. Please Reload this Page.");
                            return result;
                        }

                        student.EnrollmentStatusEnumId = (byte)User_Student.EnrollmentStatusEnum.Continuing;
                        student.User_Account.UserStatusEnumId = (byte)User_Account.UserStatusEnum.Active;
                        student.User_Account.IsApproved = true;
                        student.User_Account.UserTypeEnumId = (byte)User_Account.UserTypeEnum.Student;
                        student.User_Account.IsProfileCompleted = true;
                        student.AdmissionStatus = User_Student.AdmissionStatusEnum.ProfileAndAdmissionCompleted;
                        //student.IsAdmissionFeePaid = true;
                        //student.IsFeesVerified = true;//todo need recheck

                        if (isSendEmail)
                        {
                            if (student.User_Account.UserEmail.IsValidEmail())
                            {
                                string msg = string.Empty;
                                var isEmailSent = EmailTemplate.SendNewAccountEmail(
                                    student.FullName,
                                    $"Congratulation! Your {SiteSettings.Instance.ProductBrandNameSolo} Online Account Created and Confirmed By Admin",
                                    student.User_Account.UserEmail,
                                    student.User_Account.UserName,
                                    student.User_Account.Password,
                                    out msg);
                                if (!isEmailSent)
                                {
                                    result.DataExtra.Message = "Email Send Fail , Reason :" + msg;
                                }
                                else
                                {
                                    result.DataExtra.Message = "Email Sent Successfully";
                                }
                            }
                            else
                            {
                                result.DataExtra.Message = "Email Send Fail , Reason :Email is not valid!";
                            }
                        }

                    }
                    else
                    {
                        if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanCancelAdmission, out error))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                            return result;
                        }

                        var hasValidReason = GetCancelAdmissionReasonEnumList().Any(x => x.Id == enrollmentStatusEnumId);
                        if (!hasValidReason)
                        {
                            result.HasError = true;
                            result.Errors.Add("Please Select Valid Reason for Admission Cancel.");
                            return result;
                        }
                        
                        if (student.AdmissionStatusEnumId == (byte)User_Student.AdmissionStatusEnum.AdmissionCancelled)
                        {
                            result.HasError = true;
                            result.Errors.Add("This Student Already Admission Cancelled. Please Reload this Page.");
                            return result;
                        }

                        student.EnrollmentStatusEnumId = enrollmentStatusEnumId;//(byte)User_Student.EnrollmentStatusEnum.AdmissionCancelled;
                        student.User_Account.UserStatusEnumId = (byte)User_Account.UserStatusEnum.Inactive;
                        student.User_Account.IsApproved = false;
                        student.AdmissionRemark = admissionRemark;

                        //student.User_Account.UserTypeEnumId = (byte)User_Account.UserTypeEnum.Applicant;
                        //student.User_Account.IsProfileCompleted = false;

                        student.AdmissionStatusEnumId = (byte) User_Student.AdmissionStatusEnum.AdmissionCancelled;

                        /*
                         student.User_Account.UserStatusEnumId = (byte)User_Account.UserStatusEnum.LockedWithReason;
                         student.User_Account.LockReason = "Admission Cancelled, Please Contact at Admission Dept.";
                        */
                    }



                    DbInstance.SaveChanges();
                    scope.Commit();
                }
                catch (Exception ex)
                {
                    result.HasError = true;
                    result.Errors.Add(ex.Message);
                    return result;
                }
            }

            return result;
        }

        public HttpResult<UpdateStudentJson> SaveUpdateUserNameGroup(UpdateStudentJson studentJsonObj)
        {
            string error = String.Empty;
            var result = new HttpResult<UpdateStudentJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanChangeUserName, out error)
            && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanChangeClassRoll, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            if (studentJsonObj == null)
            {
                result.HasError = true;
                result.Errors.Add("Student to save can't be null!");
                return result;
            }

            if (!studentJsonObj.UserName.IsValid())
            {
                result.HasError = true;
                result.Errors.Add("Student ID can't blank.");
                return result;
            }

            //if (studentJsonObj.UgcUniqueId.IsValid())
            //{
            //    if (!studentJsonObj.UgcUniqueId.IsNumeric())
            //    {
            //        result.HasError = true;
            //        result.Errors.Add("UGC Unique Id Must be Numeric.");
            //        return result;
            //    }
            //}

            if (studentJsonObj.UserName.Trim().Length > 50)
            {
                result.HasError = true;
                result.Errors.Add("Student ID exceeded its maximum length 50.");
                return result;
            }
            if (studentJsonObj.UserName.Trim().Length <=3)
            {
                result.HasError = true;
                result.Errors.Add("Student ID can't be blank.");
                return result;
            }

            var userName = studentJsonObj.UserName.Trim();

            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    long output;
                    var id = long.TryParse(studentJsonObj.Id, out output) ? output : 0;
                    User_Student objToSave = null;

                    if (id != 0)
                    {
                        objToSave = DbInstance.User_Student
                            .Include(x => x.User_Account)
                            .SingleOrDefault(x => x.UserId == id);
                    }
                    if(objToSave == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("Invalid Student ID!");
                        return result;
                    }

                    if (objToSave != null)
                    {
                        if (objToSave.User_Account.UserName != studentJsonObj.UserName)
                        {
                            //Ems4 -update
                            var isExistUserName = DbInstance.User_Student
                                .Include(x => x.User_Account)
                                .Any(x => (x.ClassRollNo == userName
                                           || x.User_Account.UserName == userName)
                                );

                            if (isExistUserName)
                            {
                                result.HasError = true;
                                result.Errors.Add($"You can't Update this User, Because Student ID:{userName} Already Exist.");
                                return result;
                            }

                            #region Adding Log User Name Changing.

                            var userProfileChangeAudit = User_ProfileChangeAudit.GetNew();

                            // Here for department changing previous students user id will be considered as logged userID.

                            userProfileChangeAudit.UserId = objToSave.UserId;

                            userProfileChangeAudit.FieldEnumId = (byte)User_ProfileChangeAudit.FieldEnum.UserNameChange;

                            var currentSemesterId =
                                DbInstance.Aca_Semester.Where(x => x.StatusEnumId == (byte)Aca_Semester.StatusEnum.Ongoing).Select(x => x.Id).FirstOrDefault();

                            if (currentSemesterId <= 0)
                            {
                                result.HasError = true;
                                result.Errors.Add($"System does not have any ongoing semester. Please set a ongoing semester first.");
                                return result;
                            }

                            userProfileChangeAudit.SemesterId = currentSemesterId;

                            userProfileChangeAudit.OldValue = objToSave.User_Account.UserName;
                            userProfileChangeAudit.NewValue = studentJsonObj.UserName;

                            if (!Facade.UserCredentialFacade.AddProfileChangeAudit(userProfileChangeAudit, out error))
                            {
                                result.HasError = true;
                                result.Errors.Add(error);
                                return result;
                            }

                            #endregion

                            objToSave.User_Account.History += $"Student's Student Id Changed From {objToSave.User_Account.UserName} to {studentJsonObj.UserName} by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
                        }

                        objToSave.ClassRollNo = objToSave.User_Account.UserName = studentJsonObj.UserName?.Trim() ?? studentJsonObj.UserName;

                        if (objToSave.PreviousStudentUserName != studentJsonObj.PreviousStudentUserName)
                        {
                            objToSave.User_Account.History += $"Student's Previous Student Id Changed From {objToSave.PreviousStudentUserName} to {studentJsonObj.PreviousStudentUserName} by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
                        }
                        objToSave.PreviousStudentUserName = studentJsonObj.PreviousStudentUserName;


                        if (objToSave.UgcUniqueId != studentJsonObj.UgcUniqueId)
                        {
                            //Ems4 -update
                            var isExistUgcUniqueId = DbInstance.User_Student
                                .Any(x => (x.UgcUniqueId.Trim() == studentJsonObj.UgcUniqueId.Trim()));

                            if (isExistUgcUniqueId)
                            {
                                result.HasError = true;
                                result.Errors.Add($"You can't Update this User, Because UGC Unique ID:{studentJsonObj.UgcUniqueId} Already Exist.");
                                return result;
                            }

                            #region Adding Log For UGC Unique Id Changing.

                            var userProfileChangeAudit = User_ProfileChangeAudit.GetNew();

                            // Here for department changing previous students user id will be considered as logged userID.

                            userProfileChangeAudit.UserId = objToSave.UserId;

                            userProfileChangeAudit.FieldEnumId = (byte)User_ProfileChangeAudit.FieldEnum.UgcUniqueIdChange;

                            var currentSemesterId =
                                DbInstance.Aca_Semester.Where(x => x.StatusEnumId == (byte)Aca_Semester.StatusEnum.Ongoing).Select(x => x.Id).FirstOrDefault();

                            if (currentSemesterId <= 0)
                            {
                                result.HasError = true;
                                result.Errors.Add($"System does not have any ongoing semester. Please set a ongoing semester first.");
                                return result;
                            }

                            userProfileChangeAudit.SemesterId = currentSemesterId;

                            userProfileChangeAudit.OldValue = objToSave.UgcUniqueId;
                            userProfileChangeAudit.NewValue = studentJsonObj.UgcUniqueId;

                            if (!Facade.UserCredentialFacade.AddProfileChangeAudit(userProfileChangeAudit, out error))
                            {
                                result.HasError = true;
                                result.Errors.Add(error);
                                return result;
                            }

                            #endregion

                            objToSave.User_Account.History += $"Student's UGC Unique Id Changed From {objToSave.UgcUniqueId} to {studentJsonObj.UgcUniqueId} by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
                        }
                        objToSave.UgcUniqueId = studentJsonObj.UgcUniqueId;

                        //objToSave.ClassRollNo = studentJsonObj.ClassRollNo?.Trim() ?? studentJsonObj.ClassRollNo;

                        //objToSave.RegistrationNo= studentJsonObj.UserName?.Trim() ?? studentJsonObj.UserName;
                        //objToSave.AdmissionTestRollNo= studentJsonObj.UserName?.Trim() ?? studentJsonObj.UserName;


                        DbInstance.SaveChanges();
                        scope.Commit();
                    }


                }
                catch (Exception ex)
                {
                    result.HasError = true;
                    result.Errors.Add(ex.Message);
                }
            }
            return result;
        }
        public HttpResult<UpdateStudentJson> SaveUpdateStudentNameGroup(UpdateStudentJson studentJsonObj)
        {
            string error = String.Empty;
            var result = new HttpResult<UpdateStudentJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanChangeStudentName, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            if (studentJsonObj == null)
            {
                result.HasError = true;
                result.Errors.Add("Student to save can't be null!");
                return result;
            }

            if (!studentJsonObj.FullName.IsValid())
            {
                result.HasError = true;
                result.Errors.Add("Student Name can't be blank.");
                return result;
            }

            //if (studentJsonObj.UserName.Trim().Length > 50)
            //{
            //    result.HasError = true;
            //    result.Errors.Add("Student ID exceeded its maximum length 50.");
            //    return result;
            //}
            //if (studentJsonObj.UserName.Trim().Length <= 3)
            //{
            //    result.HasError = true;
            //    result.Errors.Add("Student ID can't be blank.");
            //    return result;
            //}

            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    long output;
                    var id = long.TryParse(studentJsonObj.Id, out output) ? output : 0;
                    User_Student objToSave = null;

                    if (id != 0)
                    {
                        objToSave = DbInstance.User_Student
                            .Include(x => x.User_Account)
                            .SingleOrDefault(x => x.UserId == id);
                    }
                    else
                    {
                        result.HasError = true;
                        result.Errors.Add("Invalid Student ID!");
                        return result;
                    }

                    if (objToSave != null)
                    {
                        if (objToSave.User_Account.FullName != studentJsonObj.FullName)
                        {
                            #region Adding Log For UGC Unique Id Changing.

                            var userProfileChangeAudit = User_ProfileChangeAudit.GetNew();

                            // Here for department changing previous students user id will be considered as logged userID.

                            userProfileChangeAudit.UserId = objToSave.UserId;

                            userProfileChangeAudit.FieldEnumId = (byte)User_ProfileChangeAudit.FieldEnum.StudentNameChange;

                            var currentSemesterId =
                                DbInstance.Aca_Semester.Where(x => x.StatusEnumId == (byte)Aca_Semester.StatusEnum.Ongoing).Select(x => x.Id).FirstOrDefault();

                            if (currentSemesterId <= 0)
                            {
                                result.HasError = true;
                                result.Errors.Add($"System does not have any ongoing semester. Please set a ongoing semester first.");
                                return result;
                            }

                            userProfileChangeAudit.SemesterId = currentSemesterId;

                            userProfileChangeAudit.OldValue = objToSave.User_Account.FullName;
                            userProfileChangeAudit.NewValue = studentJsonObj.FullName;

                            if (!Facade.UserCredentialFacade.AddProfileChangeAudit(userProfileChangeAudit, out error))
                            {
                                result.HasError = true;
                                result.Errors.Add(error);
                                return result;
                            }

                            #endregion

                            objToSave.User_Account.History += $"Student's Name Changed From {objToSave.User_Account.FullName} to {studentJsonObj.FullName} by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
                        }



                        objToSave.User_Account.FullName = studentJsonObj.FullName;
                        DbInstance.SaveChanges();
                        scope.Commit();
                    }


                }
                catch (Exception ex)
                {
                    result.HasError = true;
                    result.Errors.Add(ex.Message);
                }
            }
            return result;
        }

        public HttpResult<UpdateStudentJson> SaveUpdateAcademicInfoGroup(UpdateStudentJson studentJsonObj)
        {
            string error = String.Empty;
            var result = new HttpResult<UpdateStudentJson>();
            try
            {
                long output;

                var id = long.TryParse(studentJsonObj.Id, out output) ? output : 0;
                User_Student objToSave = null;

                if (id != 0)
                {
                    objToSave = DbInstance.User_Student
                        .Include(x => x.User_Account)
                        .SingleOrDefault(x => x.UserId == id);
                    if (objToSave == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("No Student Found!");
                        return result;
                    }
                }
                else
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Student ID!");
                    return result;
                }

                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (!SaveAcademicInfoGroupLogic(studentJsonObj, ref objToSave, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        return result;
                    }

                    DbInstance.SaveChanges();
                    scope.Commit();
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
            }
            return result;
        }

        private bool SaveAcademicInfoGroupLogic(UpdateStudentJson studentJsonObj,ref User_Student objToSave,out string error)
        {
            error = string.Empty;

            bool isPermittedToChangeProgram = PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanChangeProgram);
            bool isPermittedToChangeFeeCode = PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanChangeFeeCode);
            bool isPermittedToChangeCurriculum = PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanChangeCurriculum);

            if (!(isPermittedToChangeProgram || isPermittedToChangeFeeCode || isPermittedToChangeCurriculum))
            {
                error = "Sorry, you are not permitted to do this task.";
                return false;
            }
            long output;
            if (studentJsonObj == null)
            {
                error +="Student to save can't be null!";
                return false;
            }

            if (studentJsonObj.ProgramId <= 0)
            {
                error += "Please select valid Program.";
                return false;

            }
            
            long curriculumId = long.TryParse(studentJsonObj.CurriculumId, out output) ? output : 0;
            if (curriculumId <= 0)
            {
                error += "Please select valid Curriculum.";
                return false;
            }

            if (studentJsonObj.ClassTimingGroupEnumId == null)
            {
                error += "Please select valid Class Timing Group.";
                return false;
            }
            if (studentJsonObj.DepartmentId <= 0)
            {
                error += "Please select valid Department.";
                return false;
            }


            //If Program Changed then all this will be checked.
            if (objToSave.ProgramId != studentJsonObj.ProgramId)
            {
                if (isPermittedToChangeProgram)
                {
                    var oldProgramId = objToSave.ProgramId;
                    var oldProgramName = DbInstance.Aca_Program.Where(x => x.Id == oldProgramId).Select(x =>
                        new
                        {
                            Name = x.ShortTitle
                        }).FirstOrDefault()?.Name;

                    if (!oldProgramName.IsValid())
                    {
                        error = "Previous Program couldn't be found.";
                        return false;
                    }
                    var newProgram = DbInstance.Aca_Program.Where(x => x.Id == studentJsonObj.ProgramId).Select(x =>
                        new
                        {
                            Name = x.ShortTitle,
                        }).FirstOrDefault();
                    if (newProgram == null)
                    {
                        error = "New Selected Program couldn't be found.";
                        return false;
                    }

                    #region Adding Log For Program Changing.

                    var userProfileChangeAudit = User_ProfileChangeAudit.GetNew();

                    // Here for department changing previous students user id will be considered as logged userID.

                    userProfileChangeAudit.UserId = objToSave.UserId;

                    userProfileChangeAudit.FieldEnumId = (byte)User_ProfileChangeAudit.FieldEnum.DepartmentChange;

                    var currentSemesterId =
                        DbInstance.Aca_Semester.Where(x => x.StatusEnumId == (byte)Aca_Semester.StatusEnum.Ongoing).Select(x => x.Id).FirstOrDefault();

                    if (currentSemesterId <= 0)
                    {
                        error = "System does not have any ongoing semester. Please set a ongoing semester first.";
                        return false;
                    }

                    userProfileChangeAudit.SemesterId = currentSemesterId;

                    userProfileChangeAudit.OldPk = oldProgramId.ToString();
                    userProfileChangeAudit.NewPk = studentJsonObj.ProgramId.ToString();

                    userProfileChangeAudit.OldValue = oldProgramName;
                    userProfileChangeAudit.NewValue = newProgram.Name;

                    userProfileChangeAudit.Remark = "Program Changed From Student's Profile";

                    if (!Facade.UserCredentialFacade.AddProfileChangeAudit(userProfileChangeAudit, out error))
                    {
                        return false;
                    }

                    #endregion

                    objToSave.User_Account.History += $"Student's Program Changed From {oldProgramName} to {newProgram.Name} by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
                }
                else
                {
                    error += "Sorry, you are not permitted to change Program of a Student.";
                    return false;
                }
            }
            objToSave.ProgramId = studentJsonObj.ProgramId;

            if (objToSave.PreviousProgramId != studentJsonObj.PreviousProgramId)
            {
                if (isPermittedToChangeProgram && objToSave.EnrollmentTypeEnumId == (byte)User_Student.EnrollmentTypeEnum.InternalDepartmentChange)
                {
                    var oldProgramId = objToSave.PreviousProgramId;
                    var oldProgramName = DbInstance.Aca_Program.Where(x => x.Id == oldProgramId).Select(x =>
                        new
                        {
                            Name = x.Name
                        }).FirstOrDefault()?.Name;

                    var newProgramName = DbInstance.Aca_Program.Where(x => x.Id == studentJsonObj.PreviousProgramId).Select(x =>
                        new
                        {
                            Name = x.Name
                        }).FirstOrDefault()?.Name;
                    if (!newProgramName.IsValid())
                    {
                        error = "New Selected Program couldn't be found.";
                        return false;
                    }

                    objToSave.User_Account.History += $"Student's Previous Program Changed From {oldProgramName} to {newProgramName} by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
                    objToSave.PreviousProgramId = studentJsonObj.PreviousProgramId;
                }
                else if(objToSave.EnrollmentTypeEnumId != (byte)User_Student.EnrollmentTypeEnum.InternalDepartmentChange)
                {
                    error += "Sorry, You can only set previous Program Id of a Internal Department Change Student.";
                    return false;
                }
                else
                {
                    error += "Sorry, you are not permitted to change Previous Program of a Student.";
                    return false;
                }
            }
            

            if (objToSave.JoiningBatchId != studentJsonObj.JoiningBatchId)
            {
                if (isPermittedToChangeProgram)
                {
                    var previousBatchId = objToSave.JoiningBatchId;
                    var oldBatchName = DbInstance.Aca_DeptBatch.Where(x => x.Id == previousBatchId).Select(x =>
                        new
                        {
                            Name = x.Name
                        }).FirstOrDefault()?.Name;

                    var newBatchName = DbInstance.Aca_DeptBatch.Where(x => x.Id == studentJsonObj.JoiningBatchId).Select(x =>
                        new
                        {
                            Name = x.Name
                        }).FirstOrDefault()?.Name;

                    if (!newBatchName.IsValid())
                    {
                        error = "New Selected Joining Batch couldn't be found.";
                        return false;
                    }
                    objToSave.User_Account.History += $"Student's Joining Batch Changed From {oldBatchName} to {newBatchName} by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
                }
                else
                {
                    error += "Sorry, you are not permitted to change Joining Batch of a Student.";
                    return false;
                }
            }
            objToSave.JoiningBatchId = studentJsonObj.JoiningBatchId;

            //If Grading Policy Changed then all this will be checked.
            if (objToSave.GradingPolicyId != studentJsonObj.GradingPolicyId)
            {
                if (isPermittedToChangeProgram)
                {
                    var oldGradingPolicyId = objToSave.GradingPolicyId;
                    var oldGradingPolicyName = DbInstance.Aca_GradingPolicy.Where(x => x.Id == oldGradingPolicyId).Select(x =>
                        new
                        {
                            Name = x.Name
                        }).FirstOrDefault()?.Name;

                    if (!oldGradingPolicyName.IsValid())
                    {
                        error = "Previous Grading Policy couldn't be found.";
                        return false;
                    }
                    var newGradingPolicyName = DbInstance.Aca_GradingPolicy.Where(x => x.Id == studentJsonObj.GradingPolicyId).Select(x =>
                        new
                        {
                            Name = x.Name
                        }).FirstOrDefault()?.Name;
                    if (!newGradingPolicyName.IsValid())
                    {
                        error = "New Selected Grading Policy couldn't be found.";
                        return false;
                    }
                    objToSave.User_Account.History += $"Student's Grading Policy Changed From {oldGradingPolicyName} to {newGradingPolicyName} by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
                }
                else
                {
                    error += "Sorry, you are not permitted to change GradingPolicy of a Student.";
                    return false;
                }
            }
            objToSave.GradingPolicyId = studentJsonObj.GradingPolicyId;


            if (objToSave.FeeCodeId != studentJsonObj.FeeCodeId)
            {
                if (isPermittedToChangeFeeCode || isPermittedToChangeProgram)
                {
                    var oldFeeCodeId = objToSave.FeeCodeId;
                    /*var oldFeeCodeName = DbInstance.Acnt_FeeCode.Where(x => x.Id == oldFeeCodeId).Select(x =>
                        new
                        {
                            Name = x.Name
                        }).FirstOrDefault()?.Name;

                    if (!oldFeeCodeName.IsValid())
                    {
                        error = "Previous Fee Code couldn't be found.";
                        return false;
                    }*/
                    /*var newFeeCodeName = DbInstance.Acnt_FeeCode.Where(x => x.Id == studentJsonObj.FeeCodeId).Select(x =>
                        new
                        {
                            Name = x.Name
                        }).FirstOrDefault()?.Name;
                    if (!newFeeCodeName.IsValid())
                    {
                        error = "New Selected Fee Code couldn't be found.";
                        return false;
                    }*/

                    #region Adding Log For Fee Code Changing.

                    var userProfileChangeAudit = User_ProfileChangeAudit.GetNew();

                    // Here for department changing previous students user id will be considered as logged userID.

                    userProfileChangeAudit.UserId = objToSave.UserId;

                    userProfileChangeAudit.FieldEnumId = (byte)User_ProfileChangeAudit.FieldEnum.FeeCodeChange;

                    var currentSemesterId =
                        DbInstance.Aca_Semester.Where(x => x.StatusEnumId == (byte)Aca_Semester.StatusEnum.Ongoing).Select(x => x.Id).FirstOrDefault();

                    if (currentSemesterId <= 0)
                    {
                        error = "System does not have any ongoing semester. Please set a ongoing semester first.";
                        return false;
                    }

                    userProfileChangeAudit.SemesterId = currentSemesterId;

                    userProfileChangeAudit.OldPk = oldFeeCodeId.ToString();
                    userProfileChangeAudit.NewPk = studentJsonObj.FeeCodeId.ToString();

                    /*userProfileChangeAudit.OldValue = oldFeeCodeName;
                    userProfileChangeAudit.NewValue = newFeeCodeName;*/

                    if (!Facade.UserCredentialFacade.AddProfileChangeAudit(userProfileChangeAudit, out error))
                    {
                        return false;
                    }

                    #endregion

                    //objToSave.User_Account.History += $"Student's Fee Code Changed From {oldFeeCodeName} to {newFeeCodeName} by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
                }
                else
                {
                    error += "Sorry, you are not permitted to change Fee Code of a Student.";
                    return false;
                }
            }
            objToSave.FeeCodeId = studentJsonObj.FeeCodeId;

            //If curriculum changed then this msg will show.
            if (objToSave.CurriculumId != curriculumId)
            {
                if (isPermittedToChangeCurriculum || isPermittedToChangeProgram)
                {
                    var oldCurriculumId = objToSave.CurriculumId;
                    var oldCurriculumName = DbInstance.Aca_Curriculum.Where(x => x.Id == oldCurriculumId).Select(x =>
                        new
                        {
                            Name = x.ShortName
                        }).FirstOrDefault()?.Name;

                    if (!oldCurriculumName.IsValid())
                    {
                        error = "Previous Curriculum couldn't be found.";
                        return false;
                    }
                    var newCurriculumName = DbInstance.Aca_Curriculum.Where(x => x.Id == curriculumId).Select(x =>
                        new
                        {
                            Name = x.ShortName
                        }).FirstOrDefault()?.Name;
                    if (!newCurriculumName.IsValid())
                    {
                        error = "New Selected Curriculum couldn't be found.";
                        return false;
                    }

                    #region Adding Log For Curriculum Changing.

                    var userProfileChangeAudit = User_ProfileChangeAudit.GetNew();

                    // Here for department changing previous students user id will be considered as logged userID.

                    userProfileChangeAudit.UserId = objToSave.UserId;

                    userProfileChangeAudit.FieldEnumId = (byte)User_ProfileChangeAudit.FieldEnum.CurriculumChange;

                    var currentSemesterId =
                        DbInstance.Aca_Semester.Where(x => x.StatusEnumId == (byte)Aca_Semester.StatusEnum.Ongoing).Select(x => x.Id).FirstOrDefault();

                    if (currentSemesterId <= 0)
                    {
                        error = "System does not have any ongoing semester. Please set a ongoing semester first.";
                        return false;
                    }

                    userProfileChangeAudit.SemesterId = currentSemesterId;

                    userProfileChangeAudit.OldPk = oldCurriculumId.ToString();
                    userProfileChangeAudit.NewPk = curriculumId.ToString();

                    userProfileChangeAudit.OldValue = oldCurriculumName;
                    userProfileChangeAudit.NewValue = newCurriculumName;

                    if (!Facade.UserCredentialFacade.AddProfileChangeAudit(userProfileChangeAudit, out error))
                    {
                        return false;
                    }

                    #endregion

                    objToSave.User_Account.History += $"Student's Core Curriculum Changed From {oldCurriculumName} to {newCurriculumName} by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
                }
                else
                {
                    error += "Sorry, you are not permitted to change Curriculum of a Student.";
                    return false;
                }
            }
            objToSave.CurriculumId = long.TryParse(studentJsonObj.CurriculumId, out output) ? output : 0;

            objToSave.MajorCurriculumId = long.TryParse(studentJsonObj.MajorCurriculumId, out output) ? output : (Int64?)null;
            objToSave.SecondMajorCurriculumId = long.TryParse(studentJsonObj.SecondMajorCurriculumId, out output) ? output : (Int64?)null;
            objToSave.MinorCurriculumId = long.TryParse(studentJsonObj.MinorCurriculumId, out output) ? output : (Int64?)null;
            objToSave.ElectiveCurriculumId = long.TryParse(studentJsonObj.ElectiveCurriculumId, out output) ? output : (Int64?)null;

            objToSave.ClassTimingGroupEnumId = studentJsonObj.ClassTimingGroupEnumId;
            objToSave.User_Account.DepartmentId = studentJsonObj.DepartmentId;

            return true;
        }

        public HttpResult<User_AccountJson> SaveResetAndSendPassword([FromUri] int userId)
        {
            string error = string.Empty;
            string msg = string.Empty;
            var result = new HttpResult<User_AccountJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanResetPassword, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                string confirmPassword;
                var password = confirmPassword = RandomPassword.GenerateOnlyNumber(5).ToString();// RandomPassword.Generate(6, 8);

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

        public HttpResult<bool> GetRemoveProfilePictureById(int userId)
        {
            string error = string.Empty;
            var result = new HttpResult<bool>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanRemoveProfilePicture, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            if (userId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Studnet Id.");
                return result;
            }
            var user = DbInstance.User_Account.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Student!");
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

                user.ProfilePictureImageUrl = null;
                user.History += $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} Profile Picture Removed, By {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}.<br>";

                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
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



        [System.Web.Http.HttpPost]
        public HttpResult<string> UploadProfilePicture(int userId)
        {
            string error = string.Empty;
            var result = new HttpResult<string>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanUploadProfilePicture, out error))
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
                var newFileName = newFileId + "" + extension;
                var folder = "";
                //Request.Files[file]

                string relativeFolderUrl = DiskStorageSettings.ItemFolderNames.StudentProfilePictureFolderName;

                relativeFolderUrl =Path.Combine(relativeFolderUrl,folder);

                var relativeFolderDiskPath = relativeFolderUrl.Replace("/", "\\");
                var fullDiskPathFolder = Path.Combine(DiskStorageSettings.RootStoragePathForProfilePictures, relativeFolderDiskPath);

                if (!Directory.Exists(fullDiskPathFolder))
                    Directory.CreateDirectory(fullDiskPathFolder);

                string fullFilePath = Path.Combine(fullDiskPathFolder,newFileName); //for save into file.//disk
                var relativeFileUrl = Path.Combine(relativeFolderUrl,newFileName); //for save in DB//web

                if (File.Exists(fullFilePath))
                    File.Delete(fullFilePath);

                Request.Files[0].SaveAs(fullFilePath);
                uploadFilePath.Add(fullFilePath);

                user.ProfilePictureImageUrl = relativeFileUrl;

                user.History += $"Student's Profile picture uploaded by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
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
        public HttpResult<User_AccountJson> SaveSendPassword([FromUri] int userId)
        {
            string error = string.Empty;
            string msg = string.Empty;
            var result = new HttpResult<User_AccountJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanSendEmail, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {

                if (!Facade.UserCredentialFacade.SendPasswordViaEmail(userId, out error))
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return result;
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
        #endregion

        #region Save StudentAccount
        [System.Web.Mvc.HttpPost]


        public HttpResult<UpdateStudentJson> SaveStudent(UpdateStudentJson applicantJson)
        {

            //1. Check user account is valid//done
            //2. check user student is valid//done
            //3. check user education is valid//done
            //4. check user addreess is valid//done
            //0. check given user name/classroll is valid//done
            //5. map user account and fill data
            // map user student and fill data
            // map user address and fill dada
            // map user education and fill data
            // save all and create student
            // generate admission payment
            // generate log of student add


            string error = string.Empty;
            var result = new HttpResult<UpdateStudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            long output = 0;
            long.TryParse(applicantJson.Id, out output);
            if (output <= 0)
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add("Invalid Student ID, Student not found!");
                return result;
            }
            if (output > 0)
            {
                var res = DbInstance.User_Account.Any(x => x.Id == output);
                if (!res)
                {
                    error = "Invalid Student ID, Student not found!";
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add(error);
                    return result;
                }
            }


            if (!IsValidToSaveAccountAndStudent(applicantJson, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            //check validate address
            if (applicantJson.PresentAddressJson == null)
            {
                result.HasError = true;
                result.Errors.Add("Applicant Present Address can't be empty!");
                return result;
            }
            if (applicantJson.PermanentAddressJson == null)
            {
                result.HasError = true;
                result.Errors.Add("Applicant Permanent Address can't be empty!");
                return result;
            }
            if (applicantJson.PresentAddressJson.CountryId == 0)
            {
                applicantJson.PresentAddressJson.CountryId = 1;
            }
            if (!IsValidToSaveContactAddress(applicantJson.PresentAddressJson, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            if (applicantJson.PermanentAddressJson.CountryId == 0)
            {
                applicantJson.PermanentAddressJson.CountryId = 1;
            }
            if (!IsValidToSaveContactAddress(applicantJson.PermanentAddressJson, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            //check validate education
            if (applicantJson.EducationJsonList == null)
            {
                result.HasError = true;
                result.Errors.Add("Applicant Previous Education can't be empty!");
                return result;
            }
            //if (applicantJson.EducationJsonList.Count <= 0)
            //{
            //    result.HasError = true;
            //    result.Errors.Add("Please Add at Least Two Previous Education!");
            //    return result;
            //}

            var entityReceivedEducationList = new List<User_Education>();

            foreach (var userEducation in applicantJson.EducationJsonList)
            {
                if (!IsValidToSaveEducation(userEducation, out error))
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return result;
                }

            }
            //saving separatly
            //if (applicantJson.ScholarshipJsonList != null && applicantJson.ScholarshipJsonList.Count > 0)
            //{
            //    foreach (var scholarship in applicantJson.ScholarshipJsonList)
            //    {
            //        if (scholarship.PercentAmount <= 0 || scholarship.RealAmount <= 0)
            //        {
            //            continue;
            //        }
            //        if (!IsValidToSaveStdScholarship(scholarship, out error))
            //        {
            //            result.HasError = true;
            //            result.Errors.Add(error);
            //            return result;
            //        }

            //    }
            //}



            /*todo check this code, create by mohitosh
                I have created entityReceivedEducationList and called Map function
                I have written some code in MapExtra function  \EMS.Web.Jsons\Models\User_EducationMapper.cs
                changed parameter type in SaveEducationLogic, User_EducationJson from User_Education
            */
            applicantJson.EducationJsonList.Map(entityReceivedEducationList);

            //validate userName/classroll/registrationNumber
            //if (applicantJson.IsAutoUserName && HttpUtil.DbContext.User_Account.Any(x => x.UserName == applicantJson.UserName))
            //{
            //    result.HasError = true;
            //    result.Errors.Add("User Name or Student ID Already exist!");
            //    ///return result;
            //}



            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    var userAccount = User_Account.GetNew();
                    if (!this.SaveAccountLogic(applicantJson, ref userAccount, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);

                        return result;
                    }
                    DbInstance.SaveChanges();
                    var userStudent = new User_Student();
                    if (!this.SaveStudentLogic(applicantJson, userAccount, ref userStudent, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);

                        return result;
                    }
                    //check type change
                    if (userStudent.EnrollmentStatus == User_Student.EnrollmentStatusEnum.AdmissionCancelled || userStudent.EnrollmentStatus == User_Student.EnrollmentStatusEnum.Applicant)
                    {
                        userAccount.UserTypeEnumId = (byte)User_Account.UserTypeEnum.Applicant;
                    }
                    else
                    {
                        userAccount.UserTypeEnumId = (byte)User_Account.UserTypeEnum.Student;
                    }

                    DbInstance.SaveChanges();

                    //var listContactAddress = new List<User_ContactAddress>();
                    //applicantJson.ContactAddressJsonList.Map(listContactAddress);
                    //userAccount.User_ContactAddress = listContactAddress;

                    //var listEducation = new List<User_Education>();
                    //applicantJson.EducationJsonList.Map(listEducation);
                    //userAccount.User_Education = listEducation;



                    if (!this.SaveContactAddressLogic(applicantJson.PermanentAddressJson, userAccount, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);

                        return result;
                    }
                    if (!this.SaveContactAddressLogic(applicantJson.PresentAddressJson, userAccount, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);

                        return result;
                    }
                    DbInstance.SaveChanges();
                    if (applicantJson.EducationJsonList != null && applicantJson.EducationJsonList.Count != 0)
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
                    //saving separatly
                    //if (applicantJson.ScholarshipJsonList != null && applicantJson.ScholarshipJsonList.Count > 0)
                    //{
                    //    foreach (var scholarship in applicantJson.ScholarshipJsonList)
                    //    {
                    //        if (scholarship.PercentAmount <= 0 || scholarship.RealAmount <= 0)
                    //        {
                    //            continue;
                    //        }
                    //        //new object but deleted in ui, not saving
                    //        if (scholarship.IsDeleted && scholarship.Id == 0)
                    //        {
                    //            continue;
                    //        }
                    //        if (!SaveStdScholarshipLogic(scholarship, userAccount, out error))
                    //        {
                    //            result.HasError = true;
                    //            result.Errors.Add(error);
                    //            return result;
                    //        }

                    //    }
                    //}

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
            result.Data = applicantJson;
            return result;
        }
        private bool SaveAccountLogic(UpdateStudentJson jsonObj, ref User_Account dbAddedObj, out string error)
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
                    error = "Given Student ID already exists, try new student ID!";
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

                //for new account
                //todo need to fix username generate logic
                //UserNameGenerator.GenerateStudentUserName(DateTime.Now, stdByExam.AdmissionTestRollNumber,User_Student.StudentQuotaEnum.General);



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

            if (objToSave.CampusId != jsonObj.CampusId && !isNewObject)
            {
                var oldCampusName = DbInstance.General_Campus.Where(x=>x.Id == objToSave.CampusId).Select(x=>x.ShortName).FirstOrDefault();

                var newCampusName = DbInstance.General_Campus.Where(x => x.Id == jsonObj.CampusId).Select(x => x.ShortName).FirstOrDefault(); ;

                if (!newCampusName.IsValid())
                {
                    error += "New Campus To be save cannot be found.";
                    return false;
                }

                #region Adding Log For UGC Unique Id Changing.

                var userProfileChangeAudit = User_ProfileChangeAudit.GetNew();

                // Here for department changing previous students user id will be considered as logged userID.

                userProfileChangeAudit.UserId = objToSave.Id;

                userProfileChangeAudit.FieldEnumId = (byte)User_ProfileChangeAudit.FieldEnum.CampusChange;

                var currentSemesterId =
                    DbInstance.Aca_Semester.Where(x => x.StatusEnumId == (byte)Aca_Semester.StatusEnum.Ongoing).Select(x => x.Id).FirstOrDefault();

                if (currentSemesterId <= 0)
                {
                    error += $"System does not have any ongoing semester. Please set a ongoing semester first.";
                    return false;
                }

                userProfileChangeAudit.SemesterId = currentSemesterId;

                userProfileChangeAudit.OldPk = objToSave.CampusId.ToString();
                userProfileChangeAudit.NewPk = jsonObj.CampusId.ToString();

                userProfileChangeAudit.OldValue = oldCampusName;
                userProfileChangeAudit.NewValue = newCampusName;

                if (!Facade.UserCredentialFacade.AddProfileChangeAudit(userProfileChangeAudit, out error))
                {
                    return false;
                }

                #endregion

                objToSave.History += $"Student's Campus Changed From {oldCampusName} to {newCampusName} by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yy h:mm:ss")}.<br/> ";
            }

            objToSave.CampusId = jsonObj.CampusId;// long.TryParse(newObj.CampusId, out output) ? output : 1;
            objToSave.Nationality = jsonObj.Nationality?.Trim() ?? jsonObj.Nationality;
            objToSave.NationalIdNumber = jsonObj.NationalIdNumber?.Trim() ?? jsonObj.NationalIdNumber;
            objToSave.BirthCertificateNumber = jsonObj.BirthCertificateNumber?.Trim() ?? jsonObj.BirthCertificateNumber;
            objToSave.BankAccountNo = jsonObj.BankAccountNo;
            objToSave.BankName = jsonObj.BankName;
            objToSave.PassportNo = jsonObj.PassportNo;

            //todo rechek the enum values
            objToSave.UserTypeEnumId = (int)User_Account.UserTypeEnum.Student;
            objToSave.UserStatusEnumId = jsonObj.UserStatusEnumId;
            objToSave.IsMigrate = objToSave.IsMigrate;
            objToSave.MigrationId = objToSave.MigrationId;
            objToSave.IsProfileCompleted = jsonObj.IsProfileCompleted;
            objToSave.IsApproved = jsonObj.IsApproved;

            //new col
            objToSave.IsUserCanEditProfile = jsonObj.IsUserCanEditProfile;
            objToSave.IsDocumentPending = jsonObj.IsDocumentPending;
            //objToSave.IsLockedOut = false;
            objToSave.LeavingDate = jsonObj.LeavingDate;
            objToSave.JoiningDate = jsonObj.JoiningDate;
            //objToSave.DepartmentId = jsonObj.DepartmentId;


            objToSave.FailedPasswordAttemptCount = jsonObj.FailedPasswordAttemptCount;
            objToSave.FailedPasswordAttemptWindowStart = jsonObj.FailedPasswordAttemptWindowStart;
            objToSave.FailedPasswordAnswerAttemptCount = jsonObj.FailedPasswordAnswerAttemptCount;
            objToSave.FailedPasswordAnswerAttemptWindowStart = jsonObj.FailedPasswordAnswerAttemptWindowStart;
            objToSave.Remarks = jsonObj.Remarks?.Trim() ?? jsonObj.Remarks;
            objToSave.LockReason = jsonObj.LockReason;

            //Ems-4 new column 
            objToSave.IsForceTwoFactorAuth = jsonObj.IsForceTwoFactorAuth;
            objToSave.ActiveRfidNo = jsonObj.ActiveRfidNo;

            objToSave.JoiningSemesterId = null;
            if (long.TryParse(jsonObj.JoiningSemesterId, out output))
            {
                objToSave.JoiningSemesterId = output;
            }

            //objToSave.RankId = null;
            objToSave.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = DateTime.Now;

            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }
        private bool SaveStudentLogic(UpdateStudentJson jsonObj, User_Account objAccount, ref User_Student dbAddedObj, out string error)
        {
            error = string.Empty;
            if (jsonObj == null)
            {
                error = "Student to save can't be null!";
                return false;
            }


            //if (!IsValidToSaveStudent(jsonObj, out error))
            //    return false;

            bool isNewObject = true;
            User_Student objToSave = null;

            if (objAccount.Id != 0)
            {
                objToSave = DbInstance.User_Student.SingleOrDefault(x => x.UserId == objAccount.Id);
                isNewObject = false;
            }
            //else
            //{
            //    objToSave.Id = BigInt.NewBigInt();
            //}

            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = User_Student.GetNew(objAccount.Id);
                DbInstance.User_Student.Add(objToSave);
                objToSave.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = DateTime.Now;

                objToSave.LevelTermId = 1;

                objToSave.CGPA = 0;

                objToSave.EnrollmentStatusEnumId = (byte)User_Student.EnrollmentStatusEnum.Continuing;
                objToSave.EnrollmentTypeEnumId = (byte)User_Student.EnrollmentTypeEnum.FreshStudent;

                objToSave.ClassRollNo = objAccount.UserName;
                objToSave.RegistrationNo = objAccount.UserName;
                objToSave.AdmissionTestRollNo = objAccount.UserName;
            }
            else
            {
                objToSave.ClassRollNo = jsonObj.ClassRollNo?.Trim() ?? jsonObj.ClassRollNo;
                objToSave.RegistrationNo = jsonObj.RegistrationNo?.Trim() ?? jsonObj.RegistrationNo;
                objToSave.AdmissionTestRollNo = jsonObj.AdmissionTestRollNo?.Trim() ?? jsonObj.AdmissionTestRollNo;
                objToSave.LevelTermId = jsonObj.LevelTermId;
            }


            long output;
            //objToSave.Id = long.TryParse(jsonObj.Id, out output) ? output : 0;
            objToSave.UserId = objAccount.Id;
            ////objToSave.ClassRollNo = jsonObj.ClassRollNo?.Trim() ?? jsonObj.ClassRollNo;
            ////objToSave.RegistrationNo = jsonObj.RegistrationNo?.Trim() ?? jsonObj.RegistrationNo;
            objToSave.RegistrationSession = jsonObj.RegistrationSession?.Trim() ?? jsonObj.RegistrationSession;
            objToSave.GradingPolicyId = jsonObj.GradingPolicyId;
            ////objToSave.CGPA = jsonObj.CGPA;
            ////objToSave.CourseCompleted = jsonObj.CourseCompleted;
            ////objToSave.CreditCompleted = jsonObj.CreditCompleted;
            ////objToSave.IncompleteCredits = jsonObj.IncompleteCredits;
            ////objToSave.LevelId = jsonObj.LevelId;
            ////objToSave.TermId = jsonObj.TermId;


            objToSave.ContinuingBatchId = jsonObj.ContinuingBatchId;//(Int64?)null;
            objToSave.ClassSectionId = jsonObj.ClassSectionId;

            //objToSave.ClassTimingGroupEnumId = jsonObj.ClassTimingGroupEnumId;
            //objToSave.ProgramId = jsonObj.ProgramId;// long.TryParse(jsonObj.ProgramId, out output) ? output : 0;
            //objToSave.FeeCodeId = jsonObj.FeeCodeId;
            //objToSave.CurriculumId = long.TryParse(jsonObj.CurriculumId, out output) ? output : 0;
            //objToSave.MajorCurriculumId = long.TryParse(jsonObj.MajorCurriculumId, out output) ? output : (Int64?)null;
            //objToSave.SecondMajorCurriculumId = long.TryParse(jsonObj.SecondMajorCurriculumId, out output) ? output : (Int64?)null;
            //objToSave.MinorCurriculumId = long.TryParse(jsonObj.MinorCurriculumId, out output) ? output : (Int64?)null;
            //objToSave.ElectiveCurriculumId = long.TryParse(jsonObj.ElectiveCurriculumId, out output) ? output : (Int64?)null;


            objToSave.StudentQuotaNameId = jsonObj.StudentQuotaNameId;
            objToSave.EnrollmentStatusEnumId = jsonObj.EnrollmentStatusEnumId;
            objToSave.EnrollmentTypeEnumId = jsonObj.EnrollmentTypeEnumId;
            objToSave.ParentsPrimaryJobTypeId = jsonObj.ParentsPrimaryJobTypeId;

            objToSave.FatherMobile = jsonObj.FatherMobile?.Trim() ?? jsonObj.FatherMobile;
            objToSave.MotherMobile = jsonObj.MotherMobile?.Trim() ?? jsonObj.MotherMobile;
            objToSave.AdmissionExamId = jsonObj.AdmissionExamId;
            ////objToSave.AdmissionTestRollNo = jsonObj.AdmissionTestRollNo?.Trim() ?? jsonObj.AdmissionTestRollNo;
            objToSave.AdmissionFormNo = jsonObj.AdmissionFormNo?.Trim() ?? jsonObj.AdmissionFormNo;
            objToSave.AdmissionStatusEnumId = jsonObj.AdmissionStatusEnumId;
            objToSave.AdmissionRemark = jsonObj.AdmissionRemark?.Trim() ?? jsonObj.AdmissionRemark;
            //objToSave.CertificateRegistrationNo = jsonObj.CertificateRegistrationNo?.Trim() ?? jsonObj.CertificateRegistrationNo;

            //new col
            objToSave.IsAdmissionFeePaid = jsonObj.IsAdmissionFeePaid;
            objToSave.IsFeesVerified = jsonObj.IsFeesVerified;
            objToSave.EnableOpenCreditRegistration = jsonObj.EnableOpenCreditRegistration;
            objToSave.ForceDisableOnlineRegistration = jsonObj.ForceDisableOnlineRegistration;

            //objToSave.ProvisionalCertificateIssueDate = jsonObj.ProvisionalCertificateIssueDate;
            //bjToSave.OriginalCertificateIssueDate = jsonObj.OriginalCertificateIssueDate;
            //objToSave.OriginalTranscriptIssueDate = jsonObj.OriginalTranscriptIssueDate;
            //objToSave.GraduationSemesterId = long.TryParse(jsonObj.GraduationSemesterId, out output) ? output : (Int64?)null;
            //objToSave.DegreeOptained = jsonObj.DegreeOptained;
            //objToSave.MajorMinorDegreeText = jsonObj.MajorMinorDegreeText;

            objToSave.CreditTransfer = jsonObj.CreditTransfer; //todo Need to Set read only after complete credit transfer Management 
            //objToSave.CreditWaiver = jsonObj.CreditWaiver;
            //objToSave.PerCreditFee = jsonObj.PerCreditFee;

            objToSave.LastChangedById = long.TryParse(jsonObj.LastChangedById, out output) ? output : 0;
            objToSave.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;



            //here save any child table
            return true;
        }


        private bool IsValidToSaveAccountAndStudent(UpdateStudentJson newObj, out string error)
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
            if (!newObj.IsAutoUserName && !newObj.UserName.IsValid())
            {
                error += "User Name can't blank.";
                return false;
            }
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
            //newObj.FullName = newObj.FullName.ValidateName();//added in create Student
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
                error = "Invalid Date of Birth, Student's age should be grater then 16y!";
                return false;
            }
            if (newObj.PlaceOfBirthCity.IsValid() && newObj.PlaceOfBirthCity.Length > 128)
            {
                error += "Place Of Birth City exceeded its maximum length 128.";
                return false;
            }

            if (!newObj.FatherName.IsValid())
            {
                //newObj.FatherName = "";
                error += "Father Name is not valid.";
                return false;
            }
            newObj.FatherName = newObj.FatherName.ValidateName();
            if (newObj.FatherName.Length > 128)
            {
                error += "Father Name exceeded its maximum length 128.";
                return false;
            }
            if (!newObj.MotherName.IsValid())
            {
                //newObj.MotherName = "";
                error += "Mother Name is not valid.";
                return false;
            }
            newObj.MotherName = newObj.MotherName.ValidateName();
            if (newObj.MotherName.Length > 128)
            {
                error += "Mother Name exceeded its maximum length 128.";
                return false;
            }
            if (newObj.SpouseName.IsValid() && newObj.SpouseName.Length > 128)
            {
                error += "Spouse Name exceeded its maximum length 128.";
                return false;
            }
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
                newObj.UserMobile2 = newObj.UserMobile2.ValidateMobile();// ValidateMobile();
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
            if (newObj.UserEmail.IsValid())
            {
                newObj.UserEmail = newObj.UserEmail.ToLower();
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
                newObj.EmergencyMobile = newObj.EmergencyMobile.ValidateMobile();//ValidateMobile();
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

            if (newObj.CreditTransfer == null)
            {
                error += "Credit Transfer required.";
                return false;
            }
            if (newObj.CreditWaiver == null)
            {
                error += "Credit Waiver required.";
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

            //===========================================User Student 
            //if (newObj.UserId <= 0)
            //{
            //    error += "Please select valid User.";
            //    return false;
            //}
            //if (!newObj.ClassRollNo.IsValid())
            //{
            //    error += "Class Roll No is not valid.";
            //    return false;
            //}
            //if (newObj.ClassRollNo.Length > 50)
            //{
            //    error += "Class Roll No exceeded its maximum length 50.";
            //    return false;
            //}
            if (newObj.RegistrationNo.IsValid() && newObj.RegistrationNo.Length > 50)
            {
                error += "Registration No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.RegistrationSession.IsValid() && newObj.RegistrationSession.Length > 50)
            {
                error += "Registration Session exceeded its maximum length 50.";
                return false;
            }
            if (newObj.GradingPolicyId <= 0)
            {
                error += "Please select valid Grading Policy.";
                return false;
            }
            if (newObj.CGPA == null)
            {
                error += "CGPA required.";
                return false;
            }
            if (newObj.CourseCompleted == null)
            {
                error += "Course Completed required.";
                return false;
            }
            if (newObj.CreditCompleted == null)
            {
                error += "Credit Completed required.";
                return false;
            }
            if (newObj.IncompleteCredits == null)
            {
                error += "Incomplete Credits required.";
                return false;
            }


            if (newObj.DepartmentId <= 0)
            {
                error += "Please select valid Department.";
                return false;
            }


            if (newObj.ProgramId <= 0)
            {
                error += "Please select valid Program.";
                return false;
            }
            if (newObj.ClassSectionId == null || newObj.ClassSectionId <= 0)
            {
                error += "Please select valid Class Section.";
                return false;
            }

            if (newObj.ClassTimingGroupEnumId == null)
            {
                error += "Please select valid Class Timing Group.";
                return false;
            }
            if (newObj.FeeCodeId <= 0)
            {
                error += "Please Select valid Fee Code.";
                return false;
            }
            if (!newObj.CurriculumId.IsValid())
            {
                error += "Please select valid Core Curriculum.";
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

            if (!newObj.JoiningDate.IsValid())
            {
                error += "Date Admitted is not valid.";
                return false;
            }
            if (newObj.LeavingDate != null && !newObj.LeavingDate.IsValid())
            {
                newObj.LeavingDate = null;//just put null,if its nullable and not valid.
                                          //error += "Date Graduated is not valid.";
                                          //return false;
            }

            if (newObj.ProvisionalCertificateIssueDate != null && !newObj.ProvisionalCertificateIssueDate.IsValid())
            {
                newObj.ProvisionalCertificateIssueDate = null;
            }

            if (newObj.OriginalCertificateIssueDate != null && !newObj.OriginalCertificateIssueDate.IsValid())
            {
                newObj.OriginalCertificateIssueDate = null;
            }

            if (newObj.OriginalTranscriptIssueDate != null && !newObj.OriginalTranscriptIssueDate.IsValid())
            {
                newObj.OriginalTranscriptIssueDate = null;
            }

            if (newObj.DegreeOptained != null && !newObj.DegreeOptained.IsValid())
            {
                newObj.DegreeOptained = null;
            }

            if (newObj.MajorMinorDegreeText != null && !newObj.MajorMinorDegreeText.IsValid())
            {
                newObj.MajorMinorDegreeText = null;
            }

            if (newObj.FatherMobile.IsValid() && newObj.FatherMobile.Length > 15)
            {
                error += "Father Mobile exceeded its maximum length 15.";
                return false;
            }
            if (newObj.MotherMobile.IsValid() && newObj.MotherMobile.Length > 15)
            {
                error += "Mother Mobile exceeded its maximum length 15.";
                return false;
            }
            //custom
            if (newObj.FatherMobile.IsValid())
            {
                newObj.FatherMobile = newObj.FatherMobile.ValidateMobile();// ValidateMobile();
                //error += "User Emergency Mobile can't be empty.";
                //return false;
                if (!newObj.FatherMobile.IsInt64())
                {
                    error = "User Father Mobile Should be only number!";
                    return false;
                }

                if (newObj.UserMobile.Equals(newObj.FatherMobile))
                {
                    error = "Father Mobile Number should not be same with User's Mobile Number!";
                    return false;
                }
            }
            if (newObj.MotherMobile.IsValid())
            {
                newObj.MotherMobile = newObj.MotherMobile.ValidateMobile();// ValidateMobile();
                //error += "User Emergency Mobile can't be empty.";
                //return false;
                if (!newObj.MotherMobile.IsInt64())
                {
                    error = "User Mother Mobile Should be only number!";
                    return false;
                }

                if (newObj.MotherMobile.Equals(newObj.FatherMobile))
                {
                    error = "Father's and Mother's Mobile Number should not be same!";
                    return false;
                }

                if (newObj.UserMobile.Equals(newObj.MotherMobile))
                {
                    error = "Mother Mobile Number should not be same with User's Mobile Number!";
                    return false;
                }
            }




            if (newObj.AdmissionTestRollNo.IsValid() && newObj.AdmissionTestRollNo.Length > 50)
            {
                error += "Admission Test Roll No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.AdmissionFormNo.IsValid() && newObj.AdmissionFormNo.Length > 50)
            {
                error += "Admission Form No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.AdmissionStatusEnumId == null)
            {
                error += "Please select valid Admission Status.";
                return false;
            }

            if (newObj.CertificateRegistrationNo.IsValid() && newObj.CertificateRegistrationNo.Length > 50)
            {
                error += "Certificate Registration No exceeded its maximum length 50.";
                return false;
            }


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
            string error = String.Empty;

            var result = new HttpListResult<User_EducationJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanView, out error))
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
                        result.Errors.Add("Invalid Student ID, please refresh the page the try again!");
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

        public HttpResult<User_EducationJson> GetNewEducationByUserId(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<User_EducationJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanAdd, out error))
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
            if (newObj.StudentRollOrIdNo.IsValid() && newObj.StudentRollOrIdNo.Length > 50)
            {
                error += "Student Roll Or Id No exceeded its maximum length 50.";
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
            string error = String.Empty;
            var result = new HttpResult<bool>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanDelete, out error))
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



        #region Admission Fee

        private bool AddAdmissionFee(User_Student student, out string error)
        {
            error = String.Empty;

            //Acnt_ParticularName.ParticularTypeEnum.AdmissionFee //0 transaction




            return true;
        }



        #endregion

    
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
                var userName = DbInstance.User_Account.FirstOrDefault(x => x.Id == userId)?.UserName;
                if (!userName.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("User not Found.");
                    return result;
                }

                using (UAC_LoginAuditDataService loginAuditDataService = new UAC_LoginAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<UAC_LoginAudit> query = DbInstance.UAC_LoginAudit
                        .Where(x=>x.UserId == userId || x.UserName.Trim() == userName.Trim())
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

        public HttpListResult<User_ProfileChangeAuditJson> GetDepartmentChangeAuditById(long userId)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_ProfileChangeAuditJson>();
            int count = 0;
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanViewLog, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                IQueryable<User_ProfileChangeAudit> query = DbInstance.User_ProfileChangeAudit
                      .Include(x => x.User_Account)
                      .Include(x => x.User_Account1)
                      .Include(x => x.Aca_Semester)
                      .Where(x => (x.UserId == userId || x.NewUserId == userId) 
                                  && x.FieldEnumId == (byte)User_ProfileChangeAudit.FieldEnum.DepartmentChange)
                      .OrderByDescending(x => x.SemesterId)
                      .ThenByDescending(x => x.Id);

                var entities = query.ToList();

                var distinctChangeUsersIds =
                    entities.DistinctBy(x => x.CreateById).Select(x => x.CreateById).ToList();

                var jsons = new List<User_ProfileChangeAuditJson>();
                entities.Map(jsons);

                #region Adding Changed User Name

                var userNameAndIdList = DbInstance.User_Account.Where(x => distinctChangeUsersIds.Contains(x.Id))
                    .Select(
                        x =>
                            new
                            {
                                UserId = x.Id.ToString(),
                                UserName = x.UserName,
                                FullName = x.FullName
                            }).ToList();

                foreach (var json in jsons)
                {
                    var user = userNameAndIdList.FirstOrDefault(x => x.UserId == json.CreateById);
                    json.ChangedByUser = $"{user.FullName} [{user.UserName}]";
                }

                #endregion

                result.Data = jsons;
                result.Count = count;
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