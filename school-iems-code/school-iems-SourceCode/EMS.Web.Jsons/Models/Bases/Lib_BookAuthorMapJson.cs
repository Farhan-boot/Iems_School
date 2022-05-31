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
	public partial class Lib_BookAuthorMapJson 
	{
          public String Id { get; set; }
          public String BookId { get; set; }
          public String AuthorId { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public String Remarks { get; set; }
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
	   
		public static void Map(this Lib_BookAuthorMap entity, Lib_BookAuthorMapJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.BookId = entity.BookId.ToString() ; 
			 toJson.AuthorId = entity.AuthorId.ToString() ; 
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.Remarks = entity.Remarks ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Lib_BookAuthorMapJson json, Lib_BookAuthorMap toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.BookId = long.TryParse(json.BookId, out output) ? output : 0;
                 toEntity.AuthorId = long.TryParse(json.AuthorId, out output) ? output : 0;
    			 toEntity.StatusEnumId=json.StatusEnumId;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Lib_BookAuthorMap> entityList, IList<Lib_BookAuthorMapJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Lib_BookAuthorMapJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Lib_BookAuthorMapJson> jsonList, ICollection<Lib_BookAuthorMap> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Lib_BookAuthorMap obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Lib_BookAuthorMap.GetNew();
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
