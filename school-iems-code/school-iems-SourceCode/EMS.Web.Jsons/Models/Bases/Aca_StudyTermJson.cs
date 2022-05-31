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
	public partial class Aca_StudyTermJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public Byte TermNo { get; set; }
          public String SemesterName { get; set; }
          public Boolean IsEnabled { get; set; }
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
	   
		public static void Map(this Aca_StudyTerm entity, Aca_StudyTermJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.TermNo = entity.TermNo ; 
			 toJson.SemesterName = entity.SemesterName ; 
			 toJson.IsEnabled = entity.IsEnabled ; 
			 toJson.SemesterDurationEnumId = entity.SemesterDurationEnumId ; 
             if(entity.SemesterDurationEnumId!=null)
             toJson.SemesterDuration = entity.SemesterDuration.ToString().AddSpacesToSentence();

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_StudyTermJson json, Aca_StudyTerm toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.TermNo=json.TermNo;
                 toEntity.SemesterName = json.SemesterName?.Trim() ?? json.SemesterName;
    			 toEntity.IsEnabled=json.IsEnabled;
    			 toEntity.SemesterDurationEnumId=json.SemesterDurationEnumId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_StudyTerm> entityList, IList<Aca_StudyTermJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_StudyTermJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_StudyTermJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_StudyTermJson> jsonList, ICollection<Aca_StudyTerm> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_StudyTerm obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_StudyTerm.GetNew();
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
