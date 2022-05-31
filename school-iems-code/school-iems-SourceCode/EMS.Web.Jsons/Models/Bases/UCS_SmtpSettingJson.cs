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
	public partial class UCS_SmtpSettingJson 
	{
          public String Id { get; set; }
          public String Name { get; set; }
          public String Description { get; set; }
          public String DomainName { get; set; }
          public String EmailAddress { get; set; }
          public String Host { get; set; }
          public Int32 Port { get; set; }
          public String UserName { get; set; }
          public String Password { get; set; }
          public Boolean EnableSsl { get; set; }
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
	   
		public static void Map(this UCS_SmtpSetting entity, UCS_SmtpSettingJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Name = entity.Name ; 
			 toJson.Description = entity.Description ; 
			 toJson.DomainName = entity.DomainName ; 
			 toJson.EmailAddress = entity.EmailAddress ; 
			 toJson.Host = entity.Host ; 
			 toJson.Port = entity.Port ; 
			 toJson.UserName = entity.UserName ; 
			 toJson.Password = entity.Password ; 
			 toJson.EnableSsl = entity.EnableSsl ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this UCS_SmtpSettingJson json, UCS_SmtpSetting toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
                 toEntity.DomainName = json.DomainName?.Trim() ?? json.DomainName;
                 toEntity.EmailAddress = json.EmailAddress?.Trim() ?? json.EmailAddress;
                 toEntity.Host = json.Host?.Trim() ?? json.Host;
    			 toEntity.Port=json.Port;
                 toEntity.UserName = json.UserName?.Trim() ?? json.UserName;
                 toEntity.Password = json.Password?.Trim() ?? json.Password;
    			 toEntity.EnableSsl=json.EnableSsl;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<UCS_SmtpSetting> entityList, IList<UCS_SmtpSettingJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new UCS_SmtpSettingJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<UCS_SmtpSettingJson> jsonList, ICollection<UCS_SmtpSetting> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                UCS_SmtpSetting obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = UCS_SmtpSetting.GetNew();
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
