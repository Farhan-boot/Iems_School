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
	public partial class Inv_ProductCategoryDetailsJson 
	{
          public Int32 Id { get; set; }
          public Int32 ProductCategoryId { get; set; }
          public Nullable<Int32> Unit { get; set; }
          public Nullable<Int32> UnitTypeEnumId { get; set; }
          public string UnitType { get; set; }
        public Nullable<Int32> WarrningQuantity { get; set; }
          public String Details { get; set; }
          public Nullable<Boolean> HasBarcode { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
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
	   
		public static void Map(this Inv_ProductCategoryDetails entity, Inv_ProductCategoryDetailsJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.ProductCategoryId = entity.ProductCategoryId ; 
			 toJson.Unit = entity.Unit ; 
			 toJson.UnitTypeEnumId = entity.UnitTypeEnumId ;
             if (entity.UnitTypeEnumId != null)
                 toJson.UnitType = entity.UnitType.ToString().AddSpacesToSentence();
             toJson.WarrningQuantity = entity.WarrningQuantity ; 
			 toJson.Details = entity.Details ; 
			 toJson.HasBarcode = entity.HasBarcode ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_ProductCategoryDetailsJson json, Inv_ProductCategoryDetails toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.ProductCategoryId=json.ProductCategoryId;
    			 toEntity.Unit=json.Unit;
    			 toEntity.UnitTypeEnumId=json.UnitTypeEnumId;
                 toEntity.WarrningQuantity=json.WarrningQuantity;
                 toEntity.Details = json.Details?.Trim() ?? json.Details;
    			 toEntity.HasBarcode=json.HasBarcode;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_ProductCategoryDetails> entityList, IList<Inv_ProductCategoryDetailsJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_ProductCategoryDetailsJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_ProductCategoryDetailsJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_ProductCategoryDetailsJson> jsonList, ICollection<Inv_ProductCategoryDetails> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_ProductCategoryDetails obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_ProductCategoryDetails.GetNew();
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
