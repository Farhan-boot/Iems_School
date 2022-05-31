using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Lib_Fine
	{
         #region Enum Property
            public enum LibraryFineTypeEnum
            {
                FinePaid = 0,
                LateFine = 1,
            }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public LibraryFineTypeEnum LibraryFineType
        {
            get
            {
                return (LibraryFineTypeEnum)LibraryFineTypeEnumId;
            }
            set
            {
                LibraryFineTypeEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void  GetNewExtra(Lib_Fine obj)
          {
          
          }
        
	}
}
