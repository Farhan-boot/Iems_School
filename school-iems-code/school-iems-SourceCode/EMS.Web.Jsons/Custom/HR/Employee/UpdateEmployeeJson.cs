using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;

namespace EMS.Web.Jsons.Custom.HR.Employee
{
    public class UpdateEmployeeJson
    {

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
        public String LockReason { get; set; }
        public String JoiningSemesterId { get; set; }
        public DateTime CreateDate { get; set; }
        public String CreateById { get; set; }
        public DateTime LastChanged { get; set; }
        public String LastChangedById { get; set; }
        public String RankId { get; set; }
        //Ems-4
        public String History { get; set; }
        public String ActiveRfidNo { get; set; }
        public String ProfilePictureImageUrl { get; set; }
        public String SignatureImageUrl { get; set; }
        public Boolean IsForceTwoFactorAuth { get; set; }
        #endregion

        #region Employee
        public Int32 UserId { get; set; }
        public Byte EmployeeCategoryEnumId { get; set; }
        public string EmployeeCategory { get; set; }
        public Byte EmployeeTypeEnumId { get; set; }
        public string EmployeeType { get; set; }
        public Byte JobClassEnumId { get; set; }
        public string JobClass { get; set; }
        public Byte JobTypeEnumId { get; set; }
        public string JobType { get; set; }
        public Byte WorkingStatusEnumId { get; set; }
        public string WorkingStatus { get; set; }
        public String TinNumber { get; set; }
        public String ImmediateSuperVisorId { get; set; }
        public Boolean IsAcademician { get; set; }
        public Boolean IsWorking { get; set; }
        public String OldIdNo { get; set; }
        public DateTime JoiningDate { get; set; }
        public Int32 IncrementMonthEnumId { get; set; }
        public string IncrementMonth { get; set; }
        public Int32 JoiningDepartmentId { get; set; }
        public Int32 DepartmentId { get; set; }
        public int PositionId { get; set; }
        public Nullable<DateTime> LeavingDate { get; set; }
        public Boolean HasAdminAccess { get; set; }
        #endregion


        public List<User_EducationJson> EducationJsonList { get; set; }
        public List<User_ContactAddressJson> ContactAddressJsonList { get; set; }
        public User_ContactAddressJson PresentAddressJson { get; set; }
        public User_ContactAddressJson PermanentAddressJson { get; set; }
    }

    public static partial class EntityMapper
    {

        public static void Map(this User_Account entity, UpdateEmployeeJson toJson)
        {
            if (entity == null || toJson == null)
                return;

            toJson.Id = entity.Id.ToString();

            toJson.UserName = entity.UserName;
            toJson.FullName = entity.FullName.ToTitleCase();
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
            toJson.FatherName = entity.FatherName.ToTitleCase();
            toJson.MotherName = entity.MotherName.ToTitleCase();
            toJson.SpouseName = entity.SpouseName.ToTitleCase();
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

            toJson.LeavingDate = entity.LeavingDate;

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

            //Ems-4 update
            toJson.History = entity.History;
            toJson.ActiveRfidNo = entity.ActiveRfidNo;
            toJson.ProfilePictureImageUrl = entity.ProfilePictureImageUrl;
            toJson.SignatureImageUrl = entity.SignatureImageUrl;
            toJson.IsForceTwoFactorAuth = entity.IsForceTwoFactorAuth;

            toJson.JoiningDate = entity.JoiningDate;
            toJson.DepartmentId = entity.DepartmentId;
            toJson.LeavingDate = entity.LeavingDate;
            toJson.CreateDate = entity.CreateDate;
            toJson.CreateById = entity.CreateById.ToString();
            toJson.LastChanged = entity.LastChanged;
            toJson.LastChangedById = entity.LastChangedById.ToString();
            //if (entity.RankId != null)
            //    toJson.RankId = entity.RankId.ToString();

        }
        public static void Map(this User_Employee entity, UpdateEmployeeJson toJson)
        {
            if (entity == null || toJson == null)
                return;

            toJson.Id = entity.Id.ToString();

            toJson.UserId = entity.UserId;
            toJson.EmployeeCategoryEnumId = entity.EmployeeCategoryEnumId;
            if (entity.EmployeeCategoryEnumId != null)
                toJson.EmployeeCategory = entity.EmployeeCategory.ToString().AddSpacesToSentence();
            toJson.EmployeeTypeEnumId = entity.EmployeeTypeEnumId;
            if (entity.EmployeeTypeEnumId != null)
                toJson.EmployeeType = entity.EmployeeType.ToString().AddSpacesToSentence();
            toJson.JobClassEnumId = entity.JobClassEnumId;
            if (entity.JobClassEnumId != null)
                toJson.JobClass = entity.JobClass.ToString().AddSpacesToSentence();
            toJson.JobTypeEnumId = entity.JobTypeEnumId;
            if (entity.JobTypeEnumId != null)
                toJson.JobType = entity.JobType.ToString().AddSpacesToSentence();
            toJson.WorkingStatusEnumId = entity.WorkingStatusEnumId;
            if (entity.WorkingStatusEnumId != null)
                toJson.WorkingStatus = entity.WorkingStatus.ToString().AddSpacesToSentence();
            toJson.TinNumber = entity.TinNumber;
            if (entity.ImmediateSuperVisorId != null)
                toJson.ImmediateSuperVisorId = entity.ImmediateSuperVisorId.ToString();
            toJson.IsAcademician = entity.IsAcademician;
            //toJson.IsWorking = entity.IsWorking;
            //toJson.OldIdNo = entity.OldIdNo;
            //toJson.JoiningDate = entity.JoiningDate;
            toJson.IncrementMonthEnumId = entity.IncrementMonthEnumId;
            if (entity.IncrementMonthEnumId != null)
                toJson.IncrementMonth = entity.IncrementMonth.ToString().AddSpacesToSentence();
            toJson.JoiningDepartmentId = entity.JoiningDepartmentId;
            //toJson.DepartmentId = entity.DepartmentId;
            if (entity.PositionId != null)
                toJson.PositionId = (int)entity.PositionId;
            toJson.HasAdminAccess = entity.HasAdminAccess;


            toJson.CreateDate = entity.CreateDate;
            toJson.CreateById = entity.CreateById.ToString();
            toJson.LastChanged = entity.LastChanged;
            toJson.LastChangedById = entity.LastChangedById.ToString();

        }


    }
}
