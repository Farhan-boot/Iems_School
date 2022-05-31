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
	public partial class User_ContactNumberJson 
	{
          public String Id { get; set; }
          public Int32 UserId { get; set; }
          public String ContactNumber { get; set; }
          public Byte ContactNumberTypeEnumId { get; set; }
          public string ContactNumberType { get; set; }
          public Int32 CountryId { get; set; }
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
	   
		public static void Map(this User_ContactNumber entity, User_ContactNumberJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.UserId = entity.UserId ; 
			 toJson.ContactNumber = entity.ContactNumber ; 
			 toJson.ContactNumberTypeEnumId = entity.ContactNumberTypeEnumId ; 
             if(entity.ContactNumberTypeEnumId!=null)
             toJson.ContactNumberType = entity.ContactNumberType.ToString().AddSpacesToSentence();
			 toJson.CountryId = entity.CountryId ; 
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
		
		public static void Map(this User_ContactNumberJson json, User_ContactNumber toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.UserId=json.UserId;
                 toEntity.ContactNumber = json.ContactNumber?.Trim() ?? json.ContactNumber;
    			 toEntity.ContactNumberTypeEnumId=json.ContactNumberTypeEnumId;
    			 toEntity.CountryId=json.CountryId;
    			 toEntity.IsValid=json.IsValid;
    			 toEntity.PrivacyEnumId=json.PrivacyEnumId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<User_ContactNumber> entityList, IList<User_ContactNumberJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new User_ContactNumberJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<User_ContactNumberJson> jsonList, ICollection<User_ContactNumber> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                User_ContactNumber obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = User_ContactNumber.GetNew();
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
