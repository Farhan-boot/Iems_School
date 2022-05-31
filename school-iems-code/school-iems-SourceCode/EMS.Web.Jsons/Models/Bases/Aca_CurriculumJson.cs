﻿//File:Json Model Base Partial
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
	public partial class Aca_CurriculumJson 
	{
          public String Id { get; set; }
          public String Name { get; set; }
          public String ShortName { get; set; }
          public Int32 Year { get; set; }
          public String Session { get; set; }
          public Int32 CurriculumNo { get; set; }
          public Int32 ProgramId { get; set; }
          public String Description { get; set; }
          public Boolean IsValid { get; set; }
          public Boolean IsOffering { get; set; }
          public Single TotalCourse { get; set; }
          public Int32 GradingPolicyId { get; set; }
          public Single CourseRequireForGraduation { get; set; }
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
	   
		public static void Map(this Aca_Curriculum entity, Aca_CurriculumJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Name = entity.Name ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.Year = entity.Year ; 
			 toJson.Session = entity.Session ; 
			 toJson.CurriculumNo = entity.CurriculumNo ; 
			 toJson.ProgramId = entity.ProgramId ; 
			 toJson.Description = entity.Description ; 
			 toJson.IsValid = entity.IsValid ; 
			 toJson.IsOffering = entity.IsOffering ; 
			 toJson.TotalCourse = entity.TotalCourse ; 
			 toJson.GradingPolicyId = entity.GradingPolicyId ; 
			 toJson.CourseRequireForGraduation = entity.CourseRequireForGraduation ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_CurriculumJson json, Aca_Curriculum toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
    			 toEntity.Year=json.Year;
                 toEntity.Session = json.Session?.Trim() ?? json.Session;
    			 toEntity.CurriculumNo=json.CurriculumNo;
    			 toEntity.ProgramId=json.ProgramId;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
    			 toEntity.IsValid=json.IsValid;
    			 toEntity.IsOffering=json.IsOffering;
    			 toEntity.TotalCourse=json.TotalCourse;
    			 toEntity.GradingPolicyId=json.GradingPolicyId;
    			 toEntity.CourseRequireForGraduation=json.CourseRequireForGraduation;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_Curriculum> entityList, IList<Aca_CurriculumJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_CurriculumJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_CurriculumJson> jsonList, ICollection<Aca_Curriculum> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_Curriculum obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_Curriculum.GetNew();
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
