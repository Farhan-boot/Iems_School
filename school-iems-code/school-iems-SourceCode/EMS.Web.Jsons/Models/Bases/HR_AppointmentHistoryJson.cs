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
	public partial class HR_AppointmentHistoryJson 
	{
          public String Id { get; set; }
          public Int32 EmployeeId { get; set; }
          public Int32 PositionId { get; set; }
          public Int32 DepartmentId { get; set; }
          public DateTime StartDate { get; set; }
          public Nullable<DateTime> EndDate { get; set; }
          public Boolean IsActive { get; set; }
          public Boolean IsActing { get; set; }
          public Byte HistoryTypeEnumId { get; set; }
          public string HistoryType { get; set; }
          public Boolean IsPrimary { get; set; }
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
	   
		public static void Map(this HR_AppointmentHistory entity, HR_AppointmentHistoryJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.EmployeeId = entity.EmployeeId ; 
			 toJson.PositionId = entity.PositionId ; 
			 toJson.DepartmentId = entity.DepartmentId ; 
			 toJson.StartDate = entity.StartDate ; 
			 toJson.EndDate = entity.EndDate ; 
			 toJson.IsActive = entity.IsActive ; 
			 toJson.IsActing = entity.IsActing ; 
			 toJson.HistoryTypeEnumId = entity.HistoryTypeEnumId ; 
             if(entity.HistoryTypeEnumId!=null)
             toJson.HistoryType = entity.HistoryType.ToString().AddSpacesToSentence();
			 toJson.IsPrimary = entity.IsPrimary ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_AppointmentHistoryJson json, HR_AppointmentHistory toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.EmployeeId=json.EmployeeId;
    			 toEntity.PositionId=json.PositionId;
    			 toEntity.DepartmentId=json.DepartmentId;
    			 toEntity.StartDate=json.StartDate;
    			 toEntity.EndDate=json.EndDate;
    			 toEntity.IsActive=json.IsActive;
    			 toEntity.IsActing=json.IsActing;
    			 toEntity.HistoryTypeEnumId=json.HistoryTypeEnumId;
    			 toEntity.IsPrimary=json.IsPrimary;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_AppointmentHistory> entityList, IList<HR_AppointmentHistoryJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new HR_AppointmentHistoryJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_AppointmentHistoryJson> jsonList, ICollection<HR_AppointmentHistory> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                HR_AppointmentHistory obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = HR_AppointmentHistory.GetNew();
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
