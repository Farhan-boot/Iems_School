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
	public partial class Adm_StudentQuotaNameJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String Description { get; set; }
          public Int32 OrderNo { get; set; }
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
	   
		public static void Map(this Adm_StudentQuotaName entity, Adm_StudentQuotaNameJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.Description = entity.Description ; 
			 toJson.OrderNo = entity.OrderNo ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Adm_StudentQuotaNameJson json, Adm_StudentQuotaName toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
    			 toEntity.OrderNo=json.OrderNo;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Adm_StudentQuotaName> entityList, IList<Adm_StudentQuotaNameJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Adm_StudentQuotaNameJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Adm_StudentQuotaNameJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Adm_StudentQuotaNameJson> jsonList, ICollection<Adm_StudentQuotaName> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Adm_StudentQuotaName obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Adm_StudentQuotaName.GetNew();
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
