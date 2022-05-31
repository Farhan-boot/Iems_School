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
	public partial class User_ContactEmailJson 
	{
          public String Id { get; set; }
          public Int32 UserId { get; set; }
          public Byte ContactEmailTypeEnumId { get; set; }
          public string ContactEmailType { get; set; }
          public String ContactEmail { get; set; }
          public Boolean IsValid { get; set; }
          public Byte PrivacyEnumId { get; set; }
          public string Privacy { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
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
	   
		public static void Map(this User_ContactEmail entity, User_ContactEmailJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.UserId = entity.UserId ; 
			 toJson.ContactEmailTypeEnumId = entity.ContactEmailTypeEnumId ; 
             if(entity.ContactEmailTypeEnumId!=null)
             toJson.ContactEmailType = entity.ContactEmailType.ToString().AddSpacesToSentence();
			 toJson.ContactEmail = entity.ContactEmail ; 
			 toJson.IsValid = entity.IsValid ; 
			 toJson.PrivacyEnumId = entity.PrivacyEnumId ; 
             if(entity.PrivacyEnumId!=null)
             toJson.Privacy = entity.Privacy.ToString().AddSpacesToSentence();
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this User_ContactEmailJson json, User_ContactEmail toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.UserId=json.UserId;
    			 toEntity.ContactEmailTypeEnumId=json.ContactEmailTypeEnumId;
                 toEntity.ContactEmail = json.ContactEmail?.Trim() ?? json.ContactEmail;
    			 toEntity.IsValid=json.IsValid;
    			 toEntity.PrivacyEnumId=json.PrivacyEnumId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<User_ContactEmail> entityList, IList<User_ContactEmailJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new User_ContactEmailJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<User_ContactEmailJson> jsonList, ICollection<User_ContactEmail> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                User_ContactEmail obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = User_ContactEmail.GetNew();
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
