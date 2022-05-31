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
	public partial class Lib_BookSubjectMapJson 
	{
          public String Id { get; set; }
          public String BookId { get; set; }
          public String SubjectId { get; set; }
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
	   
		public static void Map(this Lib_BookSubjectMap entity, Lib_BookSubjectMapJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.BookId = entity.BookId.ToString() ; 
			 toJson.SubjectId = entity.SubjectId.ToString() ; 
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.Remarks = entity.Remarks ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Lib_BookSubjectMapJson json, Lib_BookSubjectMap toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.BookId = long.TryParse(json.BookId, out output) ? output : 0;
                 toEntity.SubjectId = long.TryParse(json.SubjectId, out output) ? output : 0;
    			 toEntity.StatusEnumId=json.StatusEnumId;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Lib_BookSubjectMap> entityList, IList<Lib_BookSubjectMapJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Lib_BookSubjectMapJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Lib_BookSubjectMapJson> jsonList, ICollection<Lib_BookSubjectMap> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Lib_BookSubjectMap obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Lib_BookSubjectMap.GetNew();
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
