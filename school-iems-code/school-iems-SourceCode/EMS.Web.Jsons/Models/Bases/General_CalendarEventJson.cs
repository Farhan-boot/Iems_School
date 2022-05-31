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
	public partial class General_CalendarEventJson 
	{
          public String Id { get; set; }
          public String Name { get; set; }
          public String Description { get; set; }
          public String Location { get; set; }
          public DateTime StartTime { get; set; }
          public DateTime EndTime { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public String Remarks { get; set; }
          public Boolean IsAllDay { get; set; }
          public String EventColor { get; set; }
          public Byte VisibilityEnumId { get; set; }
          public string Visibility { get; set; }
          public Byte RepeatsEnumId { get; set; }
          public string Repeats { get; set; }
          public Byte RepeatsPeriodEnumId { get; set; }
          public string RepeatsPeriod { get; set; }
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
	   
		public static void Map(this General_CalendarEvent entity, General_CalendarEventJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Name = entity.Name ; 
			 toJson.Description = entity.Description ; 
			 toJson.Location = entity.Location ; 
			 toJson.StartTime = entity.StartTime ; 
			 toJson.EndTime = entity.EndTime ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.IsAllDay = entity.IsAllDay ; 
			 toJson.EventColor = entity.EventColor ; 
			 toJson.VisibilityEnumId = entity.VisibilityEnumId ; 
             if(entity.VisibilityEnumId!=null)
             toJson.Visibility = entity.Visibility.ToString().AddSpacesToSentence();
			 toJson.RepeatsEnumId = entity.RepeatsEnumId ; 
             if(entity.RepeatsEnumId!=null)
             toJson.Repeats = entity.Repeats.ToString().AddSpacesToSentence();
			 toJson.RepeatsPeriodEnumId = entity.RepeatsPeriodEnumId ; 
             if(entity.RepeatsPeriodEnumId!=null)
             toJson.RepeatsPeriod = entity.RepeatsPeriod.ToString().AddSpacesToSentence();

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this General_CalendarEventJson json, General_CalendarEvent toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
                 toEntity.Location = json.Location?.Trim() ?? json.Location;
    			 toEntity.StartTime=json.StartTime;
    			 toEntity.EndTime=json.EndTime;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
    			 toEntity.IsAllDay=json.IsAllDay;
                 toEntity.EventColor = json.EventColor?.Trim() ?? json.EventColor;
    			 toEntity.VisibilityEnumId=json.VisibilityEnumId;
    			 toEntity.RepeatsEnumId=json.RepeatsEnumId;
    			 toEntity.RepeatsPeriodEnumId=json.RepeatsPeriodEnumId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<General_CalendarEvent> entityList, IList<General_CalendarEventJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new General_CalendarEventJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<General_CalendarEventJson> jsonList, ICollection<General_CalendarEvent> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                General_CalendarEvent obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = General_CalendarEvent.GetNew();
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
