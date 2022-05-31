//File: Json Model Partial
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using EMS.CoreLibrary.Helpers;
using EMS.Framework;
using EMS.DataAccess.Data;
using EMS.Framework.Utils;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public partial class Aca_ClassJson
	{
        #region Custom Variables

        public int TotalStudent{get; set; }
        public int TotalFaculty{get; set; }
        public string ClassTakenByStudentHistory { get; set; }
        public IList<Aca_ClassTakenByEmployeeJson> Aca_ClassTakenByEmployeeJsonList { get; set; }

        public Aca_CurriculumCourseJson Aca_CurriculumCourseJson { get; set; }
        public string SemesterLevelTerm { get; set; }
        public string LevelTerm { get; set; }
	    public string SemesterLevel { get; set; }
        public string SemesterName { get; set; }
        public string BatchName { get; set; }
        public byte CourseTypeEnumId { get; set; }
        public byte CourseCategoryEnumId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentShortName { get; set; }
        public string ProgramName { get; set; }
        public string ProgramShortName { get; set; }
        public string ProgramShortTitle { get; set; }
        public string ClassSectionName { get; set; }
        public bool IsDepartmental { get; set; }
	    public List<string> EmployeeInRoleList { get; set; }
        public dynamic DataExtra = new System.Dynamic.ExpandoObject();

        public List<Aca_ClassRoutineJson> Aca_ClassRoutineJsonList { get; set; }
        public string CurriculumName { get; set; }
        public string CurriculumShortName { get; set; }
        public List<string> TeacherNameList { get; internal set; }
        /// <summary>
        /// used in student Dashboard
        /// </summary>
	    public string StudentRegistrationStatus { get; set; }
        public byte StudentRegistrationStatusEnumId { get; set; }

        public string StudentStatus { get; set; }

        //Aca_Class.Aca_OfferedCourse.Aca_CurriculumCourse
        #endregion
    }
    public static partial class EntityMapper
    {
        private static void MapExtra(Aca_Class entity, Aca_ClassJson toJson)
        {
            toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
            toJson.IsSelected = entity.IsSelected;
            toJson.TotalStudent = entity.TotalStudent;
            toJson.StudentRegistrationStatus = entity.StudentRegistrationStatus.ToString().AddSpacesToSentence();
            toJson.StudentRegistrationStatusEnumId = entity.StudentRegistrationStatusEnumId;
            toJson.StudentStatus = entity.StudentStatus.ToString().AddSpacesToSentence();
            toJson.ClassTakenByStudentHistory = entity.ClassTakenByStudentHistory;

            //add class routine, used in employee area-> dashboard and mark entry
            if (entity.Aca_ClassRoutine != null)
            {
                toJson.Aca_ClassRoutineJsonList = new List<Aca_ClassRoutineJson>();
                entity.Aca_ClassRoutine.Map(toJson.Aca_ClassRoutineJsonList);
            }

            if (entity.Aca_Program!=null)
            {
                toJson.ProgramName = entity.Aca_Program.Name;
                toJson.ProgramShortName = entity.Aca_Program.ShortName;
                toJson.ProgramShortTitle = entity.Aca_Program.ShortTitle;
            }


            if (entity.Aca_CurriculumCourse != null)
            {
                toJson.CourseTypeEnumId = entity.Aca_CurriculumCourse.CourseTypeEnumId;
                toJson.CourseCategoryEnumId = entity.Aca_CurriculumCourse.CourseCategoryEnumId;
                toJson.Aca_CurriculumCourseJson = new Aca_CurriculumCourseJson();
                entity.Aca_CurriculumCourse.Map(toJson.Aca_CurriculumCourseJson);

                if (entity.Aca_CurriculumCourse.Aca_Curriculum != null)
                {
                    toJson.CurriculumName = entity.Aca_CurriculumCourse.Aca_Curriculum.Name;
                    toJson.CurriculumShortName = entity.Aca_CurriculumCourse.Aca_Curriculum.ShortName;
                    //Cr.Hr:3.00, Co.Hr:3.00, Theory, Core, 2015 B.Sc. in AE [Aerospace] Syllabus

                    if (entity.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program != null)
                    {
                        toJson.ProgramName = entity.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.Name;
                        toJson.ProgramShortName = entity.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.ShortName;
                        toJson.ProgramShortTitle = entity.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.ShortTitle;
                    }

                }


            }
            //redundant added by Pavel, used in mark entry
            if (entity.HR_Department != null)
            {
                toJson.DepartmentName = entity.HR_Department.Name;
                toJson.DepartmentShortName = entity.HR_Department.ShortName;
            }

            if (entity.Aca_StudyLevelTerm != null)
            {
                //todo rename semester level term
                toJson.LevelTerm = entity.Aca_StudyLevelTerm.Name;
            }
            //if (entity.Aca_SemesterLevelTerm != null)
            //{
            //    toJson.SemesterLevelTerm = entity.Aca_SemesterLevelTerm.Name;
            //}
            if (entity.Aca_StudyLevelTerm?.Aca_StudyLevel != null)
            {
                toJson.SemesterLevel = entity.Aca_StudyLevelTerm.Aca_StudyLevel.Name;
            }
            if (entity.Aca_Semester != null)
            {
                toJson.SemesterName = entity.Aca_Semester.Name;
                toJson.SemesterLevelTerm = entity.Aca_Semester.Name;
            }
            if (entity.Aca_ClassSection != null)
            {
                toJson.ClassSectionName = entity.Aca_ClassSection.Name;
                //toJson.Name = $"{toJson.Name} [{toJson.ClassSectionName}]";
            }
            if (entity.Aca_DeptBatch != null)
            {
                toJson.BatchName = entity.Aca_DeptBatch.Name;
                //toJson.Name = $"{toJson.Name} [{toJson.ClassSectionName}]";
            }
            if (entity.EmployeeInRoleList!=null && entity.EmployeeInRoleList.Count>0)
            {
                toJson.EmployeeInRoleList=new List<string>();
                toJson.EmployeeInRoleList.AddRange(entity.EmployeeInRoleList);
            }
            if (entity.TeacherNameList != null && entity.TeacherNameList.Count > 0)
            {
                toJson.TeacherNameList = new List<string>();
                toJson.TeacherNameList.AddRange(entity.TeacherNameList);
            }
        }
        private static void MapExtra(Aca_ClassJson json, Aca_Class toEntity)
        {
            toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
            toEntity.IsSelected = json.IsSelected;
            toEntity.TotalStudent = json.TotalStudent;
        }
    }


}
