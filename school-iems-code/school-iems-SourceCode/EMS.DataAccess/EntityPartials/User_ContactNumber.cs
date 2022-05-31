using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class User_ContactNumber
    {
        #region Enum Property
        public enum ContactNumberTypeEnum
        {
            PersonalMobile = 0,
            OfficeMobile = 1,
            HomePhone = 2,
            OfficePhone = 3,
            OfficeIntercom = 4,
            ArmyTelephone = 5,// should remove
            HomeFax = 6,
            OfficeFax = 7
        }
        public enum PrivacyEnum
        {
            Public = 0,
            StudentParentEmployee = 1,
            EmployeeOnly = 2,
            HrOnly = 3,
            Custom = 4
        }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public ContactNumberTypeEnum ContactNumberType
        {
            get
            {
                return (ContactNumberTypeEnum)ContactNumberTypeEnumId;
            }
            set
            {
                ContactNumberTypeEnumId = (Byte)value;
            }
        }
        [DataMember]
        public PrivacyEnum Privacy
        {
            get
            {
                return (PrivacyEnum)PrivacyEnumId;
            }
            set
            {
                PrivacyEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(User_ContactNumber obj)
        {
            obj.ContactNumberTypeEnumId = (byte)ContactNumberTypeEnum.PersonalMobile;
            obj.CountryId = 1;
        }

    }
}
