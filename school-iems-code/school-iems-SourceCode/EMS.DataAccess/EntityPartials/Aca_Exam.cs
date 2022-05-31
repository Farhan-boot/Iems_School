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
    public partial class Aca_Exam
    {
        public Aca_ExamSupple ExamSupple { get; set; }

        #region Enum declaration
        //[Flags]
        //Regular = 0,
        //Backlog = 1,
        //Referred = 2
        //public enum ProgramTypeEnum
        //{
        //MidTerm = 0,
        //FinalTerm = 1,
        //ImprovementMidTerm = 2,
        //ImprovementFinalTerm = 3,
        //SpecialExam = 4,
        //OtherExam = 5,
        //}
        [Flags]
        public enum ExamTypeEnum
        {
            FinalTerm = 0,
            ImprovementMidTerm = 1,//Improvement Term 1
            ImprovementFinalTerm = 2,
            //ImprovementTerm1 = 3,
            //ImprovementTerm2 = 4,
            //ImprovementFinalTerm2 = 3,
            //SpecialExam = 4,
            //OtherExam = 5,
        }
        #endregion Enum declaration
        #region Enum Property
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
        public ExamTypeEnum ExamType
        {
            get { return (ExamTypeEnum)ExamTypeEnumId; }
            set
            {
                ExamTypeEnumId = (Byte)value;
            }
        }
        #endregion Enum Property


        private static void GetNewExtra(Aca_Exam obj)
        {

        }

    }
}
