using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class UAC_PassReset
    {
        #region Enum Property
        public enum TypeEnum
        {
            ResetRequested = 0,
            ResetPasswordBySelf = 1,
            ResetPasswordByAdmin = 2
        }
        public enum StatusEnum
        {
            Requested = 0,
            Reseted = 1
        }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public TypeEnum Type
        {
            get
            {
                return (TypeEnum)TypeEnumId;
            }
            set
            {
                TypeEnumId = (Byte)value;
            }
        }
        [DataMember]
        public StatusEnum Status
        {
            get
            {
                return (StatusEnum)StatusEnumId;
            }
            set
            {
                StatusEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(UAC_PassReset obj)
        {

        }

    }
}
