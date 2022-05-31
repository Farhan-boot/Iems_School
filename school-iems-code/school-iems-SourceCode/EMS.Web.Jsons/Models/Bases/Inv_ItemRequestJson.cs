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
	public partial class Inv_ItemRequestJson 
	{
          public Int32 Id { get; set; }
          public Int32 RequestTypeEnumId { get; set; }
          public string RequestType { get; set; }
          public String RequestPersonName { get; set; }
          public Nullable<DateTime> ReturnDate { get; set; }
          public Int32 ItemId { get; set; }
          public Int32 UnitTypeEnumId { get; set; }
          public string UnitType { get; set; }
          public Int32 Unit { get; set; }
          public Int32 StatusEnumId { get; set; }
          public string Status { get; set; }
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
	   
		public static void Map(this Inv_ItemRequest entity, Inv_ItemRequestJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.RequestTypeEnumId = entity.RequestTypeEnumId ; 
             if(entity.RequestTypeEnumId!=null)
             toJson.RequestType = entity.RequestType.ToString().AddSpacesToSentence();
			 toJson.RequestPersonName = entity.RequestPersonName ; 
			 toJson.ReturnDate = entity.ReturnDate ; 
			 toJson.ItemId = entity.ItemId ; 
			 toJson.UnitTypeEnumId = entity.UnitTypeEnumId ; 
             if(entity.UnitTypeEnumId!=null)
             toJson.UnitType = entity.UnitType.ToString().AddSpacesToSentence();
			 toJson.Unit = entity.Unit ; 
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.IsReturn = entity.IsReturn ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_ItemRequestJson json, Inv_ItemRequest toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.RequestTypeEnumId=json.RequestTypeEnumId;
                 toEntity.RequestPersonName = json.RequestPersonName?.Trim() ?? json.RequestPersonName;
    			 toEntity.ReturnDate=json.ReturnDate;
    			 toEntity.ItemId=json.ItemId;
    			 toEntity.UnitTypeEnumId=json.UnitTypeEnumId;
    			 toEntity.Unit=json.Unit;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.IsReturn=json.IsReturn;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_ItemRequest> entityList, IList<Inv_ItemRequestJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_ItemRequestJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_ItemRequestJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_ItemRequestJson> jsonList, ICollection<Inv_ItemRequest> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_ItemRequest obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_ItemRequest.GetNew();
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
