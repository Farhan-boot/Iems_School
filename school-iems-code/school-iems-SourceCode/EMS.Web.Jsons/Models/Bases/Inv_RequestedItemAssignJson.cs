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
	public partial class Inv_RequestedItemAssignJson 
	{
          public Int32 Id { get; set; }
          public Int32 ItemId { get; set; }
          public Int32 UnitTypeEnumId { get; set; }
          public string UnitType { get; set; }
          public Double Unit { get; set; }
          public String AssginPersonName { get; set; }
          public Nullable<DateTime> ReturnDate { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public Boolean IsReturn { get; set; }
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
	   
		public static void Map(this Inv_RequestedItemAssign entity, Inv_RequestedItemAssignJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.ItemId = entity.ItemId ; 
			 toJson.UnitTypeEnumId = entity.UnitTypeEnumId ; 
             if(entity.UnitTypeEnumId!=null)
             toJson.UnitType = entity.UnitType.ToString().AddSpacesToSentence();
			 toJson.Unit = entity.Unit ; 
			 toJson.AssginPersonName = entity.AssginPersonName ; 
			 toJson.ReturnDate = entity.ReturnDate ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.IsReturn = entity.IsReturn ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_RequestedItemAssignJson json, Inv_RequestedItemAssign toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.ItemId=json.ItemId;
    			 toEntity.UnitTypeEnumId=json.UnitTypeEnumId;
    			 toEntity.Unit=json.Unit;
                 toEntity.AssginPersonName = json.AssginPersonName?.Trim() ?? json.AssginPersonName;
    			 toEntity.ReturnDate=json.ReturnDate;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.IsReturn=json.IsReturn;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_RequestedItemAssign> entityList, IList<Inv_RequestedItemAssignJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_RequestedItemAssignJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_RequestedItemAssignJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_RequestedItemAssignJson> jsonList, ICollection<Inv_RequestedItemAssign> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_RequestedItemAssign obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_RequestedItemAssign.GetNew();
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
