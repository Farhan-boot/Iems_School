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
	public partial class Aca_TPEQuestionAnswerJson 
	{
          public Int32 Id { get; set; }
          public Int32 QuestionId { get; set; }
          public String AnswerTitle { get; set; }
          public Single Rating { get; set; }
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
	   
		public static void Map(this Aca_TPEQuestionAnswer entity, Aca_TPEQuestionAnswerJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.QuestionId = entity.QuestionId ; 
			 toJson.AnswerTitle = entity.AnswerTitle ; 
			 toJson.Rating = entity.Rating ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_TPEQuestionAnswerJson json, Aca_TPEQuestionAnswer toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.QuestionId=json.QuestionId;
                 toEntity.AnswerTitle = json.AnswerTitle?.Trim() ?? json.AnswerTitle;
    			 toEntity.Rating=json.Rating;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_TPEQuestionAnswer> entityList, IList<Aca_TPEQuestionAnswerJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_TPEQuestionAnswerJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_TPEQuestionAnswerJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_TPEQuestionAnswerJson> jsonList, ICollection<Aca_TPEQuestionAnswer> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_TPEQuestionAnswer obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_TPEQuestionAnswer.GetNew();
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
