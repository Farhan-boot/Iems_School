//File: Entity Partial
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    public partial class Aca_ClassRoutine
    {
        #region Enum declaration
        [DataMember]
        public EnumCollection.Common.DayEnum Day
        {
            get { return (EnumCollection.Common.DayEnum)DayEnumId; }
            set
            {
                DayEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        public bool IsOverlap(Aca_ClassRoutine routine)
        {
            if (routine.ValidateStartTime < this.ValidateEndTime && this.ValidateStartTime < routine.ValidateEndTime &&  routine.DayEnumId == this.DayEnumId)
                return true;
            //if (routine.ValidateStartTime >= this.ValidateStartTime && routine.ValidateStartTime < this.ValidateEndTime && routine.DayEnumId==this.DayEnumId)
            //    return true;

            //if (routine.ValidateEndTime > this.ValidateStartTime && routine.ValidateEndTime <= this.ValidateEndTime && routine.DayEnumId == this.DayEnumId)
            //    return true;

            return false;
        }

        public DateTime ValidateStartTime
        {
            get { return new DateTime(1753, 1, 1, this.StartTime.Hour, this.StartTime.Minute, 0, 0);  }
            set
            {
                this.StartTime= new DateTime(1753, 1, 1, value.Hour, value.Minute, 0, 0);
            }
        }
        public DateTime ValidateEndTime
        {
            get { return new DateTime(1753, 1, 1, this.EndTime.Hour, this.EndTime.Minute, 0, 0); }
            set
            {
                this.EndTime = new DateTime(1753, 1, 1, value.Hour, value.Minute, 0, 0);
            }
        }


        private static void GetNewExtra(Aca_ClassRoutine obj)
        {
            obj.DayEnumId = (byte)EnumCollection.Common.DayEnum.Sunday;
            obj.ValidateStartTime = new DateTime(1753, 1, 1, 8, 0, 0, 0); //8am
            obj.ValidateEndTime = obj.StartTime.AddMinutes(50);
        }

        #region custom variables
        public string RoutineDetails
        {
            get
            {
                string _routineDetails = this.Day.ToString().Substring(0, 3)
                    +" " +
                    this.StartTime.ToString("h:mm tt")
                    +" - " +
                    this.EndTime.ToString("h:mm tt");
                return _routineDetails;
            }
        }
        /// <summary>
        /// Return: "Room: RoomNo/RoomName, BuildingShortName"
        /// </summary>
        public string ShortRoomDetails
        {
            get
            {
                string _roomDetails = "Room:[--]";

                if (this.General_Room != null)
                {
                    return this.General_Room.ShortRoomDetails;
                }
                return _roomDetails;
            }
        }
        public string RoomDetails
        {
            get
            {
                string _roomDetails = "Room:[--]";

                if (this.General_Room != null)
                {
                    return this.General_Room.RoomDetails;
                }
                return _roomDetails;
            }
        }
        #endregion custom variables
    }
}
