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
	public partial class Htl_RoomJson 
	{
          public Int32 Id { get; set; }
          public Int32 HostelBuildingId { get; set; }
          public Int32 FloorId { get; set; }
          public String RoomNumber { get; set; }
          public Int32 Seat { get; set; }
          public Int32 Bathroom { get; set; }
          public Int32 Veranda { get; set; }
          public Boolean HasAC { get; set; }
          public Int32 Occupied { get; set; }
          public Int32 Vaccancy { get; set; }
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
	   
		public static void Map(this Htl_Room entity, Htl_RoomJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.HostelBuildingId = entity.HostelBuildingId ; 
			 toJson.FloorId = entity.FloorId ; 
			 toJson.RoomNumber = entity.RoomNumber ; 
			 toJson.Seat = entity.Seat ; 
			 toJson.Bathroom = entity.Bathroom ; 
			 toJson.Veranda = entity.Veranda ; 
			 toJson.HasAC = entity.HasAC ; 
			 toJson.Occupied = entity.Occupied ; 
			 toJson.Vaccancy = entity.Vaccancy ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Htl_RoomJson json, Htl_Room toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.HostelBuildingId=json.HostelBuildingId;
    			 toEntity.FloorId=json.FloorId;
                 toEntity.RoomNumber = json.RoomNumber?.Trim() ?? json.RoomNumber;
    			 toEntity.Seat=json.Seat;
    			 toEntity.Bathroom=json.Bathroom;
    			 toEntity.Veranda=json.Veranda;
    			 toEntity.HasAC=json.HasAC;
    			 toEntity.Occupied=json.Occupied;
    			 toEntity.Vaccancy=json.Vaccancy;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Htl_Room> entityList, IList<Htl_RoomJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Htl_RoomJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Htl_RoomJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Htl_RoomJson> jsonList, ICollection<Htl_Room> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Htl_Room obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Htl_Room.GetNew();
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
