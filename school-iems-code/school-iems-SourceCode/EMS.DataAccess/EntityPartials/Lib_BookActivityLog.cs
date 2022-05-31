using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Lib_BookActivityLog
	{
         #region Enum Property
            public enum BookActivityEnum
            {
                none = 0,
            }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public BookActivityEnum BookActivity
        {
            get
            {
                return (BookActivityEnum)BookActivityEnumId;
            }
            set
            {
                BookActivityEnumId = (Int32)value;
            }
        }
        #endregion Enum Property

        private static void  GetNewExtra(Lib_BookActivityLog obj)
          {
          
          }
        
	}
}
