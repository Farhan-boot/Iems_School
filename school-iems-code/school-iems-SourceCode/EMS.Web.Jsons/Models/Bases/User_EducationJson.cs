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
	public partial class User_EducationJson 
	{
          public Int32 Id { get; set; }
          public Int32 UserId { get; set; }
          public Byte DegreeEquivalentEnumId { get; set; }
          public string DegreeEquivalent { get; set; }
          public Int32 DegreeCategoryId { get; set; }
          public String DegreeTitle { get; set; }
          public Byte DegreeGroupMajorEnumId { get; set; }
          public string DegreeGroupMajor { get; set; }
          public String DegreeGroupMajorOther { get; set; }
          public String InstitutionName { get; set; }
          public Nullable<Int32> BoardId { get; set; }
          public String CourseDuration { get; set; }
          public Nullable<Int32> YearOfPassing { get; set; }
          public String RegistrationNo { get; set; }
          public String StudentRollOrIdNo { get; set; }
          public Byte ResultSystemEnumId { get; set; }
          public string ResultSystem { get; set; }
          public Nullable<Single> ObtainedGpaOrMarks { get; set; }
          public String ObtainedGradeOrClass { get; set; }
          public Nullable<Single> GpaNo4Sub { get; set; }
          public Nullable<Single> FullMarksOrScale { get; set; }
          public Nullable<Boolean> IsGolden { get; set; }
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
	   
		public static void Map(this User_Education entity, User_EducationJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.UserId = entity.UserId ; 
			 toJson.DegreeEquivalentEnumId = entity.DegreeEquivalentEnumId ; 
             if(entity.DegreeEquivalentEnumId!=null)
             toJson.DegreeEquivalent = entity.DegreeEquivalent.ToString().AddSpacesToSentence();
			 toJson.DegreeCategoryId = entity.DegreeCategoryId ; 
			 toJson.DegreeTitle = entity.DegreeTitle ; 
			 toJson.DegreeGroupMajorEnumId = entity.DegreeGroupMajorEnumId ; 
             if(entity.DegreeGroupMajorEnumId!=null)
             toJson.DegreeGroupMajor = entity.DegreeGroupMajor.ToString().AddSpacesToSentence();
			 toJson.DegreeGroupMajorOther = entity.DegreeGroupMajorOther ; 
			 toJson.InstitutionName = entity.InstitutionName ; 
			 toJson.BoardId = entity.BoardId ; 
			 toJson.CourseDuration = entity.CourseDuration ; 
			 toJson.YearOfPassing = entity.YearOfPassing ; 
			 toJson.RegistrationNo = entity.RegistrationNo ; 
			 toJson.StudentRollOrIdNo = entity.StudentRollOrIdNo ; 
			 toJson.ResultSystemEnumId = entity.ResultSystemEnumId ; 
             if(entity.ResultSystemEnumId!=null)
             toJson.ResultSystem = entity.ResultSystem.ToString().AddSpacesToSentence();
			 toJson.ObtainedGpaOrMarks = entity.ObtainedGpaOrMarks ; 
			 toJson.ObtainedGradeOrClass = entity.ObtainedGradeOrClass ; 
			 toJson.GpaNo4Sub = entity.GpaNo4Sub ; 
			 toJson.FullMarksOrScale = entity.FullMarksOrScale ; 
			 toJson.IsGolden = entity.IsGolden ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this User_EducationJson json, User_Education toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.UserId=json.UserId;
    			 toEntity.DegreeEquivalentEnumId=json.DegreeEquivalentEnumId;
    			 toEntity.DegreeCategoryId=json.DegreeCategoryId;
                 toEntity.DegreeTitle = json.DegreeTitle?.Trim() ?? json.DegreeTitle;
    			 toEntity.DegreeGroupMajorEnumId=json.DegreeGroupMajorEnumId;
                 toEntity.DegreeGroupMajorOther = json.DegreeGroupMajorOther?.Trim() ?? json.DegreeGroupMajorOther;
                 toEntity.InstitutionName = json.InstitutionName?.Trim() ?? json.InstitutionName;
    			 toEntity.BoardId=json.BoardId;
                 toEntity.CourseDuration = json.CourseDuration?.Trim() ?? json.CourseDuration;
    			 toEntity.YearOfPassing=json.YearOfPassing;
                 toEntity.RegistrationNo = json.RegistrationNo?.Trim() ?? json.RegistrationNo;
                 toEntity.StudentRollOrIdNo = json.StudentRollOrIdNo?.Trim() ?? json.StudentRollOrIdNo;
    			 toEntity.ResultSystemEnumId=json.ResultSystemEnumId;
    			 toEntity.ObtainedGpaOrMarks=json.ObtainedGpaOrMarks;
                 toEntity.ObtainedGradeOrClass = json.ObtainedGradeOrClass?.Trim() ?? json.ObtainedGradeOrClass;
    			 toEntity.GpaNo4Sub=json.GpaNo4Sub;
    			 toEntity.FullMarksOrScale=json.FullMarksOrScale;
    			 toEntity.IsGolden=json.IsGolden;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<User_Education> entityList, IList<User_EducationJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 User_EducationJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new User_EducationJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<User_EducationJson> jsonList, ICollection<User_Education> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    User_Education obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = User_Education.GetNew();
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
