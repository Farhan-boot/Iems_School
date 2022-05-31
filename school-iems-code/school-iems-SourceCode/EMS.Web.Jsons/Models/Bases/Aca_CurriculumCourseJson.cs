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
	public partial class Aca_CurriculumCourseJson 
	{
          public String Id { get; set; }
          public String CourseId { get; set; }
          public String Code { get; set; }
          public String Name { get; set; }
          public String ShortName { get; set; }
          public String Description { get; set; }
          public String CurriculumId { get; set; }
          public Single CreditLoad { get; set; }
          public Single CreditHour { get; set; }
          public Byte CourseCategoryEnumId { get; set; }
          public string CourseCategory { get; set; }
          public Byte CourseTypeEnumId { get; set; }
          public string CourseType { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public String ElectiveGroupId { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public String History { get; set; }
          public Int32 OrderNo { get; set; }
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
	   
		public static void Map(this Aca_CurriculumCourse entity, Aca_CurriculumCourseJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.CourseId = entity.CourseId.ToString() ; 
			 toJson.Code = entity.Code ; 
			 toJson.Name = entity.Name ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.Description = entity.Description ; 
			 toJson.CurriculumId = entity.CurriculumId.ToString() ; 
			 toJson.CreditLoad = entity.CreditLoad ; 
			 toJson.CreditHour = entity.CreditHour ; 
			 toJson.CourseCategoryEnumId = entity.CourseCategoryEnumId ; 
             if(entity.CourseCategoryEnumId!=null)
             toJson.CourseCategory = entity.CourseCategory.ToString().AddSpacesToSentence();
			 toJson.CourseTypeEnumId = entity.CourseTypeEnumId ; 
             if(entity.CourseTypeEnumId!=null)
             toJson.CourseType = entity.CourseType.ToString().AddSpacesToSentence();
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
             if(entity.ElectiveGroupId!=null)
			 toJson.ElectiveGroupId = entity.ElectiveGroupId.ToString() ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.History = entity.History ; 
			 toJson.OrderNo = entity.OrderNo ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_CurriculumCourseJson json, Aca_CurriculumCourse toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.CourseId = long.TryParse(json.CourseId, out output) ? output : 0;
                 toEntity.Code = json.Code?.Trim() ?? json.Code;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
                 toEntity.CurriculumId = long.TryParse(json.CurriculumId, out output) ? output : 0;
    			 toEntity.CreditLoad=json.CreditLoad;
    			 toEntity.CreditHour=json.CreditHour;
    			 toEntity.CourseCategoryEnumId=json.CourseCategoryEnumId;
    			 toEntity.CourseTypeEnumId=json.CourseTypeEnumId;
    			 toEntity.StatusEnumId=json.StatusEnumId;
                 toEntity.ElectiveGroupId = long.TryParse(json.ElectiveGroupId, out output) ? output : (Int64?)null;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.OrderNo=json.OrderNo;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_CurriculumCourse> entityList, IList<Aca_CurriculumCourseJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_CurriculumCourseJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_CurriculumCourseJson> jsonList, ICollection<Aca_CurriculumCourse> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_CurriculumCourse obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_CurriculumCourse.GetNew();
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
