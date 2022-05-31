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
	public partial class Inv_RequisitionDetailsJson 
	{
          public Int32 Id { get; set; }
          public Nullable<Int32> RequisitionId { get; set; }
          public String ItemName { get; set; }
          public Nullable<Int32> ItemId { get; set; }
          public String Detail { get; set; }
          public Nullable<Int32> Quantity { get; set; }
          public Nullable<Int32> ApprovedQuantity { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }

        //New Fild
        public string RequisitionNo { get; set; }
        public Nullable<int> RequisitionByBPId { get; set; }
        public string RequisitionByName { get; set; }
        public string RequisitionByRank { get; set; }
        public string RequestedByDepartment { get; set; }
        public string RequestedByDepartmentArea { get; set; }
        public Nullable<System.DateTime> RequisitionDate { get; set; }
        public Nullable<System.DateTime> RequierDate { get; set; }
        public Nullable<int> ApprovedByBPId { get; set; }
        public string ApprovedByRank { get; set; }
        public string ApprovedByDepartment { get; set; }
        public Nullable<int> StatusEnumId { get; set; }
        public string Purpose { get; set; }

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
	   
		public static void Map(this Inv_RequisitionDetails entity, Inv_RequisitionDetailsJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.RequisitionId = entity.RequisitionId ; 
			 toJson.ItemName = entity.ItemName ; 
			 toJson.ItemId = entity.ItemId ; 
			 toJson.Detail = entity.Detail ; 
			 toJson.Quantity = entity.Quantity ; 
			 toJson.ApprovedQuantity = entity.ApprovedQuantity ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ;
            //New Proparty
            toJson.RequisitionNo = entity.RequisitionNo;
            toJson.RequisitionByBPId = entity.RequisitionByBPId;
            toJson.RequisitionByName = entity.RequisitionByName;
            toJson.RequisitionByRank = entity.RequisitionByRank;
            toJson.RequestedByDepartment = entity.RequestedByDepartment;
            toJson.RequestedByDepartmentArea = entity.RequestedByDepartmentArea;
            toJson.RequisitionDate = entity.RequisitionDate;
            toJson.RequierDate = entity.RequierDate;
            toJson.ApprovedByBPId = entity.ApprovedByBPId;
            toJson.ApprovedByRank = entity.ApprovedByRank;
            toJson.ApprovedByDepartment = entity.ApprovedByDepartment;
            toJson.StatusEnumId = entity.StatusEnumId;
            toJson.Purpose = entity.Purpose;

            MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_RequisitionDetailsJson json, Inv_RequisitionDetails toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.RequisitionId=json.RequisitionId;
                 toEntity.ItemName = json.ItemName?.Trim() ?? json.ItemName;
    			 toEntity.ItemId=json.ItemId;
                 toEntity.Detail = json.Detail?.Trim() ?? json.Detail;
    			 toEntity.Quantity=json.Quantity;
    			 toEntity.ApprovedQuantity=json.ApprovedQuantity;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;
            //New Proparty
            toEntity.RequisitionNo = json.RequisitionNo;
            toEntity.RequisitionByBPId = json.RequisitionByBPId;
            toEntity.RequisitionByName = json.RequisitionByName;
            toEntity.RequisitionByRank = json.RequisitionByRank;
            toEntity.RequestedByDepartment = json.RequestedByDepartment;
            toEntity.RequestedByDepartmentArea = json.RequestedByDepartmentArea;
            toEntity.RequisitionDate = json.RequisitionDate;
            toEntity.RequierDate = json.RequierDate;
            toEntity.ApprovedByBPId = json.ApprovedByBPId;
            toEntity.ApprovedByRank = json.ApprovedByRank;
            toEntity.ApprovedByDepartment = json.ApprovedByDepartment;
            toEntity.StatusEnumId = json.StatusEnumId;
            toEntity.Purpose = json.Purpose;

            MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_RequisitionDetails> entityList, IList<Inv_RequisitionDetailsJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_RequisitionDetailsJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_RequisitionDetailsJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_RequisitionDetailsJson> jsonList, ICollection<Inv_RequisitionDetails> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_RequisitionDetails obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_RequisitionDetails.GetNew();
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
