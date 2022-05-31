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
	public partial class Aca_HomeWorkSubmissionJson 
	{
          public Int32 Id { get; set; }
          public Int32 HomeworkId { get; set; }
          public Int32 StudentId { get; set; }
          public DateTime SubmissionDate { get; set; }
          public Int32 SubmissionStatusEnumId { get; set; }
          public string SubmissionStatus { get; set; }
          public Boolean Checked { get; set; }
          public Int32 TeacherId { get; set; }
          public String Feedback { get; set; }
          public String Remark { get; set; }
          public String History { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public String SubmittedText { get; set; }
          public Single ObtainedMark { get; set; }
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
	   
		public static void Map(this Aca_HomeWorkSubmission entity, Aca_HomeWorkSubmissionJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.HomeworkId = entity.HomeworkId ; 
			 toJson.StudentId = entity.StudentId ; 
			 toJson.SubmissionDate = entity.SubmissionDate ; 
			 toJson.SubmissionStatusEnumId = entity.SubmissionStatusEnumId ; 
             if(entity.SubmissionStatusEnumId!=null)
             toJson.SubmissionStatus = entity.SubmissionStatus.ToString().AddSpacesToSentence();
			 toJson.Checked = entity.Checked ; 
			 toJson.TeacherId = entity.TeacherId ; 
			 toJson.Feedback = entity.Feedback ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.SubmittedText = entity.SubmittedText ; 
			 toJson.ObtainedMark = entity.ObtainedMark ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_HomeWorkSubmissionJson json, Aca_HomeWorkSubmission toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.HomeworkId=json.HomeworkId;
    			 toEntity.StudentId=json.StudentId;
    			 toEntity.SubmissionDate=json.SubmissionDate;
    			 toEntity.SubmissionStatusEnumId=json.SubmissionStatusEnumId;
    			 toEntity.Checked=json.Checked;
    			 toEntity.TeacherId=json.TeacherId;
                 toEntity.Feedback = json.Feedback?.Trim() ?? json.Feedback;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;
                 toEntity.SubmittedText = json.SubmittedText?.Trim() ?? json.SubmittedText;
    			 toEntity.ObtainedMark=json.ObtainedMark;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_HomeWorkSubmission> entityList, IList<Aca_HomeWorkSubmissionJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_HomeWorkSubmissionJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_HomeWorkSubmissionJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_HomeWorkSubmissionJson> jsonList, ICollection<Aca_HomeWorkSubmission> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_HomeWorkSubmission obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_HomeWorkSubmission.GetNew();
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
