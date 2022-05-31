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
	public partial class Aca_QuestionBankJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String QuestionImagePath { get; set; }
          public Byte QuestionTypeEnumId { get; set; }
          public string QuestionType { get; set; }
          public Single Mark { get; set; }
          public Nullable<Int32> QuestionGroupId { get; set; }
          public Nullable<Int32> ProgramTypeEnumId { get; set; }
          public string ProgramType { get; set; }
          public String CurriculumCourseId { get; set; }
          public Nullable<Byte> LanguageMediumEnumId { get; set; }
          public string LanguageMedium { get; set; }
          public Boolean IsShareable { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public Nullable<DateTime> LastChangedDate { get; set; }
          public Nullable<Int32> LastChangedById { get; set; }
          public Boolean IsActive { get; set; }
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
	   
		public static void Map(this Aca_QuestionBank entity, Aca_QuestionBankJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.QuestionImagePath = entity.QuestionImagePath ; 
			 toJson.QuestionTypeEnumId = entity.QuestionTypeEnumId ; 
             if(entity.QuestionTypeEnumId!=null)
             toJson.QuestionType = entity.QuestionType.ToString().AddSpacesToSentence();
			 toJson.Mark = entity.Mark ; 
			 toJson.QuestionGroupId = entity.QuestionGroupId ; 
			 toJson.ProgramTypeEnumId = entity.ProgramTypeEnumId ; 
             if(entity.ProgramTypeEnumId!=null)
             toJson.ProgramType = entity.ProgramType.ToString().AddSpacesToSentence();
             if(entity.CurriculumCourseId!=null)
			 toJson.CurriculumCourseId = entity.CurriculumCourseId.ToString() ; 
			 toJson.LanguageMediumEnumId = entity.LanguageMediumEnumId ; 
             if(entity.LanguageMediumEnumId!=null)
             toJson.LanguageMedium = entity.LanguageMedium.ToString().AddSpacesToSentence();
			 toJson.IsShareable = entity.IsShareable ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChangedDate = entity.LastChangedDate ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsActive = entity.IsActive ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_QuestionBankJson json, Aca_QuestionBank toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.QuestionImagePath = json.QuestionImagePath?.Trim() ?? json.QuestionImagePath;
    			 toEntity.QuestionTypeEnumId=json.QuestionTypeEnumId;
    			 toEntity.Mark=json.Mark;
    			 toEntity.QuestionGroupId=json.QuestionGroupId;
    			 toEntity.ProgramTypeEnumId=json.ProgramTypeEnumId;
                 toEntity.CurriculumCourseId = long.TryParse(json.CurriculumCourseId, out output) ? output : (Int64?)null;
    			 toEntity.LanguageMediumEnumId=json.LanguageMediumEnumId;
    			 toEntity.IsShareable=json.IsShareable;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChangedDate=json.LastChangedDate;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsActive=json.IsActive;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_QuestionBank> entityList, IList<Aca_QuestionBankJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_QuestionBankJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_QuestionBankJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_QuestionBankJson> jsonList, ICollection<Aca_QuestionBank> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_QuestionBank obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_QuestionBank.GetNew();
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
