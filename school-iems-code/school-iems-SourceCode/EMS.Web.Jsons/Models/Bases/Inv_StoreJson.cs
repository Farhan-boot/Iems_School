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
	public partial class Inv_StoreJson 
	{
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> RoomId { get; set; }
        public Nullable<int> CampusId { get; set; }
        public string Phone { get; set; }
        public string Details { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }

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
	   
		public static void Map(this Inv_Store entity, Inv_StoreJson toJson)
        {
            if (entity == null || toJson == null)
                return;

            toJson.Id = entity.Id;
            toJson.Name = entity.Name;
            toJson.RoomId = entity.RoomId; 
            toJson.CampusId = entity.CampusId;
            toJson.Phone = entity.Phone; 
            toJson.Details = entity.Details;
            toJson.Address = entity.Address;
            toJson.Description = entity.Description;
            toJson.Remark = entity.Remark;

            toJson.CreateDate = entity.CreateDate ; 
            toJson.CreateById = entity.CreateById ; 
            toJson.LastChanged = entity.LastChanged ; 
            toJson.LastChangedById = entity.LastChangedById ; 
            toJson.IsDeleted = entity.IsDeleted ;
            
            MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_StoreJson json, Inv_Store toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.RoomId = json.RoomId;
                 toEntity.CampusId = json.CampusId;
                 toEntity.Phone = json.Phone;
                 toEntity.Details = json.Details;
                 toEntity.Address = json.Address;
                 toEntity.Description = json.Description;
                 toEntity.Remark = json.Remark;

    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

            MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_Store> entityList, IList<Inv_StoreJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_StoreJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_StoreJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_StoreJson> jsonList, ICollection<Inv_Store> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_Store obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_Store.GetNew();
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
