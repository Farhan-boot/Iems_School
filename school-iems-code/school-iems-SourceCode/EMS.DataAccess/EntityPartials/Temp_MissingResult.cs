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
    public partial class Temp_MissingResult
	{
          #region Enum declaration
           [Flags]
           public enum TypeEnum
           {
                none = 0,
           }
            [DataMember]
            public TypeEnum Type
            {
                get{return (TypeEnum)TypeEnumId;}
                set
                {
                    TypeEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(Temp_MissingResult obj)
          {
          
          }
        
	}
}
