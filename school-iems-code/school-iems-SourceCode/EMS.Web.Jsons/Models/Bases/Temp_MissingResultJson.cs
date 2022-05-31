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
	public partial class Temp_MissingResultJson 
	{
          public Int32 Id { get; set; }
          public Int32 StudentId { get; set; }
          public String UserName { get; set; }
          public Int32 ProgramId { get; set; }
          public String CurriculumId { get; set; }
          public Int32 DeptBatchId { get; set; }
          public String CourseCode { get; set; }
          public Single CourseCredit { get; set; }
          public Single MidTerm { get; set; }
          public Single FinalTerm { get; set; }
          public Single Assignment { get; set; }
          public Single OralTest { get; set; }
          public Single Attendance { get; set; }
          public Single TotalMark { get; set; }
          public String Grade { get; set; }
          public Single GradePoint { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 AcLgId { get; set; }
          public Byte TypeEnumId { get; set; }
          public string Type { get; set; }
          public String EmsCourseCode { get; set; }
          public String Remarks { get; set; }
          public String ResultCode { get; set; }
          public Nullable<Single> ResultCredit { get; set; }
          public Nullable<DateTime> ResultDate { get; set; }
          public String RegCode { get; set; }
          public Nullable<Single> RegCredit { get; set; }
          public Nullable<DateTime> RegDate { get; set; }
          public Nullable<Int32> ResultPkId { get; set; }
          public Nullable<Int32> RegPkId { get; set; }
          public Nullable<Int32> RegSemesterId { get; set; }
          public String ClassId { get; set; }
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
	   
		public static void Map(this Temp_MissingResult entity, Temp_MissingResultJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.StudentId = entity.StudentId ; 
			 toJson.UserName = entity.UserName ; 
			 toJson.ProgramId = entity.ProgramId ; 
			 toJson.CurriculumId = entity.CurriculumId.ToString() ; 
			 toJson.DeptBatchId = entity.DeptBatchId ; 
			 toJson.CourseCode = entity.CourseCode ; 
			 toJson.CourseCredit = entity.CourseCredit ; 
			 toJson.MidTerm = entity.MidTerm ; 
			 toJson.FinalTerm = entity.FinalTerm ; 
			 toJson.Assignment = entity.Assignment ; 
			 toJson.OralTest = entity.OralTest ; 
			 toJson.Attendance = entity.Attendance ; 
			 toJson.TotalMark = entity.TotalMark ; 
			 toJson.Grade = entity.Grade ; 
			 toJson.GradePoint = entity.GradePoint ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.AcLgId = entity.AcLgId ; 
			 toJson.TypeEnumId = entity.TypeEnumId ; 
             if(entity.TypeEnumId!=null)
             toJson.Type = entity.Type.ToString().AddSpacesToSentence();
			 toJson.EmsCourseCode = entity.EmsCourseCode ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.ResultCode = entity.ResultCode ; 
			 toJson.ResultCredit = entity.ResultCredit ; 
			 toJson.ResultDate = entity.ResultDate ; 
			 toJson.RegCode = entity.RegCode ; 
			 toJson.RegCredit = entity.RegCredit ; 
			 toJson.RegDate = entity.RegDate ; 
			 toJson.ResultPkId = entity.ResultPkId ; 
			 toJson.RegPkId = entity.RegPkId ; 
			 toJson.RegSemesterId = entity.RegSemesterId ; 
             if(entity.ClassId!=null)
			 toJson.ClassId = entity.ClassId.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Temp_MissingResultJson json, Temp_MissingResult toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.StudentId=json.StudentId;
                 toEntity.UserName = json.UserName?.Trim() ?? json.UserName;
    			 toEntity.ProgramId=json.ProgramId;
                 toEntity.CurriculumId = long.TryParse(json.CurriculumId, out output) ? output : 0;
    			 toEntity.DeptBatchId=json.DeptBatchId;
                 toEntity.CourseCode = json.CourseCode?.Trim() ?? json.CourseCode;
    			 toEntity.CourseCredit=json.CourseCredit;
    			 toEntity.MidTerm=json.MidTerm;
    			 toEntity.FinalTerm=json.FinalTerm;
    			 toEntity.Assignment=json.Assignment;
    			 toEntity.OralTest=json.OralTest;
    			 toEntity.Attendance=json.Attendance;
    			 toEntity.TotalMark=json.TotalMark;
                 toEntity.Grade = json.Grade?.Trim() ?? json.Grade;
    			 toEntity.GradePoint=json.GradePoint;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.AcLgId=json.AcLgId;
    			 toEntity.TypeEnumId=json.TypeEnumId;
                 toEntity.EmsCourseCode = json.EmsCourseCode?.Trim() ?? json.EmsCourseCode;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
                 toEntity.ResultCode = json.ResultCode?.Trim() ?? json.ResultCode;
    			 toEntity.ResultCredit=json.ResultCredit;
    			 toEntity.ResultDate=json.ResultDate;
                 toEntity.RegCode = json.RegCode?.Trim() ?? json.RegCode;
    			 toEntity.RegCredit=json.RegCredit;
    			 toEntity.RegDate=json.RegDate;
    			 toEntity.ResultPkId=json.ResultPkId;
    			 toEntity.RegPkId=json.RegPkId;
    			 toEntity.RegSemesterId=json.RegSemesterId;
                 toEntity.ClassId = long.TryParse(json.ClassId, out output) ? output : (Int64?)null;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Temp_MissingResult> entityList, IList<Temp_MissingResultJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Temp_MissingResultJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Temp_MissingResultJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Temp_MissingResultJson> jsonList, ICollection<Temp_MissingResult> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Temp_MissingResult obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Temp_MissingResult.GetNew();
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
