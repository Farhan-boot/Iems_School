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
	public partial class Aca_ClassAttendanceSmsLogJson 
	{
          public String Id { get; set; }
          public String ClassId { get; set; }
          public Int32 StudentId { get; set; }
          public String AttendanceSettingId { get; set; }
          public String SmsText { get; set; }
          public String MobileNumber { get; set; }
          public Byte DeliveryStatusEnumId { get; set; }
          public string DeliveryStatus { get; set; }
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
	   
		public static void Map(this Aca_ClassAttendanceSmsLog entity, Aca_ClassAttendanceSmsLogJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.ClassId = entity.ClassId.ToString() ; 
			 toJson.StudentId = entity.StudentId ; 
			 toJson.AttendanceSettingId = entity.AttendanceSettingId.ToString() ; 
			 toJson.SmsText = entity.SmsText ; 
			 toJson.MobileNumber = entity.MobileNumber ; 
			 toJson.DeliveryStatusEnumId = entity.DeliveryStatusEnumId ; 
             if(entity.DeliveryStatusEnumId!=null)
             toJson.DeliveryStatus = entity.DeliveryStatus.ToString().AddSpacesToSentence();
			 toJson.DeliveryErrorText = entity.DeliveryErrorText ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ClassAttendanceSmsLogJson json, Aca_ClassAttendanceSmsLog toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.ClassId = long.TryParse(json.ClassId, out output) ? output : 0;
    			 toEntity.StudentId=json.StudentId;
                 toEntity.AttendanceSettingId = long.TryParse(json.AttendanceSettingId, out output) ? output : 0;
                 toEntity.SmsText = json.SmsText?.Trim() ?? json.SmsText;
                 toEntity.MobileNumber = json.MobileNumber?.Trim() ?? json.MobileNumber;
    			 toEntity.DeliveryStatusEnumId=json.DeliveryStatusEnumId;
                 toEntity.DeliveryErrorText = json.DeliveryErrorText?.Trim() ?? json.DeliveryErrorText;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_ClassAttendanceSmsLog> entityList, IList<Aca_ClassAttendanceSmsLogJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_ClassAttendanceSmsLogJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ClassAttendanceSmsLogJson> jsonList, ICollection<Aca_ClassAttendanceSmsLog> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_ClassAttendanceSmsLog obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_ClassAttendanceSmsLog.GetNew();
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
