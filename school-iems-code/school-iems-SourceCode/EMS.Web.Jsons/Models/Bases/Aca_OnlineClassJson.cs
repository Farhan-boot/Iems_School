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
	public partial class Aca_OnlineClassJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public DateTime ClassDateTime { get; set; }
          public Single ClassDuration { get; set; }
          public Int32 ProgramId { get; set; }
          public String CurriculumCourseId { get; set; }
          public Byte ClassMediaEnumId { get; set; }
          public string ClassMedia { get; set; }
          public String ClassUrl { get; set; }
          public String Remark { get; set; }
          public String History { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public String InquiryMobileNumber { get; set; }
          public String SyllabusInformation { get; set; }
          public Boolean IsRecurring { get; set; }
          public Nullable<Byte> RecurringDayEnumId { get; set; }
          public string RecurringDay { get; set; }
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
	   
		public static void Map(this Aca_OnlineClass entity, Aca_OnlineClassJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.ClassDateTime = entity.ClassDateTime ; 
			 toJson.ClassDuration = entity.ClassDuration ; 
			 toJson.ProgramId = entity.ProgramId ; 
			 toJson.CurriculumCourseId = entity.CurriculumCourseId.ToString() ; 
			 toJson.ClassMediaEnumId = entity.ClassMediaEnumId ; 
             if(entity.ClassMediaEnumId!=null)
             toJson.ClassMedia = entity.ClassMedia.ToString().AddSpacesToSentence();
			 toJson.ClassUrl = entity.ClassUrl ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.InquiryMobileNumber = entity.InquiryMobileNumber ; 
			 toJson.SyllabusInformation = entity.SyllabusInformation ; 
			 toJson.IsRecurring = entity.IsRecurring ; 
			 toJson.RecurringDayEnumId = entity.RecurringDayEnumId ; 
             if(entity.RecurringDayEnumId!=null)
             toJson.RecurringDay = entity.RecurringDay.ToString().AddSpacesToSentence();

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_OnlineClassJson json, Aca_OnlineClass toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.ClassDateTime=json.ClassDateTime;
    			 toEntity.ClassDuration=json.ClassDuration;
    			 toEntity.ProgramId=json.ProgramId;
                 toEntity.CurriculumCourseId = long.TryParse(json.CurriculumCourseId, out output) ? output : 0;
    			 toEntity.ClassMediaEnumId=json.ClassMediaEnumId;
                 toEntity.ClassUrl = json.ClassUrl?.Trim() ?? json.ClassUrl;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;
                 toEntity.InquiryMobileNumber = json.InquiryMobileNumber?.Trim() ?? json.InquiryMobileNumber;
                 toEntity.SyllabusInformation = json.SyllabusInformation?.Trim() ?? json.SyllabusInformation;
    			 toEntity.IsRecurring=json.IsRecurring;
    			 toEntity.RecurringDayEnumId=json.RecurringDayEnumId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_OnlineClass> entityList, IList<Aca_OnlineClassJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_OnlineClassJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_OnlineClassJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_OnlineClassJson> jsonList, ICollection<Aca_OnlineClass> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_OnlineClass obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_OnlineClass.GetNew();
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
