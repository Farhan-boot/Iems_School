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
    public partial class Aca_StudyTerm
	{
        #region Enum declaration
        #endregion Enum declaration
        #region Enum Property
        #endregion Enum Property

        [DataMember]
        public Aca_Semester.SemesterDurationEnum SemesterDuration
        {
            get
            {
                return (Aca_Semester.SemesterDurationEnum)SemesterDurationEnumId;
            }
            set
            {
                SemesterDurationEnumId = (int)value;
            }
        }


        private static void  GetNewExtra(Aca_StudyTerm obj)
          {
          
          }
        
	}
}
