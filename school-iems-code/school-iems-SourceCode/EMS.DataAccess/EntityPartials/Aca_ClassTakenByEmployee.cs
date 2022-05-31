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
    public partial class Aca_ClassTakenByEmployee
    {
        #region Enum Property
        public enum RoleEnum
        {
            Faculty = 0,
            ExaminerRegularExam = 1,
            ExaminerReferredExam = 2,
            ExaminerBacklogExam = 3,
            ScrutinizerRegularExam = 4,
            ScrutinizerReferredExam = 5,
            ScrutinizerBacklogExam = 6,
        }
        public enum ExaminerRoleEnum
        {
            ExaminerRegularExam = 1,
            ExaminerReferredExam = 2,
            ExaminerBacklogExam = 3
        }
        public enum ScrutinizerRoleEnum
        {
            ScrutinizerRegularExam = 4,
            ScrutinizerReferredExam = 5,
            ScrutinizerBacklogExam = 6
        }
        public enum StatusEnum
        {
            Inactive = 0,
            Active = 1,
        }
        public enum SectionEnum
        {
            BothSection = 0,
            SectionA = 1,
            SectionB = 2,
        }
        #endregion Enum Property

        #region Enum Property
        [DataMember]
        public RoleEnum Role
        {
            get
            {
                return (RoleEnum)RoleEnumId;
            }
            set
            {
                RoleEnumId = (Byte)value;
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
        [DataMember]
        public SectionEnum Section
        {
            get
            {
                return (SectionEnum)SectionEnumId;
            }
            set
            {
                SectionEnumId = (Byte)value;
            }
        }
        #endregion Enum Property
        private static void GetNewExtra(Aca_ClassTakenByEmployee obj)
        {
            obj.RoleEnumId = (byte)RoleEnum.ExaminerRegularExam;
            obj.StatusEnumId = (byte)StatusEnum.Active;
            obj.SectionEnumId = (byte)SectionEnum.BothSection;
        }

    }
}
