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
	public partial class Aca_StudyLevelTermJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String Name2 { get; set; }
          public String ShortName { get; set; }
          public Int32 LevelId { get; set; }
          public Int32 TermId { get; set; }
          public Int32 SemesterDurationEnumId { get; set; }
          public string SemesterDuration { get; set; }
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
	   
		public static void Map(this Aca_StudyLevelTerm entity, Aca_StudyLevelTermJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.Name2 = entity.Name2 ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.LevelId = entity.LevelId ; 
			 toJson.TermId = entity.TermId ; 
			 toJson.SemesterDurationEnumId = entity.SemesterDurationEnumId ; 
             if(entity.SemesterDurationEnumId!=null)
             toJson.SemesterDuration = entity.SemesterDuration.ToString().AddSpacesToSentence();

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_StudyLevelTermJson json, Aca_StudyLevelTerm toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.Name2 = json.Name2?.Trim() ?? json.Name2;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
    			 toEntity.LevelId=json.LevelId;
    			 toEntity.TermId=json.TermId;
    			 toEntity.SemesterDurationEnumId=json.SemesterDurationEnumId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_StudyLevelTerm> entityList, IList<Aca_StudyLevelTermJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_StudyLevelTermJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_StudyLevelTermJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_StudyLevelTermJson> jsonList, ICollection<Aca_StudyLevelTerm> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_StudyLevelTerm obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_StudyLevelTerm.GetNew();
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
