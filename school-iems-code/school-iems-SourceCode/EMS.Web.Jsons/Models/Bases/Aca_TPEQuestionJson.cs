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
	public partial class Aca_TPEQuestionJson 
	{
          public Int32 Id { get; set; }
          public String LetterId { get; set; }
          public String QuestionTitle { get; set; }
          public Int32 CategoryId { get; set; }
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
	   
		public static void Map(this Aca_TPEQuestion entity, Aca_TPEQuestionJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.LetterId = entity.LetterId ; 
			 toJson.QuestionTitle = entity.QuestionTitle ; 
			 toJson.CategoryId = entity.CategoryId ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_TPEQuestionJson json, Aca_TPEQuestion toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.LetterId = json.LetterId?.Trim() ?? json.LetterId;
                 toEntity.QuestionTitle = json.QuestionTitle?.Trim() ?? json.QuestionTitle;
    			 toEntity.CategoryId=json.CategoryId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_TPEQuestion> entityList, IList<Aca_TPEQuestionJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_TPEQuestionJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_TPEQuestionJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_TPEQuestionJson> jsonList, ICollection<Aca_TPEQuestion> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_TPEQuestion obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_TPEQuestion.GetNew();
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
