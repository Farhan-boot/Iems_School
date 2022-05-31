//File: Entity Partial Base
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using EMS.CoreLibrary.Helpers;

namespace EMS.DataAccess.Data
{

    #region TempRegistrableCourseJson

    public class CourseListForRegistrationJson
    {
        public long SemesterId { get; set; }
        public int StudentId { get; set; }
        public List<RegistrableCourseJson> ClassList { get; set; }

        public CourseListForRegistrationJson()
        {
            ClassList = new List<RegistrableCourseJson>();
        }
    }

    public class RegistrableCourseJson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? Batch { get; set; }
        public string SyllabusName { get; set; }
        public bool IsSelected { get; set; }
        public bool IsAlreadyAdded { get; set; }
        public List<ClassRoutineJson> ClassRoutineJsonList { get; set; }
        public byte RegistrableCourseStatusEnumId { get; set; }

        public string RegistrableCourseStatus
        {
            get
            {
                return ((Aca_Class.RegistrableCourseStatusEnum)RegistrableCourseStatusEnumId).ToString().AddSpacesToSentence();
            }
        }

        public int LevelTermId { get; set; }
        public string LevelTermName { get; set; }
    }

    public class ClassRoutineJson
    {
        public byte DayEnumId { get; set; }
        public string RoutineDetails { get; set; }
        public string GeneralRoomDetails { get; set; }
        public string RoomId { get; set; }
    }

    #endregion
}
