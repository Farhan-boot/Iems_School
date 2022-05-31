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
	public partial class Invt_PurchaseDetailJson 
	{
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int ItemId { get; set; }
        public string BatchNo { get; set; }
        public string DocumentPath { get; set; }
        public float Amount { get; set; }
        public int UnitTypeEnumId { get; set; }
        public float Unit { get; set; }
        public int WarhouseId { get; set; }
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
	   
		public static void Map(this Invt_PurchaseDetail entity, Invt_PurchaseDetailJson toJson)
        {
            if (entity == null || toJson == null)
                return;

                toJson.Id = entity.Id;
                toJson.PurchaseId = entity.PurchaseId;
                toJson.ItemId = entity.ItemId;
                toJson.BatchNo = entity.BatchNo;
                toJson.DocumentPath = entity.DocumentPath;
                toJson.Amount = entity.Amount;
                toJson.UnitTypeEnumId = entity.UnitTypeEnumId;
                toJson.Unit = entity.Unit;
                toJson.WarhouseId = entity.WarhouseId;
                toJson.CreateDate = entity.CreateDate;
                toJson.CreateById = entity.CreateById;
                toJson.LastChanged = entity.LastChanged;
                toJson.LastChangedById = entity.LastChangedById;
                toJson.IsDeleted = entity.IsDeleted;

            MapExtra( entity,  toJson);
        }
		
		public static void Map(this Invt_PurchaseDetailJson json, Invt_PurchaseDetail toEntity)
        {
            if (toEntity == null || json == null)
                return;
                //long output;
    			 toEntity.Id=json.Id;
                 toEntity.PurchaseId = json.PurchaseId;
                 toEntity.ItemId = json.ItemId;
    			 toEntity.BatchNo = json.BatchNo;
    			 toEntity.DocumentPath = json.DocumentPath;
    			 toEntity.Amount = json.Amount;
                 toEntity.UnitTypeEnumId = json.UnitTypeEnumId;
                 toEntity.Unit = json.Unit;
                 toEntity.WarhouseId = json.WarhouseId;
                 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Invt_PurchaseDetail> entityList, IList<Invt_PurchaseDetailJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                Invt_PurchaseDetailJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Invt_PurchaseDetailJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Invt_PurchaseDetailJson> jsonList, ICollection<Invt_PurchaseDetail> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Invt_PurchaseDetail obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Invt_PurchaseDetail.GetNew();
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
