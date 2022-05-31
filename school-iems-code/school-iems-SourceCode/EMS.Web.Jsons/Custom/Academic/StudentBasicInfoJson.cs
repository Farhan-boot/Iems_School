using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Web.Jsons.Custom.Academic
{
    public class StudentBasicInfoJson
    {
        public string StudentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ClassRollNo { get; set; }
        //public string ExamRoll { get; set; }
        public string CGPA { get; set; }
        public string CreditCompleted { get; set; }
        public string CreditWaiver { get; set; }
        public string CreditTransfer { get; set; }
        public string Program { get; set; }
        public string Batch { get; set; }
        //public string LevelTerm { get; set; }
        public string UserMobile { get; set; }
        public string UserEmail { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        
        public string FatherMobile { get; set; }
        public string MotherMobile { get; set; }
        public string EmergencyMobile { get; set; }
        public string ImageUrl { get; set; }
        public string EnrollmentStatus { get; set; }
        public string EnrollmentType { get; set; }
        public dynamic DataBag { get; set; }

        public static StudentBasicInfoJson GetNew()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            StudentBasicInfoJson std = new StudentBasicInfoJson();
            std.StudentId = "";
            std.UserId = "";
            std.FullName = "";
            std.ClassRollNo = "";
            //std.ExamRoll ="";
            std.CGPA = "";
            std.CreditCompleted = "";
            std.CreditWaiver = "";
            std.CreditTransfer = "";
            std.Program = "";
            std.Batch = "";
            //std.LevelTerm ="";
            std.UserMobile = "";
            std.UserEmail = "";
            std.FatherName = "";
            std.MotherName = "";

            std.FatherMobile = "";
            std.MotherMobile = "";
            std.EmergencyMobile = "";
            std.ImageUrl = "";
            std.EnrollmentStatus = "";
            std.EnrollmentType = "";
            std.DataBag = new ExpandoObject();
            //std.ExamObj = new ExamJson();
            //std.CourseItemList = new List<CourseItemJson>();
            return std;
        }
    }
}
