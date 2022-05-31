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
	public partial class Inv_ItemJson 
	{
          public Int32 Id { get; set; }
          public Nullable<Int32> CategoryId { get; set; }
          public Nullable<Int32> StoreId { get; set; }
          public Nullable<Int32> ItemStatus { get; set; }
          public Nullable<Int32> AssetTypeEnumId { get; set; }
          public string AssetType { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public string ItemName { get; set; }
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
	   
		public static void Map(this Inv_Item entity, Inv_ItemJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.CategoryId = entity.CategoryId ; 
			 toJson.StoreId = entity.StoreId ; 
			 toJson.ItemStatus = entity.ItemStatus ; 
			 toJson.AssetTypeEnumId = entity.AssetTypeEnumId ; 
             if(entity.AssetTypeEnumId!=null)
             toJson.AssetType = entity.AssetType.ToString().AddSpacesToSentence();
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ;
             toJson.ItemName = entity.ItemName;


			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_ItemJson json, Inv_Item toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.CategoryId=json.CategoryId;
    			 toEntity.StoreId=json.StoreId;
    			 toEntity.ItemStatus=json.ItemStatus;
    			 toEntity.AssetTypeEnumId=json.AssetTypeEnumId;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;
                 toEntity.ItemName = json.ItemName;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_Item> entityList, IList<Inv_ItemJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_ItemJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_ItemJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_ItemJson> jsonList, ICollection<Inv_Item> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_Item obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_Item.GetNew();
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
