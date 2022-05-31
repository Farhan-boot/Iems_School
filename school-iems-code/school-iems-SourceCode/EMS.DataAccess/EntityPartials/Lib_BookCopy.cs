using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Lib_BookCopy
    {
        #region Enum Property
        public enum BorrowingAllowedEnum
        {
            NotAllowed = 0,
            Allowed = 1
        }
        public enum AvailabilityEnum
        {
            NotAvailable = 0,
            Available = 1
        }
        public enum ReservationEnum
        {
            NotReserved = 0,
            Reserved = 1
        }
        public enum DeletedEnum
        {
            UnDeleted = 0,
            Deleted = 1
        }
        public enum CopySourceEnum
        {
            DirectBuy = 0,
            Donation = 1,
            Other = 2
        }
        public enum CopyStatusEnum
        {
            UnChanged = 0,
            Changed = 1,
            Missing = 2,
            Weeded = 3,
            Replaced = 4
        }
        public enum CopyTypeEnum
        {
            Printed = 0,
            Electronic = 1,
            Microfilm = 2,
            Photocopy = 3,
            SerialPublication = 4,
            Others = 5
        }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public Lib_Book.CategoryEnum Category
        {
            get
            {
                return (Lib_Book.CategoryEnum)CategoryEnumId;
            }
            set
            {
                CategoryEnumId = (Byte)value;
            }
        }
        [DataMember]
        public CopySourceEnum CopySource
        {
            get
            {
                return (CopySourceEnum)CopySourceEnumId;
            }
            set
            {
                CopySourceEnumId = (Byte)value;
            }
        }
        [DataMember]
        public BorrowingAllowedEnum BorrowingAllowed
        {
            get
            {
                return (BorrowingAllowedEnum)BorrowingAllowedEnumId;
            }
            set
            {
                BorrowingAllowedEnumId = (Byte)value;
            }
        }
        [DataMember]
        public AvailabilityEnum Availability
        {
            get
            {
                return (AvailabilityEnum)AvailabilityEnumId;
            }
            set
            {
                AvailabilityEnumId = (Byte)value;
            }
        }
        [DataMember]
        public ReservationEnum Reservation
        {
            get
            {
                return (ReservationEnum)ReservationEnumId;
            }
            set
            {
                ReservationEnumId = (Byte)value;
            }
        }
        [DataMember]
        public CopyStatusEnum CopyStatus
        {
            get
            {
                return (CopyStatusEnum)CopyStatusEnumId;
            }
            set
            {
                CopyStatusEnumId = (Byte)value;
            }
        }
        [DataMember]
        public DeletedEnum Deleted
        {
            get
            {
                return (DeletedEnum)DeletedEnumId;
            }
            set
            {
                DeletedEnumId = (Byte)value;
            }
        }
        [DataMember]
        public CopyTypeEnum CopyType
        {
            get
            {
                return (CopyTypeEnum)CopyTypeEnumId;
            }
            set
            {
                CopyTypeEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(Lib_BookCopy obj)
        {
            obj.DateAcquired = DateTime.Now;
            obj.PermanentLibraryId = 1;
            obj.PresentLibraryId = 1;
            obj.BorrowingAllowedEnumId = (byte)BorrowingAllowedEnum.Allowed;
            obj.AvailabilityEnumId = (byte)AvailabilityEnum.Available;
            obj.ReservationEnumId = (byte)ReservationEnum.NotReserved;
            obj.CopyTypeEnumId = (byte)CopyTypeEnum.Printed;
            obj.DeletedEnumId = (byte)DeletedEnum.UnDeleted;
        }

    }
}
