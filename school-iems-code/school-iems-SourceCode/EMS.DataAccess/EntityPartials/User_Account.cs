using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.CoreLibrary;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class User_Account
    {
        //todo delted col
        //IsApproved
        //IsLockedOut
        //BankId=>BankName
        //Weight
        #region Enum Property
        public enum UserStatusEnum
        {
            Active = 0,
            Inactive = 1,
            LockedWithReason = 2,
            PasswordFailedLocked = 3,
        }

        public enum UserTypeEnum
        {
            Applicant = 0,
            Student = 1,
            Employee = 2,
            Guardian = 3,
            None=5
        }

        public enum UserCategoryEnum
        {
            Army = 1,
            Navy = 2,
            AirForce = 3,
            Civil = 4,
        }

        public enum ReligionEnum
        {
            Unknown = 0,
            Muslim = 1,
            Christianity = 2,
            Hinduism = 3,
            Buddhism = 4,
            Other = 5,
        }
       
        public enum MaritalStatusEnum
        {
            Unknown = 0,
            Single = 1,
            Married = 2,
            Separated = 3,
            Divorced = 4,
            Widowed = 5
        }

        public enum GenderEnum
        {
            Unknown = 0,
            Male = 1,
            Female = 2,
            Other = 3,
        }
        public enum UserContactPrivacyEnum
        {
            AdminstrationOnly = 0,
            AllEmployees = 1,
            StudentAndAllEmployees = 2,
            Public = 3
        }
        //[Flags]
        //public enum UserMobilePrivacyEnum
        //{
        //    none = 0,
        //}
        //[Flags]
        //public enum UserEmailPrivacyEnum
        //{
        //    none = 0,
        //}
        //[Flags]
        //public enum EmergencyContactPersonRelationEnum
        //{
        //    none = 0,
        //}



        #endregion Enum Property

        #region Enum Property
        [DataMember]
        public User_Account.GenderEnum Gender
        {
            get
            {
                return (User_Account.GenderEnum)GenderEnumId;
            }
            set
            {
                GenderEnumId = (Byte)value;
            }
        }
        [DataMember]
        public User_Account.ReligionEnum Religion
        {
            get
            {
                return (User_Account.ReligionEnum)ReligionEnumId;
            }
            set
            {
                ReligionEnumId = (Byte)value;
            }
        }
        [DataMember]
        public User_Account.MaritalStatusEnum MaritalStatus
        {
            get
            {
                return (User_Account.MaritalStatusEnum)MaritalStatusEnumId;
            }
            set
            {
                MaritalStatusEnumId = (Byte)value;
            }
        }
        [DataMember]
        public EnumCollection.Common.BloodGroupEnum BloodGroup
        {
            get
            {
                return (EnumCollection.Common.BloodGroupEnum)BloodGroupEnumId;
            }
            set
            {
                BloodGroupEnumId = (Byte)value;
            }
        }
 
        [DataMember]
        public UserTypeEnum UserType
        {
            get
            {
                return (UserTypeEnum)UserTypeEnumId;
            }
            set
            {
                UserTypeEnumId = (Byte)value;
            }
        }
        [DataMember]
        public User_Account.UserStatusEnum UserStatus
        {
            get
            {
                return (User_Account.UserStatusEnum)UserStatusEnumId;
            }
            set
            {
                UserStatusEnumId = (Byte)value;
            }
        }
        [DataMember]
        public UserContactPrivacyEnum UserMobilePrivacy
        {
            get { return (UserContactPrivacyEnum)UserMobilePrivacyEnumId; }
            set
            {
                UserMobilePrivacyEnumId = (Byte)value;
            }
        }
        [DataMember]
        public UserContactPrivacyEnum UserEmailPrivacy
        {
            get { return (UserContactPrivacyEnum)UserEmailPrivacyEnumId; }
            set
            {
                UserEmailPrivacyEnumId = (Byte)value;
            }
        }
        //[DataMember]
        //public EmergencyContactPersonRelationEnum? EmergencyContactPersonRelation
        //{
        //    get { return EmergencyContactPerson != null ? (EmergencyContactPersonRelationEnum?)EmergencyContactPersonRelationEnumId : null; }
        //    set
        //    {
        //        EmergencyContactPersonRelationEnumId = (Nullable<Byte>)value;
        //    }
        //}
        #endregion Enum Property

        private static void GetNewExtra(User_Account obj)
        {
            obj.CampusId =1; //mist
            obj.JoiningDate = DateTime.Now;
        }
    }
}
