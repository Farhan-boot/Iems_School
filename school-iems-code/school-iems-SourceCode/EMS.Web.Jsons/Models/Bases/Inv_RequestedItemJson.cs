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
	public partial class Inv_RequestedItemJson
    {
        public int Id { get; set; }
        public Nullable<int> RequisitionId { get; set; }
        public Nullable<int> CatagoryId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> ItemStatus { get; set; }
        public string Remark { get; set; }
        public string ItemCategory { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
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
	   
		public static void Map(this Inv_RequestedItem entity, Inv_RequestedItemJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.RequisitionId = entity.RequisitionId; 
			 toJson.CatagoryId = entity.CatagoryId; 
			 toJson.Quantity = entity.Quantity; 
			 toJson.ItemStatus = entity.ItemStatus; 
			 toJson.Remark = entity.Remark; 
			 toJson.ItemCategory = entity.ItemCategory;
             toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_RequestedItemJson json, Inv_RequestedItem toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.RequisitionId = json.RequisitionId;
                 toEntity.CatagoryId = json.CatagoryId;
                 toEntity.Quantity = json.Quantity;
                 toEntity.ItemStatus = json.ItemStatus;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
                 toEntity.ItemCategory = json.ItemCategory?.Trim() ?? json.ItemCategory;

    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_RequestedItem> entityList, IList<Inv_RequestedItemJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                Inv_RequestedItemJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_RequestedItemJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_RequestedItemJson> jsonList, ICollection<Inv_RequestedItem> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Inv_RequestedItem obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_RequestedItem.GetNew();
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
