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
	public partial class Inv_InventoryDetailsJson 
	{
          public Int32 Id { get; set; }
          public Nullable<Int32> InventoryId { get; set; }
          public Nullable<Int32> ProductCategoryId { get; set; }
          public Nullable<Single> Quantity { get; set; }
          public Nullable<Single> UnitPrice { get; set; }
          public Nullable<DateTime> WarrentyStartDate { get; set; }
          public Nullable<DateTime> WarrentyExpairDate { get; set; }
          public String Description { get; set; }
          public String OwnBarcode { get; set; }
          public String OurBarcode { get; set; }
          public Nullable<Int32> StatusEnumId { get; set; }
          public string Status { get; set; }
          public String Remark { get; set; }
          public Nullable<Boolean> IsBarcoded { get; set; }
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
	   
		public static void Map(this Inv_InventoryDetails entity, Inv_InventoryDetailsJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.InventoryId = entity.InventoryId ; 
			 toJson.ProductCategoryId = entity.ProductCategoryId ; 
			 toJson.Quantity = entity.Quantity ; 
			 toJson.UnitPrice = entity.UnitPrice ; 
			 toJson.WarrentyStartDate = entity.WarrentyStartDate ; 
			 toJson.WarrentyExpairDate = entity.WarrentyExpairDate ; 
			 toJson.Description = entity.Description ; 
			 toJson.OwnBarcode = entity.OwnBarcode ; 
			 toJson.OurBarcode = entity.OurBarcode ; 
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.Remark = entity.Remark ; 
			 toJson.IsBarcoded = entity.IsBarcoded ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_InventoryDetailsJson json, Inv_InventoryDetails toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.InventoryId=json.InventoryId;
    			 toEntity.ProductCategoryId=json.ProductCategoryId;
    			 toEntity.Quantity=json.Quantity;
    			 toEntity.UnitPrice=json.UnitPrice;
    			 toEntity.WarrentyStartDate=json.WarrentyStartDate;
    			 toEntity.WarrentyExpairDate=json.WarrentyExpairDate;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
                 toEntity.OwnBarcode = json.OwnBarcode?.Trim() ?? json.OwnBarcode;
                 toEntity.OurBarcode = json.OurBarcode?.Trim() ?? json.OurBarcode;
    			 toEntity.StatusEnumId=json.StatusEnumId;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
    			 toEntity.IsBarcoded=json.IsBarcoded;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_InventoryDetails> entityList, IList<Inv_InventoryDetailsJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_InventoryDetailsJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_InventoryDetailsJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_InventoryDetailsJson> jsonList, ICollection<Inv_InventoryDetails> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_InventoryDetails obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_InventoryDetails.GetNew();
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
