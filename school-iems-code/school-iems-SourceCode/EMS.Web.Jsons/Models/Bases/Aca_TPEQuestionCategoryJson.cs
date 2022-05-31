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
	public partial class Aca_TPEQuestionCategoryJson 
	{
          public Int32 Id { get; set; }
          public String LetterId { get; set; }
          public String CateGoryTitle { get; set; }
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
	   
		public static void Map(this Aca_TPEQuestionCategory entity, Aca_TPEQuestionCategoryJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.LetterId = entity.LetterId ; 
			 toJson.CateGoryTitle = entity.CateGoryTitle ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_TPEQuestionCategoryJson json, Aca_TPEQuestionCategory toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.LetterId = json.LetterId?.Trim() ?? json.LetterId;
                 toEntity.CateGoryTitle = json.CateGoryTitle?.Trim() ?? json.CateGoryTitle;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_TPEQuestionCategory> entityList, IList<Aca_TPEQuestionCategoryJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_TPEQuestionCategoryJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_TPEQuestionCategoryJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_TPEQuestionCategoryJson> jsonList, ICollection<Aca_TPEQuestionCategory> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_TPEQuestionCategory obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_TPEQuestionCategory.GetNew();
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
