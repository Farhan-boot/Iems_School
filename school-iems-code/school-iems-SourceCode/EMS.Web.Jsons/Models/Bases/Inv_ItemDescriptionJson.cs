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
	public partial class Inv_ItemDescriptionJson 
	{
          public Int32 Id { get; set; }
          public int? ItemId { get; set; }
          public String CompanyItemBarcode { get; set; }
          public Nullable<DateTime> WarrentyDate { get; set; }
          public String Description { get; set; }
          public Nullable<DateTime> LastChanged { get; set; }
          public Nullable<Int32> LastChangedById { get; set; }
          public Nullable<Boolean> IsDeleted { get; set; }
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
	   
		public static void Map(this Inv_ItemDescription entity, Inv_ItemDescriptionJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.ItemId = entity.ItemId ; 
			 toJson.CompanyItemBarcode = entity.CompanyItemBarcode ; 
			 toJson.WarrentyDate = entity.WarrentyDate ; 
			 toJson.Description = entity.Description ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_ItemDescriptionJson json, Inv_ItemDescription toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.ItemId = json.ItemId ?? json.ItemId;
                 toEntity.CompanyItemBarcode = json.CompanyItemBarcode?.Trim() ?? json.CompanyItemBarcode;
    			 toEntity.WarrentyDate=json.WarrentyDate;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_ItemDescription> entityList, IList<Inv_ItemDescriptionJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_ItemDescriptionJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_ItemDescriptionJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_ItemDescriptionJson> jsonList, ICollection<Inv_ItemDescription> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_ItemDescription obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_ItemDescription.GetNew();
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
