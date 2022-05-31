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
	public partial class Aca_OnlineExamJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public Int32 ProgramId { get; set; }
          public DateTime StartTime { get; set; }
          public DateTime EndTime { get; set; }
          public Boolean HasNegativeMarking { get; set; }
          public Nullable<Single> NegativeMarkPercentage { get; set; }
          public Boolean IsOnlineExamActive { get; set; }
          public String Remark { get; set; }
          public String History { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public Nullable<DateTime> LastChanged { get; set; }
          public Nullable<Int32> LastChangedById { get; set; }
          public Boolean IsActive { get; set; }
          public Boolean IsResultPublished { get; set; }
          public Single TotalMarks { get; set; }
          public String CurriculumCourseId { get; set; }
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
	   
		public static void Map(this Aca_OnlineExam entity, Aca_OnlineExamJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.ProgramId = entity.ProgramId ; 
			 toJson.StartTime = entity.StartTime ; 
			 toJson.EndTime = entity.EndTime ; 
			 toJson.HasNegativeMarking = entity.HasNegativeMarking ; 
			 toJson.NegativeMarkPercentage = entity.NegativeMarkPercentage ; 
			 toJson.IsOnlineExamActive = entity.IsOnlineExamActive ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsActive = entity.IsActive ; 
			 toJson.IsResultPublished = entity.IsResultPublished ; 
			 toJson.TotalMarks = entity.TotalMarks ; 
			 toJson.CurriculumCourseId = entity.CurriculumCourseId.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_OnlineExamJson json, Aca_OnlineExam toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.ProgramId=json.ProgramId;
    			 toEntity.StartTime=json.StartTime;
    			 toEntity.EndTime=json.EndTime;
    			 toEntity.HasNegativeMarking=json.HasNegativeMarking;
    			 toEntity.NegativeMarkPercentage=json.NegativeMarkPercentage;
    			 toEntity.IsOnlineExamActive=json.IsOnlineExamActive;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsActive=json.IsActive;
    			 toEntity.IsResultPublished=json.IsResultPublished;
    			 toEntity.TotalMarks=json.TotalMarks;
                 toEntity.CurriculumCourseId = long.TryParse(json.CurriculumCourseId, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_OnlineExam> entityList, IList<Aca_OnlineExamJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_OnlineExamJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_OnlineExamJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_OnlineExamJson> jsonList, ICollection<Aca_OnlineExam> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_OnlineExam obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_OnlineExam.GetNew();
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
