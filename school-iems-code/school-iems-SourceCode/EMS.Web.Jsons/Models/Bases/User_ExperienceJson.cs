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
	public partial class User_ExperienceJson 
	{
          public String Id { get; set; }
          public Int32 UserId { get; set; }
          public String CompanyName { get; set; }
          public String CompanyBusiness { get; set; }
          public String CompanyLocation { get; set; }
          public String Position { get; set; }
          public String Department { get; set; }
          public String Responsibilities { get; set; }
          public DateTime JoinDate { get; set; }
          public DateTime EndDate { get; set; }
          public Boolean IsActive { get; set; }
          public Boolean IsActing { get; set; }
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
	   
		public static void Map(this User_Experience entity, User_ExperienceJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.UserId = entity.UserId ; 
			 toJson.CompanyName = entity.CompanyName ; 
			 toJson.CompanyBusiness = entity.CompanyBusiness ; 
			 toJson.CompanyLocation = entity.CompanyLocation ; 
			 toJson.Position = entity.Position ; 
			 toJson.Department = entity.Department ; 
			 toJson.Responsibilities = entity.Responsibilities ; 
			 toJson.JoinDate = entity.JoinDate ; 
			 toJson.EndDate = entity.EndDate ; 
			 toJson.IsActive = entity.IsActive ; 
			 toJson.IsActing = entity.IsActing ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this User_ExperienceJson json, User_Experience toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.UserId=json.UserId;
                 toEntity.CompanyName = json.CompanyName?.Trim() ?? json.CompanyName;
                 toEntity.CompanyBusiness = json.CompanyBusiness?.Trim() ?? json.CompanyBusiness;
                 toEntity.CompanyLocation = json.CompanyLocation?.Trim() ?? json.CompanyLocation;
                 toEntity.Position = json.Position?.Trim() ?? json.Position;
                 toEntity.Department = json.Department?.Trim() ?? json.Department;
                 toEntity.Responsibilities = json.Responsibilities?.Trim() ?? json.Responsibilities;
    			 toEntity.JoinDate=json.JoinDate;
    			 toEntity.EndDate=json.EndDate;
    			 toEntity.IsActive=json.IsActive;
    			 toEntity.IsActing=json.IsActing;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<User_Experience> entityList, IList<User_ExperienceJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new User_ExperienceJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<User_ExperienceJson> jsonList, ICollection<User_Experience> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                User_Experience obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = User_Experience.GetNew();
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
