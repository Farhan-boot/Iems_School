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
	public partial class Aca_TPEQuestionAnswer2Json 
	{
          public Int32 Id { get; set; }
          public Int32 QuestionId { get; set; }
          public String AnswerTitle { get; set; }
          public Single Rating { get; set; }
          public Int32 NewId { get; set; }
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
	   
		public static void Map(this Aca_TPEQuestionAnswer2 entity, Aca_TPEQuestionAnswer2Json toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.QuestionId = entity.QuestionId ; 
			 toJson.AnswerTitle = entity.AnswerTitle ; 
			 toJson.Rating = entity.Rating ; 
			 toJson.NewId = entity.NewId ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_TPEQuestionAnswer2Json json, Aca_TPEQuestionAnswer2 toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.QuestionId=json.QuestionId;
                 toEntity.AnswerTitle = json.AnswerTitle?.Trim() ?? json.AnswerTitle;
    			 toEntity.Rating=json.Rating;
    			 toEntity.NewId=json.NewId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_TPEQuestionAnswer2> entityList, IList<Aca_TPEQuestionAnswer2Json> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_TPEQuestionAnswer2Json obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_TPEQuestionAnswer2Json();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_TPEQuestionAnswer2Json> jsonList, ICollection<Aca_TPEQuestionAnswer2> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_TPEQuestionAnswer2 obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_TPEQuestionAnswer2.GetNew();
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
