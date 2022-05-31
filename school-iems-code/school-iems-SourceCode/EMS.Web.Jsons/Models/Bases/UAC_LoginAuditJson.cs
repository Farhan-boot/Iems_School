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
	public partial class UAC_LoginAuditJson 
	{
          public String Id { get; set; }
          public String UserId { get; set; }
          public String UserName { get; set; }
          public String Password { get; set; }
          public DateTime AuditDate { get; set; }
          public String FromIp { get; set; }
          public String MacAddress { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public Boolean IsMobileBrowser { get; set; }
          public String Browser { get; set; }
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
	   
		public static void Map(this UAC_LoginAudit entity, UAC_LoginAuditJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.UserId = entity.UserId.ToString() ; 
			 toJson.UserName = entity.UserName ; 
			 toJson.Password = entity.Password ; 
			 toJson.AuditDate = entity.AuditDate ; 
			 toJson.FromIp = entity.FromIp ; 
			 toJson.MacAddress = entity.MacAddress ; 
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.IsMobileBrowser = entity.IsMobileBrowser ; 
			 toJson.Browser = entity.Browser ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this UAC_LoginAuditJson json, UAC_LoginAudit toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.UserId = long.TryParse(json.UserId, out output) ? output : 0;
                 toEntity.UserName = json.UserName?.Trim() ?? json.UserName;
                 toEntity.Password = json.Password?.Trim() ?? json.Password;
    			 toEntity.AuditDate=json.AuditDate;
                 toEntity.FromIp = json.FromIp?.Trim() ?? json.FromIp;
                 toEntity.MacAddress = json.MacAddress?.Trim() ?? json.MacAddress;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.IsMobileBrowser=json.IsMobileBrowser;
                 toEntity.Browser = json.Browser?.Trim() ?? json.Browser;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<UAC_LoginAudit> entityList, IList<UAC_LoginAuditJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new UAC_LoginAuditJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<UAC_LoginAuditJson> jsonList, ICollection<UAC_LoginAudit> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                UAC_LoginAudit obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = UAC_LoginAudit.GetNew();
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
