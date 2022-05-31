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
    public partial class Adm_EducationBoard
    {
        #region Enum declaration
        [Flags]
        public enum BoardTypeEnum
        {
            Other = 0,
            BdGeneralEducationBoard = 1,
            ALevelOLevelBoard = 2,
            DakhilAlimBoard = 3,
            TechnicalDiplomaBoard = 4,
            GraduatePostGraduateExam = 5,
            

        }
        [DataMember]
        public BoardTypeEnum BoardType
        {
            get { return (BoardTypeEnum)BoardTypeEnumId; }
            set
            {
                BoardTypeEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(Adm_EducationBoard obj)
        {

        }

    }
}
