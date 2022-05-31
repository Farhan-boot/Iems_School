//File: Entity Partial Base
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace EMS.DataAccess.Data
{

    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
    public partial class User_Account
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_UserName = "UserName";		            
		public const string Property_FullName = "FullName";		            
		public const string Property_BanglaName = "BanglaName";		            
		public const string Property_DateOfBirth = "DateOfBirth";		            
		public const string Property_PlaceOfBirthCity = "PlaceOfBirthCity";		            
		public const string Property_PlaceOfBirthCountryId = "PlaceOfBirthCountryId";		            
		public const string Property_GenderEnumId = "GenderEnumId";		            
        public const string Property_Gender = "Gender";
		public const string Property_ReligionEnumId = "ReligionEnumId";		            
        public const string Property_Religion = "Religion";
		public const string Property_BloodGroupEnumId = "BloodGroupEnumId";		            
        public const string Property_BloodGroup = "BloodGroup";
		public const string Property_MaritalStatusEnumId = "MaritalStatusEnumId";		            
        public const string Property_MaritalStatus = "MaritalStatus";
		public const string Property_FatherName = "FatherName";		            
		public const string Property_MotherName = "MotherName";		            
		public const string Property_SpouseName = "SpouseName";		            
		public const string Property_UserMobile = "UserMobile";		            
		public const string Property_UserMobile2 = "UserMobile2";		            
		public const string Property_UserMobilePrivacyEnumId = "UserMobilePrivacyEnumId";		            
        public const string Property_UserMobilePrivacy = "UserMobilePrivacy";
		public const string Property_IsVerifiedUserMobile = "IsVerifiedUserMobile";		            
		public const string Property_UserEmail = "UserEmail";		            
		public const string Property_UserEmailPrivacyEnumId = "UserEmailPrivacyEnumId";		            
        public const string Property_UserEmailPrivacy = "UserEmailPrivacy";
		public const string Property_IsVerifiedUserEmaill = "IsVerifiedUserEmaill";		            
		public const string Property_EmergencyContactPersonName = "EmergencyContactPersonName";		            
		public const string Property_EmergencyMobile = "EmergencyMobile";		            
		public const string Property_EmergencyContactAdress = "EmergencyContactAdress";		            
		public const string Property_EmergencyContactRelationshipId = "EmergencyContactRelationshipId";		            
		public const string Property_Height = "Height";		            
		public const string Property_IsLate = "IsLate";		            
		public const string Property_CampusId = "CampusId";		            
		public const string Property_Nationality = "Nationality";		            
		public const string Property_NationalIdNumber = "NationalIdNumber";		            
		public const string Property_BirthCertificateNumber = "BirthCertificateNumber";		            
		public const string Property_BankAccountNo = "BankAccountNo";		            
		public const string Property_BankName = "BankName";		            
		public const string Property_PassportNo = "PassportNo";		            
		public const string Property_UserTypeEnumId = "UserTypeEnumId";		            
        public const string Property_UserType = "UserType";
		public const string Property_UserStatusEnumId = "UserStatusEnumId";		            
        public const string Property_UserStatus = "UserStatus";
		public const string Property_IsMigrate = "IsMigrate";		            
		public const string Property_MigrationId = "MigrationId";		            
		public const string Property_IsProfileCompleted = "IsProfileCompleted";		            
		public const string Property_IsUserCanEditProfile = "IsUserCanEditProfile";		            
		public const string Property_IsDocumentPending = "IsDocumentPending";		            
		public const string Property_IsApproved = "IsApproved";		            
		public const string Property_Password = "Password";		            
		public const string Property_PasswordSalt = "PasswordSalt";		            
		public const string Property_LastLoginDate = "LastLoginDate";		            
		public const string Property_LastPasswordChangedDate = "LastPasswordChangedDate";		            
		public const string Property_LastLockoutDate = "LastLockoutDate";		            
		public const string Property_FailedPasswordAttemptCount = "FailedPasswordAttemptCount";		            
		public const string Property_FailedPasswordAttemptWindowStart = "FailedPasswordAttemptWindowStart";		            
		public const string Property_FailedPasswordAnswerAttemptCount = "FailedPasswordAnswerAttemptCount";		            
		public const string Property_FailedPasswordAnswerAttemptWindowStart = "FailedPasswordAnswerAttemptWindowStart";		            
		public const string Property_Remarks = "Remarks";		            
		public const string Property_LockReason = "LockReason";		            
		public const string Property_DepartmentId = "DepartmentId";		            
		public const string Property_JoiningSemesterId = "JoiningSemesterId";		            
		public const string Property_JoiningDate = "JoiningDate";		            
		public const string Property_LeavingDate = "LeavingDate";		            
		public const string Property_TrackingId = "TrackingId";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_History = "History";		            
		public const string Property_ActiveRfidNo = "ActiveRfidNo";		            
		public const string Property_ProfilePictureImageUrl = "ProfilePictureImageUrl";		            
		public const string Property_SignatureImageUrl = "SignatureImageUrl";		            
		public const string Property_IsForceTwoFactorAuth = "IsForceTwoFactorAuth";		            
		public const string Property_IsAllowMultiLogin = "IsAllowMultiLogin";		            
		public const string Property_AuthTokenJson = "AuthTokenJson";		            
              
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public const string Property_IsSelected = "IsSelected";
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public const string Property_IsAlreadyAdded = "IsAlreadyAdded";
	  #endregion
      
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public bool IsAlreadyAdded = true;
      
     
     
      
        public static User_Account GetNew(Int32 id=0)
        {
           User_Account obj = new User_Account();
               obj.Id = id;
                obj.UserName = string.Empty ; 
                obj.FullName = string.Empty ; 
                obj.BanglaName = null ; 
                obj.DateOfBirth = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.PlaceOfBirthCity = null ; 
                obj.PlaceOfBirthCountryId = null ; 
                obj.GenderEnumId = 0 ; 
                obj.ReligionEnumId = 0 ; 
                obj.BloodGroupEnumId = 0 ; 
                obj.MaritalStatusEnumId = 0 ; 
                obj.FatherName = string.Empty ; 
                obj.MotherName = string.Empty ; 
                obj.SpouseName = null ; 
                obj.UserMobile = null ; 
                obj.UserMobile2 = null ; 
                obj.UserMobilePrivacyEnumId = 0 ; 
                obj.IsVerifiedUserMobile = false ; 
                obj.UserEmail = null ; 
                obj.UserEmailPrivacyEnumId = 0 ; 
                obj.IsVerifiedUserEmaill = false ; 
                obj.EmergencyContactPersonName = null ; 
                obj.EmergencyMobile = null ; 
                obj.EmergencyContactAdress = null ; 
                obj.EmergencyContactRelationshipId = null ; 
                obj.Height = null ; 
                obj.IsLate = false ; 
                obj.CampusId = 0 ; 
                obj.Nationality = string.Empty ; 
                obj.NationalIdNumber = null ; 
                obj.BirthCertificateNumber = null ; 
                obj.BankAccountNo = null ; 
                obj.BankName = null ; 
                obj.PassportNo = null ; 
                obj.UserTypeEnumId = 0 ; 
                obj.UserStatusEnumId = 0 ; 
                obj.IsMigrate = false ; 
                obj.MigrationId = null ; 
                obj.IsProfileCompleted = false ; 
                obj.IsUserCanEditProfile = false ; 
                obj.IsDocumentPending = false ; 
                obj.IsApproved = false ; 
                obj.Password = string.Empty ; 
                obj.PasswordSalt = string.Empty ; 
                obj.LastLoginDate = null ; 
                obj.LastPasswordChangedDate = null ; 
                obj.LastLockoutDate = null ; 
                obj.FailedPasswordAttemptCount = 0 ; 
                obj.FailedPasswordAttemptWindowStart = null ; 
                obj.FailedPasswordAnswerAttemptCount = 0 ; 
                obj.FailedPasswordAnswerAttemptWindowStart = null ; 
                obj.Remarks = null ; 
                obj.LockReason = null ; 
                obj.DepartmentId = 0 ; 
                obj.JoiningSemesterId = null ; 
                obj.JoiningDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LeavingDate = null ; 
                obj.TrackingId = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.History = null ; 
                obj.ActiveRfidNo = null ; 
                obj.ProfilePictureImageUrl = null ; 
                obj.SignatureImageUrl = null ; 
                obj.IsForceTwoFactorAuth = false ; 
                obj.IsAllowMultiLogin = false ; 
                obj.AuthTokenJson = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
