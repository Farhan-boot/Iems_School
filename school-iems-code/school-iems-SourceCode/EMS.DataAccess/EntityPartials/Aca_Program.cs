using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Aca_Program
    {
        #region Enum Property

        /// <summary>
        /// Undergraduate/Postgraduate
        /// </summary>
        public enum ProgramTypeEnum
        {
            Play = 1,
            Nursery = 2,
            KG = 3,
            One = 4,
            Two = 5,
            Three = 6,
            Four = 7,
            Five = 8,
            Six = 9,
            Seven = 10,
            Eight = 11,
            Nine = 12,
            Ten = 13,
            Eleven = 14,
            Twelve = 15
        }

        public enum LanguageMediumEnum
        {
            BanglaMedium = 1,
            EnglishMedium = 2,
            EnglishVersion = 3
        }

        public enum ClassTimingGroupEnum
        {
            Morning = 1,
            Day = 2,
            
        }
        public Aca_Program.ClassTimingGroupEnum ClassTimingGroup
        {
            get
            {
                return (Aca_Program.ClassTimingGroupEnum)ClassTimingGroupEnumId;
            }
            set
            {
                ClassTimingGroupEnumId = (Byte)value;
            }
        }
        

        

        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public ProgramTypeEnum ProgramType
        {
            get
            {
                return (ProgramTypeEnum)ProgramTypeEnumId;
            }
            set
            {
                ProgramTypeEnumId = (Byte)value;
            }
        }
        public LanguageMediumEnum LanguageMedium
        {
            get
            {
                return (LanguageMediumEnum)LanguageMediumEnumId;
            }
            set
            {
                LanguageMediumEnumId = (Byte)value;
            }
        }

        public ProgramTypeEnum RequiredProgramType
        {
            get
            {
                return (ProgramTypeEnum)RequiredProgramTypeEnumId;
            }
            set
            {
                RequiredProgramTypeEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(Aca_Program obj)
        {
        }

    }
}
