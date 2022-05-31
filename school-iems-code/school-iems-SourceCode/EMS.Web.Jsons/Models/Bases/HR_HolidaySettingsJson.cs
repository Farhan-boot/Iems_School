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
	public partial class HR_HolidaySettingsJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public DateTime StartDate { get; set; }
          public DateTime EndDate { get; set; }
          public Int32 CalendarYearId { get; set; }
          public Byte WorkingDayTypeEnumId { get; set; }
          public string WorkingDayType { get; set; }
          public Boolean IsHalfDay { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
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
	   
		public static void Map(this HR_HolidaySettings entity, HR_HolidaySettingsJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.StartDate = entity.StartDate ; 
			 toJson.EndDate = entity.EndDate ; 
			 toJson.CalendarYearId = entity.CalendarYearId ; 
			 toJson.WorkingDayTypeEnumId = entity.WorkingDayTypeEnumId ; 
             if(entity.WorkingDayTypeEnumId!=null)
             toJson.WorkingDayType = entity.WorkingDayType.ToString().AddSpacesToSentence();
			 toJson.IsHalfDay = entity.IsHalfDay ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_HolidaySettingsJson json, HR_HolidaySettings toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.StartDate=json.StartDate;
    			 toEntity.EndDate=json.EndDate;
    			 toEntity.CalendarYearId=json.CalendarYearId;
    			 toEntity.WorkingDayTypeEnumId=json.WorkingDayTypeEnumId;
    			 toEntity.IsHalfDay=json.IsHalfDay;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_HolidaySettings> entityList, IList<HR_HolidaySettingsJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_HolidaySettingsJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_HolidaySettingsJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_HolidaySettingsJson> jsonList, ICollection<HR_HolidaySettings> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_HolidaySettings obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_HolidaySettings.GetNew();
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
