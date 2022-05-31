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
	public partial class General_AllSmsLogJson 
	{
          public String Id { get; set; }
          public String MobileNumber { get; set; }
          public Byte TypeEnumId { get; set; }
          public string Type { get; set; }
          public String SmsText { get; set; }
          public Byte DeliveryStatusEnumId { get; set; }
          public string DeliveryStatus { get; set; }
          public Nullable<Int32> SentToUserId { get; set; }
          public String ObjId { get; set; }
          public String DeliveryErrorText { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
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
	   
		public static void Map(this General_AllSmsLog entity, General_AllSmsLogJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.MobileNumber = entity.MobileNumber ; 
			 toJson.TypeEnumId = entity.TypeEnumId ; 
             if(entity.TypeEnumId!=null)
             toJson.Type = entity.Type.ToString().AddSpacesToSentence();
			 toJson.SmsText = entity.SmsText ; 
			 toJson.DeliveryStatusEnumId = entity.DeliveryStatusEnumId ; 
             if(entity.DeliveryStatusEnumId!=null)
             toJson.DeliveryStatus = entity.DeliveryStatus.ToString().AddSpacesToSentence();
			 toJson.SentToUserId = entity.SentToUserId ; 
             if(entity.ObjId!=null)
			 toJson.ObjId = entity.ObjId.ToString() ; 
			 toJson.DeliveryErrorText = entity.DeliveryErrorText ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this General_AllSmsLogJson json, General_AllSmsLog toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.MobileNumber = json.MobileNumber?.Trim() ?? json.MobileNumber;
    			 toEntity.TypeEnumId=json.TypeEnumId;
                 toEntity.SmsText = json.SmsText?.Trim() ?? json.SmsText;
    			 toEntity.DeliveryStatusEnumId=json.DeliveryStatusEnumId;
    			 toEntity.SentToUserId=json.SentToUserId;
                 toEntity.ObjId = long.TryParse(json.ObjId, out output) ? output : (Int64?)null;
                 toEntity.DeliveryErrorText = json.DeliveryErrorText?.Trim() ?? json.DeliveryErrorText;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<General_AllSmsLog> entityList, IList<General_AllSmsLogJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new General_AllSmsLogJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<General_AllSmsLogJson> jsonList, ICollection<General_AllSmsLog> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                General_AllSmsLog obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = General_AllSmsLog.GetNew();
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
