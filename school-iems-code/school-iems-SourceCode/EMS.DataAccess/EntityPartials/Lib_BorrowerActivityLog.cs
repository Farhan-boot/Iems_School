using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Lib_BorrowerActivityLog
	{
         #region Enum Property
            public enum BorrowerActivityEnum
            {
                none = 0,
            }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public BorrowerActivityEnum BorrowerActivity
        {
            get
            {
                return (BorrowerActivityEnum)BorrowerActivityEnumId;
            }
            set
            {
                BorrowerActivityEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void  GetNewExtra(Lib_BorrowerActivityLog obj)
          {
          
          }
        
	}
}
