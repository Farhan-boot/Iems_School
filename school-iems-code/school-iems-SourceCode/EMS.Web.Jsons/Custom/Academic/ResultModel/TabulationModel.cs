using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;

namespace EMS.Web.Jsons.Custom.Academic.ResultModel
{

    public class CourseResult
    {
        public string Id { get; set; }
        public string Grade { get; set; }
        public string GradePoint { get; set; }
        public string ClassId { get; set; }
        public string Color { get; set; }
        public string CourseCode { get; set; }
        public float? Credit { get; set; }


        public static CourseResult GetNew()
        {
            CourseResult obj = new CourseResult();
            obj.Id = "0";
            obj.Grade = "-";
            obj.GradePoint = "-";
            obj.ClassId = "0";
            obj.Color = "BLACK";
            obj.CourseCode = "";
            obj.Credit = null;
            return obj;
        }
    }

    //public class SemesterResult
    //{
    //    public long Id { get; set; }
    //    public long StudentId { get; set; }
    //    public long SemesterId { get; set; }
    //    public float TotalCreditHour { get; set; }
    //    public float EarnedCreditHour { get; set; }
    //    public float GPA { get; set; }
    //    public byte ResultTypeEnumId { get; set; }
    //    public bool IsLevelDrop { get; set; }
    //    public int TotalFailedInTheory { get; set; }
    //    public int TotalFailedInNonTheory { get; set; }
    //    public string Remark { get; set; }
    //}

    public class ResultStatistics
    {
        //public string TotalStudent { get; set; }
        public int TotalMale { get; set; }
        public int TotalFemale { get; set; }
        //public string TotalNumberAppeared { get; set; }
        //public string TotalMaleAppeared { get; set; }
        //public string TotalFemaleAppeared { get; set; }
        //public string TotalPassed { get; set; }
        public int TotalMalePassed { get; set; }
        public int TotalFemalePassed { get; set; }
        //public string TotalPassedPercentage { get; set; }
        public string TotalMalePassedPercentage { get; set; }
        public string TotalFealePassedPercentage { get; set; }
    }

    public class ResultStatisticsByGrade 
    {
        /// <summary>
        /// Total Student Get This Grade
        /// </summary>
        public int TotalStudentGet { get; set; }
        public float Percentage { get; set; }
        public string Grade { get; set; }
        public float GradePoint { get; set; }

        public static List<ResultStatisticsByGrade> GetNewList(List<Aca_GradingPolicyOption> gradingPolicyOptionList)
        {
            var objList = new List<ResultStatisticsByGrade>();


            gradingPolicyOptionList = gradingPolicyOptionList
                .Where(x => x.GradePoint > 0.0 || x.Grade == "F").ToList();

            foreach (var entity in gradingPolicyOptionList)
            {
                var obj = new ResultStatisticsByGrade();

                obj.Grade = entity.Grade;
                obj.GradePoint = entity.GradePoint;

                objList.Add(obj);
            }

            return objList;

        }
    }

}
