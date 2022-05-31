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
	public partial class HR_SalarySettingsDetailsJson 
	{
          public Int32 Id { get; set; }
          public Int32 SalarySettingsId { get; set; }
          public String Name { get; set; }
          public Byte SalaryTypeEnumId { get; set; }
          public string SalaryType { get; set; }
          public Byte CategoryTypeEnumId { get; set; }
          public string CategoryType { get; set; }
          public Single OrderNo { get; set; }
          public Single Value { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Nullable<Int32> ParentId { get; set; }
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
	   
		public static void Map(this HR_SalarySettingsDetails entity, HR_SalarySettingsDetailsJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.SalarySettingsId = entity.SalarySettingsId ; 
			 toJson.Name = entity.Name ; 
			 toJson.SalaryTypeEnumId = entity.SalaryTypeEnumId ; 
             if(entity.SalaryTypeEnumId!=null)
             toJson.SalaryType = entity.SalaryType.ToString().AddSpacesToSentence();
			 toJson.CategoryTypeEnumId = entity.CategoryTypeEnumId ; 
             if(entity.CategoryTypeEnumId!=null)
             toJson.CategoryType = entity.CategoryType.ToString().AddSpacesToSentence();
			 toJson.OrderNo = entity.OrderNo ; 
			 toJson.Value = entity.Value ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.ParentId = entity.ParentId ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_SalarySettingsDetailsJson json, HR_SalarySettingsDetails toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.SalarySettingsId=json.SalarySettingsId;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.SalaryTypeEnumId=json.SalaryTypeEnumId;
    			 toEntity.CategoryTypeEnumId=json.CategoryTypeEnumId;
    			 toEntity.OrderNo=json.OrderNo;
    			 toEntity.Value=json.Value;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.ParentId=json.ParentId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_SalarySettingsDetails> entityList, IList<HR_SalarySettingsDetailsJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_SalarySettingsDetailsJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_SalarySettingsDetailsJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_SalarySettingsDetailsJson> jsonList, ICollection<HR_SalarySettingsDetails> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_SalarySettingsDetails obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_SalarySettingsDetails.GetNew();
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
