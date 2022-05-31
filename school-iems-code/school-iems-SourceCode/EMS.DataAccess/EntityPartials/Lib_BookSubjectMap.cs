using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Lib_BookSubjectMap
    {
        #region Enum Property
        public enum StatusEnum
        {
            UnChanged = 0,
            Replaced = 1
        }
        #endregion Enum Property
        #region Enum Property
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

        private static void GetNewExtra(Lib_BookSubjectMap obj)
        {

        }

    }
}
