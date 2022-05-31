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
    public partial class User_Relationship
	{
          #region Enum declaration
   
            [DataMember]
            public User_Account.GenderEnum Gender
            {
                get{return (User_Account.GenderEnum)GenderEnumId;}
                set
                {
                    GenderEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(User_Relationship obj)
          {
          
          }
        
	}
}
