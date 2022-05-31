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
	public partial class Aca_ClassRoutineJson 
	{
          public String Id { get; set; }
          public Byte DayEnumId { get; set; }
          public string Day { get; set; }
          public DateTime StartTime { get; set; }
          public DateTime EndTime { get; set; }
          public String ClassId { get; set; }
          public String RoomId { get; set; }
          public Nullable<Int32> EmployeeId { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public String Remarks { get; set; }
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
	   
		public static void Map(this Aca_ClassRoutine entity, Aca_ClassRoutineJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.DayEnumId = entity.DayEnumId ; 
             if(entity.DayEnumId!=null)
             toJson.Day = entity.Day.ToString().AddSpacesToSentence();
			 toJson.StartTime = entity.StartTime ; 
			 toJson.EndTime = entity.EndTime ; 
			 toJson.ClassId = entity.ClassId.ToString() ; 
             if(entity.RoomId!=null)
			 toJson.RoomId = entity.RoomId.ToString() ; 
			 toJson.EmployeeId = entity.EmployeeId ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.Remarks = entity.Remarks ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ClassRoutineJson json, Aca_ClassRoutine toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.DayEnumId=json.DayEnumId;
    			 toEntity.StartTime=json.StartTime;
    			 toEntity.EndTime=json.EndTime;
                 toEntity.ClassId = long.TryParse(json.ClassId, out output) ? output : 0;
                 toEntity.RoomId = long.TryParse(json.RoomId, out output) ? output : (Int64?)null;
    			 toEntity.EmployeeId=json.EmployeeId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_ClassRoutine> entityList, IList<Aca_ClassRoutineJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_ClassRoutineJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ClassRoutineJson> jsonList, ICollection<Aca_ClassRoutine> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_ClassRoutine obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_ClassRoutine.GetNew();
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
