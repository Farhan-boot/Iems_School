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
	public partial class Inv_RequisitionJson 
	{
          public Int32 Id { get; set; }
          public Nullable<Int32> RequestedByEmployeeId { get; set; }
          public Nullable<DateTime> RequisitionDate { get; set; }
          public Nullable<DateTime> RequireDate { get; set; }
          public String Purpose { get; set; }
          public String Remark { get; set; }
          public Nullable<Int32> ReferencedByEmployeeId { get; set; }
          public Nullable<Int32> IssuedOrReleaseByUserId { get; set; }
          public Nullable<Int32> ApprovedByAdminId { get; set; }
          public Nullable<Int32> ReceivedByEmployeeId { get; set; }
          public Nullable<Int32> Status { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }

        //New Fild 
        public Nullable<int> RequisitionStatusId { get; set; }
        public Nullable<int> ApprovedById { get; set; }
        public Nullable<int> DeliveredById { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<int> Quantity { get; set; }



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
	   
		public static void Map(this Inv_Requisition entity, Inv_RequisitionJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.RequestedByEmployeeId = entity.RequestedByEmployeeId ; 
			 toJson.RequisitionDate = entity.RequisitionDate ; 
			 toJson.RequireDate = entity.RequireDate ; 
			 toJson.Purpose = entity.Purpose ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.ReferencedByEmployeeId = entity.ReferencedByEmployeeId ; 
			 toJson.IssuedOrReleaseByUserId = entity.IssuedOrReleaseByUserId ; 
			 toJson.ApprovedByAdminId = entity.ApprovedByAdminId ; 
			 toJson.ReceivedByEmployeeId = entity.ReceivedByEmployeeId ; 
			 toJson.Status = entity.Status ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ;
            // New Proparty
            toJson.RequisitionStatusId = entity.RequisitionStatusId;
            toJson.ApprovedById = entity.ApprovedById;
            toJson.DeliveredById = entity.DeliveredById;
            toJson.ItemCategoryId = entity.ItemCategoryId;
            toJson.Quantity = entity.Quantity;

            MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_RequisitionJson json, Inv_Requisition toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.RequestedByEmployeeId=json.RequestedByEmployeeId;
    			 toEntity.RequisitionDate=json.RequisitionDate;
    			 toEntity.RequireDate=json.RequireDate;
                 toEntity.Purpose = json.Purpose?.Trim() ?? json.Purpose;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
    			 toEntity.ReferencedByEmployeeId=json.ReferencedByEmployeeId;
    			 toEntity.IssuedOrReleaseByUserId=json.IssuedOrReleaseByUserId;
    			 toEntity.ApprovedByAdminId=json.ApprovedByAdminId;
    			 toEntity.ReceivedByEmployeeId=json.ReceivedByEmployeeId;
    			 toEntity.Status=json.Status;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;
            //New Proparty
            toEntity.RequisitionStatusId = json.RequisitionStatusId;
            toEntity.ApprovedById = json.ApprovedById;
            toEntity.DeliveredById = json.DeliveredById;
            toEntity.ItemCategoryId = json.ItemCategoryId;
            toEntity.Quantity = json.Quantity;

            MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_Requisition> entityList, IList<Inv_RequisitionJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_RequisitionJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_RequisitionJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_RequisitionJson> jsonList, ICollection<Inv_Requisition> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_Requisition obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_Requisition.GetNew();
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
