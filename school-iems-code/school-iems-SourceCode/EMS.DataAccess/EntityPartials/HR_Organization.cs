//File: Entity Partial
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    public partial class HR_Organization
	{
          #region Enum Property
           [Flags]
           public enum TypeEnum
           {
                none = 0,
           }
           [Flags]
           public enum StatusEnum
           {
                none = 0,
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

        private static void  GetNewExtra(HR_Organization obj)
          {
          
          }
        
	}
}
