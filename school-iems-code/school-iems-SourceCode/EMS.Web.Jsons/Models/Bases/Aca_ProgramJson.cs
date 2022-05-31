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
	public partial class Aca_ProgramJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String ShortName { get; set; }
          public String ShortTitle { get; set; }
          public String OfficialCertificateTitle { get; set; }
          public String Description { get; set; }
          public String Code { get; set; }
          public Int32 ProgramTypeEnumId { get; set; }
          public string ProgramType { get; set; }
          public Byte ClassTimingGroupEnumId { get; set; }
          public string ClassTimingGroup { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public Byte LanguageMediumEnumId { get; set; }
          public string LanguageMedium { get; set; }
          public Int32 DefaultClassSectionRange { get; set; }
          public Boolean IsEnable { get; set; }
          public Nullable<Int32> ClassTeacherId { get; set; }
          public String CurriculumId { get; set; }
          public Nullable<Int32> SectionId { get; set; }
          public Nullable<Int32> RequiredProgramTypeEnumId { get; set; }
          public string RequiredProgramType { get; set; }
          public String SemesterId { get; set; }
          public Int32 DepartmentId { get; set; }
          public Single OrderNo { get; set; }
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
	   
		public static void Map(this Aca_Program entity, Aca_ProgramJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.ShortTitle = entity.ShortTitle ; 
			 toJson.OfficialCertificateTitle = entity.OfficialCertificateTitle ; 
			 toJson.Description = entity.Description ; 
			 toJson.Code = entity.Code ; 
			 toJson.ProgramTypeEnumId = entity.ProgramTypeEnumId ; 
             if(entity.ProgramTypeEnumId!=null)
             toJson.ProgramType = entity.ProgramType.ToString().AddSpacesToSentence();
			 toJson.ClassTimingGroupEnumId = entity.ClassTimingGroupEnumId ; 
             if(entity.ClassTimingGroupEnumId!=null)
             toJson.ClassTimingGroup = entity.ClassTimingGroup.ToString().AddSpacesToSentence();
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.LanguageMediumEnumId = entity.LanguageMediumEnumId ; 
             if(entity.LanguageMediumEnumId!=null)
             toJson.LanguageMedium = entity.LanguageMedium.ToString().AddSpacesToSentence();
			 toJson.DefaultClassSectionRange = entity.DefaultClassSectionRange ; 
			 toJson.IsEnable = entity.IsEnable ; 
			 toJson.ClassTeacherId = entity.ClassTeacherId ; 
             if(entity.CurriculumId!=null)
			 toJson.CurriculumId = entity.CurriculumId.ToString() ; 
			 toJson.SectionId = entity.SectionId ; 
			 toJson.RequiredProgramTypeEnumId = entity.RequiredProgramTypeEnumId ; 
             if(entity.RequiredProgramTypeEnumId!=null)
             toJson.RequiredProgramType = entity.RequiredProgramType.ToString().AddSpacesToSentence();
			 toJson.SemesterId = entity.SemesterId.ToString() ; 
			 toJson.DepartmentId = entity.DepartmentId ; 
			 toJson.OrderNo = entity.OrderNo ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ProgramJson json, Aca_Program toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
                 toEntity.ShortTitle = json.ShortTitle?.Trim() ?? json.ShortTitle;
                 toEntity.OfficialCertificateTitle = json.OfficialCertificateTitle?.Trim() ?? json.OfficialCertificateTitle;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
                 toEntity.Code = json.Code?.Trim() ?? json.Code;
    			 toEntity.ProgramTypeEnumId=json.ProgramTypeEnumId;
    			 toEntity.ClassTimingGroupEnumId=json.ClassTimingGroupEnumId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.LanguageMediumEnumId=json.LanguageMediumEnumId;
    			 toEntity.DefaultClassSectionRange=json.DefaultClassSectionRange;
    			 toEntity.IsEnable=json.IsEnable;
    			 toEntity.ClassTeacherId=json.ClassTeacherId;
                 toEntity.CurriculumId = long.TryParse(json.CurriculumId, out output) ? output : (Int64?)null;
    			 toEntity.SectionId=json.SectionId;
    			 toEntity.RequiredProgramTypeEnumId=json.RequiredProgramTypeEnumId;
                 toEntity.SemesterId = long.TryParse(json.SemesterId, out output) ? output : 0;
    			 toEntity.DepartmentId=json.DepartmentId;
    			 toEntity.OrderNo=json.OrderNo;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_Program> entityList, IList<Aca_ProgramJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_ProgramJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_ProgramJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ProgramJson> jsonList, ICollection<Aca_Program> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_Program obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_Program.GetNew();
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
