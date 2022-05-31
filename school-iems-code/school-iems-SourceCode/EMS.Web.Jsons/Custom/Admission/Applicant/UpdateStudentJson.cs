using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Settings;
using EMS.Web.Jsons.Models;

namespace EMS.Web.Jsons.Custom.Admission.Applicant
{
    public class UpdateStudentJson
    {
        #region Profile

        #region Personal

        #region Account Table



        #endregion

        #region Student Table



        #endregion

        #endregion

        #region Previous Education



        #endregion

        #region Package & Discount



        #endregion

        #region Academic



        #endregion

        #region Credential


        #endregion

        #endregion


        #region Account
        public String Id { get; set; }
        public String UserName { get; set; }
        public String FullName { get; set; }
        public String BanglaName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String PlaceOfBirthCity { get; set; }
        public Nullable<Int32> PlaceOfBirthCountryId { get; set; }
        public Byte GenderEnumId { get; set; }
        public string Gender { get; set; }
        public Byte ReligionEnumId { get; set; }
        public string Religion { get; set; }
        public Byte BloodGroupEnumId { get; set; }
        public string BloodGroup { get; set; }
        public Byte MaritalStatusEnumId { get; set; }
        public string MaritalStatus { get; set; }
        public String FatherName { get; set; }
        public String MotherName { get; set; }
        public String SpouseName { get; set; }
        public String UserMobile { get; set; }
        public String UserMobile2 { get; set; }
        public Byte UserMobilePrivacyEnumId { get; set; }
        public string UserMobilePrivacy { get; set; }
        public Boolean IsVerifiedUserMobile { get; set; }
        public String UserEmail { get; set; }
        public Byte UserEmailPrivacyEnumId { get; set; }
        public string UserEmailPrivacy { get; set; }
        public Boolean IsVerifiedUserEmaill { get; set; }
        public String EmergencyContactPersonName { get; set; }
        public String EmergencyMobile { get; set; }
        public String EmergencyContactAdress { get; set; }
        public Nullable<Int32> EmergencyContactRelationshipId { get; set; }
        public Nullable<Single> Height { get; set; }
        public Boolean IsLate { get; set; }
        public Int32 CampusId { get; set; }
        public String Nationality { get; set; }
        public String NationalIdNumber { get; set; }
        public String BirthCertificateNumber { get; set; }
        public String BankAccountNo { get; set; }
        public String BankName { get; set; }
        public String PassportNo { get; set; }
        public Byte UserTypeEnumId { get; set; }
        public string UserType { get; set; }
        public Byte UserStatusEnumId { get; set; }
        public string UserStatus { get; set; }
        public Boolean IsMigrate { get; set; }
        public string MigrationId { get; set; }
        public Boolean IsProfileCompleted { get; set; }
        public Boolean IsApproved { get; set; }
        public Boolean IsUserCanEditProfile { get; set; }
        public Boolean IsDocumentPending { get; set; }
        public String Password { get; set; }
        public String PasswordSalt { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChangedDate { get; set; }
        public DateTime? LastLockoutDate { get; set; }
        public Byte FailedPasswordAttemptCount { get; set; }
        public DateTime? FailedPasswordAttemptWindowStart { get; set; }
        public Byte FailedPasswordAnswerAttemptCount { get; set; }
        public DateTime? FailedPasswordAnswerAttemptWindowStart { get; set; }
        public String Remarks { get; set; }
        public String History { get; set; }
        public String LockReason { get; set; }

        public Int32 DepartmentId { get; set; }
        public String JoiningSemesterId { get; set; }
        public String ProfilePictureImageUrl { get; set; }
        public String ProfilePictureBase64 { get; set; }
        public Boolean IsForceTwoFactorAuth { get; set; }
        public String ActiveRfidNo { get; set; }

        
        
        public DateTime CreateDate { get; set; }
        public String CreateById { get; set; }
        public String CreateByFullName { get; set; }
        public String CreateByUserName { get; set; }
        public DateTime LastChanged { get; set; }
        public String LastChangedById { get; set; }
        public String LastChangedByFullName { get; set; }
        public String LastChangedByUserName { get; set; }
        public String RankId { get; set; }

        #endregion

        //note:from student json

        #region Student

        public String StudentId { get; set; }
        //public String UserId { get; set; }
        public String ClassRollNo { get; set; }
        public String RegistrationNo { get; set; }
        public String RegistrationSession { get; set; }
        public Int32 GradingPolicyId { get; set; }
        public Single CGPA { get; set; }
        public Single CourseCompleted { get; set; }
        public Single CreditCompleted { get; set; }
        public Single IncompleteCredits { get; set; }
        public Nullable<Int32> LevelTermId { get; set; }


        public Int32 ProgramId { get; set; }
        //ems-4 update
        public Int32? PreviousProgramId { get; set; }
        public Int32 SemesterDurationEnumId { get; set; }
        public Int32 ContinuingBatchId { get; set; }
        public String PreviousStudentUserName { get; set; }
        public bool ForceDisableOnlineRegistration { get; set; }
        public bool EnableOpenCreditRegistration { get; set; }
        public bool IsRequiredCGPACalculation { get; set; }
        public DateTime? LastCGPASyncTime { get; set; }
        //ems-4 update
        public int JoiningBatchId { get; set; }
        public Byte ClassTimingGroupEnumId { get; set; }
        public string ClassTimingGroup { get; set; }
        public Int32 FeeCodeId { get; set; }
        public String CurriculumId { get; set; }
        public String MajorCurriculumId { get; set; }
        public String SecondMajorCurriculumId { get; set; }
        public String MinorCurriculumId { get; set; }
        public String ElectiveCurriculumId { get; set; }
        public Nullable<Int32> StudentQuotaNameId { get; set; }
        public Byte EnrollmentStatusEnumId { get; set; }
        public string EnrollmentStatus { get; set; }
        public Byte EnrollmentTypeEnumId { get; set; }
        public string EnrollmentType { get; set; }
        public Nullable<Int32> ParentsPrimaryJobTypeId { get; set; }
        public DateTime JoiningDate { get; set; }
        public Nullable<DateTime> LeavingDate { get; set; }
        public String FatherMobile { get; set; }
        public String MotherMobile { get; set; }
        public int AdmissionExamId { get; set; }
        public String AdmissionTestRollNo { get; set; }
        public String AdmissionFormNo { get; set; }
        public Byte AdmissionStatusEnumId { get; set; }
        public string AdmissionStatus { get; set; }
        public String AdmissionRemark { get; set; }
        public String CertificateRegistrationNo { get; set; }
        public Boolean IsAdmissionFeePaid { get; set; }
        public Boolean IsFeesVerified { get; set; }

        public Nullable<DateTime> ProvisionalCertificateIssueDate { get; set; }
        public Nullable<DateTime> OriginalCertificateIssueDate { get; set; }
        public Nullable<DateTime> OriginalTranscriptIssueDate { get; set; }

        public string TranscriptSerialNo { get; set; }
        public string OriginalCertificateSerialNo { get; set; }
        public string OriginalCertificateVerifyNo { get; set; }
        public string ProvisionalCertificateSerialNo { get; set; }
        public string ProvisionalCertificateVerifyNo { get; set; }
        public string GraduationSemesterId { get; set; }
        public String DegreeOptained { get; set; }
        public String MajorMinorDegreeText { get; set; }
        public Single CreditTransfer { get; set; }
        public Single CreditWaiver { get; set; }
        public float PerCreditFee { get; set; }
        public int ClassSectionId { get; set; }
        public String UgcUniqueId { get; set; }

        #endregion
        //custom Column
        public bool IsAutoUserName { get; set; }
        public bool IsGraduated { get; set; }


        //public bool IsAutoRollNo { get; set; }
        //public bool IsAutoRegistrationNumber { get; set; }
        //public bool IsAutoAdmissionRollNumber { get; set; }

        //public bool IsSameUserNameAndRollNo { get; set; }
        //public bool IsSameUserNameAndRegistrationNo { get; set; }
        //public bool IsSameUserNameAndAdmissionRollNo { get; set; }
        public List<User_EducationJson> EducationJsonList { get; set; }
        public List<User_ContactAddressJson> ContactAddressJsonList { get; set; }
        public User_ContactAddressJson PresentAddressJson { get; set; }
        public User_ContactAddressJson PermanentAddressJson { get; set; }

        

    }
    public static partial class EntityMapper
    {

        public static void Map(this User_Account entity, UpdateStudentJson toJson)
        {
            if (entity == null || toJson == null)
                return;
            toJson.Id = entity.Id.ToString();

            toJson.UserName = entity.UserName;
            toJson.FullName = entity.FullName;
            toJson.BanglaName = entity.BanglaName;
            toJson.DateOfBirth = entity.DateOfBirth;
            toJson.PlaceOfBirthCity = entity.PlaceOfBirthCity;
            toJson.PlaceOfBirthCountryId = entity.PlaceOfBirthCountryId;
            toJson.GenderEnumId = entity.GenderEnumId;
            if (entity.GenderEnumId != null)
                toJson.Gender = entity.Gender.ToString().AddSpacesToSentence();
            toJson.ReligionEnumId = entity.ReligionEnumId;
            if (entity.ReligionEnumId != null)
                toJson.Religion = entity.Religion.ToString().AddSpacesToSentence();
            toJson.BloodGroupEnumId = entity.BloodGroupEnumId;
            if (entity.BloodGroupEnumId != null)
                toJson.BloodGroup = entity.BloodGroup.ToString().AddSpacesToSentence();
            toJson.MaritalStatusEnumId = entity.MaritalStatusEnumId;
            if (entity.MaritalStatusEnumId != null)
                toJson.MaritalStatus = entity.MaritalStatus.ToString().AddSpacesToSentence();
            toJson.FatherName = entity.FatherName.ValidateName();
            toJson.MotherName = entity.MotherName.ValidateName();
            toJson.SpouseName = entity.SpouseName.ValidateName();
            toJson.UserMobile = entity.UserMobile;
            toJson.UserMobile2 = entity.UserMobile2;
            toJson.UserMobilePrivacyEnumId = entity.UserMobilePrivacyEnumId;
            if (entity.UserMobilePrivacyEnumId != null)
                toJson.UserMobilePrivacy = entity.UserMobilePrivacy.ToString().AddSpacesToSentence();
            toJson.IsVerifiedUserMobile = entity.IsVerifiedUserMobile;
            toJson.UserEmail = entity.UserEmail;
            toJson.UserEmailPrivacyEnumId = entity.UserEmailPrivacyEnumId;
            if (entity.UserEmailPrivacyEnumId != null)
                toJson.UserEmailPrivacy = entity.UserEmailPrivacy.ToString().AddSpacesToSentence();
            toJson.IsVerifiedUserEmaill = entity.IsVerifiedUserEmaill;
            toJson.EmergencyContactPersonName = entity.EmergencyContactPersonName.ToTitleCase();
            toJson.EmergencyMobile = entity.EmergencyMobile;
            toJson.EmergencyContactAdress = entity.EmergencyContactAdress;
            toJson.EmergencyContactRelationshipId = entity.EmergencyContactRelationshipId;
            toJson.Height = entity.Height;
            toJson.IsLate = entity.IsLate;
            toJson.CampusId = entity.CampusId;
            toJson.Nationality = entity.Nationality;
            toJson.NationalIdNumber = entity.NationalIdNumber;
            toJson.BirthCertificateNumber = entity.BirthCertificateNumber;
            toJson.BankAccountNo = entity.BankAccountNo;
            toJson.BankName = entity.BankName;
            toJson.PassportNo = entity.PassportNo;
            toJson.UserTypeEnumId = entity.UserTypeEnumId;
            if (entity.UserTypeEnumId != null)
                toJson.UserType = entity.UserType.ToString().AddSpacesToSentence();
            toJson.UserStatusEnumId = entity.UserStatusEnumId;
            if (entity.UserStatusEnumId != null)
                toJson.UserStatus = entity.UserStatus.ToString().AddSpacesToSentence();
            toJson.IsMigrate = entity.IsMigrate;
            toJson.MigrationId = entity.MigrationId;
            toJson.IsProfileCompleted = entity.IsProfileCompleted;
            toJson.IsApproved = entity.IsApproved;

            toJson.IsUserCanEditProfile = entity.IsUserCanEditProfile;
            toJson.IsDocumentPending = entity.IsDocumentPending;
            toJson.DepartmentId = entity.DepartmentId;
            toJson.JoiningDate = entity.JoiningDate;
            toJson.LeavingDate = entity.LeavingDate;

            //Ems-4 Mapping
            toJson.ProfilePictureImageUrl = entity.ProfilePictureImageUrl;
            toJson.IsForceTwoFactorAuth = entity.IsForceTwoFactorAuth;
            toJson.ActiveRfidNo = entity.ActiveRfidNo;
            toJson.History = entity.History;

            toJson.Password = entity.Password;
            toJson.PasswordSalt = entity.PasswordSalt;
            toJson.LastLoginDate = entity.LastLoginDate;
            toJson.LastPasswordChangedDate = entity.LastPasswordChangedDate;
            toJson.LastLockoutDate = entity.LastLockoutDate;
            toJson.FailedPasswordAttemptCount = entity.FailedPasswordAttemptCount;
            toJson.FailedPasswordAttemptWindowStart = entity.FailedPasswordAttemptWindowStart;
            toJson.FailedPasswordAnswerAttemptCount = entity.FailedPasswordAnswerAttemptCount;
            toJson.FailedPasswordAnswerAttemptWindowStart = entity.FailedPasswordAnswerAttemptWindowStart;
            toJson.Remarks = entity.Remarks;
            toJson.LockReason = entity.LockReason;
            if (entity.JoiningSemesterId != null)
                toJson.JoiningSemesterId = entity.JoiningSemesterId.ToString();
            toJson.CreateDate = entity.CreateDate;
            toJson.CreateById = entity.CreateById.ToString();
            toJson.LastChanged = entity.LastChanged;
            toJson.LastChangedById = entity.LastChangedById.ToString();
            //if (entity.RankId != null)
            //    toJson.RankId = entity.RankId.ToString();


        }
        public static void Map(this User_Student entity, UpdateStudentJson toJson)
        {
            if (entity == null || toJson == null)
                return;

            toJson.StudentId = entity.Id.ToString();

            //toJson.UserId = entity.UserId.ToString();
            toJson.ClassRollNo = entity.ClassRollNo;
            toJson.RegistrationNo = entity.RegistrationNo;
            toJson.RegistrationSession = entity.RegistrationSession;
            toJson.GradingPolicyId = entity.GradingPolicyId;
            toJson.CGPA = entity.CGPA;
            toJson.CourseCompleted = entity.CourseCompleted;
            toJson.CreditCompleted = entity.CreditCompleted;
            toJson.IncompleteCredits = entity.IncompleteCredits;
            toJson.LevelTermId = entity.LevelTermId;

            //Ems-4 Update
            toJson.SemesterDurationEnumId = entity.SemesterDurationEnumId;
            toJson.PreviousProgramId = entity.PreviousProgramId;
            toJson.JoiningBatchId = entity.JoiningBatchId;
            toJson.EnableOpenCreditRegistration = entity.EnableOpenCreditRegistration;
            toJson.ForceDisableOnlineRegistration = entity.ForceDisableOnlineRegistration;
            toJson.IsRequiredCGPACalculation = entity.IsRequiredCGPACalculation;
            toJson.LastCGPASyncTime = entity.LastCGPASyncTime;
            toJson.PreviousStudentUserName = entity.PreviousStudentUserName;

            toJson.ProgramId = entity.ProgramId;
            if (entity.ContinuingBatchId != null)
                toJson.ContinuingBatchId = entity.ContinuingBatchId;
            toJson.ClassTimingGroupEnumId = entity.ClassTimingGroupEnumId;
            if (entity.ClassTimingGroupEnumId != null)
                toJson.ClassTimingGroup = entity.ClassTimingGroup.ToString().AddSpacesToSentence();
            toJson.FeeCodeId = entity.FeeCodeId;
            toJson.CurriculumId = entity.CurriculumId.ToString();
            if (entity.MajorCurriculumId != null)
                toJson.MajorCurriculumId = entity.MajorCurriculumId.ToString();
            if (entity.SecondMajorCurriculumId != null)
                toJson.SecondMajorCurriculumId = entity.SecondMajorCurriculumId.ToString();
            if (entity.MinorCurriculumId != null)
                toJson.MinorCurriculumId = entity.MinorCurriculumId.ToString();
            if (entity.ElectiveCurriculumId != null)
                toJson.ElectiveCurriculumId = entity.ElectiveCurriculumId.ToString();
            toJson.StudentQuotaNameId = entity.StudentQuotaNameId;
            toJson.EnrollmentStatusEnumId = entity.EnrollmentStatusEnumId;
            if (entity.EnrollmentStatusEnumId != null)
                toJson.EnrollmentStatus = entity.EnrollmentStatus.ToString().AddSpacesToSentence();
            toJson.EnrollmentTypeEnumId = entity.EnrollmentTypeEnumId;
            if (entity.EnrollmentTypeEnumId != null)
                toJson.EnrollmentType = entity.EnrollmentType.ToString().AddSpacesToSentence();
            toJson.ParentsPrimaryJobTypeId = entity.ParentsPrimaryJobTypeId;

            toJson.FatherMobile = entity.FatherMobile;
            toJson.MotherMobile = entity.MotherMobile;
            toJson.AdmissionExamId = entity.AdmissionExamId;
            toJson.AdmissionTestRollNo = entity.AdmissionTestRollNo;
            toJson.AdmissionFormNo = entity.AdmissionFormNo;
            toJson.AdmissionStatusEnumId = entity.AdmissionStatusEnumId;
            if (entity.AdmissionStatusEnumId != null)
                toJson.AdmissionStatus = entity.AdmissionStatus.ToString().AddSpacesToSentence();
            toJson.AdmissionRemark = entity.AdmissionRemark;
            toJson.CertificateRegistrationNo = entity.CertificateRegistrationNo;

            toJson.IsAdmissionFeePaid = entity.IsAdmissionFeePaid;
            toJson.IsFeesVerified = entity.IsFeesVerified;

            toJson.CreateDate = entity.CreateDate;
            toJson.CreateById = entity.CreateById.ToString();
            toJson.LastChanged = entity.LastChanged;
            toJson.LastChangedById = entity.LastChangedById.ToString();

            toJson.ProvisionalCertificateIssueDate = entity.ProvisionalCertificateIssueDate;
            toJson.OriginalCertificateIssueDate = entity.OriginalCertificateIssueDate;
            toJson.OriginalTranscriptIssueDate = entity.OriginalTranscriptIssueDate;
            if (entity.GraduationSemesterId!=null)
            {
                toJson.GraduationSemesterId = entity.GraduationSemesterId.ToString();
            }
            
            toJson.DegreeOptained = entity.DegreeOptained;
            toJson.MajorMinorDegreeText = entity.MajorMinorDegreeText;
            toJson.CreditTransfer = entity.CreditTransfer;
            toJson.CreditWaiver = entity.CreditWaiver;
            toJson.PerCreditFee = entity.PerCreditFee;
            toJson.ClassSectionId = entity.ClassSectionId;
            toJson.UgcUniqueId = entity.UgcUniqueId;

            toJson.IsGraduated = false;
            if (entity.EnrollmentStatusEnumId==(byte)User_Student.EnrollmentStatusEnum.Graduated)
            {
                toJson.IsGraduated = true;
            }

        }
        //public static void Map(this NewApplicantJson json, User_Account toEntity)
        //{
        //    if (toEntity == null || json == null)
        //        return;
        //    long output;
        //    toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
        //    toEntity.UserName = json.UserName;
        //    toEntity.FullName = json.FullName;
        //    toEntity.BanglaName = json.BanglaName;
        //    //toEntity.Prefix = json.Prefix;
        //    //toEntity.Suffix = json.Suffix;
        //    toEntity.DateOfBirth = json.DateOfBirth;
        //    toEntity.PlaceOfBirthCity = json.PlaceOfBirthCity;
        //    toEntity.PlaceOfBirthCountryId = json.PlaceOfBirthCountryId;
        //    toEntity.GenderEnumId = json.GenderEnumId;
        //    toEntity.ReligionEnumId = json.ReligionEnumId;
        //    toEntity.BloodGroupEnumId = json.BloodGroupEnumId;
        //    toEntity.MaritalStatusEnumId = json.MaritalStatusEnumId;
        //    toEntity.FatherName = json.FatherName;
        //    toEntity.MotherName = json.MotherName;
        //    //toEntity.SpouseName = json.SpouseName;

        //    //toEntity.Height = json.Height;
        //    //toEntity.Weight = json.Weight;
        //    //toEntity.IsLate = json.IsLate;
        //    toEntity.CampusId = json.CampusId;
        //    toEntity.Nationality = json.Nationality;
        //    toEntity.NationalIdNumber = json.NationalIdNumber;
        //    toEntity.BirthCertificateNumber = json.BirthCertificateNumber;
        //    //toEntity.BankAccountNo = json.BankAccountNo;
        //    //toEntity.BankId = json.BankId;
        //    //toEntity.CategoryEnumId = json.CategoryEnumId;
        //    //toEntity.UserTypeEnumId = json.UserTypeEnumId;
        //    //toEntity.UserStatusEnumId = json.UserStatusEnumId;
        //    //toEntity.IsMigrate = json.IsMigrate;
        //    //toEntity.MigrationId = json.MigrationId;
        //    //toEntity.IsProfileCompleted = json.IsProfileCompleted;
        //    //toEntity.IsApproved = json.IsApproved;
        //    //toEntity.Password = json.Password;
        //    //toEntity.PasswordSalt = json.PasswordSalt;
        //    //toEntity.IsLockedOut = json.IsLockedOut;
        //    //toEntity.LastLoginDate = json.LastLoginDate;
        //    //toEntity.LastPasswordChangedDate = json.LastPasswordChangedDate;
        //    //toEntity.LastLockoutDate = json.LastLockoutDate;
        //    //toEntity.FailedPasswordAttemptCount = json.FailedPasswordAttemptCount;
        //    //toEntity.FailedPasswordAttemptWindowStart = json.FailedPasswordAttemptWindowStart;
        //    //toEntity.FailedPasswordAnswerAttemptCount = json.FailedPasswordAnswerAttemptCount;
        //    //toEntity.FailedPasswordAnswerAttemptWindowStart = json.FailedPasswordAnswerAttemptWindowStart;
        //    toEntity.Remarks = json.Remarks;
        //    toEntity.UserMobile = json.UserMobile;
        //    //toEntity.UserMobilePrivacyEnumId = json.UserMobilePrivacyEnumId;
        //    //toEntity.IsVarifiedUserMobile = json.IsVarifiedUserMobile;
        //    toEntity.UserEmail = json.UserEmail;
        //    //toEntity.UserEmailPrivacyEnumId = json.UserEmailPrivacyEnumId;
        //    //toEntity.IsVarifiedUserEmaill = json.IsVarifiedUserEmaill;
        //    toEntity.EmergencyContactPersonName = json.EmergencyContactPersonName;
        //    toEntity.EmergencyMobile = json.EmergencyMobile;
        //    toEntity.EmergencyContactAdress = json.EmergencyContactAdress;
        //    toEntity.EmergencyContactRelationshipId = json.EmergencyContactRelationshipId;
        //    //toEntity.CreateDate = json.CreateDate;
        //    //toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
        //    //toEntity.LastChanged = json.LastChanged;
        //    //toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
        //    //toEntity.RankId = long.TryParse(json.RankId, out output) ? output : (Int64?)null;
        //}
        //public static void Map(this NewApplicantJson json, User_Student toEntity)
        //{
        //    if (toEntity == null || json == null)
        //        return;
        //    long output;
        //    toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
        //    //toEntity.UserId = long.TryParse(json.UserId, out output) ? output : 0;
        //    //toEntity.RegistrationNo = json.RegistrationNo;

        //    //toEntity.RegistrationSession = json.RegistrationSession;
        //    //toEntity.ClassSectionId = json.ClassSectionId;
        //    //toEntity.ClassRollNo = json.ClassRollNo;
        //    //toEntity.AdmissionTestRollNo = json.AdmissionTestRollNo;

        //    toEntity.AdmissionExamId = json.AdmissionExamId;
        //    toEntity.DepartmentId = json.DepartmentId;
        //    toEntity.ProgramId = long.TryParse(json.ProgramId, out output) ? output : 0;
        //    toEntity.ContinuingBatchId = long.TryParse(json.ContinuingBatchId, out output) ? output : 0;
        //    toEntity.FeeCodeId = json.FeeCodeId; //long.TryParse(json.FeeCodeId, out output) ? output : 0;
        //    toEntity.CurriculumId = long.TryParse(json.CurriculumId, out output) ? output : 0;
        //    toEntity.ElectiveCurriculumId = json.ElectiveCurriculumId;
        //    toEntity.StudentQuotaNameId = json.StudentQuotaNameId;
        //    //toEntity.EnrollmentStatusEnumId = json.EnrollmentStatusEnumId;
        //    toEntity.EnrollmentTypeEnumId = json.EnrollmentTypeEnumId;
        //    toEntity.ParentsPrimaryJobTypeId = json.ParentsPrimaryJobTypeId;
        //    //toEntity.LevelId = json.LevelId;
        //    //toEntity.TermId = json.TermId;
        //    //toEntity.CGPA = json.CGPA;
        //    //toEntity.CreditCompleted = json.CreditCompleted;
        //    toEntity.DateAdmitted = json.DateAdmitted;
        //    //toEntity.DateGraduated = json.DateGraduated;
        //    //toEntity.GradeTypeEnumId = json.GradeTypeEnumId;
        //    //toEntity.CourseCompleted = json.CourseCompleted;
        //    //toEntity.IncompleteCredits = json.IncompleteCredits;
        //    //toEntity.FatherId = long.TryParse(json.FatherId, out output) ? output : (Int64?)null;
        //    //toEntity.MotherId = long.TryParse(json.MotherId, out output) ? output : (Int64?)null;
        //    //toEntity.OtherParentId = long.TryParse(json.OtherParentId, out output) ? output : (Int64?)null;
        //    //toEntity.CreateDate = json.CreateDate;
        //    //toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
        //    //toEntity.LastChanged = json.LastChanged;
        //    //toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
        //    toEntity.FatherMobile = json.FatherMobile;
        //    toEntity.MotherMobile = json.MotherMobile;
        //    //toEntity.OtherParentMobile = json.OtherParentMobile;

        //    toEntity.AdmissionFormNo = json.AdmissionFormNo;
        //    toEntity.AdmissionStatusEnumId = json.AdmissionStatusEnumId;
        //    toEntity.AdmissionRemark = json.AdmissionRemark;


        //}

    }
}
