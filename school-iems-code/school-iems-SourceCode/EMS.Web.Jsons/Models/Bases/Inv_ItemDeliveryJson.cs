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
	public partial class Inv_ItemDeliveryJson
    {
          public Int32 Id { get; set; }
        public Nullable<int> ItemAllocationId { get; set; }
        public Nullable<int> DeliveredQuantity { get; set; }
        public Nullable<System.DateTime> DeliveredDate { get; set; }
        public Nullable<int> DeliveredTo { get; set; }
        public string Note { get; set; }
        public Nullable<int> ItemStockId { get; set; }
        public Nullable<int> ReceivedByBPId { get; set; }
        public string ReceivedByName { get; set; }
        public string ReceivedByMobile { get; set; }
        public Nullable<int> ReceivedByRankId { get; set; }
        public Nullable<int> ReceivedByDepartmentId { get; set; }
        public Nullable<int> ReceivedByAreaId { get; set; }

        public DateTime CreateDate { get; set; }
        public Int32 CreateById { get; set; }
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
	   
		public static void Map(this Inv_ItemDelivery entity, Inv_ItemDeliveryJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.ItemAllocationId = entity.ItemAllocationId; 
			 toJson.DeliveredQuantity = entity.DeliveredQuantity; 
			 toJson.DeliveredDate = entity.DeliveredDate; 
			 toJson.DeliveredTo = entity.DeliveredTo; 
			 toJson.Note = entity.Note;
             toJson.ItemStockId = entity.ItemStockId;
             toJson.ReceivedByBPId = entity.ReceivedByBPId;
             toJson.ReceivedByName = entity.ReceivedByName;
             toJson.ReceivedByMobile = entity.ReceivedByMobile;
             toJson.ReceivedByRankId = entity.ReceivedByRankId;
             toJson.ReceivedByDepartmentId = entity.ReceivedByDepartmentId;
             toJson.ReceivedByAreaId = entity.ReceivedByAreaId;

            toJson.CreateDate = entity.CreateDate ; 
            toJson.CreateById = entity.CreateById ; 
            toJson.LastChanged = entity.LastChanged ; 
            toJson.LastChangedById = entity.LastChangedById ; 
            toJson.IsDeleted = entity.IsDeleted ;
            
            MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_ItemDeliveryJson json, Inv_ItemDelivery toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.ItemAllocationId = json.ItemAllocationId;
                 toEntity.DeliveredQuantity = json.DeliveredQuantity;
                 toEntity.DeliveredDate = json.DeliveredDate;
                 toEntity.DeliveredTo = json.DeliveredTo;
                 toEntity.Note = json.Note?.Trim() ?? json.Note;
                 toEntity.ItemStockId = json.ItemStockId;
                 toEntity.ReceivedByBPId = json.ReceivedByBPId;
                 toEntity.ReceivedByName = json.ReceivedByName;
                 toEntity.ReceivedByMobile = json.ReceivedByMobile;
                 toEntity.ReceivedByRankId = json.ReceivedByRankId;
                 toEntity.ReceivedByDepartmentId = json.ReceivedByDepartmentId;
                 toEntity.ReceivedByAreaId = json.ReceivedByAreaId;

    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;
            

            MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_ItemDelivery> entityList, IList<Inv_ItemDeliveryJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                Inv_ItemDeliveryJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_ItemDeliveryJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_ItemDeliveryJson> jsonList, ICollection<Inv_ItemDelivery> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Inv_ItemDelivery obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_ItemDelivery.GetNew();
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
