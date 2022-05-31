using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Lib_BookDeptMap
	{
         #region Enum Property
            public enum StatusEnum
            {
                none = 0,
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

        private static void  GetNewExtra(Lib_BookDeptMap obj)
          {
          
          }
        
	}
}
