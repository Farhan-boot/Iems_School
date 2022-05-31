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
	public partial class Aca_CreditTransferCoursesJson 
	{
          public Int32 Id { get; set; }
          public Int32 StudentId { get; set; }
          public Int32 CreditTransfarId { get; set; }
          public String CurriculumCourseId { get; set; }
          public String TransferedCourseCode { get; set; }
          public String TransferedCourseName { get; set; }
          public Single AcceptedCourseCredit { get; set; }
          public String AcceptedGrade { get; set; }
          public Single AcceptedGradePoint { get; set; }
          public String Remarks { get; set; }
          public String History { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
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
	   
		public static void Map(this Aca_CreditTransferCourses entity, Aca_CreditTransferCoursesJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.StudentId = entity.StudentId ; 
			 toJson.CreditTransfarId = entity.CreditTransfarId ; 
             if(entity.CurriculumCourseId!=null)
			 toJson.CurriculumCourseId = entity.CurriculumCourseId.ToString() ; 
			 toJson.TransferedCourseCode = entity.TransferedCourseCode ; 
			 toJson.TransferedCourseName = entity.TransferedCourseName ; 
			 toJson.AcceptedCourseCredit = entity.AcceptedCourseCredit ; 
			 toJson.AcceptedGrade = entity.AcceptedGrade ; 
			 toJson.AcceptedGradePoint = entity.AcceptedGradePoint ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_CreditTransferCoursesJson json, Aca_CreditTransferCourses toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.StudentId=json.StudentId;
    			 toEntity.CreditTransfarId=json.CreditTransfarId;
                 toEntity.CurriculumCourseId = long.TryParse(json.CurriculumCourseId, out output) ? output : (Int64?)null;
                 toEntity.TransferedCourseCode = json.TransferedCourseCode?.Trim() ?? json.TransferedCourseCode;
                 toEntity.TransferedCourseName = json.TransferedCourseName?.Trim() ?? json.TransferedCourseName;
    			 toEntity.AcceptedCourseCredit=json.AcceptedCourseCredit;
                 toEntity.AcceptedGrade = json.AcceptedGrade?.Trim() ?? json.AcceptedGrade;
    			 toEntity.AcceptedGradePoint=json.AcceptedGradePoint;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_CreditTransferCourses> entityList, IList<Aca_CreditTransferCoursesJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_CreditTransferCoursesJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_CreditTransferCoursesJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_CreditTransferCoursesJson> jsonList, ICollection<Aca_CreditTransferCourses> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_CreditTransferCourses obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_CreditTransferCourses.GetNew();
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
