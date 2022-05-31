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
	public partial class Inv_InventoryJson 
	{
          public Int32 Id { get; set; }
          public Int32 StoreId { get; set; }
          public String VoucherNo { get; set; }
          public Nullable<DateTime> VoucherDate { get; set; }
          public String InvoiceNo { get; set; }
          public Nullable<DateTime> InvoiceDate { get; set; }
          public string PurchaseOrderNo { get; set; }
          public Nullable<DateTime> PurchaseOrderDate { get; set; }
          public String ChalanNo { get; set; }
          public Nullable<DateTime> ChalanDate { get; set; }
          public Nullable<DateTime> ReceivedDate { get; set; }
          public Nullable<Int32> ReceivedBy { get; set; }
          public Nullable<Int32> SupplierId { get; set; }
          public String Remark { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public String History { get; set; }
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
	   
		public static void Map(this Inv_Inventory entity, Inv_InventoryJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.StoreId = entity.StoreId ; 
			 toJson.VoucherNo = entity.VoucherNo ; 
			 toJson.VoucherDate = entity.VoucherDate ; 
			 toJson.InvoiceNo = entity.InvoiceNo ; 
			 toJson.InvoiceDate = entity.InvoiceDate ; 
			 toJson.PurchaseOrderNo = entity.PurchaseOrderNo ; 
			 toJson.PurchaseOrderDate = entity.PurchaseOrderDate ; 
			 toJson.ChalanNo = entity.ChalanNo ; 
			 toJson.ChalanDate = entity.ChalanDate ; 
			 toJson.ReceivedDate = entity.ReceivedDate ; 
			 toJson.ReceivedBy = entity.ReceivedBy ; 
			 toJson.SupplierId = entity.SupplierId ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.History = entity.History ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_InventoryJson json, Inv_Inventory toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.StoreId=json.StoreId;
                 toEntity.VoucherNo = json.VoucherNo?.Trim() ?? json.VoucherNo;
    			 toEntity.VoucherDate=json.VoucherDate;
                 toEntity.InvoiceNo = json.InvoiceNo?.Trim() ?? json.InvoiceNo;
    			 toEntity.InvoiceDate=json.InvoiceDate;
    			 toEntity.PurchaseOrderNo=json.PurchaseOrderNo;
    			 toEntity.PurchaseOrderDate=json.PurchaseOrderDate;
                 toEntity.ChalanNo = json.ChalanNo?.Trim() ?? json.ChalanNo;
    			 toEntity.ChalanDate=json.ChalanDate;
    			 toEntity.ReceivedDate=json.ReceivedDate;
    			 toEntity.ReceivedBy=json.ReceivedBy;
    			 toEntity.SupplierId=json.SupplierId;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;
                 toEntity.History = json.History?.Trim() ?? json.History;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_Inventory> entityList, IList<Inv_InventoryJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_InventoryJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_InventoryJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_InventoryJson> jsonList, ICollection<Inv_Inventory> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_Inventory obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_Inventory.GetNew();
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
