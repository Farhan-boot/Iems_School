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
	public partial class Inv_PurchaseRequisitionDetailJson 
	{
          public Int32 Id { get; set; }
          public String ItemName { get; set; }
          public Nullable<Int32> ProductCategoryId { get; set; }
          public String Detail { get; set; }
          public Nullable<Int32> Quantity { get; set; }
          public Nullable<Decimal> UnitPrice { get; set; }
          public Int32 PurchaseRequisitionId { get; set; }
          public Int32 UnitTypeEnumId { get; set; }
          public string UnitType { get; set; }
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
	   
		public static void Map(this Inv_PurchaseRequisitionDetail entity, Inv_PurchaseRequisitionDetailJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.ItemName = entity.ItemName ; 
			 toJson.ProductCategoryId = entity.ProductCategoryId ; 
			 toJson.Detail = entity.Detail ; 
			 toJson.Quantity = entity.Quantity ; 
			 toJson.UnitPrice = entity.UnitPrice ; 
			 toJson.PurchaseRequisitionId = entity.PurchaseRequisitionId ; 
			 toJson.UnitTypeEnumId = entity.UnitTypeEnumId ; 
             if(entity.UnitTypeEnumId!=null)
             toJson.UnitType = entity.UnitType.ToString().AddSpacesToSentence();
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_PurchaseRequisitionDetailJson json, Inv_PurchaseRequisitionDetail toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.ItemName = json.ItemName?.Trim() ?? json.ItemName;
    			 toEntity.ProductCategoryId=json.ProductCategoryId;
                 toEntity.Detail = json.Detail?.Trim() ?? json.Detail;
    			 toEntity.Quantity=json.Quantity;
    			 toEntity.UnitPrice=json.UnitPrice;
    			 toEntity.PurchaseRequisitionId=json.PurchaseRequisitionId;
    			 toEntity.UnitTypeEnumId=json.UnitTypeEnumId;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_PurchaseRequisitionDetail> entityList, IList<Inv_PurchaseRequisitionDetailJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_PurchaseRequisitionDetailJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_PurchaseRequisitionDetailJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_PurchaseRequisitionDetailJson> jsonList, ICollection<Inv_PurchaseRequisitionDetail> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_PurchaseRequisitionDetail obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_PurchaseRequisitionDetail.GetNew();
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
