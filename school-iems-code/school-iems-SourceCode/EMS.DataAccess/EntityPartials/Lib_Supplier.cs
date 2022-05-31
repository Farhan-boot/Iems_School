using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Lib_Supplier
	{
         #region Enum Property
            public enum SupplierTypeEnum
            {
                none = 0,
            }
            public enum SupplierStatusEnum
            {
                none = 0,
            }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public SupplierTypeEnum SupplierType
        {
            get
            {
                return (SupplierTypeEnum)SupplierTypeEnumId;
            }
            set
            {
                SupplierTypeEnumId = (Byte)value;
            }
        }
        [DataMember]
        public SupplierStatusEnum SupplierStatus
        {
            get
            {
                return (SupplierStatusEnum)SupplierStatusEnumId;
            }
            set
            {
                SupplierStatusEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void  GetNewExtra(Lib_Supplier obj)
          {
          
          }
        
	}
}
