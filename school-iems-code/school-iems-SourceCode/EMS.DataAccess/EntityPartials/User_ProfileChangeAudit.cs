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
    public partial class User_ProfileChangeAudit
	{
          #region Enum declaration
           [Flags]
           public enum FieldEnum
           {
                DepartmentChange = 1,
                UserNameChange =2,
                StudentNameChange = 3,
                FeeCodeChange = 4,
                CurriculumChange = 5,
                RegNumberChange=6,
                UgcUniqueIdChange=7,
                CampusChange=8,
           }
            [DataMember]
            public FieldEnum Field
            {
                get { return (FieldEnum)FieldEnumId;}
                set
                {
                    FieldEnumId = (byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(User_ProfileChangeAudit obj)
          {
          
          }
        
	}
}
