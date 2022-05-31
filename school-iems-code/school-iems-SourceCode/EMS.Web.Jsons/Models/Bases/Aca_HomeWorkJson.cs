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
	public partial class Aca_HomeWorkJson 
	{
          public Int32 Id { get; set; }
          public String Title { get; set; }
          public DateTime DueTime { get; set; }
          public DateTime CloseTime { get; set; }
          public Nullable<Int32> GroupEnumId { get; set; }
          public string Group { get; set; }
          public Int32 HomeworkTypeEnumId { get; set; }
          public string HomeworkType { get; set; }
          public String HomeworkKey { get; set; }
          public String Description { get; set; }
          public Int32 TeacherId { get; set; }
          public String Remark { get; set; }
          public String History { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public Int32 ProgramId { get; set; }
          public String CourseId { get; set; }
          public Single Mark { get; set; }
          public Byte TeacherHomeworkTypeEnumId { get; set; }
          public string TeacherHomeworkType { get; set; }
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
	   
		public static void Map(this Aca_HomeWork entity, Aca_HomeWorkJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Title = entity.Title ; 
			 toJson.DueTime = entity.DueTime ; 
			 toJson.CloseTime = entity.CloseTime ; 
			 toJson.GroupEnumId = entity.GroupEnumId ; 
             if(entity.GroupEnumId!=null)
             toJson.Group = entity.Group.ToString().AddSpacesToSentence();
			 toJson.HomeworkTypeEnumId = entity.HomeworkTypeEnumId ; 
             if(entity.HomeworkTypeEnumId!=null)
             toJson.HomeworkType = entity.HomeworkType.ToString().AddSpacesToSentence();
			 toJson.HomeworkKey = entity.HomeworkKey ; 
			 toJson.Description = entity.Description ; 
			 toJson.TeacherId = entity.TeacherId ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.ProgramId = entity.ProgramId ; 
			 toJson.CourseId = entity.CourseId.ToString() ; 
			 toJson.Mark = entity.Mark ; 
			 toJson.TeacherHomeworkTypeEnumId = entity.TeacherHomeworkTypeEnumId ; 
             if(entity.TeacherHomeworkTypeEnumId!=null)
             toJson.TeacherHomeworkType = entity.TeacherHomeworkType.ToString().AddSpacesToSentence();

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_HomeWorkJson json, Aca_HomeWork toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Title = json.Title?.Trim() ?? json.Title;
    			 toEntity.DueTime=json.DueTime;
    			 toEntity.CloseTime=json.CloseTime;
    			 toEntity.GroupEnumId=json.GroupEnumId;
    			 toEntity.HomeworkTypeEnumId=json.HomeworkTypeEnumId;
                 toEntity.HomeworkKey = json.HomeworkKey?.Trim() ?? json.HomeworkKey;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
    			 toEntity.TeacherId=json.TeacherId;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.ProgramId=json.ProgramId;
                 toEntity.CourseId = long.TryParse(json.CourseId, out output) ? output : 0;
    			 toEntity.Mark=json.Mark;
    			 toEntity.TeacherHomeworkTypeEnumId=json.TeacherHomeworkTypeEnumId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_HomeWork> entityList, IList<Aca_HomeWorkJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_HomeWorkJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_HomeWorkJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_HomeWorkJson> jsonList, ICollection<Aca_HomeWork> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_HomeWork obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_HomeWork.GetNew();
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
