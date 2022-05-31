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
	public partial class Inv_PurchaseRequisitionJson 
	{
          public Int32 Id { get; set; }
          public Nullable<DateTime> RequisitionDate { get; set; }
          public Nullable<Int32> RequisitionByUserId { get; set; }
          public Nullable<Int32> Status { get; set; }
          public String Purpose { get; set; }
          public String Remarks { get; set; }
          public Nullable<Int32> ApprovedByUserId { get; set; }
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
	   
		public static void Map(this Inv_PurchaseRequisition entity, Inv_PurchaseRequisitionJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.RequisitionDate = entity.RequisitionDate ; 
			 toJson.RequisitionByUserId = entity.RequisitionByUserId ; 
			 toJson.Status = entity.Status ; 
			 toJson.Purpose = entity.Purpose ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.ApprovedByUserId = entity.ApprovedByUserId ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_PurchaseRequisitionJson json, Inv_PurchaseRequisition toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.RequisitionDate=json.RequisitionDate;
    			 toEntity.RequisitionByUserId=json.RequisitionByUserId;
    			 toEntity.Status=json.Status;
                 toEntity.Purpose = json.Purpose?.Trim() ?? json.Purpose;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
    			 toEntity.ApprovedByUserId=json.ApprovedByUserId;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_PurchaseRequisition> entityList, IList<Inv_PurchaseRequisitionJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_PurchaseRequisitionJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_PurchaseRequisitionJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_PurchaseRequisitionJson> jsonList, ICollection<Inv_PurchaseRequisition> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_PurchaseRequisition obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_PurchaseRequisition.GetNew();
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
