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
	public partial class Aca_ClassAttendanceJson 
	{
          public String Id { get; set; }
          public String SettingId { get; set; }
          public String ClassId { get; set; }
          public Int32 StudentId { get; set; }
          public Boolean IsAbsent { get; set; }
          public Boolean IsLate { get; set; }
          public Boolean IsLeftEarly { get; set; }
          public Nullable<Byte> ReasonEnumId { get; set; }
          public string Reason { get; set; }
          public String Remark { get; set; }
          public DateTime StartTime { get; set; }
          public DateTime EndTime { get; set; }
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
	   
		public static void Map(this Aca_ClassAttendance entity, Aca_ClassAttendanceJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.SettingId = entity.SettingId.ToString() ; 
			 toJson.ClassId = entity.ClassId.ToString() ; 
			 toJson.StudentId = entity.StudentId ; 
			 toJson.IsAbsent = entity.IsAbsent ; 
			 toJson.IsLate = entity.IsLate ; 
			 toJson.IsLeftEarly = entity.IsLeftEarly ; 
			 toJson.ReasonEnumId = entity.ReasonEnumId ; 
             if(entity.ReasonEnumId!=null)
             toJson.Reason = entity.Reason.ToString().AddSpacesToSentence();
			 toJson.Remark = entity.Remark ; 
			 toJson.StartTime = entity.StartTime ; 
			 toJson.EndTime = entity.EndTime ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ClassAttendanceJson json, Aca_ClassAttendance toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.SettingId = long.TryParse(json.SettingId, out output) ? output : 0;
                 toEntity.ClassId = long.TryParse(json.ClassId, out output) ? output : 0;
    			 toEntity.StudentId=json.StudentId;
    			 toEntity.IsAbsent=json.IsAbsent;
    			 toEntity.IsLate=json.IsLate;
    			 toEntity.IsLeftEarly=json.IsLeftEarly;
    			 toEntity.ReasonEnumId=json.ReasonEnumId;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
    			 toEntity.StartTime=json.StartTime;
    			 toEntity.EndTime=json.EndTime;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_ClassAttendance> entityList, IList<Aca_ClassAttendanceJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_ClassAttendanceJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ClassAttendanceJson> jsonList, ICollection<Aca_ClassAttendance> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_ClassAttendance obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_ClassAttendance.GetNew();
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
