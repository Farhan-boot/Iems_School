﻿//File:Json Model Base Partial
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
	public partial class Inv_ItemReturnJson 
	{
          public Int32 Id { get; set; }
          public int? ItemId { get; set; }
          public Nullable<DateTime> ReturnDate { get; set; }
          public Nullable<Int32> ReceivedBy { get; set; }
          public Nullable<Int32> ToStore { get; set; }
          public Nullable<Int32> ReturnStatus { get; set; }
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
	   
		public static void Map(this Inv_ItemReturn entity, Inv_ItemReturnJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.ItemId = entity.ItemId ; 
			 toJson.ReturnDate = entity.ReturnDate ; 
			 toJson.ReceivedBy = entity.ReceivedBy ; 
			 toJson.ToStore = entity.ToStore ; 
			 toJson.ReturnStatus = entity.ReturnStatus ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_ItemReturnJson json, Inv_ItemReturn toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.ItemId = json.ItemId ?? json.ItemId;
    			 toEntity.ReturnDate=json.ReturnDate;
    			 toEntity.ReceivedBy=json.ReceivedBy;
    			 toEntity.ToStore=json.ToStore;
    			 toEntity.ReturnStatus=json.ReturnStatus;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_ItemReturn> entityList, IList<Inv_ItemReturnJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_ItemReturnJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_ItemReturnJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_ItemReturnJson> jsonList, ICollection<Inv_ItemReturn> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_ItemReturn obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_ItemReturn.GetNew();
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
