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
    public partial class Aca_SemesterLevelTerm
	{
        #region Enum Property
        [DataMember]
        public Aca_Semester.StatusEnum Status
        {
            get
            {
                return (Aca_Semester.StatusEnum)StatusEnumId;
            }
            set
            {
                StatusEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void  GetNewExtra(Aca_SemesterLevelTerm obj)
          {
          
          }
        
	}
}
