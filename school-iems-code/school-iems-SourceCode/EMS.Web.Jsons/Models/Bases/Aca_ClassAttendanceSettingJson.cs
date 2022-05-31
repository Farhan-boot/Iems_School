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
	public partial class Aca_ClassAttendanceSettingJson 
	{
          public String Id { get; set; }
          public String ClassId { get; set; }
          public String RoutineId { get; set; }
          public DateTime StartTime { get; set; }
          public DateTime EndTime { get; set; }
          public Int32 EmployeeId { get; set; }
          public Byte ClassTypeEnumId { get; set; }
          public string ClassType { get; set; }
          public Boolean IsLocked { get; set; }
          public String Remark { get; set; }
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
	   
		public static void Map(this Aca_ClassAttendanceSetting entity, Aca_ClassAttendanceSettingJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.ClassId = entity.ClassId.ToString() ; 
             if(entity.RoutineId!=null)
			 toJson.RoutineId = entity.RoutineId.ToString() ; 
			 toJson.StartTime = entity.StartTime ; 
			 toJson.EndTime = entity.EndTime ; 
			 toJson.EmployeeId = entity.EmployeeId ; 
			 toJson.ClassTypeEnumId = entity.ClassTypeEnumId ; 
             if(entity.ClassTypeEnumId!=null)
             toJson.ClassType = entity.ClassType.ToString().AddSpacesToSentence();
			 toJson.IsLocked = entity.IsLocked ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ClassAttendanceSettingJson json, Aca_ClassAttendanceSetting toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.ClassId = long.TryParse(json.ClassId, out output) ? output : 0;
                 toEntity.RoutineId = long.TryParse(json.RoutineId, out output) ? output : (Int64?)null;
    			 toEntity.StartTime=json.StartTime;
    			 toEntity.EndTime=json.EndTime;
    			 toEntity.EmployeeId=json.EmployeeId;
    			 toEntity.ClassTypeEnumId=json.ClassTypeEnumId;
    			 toEntity.IsLocked=json.IsLocked;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_ClassAttendanceSetting> entityList, IList<Aca_ClassAttendanceSettingJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_ClassAttendanceSettingJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ClassAttendanceSettingJson> jsonList, ICollection<Aca_ClassAttendanceSetting> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_ClassAttendanceSetting obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_ClassAttendanceSetting.GetNew();
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
