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
	public partial class General_DivisionJson 
	{
          public String Id { get; set; }
          public String Name { get; set; }
          public Nullable<Int32> DivisionNo { get; set; }
          public Int32 CountryId { get; set; }
          public Int32 OrderNo { get; set; }
          public Boolean IsDeleted { get; set; }
          public Boolean IsActive { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreatedById { get; set; }
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
	   
		public static void Map(this General_Division entity, General_DivisionJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Name = entity.Name ; 
			 toJson.DivisionNo = entity.DivisionNo ; 
			 toJson.CountryId = entity.CountryId ; 
			 toJson.OrderNo = entity.OrderNo ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.IsActive = entity.IsActive ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreatedById = entity.CreatedById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this General_DivisionJson json, General_Division toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.DivisionNo=json.DivisionNo;
    			 toEntity.CountryId=json.CountryId;
    			 toEntity.OrderNo=json.OrderNo;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.IsActive=json.IsActive;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreatedById = long.TryParse(json.CreatedById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<General_Division> entityList, IList<General_DivisionJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new General_DivisionJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<General_DivisionJson> jsonList, ICollection<General_Division> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                General_Division obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = General_Division.GetNew();
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
