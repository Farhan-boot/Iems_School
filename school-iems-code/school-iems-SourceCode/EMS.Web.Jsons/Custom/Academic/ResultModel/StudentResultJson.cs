using System;
using System.Collections.Generic;
using System.Dynamic;
using EMS.CoreLibrary.Helpers;

namespace EMS.Web.Jsons.Custom.Academic.ResultModel
{
    /// <summary>
    /// this classes used for display all kind of result
    /// </summary>
    public class StudentJson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ClassRoll { get; set; }
        public string ExamRoll { get; set; }
        public string RegNumber { get; set; }
        public string CGPA { get; set; }
        public string CurrentSemesterGPA { get; set; }
        public string Grade { get; set; }
        public string Level { get; set; }
        public string Term { get; set; }
        public string SemesterName { get; set; }
        public string SemesterId { get; set; }
        public bool IsSelected { get; set; }
        public dynamic DataBag { get; set; }
        public static StudentJson GetNew()
        {
            StudentJson std = new StudentJson();
            std.Id="";
            std.Name="";
            std.ClassRoll="";
            std.ExamRoll="";
            std.RegNumber="";
            std.CGPA="";
            std.CurrentSemesterGPA="";
            std.Grade="";
            std.Level="";
            std.Term = "";
            std.SemesterName="";
            std.SemesterId="";
            std.IsSelected = true;
            std.DataBag = new ExpandoObject();
            //std.ExamObj = new ExamJson();
            //std.CourseItemList = new List<CourseItemJson>();
            return std;
        }

       
    }
    
    public class CourseItemJson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string DeptName { get; set; }
        public string ProgramName { get; set; }
        public string Level { get; set; }
        public string Term { get; set; }
        public string ExamName { get; set; }
        public string ExamDate { get; set; }
        public string SemesterName { get; set; }
        public string CreditLoad { get; set; }
        public string TotallMark { get; set; }
        public bool IsLocked { get; set; }
        public string StudentCount { get; set; }
        public dynamic DataBag { get; set; }
        public float ConvertPercentage { get; set; }
        public int TotalStudent { get; set; }
        public int TotalAbsentInFinal { get; set; }

        public static CourseItemJson GetNew()
        {
            CourseItemJson course = new CourseItemJson();
            course.Id = "";
            course.Name = "";
            course.ShortName = "";
            course.Code = "";
            course.Category = "";
            course.DeptName = "";
            course.Level = "";
            course.Term = "";
            course.CreditLoad = "";
            course.SemesterName = "";
            course.ExamName = "";
            course.ExamDate = "";
            course.IsLocked = false;
            course.DataBag = new ExpandoObject();
            return course;
        }
        //public class LockStatusJson
        //{
        //    public string Id { get; set; }
        //}
    }
    public class MarkDetailJson
    {
        public string Id { get; set; }
        public string SettingId { get; set; }
        public string SettingName { get; set; }
        public string ContributedMark { get; set; }
        public string Mark { get; set; }
        public float ExamMark { get; set; }
        public string OldMark { get; set; }
        public float ConvertPercentage { get; set; }
        public string Grade { get; set; }
        public string GradePoint { get; set; }
        public bool IsLocked { get; set; }
        public dynamic DataBag { get; set; }
        public static MarkDetailJson GetNew()
        {
            MarkDetailJson markDetail = new MarkDetailJson();
            markDetail.Id = "";
            markDetail.SettingId = "";
            markDetail.SettingName = "";
            markDetail.ContributedMark = "0";
            markDetail.Mark = "0";
            markDetail.OldMark = "0";
            markDetail.ExamMark = 0;
            markDetail.ConvertPercentage = 0;
            markDetail.Grade = "-";
            markDetail.GradePoint = "0";
            markDetail.IsLocked = false;
            markDetail.DataBag = new ExpandoObject();
            return markDetail;
        }
    }

    //public class CourseTeacherJson
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public string RollType { get; set; }
    //}
    public class ExamJson
    {
        public string ExamName { get; set; }
        public string ExamId { get; set; }
        public string ExamType { get; set; }
        public string Level { get; set; }
        public string Term { get; set; }
        public string ExamDate { get; set; }
        public bool IsLocked { get; set; }
        public dynamic DataBag { get; set; }
        public static ExamJson GetNew()
        {
            ExamJson exam = new ExamJson();
            exam.ExamName = "";
            exam.ExamId = "";
            exam.ExamType = "";
            exam.ExamDate = "";
            exam.IsLocked = false;
            exam.DataBag = new ExpandoObject();
            //exam.StudentList = new List<StudentJson>();
            //exam.CourseItemList = new List<CourseItemJson>();
            return exam;
        }
    }

}
