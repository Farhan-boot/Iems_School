using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EMS.Communication.EmailProxy;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataMigration.Win.Data;
using EMS.Framework.Helpers;
using EMS.Framework.Objects;
using EMS.Framework.Utils;
using EMS.Framework.Settings;

namespace EMS.DataMigration.Win.Migrator
{
    public class UgMigrator
    {
        protected EmsDbContext emsDbDsc { get; }

        protected emsEntities admDbSrc { get; }

        protected UserProfile Profile { get; }
        public UgMigrator()
        {
            admDbSrc = new emsEntities();
            emsDbDsc = HttpUtil.DbContext;
            Profile = HttpUtil.GetTempFrofile();
        }
        public AdmUG_ExamSetting GetCurrentExamSetting()
        {
            if (!admDbSrc.AdmUG_ExamSetting.Any())
                return null;

            var exam = admDbSrc.AdmUG_ExamSetting.FirstOrDefault(e => e.CircularStatusId == (int)EnumCollection.AdmissionUG.CircularStatusEnum.Current) ??
                       admDbSrc.AdmUG_ExamSetting.OrderByDescending(e => e.ExamDate).FirstOrDefault();
            return exam;
        }

        private string eSubject = "Your Exam Entry Form is ready to download (Term-1 final exam 2016), please download.";

        public void EmailAccountInfoToAllUser()
        {
            var admCurrentExam = GetCurrentExamSetting();
            var studentList = emsDbDsc.User_Student.Include(x => x.User_Account).Include(x => x.User_Account.User_ContactEmail).Where(x => x.AdmissionExamSettingId == admCurrentExam.Id).OrderBy(x => x.AdmissionTestRollNo).ToList();

            string eSubject = "EMS Online Login Information for Student";
            string error = "";
            int cout = 1;

            foreach (var student in studentList)
            {
                if (student.User_Account.User_ContactEmail == null || student.User_Account.User_ContactEmail.Count < 1)
                    continue;
                var name = student.User_Account.FullName;
                var email = student.User_Account.User_ContactEmail.ToList()[0].ContactEmail;
                var username = student.User_Account.UserName;
                var password = student.User_Account.Password;

                if (SendNewAccoutEmail(name, eSubject, email, username, password, out error))
                {

                    Debug.WriteLine($"{cout.ToString()} Y. {username}");
                }
                else
                {
                    Debug.WriteLine($"{cout.ToString()} N.{username}");
                }
                cout++;

            }
        }
        public bool SendNewAccoutEmail(string name, string subject, string emailToAddress, string userName, string password, out string msg)
        {
            //if (IsDataValid())
            {
                string error = "";
                //string subject = eSubject;

                MailAddressCollection senderAddress = DefaultEmailSettings.GetSenderAddress(DefaultEmailSettings.Email_Address);

                //MailAddressCollection toAddress = EmailSettings.GetEmailToAddress(); ;

                MailAddress toAddress = new MailAddress(emailToAddress, name);

                string body = $"Hi {name},";

                body += $"\nWelcome to {SiteSettings.Instance.InstituteShortName} \"Education Management System\". Please preserve below information for farther use!";

                body +=  $"\n\nLogin URL: {SiteSettings.Instance.EmsLink}";;
                body += "\nUser Name: " + userName;
                body += "\nPassword: " + password;
               body += "\n\nPowered By";
                body += "\n" +SiteSettings.PoweredBy;
                body += "\nWeb: "+SiteSettings.CompanyWebsite;
                body += "\n---------------------------------------------\n ";
                body += "\nDO NOT reply to this email. This email is system generated.";
                body += $"\n{SiteSettings.PoweredBy} Automated Email Sender Service.";

                //body += "\nPhone: " + txtContactNo.Text;
                //body += "\nAddress: " + txtAddress.Text;

                bool result = false;
                using (EmailSender em = new EmailSender(DefaultEmailSettings.GetSmtpClientSettings()))
                {
                    result = em.SendMail(toAddress, subject, body, out error, senderAddress);
                }

                msg = error.ToString();
                return result;
                if (result)
                {
                    //ShowSuccess("Thank you, Email sent successfully...");
                    //txtCompany.Text = txtEmail.Text = txtMessage.Text = txtName.Text = txtSubject.Text = "";
                }
                else
                {
                    //Debug.WriteLine($"{totalStudentUpdated}. admRoll:\t{stdByExam.AdmissionTestRollNumber}\t ClassRoll:\t{stdByExam.ClassRollNo}.\r\n");
                    // ShowError("Email sending failed try again!");
                }
            }
        }


        #region Applicant By Exam
        public bool MigrateUgStdByExamToEMS(bool updateExisting, out string error)
        {

            error = null;
            var admCurrentExam = GetCurrentExamSetting();
            var stdByExamList = admDbSrc.AdmUG_Applicant.Include(x => x.AdmUG_ApplicantDeptAllocHistory).Include("AdmUG_ApplicantDeptAllocHistory.Department")
                .Where(x => x.ExamSettingId == admCurrentExam.Id && x.AdmissionStatusId == (int)EnumCollection.AdmissionUG.AdmissionStatusEnum.Admitted && x.PreRegistrationStatusId == 100).OrderBy(x => x.AdmissionTestRollNumber).ToList();

            var res = MigrateUgStdByExamToEMS(stdByExamList, updateExisting, out error);//UpdateParentsMobileUgStdByExamToEms(stdByExamList, updateExisting, out error); //
            return res;
        }
        public bool MigrateUgStdByExamByAdmRollToEMS(int admroll, bool updateExisting, out string error)
        {
            error = null;
            var admCurrentExam = GetCurrentExamSetting();
            var stdByExamList = admDbSrc.AdmUG_Applicant.Include(x => x.AdmUG_ApplicantDeptAllocHistory).Include("AdmUG_ApplicantDeptAllocHistory.Department")
                .Where(x => x.ExamSettingId == admCurrentExam.Id && x.AdmissionTestRollNumber == admroll && x.AdmissionStatusId == (int)EnumCollection.AdmissionUG.AdmissionStatusEnum.Admitted && x.PreRegistrationStatusId == 100).OrderBy(x => x.AdmissionTestRollNumber).ToList();

            var res = MigrateUgStdByExamToEMS(stdByExamList, updateExisting, out error);
            return res;
        }
        private bool ValidateApplicant(AdmUG_Applicant admApplicant, out string error)
        {
            error = null;
            return true;
        }
        public bool MigrateUgStdByExamToEMS(List<AdmUG_Applicant> stdByExamList, bool updateExisting, out string error)
        {
            error = string.Empty;
            string msg = string.Empty;
            if (stdByExamList == null || stdByExamList.Count < 1)
                return false;
            int totalStudentUpdated = 0;
            foreach (var stdByExam in stdByExamList)
            {
                if (ValidateApplicant(stdByExam, out error))
                {
                    using (var scope = emsDbDsc.Database.BeginTransaction())
                    {

                        try
                        {
                            bool isNewObject = false;
                            //take existing user
                            User_Account userToSave = emsDbDsc.User_Account.SingleOrDefault(x => x.Id == stdByExam.Id);
                            //not update existing std
                            if (userToSave != null && !updateExisting)
                            {
                                continue;
                            }
                            if (userToSave == null)
                            {
                                isNewObject = true;
                                userToSave = User_Account.GetNew(stdByExam.Id);

                                // DateTime.Now.ToString("yy") + "-" + stdByExam.AdmissionTestRollNumber.ToStringPadLeft(4, '0') + "-" + 1;//get from function
                                //if(isNewObject) //only set if new object
                                userToSave.Password = RandomPassword.GenerateOnlyNumber(4).ToString(); //generate password and add
                                userToSave.CreateDate = DateTime.Now;
                                userToSave.CreateById = Profile.UserId;

                                userToSave.UserStatusEnumId = (int)User_Account.StatusEnum.Active;

                                userToSave.FullName = stdByExam.Name.Trim();
                                userToSave.BanglaName = stdByExam.FullNameBangla.Trim();
                                userToSave.FatherName = stdByExam.HSC_FatherName;
                                userToSave.MotherName = stdByExam.HSC_MotherName;

                                userToSave.DateOfBirth = stdByExam.DateOfBirth < DateTimeHelper.SqlMinDateTime ? DateTimeHelper.SqlMinDateTime : stdByExam.DateOfBirth;
                                userToSave.IsApproved = true;
                                userToSave.IsMigrate = false;
                                userToSave.CampusId = 1;//MAIN
                            }
                            if (stdByExam.AdmissionTestRollNumber > 0)
                            {
                                userToSave.UserName = UserNameGenerator.GenerateStudentUserName(DateTime.Now, stdByExam.AdmissionTestRollNumber,
                              User_Student.StudentQuotaEnum.General);
                            }
                            userToSave.UserTypeEnumId = (int)User_Account.UserTypeEnum.Student;
                            //userToSave.PlaceOfBirthCity = stdByExam.AddressPermanentDistrict;
                            //userToSave.PlaceOfBirth = stdByExam.AddressPermanentPoliceStation;
                            userToSave.PlaceOfBirthCountryId = 1;//bangladesh
                            userToSave.Nationality = stdByExam.Nationality.Trim().ToLower().ToTitleCase();
                            userToSave.GenderEnumId = (byte)stdByExam.Gender;
                            userToSave.BloodGroupEnumId = (byte)stdByExam.BloodGroupId;
                            userToSave.MaritalStatusEnumId = (byte)stdByExam.MaritalStatusId;
                            userToSave.ReligionEnumId = (byte)stdByExam.ReligionId;
                            userToSave.NationalIdNumber = stdByExam.NationalId.ToString();
                            userToSave.BirthCertificateNumber = "";

                            userToSave.RankId = null;
                            userToSave.Category = User_Account.UserCategoryEnum.Civil;

                            userToSave.LastChanged = stdByExam.LastChanged;
                            userToSave.LastChangedById = stdByExam.LastChangedBy;


                            //
                            if (isNewObject)
                            {
                                emsDbDsc.User_Account.Add(userToSave);
                            }
                            // SaveStudent
                            if (!SaveStdByExam(stdByExam, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveAddress   
                            if (!SaveStdByExamPresentAddress(stdByExam, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            if (!SaveStdByExamPermanetAddress(stdByExam, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveFather
                            if (!SaveStdByExamFather(stdByExam, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveMother
                            if (!SaveStdByExamMother(stdByExam, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveOtherParent
                            //if (!SaveStdByExamOtherParent(stdByExam, out msg))
                            //{
                            //    error = msg;
                            //    scope.Rollback();
                            //    continue;
                            //}
                            // SaveEmail
                            if (!SaveStdByExamEmail(stdByExam, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveContactNumber
                            if (!SaveStdByExamContactNumber(stdByExam, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveSscEducation
                            if (!SaveStdByExamSscEducation(stdByExam, out error))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveHscEducation
                            if (!SaveStdByExamHscEducation(stdByExam, out error))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            emsDbDsc.SaveChanges();
                            scope.Commit();
                            totalStudentUpdated++;
                            Debug.WriteLine($"{totalStudentUpdated}. admRoll:\t{stdByExam.AdmissionTestRollNumber}\t ClassRoll:\t{stdByExam.ClassRollNo}.\r\n");

                        }
                        catch (Exception ex)
                        {
                            error += $"{stdByExam.Id}  user.{ex.ToString()}.\r\n";
                            Debug.WriteLine(error);
                            scope.Rollback();
                        }


                    }

                }
            }


            if (totalStudentUpdated > 0)
            {
                return true;
            }
            return false;
        }
        private bool SaveStdByExam(AdmUG_Applicant stdByExam, out string error)
        {
            error = null;

            //Student
            try
            {
                bool isNewObject = false;
                User_Student studentObj = emsDbDsc.User_Student.SingleOrDefault(x => x.UserId == stdByExam.Id);
                if (studentObj == null)
                {
                    isNewObject = true;
                    studentObj = User_Student.GetNew(stdByExam.Id); //stdByExam.Id
                    studentObj.CreateDate = DateTime.Now;
                    studentObj.CreateById = Profile.UserId;
                    //update once when creation
                    studentObj.UserId = stdByExam.Id;
                    if (stdByExam.ClassRollNo != null)
                        studentObj.ClassRollNo = stdByExam.ClassRollNo.IsNull() ? "0" : stdByExam.ClassRollNo.ToString(); //(int)stdByExam.ClassRollNo;
                    studentObj.GradeTypeEnumId = 0;
                    studentObj.FatherMobile = stdByExam.FathersMobile;
                    studentObj.MotherMobile = stdByExam.MothersMobile;
                    //studentObj.OtherParentMobile = stdByExam.LocalGuardianContactNo;
                    //studentObj.AdmissionExamSettingId = stdByExam.ExamSettingId;
                    studentObj.StudentQuotaEnumId = (byte)stdByExam.QuotaId;//this will as same as admission; (byte)User_Student.QuotaEnum.General;
                    studentObj.EnrollmentStatusEnumId = (byte)User_Student.EnrollmentStatusEnum.Continuing;
                    studentObj.EnrollmentTypeEnumId = (byte)User_Student.EnrollmentTypeEnum.FreshStudent;
                    studentObj.ClassSectionId = 1;//A
                }

                studentObj.AdmissionTestRollNo = stdByExam.AdmissionTestRollNumber.ToString();
                //studentObj.AdmissionTestRollNo = stdByExam.AdmissionTestRollNumber;
                //getting dept
                var dept = stdByExam.AdmUG_ApplicantDeptAllocHistory.OrderByDescending(x => x.CreateDate).FirstOrDefault();
                if (dept == null)
                {
                    error = "Student Dept. not found!";
                    return false;
                }
                studentObj.DepartmentId = dept.Department.DepartmentNo; //admDeptNo=ems Dept ID
                //getting bach
                //var acaDeptBatch = emsDbDsc.Aca_DeptBatch.SingleOrDefault(x => x.DepartmentId == studentObj.DepartmentId && x.Year == DateTime.Now.Year && x.IsNewBatch);
                //if (acaDeptBatch == null)
                //{
                //    error = "Student Batch not found!";
                //    return false;
                //}
                //studentObj.DeptBatchId = acaDeptBatch.Id;

                var program = emsDbDsc.Aca_Program.FirstOrDefault(x => x.ProgramTypeEnumId == (byte)Aca_Program.ProgramTypeEnum.Undergraduate && x.DepartmentId == studentObj.DepartmentId);
                if (program == null)
                {
                    error = "Degree Program not found!";
                    return false;
                }
                studentObj.ProgramId = program.Id;
                var curriculum = emsDbDsc.Aca_Curriculum.OrderByDescending(x => x.Year).FirstOrDefault(x => x.Aca_Program.ProgramTypeEnumId == (byte)Aca_Program.ProgramTypeEnum.Undergraduate && x.DepartmentId == studentObj.DepartmentId);
                if (curriculum == null)
                {
                    error = "Student Curriculum not found!";
                    return false;
                }
                studentObj.CurriculumId = curriculum.Id;
                //studentObj.RegistrationSession = $"{acaDeptBatch.Year - 1}-{acaDeptBatch.Year.ToString().Substring(2)}";
                studentObj.LevelId = 1;
                studentObj.TermId = 1;

                if (stdByExam.AdmissionDate != null) studentObj.DateAdmitted = (DateTime)stdByExam.AdmissionDate;

                studentObj.ParentsPrimaryJobTypeEnumId = (byte)stdByExam.ParentsPrimaryJobStatusId;

                studentObj.LastChanged = stdByExam.LastChanged;
                studentObj.LastChangedById = stdByExam.LastChangedBy;


                //studentObj.RegistrationNo = string.Empty;
                //studentObj.FeeCodeId = 0;
                //studentObj.CGPA = 0.0F;
                //studentObj.CreditCompleted = 0.0F;
                //studentObj.DateGraduated = null;
                //studentObj.GradeTypeEnumId = 0;
                //studentObj.CourseCompleted = 0.0F;
                //studentObj.IncompleteCredits = 0.0F;
                //studentObj.FatherId = null;
                //studentObj.MotherId = null;
                //studentObj.OtherParentId = null;


                if (isNewObject)
                {
                    emsDbDsc.User_Student.Add(studentObj);
                }
                //emsDbDsc.SaveChanges();
                //scope.Commit();
                return true;
            }
            catch (Exception ex)
            {
                error += stdByExam.Id + " student " + ex.Message;
                return false;
            }

        }
        //public bool UpdateParentsMobileUgStdByExamToEms(List<AdmUG_Applicant> stdByExamList, bool updateExisting, out string error)
        //{
        //    error = string.Empty;
        //    string msg = string.Empty;
        //    if (stdByExamList == null || stdByExamList.Count < 1)
        //        return false;
        //    int totalStudentUpdated = 0;
        //    foreach (var stdByExam in stdByExamList)
        //    {
        //        using (var scope = emsDbDsc.Database.BeginTransaction())
        //        {

        //            try
        //            {

        //                User_Student studentObj = emsDbDsc.User_Student.SingleOrDefault(x => x.UserId == stdByExam.Id);
        //                if (studentObj != null)
        //                {
        //                    studentObj.FatherMobile = stdByExam.FathersMobile;
        //                    studentObj.MotherMobile = stdByExam.MothersMobile;
        //                }

        //                emsDbDsc.SaveChanges();
        //                scope.Commit();
        //                totalStudentUpdated++;
        //                Debug.WriteLine($"{totalStudentUpdated}. admRoll:\t{stdByExam.AdmissionTestRollNumber}\t ClassRoll:\t{stdByExam.ClassRollNo}.\r\n");

        //            }
        //            catch (Exception ex)
        //            {
        //                error += $"{stdByExam.Id}  user.{ex.ToString()}.\r\n";
        //                scope.Rollback();
        //            }


        //        }
        //    }


        //    if (totalStudentUpdated > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //private bool SaveStdByExamOtherParent(AdmUG_Applicant stdByExam, out string error)
        //{
        //    error = null;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(stdByExam.HSC_FatherName))
        //        {
        //            bool isNewObject = false;
        //            User_GuardianName guardianName = emsDbDsc.User_GuardianName.SingleOrDefault(
        //                    x => x.UserId == stdByExam.Id);

        //            //if name found  need to update
        //            if (guardianName == null)
        //            {
        //                isNewObject = true;
        //                guardianName = new User_GuardianName();
        //                guardianName.Id = BigInt.NewBigInt();
        //                guardianName.UserId = stdByExam.Id;
        //            }

        //            guardianName.FatherName = stdByExam.HSC_FatherName;
        //            guardianName.MotherName = stdByExam.HSC_MotherName;
        //            guardianName.OtherParentsName = stdByExam.LocalGuardianName;

        //            if (isNewObject)
        //            {
        //                emsDbDsc.User_GuardianName.Add(guardianName);
        //            }


        //            //emsDbDsc.SaveChanges();
        //            //scope.Commit();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        error += $"{stdByExam.Id} contactEmail.{ex}";
        //        return false;
        //    }
        //    return false;
        //}

        private bool SaveStdByExamMother(AdmUG_Applicant stdByExam, out string msg)
        {
            msg = null;
            return true;
        }

        private bool SaveStdByExamFather(AdmUG_Applicant stdByExam, out string msg)
        {
            msg = null;

            return true;
        }

        private bool SaveStdByExamPresentAddress(AdmUG_Applicant stdByExam, out string error)
        {
            error = null;
            try
            {
                //if (!string.IsNullOrEmpty(stdByExam.hsc))
                {
                    bool isNewObject = false;
                    User_ContactAddress address =
                        emsDbDsc.User_ContactAddress.SingleOrDefault(x => x.UserId == stdByExam.Id && x.ContactAddressTypeEnumId == (byte)User_ContactAddress.ContactAddressTypeEnum.Present);
                    //if valid education found not update 
                    //if (address != null && address.AreaInfo.IsValid())
                    //{
                    //    return true;
                    //}
                    // if address found need to update
                    if (address == null)
                    {
                        isNewObject = true;
                        address = User_ContactAddress.GetNew(BigInt.NewBigInt());
                    }
                    address.UserId = stdByExam.Id;

                    //address.AppartmentNo = "";
                    //address.HouseNo = "";//need to print in admit card
                    //address.RoadNo = string.Empty;
                    //address.AreaInfo = stdByExam.AddressPresent.Replace("\r\n", " "); ;
                    address.PoliceStation = stdByExam.AddressPresentPoliceStation;
                    address.PostOffice = stdByExam.AddressPresentPoliceStation;
                    //address.PostCode = stdByExam.AddressPresentPostCode;
                    address.District = stdByExam.AddressPresentDistrict;
                    //address.CareOfPersonName = stdByExam.AddressPresentCareOf;
                    address.CountryId = 1;
                    address.ContactAddressTypeEnumId = (byte)User_ContactAddress.ContactAddressTypeEnum.Present;
                    //address.IsMailingContact = true;
                    address.IsValid = true;
                    //care of person name not found

                    address.CreateDate = DateTime.Now;
                    address.CreateById = Profile.UserId;
                    address.LastChanged = stdByExam.LastChanged;
                    address.LastChangedById = stdByExam.LastChangedBy;
                    if (isNewObject)
                    {
                        emsDbDsc.User_ContactAddress.Add(address);
                    }
                    //emsDbDsc.SaveChanges();
                    //scope.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"{stdByExam.Id} contactEmail.{ex}";
                return false;
            }

        }
        private bool SaveStdByExamPermanetAddress(AdmUG_Applicant stdByExam, out string error)
        {
            error = null;
            try
            {
                //if (!string.IsNullOrEmpty(stdByExam.hsc))
                {
                    bool isNewObject = false;
                    User_ContactAddress address =
                        emsDbDsc.User_ContactAddress.SingleOrDefault(x => x.UserId == stdByExam.Id && x.ContactAddressTypeEnumId == (byte)User_ContactAddress.ContactAddressTypeEnum.Permanent);
                    //if valid education found not update 
                    //if (address != null && address.AreaInfo.IsValid())
                    //{
                    //    return true;
                    //}
                    // if address found need to update
                    if (address == null)
                    {
                        isNewObject = true;
                        address = User_ContactAddress.GetNew(BigInt.NewBigInt());
                    }
                    address.UserId = stdByExam.Id;

                    //address.AppartmentNo = "";
                    //address.HouseNo = "";//need to print in admit card
                    //address.RoadNo = string.Empty;
                    //address.AreaInfo = stdByExam.AddressPermanent.Replace("\r\n", " ");
                    address.PoliceStation = stdByExam.AddressPermanentPoliceStation;
                    address.PostOffice = stdByExam.AddressPermanentPoliceStation;
                    //address.PostCode = stdByExam.AddressPermanentPostCode;
                    address.District = stdByExam.AddressPermanentDistrict;
                    address.CountryId = 1;// stdByExam.Nationality;
                    //address.CareOfPersonName = stdByExam.AddressPermanentCareOf;
                    address.ContactAddressTypeEnumId = (byte)User_ContactAddress.ContactAddressTypeEnum.Permanent;
                    //address.IsMailingContact = true;
                    address.IsValid = true;
                    //care of person name not found

                    address.CreateDate = DateTime.Now;
                    address.CreateById = Profile.UserId;
                    address.LastChanged = stdByExam.LastChanged;
                    address.LastChangedById = stdByExam.LastChangedBy;
                    if (isNewObject)
                    {
                        emsDbDsc.User_ContactAddress.Add(address);
                    }
                    //emsDbDsc.SaveChanges();
                    //scope.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"{stdByExam.Id} contactEmail.{ex}";
                return false;
            }
        }
        private bool SaveStdByExamEmail(AdmUG_Applicant stdByExam, out string error)
        {
            error = null;
            try
            {
                if (string.IsNullOrEmpty(stdByExam.Email))
                    return true;

                {

                    User_ContactEmail contactEmail =
                        emsDbDsc.User_ContactEmail.FirstOrDefault(x => x.UserId == stdByExam.Id);

                    // if email found not need to update
                    if (contactEmail != null && contactEmail.ContactEmail.IsValidEmail())
                    {
                        return true;
                    }
                    // if email found not need to update
                    if (contactEmail == null)
                    {
                        contactEmail = User_ContactEmail.GetNew(BigInt.NewBigInt());
                        contactEmail.CreateDate = DateTime.Now;
                        contactEmail.CreateById = Profile.UserId;
                    }
                    //if new object it will execute below lines

                    contactEmail.ContactEmail = stdByExam.Email.Trim();
                    contactEmail.ContactEmailTypeEnumId = (int)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail;
                    contactEmail.UserId = stdByExam.Id;

                    //contactEmail.IsPrimary = true;
                    //contactEmail.IsActive = true;
                    contactEmail.IsValid = true;
                    //contactEmail.IsMailingContact = true;
                    contactEmail.PrivacyEnumId = (byte)User_ContactEmail.PrivacyEnum.Public;


                    contactEmail.LastChanged = stdByExam.LastChanged;
                    contactEmail.LastChangedById = stdByExam.LastChangedBy;

                    emsDbDsc.User_ContactEmail.Add(contactEmail);

                    //emsDbDsc.SaveChanges();
                    //scope.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"{stdByExam.Id} contactEmail.{ex}";
                return false;
            }
            return false;
        }
        private bool SaveStdByExamContactNumber(AdmUG_Applicant stdByExam, out string error)
        {
            error = null;
            //contactNumber cellPhone
            try
            {
                if (string.IsNullOrEmpty(stdByExam.Mobile))
                    return true;
                {
                    stdByExam.Mobile = stdByExam.Mobile.Replace("-", "").Trim();

                    User_ContactNumber contactNumber = emsDbDsc.User_ContactNumber.FirstOrDefault(x => x.UserId == stdByExam.Id);
                    // if contact found for this user not update
                    if (contactNumber != null && contactNumber.ContactNumber.IsValid())
                    {
                        return true;
                    }
                    if (contactNumber == null)
                    {
                        contactNumber = User_ContactNumber.GetNew(BigInt.NewBigInt());
                    }
                    //if new object it will execute below lines

                    contactNumber.ContactNumber = stdByExam.Mobile;
                    if (stdByExam.Mobile.Length > 50)
                    {
                        contactNumber.ContactNumber = stdByExam.Mobile.Remove(51);
                    }
                    contactNumber.ContactNumberTypeEnumId = (int)User_ContactNumber.ContactNumberTypeEnum.PersonalMobile;
                    contactNumber.UserId = stdByExam.Id;

                    emsDbDsc.User_ContactNumber.Add(contactNumber);

                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"{stdByExam.Id} cellphone.{ex}";
                //richTextBox.Text = obj.Id + " cellphone " + ex.Message + richTextBox.Text;
                return false;
            }
            return false;
        }
        private bool SaveStdByExamSscEducation(AdmUG_Applicant stdByExam, out string error)
        {
            error = null;
            try
            {
                //if (!string.IsNullOrEmpty(stdByExam.ssc))
                {
                    bool isNewObject = false;
                    User_Education education =
                        emsDbDsc.User_Education.SingleOrDefault(x => x.UserId == stdByExam.Id && x.DegreeEquivalentEnumId == (byte)User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent);

                    //if valid education found not update 
                    //if (education != null && education.Result.IsValid())
                    //{
                    //    return true;
                    //}
                    // if degree found need to update
                    if (education == null)
                    {
                        isNewObject = true;
                        education = User_Education.GetNew(BigInt.NewBigInt());
                    }

                    education.UserId = stdByExam.Id;
                    //education.EducationTypeEnumId = (byte)stdByExam.ApplicantTypeId;
                    education.DegreeEquivalentEnumId = (byte)User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent;
                    education.DegreeTitle = User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent.ToString().AddAndSpacesToSentence();
                    //education.ConcentratedMajor = "";
                    education.InstitutionName = stdByExam.SSC_InstituteName;
                    //education.Board = stdByExam.SSC_Board;
                    //education.Result = stdByExam.SSC_GPA.ToString();
                    education.YearOfPassing = stdByExam.SSC_Year;
                    //education.Session = $"{stdByExam.SSC_Year - 2}-{stdByExam.SSC_Year - 1}";
                    //education.Duration = "2 Year";
                    education.CreateDate = DateTime.Now;
                    education.CreateById = Profile.UserId;
                    education.LastChanged = stdByExam.LastChanged;
                    education.LastChangedById = stdByExam.LastChangedBy;
                    if (isNewObject)
                    {
                        emsDbDsc.User_Education.Add(education);
                    }
                    //emsDbDsc.SaveChanges();
                    //scope.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"{stdByExam.Id} contactEmail.{ex}";
                return false;
            }
            return false;
        }
        private bool SaveStdByExamHscEducation(AdmUG_Applicant stdByExam, out string error)
        {
            error = null;
            try
            {
                //if (!string.IsNullOrEmpty(stdByExam.hsc))
                {
                    bool isNewObject = false;
                    User_Education education =
                        emsDbDsc.User_Education.SingleOrDefault(x => x.UserId == stdByExam.Id && x.DegreeEquivalentEnumId == (byte)User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent);
                    //if valid education found not update 
                    //if (education != null && education.Result.IsValid())
                    //{
                    //    return true;
                    //}
                    // if degree found need to update
                    if (education == null)
                    {
                        isNewObject = true;
                        education = User_Education.GetNew(BigInt.NewBigInt());
                    }
                    education.UserId = stdByExam.Id;
                    //education.EducationTypeEnumId = (byte)stdByExam.ApplicantTypeId;
                    education.DegreeEquivalentEnumId = (byte)User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent;
                    education.DegreeTitle = User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent.ToString().AddAndSpacesToSentence();
                    //education.ConcentratedMajor = "";
                    education.InstitutionName = stdByExam.HSC_InstituteName;
                    //education.Board = stdByExam.HSC_Board;
                    //education.Result = stdByExam.HSC_GPA.ToString();
                    education.YearOfPassing = stdByExam.HSC_Year;
                    //education.Session = $"{stdByExam.HSC_Year - 2}-{stdByExam.HSC_Year - 1}";
                    //education.Duration = "2 Year";
                    education.CreateDate = DateTime.Now;
                    education.CreateById = Profile.UserId;
                    education.LastChanged = stdByExam.LastChanged;
                    education.LastChangedById = stdByExam.LastChangedBy;
                    if (isNewObject)
                    {
                        emsDbDsc.User_Education.Add(education);
                    }
                    //emsDbDsc.SaveChanges();
                    //scope.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"{stdByExam.Id} contactEmail.{ex}";
                return false;
            }
            return false;
        }


        #endregion


        #region Applicant By Rule

        private bool IsValidateApplicant(AdmUG_ApplicantByRule admApplicant, out string error)
        {
            error = null;
            return true;
        }
        public bool MigrateUgStdByRuleToEMS(bool updateExisting, out string error)
        {
            error = null;
            var admCurrentExam = GetCurrentExamSetting();
            var stdByRuleList = admDbSrc.AdmUG_ApplicantByRule.Include(x => x.Department)
                .Where(x => x.ExamSettingId == admCurrentExam.Id && x.AdmissionStatusId == (int)EnumCollection.AdmissionUG.AdmissionStatusEnum.Admitted && x.PreRegistrationStatusId == 100  ).OrderBy(x => x.RollNo).ToList();

            var res = MigrateUgStdByRuleToEMS(stdByRuleList, false, out error); // UpdateParentsMobileUgStdByRuleToEms(stdByRuleList, updateExisting, out error); //  && x.QuotaId == (int)EnumCollection.QuotaByRuleEnum.MBO
            return res;
        }

        public bool MigrateUgStdByRuleToEMS(List<AdmUG_ApplicantByRule> stdByRuleList, bool updateExisting, out string error)
        {
            error = string.Empty;
            string msg = string.Empty;
            if (stdByRuleList == null || stdByRuleList.Count < 1)
                return false;
            int totalStudentUpdated = 0;
            //Parallel.ForEach(stdByRuleList, (stdByRule) =>
            //{ });


            foreach (var stdByRule in stdByRuleList)
            {
                if (IsValidateApplicant(stdByRule, out error))
                {
                    using (var scope = emsDbDsc.Database.BeginTransaction())
                    {

                        try
                        {
                            bool isNewObject = false;
                            //take existing user
                            User_Account userToSave = emsDbDsc.User_Account.SingleOrDefault(x => x.Id == stdByRule.Id);
                            //not update existing std
                            if (userToSave != null && !updateExisting)
                            {
                                continue;
                            }
                            if (userToSave == null)
                            {
                                isNewObject = true;
                                userToSave = User_Account.GetNew(stdByRule.Id);
                                userToSave.Password = RandomPassword.GenerateOnlyNumber(4).ToString(); //generate password and add
                                userToSave.CreateDate = DateTime.Now;
                                userToSave.CreateById = Profile.UserId;
                                userToSave.FullName = stdByRule.Name.Trim();
                                userToSave.BanglaName = stdByRule.FullNameBangla.Trim();
                                userToSave.FatherName = stdByRule.FathersName;
                                userToSave.MotherName = stdByRule.MothersName;
                                userToSave.IsApproved = true;
                                userToSave.IsMigrate = false;
                                userToSave.CampusId = 1;//mist
                                userToSave.UserStatusEnumId = (int)User_Account.StatusEnum.Active;
                            }
                            //if (stdByRule.RollNo > 0)
                            //{
                            //    userToSave.UserName = UserNameGenerator.GenerateStudentUserName(DateTime.Now, stdByRule.RollNo,
                            //     User_Student.GetQuotaByQuotaByRule(stdByRule.Quota));
                            //}
                            //userToSave.IsMilitary = stdByRule.UserCategory != User_Account.UserCategoryEnum.Civil;
                            userToSave.CategoryEnumId = (byte)User_Account.UserCategoryEnum.Civil;
                            if (stdByRule.UserCategory != User_Account.UserCategoryEnum.Civil)
                            {
                                //userToSave.RankId = stdByRule.MilitaryRankId;
                                userToSave.CategoryEnumId = (byte)stdByRule.UserCategory;
                            }
                            userToSave.UserTypeEnumId = (int)User_Account.UserTypeEnum.Student;

                            //userToSave.SpouseName = stdByRule.name;
                            userToSave.DateOfBirth = stdByRule.DateOfBirth < DateTimeHelper.SqlMinDateTime ? DateTimeHelper.SqlMinDateTime : stdByRule.DateOfBirth;

                            //userToSave.PlaceOfBirthCity = stdByRule.AddressPermanentDistrict;
                            //userToSave.PlaceOfBirth = stdByRule.AddressPermanentPoliceStation;
                            userToSave.PlaceOfBirthCountryId = stdByRule.AddressPermanentCountryId;//Bangladesh

                            userToSave.GenderEnumId = (byte)stdByRule.GenderId;
                            userToSave.BloodGroupEnumId = (byte)stdByRule.BloodGroupId;
                            userToSave.MaritalStatusEnumId = (byte)stdByRule.MaritalStatusId;
                            userToSave.ReligionEnumId = (byte)stdByRule.ReligionId;
                            userToSave.Nationality = stdByRule.Nationality.Trim().ToLower().ToTitleCase();
                            userToSave.NationalIdNumber = stdByRule.NationalId.ToString();
                            userToSave.BirthCertificateNumber = "";
                            //todo need to set from db

                            userToSave.LastChanged = stdByRule.LastChanged;
                            userToSave.LastChangedById = stdByRule.LastChangedBy;
                            if (isNewObject)
                            {
                                emsDbDsc.User_Account.Add(userToSave);
                            }
                            // SaveStudent
                            if (!SaveStdByRule(stdByRule, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveAddress   
                            if (!SaveStdByRulePresentAddress(stdByRule, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            if (!SaveStdByRulePermanetAddress(stdByRule, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveFather
                            if (!SaveStdByRuleFather(stdByRule, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveMother
                            if (!SaveStdByRuleMother(stdByRule, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveOtherParent
                            //if (!SaveStdByRuleOtherParent(stdByRule, out msg))
                            //{
                            //    error = msg;
                            //    scope.Rollback();
                            //    continue;
                            //}
                            // SaveEmail
                            if (!SaveStdByRuleEmail(stdByRule, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveContactNumber
                            if (!SaveStdByRuleContactNumber(stdByRule, out msg))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveSscEducation
                            if (!SaveStdByRuleSscEducation(stdByRule, out error))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }
                            // SaveHscEducation
                            if (!SaveStdByRuleHscEducation(stdByRule, out error))
                            {
                                error = msg;
                                scope.Rollback();
                                continue;
                            }

                            emsDbDsc.SaveChanges();
                            scope.Commit();
                            totalStudentUpdated++;
                            Debug.WriteLine($"{totalStudentUpdated}. admRoll:\t{stdByRule.RollNo}\t ClassRoll:\t{stdByRule.ClassRollNo}.\r\n");

                        }
                        catch (Exception ex)
                        {
                            error += $"{stdByRule.Id}  user.{ex.ToString()}.\r\n";
                            Debug.WriteLine($"Error: admRoll:\t{stdByRule.RollNo}.\r\n" + ex.GetBaseException());
                            scope.Rollback();
                        }


                    }

                }
            }



            if (totalStudentUpdated > 0)
            {
                return true;
            }
            return false;
        }
        private bool SaveStdByRule(AdmUG_ApplicantByRule stdByRule, out string error)
        {
            error = null;

            //Student
            try
            {
                bool isNewObject = false;
                User_Student studentObj = emsDbDsc.User_Student.SingleOrDefault(x => x.UserId == stdByRule.Id);
                if (studentObj == null)
                {
                    isNewObject = true;
                    studentObj = User_Student.GetNew(stdByRule.Id);
                    studentObj.CreateDate = DateTime.Now;
                    studentObj.CreateById = Profile.UserId;
                    if (stdByRule.ClassRollNo != null)
                        studentObj.ClassRollNo = stdByRule.ClassRollNo.IsNull()?"0": stdByRule.ClassRollNo.ToString();

                    studentObj.FatherMobile = stdByRule.FathersMobile;
                    studentObj.MotherMobile = stdByRule.MothersMobile;
                    //studentObj.OtherParentMobile = stdByRule.LocalGuardianContactNo;
                    studentObj.UserId = stdByRule.Id;
                    studentObj.EnrollmentStatusEnumId = (byte)User_Student.EnrollmentStatusEnum.Continuing;
                    studentObj.EnrollmentTypeEnumId = (byte)User_Student.EnrollmentTypeEnum.FreshStudent;
                    studentObj.ClassSectionId = 1;//A
                }

                studentObj.AdmissionTestRollNo = stdByRule.RollNo.ToString();
                //getting dept

                if (stdByRule.Department != null)
                    studentObj.DepartmentId = stdByRule.Department.DepartmentNo;// admDeptNo = ems Dept ID
                //getting bach
                //var acaDeptBatch = emsDbDsc.Aca_DeptBatch.SingleOrDefault(x => x.DepartmentId == studentObj.DepartmentId && x.Year == DateTime.Now.Year && x.IsNewBatch);
                if (acaDeptBatch == null)
                {
                    error = "Student Batch not found!";
                    return false;
                }
                studentObj.DeptBatchId = acaDeptBatch.Id;

                studentObj.ProgramId = emsDbDsc.Aca_Program.FirstOrDefault(x => x.ProgramTypeEnumId == (byte)Aca_Program.ProgramTypeEnum.Undergraduate && x.DepartmentId == studentObj.DepartmentId).Id;
                studentObj.CurriculumId = emsDbDsc.Aca_Curriculum.OrderByDescending(x => x.Year).FirstOrDefault(x => x.Aca_Program.ProgramTypeEnumId == (byte)Aca_Program.ProgramTypeEnum.Undergraduate && x.DepartmentId == studentObj.DepartmentId).Id;
                studentObj.GradeTypeEnumId = 0;



                studentObj.AdmissionTestRollNo = stdByRule.RollNo.ToString();
                studentObj.AdmissionExamSettingId = (int)stdByRule.ExamSettingId;

                //studentObj.StudentQuotaEnumId = (byte)User_Student.GetQuotaByQuotaByRule(stdByRule.Quota);//this will as same as admission; (byte)User_Student.QuotaEnum.General;

                studentObj.ParentsPrimaryJobTypeEnumId = (byte)stdByRule.ParentsPrimaryJobStatusId;




                studentObj.RegistrationSession = $"{acaDeptBatch.Year - 1}-{acaDeptBatch.Year.ToString().Substring(2)}";
                studentObj.LevelId = 1;
                studentObj.TermId = 1;

                if (stdByRule.AdmissionDate != null) studentObj.DateAdmitted = (DateTime)stdByRule.AdmissionDate;

                studentObj.LastChanged = stdByRule.LastChanged;
                studentObj.LastChangedById = stdByRule.LastChangedBy;

                if (isNewObject)
                {
                    emsDbDsc.User_Student.Add(studentObj);
                }
                //emsDbDsc.SaveChanges();
                //scope.Commit();
                return true;
            }
            catch (Exception ex)
            {
                error += stdByRule.Id + " student " + ex.Message;
                return false;
            }

        }

        //public bool UpdateParentsMobileUgStdByRuleToEms(List<AdmUG_ApplicantByRule> stdByExamList, bool updateExisting, out string error)
        //{
        //    error = string.Empty;
        //    string msg = string.Empty;
        //    if (stdByExamList == null || stdByExamList.Count < 1)
        //        return false;
        //    int totalStudentUpdated = 0;
        //    foreach (var stdByExam in stdByExamList)
        //    {
        //        using (var scope = emsDbDsc.Database.BeginTransaction())
        //        {

        //            try
        //            {

        //                User_Student studentObj = emsDbDsc.User_Student.SingleOrDefault(x => x.UserId == stdByExam.Id);
        //                if (studentObj != null)
        //                {
        //                    studentObj.FatherMobile = stdByExam.FathersMobile;
        //                    studentObj.MotherMobile = stdByExam.MothersMobile;
        //                }

        //                emsDbDsc.SaveChanges();
        //                scope.Commit();
        //                totalStudentUpdated++;
        //                Debug.WriteLine($"{totalStudentUpdated}. admRoll:\t{stdByExam.RollNo}\t ClassRoll:\t{stdByExam.ClassRollNo}.\r\n");

        //            }
        //            catch (Exception ex)
        //            {
        //                error += $"{stdByExam.Id}  user.{ex.ToString()}.\r\n";
        //                scope.Rollback();
        //            }


        //        }
        //    }


        //    if (totalStudentUpdated > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private bool SaveStdByRuleOtherParent(AdmUG_ApplicantByRule stdByRule, out string error)
        //{
        //    error = null;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(stdByRule.FathersName))
        //        {
        //            bool isNewObject = false;
        //            User_GuardianName guardianName = emsDbDsc.User_GuardianName.SingleOrDefault(
        //                    x => x.UserId == stdByRule.Id);

        //            //if name found  need to update
        //            if (guardianName == null)
        //            {
        //                isNewObject = true;
        //                guardianName = new User_GuardianName();
        //                guardianName.Id = BigInt.NewBigInt();
        //                guardianName.UserId = stdByRule.Id;
        //            }

        //            guardianName.FatherName = stdByRule.FathersName;
        //            guardianName.MotherName = stdByRule.MothersName;
        //            guardianName.OtherParentsName = stdByRule.LocalGuardianName;

        //            if (isNewObject)
        //            {
        //                emsDbDsc.User_GuardianName.Add(guardianName);
        //            }


        //            //emsDbDsc.SaveChanges();
        //            //scope.Commit();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        error += $"{stdByRule.Id} contactEmail.{ex}";
        //        return false;
        //    }
        //    return false;
        //}

        private bool SaveStdByRuleMother(AdmUG_ApplicantByRule stdByRule, out string msg)
        {
            msg = null;
            return true;
        }

        private bool SaveStdByRuleFather(AdmUG_ApplicantByRule stdByRule, out string msg)
        {
            msg = null;
            return true;
        }



        private bool SaveStdByRulePresentAddress(AdmUG_ApplicantByRule stdByRule, out string error)
        {
            error = null;
            try
            {
                //if (!string.IsNullOrEmpty(stdByRule.hsc))
                {
                    bool isNewObject = false;
                    User_ContactAddress address =
                        emsDbDsc.User_ContactAddress.SingleOrDefault(x => x.UserId == stdByRule.Id && x.ContactAddressTypeEnumId == (byte)User_ContactAddress.ContactAddressTypeEnum.Present);
                    //if valid education found not update 
                    //if (address != null && address.AreaInfo.IsValid())
                    //{
                    //    return true;
                    //}
                    // if address found need to update
                    if (address == null)
                    {
                        isNewObject = true;
                        address = User_ContactAddress.GetNew(BigInt.NewBigInt());
                    }
                    address.UserId = stdByRule.Id;

                    //address.AppartmentNo = "";
                    //address.HouseNo = "";//need to print in admit card
                    //address.RoadNo = string.Empty;
                    //address.AreaInfo = stdByRule.AddressPresent.Replace("\r\n", " "); ;
                    address.PoliceStation = stdByRule.AddressPresentPoliceStation;
                    address.PostOffice = stdByRule.AddressPresentPoliceStation;
                    //address.PostCode = stdByRule.AddressPresentPostCode;
                    address.District = stdByRule.AddressPresentDistrict;
                    //address.CareOfPersonName = stdByRule.AddressPresentCareOf;
                    address.CountryId = 1;
                    address.ContactAddressTypeEnumId = (byte)User_ContactAddress.ContactAddressTypeEnum.Present;
                    //address.IsMailingContact = true;
                    address.IsValid = true;
                    //care of person name not found

                    address.CreateDate = DateTime.Now;
                    address.CreateById = Profile.UserId;
                    address.LastChanged = stdByRule.LastChanged;
                    address.LastChangedById = stdByRule.LastChangedBy;
                    if (isNewObject)
                    {
                        emsDbDsc.User_ContactAddress.Add(address);
                    }
                    //emsDbDsc.SaveChanges();
                    //scope.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"{stdByRule.Id} contactEmail.{ex}";
                return false;
            }

        }
        private bool SaveStdByRulePermanetAddress(AdmUG_ApplicantByRule stdByRule, out string error)
        {
            error = null;
            try
            {
                //if (!string.IsNullOrEmpty(stdByRule.hsc))
                {
                    bool isNewObject = false;
                    User_ContactAddress address =
                        emsDbDsc.User_ContactAddress.SingleOrDefault(x => x.UserId == stdByRule.Id && x.ContactAddressTypeEnumId == (byte)User_ContactAddress.ContactAddressTypeEnum.Permanent);
                    //if valid education found not update 
                    //if (address != null && address.AreaInfo.IsValid())
                    //{
                    //    return true;
                    //}
                    // if address found need to update
                    if (address == null)
                    {
                        isNewObject = true;
                        address = User_ContactAddress.GetNew(BigInt.NewBigInt());
                    }
                    address.UserId = stdByRule.Id;

                    //address.AppartmentNo = "";
                    //address.HouseNo = "";//need to print in admit card
                    //address.RoadNo = string.Empty;
                    //address.AreaInfo = stdByRule.AddressPermanent.Replace("\r\n", " "); ;
                    address.PoliceStation = stdByRule.AddressPermanentPoliceStation;
                    address.PostOffice = stdByRule.AddressPermanentPoliceStation; ;
                    //address.PostCode = stdByRule.AddressPermanentPostCode;
                    address.District = stdByRule.AddressPermanentDistrict;
                    address.CountryId = 1;// stdByRule.Nationality;
                    //address.CareOfPersonName = stdByRule.AddressPermanentCareOf;
                    address.ContactAddressTypeEnumId = (byte)User_ContactAddress.ContactAddressTypeEnum.Permanent;
                    //address.IsMailingContact = true;
                    address.IsValid = true;
                    //care of person name not found

                    address.CreateDate = DateTime.Now;
                    address.CreateById = Profile.UserId;
                    address.LastChanged = stdByRule.LastChanged;
                    address.LastChangedById = stdByRule.LastChangedBy;
                    if (isNewObject)
                    {
                        emsDbDsc.User_ContactAddress.Add(address);
                    }
                    //emsDbDsc.SaveChanges();
                    //scope.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"{stdByRule.Id} contactEmail.{ex}";
                return false;
            }
        }
        private bool SaveStdByRuleEmail(AdmUG_ApplicantByRule stdByRule, out string error)
        {
            error = null;
            try
            {
                if (string.IsNullOrEmpty(stdByRule.Email))
                    return true;

                {

                    User_ContactEmail contactEmail =
                        emsDbDsc.User_ContactEmail.SingleOrDefault(
                            x => x.ContactEmail.Trim() == stdByRule.Email.Trim() && x.UserId == stdByRule.Id);

                    // if email found not need to update
                    if (contactEmail != null && contactEmail.ContactEmail.IsValidEmail())
                    {
                        return true;
                    }
                    //if new object it will execute below lines
                    if (contactEmail == null)
                    {
                        contactEmail = User_ContactEmail.GetNew(BigInt.NewBigInt());
                        contactEmail.CreateDate = DateTime.Now;
                        contactEmail.CreateById = Profile.UserId;
                    }
                    contactEmail.ContactEmail = stdByRule.Email.Trim();
                    contactEmail.ContactEmailTypeEnumId = (int)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail;
                    contactEmail.UserId = stdByRule.Id;
                    //contactEmail.IsPrimary = true;
                    //contactEmail.IsActive = true;
                    contactEmail.IsValid = true;
                    //contactEmail.IsMailingContact = true;
                    contactEmail.PrivacyEnumId = (byte)User_ContactEmail.PrivacyEnum.Public;

                    contactEmail.LastChanged = stdByRule.LastChanged;
                    contactEmail.LastChangedById = stdByRule.LastChangedBy;

                    emsDbDsc.User_ContactEmail.Add(contactEmail);

                    //emsDbDsc.SaveChanges();
                    //scope.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"{stdByRule.Id} contactEmail.{ex}";
                return false;
            }
            return false;
        }
        private bool SaveStdByRuleContactNumber(AdmUG_ApplicantByRule stdByRule, out string error)
        {
            error = null;
            //contactNumber cellPhone
            try
            {
                if (string.IsNullOrEmpty(stdByRule.Mobile))
                    return true;
                {
                    stdByRule.Mobile = stdByRule.Mobile.Replace("-", "").Trim();

                    User_ContactNumber contactNumber = emsDbDsc.User_ContactNumber.FirstOrDefault(x => x.UserId == stdByRule.Id);
                    // if same email found for this user need to update
                    if (contactNumber != null && contactNumber.ContactNumber.IsValid())
                    {
                        return true;
                    }
                    if (contactNumber == null)
                    {
                        contactNumber = User_ContactNumber.GetNew(BigInt.NewBigInt());
                    }
                    contactNumber.ContactNumber = stdByRule.Mobile;
                    if (stdByRule.Mobile.Length > 50)
                    {
                        contactNumber.ContactNumber = stdByRule.Mobile.Remove(51);
                    }
                    contactNumber.ContactNumberTypeEnumId = (int)User_ContactNumber.ContactNumberTypeEnum.PersonalMobile;
                    contactNumber.UserId = stdByRule.Id;

                    emsDbDsc.User_ContactNumber.Add(contactNumber);

                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"{stdByRule.Id} cellphone.{ex}";
                //richTextBox.Text = obj.Id + " cellphone " + ex.Message + richTextBox.Text;
                return false;
            }
            return false;
        }
        private bool SaveStdByRuleSscEducation(AdmUG_ApplicantByRule stdByRule, out string error)
        {
            error = null;
            try
            {
                //if (!string.IsNullOrEmpty(stdByRule.ssc))
                {
                    bool isNewObject = false;
                    User_Education education =
                        emsDbDsc.User_Education.SingleOrDefault(x => x.UserId == stdByRule.Id && x.DegreeEquivalentEnumId == (byte)User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent);
                    //if valid education found not update 
                    //if (education != null && education.Result.IsValid())
                    //{
                    //    return true;
                    //}
                    // if degree found need to update
                    if (education == null)
                    {
                        isNewObject = true;
                        education = User_Education.GetNew(BigInt.NewBigInt());
                    }
                    education.UserId = stdByRule.Id;
                    //education.EducationTypeEnumId = (byte)stdByRule.EducationTypeId;
                    education.DegreeEquivalentEnumId = (byte)User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent;
                    education.DegreeTitle = User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent.ToString().AddAndSpacesToSentence();
                    //education.ConcentratedMajor = "";
                    education.InstitutionName = stdByRule.SSC_InstituteName;
                    //education.Board = stdByRule.SSC_Board.ToString();
                    //education.Result = stdByRule.SSC_GPA.ToString();
                    education.YearOfPassing = stdByRule.SSC_Year;
                    //education.Session = $"{stdByRule.SSC_Year - 2}-{stdByRule.SSC_Year - 1}";
                    //education.Duration = "2 Year";
                    education.CreateDate = DateTime.Now;
                    education.CreateById = Profile.UserId;
                    education.LastChanged = stdByRule.LastChanged;
                    education.LastChangedById = stdByRule.LastChangedBy;
                    if (isNewObject)
                    {
                        emsDbDsc.User_Education.Add(education);
                    }
                    //emsDbDsc.SaveChanges();
                    //scope.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"{stdByRule.Id} contactEmail.{ex}";
                return false;
            }
            return false;
        }
        private bool SaveStdByRuleHscEducation(AdmUG_ApplicantByRule stdByRule, out string error)
        {
            error = null;
            try
            {
                //if (!string.IsNullOrEmpty(stdByRule.hsc))
                {
                    bool isNewObject = false;
                    User_Education education =
                        emsDbDsc.User_Education.SingleOrDefault(x => x.UserId == stdByRule.Id && x.DegreeEquivalentEnumId == (byte)User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent);
                    //if valid education found not update 
                    //if (education != null && education.Result.IsValid())
                    {
                        return true;
                    }
                    // if degree found need to update
                    if (education == null)
                    {
                        isNewObject = true;
                        education = User_Education.GetNew(BigInt.NewBigInt());
                    }
                    education.UserId = stdByRule.Id;
                    //education.EducationTypeEnumId = (byte)stdByRule.EducationTypeId;
                    education.DegreeEquivalentEnumId = (byte)User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent;
                    education.DegreeTitle = User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent.ToString().AddAndSpacesToSentence();
                    //education.ConcentratedMajor = "";
                    education.InstitutionName = stdByRule.HSC_InstituteName;
                    //education.Board = stdByRule.HSC_Board.ToString();
                    //education.Result = stdByRule.HSC_GPA.ToString();
                    education.YearOfPassing = stdByRule.HSC_Year;
                    //education.Session = education.Session = $"{stdByRule.HSC_Year - 2}-{stdByRule.HSC_Year - 1}";
                    //education.Duration = "2 Year";
                    education.CreateDate = DateTime.Now;
                    education.CreateById = Profile.UserId;
                    education.LastChanged = stdByRule.LastChanged;
                    education.LastChangedById = stdByRule.LastChangedBy;
                    if (isNewObject)
                    {
                        emsDbDsc.User_Education.Add(education);
                    }
                    //emsDbDsc.SaveChanges();
                    //scope.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error += $"{stdByRule.Id} contactEmail.{ex}";
                return false;
            }
            return false;
        }



        #endregion
    }
}
