using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;

namespace EMS.Web.Jsons.Custom.Academic
{
    public class ClassInfoJson
    {
        
        public string ClassId { get; set; }
        public string Code { get; set; }
        public string ClassName { get; set; }
        public float CreditHour { get; set; }
        public string ClassStudyLevelTermName { get; set; }
        public string CourseCategory { get; set; }
        public int CourseCategoryEnumId { get; set; }
        public string CourseType { get; set; }
        public string CurriculumId { get; set; }
        public string CurriculumName { get; set; }
        public string CurriculumShortName { get; set; }
        public string CurriculumStudyLevelTermName { get; set; }
        public string CurriculumCourseId { get; set; }
        public string CurriculumCourseName { get; set; }
        public string BaseCourseId { get; set; }
        public string BaseCourseName { get; set; }
        public string SemesterId { get; set; }
        public string SemesterName { get; set; }
        public int ProgramId { get; set; }
        public string ProgramShortName { get; set; }
        public string ProgramShortShortName { get; set; }
        public string ProgramShortTitle { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        #region Use For Supple
        public int TotalApproved { get; set; }
        public int TotalPending { get; set; }
        public int TotalDisapproved { get; set; }
        public int TotalExaminer { get; set; }
        public int TotalReferred { get; set; }
        public int TotalBacklog { get; set; }
        #endregion


        public static ClassInfoJson GetNew()
        {
            var obj=new ClassInfoJson();
            return obj;
        }

    }


    public static partial class EntityMapper
    {
        public static void Map(this Aca_Class entity, ClassInfoJson toJson)
        {
            if (entity == null || toJson == null)
                return;


            //Class
            toJson.ClassId = entity.Id.ToString();
            toJson.Code = entity.Code;
            toJson.ClassName = entity.Name;
            toJson.CurriculumCourseId = entity.CurriculumCourseId.ToString();
            toJson.SemesterId = entity.SemesterId.ToString();
            toJson.ProgramId = entity.ProgramId;
            toJson.DepartmentId = entity.DepartmentId.ToString();

            //Aca_CurriculumCourse
            if (entity.Aca_CurriculumCourse!=null)
            {
                toJson.CreditHour = entity.Aca_CurriculumCourse.CreditHour;
                toJson.CourseCategory = entity.Aca_CurriculumCourse.CourseCategory.ToString();
                toJson.CourseType = entity.Aca_CurriculumCourse.CourseType.ToString();
                toJson.CurriculumId = entity.Aca_CurriculumCourse.CurriculumId.ToString();
                toJson.CurriculumCourseName = entity.Aca_CurriculumCourse.Name;
                toJson.CourseCategoryEnumId = entity.Aca_CurriculumCourse.CourseCategoryEnumId;

                //Aca_Curriculum
                if (entity.Aca_CurriculumCourse.Aca_Curriculum!=null)
                {
                    toJson.CurriculumName = entity.Aca_CurriculumCourse.Aca_Curriculum.Name;
                    toJson.CurriculumShortName = entity.Aca_CurriculumCourse.Aca_Curriculum.ShortName;
                }

                //Aca_StudyLevelTerm
                //if (entity.Aca_CurriculumCourse.Aca_StudyLevelTerm!=null)
                //{
                //    toJson.CurriculumStudyLevelTermName =
                //        entity.Aca_CurriculumCourse.Aca_StudyLevelTerm.Name;
                //}


                //Aca_Course
                toJson.BaseCourseId = entity.Aca_CurriculumCourse.CourseId.ToString();
                if (entity.Aca_CurriculumCourse.Aca_Course != null)
                {
                    toJson.BaseCourseName = entity.Aca_CurriculumCourse.Aca_Course.Name;
                }

                //HR_Department
                //if (entity.Aca_CurriculumCourse.HR_Department != null)
                //{
                //    toJson.DepartmentName = entity.Aca_CurriculumCourse.HR_Department.Name;
                //}


            }//end Aca_CurriculumCourse

            if (entity.Aca_StudyLevelTerm!=null)
            {
                toJson.ClassStudyLevelTermName = entity.Aca_StudyLevelTerm.Name;
            }

            if (entity.Aca_Semester!=null)
            {
                toJson.SemesterName = entity.Aca_Semester.Name;
            }

            if (entity.Aca_Program != null)
            {
                toJson.ProgramShortName = entity.Aca_Program.Name;
                toJson.ProgramShortShortName = entity.Aca_Program.ShortName;
                toJson.ProgramShortTitle = entity.Aca_Program.ShortTitle;
            }
            

        }
        public static void Map(this ICollection<Aca_Class> entityList, IList<ClassInfoJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.ClassId == entity.Id.ToString());

                if (obj == null)
                {
                    obj = new ClassInfoJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                    entity.Map(obj);
                }
            }
        }

    }

}
