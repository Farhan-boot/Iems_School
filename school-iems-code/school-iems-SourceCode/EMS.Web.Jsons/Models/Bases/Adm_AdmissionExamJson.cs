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
	public partial class Adm_AdmissionExamJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String ShortName { get; set; }
          public String SessionName { get; set; }
          public String StudentIdPrefix { get; set; }
          public String StudentIdSuffix { get; set; }
          public Byte ProgramTypeEnumId { get; set; }
          public string ProgramType { get; set; }
          public Byte CircularStatusEnumId { get; set; }
          public string CircularStatus { get; set; }
          public String SemesterId { get; set; }
          public DateTime OnlineFormFillupStartDate { get; set; }
          public DateTime OnlineFormFillupEndDate { get; set; }
          public DateTime OnlineAdmitCardPublishDate { get; set; }
          public DateTime OnlineAdmitCardLockDate { get; set; }
          public DateTime ExamDate { get; set; }
          public DateTime ExamResultPublishDate { get; set; }
          public DateTime AdmissionStartDate { get; set; }
          public DateTime AmissionFormsDownloadStartDate { get; set; }
          public DateTime AmissionFormsDownloadEndDate { get; set; }
          public String CircularNoticeHtml { get; set; }
          public String Remark { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public String DefaultSettingsJson { get; set; }
          public Boolean IsEnable { get; set; }
          public String UgcIdPrefix { get; set; }
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
	   
		public static void Map(this Adm_AdmissionExam entity, Adm_AdmissionExamJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.SessionName = entity.SessionName ; 
			 toJson.StudentIdPrefix = entity.StudentIdPrefix ; 
			 toJson.StudentIdSuffix = entity.StudentIdSuffix ; 
			 toJson.ProgramTypeEnumId = entity.ProgramTypeEnumId ; 
             if(entity.ProgramTypeEnumId!=null)
             toJson.ProgramType = entity.ProgramType.ToString().AddSpacesToSentence();
			 toJson.CircularStatusEnumId = entity.CircularStatusEnumId ; 
             if(entity.CircularStatusEnumId!=null)
             toJson.CircularStatus = entity.CircularStatus.ToString().AddSpacesToSentence();
			 toJson.SemesterId = entity.SemesterId.ToString() ; 
			 toJson.OnlineFormFillupStartDate = entity.OnlineFormFillupStartDate ; 
			 toJson.OnlineFormFillupEndDate = entity.OnlineFormFillupEndDate ; 
			 toJson.OnlineAdmitCardPublishDate = entity.OnlineAdmitCardPublishDate ; 
			 toJson.OnlineAdmitCardLockDate = entity.OnlineAdmitCardLockDate ; 
			 toJson.ExamDate = entity.ExamDate ; 
			 toJson.ExamResultPublishDate = entity.ExamResultPublishDate ; 
			 toJson.AdmissionStartDate = entity.AdmissionStartDate ; 
			 toJson.AmissionFormsDownloadStartDate = entity.AmissionFormsDownloadStartDate ; 
			 toJson.AmissionFormsDownloadEndDate = entity.AmissionFormsDownloadEndDate ; 
			 toJson.CircularNoticeHtml = entity.CircularNoticeHtml ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.DefaultSettingsJson = entity.DefaultSettingsJson ; 
			 toJson.IsEnable = entity.IsEnable ; 
			 toJson.UgcIdPrefix = entity.UgcIdPrefix ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Adm_AdmissionExamJson json, Adm_AdmissionExam toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
                 toEntity.SessionName = json.SessionName?.Trim() ?? json.SessionName;
                 toEntity.StudentIdPrefix = json.StudentIdPrefix?.Trim() ?? json.StudentIdPrefix;
                 toEntity.StudentIdSuffix = json.StudentIdSuffix?.Trim() ?? json.StudentIdSuffix;
    			 toEntity.ProgramTypeEnumId=json.ProgramTypeEnumId;
    			 toEntity.CircularStatusEnumId=json.CircularStatusEnumId;
                 toEntity.SemesterId = long.TryParse(json.SemesterId, out output) ? output : 0;
    			 toEntity.OnlineFormFillupStartDate=json.OnlineFormFillupStartDate;
    			 toEntity.OnlineFormFillupEndDate=json.OnlineFormFillupEndDate;
    			 toEntity.OnlineAdmitCardPublishDate=json.OnlineAdmitCardPublishDate;
    			 toEntity.OnlineAdmitCardLockDate=json.OnlineAdmitCardLockDate;
    			 toEntity.ExamDate=json.ExamDate;
    			 toEntity.ExamResultPublishDate=json.ExamResultPublishDate;
    			 toEntity.AdmissionStartDate=json.AdmissionStartDate;
    			 toEntity.AmissionFormsDownloadStartDate=json.AmissionFormsDownloadStartDate;
    			 toEntity.AmissionFormsDownloadEndDate=json.AmissionFormsDownloadEndDate;
                 toEntity.CircularNoticeHtml = json.CircularNoticeHtml?.Trim() ?? json.CircularNoticeHtml;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
                 toEntity.DefaultSettingsJson = json.DefaultSettingsJson?.Trim() ?? json.DefaultSettingsJson;
    			 toEntity.IsEnable=json.IsEnable;
                 toEntity.UgcIdPrefix = json.UgcIdPrefix?.Trim() ?? json.UgcIdPrefix;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Adm_AdmissionExam> entityList, IList<Adm_AdmissionExamJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Adm_AdmissionExamJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Adm_AdmissionExamJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Adm_AdmissionExamJson> jsonList, ICollection<Adm_AdmissionExam> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Adm_AdmissionExam obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Adm_AdmissionExam.GetNew();
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
