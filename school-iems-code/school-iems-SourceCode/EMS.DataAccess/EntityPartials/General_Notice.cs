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
    public partial class General_Notice
	{
          #region Enum declaration
           [Flags]
           public enum VisibilityTypeEnum
           {
                none = 0,
           }
            [DataMember]
            public VisibilityTypeEnum VisibilityType
            {
                get{return (VisibilityTypeEnum)VisibilityTypeEnumId;}
                set
                {
                    VisibilityTypeEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(General_Notice obj)
          {
          
          }
        
	}
}
