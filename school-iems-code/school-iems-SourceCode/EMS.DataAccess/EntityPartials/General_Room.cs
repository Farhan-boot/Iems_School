//File: Entity Partial
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    public partial class General_Room
    {
        #region Enum declaration
        [Flags]
        public enum TypeEnum
        {
            None = -1,
            ClassRoom = 0,
            Laboratory = 1,
            ReadingRoom = 2,
            FacultyRoom = 3,
            OfficeRoom = 4,
            CommonRoom = 5,
            ConferenceRoom = 6,
            HallRoom = 7,
            Auditorium = 8,
            TeaBar = 9,
            Cafe = 10,
            StoreRoom = 11,
            Workshop = 12,
            PrayerRoom = 13,
            WashRoom = 14,
            LivingRoom = 15,
            DiningRoom = 16,
            KitchenRoom = 17,
            Gymnasium = 18,
        }
        [Flags]
        public enum StatusEnum
        {
            Available = 0,
            UnderMaintenance = 1,
            UnderConstruction = 2,
            NotExist = 3,

        }
        [DataMember]
        public TypeEnum Type
        {
            get { return (TypeEnum)TypeEnumId; }
            set
            {
                TypeEnumId = (Byte)value;
            }
        }
        [DataMember]
        public StatusEnum Status
        {
            get { return (StatusEnum)StatusEnumId; }
            set
            {
                StatusEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(General_Room obj)
        {
            obj.RoomNo = "0";
            obj.FloorId = 0;
            obj.BuildingId = 1;
            obj.DepartmentId = 101;
        }

        #region custom variables
        public string RoomDetails
        {
            get
            {
                string _roomDetails = this.RoomNo + "-" + this.Name;
                if (this.General_Building != null)
                {
                    _roomDetails += ", " + this.General_Building.ShortName;
                    if (this.General_Building.General_Campus != null)
                    {
                        _roomDetails += ", " + this.General_Building.General_Campus.ShortName;
                    }
                }
                return _roomDetails;
            }
        }

        public string ShortRoomDetails
        {
            get
            {
                string _roomDetails = "Room:";
                if (this.RoomNo.IsValid())
                {
                     _roomDetails +=  this.RoomNo;
                }
                else if(this.Name.IsValid())
                {
                    _roomDetails += this.Name;
                }
                else
                {
                    _roomDetails += "[--]";
                }


                if (this.General_Building != null)
                {
                    _roomDetails += ", " + this.General_Building.ShortName;

                    //if (this.General_Building.General_Campus != null)
                    //{
                    //    _roomDetails += ", " + this.General_Building.General_Campus.ShortName;
                    //}
                }
                return _roomDetails;
            }
        }

        #endregion custom variables
    }
}
