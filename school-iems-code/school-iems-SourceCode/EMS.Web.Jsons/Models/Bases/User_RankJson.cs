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
	public partial class User_RankJson 
	{
          public String Id { get; set; }
          public String Name { get; set; }
          public String LocalName { get; set; }
          public String ShortName { get; set; }
          public Int32 Priority { get; set; }
          public Byte CategoryEnumId { get; set; }
          public string Category { get; set; }
          public Byte JobClassEnumId { get; set; }
          public string JobClass { get; set; }
          public Byte AcademicLevelEnumId { get; set; }
          public string AcademicLevel { get; set; }
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
	   
		public static void Map(this User_Rank entity, User_RankJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Name = entity.Name ; 
			 toJson.LocalName = entity.LocalName ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.Priority = entity.Priority ; 
			 toJson.CategoryEnumId = entity.CategoryEnumId ; 
             if(entity.CategoryEnumId!=null)
             toJson.Category = entity.Category.ToString().AddSpacesToSentence();
			 toJson.JobClassEnumId = entity.JobClassEnumId ; 
             if(entity.JobClassEnumId!=null)
             toJson.JobClass = entity.JobClass.ToString().AddSpacesToSentence();
			 toJson.AcademicLevelEnumId = entity.AcademicLevelEnumId ; 
             if(entity.AcademicLevelEnumId!=null)
             toJson.AcademicLevel = entity.AcademicLevel.ToString().AddSpacesToSentence();
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this User_RankJson json, User_Rank toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.LocalName = json.LocalName?.Trim() ?? json.LocalName;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
    			 toEntity.Priority=json.Priority;
    			 toEntity.CategoryEnumId=json.CategoryEnumId;
    			 toEntity.JobClassEnumId=json.JobClassEnumId;
    			 toEntity.AcademicLevelEnumId=json.AcademicLevelEnumId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<User_Rank> entityList, IList<User_RankJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new User_RankJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<User_RankJson> jsonList, ICollection<User_Rank> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                User_Rank obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = User_Rank.GetNew();
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
