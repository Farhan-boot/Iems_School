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
	public partial class Aca_OfferedCourseJson 
	{
          public String Id { get; set; }
          public String Code { get; set; }
          public String Name { get; set; }
          public String ShortName { get; set; }
          public String CurriculumCourseId { get; set; }
          public Int32 StudyLevelTermId { get; set; }
          public String SemesterId { get; set; }
          public String SemesterLevelTermId { get; set; }
          public String RegularExamMarkDistributionPolicyId { get; set; }
          public String ReferredExamMarkDistributionPolicyId { get; set; }
          public String BacklogExamMarkDistributionPolicyId { get; set; }
          public String Description { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
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
	   
		public static void Map(this Aca_OfferedCourse entity, Aca_OfferedCourseJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Code = entity.Code ; 
			 toJson.Name = entity.Name ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.CurriculumCourseId = entity.CurriculumCourseId.ToString() ; 
			 toJson.StudyLevelTermId = entity.StudyLevelTermId ; 
			 toJson.SemesterId = entity.SemesterId.ToString() ; 
			 toJson.SemesterLevelTermId = entity.SemesterLevelTermId.ToString() ; 
			 toJson.RegularExamMarkDistributionPolicyId = entity.RegularExamMarkDistributionPolicyId.ToString() ; 
			 toJson.ReferredExamMarkDistributionPolicyId = entity.ReferredExamMarkDistributionPolicyId.ToString() ; 
			 toJson.BacklogExamMarkDistributionPolicyId = entity.BacklogExamMarkDistributionPolicyId.ToString() ; 
			 toJson.Description = entity.Description ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_OfferedCourseJson json, Aca_OfferedCourse toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Code = json.Code?.Trim() ?? json.Code;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
                 toEntity.CurriculumCourseId = long.TryParse(json.CurriculumCourseId, out output) ? output : 0;
    			 toEntity.StudyLevelTermId=json.StudyLevelTermId;
                 toEntity.SemesterId = long.TryParse(json.SemesterId, out output) ? output : 0;
                 toEntity.SemesterLevelTermId = long.TryParse(json.SemesterLevelTermId, out output) ? output : 0;
                 toEntity.RegularExamMarkDistributionPolicyId = long.TryParse(json.RegularExamMarkDistributionPolicyId, out output) ? output : 0;
                 toEntity.ReferredExamMarkDistributionPolicyId = long.TryParse(json.ReferredExamMarkDistributionPolicyId, out output) ? output : 0;
                 toEntity.BacklogExamMarkDistributionPolicyId = long.TryParse(json.BacklogExamMarkDistributionPolicyId, out output) ? output : 0;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_OfferedCourse> entityList, IList<Aca_OfferedCourseJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_OfferedCourseJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_OfferedCourseJson> jsonList, ICollection<Aca_OfferedCourse> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_OfferedCourse obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_OfferedCourse.GetNew();
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
