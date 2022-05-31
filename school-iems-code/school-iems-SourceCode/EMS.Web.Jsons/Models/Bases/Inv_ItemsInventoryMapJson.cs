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
	public partial class Inv_ItemsInventoryMapJson 
	{
          public Int32 Id { get; set; }
          public String ItemId { get; set; }
          public Nullable<Int32> InventoryId { get; set; }
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
	   
		public static void Map(this Inv_ItemsInventoryMap entity, Inv_ItemsInventoryMapJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.ItemId = entity.ItemId ; 
			 toJson.InventoryId = entity.InventoryId ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_ItemsInventoryMapJson json, Inv_ItemsInventoryMap toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.ItemId = json.ItemId?.Trim() ?? json.ItemId;
    			 toEntity.InventoryId=json.InventoryId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_ItemsInventoryMap> entityList, IList<Inv_ItemsInventoryMapJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_ItemsInventoryMapJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_ItemsInventoryMapJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_ItemsInventoryMapJson> jsonList, ICollection<Inv_ItemsInventoryMap> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_ItemsInventoryMap obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_ItemsInventoryMap.GetNew();
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
