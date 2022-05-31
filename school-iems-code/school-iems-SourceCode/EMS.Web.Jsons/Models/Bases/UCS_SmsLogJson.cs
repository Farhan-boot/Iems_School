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
	public partial class UCS_SmsLogJson 
	{
          public String Id { get; set; }
          public String ProviderId { get; set; }
          public String UserId { get; set; }
          public String CallerId { get; set; }
          public String TextMessage { get; set; }
          public DateTime RequestDate { get; set; }
          public String SendById { get; set; }
          public Nullable<DateTime> SendDate { get; set; }
          public Byte ModuleEnumId { get; set; }
          public string Module { get; set; }
          public Nullable<Int32> MessageId { get; set; }
          public Nullable<Byte> StatusEnumId { get; set; }
          public string Status { get; set; }
          public String StatusText { get; set; }
          public Nullable<Int32> ErrorCode { get; set; }
          public String ErrorText { get; set; }
          public Nullable<Int32> SMSCount { get; set; }
          public Nullable<Int32> CurrentCredit { get; set; }
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
	   
		public static void Map(this UCS_SmsLog entity, UCS_SmsLogJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

             if(entity.ProviderId!=null)
			 toJson.ProviderId = entity.ProviderId.ToString() ; 
			 toJson.UserId = entity.UserId.ToString() ; 
			 toJson.CallerId = entity.CallerId ; 
			 toJson.TextMessage = entity.TextMessage ; 
			 toJson.RequestDate = entity.RequestDate ; 
			 toJson.SendById = entity.SendById.ToString() ; 
			 toJson.SendDate = entity.SendDate ; 
			 toJson.ModuleEnumId = entity.ModuleEnumId ; 
             if(entity.ModuleEnumId!=null)
             toJson.Module = entity.Module.ToString().AddSpacesToSentence();
			 toJson.MessageId = entity.MessageId ; 
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.StatusText = entity.StatusText ; 
			 toJson.ErrorCode = entity.ErrorCode ; 
			 toJson.ErrorText = entity.ErrorText ; 
			 toJson.SMSCount = entity.SMSCount ; 
			 toJson.CurrentCredit = entity.CurrentCredit ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this UCS_SmsLogJson json, UCS_SmsLog toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.ProviderId = long.TryParse(json.ProviderId, out output) ? output : (Int64?)null;
                 toEntity.UserId = long.TryParse(json.UserId, out output) ? output : 0;
                 toEntity.CallerId = json.CallerId?.Trim() ?? json.CallerId;
                 toEntity.TextMessage = json.TextMessage?.Trim() ?? json.TextMessage;
    			 toEntity.RequestDate=json.RequestDate;
                 toEntity.SendById = long.TryParse(json.SendById, out output) ? output : 0;
    			 toEntity.SendDate=json.SendDate;
    			 toEntity.ModuleEnumId=json.ModuleEnumId;
    			 toEntity.MessageId=json.MessageId;
    			 toEntity.StatusEnumId=json.StatusEnumId;
                 toEntity.StatusText = json.StatusText?.Trim() ?? json.StatusText;
    			 toEntity.ErrorCode=json.ErrorCode;
                 toEntity.ErrorText = json.ErrorText?.Trim() ?? json.ErrorText;
    			 toEntity.SMSCount=json.SMSCount;
    			 toEntity.CurrentCredit=json.CurrentCredit;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<UCS_SmsLog> entityList, IList<UCS_SmsLogJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new UCS_SmsLogJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<UCS_SmsLogJson> jsonList, ICollection<UCS_SmsLog> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                UCS_SmsLog obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = UCS_SmsLog.GetNew();
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
