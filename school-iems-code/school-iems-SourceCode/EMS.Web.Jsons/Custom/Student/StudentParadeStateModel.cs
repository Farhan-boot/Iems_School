using System.Collections.Generic;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Custom.Student
{
    public class StudentParadeStateModel
    {
        public string Department { get; set; }
        public int DepartmentNo { get; set; }
        public int Level { get; set; }
        public int TotalMilitary { get; set; }
        public int TotalCivil { get; set; }
        public int PresentMilitary { get; set; }
        public int PresentCivil { get; set; }
        public int AbsentMilitary { get; set; }
        public int AbsentCivil { get; set; }
        public int AbsentSIQ { get; set; }
        public int AbsentCMH { get; set; }
        public int AbsentRSick { get; set; }
        public int AbsentLeave { get; set; }
        public int AbsentUnknown { get; set; }
        public List<Aca_ClassAttendance> ClassAttendanceList { get; set; }
        public List<Aca_ClassAttendanceSmsLog> ClassAttendanceSmsLogList { get; set; }
    }
}
