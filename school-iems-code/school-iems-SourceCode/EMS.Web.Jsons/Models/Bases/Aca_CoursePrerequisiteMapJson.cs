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
	public partial class Aca_CoursePrerequisiteMapJson 
	{
          public Int32 Id { get; set; }
          public String CourseId { get; set; }
          public String PrerequisiteCourseId { get; set; }
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
	   
		public static void Map(this Aca_CoursePrerequisiteMap entity, Aca_CoursePrerequisiteMapJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.CourseId = entity.CourseId.ToString() ; 
			 toJson.PrerequisiteCourseId = entity.PrerequisiteCourseId.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_CoursePrerequisiteMapJson json, Aca_CoursePrerequisiteMap toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.CourseId = long.TryParse(json.CourseId, out output) ? output : 0;
                 toEntity.PrerequisiteCourseId = long.TryParse(json.PrerequisiteCourseId, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_CoursePrerequisiteMap> entityList, IList<Aca_CoursePrerequisiteMapJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_CoursePrerequisiteMapJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_CoursePrerequisiteMapJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_CoursePrerequisiteMapJson> jsonList, ICollection<Aca_CoursePrerequisiteMap> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_CoursePrerequisiteMap obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_CoursePrerequisiteMap.GetNew();
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
