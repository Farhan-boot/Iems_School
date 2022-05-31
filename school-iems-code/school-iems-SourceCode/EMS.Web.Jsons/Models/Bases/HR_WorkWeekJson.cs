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
	public partial class HR_WorkWeekJson 
	{
          public Int32 Id { get; set; }
          public Byte DayEnumId { get; set; }
          public string Day { get; set; }
          public Boolean IsHalfDay { get; set; }
          public Byte WorkingDayTypeEnumId { get; set; }
          public string WorkingDayType { get; set; }
          public Int32 CalendarYearId { get; set; }
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
	   
		public static void Map(this HR_WorkWeek entity, HR_WorkWeekJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.DayEnumId = entity.DayEnumId ; 
             if(entity.DayEnumId!=null)
             toJson.Day = entity.Day.ToString().AddSpacesToSentence();
			 toJson.IsHalfDay = entity.IsHalfDay ; 
			 toJson.WorkingDayTypeEnumId = entity.WorkingDayTypeEnumId ; 
             if(entity.WorkingDayTypeEnumId!=null)
             toJson.WorkingDayType = entity.WorkingDayType.ToString().AddSpacesToSentence();
			 toJson.CalendarYearId = entity.CalendarYearId ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_WorkWeekJson json, HR_WorkWeek toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.DayEnumId=json.DayEnumId;
    			 toEntity.IsHalfDay=json.IsHalfDay;
    			 toEntity.WorkingDayTypeEnumId=json.WorkingDayTypeEnumId;
    			 toEntity.CalendarYearId=json.CalendarYearId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_WorkWeek> entityList, IList<HR_WorkWeekJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_WorkWeekJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_WorkWeekJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_WorkWeekJson> jsonList, ICollection<HR_WorkWeek> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_WorkWeek obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_WorkWeek.GetNew();
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
