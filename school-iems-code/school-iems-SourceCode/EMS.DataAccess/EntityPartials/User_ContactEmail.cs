using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class User_ContactEmail
    {
        #region Enum Property
        public enum ContactEmailTypeEnum
        {
            PersonalEmail = 0,
            OfficeEmail = 1,
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
        public ContactEmailTypeEnum ContactEmailType
        {
            get
            {
                return (ContactEmailTypeEnum)ContactEmailTypeEnumId;
            }
            set
            {
                ContactEmailTypeEnumId = (Byte)value;
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

        private static void GetNewExtra(User_ContactEmail obj)
        {
            obj.ContactEmailTypeEnumId = (byte)ContactEmailTypeEnum.PersonalEmail;
        }

    }
}
