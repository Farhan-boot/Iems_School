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
    public partial class Aca_ClassTakenByStudent
    {
        #region Enum Property
        public enum EnrollTypeEnum
        {
            Regular = 0,
            Referred = 1,
            Backlog = 2
        }


        /// <summary>
        /// CourseStatus:Todo Drop,Retake/Isretake,
        /// public enum CourseStatusBit
        /*
            /// <summary>
            /// 
            /// </summary>
            ValidInvalidBitMask = 1,
            /// <summary>
            /// 
            /// </summary>
            DropBitMask = 2,
            /// <summary>
            /// 
            /// </summary>
            UnofficialWithdrawBitMask = 4,
            /// <summary>
            /// 
            /// </summary>
            RetakeBitMask = 8,
            /// <summary>
            /// 
            /// </summary>
            LockedBitMask = 16
        }*/
        /// </summary>

        //Registered,Retake,RetakeDrop,Drop,Cancelled,PreBooked = 3
        //Lock,Valid,Invalid

        public enum RegistrationStatusEnum
        {
            FreshRegistration = 0,
            RetakeRegistration = 1,
            DropForRetakeOrImprovement = 2,
            Drop = 3,
            Cancelled = 4,
            ImprovementRegistration = 5
            //PreBooked = 5
            //PreRegCancelled = 3,
            //RegPending = 4,
            //RegCompleted = 5,
            //RegCancelled = 6
        }
        //todo make isValid in db, drop percent info
        public enum StatusEnum
        {
            Invalid = 0,
            Valid = 1,

            //Withdraw = 2,
            //Cancelled = 3,
        }
        #endregion Enum Property
        #region Enum Property
        //[DataMember]
        //public EnrollTypeEnum EnrollType
        //{
        //    get
        //    {
        //        return (EnrollTypeEnum)EnrollTypeEnumId;
        //    }
        //    set
        //    {
        //        EnrollTypeEnumId = (Byte)value;
        //    }
        //}

        [DataMember]
        public Aca_Exam.ExamTypeEnum EnrollType
        {
            get
            {
                return (Aca_Exam.ExamTypeEnum)EnrollTypeEnumId;
            }
            set
            {
                EnrollTypeEnumId = (Byte)value;
            }
        }
        [DataMember]
        public RegistrationStatusEnum RegistrationStatus
        {
            get
            {
                return (RegistrationStatusEnum)RegistrationStatusEnumId;
            }
            set
            {
                RegistrationStatusEnumId = (Byte)value;
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
        #endregion Enum Property

        private static void GetNewExtra(Aca_ClassTakenByStudent obj)
        {

        }

    }
}
