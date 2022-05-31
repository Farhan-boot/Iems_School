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
	public partial class Htl_HostalBuildingJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public Int32 BuildingTypeEnumId { get; set; }
          public string BuildingType { get; set; }
          public String HouseNo { get; set; }
          public String RoadNo { get; set; }
          public String Address { get; set; }
          public Nullable<Int32> TotalFloars { get; set; }
          public Nullable<Int32> TotalRooms { get; set; }
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
	   
		public static void Map(this Htl_HostalBuilding entity, Htl_HostalBuildingJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.BuildingTypeEnumId = entity.BuildingTypeEnumId ; 
             if(entity.BuildingTypeEnumId!=null)
             toJson.BuildingType = entity.BuildingType.ToString().AddSpacesToSentence();
			 toJson.HouseNo = entity.HouseNo ; 
			 toJson.RoadNo = entity.RoadNo ; 
			 toJson.Address = entity.Address ; 
			 toJson.TotalFloars = entity.TotalFloars ; 
			 toJson.TotalRooms = entity.TotalRooms ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Htl_HostalBuildingJson json, Htl_HostalBuilding toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.BuildingTypeEnumId=json.BuildingTypeEnumId;
                 toEntity.HouseNo = json.HouseNo?.Trim() ?? json.HouseNo;
                 toEntity.RoadNo = json.RoadNo?.Trim() ?? json.RoadNo;
                 toEntity.Address = json.Address?.Trim() ?? json.Address;
    			 toEntity.TotalFloars=json.TotalFloars;
    			 toEntity.TotalRooms=json.TotalRooms;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Htl_HostalBuilding> entityList, IList<Htl_HostalBuildingJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Htl_HostalBuildingJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Htl_HostalBuildingJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Htl_HostalBuildingJson> jsonList, ICollection<Htl_HostalBuilding> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Htl_HostalBuilding obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Htl_HostalBuilding.GetNew();
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
