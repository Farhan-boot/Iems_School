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
	public partial class Aca_ExamRollJson 
	{
          public String Id { get; set; }
          public Int32 StudentId { get; set; }
          public String ExamId { get; set; }
          public Int32 ExamRoll { get; set; }
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
	   
		public static void Map(this Aca_ExamRoll entity, Aca_ExamRollJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.StudentId = entity.StudentId ; 
			 toJson.ExamId = entity.ExamId.ToString() ; 
			 toJson.ExamRoll = entity.ExamRoll ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ExamRollJson json, Aca_ExamRoll toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.StudentId=json.StudentId;
                 toEntity.ExamId = long.TryParse(json.ExamId, out output) ? output : 0;
    			 toEntity.ExamRoll=json.ExamRoll;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_ExamRoll> entityList, IList<Aca_ExamRollJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_ExamRollJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ExamRollJson> jsonList, ICollection<Aca_ExamRoll> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_ExamRoll obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_ExamRoll.GetNew();
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
