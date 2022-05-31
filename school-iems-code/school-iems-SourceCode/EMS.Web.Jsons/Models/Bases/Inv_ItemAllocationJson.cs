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
	public partial class Inv_ItemAllocationJson 
	{
          public Int32 Id { get; set; }
          public int? ItemId { get; set; }
          public Nullable<Int32> FromStore { get; set; }
          public Nullable<Int32> AllocatedTo { get; set; }
          public Nullable<DateTime> AllocationDate { get; set; }
          public Nullable<Int32> AllocationStatus { get; set; }
          public Nullable<Int32> ReferenceBy { get; set; }
          public String Remarks { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }

        //New Proparty
        public Nullable<int> RequestedItemId { get; set; }
        public Nullable<int> ApprovedQuantity { get; set; }
        public Nullable<int> ApprovedById { get; set; }

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
	   
		public static void Map(this Inv_ItemAllocation entity, Inv_ItemAllocationJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.ItemId = entity.ItemId ; 
			 toJson.FromStore = entity.FromStore ; 
			 toJson.AllocatedTo = entity.AllocatedTo ; 
			 toJson.AllocationDate = entity.AllocationDate ; 
			 toJson.AllocationStatus = entity.AllocationStatus ; 
			 toJson.ReferenceBy = entity.ReferenceBy ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ;
            //New Object
            toJson.RequestedItemId = entity.RequestedItemId;
            toJson.ApprovedQuantity = entity.ApprovedQuantity;
            toJson.ApprovedById = entity.ApprovedById;

            MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_ItemAllocationJson json, Inv_ItemAllocation toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.ItemId = json.ItemId ?? json.ItemId;
    			 toEntity.FromStore=json.FromStore;
    			 toEntity.AllocatedTo=json.AllocatedTo;
    			 toEntity.AllocationDate=json.AllocationDate;
    			 toEntity.AllocationStatus=json.AllocationStatus;
    			 toEntity.ReferenceBy=json.ReferenceBy;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;
            //New Object
            toEntity.RequestedItemId = json.RequestedItemId;
            toEntity.ApprovedQuantity = json.ApprovedQuantity;
            toEntity.ApprovedById = json.ApprovedById;

            MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_ItemAllocation> entityList, IList<Inv_ItemAllocationJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_ItemAllocationJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_ItemAllocationJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_ItemAllocationJson> jsonList, ICollection<Inv_ItemAllocation> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_ItemAllocation obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_ItemAllocation.GetNew();
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
