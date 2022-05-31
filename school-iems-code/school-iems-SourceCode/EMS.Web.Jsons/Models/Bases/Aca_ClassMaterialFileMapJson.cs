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
	public partial class Aca_ClassMaterialFileMapJson 
	{
          public String Id { get; set; }
          public String ClassId { get; set; }
          public String FileId { get; set; }
          public Int32 UserId { get; set; }
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
	   
		public static void Map(this Aca_ClassMaterialFileMap entity, Aca_ClassMaterialFileMapJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.ClassId = entity.ClassId.ToString() ; 
			 toJson.FileId = entity.FileId.ToString() ; 
			 toJson.UserId = entity.UserId ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ClassMaterialFileMapJson json, Aca_ClassMaterialFileMap toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.ClassId = long.TryParse(json.ClassId, out output) ? output : 0;
                 toEntity.FileId = long.TryParse(json.FileId, out output) ? output : 0;
    			 toEntity.UserId=json.UserId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_ClassMaterialFileMap> entityList, IList<Aca_ClassMaterialFileMapJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_ClassMaterialFileMapJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ClassMaterialFileMapJson> jsonList, ICollection<Aca_ClassMaterialFileMap> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_ClassMaterialFileMap obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_ClassMaterialFileMap.GetNew();
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
