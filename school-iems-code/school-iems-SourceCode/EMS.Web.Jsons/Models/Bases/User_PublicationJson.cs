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
	public partial class User_PublicationJson 
	{
          public String Id { get; set; }
          public Int32 UserId { get; set; }
          public DateTime PublicationDate { get; set; }
          public String Title { get; set; }
          public String Volume { get; set; }
          public String Journal { get; set; }
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
	   
		public static void Map(this User_Publication entity, User_PublicationJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.UserId = entity.UserId ; 
			 toJson.PublicationDate = entity.PublicationDate ; 
			 toJson.Title = entity.Title ; 
			 toJson.Volume = entity.Volume ; 
			 toJson.Journal = entity.Journal ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this User_PublicationJson json, User_Publication toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.UserId=json.UserId;
    			 toEntity.PublicationDate=json.PublicationDate;
                 toEntity.Title = json.Title?.Trim() ?? json.Title;
                 toEntity.Volume = json.Volume?.Trim() ?? json.Volume;
                 toEntity.Journal = json.Journal?.Trim() ?? json.Journal;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<User_Publication> entityList, IList<User_PublicationJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new User_PublicationJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<User_PublicationJson> jsonList, ICollection<User_Publication> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                User_Publication obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = User_Publication.GetNew();
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
