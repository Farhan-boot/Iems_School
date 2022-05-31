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
    public partial class Aca_HomeWork
	{
          #region Enum declaration
           [Flags]
           public enum GroupEnum
           {
                none = 0,
           }
           [Flags]
           public enum HomeworkTypeEnum
           {
                none = 0,
           }
            [DataMember]
            public GroupEnum? Group
            {
                get { return GroupEnumId != null ? (GroupEnum?)GroupEnumId : null; }
                set
                {
                    GroupEnumId = (Nullable<Int32>)value;
                }
            }
            [DataMember]
            public HomeworkTypeEnum HomeworkType
            {
                get{return (HomeworkTypeEnum)HomeworkTypeEnumId;}
                set
                {
                    HomeworkTypeEnumId = (Int32)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(Aca_HomeWork obj)
          {
          
          }
        
	}
}
