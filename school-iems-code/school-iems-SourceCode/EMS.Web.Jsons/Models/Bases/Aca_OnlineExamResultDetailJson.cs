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
	public partial class Aca_OnlineExamResultDetailJson 
	{
          public Int32 Id { get; set; }
          public Int32 StudentId { get; set; }
          public Int32 OnlineExamId { get; set; }
          public Nullable<Int32> SubmittedQuestionOptionId { get; set; }
          public String SubmittedAnswerText { get; set; }
          public String SubmittedAnswerFilePath { get; set; }
          public Boolean IsCorrect { get; set; }
          public Single ObtainedMark { get; set; }
          public String History { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public Int32 QuestionBankId { get; set; }
          public String SubmittedQuestionOptionsJson { get; set; }
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
	   
		public static void Map(this Aca_OnlineExamResultDetail entity, Aca_OnlineExamResultDetailJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.StudentId = entity.StudentId ; 
			 toJson.OnlineExamId = entity.OnlineExamId ; 
			 toJson.SubmittedQuestionOptionId = entity.SubmittedQuestionOptionId ; 
			 toJson.SubmittedAnswerText = entity.SubmittedAnswerText ; 
			 toJson.SubmittedAnswerFilePath = entity.SubmittedAnswerFilePath ; 
			 toJson.IsCorrect = entity.IsCorrect ; 
			 toJson.ObtainedMark = entity.ObtainedMark ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.QuestionBankId = entity.QuestionBankId ; 
			 toJson.SubmittedQuestionOptionsJson = entity.SubmittedQuestionOptionsJson ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_OnlineExamResultDetailJson json, Aca_OnlineExamResultDetail toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.StudentId=json.StudentId;
    			 toEntity.OnlineExamId=json.OnlineExamId;
    			 toEntity.SubmittedQuestionOptionId=json.SubmittedQuestionOptionId;
                 toEntity.SubmittedAnswerText = json.SubmittedAnswerText?.Trim() ?? json.SubmittedAnswerText;
                 toEntity.SubmittedAnswerFilePath = json.SubmittedAnswerFilePath?.Trim() ?? json.SubmittedAnswerFilePath;
    			 toEntity.IsCorrect=json.IsCorrect;
    			 toEntity.ObtainedMark=json.ObtainedMark;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.QuestionBankId=json.QuestionBankId;
                 toEntity.SubmittedQuestionOptionsJson = json.SubmittedQuestionOptionsJson?.Trim() ?? json.SubmittedQuestionOptionsJson;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_OnlineExamResultDetail> entityList, IList<Aca_OnlineExamResultDetailJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_OnlineExamResultDetailJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_OnlineExamResultDetailJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_OnlineExamResultDetailJson> jsonList, ICollection<Aca_OnlineExamResultDetail> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_OnlineExamResultDetail obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_OnlineExamResultDetail.GetNew();
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
