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
	public partial class HR_SalaryTemplateDetailsJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public Int32 SalaryTemplateId { get; set; }
          public Byte SalaryTypeEnumId { get; set; }
          public string SalaryType { get; set; }
          public Byte CategoryTypeEnumId { get; set; }
          public string CategoryType { get; set; }
          public Single OrderNo { get; set; }
          public Nullable<Int32> ParentId { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
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
	   
		public static void Map(this HR_SalaryTemplateDetails entity, HR_SalaryTemplateDetailsJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.SalaryTemplateId = entity.SalaryTemplateId ; 
			 toJson.SalaryTypeEnumId = entity.SalaryTypeEnumId ; 
             if(entity.SalaryTypeEnumId!=null)
             toJson.SalaryType = entity.SalaryType.ToString().AddSpacesToSentence();
			 toJson.CategoryTypeEnumId = entity.CategoryTypeEnumId ; 
             if(entity.CategoryTypeEnumId!=null)
             toJson.CategoryType = entity.CategoryType.ToString().AddSpacesToSentence();
			 toJson.OrderNo = entity.OrderNo ; 
			 toJson.ParentId = entity.ParentId ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_SalaryTemplateDetailsJson json, HR_SalaryTemplateDetails toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.SalaryTemplateId=json.SalaryTemplateId;
    			 toEntity.SalaryTypeEnumId=json.SalaryTypeEnumId;
    			 toEntity.CategoryTypeEnumId=json.CategoryTypeEnumId;
    			 toEntity.OrderNo=json.OrderNo;
    			 toEntity.ParentId=json.ParentId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_SalaryTemplateDetails> entityList, IList<HR_SalaryTemplateDetailsJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_SalaryTemplateDetailsJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_SalaryTemplateDetailsJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_SalaryTemplateDetailsJson> jsonList, ICollection<HR_SalaryTemplateDetails> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_SalaryTemplateDetails obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_SalaryTemplateDetails.GetNew();
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
