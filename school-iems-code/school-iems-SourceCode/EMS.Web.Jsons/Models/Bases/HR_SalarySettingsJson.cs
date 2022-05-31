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
	public partial class HR_SalarySettingsJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public Byte SalaryTypeEnumId { get; set; }
          public string SalaryType { get; set; }
          public Int32 EmployeeId { get; set; }
          public DateTime EffectiveFrom { get; set; }
          public Boolean IsCurrent { get; set; }
          public Boolean IsExcludedFromAutoGeneration { get; set; }
          public String History { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public Nullable<DateTime> NextPromotionDate { get; set; }
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
	   
		public static void Map(this HR_SalarySettings entity, HR_SalarySettingsJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.SalaryTypeEnumId = entity.SalaryTypeEnumId ; 
             if(entity.SalaryTypeEnumId!=null)
             toJson.SalaryType = entity.SalaryType.ToString().AddSpacesToSentence();
			 toJson.EmployeeId = entity.EmployeeId ; 
			 toJson.EffectiveFrom = entity.EffectiveFrom ; 
			 toJson.IsCurrent = entity.IsCurrent ; 
			 toJson.IsExcludedFromAutoGeneration = entity.IsExcludedFromAutoGeneration ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.NextPromotionDate = entity.NextPromotionDate ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_SalarySettingsJson json, HR_SalarySettings toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.SalaryTypeEnumId=json.SalaryTypeEnumId;
    			 toEntity.EmployeeId=json.EmployeeId;
    			 toEntity.EffectiveFrom=json.EffectiveFrom;
    			 toEntity.IsCurrent=json.IsCurrent;
    			 toEntity.IsExcludedFromAutoGeneration=json.IsExcludedFromAutoGeneration;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.NextPromotionDate=json.NextPromotionDate;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_SalarySettings> entityList, IList<HR_SalarySettingsJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_SalarySettingsJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_SalarySettingsJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_SalarySettingsJson> jsonList, ICollection<HR_SalarySettings> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_SalarySettings obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_SalarySettings.GetNew();
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
