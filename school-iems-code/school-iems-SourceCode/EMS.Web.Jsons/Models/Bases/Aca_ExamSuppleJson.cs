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
	public partial class Aca_ExamSuppleJson 
	{
          public Int32 Id { get; set; }
          public String ExamId { get; set; }
          public Boolean IsOpenTheorySuppleApplyForStudent { get; set; }
          public Boolean IsOpenNonTheorySuppleApplyForStudent { get; set; }
          public DateTime SuppleApplicationLastDate { get; set; }
          public Boolean IsOpenTheorySuppleApplyForAdmin { get; set; }
          public Boolean IsOpenNonTheorySuppleApplyForAdmin { get; set; }
          public Single SuppleApplyAllowedPaymentDueUpto { get; set; }
          public Int32 MaxTimeCanApplyForOneTheory { get; set; }
          public Int32 MaxTimeCanApplyForOneNonTheory { get; set; }
          public Single ReferredSuppleExamFee { get; set; }
          public Single BackLogSuppleExamFee { get; set; }
          public Boolean IsPublishedSuppleAdmitCard { get; set; }
          public Single SuppleAdmitCardDownloadPaymentDueUpto { get; set; }
          public Boolean IsOpenTheorySuppleMarkInput { get; set; }
          public Boolean IsOpenNonTheorySuppleMarkInput { get; set; }
          public Single AutoGradeGraceMarkForPass { get; set; }
          public Boolean IsPublishTheorySuppleResult { get; set; }
          public Boolean IsPublishNonTheorySuppleResult { get; set; }
          public Single SuppleResultAllowedPaymentDueUpto { get; set; }
          public String StudentsInstructionsForApply { get; set; }
          public String FacultyInstructionsForMarkEntry { get; set; }
          public String StudentApplyConfirmationMessage { get; set; }
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
	   
		public static void Map(this Aca_ExamSupple entity, Aca_ExamSuppleJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.ExamId = entity.ExamId.ToString() ; 
			 toJson.IsOpenTheorySuppleApplyForStudent = entity.IsOpenTheorySuppleApplyForStudent ; 
			 toJson.IsOpenNonTheorySuppleApplyForStudent = entity.IsOpenNonTheorySuppleApplyForStudent ; 
			 toJson.SuppleApplicationLastDate = entity.SuppleApplicationLastDate ; 
			 toJson.IsOpenTheorySuppleApplyForAdmin = entity.IsOpenTheorySuppleApplyForAdmin ; 
			 toJson.IsOpenNonTheorySuppleApplyForAdmin = entity.IsOpenNonTheorySuppleApplyForAdmin ; 
			 toJson.SuppleApplyAllowedPaymentDueUpto = entity.SuppleApplyAllowedPaymentDueUpto ; 
			 toJson.MaxTimeCanApplyForOneTheory = entity.MaxTimeCanApplyForOneTheory ; 
			 toJson.MaxTimeCanApplyForOneNonTheory = entity.MaxTimeCanApplyForOneNonTheory ; 
			 toJson.ReferredSuppleExamFee = entity.ReferredSuppleExamFee ; 
			 toJson.BackLogSuppleExamFee = entity.BackLogSuppleExamFee ; 
			 toJson.IsPublishedSuppleAdmitCard = entity.IsPublishedSuppleAdmitCard ; 
			 toJson.SuppleAdmitCardDownloadPaymentDueUpto = entity.SuppleAdmitCardDownloadPaymentDueUpto ; 
			 toJson.IsOpenTheorySuppleMarkInput = entity.IsOpenTheorySuppleMarkInput ; 
			 toJson.IsOpenNonTheorySuppleMarkInput = entity.IsOpenNonTheorySuppleMarkInput ; 
			 toJson.AutoGradeGraceMarkForPass = entity.AutoGradeGraceMarkForPass ; 
			 toJson.IsPublishTheorySuppleResult = entity.IsPublishTheorySuppleResult ; 
			 toJson.IsPublishNonTheorySuppleResult = entity.IsPublishNonTheorySuppleResult ; 
			 toJson.SuppleResultAllowedPaymentDueUpto = entity.SuppleResultAllowedPaymentDueUpto ; 
			 toJson.StudentsInstructionsForApply = entity.StudentsInstructionsForApply ; 
			 toJson.FacultyInstructionsForMarkEntry = entity.FacultyInstructionsForMarkEntry ; 
			 toJson.StudentApplyConfirmationMessage = entity.StudentApplyConfirmationMessage ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ExamSuppleJson json, Aca_ExamSupple toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.ExamId = long.TryParse(json.ExamId, out output) ? output : 0;
    			 toEntity.IsOpenTheorySuppleApplyForStudent=json.IsOpenTheorySuppleApplyForStudent;
    			 toEntity.IsOpenNonTheorySuppleApplyForStudent=json.IsOpenNonTheorySuppleApplyForStudent;
    			 toEntity.SuppleApplicationLastDate=json.SuppleApplicationLastDate;
    			 toEntity.IsOpenTheorySuppleApplyForAdmin=json.IsOpenTheorySuppleApplyForAdmin;
    			 toEntity.IsOpenNonTheorySuppleApplyForAdmin=json.IsOpenNonTheorySuppleApplyForAdmin;
    			 toEntity.SuppleApplyAllowedPaymentDueUpto=json.SuppleApplyAllowedPaymentDueUpto;
    			 toEntity.MaxTimeCanApplyForOneTheory=json.MaxTimeCanApplyForOneTheory;
    			 toEntity.MaxTimeCanApplyForOneNonTheory=json.MaxTimeCanApplyForOneNonTheory;
    			 toEntity.ReferredSuppleExamFee=json.ReferredSuppleExamFee;
    			 toEntity.BackLogSuppleExamFee=json.BackLogSuppleExamFee;
    			 toEntity.IsPublishedSuppleAdmitCard=json.IsPublishedSuppleAdmitCard;
    			 toEntity.SuppleAdmitCardDownloadPaymentDueUpto=json.SuppleAdmitCardDownloadPaymentDueUpto;
    			 toEntity.IsOpenTheorySuppleMarkInput=json.IsOpenTheorySuppleMarkInput;
    			 toEntity.IsOpenNonTheorySuppleMarkInput=json.IsOpenNonTheorySuppleMarkInput;
    			 toEntity.AutoGradeGraceMarkForPass=json.AutoGradeGraceMarkForPass;
    			 toEntity.IsPublishTheorySuppleResult=json.IsPublishTheorySuppleResult;
    			 toEntity.IsPublishNonTheorySuppleResult=json.IsPublishNonTheorySuppleResult;
    			 toEntity.SuppleResultAllowedPaymentDueUpto=json.SuppleResultAllowedPaymentDueUpto;
                 toEntity.StudentsInstructionsForApply = json.StudentsInstructionsForApply?.Trim() ?? json.StudentsInstructionsForApply;
                 toEntity.FacultyInstructionsForMarkEntry = json.FacultyInstructionsForMarkEntry?.Trim() ?? json.FacultyInstructionsForMarkEntry;
                 toEntity.StudentApplyConfirmationMessage = json.StudentApplyConfirmationMessage?.Trim() ?? json.StudentApplyConfirmationMessage;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_ExamSupple> entityList, IList<Aca_ExamSuppleJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_ExamSuppleJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_ExamSuppleJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ExamSuppleJson> jsonList, ICollection<Aca_ExamSupple> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_ExamSupple obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_ExamSupple.GetNew();
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
