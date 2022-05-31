//File:Json Model Base Partial
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

using EMS.Framework;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
	public partial class User_AccountJson 
	{
          public Int32 Id { get; set; }
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
          public String MigrationId { get; set; }
          public Boolean IsProfileCompleted { get; set; }
          public Boolean IsUserCanEditProfile { get; set; }
          public Boolean IsDocumentPending { get; set; }
          public Boolean IsApproved { get; set; }
          public String Password { get; set; }
          public String PasswordSalt { get; set; }
          public Nullable<DateTime> LastLoginDate { get; set; }
          public Nullable<DateTime> LastPasswordChangedDate { get; set; }
          public Nullable<DateTime> LastLockoutDate { get; set; }
          public Byte FailedPasswordAttemptCount { get; set; }
          public Nullable<DateTime> FailedPasswordAttemptWindowStart { get; set; }
          public Byte FailedPasswordAnswerAttemptCount { get; set; }
          public Nullable<DateTime> FailedPasswordAnswerAttemptWindowStart { get; set; }
          public String Remarks { get; set; }
          public String LockReason { get; set; }
          public Int32 DepartmentId { get; set; }
          public String JoiningSemesterId { get; set; }
          public DateTime JoiningDate { get; set; }
          public Nullable<DateTime> LeavingDate { get; set; }
          public String TrackingId { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public String History { get; set; }
          public String ActiveRfidNo { get; set; }
          public String ProfilePictureImageUrl { get; set; }
          public String SignatureImageUrl { get; set; }
          public Boolean IsForceTwoFactorAuth { get; set; }
          public Boolean IsAllowMultiLogin { get; set; }
          public String AuthTokenJson { get; set; }
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public bool IsAlreadyAdded { get; set; }
	}
    #region Mapper
     /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
	public static partial class EntityMapper
	{
	   
		public static void Map(this User_Account entity, User_AccountJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.UserName = entity.UserName ; 
			 toJson.FullName = entity.FullName ; 
			 toJson.BanglaName = entity.BanglaName ; 
			 toJson.DateOfBirth = entity.DateOfBirth ; 
			 toJson.PlaceOfBirthCity = entity.PlaceOfBirthCity ; 
			 toJson.PlaceOfBirthCountryId = entity.PlaceOfBirthCountryId ; 
			 toJson.GenderEnumId = entity.GenderEnumId ; 
             if(entity.GenderEnumId!=null)
             toJson.Gender = entity.Gender.ToString().AddSpacesToSentence();
			 toJson.ReligionEnumId = entity.ReligionEnumId ; 
             if(entity.ReligionEnumId!=null)
             toJson.Religion = entity.Religion.ToString().AddSpacesToSentence();
			 toJson.BloodGroupEnumId = entity.BloodGroupEnumId ; 
             if(entity.BloodGroupEnumId!=null)
             toJson.BloodGroup = entity.BloodGroup.ToString().AddSpacesToSentence();
			 toJson.MaritalStatusEnumId = entity.MaritalStatusEnumId ; 
             if(entity.MaritalStatusEnumId!=null)
             toJson.MaritalStatus = entity.MaritalStatus.ToString().AddSpacesToSentence();
			 toJson.FatherName = entity.FatherName ; 
			 toJson.MotherName = entity.MotherName ; 
			 toJson.SpouseName = entity.SpouseName ; 
			 toJson.UserMobile = entity.UserMobile ; 
			 toJson.UserMobile2 = entity.UserMobile2 ; 
			 toJson.UserMobilePrivacyEnumId = entity.UserMobilePrivacyEnumId ; 
             if(entity.UserMobilePrivacyEnumId!=null)
             toJson.UserMobilePrivacy = entity.UserMobilePrivacy.ToString().AddSpacesToSentence();
			 toJson.IsVerifiedUserMobile = entity.IsVerifiedUserMobile ; 
			 toJson.UserEmail = entity.UserEmail ; 
			 toJson.UserEmailPrivacyEnumId = entity.UserEmailPrivacyEnumId ; 
             if(entity.UserEmailPrivacyEnumId!=null)
             toJson.UserEmailPrivacy = entity.UserEmailPrivacy.ToString().AddSpacesToSentence();
			 toJson.IsVerifiedUserEmaill = entity.IsVerifiedUserEmaill ; 
			 toJson.EmergencyContactPersonName = entity.EmergencyContactPersonName ; 
			 toJson.EmergencyMobile = entity.EmergencyMobile ; 
			 toJson.EmergencyContactAdress = entity.EmergencyContactAdress ; 
			 toJson.EmergencyContactRelationshipId = entity.EmergencyContactRelationshipId ; 
			 toJson.Height = entity.Height ; 
			 toJson.IsLate = entity.IsLate ; 
			 toJson.CampusId = entity.CampusId ; 
			 toJson.Nationality = entity.Nationality ; 
			 toJson.NationalIdNumber = entity.NationalIdNumber ; 
			 toJson.BirthCertificateNumber = entity.BirthCertificateNumber ; 
			 toJson.BankAccountNo = entity.BankAccountNo ; 
			 toJson.BankName = entity.BankName ; 
			 toJson.PassportNo = entity.PassportNo ; 
			 toJson.UserTypeEnumId = entity.UserTypeEnumId ; 
             if(entity.UserTypeEnumId!=null)
             toJson.UserType = entity.UserType.ToString().AddSpacesToSentence();
			 toJson.UserStatusEnumId = entity.UserStatusEnumId ; 
             if(entity.UserStatusEnumId!=null)
             toJson.UserStatus = entity.UserStatus.ToString().AddSpacesToSentence();
			 toJson.IsMigrate = entity.IsMigrate ; 
			 toJson.MigrationId = entity.MigrationId ; 
			 toJson.IsProfileCompleted = entity.IsProfileCompleted ; 
			 toJson.IsUserCanEditProfile = entity.IsUserCanEditProfile ; 
			 toJson.IsDocumentPending = entity.IsDocumentPending ; 
			 toJson.IsApproved = entity.IsApproved ; 
			 toJson.Password = entity.Password ; 
			 toJson.PasswordSalt = entity.PasswordSalt ; 
			 toJson.LastLoginDate = entity.LastLoginDate ; 
			 toJson.LastPasswordChangedDate = entity.LastPasswordChangedDate ; 
			 toJson.LastLockoutDate = entity.LastLockoutDate ; 
			 toJson.FailedPasswordAttemptCount = entity.FailedPasswordAttemptCount ; 
			 toJson.FailedPasswordAttemptWindowStart = entity.FailedPasswordAttemptWindowStart ; 
			 toJson.FailedPasswordAnswerAttemptCount = entity.FailedPasswordAnswerAttemptCount ; 
			 toJson.FailedPasswordAnswerAttemptWindowStart = entity.FailedPasswordAnswerAttemptWindowStart ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.LockReason = entity.LockReason ; 
			 toJson.DepartmentId = entity.DepartmentId ; 
             if(entity.JoiningSemesterId!=null)
			 toJson.JoiningSemesterId = entity.JoiningSemesterId.ToString() ; 
			 toJson.JoiningDate = entity.JoiningDate ; 
			 toJson.LeavingDate = entity.LeavingDate ; 
			 toJson.TrackingId = entity.TrackingId ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.History = entity.History ; 
			 toJson.ActiveRfidNo = entity.ActiveRfidNo ; 
			 toJson.ProfilePictureImageUrl = entity.ProfilePictureImageUrl ; 
			 toJson.SignatureImageUrl = entity.SignatureImageUrl ; 
			 toJson.IsForceTwoFactorAuth = entity.IsForceTwoFactorAuth ; 
			 toJson.IsAllowMultiLogin = entity.IsAllowMultiLogin ; 
			 toJson.AuthTokenJson = entity.AuthTokenJson ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this User_AccountJson json, User_Account toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.UserName = json.UserName?.Trim() ?? json.UserName;
                 toEntity.FullName = json.FullName?.Trim() ?? json.FullName;
                 toEntity.BanglaName = json.BanglaName?.Trim() ?? json.BanglaName;
    			 toEntity.DateOfBirth=json.DateOfBirth;
                 toEntity.PlaceOfBirthCity = json.PlaceOfBirthCity?.Trim() ?? json.PlaceOfBirthCity;
    			 toEntity.PlaceOfBirthCountryId=json.PlaceOfBirthCountryId;
    			 toEntity.GenderEnumId=json.GenderEnumId;
    			 toEntity.ReligionEnumId=json.ReligionEnumId;
    			 toEntity.BloodGroupEnumId=json.BloodGroupEnumId;
    			 toEntity.MaritalStatusEnumId=json.MaritalStatusEnumId;
                 toEntity.FatherName = json.FatherName?.Trim() ?? json.FatherName;
                 toEntity.MotherName = json.MotherName?.Trim() ?? json.MotherName;
                 toEntity.SpouseName = json.SpouseName?.Trim() ?? json.SpouseName;
                 toEntity.UserMobile = json.UserMobile?.Trim() ?? json.UserMobile;
                 toEntity.UserMobile2 = json.UserMobile2?.Trim() ?? json.UserMobile2;
    			 toEntity.UserMobilePrivacyEnumId=json.UserMobilePrivacyEnumId;
    			 toEntity.IsVerifiedUserMobile=json.IsVerifiedUserMobile;
                 toEntity.UserEmail = json.UserEmail?.Trim() ?? json.UserEmail;
    			 toEntity.UserEmailPrivacyEnumId=json.UserEmailPrivacyEnumId;
    			 toEntity.IsVerifiedUserEmaill=json.IsVerifiedUserEmaill;
                 toEntity.EmergencyContactPersonName = json.EmergencyContactPersonName?.Trim() ?? json.EmergencyContactPersonName;
                 toEntity.EmergencyMobile = json.EmergencyMobile?.Trim() ?? json.EmergencyMobile;
                 toEntity.EmergencyContactAdress = json.EmergencyContactAdress?.Trim() ?? json.EmergencyContactAdress;
    			 toEntity.EmergencyContactRelationshipId=json.EmergencyContactRelationshipId;
    			 toEntity.Height=json.Height;
    			 toEntity.IsLate=json.IsLate;
    			 toEntity.CampusId=json.CampusId;
                 toEntity.Nationality = json.Nationality?.Trim() ?? json.Nationality;
                 toEntity.NationalIdNumber = json.NationalIdNumber?.Trim() ?? json.NationalIdNumber;
                 toEntity.BirthCertificateNumber = json.BirthCertificateNumber?.Trim() ?? json.BirthCertificateNumber;
                 toEntity.BankAccountNo = json.BankAccountNo?.Trim() ?? json.BankAccountNo;
                 toEntity.BankName = json.BankName?.Trim() ?? json.BankName;
                 toEntity.PassportNo = json.PassportNo?.Trim() ?? json.PassportNo;
    			 toEntity.UserTypeEnumId=json.UserTypeEnumId;
    			 toEntity.UserStatusEnumId=json.UserStatusEnumId;
    			 toEntity.IsMigrate=json.IsMigrate;
                 toEntity.MigrationId = json.MigrationId?.Trim() ?? json.MigrationId;
    			 toEntity.IsProfileCompleted=json.IsProfileCompleted;
    			 toEntity.IsUserCanEditProfile=json.IsUserCanEditProfile;
    			 toEntity.IsDocumentPending=json.IsDocumentPending;
    			 toEntity.IsApproved=json.IsApproved;
                 toEntity.Password = json.Password?.Trim() ?? json.Password;
                 toEntity.PasswordSalt = json.PasswordSalt?.Trim() ?? json.PasswordSalt;
    			 toEntity.LastLoginDate=json.LastLoginDate;
    			 toEntity.LastPasswordChangedDate=json.LastPasswordChangedDate;
    			 toEntity.LastLockoutDate=json.LastLockoutDate;
    			 toEntity.FailedPasswordAttemptCount=json.FailedPasswordAttemptCount;
    			 toEntity.FailedPasswordAttemptWindowStart=json.FailedPasswordAttemptWindowStart;
    			 toEntity.FailedPasswordAnswerAttemptCount=json.FailedPasswordAnswerAttemptCount;
    			 toEntity.FailedPasswordAnswerAttemptWindowStart=json.FailedPasswordAnswerAttemptWindowStart;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
                 toEntity.LockReason = json.LockReason?.Trim() ?? json.LockReason;
    			 toEntity.DepartmentId=json.DepartmentId;
                 toEntity.JoiningSemesterId = long.TryParse(json.JoiningSemesterId, out output) ? output : (Int64?)null;
    			 toEntity.JoiningDate=json.JoiningDate;
    			 toEntity.LeavingDate=json.LeavingDate;
                 toEntity.TrackingId = json.TrackingId?.Trim() ?? json.TrackingId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;
                 toEntity.History = json.History?.Trim() ?? json.History;
                 toEntity.ActiveRfidNo = json.ActiveRfidNo?.Trim() ?? json.ActiveRfidNo;
                 toEntity.ProfilePictureImageUrl = json.ProfilePictureImageUrl?.Trim() ?? json.ProfilePictureImageUrl;
                 toEntity.SignatureImageUrl = json.SignatureImageUrl?.Trim() ?? json.SignatureImageUrl;
    			 toEntity.IsForceTwoFactorAuth=json.IsForceTwoFactorAuth;
    			 toEntity.IsAllowMultiLogin=json.IsAllowMultiLogin;
                 toEntity.AuthTokenJson = json.AuthTokenJson?.Trim() ?? json.AuthTokenJson;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<User_Account> entityList, IList<User_AccountJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 User_AccountJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new User_AccountJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<User_AccountJson> jsonList, ICollection<User_Account> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    User_Account obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = User_Account.GetNew();
                    json.Map(obj);
                    toEntityList.Add(obj);
                    
                }
                else
                {
                   json.Map(obj);
                }
            }
        }
	}
    #endregion
}
