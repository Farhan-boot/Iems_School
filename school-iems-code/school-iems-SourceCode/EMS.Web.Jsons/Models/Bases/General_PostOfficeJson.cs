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
	public partial class General_PostOfficeJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String PostCode { get; set; }
          public Int32 PoliceStationId { get; set; }
          public Int32 DistrictId { get; set; }
          public Int32 OrderNo { get; set; }
          public Boolean IsDeleted { get; set; }
          public Boolean IsActive { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
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
	   
		public static void Map(this General_PostOffice entity, General_PostOfficeJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.PostCode = entity.PostCode ; 
			 toJson.PoliceStationId = entity.PoliceStationId ; 
			 toJson.DistrictId = entity.DistrictId ; 
			 toJson.OrderNo = entity.OrderNo ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.IsActive = entity.IsActive ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this General_PostOfficeJson json, General_PostOffice toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.PostCode = json.PostCode?.Trim() ?? json.PostCode;
    			 toEntity.PoliceStationId=json.PoliceStationId;
    			 toEntity.DistrictId=json.DistrictId;
    			 toEntity.OrderNo=json.OrderNo;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.IsActive=json.IsActive;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<General_PostOffice> entityList, IList<General_PostOfficeJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 General_PostOfficeJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new General_PostOfficeJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<General_PostOfficeJson> jsonList, ICollection<General_PostOffice> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    General_PostOffice obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = General_PostOffice.GetNew();
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
