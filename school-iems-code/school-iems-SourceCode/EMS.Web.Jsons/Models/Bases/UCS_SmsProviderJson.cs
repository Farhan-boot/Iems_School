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
	public partial class UCS_SmsProviderJson 
	{
          public String Id { get; set; }
          public String Name { get; set; }
          public String Description { get; set; }
          public String UserName { get; set; }
          public String Password { get; set; }
          public String SenderId { get; set; }
          public String ServiceUrl { get; set; }
          public String ApiKey { get; set; }
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
	   
		public static void Map(this UCS_SmsProvider entity, UCS_SmsProviderJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Name = entity.Name ; 
			 toJson.Description = entity.Description ; 
			 toJson.UserName = entity.UserName ; 
			 toJson.Password = entity.Password ; 
			 toJson.SenderId = entity.SenderId ; 
			 toJson.ServiceUrl = entity.ServiceUrl ; 
			 toJson.ApiKey = entity.ApiKey ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this UCS_SmsProviderJson json, UCS_SmsProvider toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
                 toEntity.UserName = json.UserName?.Trim() ?? json.UserName;
                 toEntity.Password = json.Password?.Trim() ?? json.Password;
                 toEntity.SenderId = json.SenderId?.Trim() ?? json.SenderId;
                 toEntity.ServiceUrl = json.ServiceUrl?.Trim() ?? json.ServiceUrl;
                 toEntity.ApiKey = json.ApiKey?.Trim() ?? json.ApiKey;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<UCS_SmsProvider> entityList, IList<UCS_SmsProviderJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new UCS_SmsProviderJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<UCS_SmsProviderJson> jsonList, ICollection<UCS_SmsProvider> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                UCS_SmsProvider obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = UCS_SmsProvider.GetNew();
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
