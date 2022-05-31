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
	public partial class Aca_ClassTakenByStudentJson 
	{
          public String Id { get; set; }
          public Int32 StudentId { get; set; }
          public String ClassId { get; set; }
          public Byte EnrollTypeEnumId { get; set; }
          public string EnrollType { get; set; }
          public Byte RegistrationStatusEnumId { get; set; }
          public string RegistrationStatus { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public Nullable<Single> TotalMark { get; set; }
          public Nullable<Int32> GradeId { get; set; }
          public String Remarks { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public String History { get; set; }
          public String MidTermAdmitCardSecretNo { get; set; }
          public String FinaltermAdmitCardSecretNo { get; set; }
          public Boolean IsPassed { get; set; }
          public Boolean IsBlockResult { get; set; }
          public String BlockResultReason { get; set; }
          public Boolean IsBlockRetake { get; set; }
          public String BlockRetakeReason { get; set; }
          public Boolean IsBlockSuppleApply { get; set; }
          public String BlockSuppleApplyReason { get; set; }
          public Boolean IsShowTranscriptAsNonCredit { get; set; }
          public Nullable<Single> GradePoint { get; set; }
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
	   
		public static void Map(this Aca_ClassTakenByStudent entity, Aca_ClassTakenByStudentJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.StudentId = entity.StudentId ; 
			 toJson.ClassId = entity.ClassId.ToString() ; 
			 toJson.EnrollTypeEnumId = entity.EnrollTypeEnumId ; 
             if(entity.EnrollTypeEnumId!=null)
             toJson.EnrollType = entity.EnrollType.ToString().AddSpacesToSentence();
			 toJson.RegistrationStatusEnumId = entity.RegistrationStatusEnumId ; 
             if(entity.RegistrationStatusEnumId!=null)
             toJson.RegistrationStatus = entity.RegistrationStatus.ToString().AddSpacesToSentence();
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.TotalMark = entity.TotalMark ; 
			 toJson.GradeId = entity.GradeId ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.History = entity.History ; 
			 toJson.MidTermAdmitCardSecretNo = entity.MidTermAdmitCardSecretNo ; 
			 toJson.FinaltermAdmitCardSecretNo = entity.FinaltermAdmitCardSecretNo ; 
			 toJson.IsPassed = entity.IsPassed ; 
			 toJson.IsBlockResult = entity.IsBlockResult ; 
			 toJson.BlockResultReason = entity.BlockResultReason ; 
			 toJson.IsBlockRetake = entity.IsBlockRetake ; 
			 toJson.BlockRetakeReason = entity.BlockRetakeReason ; 
			 toJson.IsBlockSuppleApply = entity.IsBlockSuppleApply ; 
			 toJson.BlockSuppleApplyReason = entity.BlockSuppleApplyReason ; 
			 toJson.IsShowTranscriptAsNonCredit = entity.IsShowTranscriptAsNonCredit ; 
			 toJson.GradePoint = entity.GradePoint ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ClassTakenByStudentJson json, Aca_ClassTakenByStudent toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.StudentId=json.StudentId;
                 toEntity.ClassId = long.TryParse(json.ClassId, out output) ? output : 0;
    			 toEntity.EnrollTypeEnumId=json.EnrollTypeEnumId;
    			 toEntity.RegistrationStatusEnumId=json.RegistrationStatusEnumId;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.TotalMark=json.TotalMark;
    			 toEntity.GradeId=json.GradeId;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
                 toEntity.History = json.History?.Trim() ?? json.History;
                 toEntity.MidTermAdmitCardSecretNo = json.MidTermAdmitCardSecretNo?.Trim() ?? json.MidTermAdmitCardSecretNo;
                 toEntity.FinaltermAdmitCardSecretNo = json.FinaltermAdmitCardSecretNo?.Trim() ?? json.FinaltermAdmitCardSecretNo;
    			 toEntity.IsPassed=json.IsPassed;
    			 toEntity.IsBlockResult=json.IsBlockResult;
                 toEntity.BlockResultReason = json.BlockResultReason?.Trim() ?? json.BlockResultReason;
    			 toEntity.IsBlockRetake=json.IsBlockRetake;
                 toEntity.BlockRetakeReason = json.BlockRetakeReason?.Trim() ?? json.BlockRetakeReason;
    			 toEntity.IsBlockSuppleApply=json.IsBlockSuppleApply;
                 toEntity.BlockSuppleApplyReason = json.BlockSuppleApplyReason?.Trim() ?? json.BlockSuppleApplyReason;
    			 toEntity.IsShowTranscriptAsNonCredit=json.IsShowTranscriptAsNonCredit;
    			 toEntity.GradePoint=json.GradePoint;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_ClassTakenByStudent> entityList, IList<Aca_ClassTakenByStudentJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_ClassTakenByStudentJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ClassTakenByStudentJson> jsonList, ICollection<Aca_ClassTakenByStudent> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_ClassTakenByStudent obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_ClassTakenByStudent.GetNew();
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
