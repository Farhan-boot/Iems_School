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
	public partial class BAcnt_CompanyAccountJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String Description { get; set; }
          public Boolean IsCurrent { get; set; }
          public Boolean IsEnable { get; set; }
          public Nullable<DateTime> StartDate { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
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
	   
		public static void Map(this BAcnt_CompanyAccount entity, BAcnt_CompanyAccountJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.Description = entity.Description ; 
			 toJson.IsCurrent = entity.IsCurrent ; 
			 toJson.IsEnable = entity.IsEnable ; 
			 toJson.StartDate = entity.StartDate ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this BAcnt_CompanyAccountJson json, BAcnt_CompanyAccount toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
    			 toEntity.IsCurrent=json.IsCurrent;
    			 toEntity.IsEnable=json.IsEnable;
    			 toEntity.StartDate=json.StartDate;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<BAcnt_CompanyAccount> entityList, IList<BAcnt_CompanyAccountJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 BAcnt_CompanyAccountJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new BAcnt_CompanyAccountJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<BAcnt_CompanyAccountJson> jsonList, ICollection<BAcnt_CompanyAccount> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    BAcnt_CompanyAccount obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = BAcnt_CompanyAccount.GetNew();
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
