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
    public partial class Adm_DegreeCategory
	{
          #region Enum declaration
 
            [DataMember]
            public User_Education.DegreeEquivalentEnum DegreeEquivalent
            {
                get{return (User_Education.DegreeEquivalentEnum)DegreeEquivalentEnumId;}
                set
                {
                    DegreeEquivalentEnumId = (Byte)value;
                }
            }
            [DataMember]
            public Adm_EducationBoard.BoardTypeEnum BoardType
            {
                get{return (Adm_EducationBoard.BoardTypeEnum)BoardTypeEnumId;}
                set
                {
                    BoardTypeEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(Adm_DegreeCategory obj)
          {
          
          }
        
	}
}
