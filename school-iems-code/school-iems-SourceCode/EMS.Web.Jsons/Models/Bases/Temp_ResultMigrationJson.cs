//File:Json Model Base Partial
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

using EMS.Framework;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
	public partial class Temp_ResultMigrationJson 
	{
          public Int32 Id { get; set; }
          public Nullable<Int32> StudentId { get; set; }
          public String UserName { get; set; }
          public Nullable<Int32> ProgramId { get; set; }
          public String CurriculumId { get; set; }
          public Nullable<Int32> DeptBatchId { get; set; }
          public Int32 SemesterId { get; set; }
          public String ClassId { get; set; }
          public String CourseCode { get; set; }
          public Nullable<Single> CourseCredit { get; set; }
          public Nullable<Single> AttendanceMark { get; set; }
          public Nullable<Single> AssignmentMark { get; set; }
          public Nullable<Single> MidTermMark { get; set; }
          public Nullable<Single> FinalTermMark { get; set; }
          public Nullable<Single> TotalMark { get; set; }
          public String Grade { get; set; }
          public Nullable<Single> GradePoint { get; set; }
          public Nullable<DateTime> CreateDate { get; set; }
          public Byte TypeEnumId { get; set; }
          public string Type { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public Byte ResultGenerateTypeEnumId { get; set; }
          public string ResultGenerateType { get; set; }
          public String Remarks { get; set; }
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public bool IsAlreadyAdded { get; set; }
	}
    #region Mapper
     /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
	public static partial class EntityMapper
	{
	   
		public static void Map(this Temp_ResultMigration entity, Temp_ResultMigrationJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.StudentId = entity.StudentId ; 
			 toJson.UserName = entity.UserName ; 
			 toJson.ProgramId = entity.ProgramId ; 
             if(entity.CurriculumId!=null)
			 toJson.CurriculumId = entity.CurriculumId.ToString() ; 
			 toJson.DeptBatchId = entity.DeptBatchId ; 
			 toJson.SemesterId = entity.SemesterId ; 
             if(entity.ClassId!=null)
			 toJson.ClassId = entity.ClassId.ToString() ; 
			 toJson.CourseCode = entity.CourseCode ; 
			 toJson.CourseCredit = entity.CourseCredit ; 
			 toJson.AttendanceMark = entity.AttendanceMark ; 
			 toJson.AssignmentMark = entity.AssignmentMark ; 
			 toJson.MidTermMark = entity.MidTermMark ; 
			 toJson.FinalTermMark = entity.FinalTermMark ; 
			 toJson.TotalMark = entity.TotalMark ; 
			 toJson.Grade = entity.Grade ; 
			 toJson.GradePoint = entity.GradePoint ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.TypeEnumId = entity.TypeEnumId ; 
             if(entity.TypeEnumId!=null)
             toJson.Type = entity.Type.ToString().AddSpacesToSentence();
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.ResultGenerateTypeEnumId = entity.ResultGenerateTypeEnumId ; 
             if(entity.ResultGenerateTypeEnumId!=null)
             toJson.ResultGenerateType = entity.ResultGenerateType.ToString().AddSpacesToSentence();
			 toJson.Remarks = entity.Remarks ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Temp_ResultMigrationJson json, Temp_ResultMigration toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.StudentId=json.StudentId;
                 toEntity.UserName = json.UserName?.Trim() ?? json.UserName;
    			 toEntity.ProgramId=json.ProgramId;
                 toEntity.CurriculumId = long.TryParse(json.CurriculumId, out output) ? output : (Int64?)null;
    			 toEntity.DeptBatchId=json.DeptBatchId;
    			 toEntity.SemesterId=json.SemesterId;
                 toEntity.ClassId = long.TryParse(json.ClassId, out output) ? output : (Int64?)null;
                 toEntity.CourseCode = json.CourseCode?.Trim() ?? json.CourseCode;
    			 toEntity.CourseCredit=json.CourseCredit;
    			 toEntity.AttendanceMark=json.AttendanceMark;
    			 toEntity.AssignmentMark=json.AssignmentMark;
    			 toEntity.MidTermMark=json.MidTermMark;
    			 toEntity.FinalTermMark=json.FinalTermMark;
    			 toEntity.TotalMark=json.TotalMark;
                 toEntity.Grade = json.Grade?.Trim() ?? json.Grade;
    			 toEntity.GradePoint=json.GradePoint;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.TypeEnumId=json.TypeEnumId;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.ResultGenerateTypeEnumId=json.ResultGenerateTypeEnumId;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Temp_ResultMigration> entityList, IList<Temp_ResultMigrationJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Temp_ResultMigrationJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Temp_ResultMigrationJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Temp_ResultMigrationJson> jsonList, ICollection<Temp_ResultMigration> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Temp_ResultMigration obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Temp_ResultMigration.GetNew();
                    json.Map(obj);
                    toEntityList.Add(obj);
                    
                }
                else
                {
                   json.Map(obj);
                }
            }
        }
	}
    #endregion
}
