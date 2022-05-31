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
	public partial class BAcnt_BranchJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String ShortName { get; set; }
          public String Code { get; set; }
          public String Address { get; set; }
          public Int32 OrderNo { get; set; }
          public Boolean IsEnable { get; set; }
          public Boolean IsDeleted { get; set; }
          public Int32 CompanyId { get; set; }
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
	   
		public static void Map(this BAcnt_Branch entity, BAcnt_BranchJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.Code = entity.Code ; 
			 toJson.Address = entity.Address ; 
			 toJson.OrderNo = entity.OrderNo ; 
			 toJson.IsEnable = entity.IsEnable ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.CompanyId = entity.CompanyId ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this BAcnt_BranchJson json, BAcnt_Branch toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
                 toEntity.Code = json.Code?.Trim() ?? json.Code;
                 toEntity.Address = json.Address?.Trim() ?? json.Address;
    			 toEntity.OrderNo=json.OrderNo;
    			 toEntity.IsEnable=json.IsEnable;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.CompanyId=json.CompanyId;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<BAcnt_Branch> entityList, IList<BAcnt_BranchJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 BAcnt_BranchJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new BAcnt_BranchJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<BAcnt_BranchJson> jsonList, ICollection<BAcnt_Branch> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    BAcnt_Branch obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = BAcnt_Branch.GetNew();
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
