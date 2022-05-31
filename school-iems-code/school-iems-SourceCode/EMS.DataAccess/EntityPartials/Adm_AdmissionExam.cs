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
    public partial class Adm_AdmissionExam
    {
        #region Enum declaration
 
        public enum CircularStatusEnum
        {
            Upcoming = 0,
            Current = 1,
            Expired = 2,
            //Previous = 2,
            //Invalid = 4
        }
        [DataMember]
        public Aca_Program.ProgramTypeEnum ProgramType
        {
            get { return (Aca_Program.ProgramTypeEnum)ProgramTypeEnumId; }
            set
            {
                ProgramTypeEnumId = (Byte)value;
            }
        }
        [DataMember]
        public CircularStatusEnum CircularStatus
        {
            get { return (CircularStatusEnum)CircularStatusEnumId; }
            set
            {
                CircularStatusEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        //Ems 4 update
        public bool IsEnableProgramWiseBatchMap { get; set; }

        private static void GetNewExtra(Adm_AdmissionExam obj)
        {
            obj.IsEnableProgramWiseBatchMap = false;
        }

    }
}
