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
	public partial class User_ContactAddressJson 
	{
          public Int32 Id { get; set; }
          public Int32 UserId { get; set; }
          public String Address1 { get; set; }
          public String Address2 { get; set; }
          public String PostOffice { get; set; }
          public String PoliceStation { get; set; }
          public String District { get; set; }
          public Nullable<Int32> CountryId { get; set; }
          public Boolean IsValid { get; set; }
          public Byte ContactAddressTypeEnumId { get; set; }
          public string ContactAddressType { get; set; }
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
	   
		public static void Map(this User_ContactAddress entity, User_ContactAddressJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.UserId = entity.UserId ; 
			 toJson.Address1 = entity.Address1 ; 
			 toJson.Address2 = entity.Address2 ; 
			 toJson.PostOffice = entity.PostOffice ; 
			 toJson.PoliceStation = entity.PoliceStation ; 
			 toJson.District = entity.District ; 
			 toJson.CountryId = entity.CountryId ; 
			 toJson.IsValid = entity.IsValid ; 
			 toJson.ContactAddressTypeEnumId = entity.ContactAddressTypeEnumId ; 
             if(entity.ContactAddressTypeEnumId!=null)
             toJson.ContactAddressType = entity.ContactAddressType.ToString().AddSpacesToSentence();
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this User_ContactAddressJson json, User_ContactAddress toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.UserId=json.UserId;
                 toEntity.Address1 = json.Address1?.Trim() ?? json.Address1;
                 toEntity.Address2 = json.Address2?.Trim() ?? json.Address2;
                 toEntity.PostOffice = json.PostOffice?.Trim() ?? json.PostOffice;
                 toEntity.PoliceStation = json.PoliceStation?.Trim() ?? json.PoliceStation;
                 toEntity.District = json.District?.Trim() ?? json.District;
    			 toEntity.CountryId=json.CountryId;
    			 toEntity.IsValid=json.IsValid;
    			 toEntity.ContactAddressTypeEnumId=json.ContactAddressTypeEnumId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<User_ContactAddress> entityList, IList<User_ContactAddressJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 User_ContactAddressJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new User_ContactAddressJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<User_ContactAddressJson> jsonList, ICollection<User_ContactAddress> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    User_ContactAddress obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = User_ContactAddress.GetNew();
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
