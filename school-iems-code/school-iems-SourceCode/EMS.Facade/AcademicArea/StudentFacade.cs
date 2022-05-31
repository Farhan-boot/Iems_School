using System;
using System.Data.Entity;
using System.Linq;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Settings;
using EMS.Framework.Utils;

namespace EMS.Facade.AcademicArea
{
    public class StudentFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;
        private User_AccountDataService accountService = null;
        private User_ContactAddressDataService contactAddressService = null;
        private User_ContactNumberDataService contactNumberService = null;
        private User_ContactEmailDataService contactEmailService = null;
        private User_EducationDataService educationService = null;
        private User_StudentDataService studentService = null;

        public StudentFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
            accountService = new User_AccountDataService(emsDbContext, usersProfile);
            contactAddressService = new User_ContactAddressDataService(emsDbContext, usersProfile);
            contactNumberService = new User_ContactNumberDataService(emsDbContext, usersProfile);
            contactEmailService = new User_ContactEmailDataService(emsDbContext, usersProfile);
            educationService = new User_EducationDataService(emsDbContext, usersProfile);
            studentService = new User_StudentDataService(emsDbContext, usersProfile);
        }

        //private string GenerateUsername(User_Account userAccount)
        //{
        //    var result = string.Empty;
        //    var userEmployee = DbInstance.User_Employee.SingleOrDefault(x => x.UserId == userAccount.Id);
        //    if (userEmployee != null)
        //    {
        //        result += userEmployee.JoiningDate.ToString("yy");//Joining Date Year 2 digit added
        //        result += userAccount.CategoryEnumId + "-";//User category 1=Army, 2=Navy, 3=AirForce, 4=Civil
        //        //TODO SN
        //        result += DbInstance.User_Employee.Count().ToStringPadLeft(4, '0') + "-";//Serial Number
        //        result += userEmployee.JobClassEnumId;//Job Class 1st,2nd,3rd,4th
        //    }
        //    return result;
        //}

        /// <summary>
        /// xxx-xxxx-x
        /// </summary>
        /// <param name = "userAccount" ></ param >
        /// < param name="result"></param>
        /// <returns></returns>
        /// /TODO copied from employee username generate.Implement for Student here.
        public bool GenerateStudentUserName(string semesterCode,string programCode, out string result,out string error)
        {
            result = string.Empty;
            error = string.Empty;

            if (!semesterCode.IsValid())
            {
                error = "Semester Code invalid, auto generate ID not possible";
                return false;
            }
            if (!programCode.IsValid())
            {
                error = "Program Code invalid, auto generate ID not possible";
                return false;
            }
            try
            {

                string usernameFrefix = $"{semesterCode.Trim()}{programCode.Trim()}";

                var userNameLike = $"{usernameFrefix}%"; //%4-%-1
                var query = $"SELECT * FROM [User_Account] WHERE [UserName] LIKE '{userNameLike}' ORDER BY [Id] DESC";

                var lastEmp = DbInstance.User_Account
                     .SqlQuery(query)
                    .FirstOrDefault(); //.ToList<User_Account>()

                if (lastEmp == null)
                {
                    result = $"{usernameFrefix}{("1").PadLeft(SiteSettings.Instance.StudentIdSerialDigitCount, '0')}";
                    return true;
                }
                string serial = 4 > lastEmp.UserName.Length ? lastEmp.UserName : lastEmp.UserName.Substring(lastEmp.UserName.Length - SiteSettings.Instance.StudentIdSerialDigitCount); //lastEmp.UserName.Split('-')[1];
                int serialNo = int.Parse(serial) + 1;
                //making more unique
                for (int i = 0; i < 100; i++)
                {
                    result = $"{usernameFrefix}{serialNo.ToStringPadLeft(SiteSettings.Instance.StudentIdSerialDigitCount, '0')}";
                    string s = result;
                    if (DbInstance.User_Account.Any(x => x.UserName.Equals(s, StringComparison.OrdinalIgnoreCase)))
                    {
                        serialNo++;//making unique
                    }
                    else
                    {
                        break;//break when no user found in db
                    }
                }

            }
            catch (Exception exception)
            {
                error = exception.ToString();
                return false;
            }
            return true;
        }
        public bool GenerateStudentUserNameAsUgc(string semesterCode, string programCode, out string result,out string error)
        {
            result = string.Empty;
            error = string.Empty;

            if (!SiteSettings.Instance.AddNewApplicantFormSettings.UgcVersityCode.IsValid())
            {
                error = "UGC University Code invalid, auto generate UGC unique ID not possible";
                return false;
            }

            if (!semesterCode.IsValid())
            {
                error = "UGC Semester Prefix invalid, auto generate UGC unique ID not possible";
                return false;
            }
            if (!programCode.IsValid())
            {
                error = "UGC Program Code invalid, auto generate UGC unique ID not possible";
                return false;
            }
            try
            {
                string ugcIdPrefix = $"{SiteSettings.Instance.AddNewApplicantFormSettings.UgcVersityCode}{semesterCode.Trim()}{programCode.Trim()}";

                var ugcIdLike = $"{ugcIdPrefix}%"; //%4-%-1
                var query = $"SELECT * FROM [User_Student] WHERE [UgcUniqueId] LIKE '{ugcIdLike}' ORDER BY [Id] DESC";

                var lastEmp = DbInstance.User_Student
                     .SqlQuery(query)
                    .FirstOrDefault(); //.ToList<User_Account>()

                if (lastEmp == null)
                {
                    result = $"{ugcIdPrefix}{("1").PadLeft(SiteSettings.Instance.StudentIdSerialDigitCount, '0')}";
                    return true;
                }
                string serial = 4 > lastEmp.UgcUniqueId.Length ? lastEmp.UgcUniqueId : lastEmp.UgcUniqueId.Substring(lastEmp.UgcUniqueId.Length - SiteSettings.Instance.StudentIdSerialDigitCount); //lastEmp.UserName.Split('-')[1];
                int serialNo = int.Parse(serial) + 1;
                //making more unique
                for (int i = 0; i < 100; i++)
                {
                    result = $"{ugcIdPrefix}{serialNo.ToStringPadLeft(SiteSettings.Instance.StudentIdSerialDigitCount, '0')}";
                    string s = result;
                    if (DbInstance.User_Student.Any(x => x.UgcUniqueId.Equals(s, StringComparison.OrdinalIgnoreCase)))
                    {
                        serialNo++;//making unique
                    }
                    else
                    {
                        break;//break when no user found in db
                    }
                }
            }
            catch (Exception exception)
            {
                error = exception.ToString();
                return false;
            }
            return true;
        }

        public bool GenerateStudentUserName2(User_Account userAccount, string prefix, string lastSerial, out string result)
        {
            result = string.Empty;

            if (userAccount == null)
            {
                result = "User can't be null!";
                return false;
            }
            try
            {
                var userEmployee = DbInstance.User_Employee.Include(x => x.User_Account).SingleOrDefault(x => x.UserId == userAccount.Id);
                if (userEmployee == null)
                {
                    result = "No employee found in employee database!";
                    return false;
                }
                var joiningYear = userEmployee.User_Account.JoiningDate.ToString("yy");//Joining Date Year 2 digit added
                var joiningMonth = userEmployee.User_Account.JoiningDate.ToString("MM"); ;//User category 1=Army, 2=Navy, 3=AirForce, 4=Civil
                var empClass = userEmployee.JobClassEnumId;//Job Class 1st,2nd,3rd,4th

                var userNameLike = $"%{joiningMonth}-%-{empClass}"; //%4-%-1
                var query = $"SELECT * FROM [User_Account] WHERE [UserName] LIKE '{userNameLike}' and id!={userAccount.Id} ORDER BY [Id] DESC";

                var lastEmp = DbInstance.User_Account
                     .SqlQuery(query)
                    .FirstOrDefault(); //.ToList<User_Account>()

                if (lastEmp == null)
                {
                    result = $"{joiningYear}{joiningMonth}-0001-{empClass}";
                    return true;
                }
                string serial = lastEmp.UserName.Split('-')[1];
                int serialNo = int.Parse(serial) + 1;
                //making more unique
                for (int i = 0; i < 100; i++)
                {
                    result = $"{joiningYear}{joiningMonth}-{serialNo.ToStringPadLeft(4, '0')}-{empClass}";
                    string s = result;
                    if (DbInstance.User_Account.Any(x => x.UserName.Equals(s, StringComparison.OrdinalIgnoreCase) && x.Id != userAccount.Id))
                    {
                        serialNo++;//making unique
                    }
                    else
                    {
                        break;//break when no user found in db
                    }
                }

            }
            catch (Exception exception)
            {
                result = exception.ToString();
                return false;
            }
            return true;
        }




        public bool SaveUserNameChangeAudit(string currentUserName, string newUserName, int userId, DbContextTransaction scope, out string error)
        {
            try
            {
                User_UserNameChangeAuditDataService usernameChangeAudit = new User_UserNameChangeAuditDataService(DbInstance, HttpUtil.Profile);
                User_UserNameChangeAudit audit = User_UserNameChangeAudit.GetNew(BigInt.NewBigInt());

                audit.UserId = userId;
                audit.OldUserName = currentUserName;
                audit.NewUserName = newUserName;

                return usernameChangeAudit.Save(audit, out error, scope);


            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        public bool SaveAccountOnly(User_Account obj, DbContextTransaction scope, out string error)
        {
            try
            {
                if (!accountService.Save(obj, out error, scope))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
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
            //if (newObj.AppartmentNo.IsValid() && newObj.AppartmentNo.Length > 50)
            //{
            //    error += "Apartment No exceeded its maximum length 50.";
            //    return false;
            //}
            //if (newObj.HouseNo.IsValid() && newObj.HouseNo.Length > 50)
            //{
            //    error += "House No exceeded its maximum length 50.";
            //    return false;
            //}
            //if (newObj.RoadNo.IsValid() && newObj.RoadNo.Length > 50)
            //{
            //    error += "Road No exceeded its maximum length 50.";
            //    return false;
            //}
            if (!newObj.Address1.IsValid())
            {
                error += "Address Line 1 Requred.";
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
            //if (newObj.IsMailingContact == null)
            //{
            //    error += "Is Mailing Contact required.";
            //    return false;
            //}
            if (newObj.IsValid == null)
            {
                error += "Is Valid required.";
                return false;
            }
            //if (newObj.PrivacyEnumId == null)
            //{
            //    error += "Please select valid Privacy.";
            //    return false;
            //}
            //TODO write your custom validation here.
            //var res =  DbInstance.User_ContactAddress.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A ContactAddress already exists!";
            //return false;
            //}
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
            //if (newObj.EducationTypeEnumId == null)
            //{
            //    error += "Please select valid Education Type.";
            //    return false;
            //}
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
                error += $"{newObj.DegreeEquivalent.ToString().AddSpacesToSentence()} Degree Title exceeded its maximum length 250.";
                return false;
            }
            //if (newObj.ConcentratedMajor.IsValid() && newObj.ConcentratedMajor.Length > 250)
            //{
            //    error += $"{newObj.DegreeEquivalent.ToString().AddSpacesToSentence()} Group exceeded its maximum length 250.";
            //    return false;
            //}
            if (!newObj.InstitutionName.IsValid())
            {
                error += $"{newObj.DegreeEquivalent.ToString().AddSpacesToSentence()} Institution Name is not valid.";
                return false;
            }
            if (newObj.InstitutionName.Length > 250)
            {
                error += $"{newObj.DegreeEquivalent.ToString().AddSpacesToSentence()} Institution Name exceeded its maximum length 250.";
                return false;
            }
            //if (newObj.Board.IsValid() && newObj.Board.Length > 250)
            //{
            //    error += $"{newObj.DegreeEquivalent.ToString().AddSpacesToSentence()} Board exceeded its maximum length 250.";
            //    return false;
            //}
            //if (!newObj.Result.IsValid())
            //{
            //    error += $"{newObj.DegreeEquivalent.ToString().AddSpacesToSentence()} Result is not valid.";
            //    return false;
            //}
      
            //if (newObj.Result.Length > 250)
            //{
            //    error += $"{newObj.DegreeEquivalent.ToString().AddSpacesToSentence()} Result exceeded its maximum length 250.";
            //    return false;
            //}
            //if (newObj.Duration.IsValid() && newObj.Duration.Length > 250)
            //{
            //    error += $"{newObj.DegreeEquivalent.ToString().AddSpacesToSentence()} Duration exceeded its maximum length 250.";
            //    return false;
            //}
            if (newObj.YearOfPassing == null)
            {
                error += "Year Of Passing required.";
                return false;
            }
            //if (!newObj.Session.IsValid())
            //{
            //    error += $"{newObj.DegreeEquivalent.ToString().AddSpacesToSentence()} Session is not valid.";
            //    return false;
            //}
            //if (newObj.Session.Length > 50)
            //{
            //    error += $"{newObj.DegreeEquivalent.ToString().AddSpacesToSentence()} Session exceeded its maximum length 50.";
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
        public bool SaveAccount(User_Account obj, out string error, DbContextTransaction scope)
        {
            try
            {
                var user = accountService.GetById(obj.Id);

                //not mapping from json, map 
                obj.Password = user.Password;
                obj.PasswordSalt = user.PasswordSalt;
                obj.FailedPasswordAnswerAttemptCount = user.FailedPasswordAnswerAttemptCount;
                obj.FailedPasswordAnswerAttemptWindowStart = user.FailedPasswordAnswerAttemptWindowStart;
                obj.FailedPasswordAttemptCount = user.FailedPasswordAttemptCount;
                obj.FailedPasswordAttemptWindowStart = user.FailedPasswordAttemptWindowStart;
                obj.LastLoginDate = user.LastLoginDate;
                obj.LastPasswordChangedDate = user.LastPasswordChangedDate;
                obj.LastLockoutDate = user.LastLockoutDate;

                if (!HttpUtil.Profile.UserName.Equals("psdeveloper", StringComparison.InvariantCultureIgnoreCase))
                {    //below objects to be remain as database whn saving other then psdeveloper
                   
                }
                //setting  fields are as it is data base
                if (HttpUtil.Profile.UserTypeId == (byte)User_Account.UserTypeEnum.Student)
                {
                    //obj.Prefix = user.Prefix;
                    //obj.Suffix = user.Suffix;
                    obj.UserName = user.UserName;
                    obj.UserTypeEnumId = user.UserTypeEnumId;
                    obj.UserStatusEnumId = user.UserStatusEnumId;
                    obj.LastLoginDate = user.LastLoginDate;
                    obj.LastPasswordChangedDate = user.LastPasswordChangedDate;
                    obj.Password = user.Password;
                    obj.PasswordSalt = user.PasswordSalt;
                    //obj.IsApproved = user.IsApproved;
                    //obj.IsLockedOut = user.IsLockedOut;
                    obj.LastLockoutDate = user.LastLockoutDate;
                    obj.FailedPasswordAttemptCount = user.FailedPasswordAttemptCount;
                    obj.FailedPasswordAttemptWindowStart = user.FailedPasswordAttemptWindowStart;
                    obj.FailedPasswordAnswerAttemptCount = user.FailedPasswordAnswerAttemptCount;
                    obj.FailedPasswordAnswerAttemptWindowStart = user.FailedPasswordAnswerAttemptWindowStart;
                    //obj.IsMigrate = user.IsMigrate; //dont do it 
                    obj.MigrationId = user.MigrationId;
                }

                if (!accountService.Save(obj, out error, scope))
                {
                    //scope.Rollback();
                    return false;
                }
                DbInstance.SaveChanges();
                if (obj.User_ContactAddress.Count != 0)
                {
                    foreach (var userContactAddress in obj.User_ContactAddress)
                    {
                        if (HttpUtil.Profile.UserTypeId == (byte)User_Account.UserTypeEnum.Student)
                        {
                            userContactAddress.IsValid = true;
                            //userContactAddress.IsMailingContact = true;
                            //userContactAddress.PrivacyEnumId = (byte) User_ContactEmail.PrivacyEnum.Public;

                        }
                        userContactAddress.UserId = obj.Id;
                        if (userContactAddress.CountryId==0)
                        {
                            userContactAddress.CountryId = 1;
                        }

                        //TODO Have to remove this check. (Temporary) //!HttpUtil.DbContext.UAC_RoleUserMap.Any(x => x.UserId == HttpUtil.Profile.UserId)
                        if (HttpUtil.Profile.UserTypeId == (byte)User_Account.UserTypeEnum.Student)
                        {
                            if (!IsValidToSaveContactAddress(userContactAddress, out error))
                            {
                                //scope.Rollback();
                                return false;
                            }
                        }
                        if (!contactAddressService.Save(userContactAddress, out error, scope))
                        {
                            //scope.Rollback();
                            return false;
                        }
                        DbInstance.SaveChanges();
                    }
                }
                if (obj.User_ContactNumber.Count != 0)
                {
                    foreach (var userContactNumber in obj.User_ContactNumber)
                    {
                        if (userContactNumber.ContactNumber.IsValid())
                        {
                            userContactNumber.UserId = obj.Id;
                            userContactNumber.CountryId = 1;
                            if (userContactNumber.CountryId == 0)
                            {
                                userContactNumber.CountryId = 1;
                            }

                            if (!contactNumberService.Save(userContactNumber, out error, scope))
                            {
                                //scope.Rollback();
                                return false;
                            }
                            DbInstance.SaveChanges();
                        }
                    }
                }
                if (obj.User_ContactEmail.Count != 0)
                {
                    foreach (var userContactEmail in obj.User_ContactEmail)
                    {
                       
                        if (userContactEmail.ContactEmail.IsValidEmail())
                        {
                            userContactEmail.ContactEmail = userContactEmail.ContactEmail.Replace(" ","").Trim();
                            if (HttpUtil.Profile.UserTypeId == (byte)User_Account.UserTypeEnum.Student)
                            {
                                //userContactEmail.IsPrimary = true;
                                //userContactEmail.IsActive = true;
                                userContactEmail.IsValid = true;
                                //userContactEmail.IsMailingContact = true;
                                userContactEmail.Privacy = User_ContactEmail.PrivacyEnum.Public;
                            }
                            userContactEmail.UserId = obj.Id;
                            if (!contactEmailService.Save(userContactEmail, out error, scope))
                            {
                                //scope.Rollback();
                                return false;
                            }
                            DbInstance.SaveChanges();
                        }
                    }
                }
                if (obj.User_Education.Count != 0)
                {
                    foreach (var userEducation in obj.User_Education)
                    {
                        if (HttpUtil.Profile.UserTypeId == (byte)User_Account.UserTypeEnum.Student)
                        {
                            //userEducation.Achievement = "";
                            //userEducation.Duration = "2 Year";
                            if (!IsValidToSaveEducation(userEducation, out error))
                            {
                                //scope.Rollback();
                                return false;
                            }
                        }
                        userEducation.UserId = obj.Id;
                        if (userEducation.DegreeTitle.IsValid())
                        {
                            if (!educationService.Save(userEducation, out error, scope))
                            {
                                //scope.Rollback();
                                return false;
                            }
                            DbInstance.SaveChanges();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //log error
                error = ex.ToString();
                //scope.Rollback();
                return false;
            }
        }
        public bool SaveStudent(User_Student obj, out string error, DbContextTransaction scope)
        {
            try
            {
                var dbStudent = DbInstance.User_Student.SingleOrDefault(x => x.Id == obj.Id);
                if (HttpUtil.Profile.UserTypeId == (byte)User_Account.UserTypeEnum.Student)
                {
                    //setting object to be remain as database whn saving by student
                    obj.UserId = dbStudent.UserId;
                    obj.AdmissionExamId = dbStudent.AdmissionExamId;
                    obj.EnrollmentStatusEnumId = dbStudent.EnrollmentStatusEnumId;
                    obj.EnrollmentTypeEnumId = dbStudent.EnrollmentTypeEnumId;
                   
                    obj.FeeCodeId = dbStudent.FeeCodeId;
                    obj.GradingPolicyId = dbStudent.GradingPolicyId;
                    obj.CGPA = dbStudent.CGPA;
                    obj.CreditCompleted = dbStudent.CreditCompleted;
                    //obj.DateAdmitted = dbStudent.DateAdmitted;
                    //obj.DateGraduated = dbStudent.DateGraduated;

                   
                    obj.CourseCompleted = dbStudent.CourseCompleted;
                    obj.IncompleteCredits = dbStudent.IncompleteCredits;
                    //obj.FatherId = dbStudent.FatherId;
                    //obj.MotherId = dbStudent.MotherId;
                    //obj.OtherParentId = dbStudent.OtherParentId;

                    obj.ElectiveCurriculumId = dbStudent.ElectiveCurriculumId;

                    //obj.DepartmentId = dbStudent.DepartmentId;
                    obj.ContinuingBatchId = dbStudent.ContinuingBatchId;
                    obj.ProgramId = dbStudent.ProgramId;

                    obj.CurriculumId = dbStudent.CurriculumId;
                    obj.ClassRollNo = dbStudent.ClassRollNo;
                  
                    obj.LevelTermId = dbStudent.LevelTermId;
                    //obj.TermId = dbStudent.TermId;
                }
                //todo only permitted administrator can change department
                if (!HttpUtil.Profile.UserName.Equals("psdeveloper",StringComparison.InvariantCultureIgnoreCase))
                {    //below objects to be remain as database whn saving other then psdeveloper
                    //obj.DepartmentId = dbStudent.DepartmentId;
                    obj.ProgramId = dbStudent.ProgramId;
                }

                if (!studentService.Save(obj, out error, scope))
                {
                    //scope.Rollback();
                    return false;
                }
                DbInstance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //log error
                error = ex.ToString();
                //scope.Rollback();
                return false;
            }
        }

    }
}
