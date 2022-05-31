using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Lib_Borrower
    {
        #region Enum Property
        public enum BorrowerTypeEnum
        {
            Student = 0,
            Employee = 1
        }
        public enum BorrowerStatusEnum
        {
            Inactive = 0,
            Active = 1,
            Deleted = 2,
            TemporaryInactive = 3
        }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public BorrowerTypeEnum BorrowerType
        {
            get
            {
                return (BorrowerTypeEnum)BorrowerTypeEnumId;
            }
            set
            {
                BorrowerTypeEnumId = (Byte)value;
            }
        }
        [DataMember]
        public BorrowerStatusEnum BorrowerStatus
        {
            get
            {
                return (BorrowerStatusEnum)BorrowerStatusEnumId;
            }
            set
            {
                BorrowerStatusEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(Lib_Borrower obj)
        {
            obj.BorrowerTypeEnumId = (byte)BorrowerTypeEnum.Student;
            obj.BorrowerStatusEnumId = (byte)BorrowerStatusEnum.Active;
        }

    }
}
