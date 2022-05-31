using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Web.Jsons.Custom.Admission.Report
{

    public class ProgramWiseAdmissionSummaryJson
    {
        public string ProgramName { get; set; }
        public int FacultyId { get; set; }
        public int DeptId { get; set; }
        public int TotalMale { get; set; }
        public int TotalFemale { get; set; }
        public int TotalPaid { get; set; }
        public int TotalUnpaid { get; set; }
        public int GrandTotal { get; set; }
        public int TodayTotal { get; set; }
    }

    public class FacultyWiseAdmissionSummaryJson
    {
        //public dynamic DataBag { get; set; }
        //DataBag = new System.Dynamic.ExpandoObject();
        public string FacultyName { get; set; }
        public int FacultyId { get; set; }
        public string SemesterName { get; set; }
        public int TotalMale { get; set; }
        public int TotalFemale { get; set; }
        public int TotalPaid { get; set; }
        public int TotalUnpaid { get; set; }
        public int GrandTotal { get; set; }
        public int TodayTotal { get; set; }
        public List<ProgramWiseAdmissionSummaryJson> ProgramDetailListJson { get; set; }
    }
}
