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
	public partial class Aca_SemesterJson 
	{
          public String Id { get; set; }
          public Int32 SemesterNo { get; set; }
          public String Name { get; set; }
          public Int32 TermId { get; set; }
          public DateTime StartDate { get; set; }
          public DateTime EndDate { get; set; }
          public Int16 Year { get; set; }
          public String AcademicSession { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public Byte ProgramTypeEnumId { get; set; }
          public string ProgramType { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public Boolean IsRegistrationSemester { get; set; }
          public Byte EnabledRegistrationTypeEnumId { get; set; }
          public string EnabledRegistrationType { get; set; }
          public Nullable<DateTime> RegistrationStartDate { get; set; }
          public Nullable<DateTime> RegistrationEndDate { get; set; }
          public Boolean IsEnableLateRegFee { get; set; }
          public Nullable<DateTime> LateRegFeeStartDate { get; set; }
          public Single LateRegFeeAmount { get; set; }
          public Single MaximumUGCredit { get; set; }
          public Single MinimumUGCredit { get; set; }
          public Single MaximumPGCredit { get; set; }
          public Single MinimumPGCredit { get; set; }
          public DateTime ExamPermitPublishDate { get; set; }
          public Single CurrentDropPercentage { get; set; }
          public Int32 NewStudentIdPrefix { get; set; }
          public Int32 NewStudentSuffix { get; set; }
          public Int32 SectionTolarange { get; set; }
          public Boolean IsTpeEnable { get; set; }
          public Boolean IsForceTpeSubmit { get; set; }
          public Single MinimumGradePointForRetake { get; set; }
          public Int32 SemesterDurationEnumId { get; set; }
          public string SemesterDuration { get; set; }
          public Nullable<Int32> StartingMonthEnumId { get; set; }
          public string StartingMonth { get; set; }
          public Boolean IsEnable { get; set; }
          public Boolean IsOpenBulkRegistration { get; set; }
          public Boolean IsOpenOnlineRegistration { get; set; }
          public Boolean IsCloseStudentProfileRegistration { get; set; }
          public Boolean CanCancelCompletedSemesterCourse { get; set; }
          public String RegistrationMessage { get; set; }
          public Single RegistrationAllowedPaymentDueUpto { get; set; }
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
	   
		public static void Map(this Aca_Semester entity, Aca_SemesterJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.SemesterNo = entity.SemesterNo ; 
			 toJson.Name = entity.Name ; 
			 toJson.TermId = entity.TermId ; 
			 toJson.StartDate = entity.StartDate ; 
			 toJson.EndDate = entity.EndDate ; 
			 toJson.Year = entity.Year ; 
			 toJson.AcademicSession = entity.AcademicSession ; 
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.ProgramTypeEnumId = entity.ProgramTypeEnumId ; 
             if(entity.ProgramTypeEnumId!=null)
             toJson.ProgramType = entity.ProgramType.ToString().AddSpacesToSentence();
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.IsRegistrationSemester = entity.IsRegistrationSemester ; 
			 toJson.EnabledRegistrationTypeEnumId = entity.EnabledRegistrationTypeEnumId ; 
             if(entity.EnabledRegistrationTypeEnumId!=null)
             toJson.EnabledRegistrationType = entity.EnabledRegistrationType.ToString().AddSpacesToSentence();
			 toJson.RegistrationStartDate = entity.RegistrationStartDate ; 
			 toJson.RegistrationEndDate = entity.RegistrationEndDate ; 
			 toJson.IsEnableLateRegFee = entity.IsEnableLateRegFee ; 
			 toJson.LateRegFeeStartDate = entity.LateRegFeeStartDate ; 
			 toJson.LateRegFeeAmount = entity.LateRegFeeAmount ; 
			 toJson.MaximumUGCredit = entity.MaximumUGCredit ; 
			 toJson.MinimumUGCredit = entity.MinimumUGCredit ; 
			 toJson.MaximumPGCredit = entity.MaximumPGCredit ; 
			 toJson.MinimumPGCredit = entity.MinimumPGCredit ; 
			 toJson.ExamPermitPublishDate = entity.ExamPermitPublishDate ; 
			 toJson.CurrentDropPercentage = entity.CurrentDropPercentage ; 
			 toJson.NewStudentIdPrefix = entity.NewStudentIdPrefix ; 
			 toJson.NewStudentSuffix = entity.NewStudentSuffix ; 
			 toJson.SectionTolarange = entity.SectionTolarange ; 
			 toJson.IsTpeEnable = entity.IsTpeEnable ; 
			 toJson.IsForceTpeSubmit = entity.IsForceTpeSubmit ; 
			 toJson.MinimumGradePointForRetake = entity.MinimumGradePointForRetake ; 
			 toJson.SemesterDurationEnumId = entity.SemesterDurationEnumId ; 
             if(entity.SemesterDurationEnumId!=null)
             toJson.SemesterDuration = entity.SemesterDuration.ToString().AddSpacesToSentence();
			 toJson.StartingMonthEnumId = entity.StartingMonthEnumId ; 
             if(entity.StartingMonthEnumId!=null)
             toJson.StartingMonth = entity.StartingMonth.ToString().AddSpacesToSentence();
			 toJson.IsEnable = entity.IsEnable ; 
			 toJson.IsOpenBulkRegistration = entity.IsOpenBulkRegistration ; 
			 toJson.IsOpenOnlineRegistration = entity.IsOpenOnlineRegistration ; 
			 toJson.IsCloseStudentProfileRegistration = entity.IsCloseStudentProfileRegistration ; 
			 toJson.CanCancelCompletedSemesterCourse = entity.CanCancelCompletedSemesterCourse ; 
			 toJson.RegistrationMessage = entity.RegistrationMessage ; 
			 toJson.RegistrationAllowedPaymentDueUpto = entity.RegistrationAllowedPaymentDueUpto ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_SemesterJson json, Aca_Semester toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.SemesterNo=json.SemesterNo;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.TermId=json.TermId;
    			 toEntity.StartDate=json.StartDate;
    			 toEntity.EndDate=json.EndDate;
    			 toEntity.Year=json.Year;
                 toEntity.AcademicSession = json.AcademicSession?.Trim() ?? json.AcademicSession;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.ProgramTypeEnumId=json.ProgramTypeEnumId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.IsRegistrationSemester=json.IsRegistrationSemester;
    			 toEntity.EnabledRegistrationTypeEnumId=json.EnabledRegistrationTypeEnumId;
    			 toEntity.RegistrationStartDate=json.RegistrationStartDate;
    			 toEntity.RegistrationEndDate=json.RegistrationEndDate;
    			 toEntity.IsEnableLateRegFee=json.IsEnableLateRegFee;
    			 toEntity.LateRegFeeStartDate=json.LateRegFeeStartDate;
    			 toEntity.LateRegFeeAmount=json.LateRegFeeAmount;
    			 toEntity.MaximumUGCredit=json.MaximumUGCredit;
    			 toEntity.MinimumUGCredit=json.MinimumUGCredit;
    			 toEntity.MaximumPGCredit=json.MaximumPGCredit;
    			 toEntity.MinimumPGCredit=json.MinimumPGCredit;
    			 toEntity.ExamPermitPublishDate=json.ExamPermitPublishDate;
    			 toEntity.CurrentDropPercentage=json.CurrentDropPercentage;
    			 toEntity.NewStudentIdPrefix=json.NewStudentIdPrefix;
    			 toEntity.NewStudentSuffix=json.NewStudentSuffix;
    			 toEntity.SectionTolarange=json.SectionTolarange;
    			 toEntity.IsTpeEnable=json.IsTpeEnable;
    			 toEntity.IsForceTpeSubmit=json.IsForceTpeSubmit;
    			 toEntity.MinimumGradePointForRetake=json.MinimumGradePointForRetake;
    			 toEntity.SemesterDurationEnumId=json.SemesterDurationEnumId;
    			 toEntity.StartingMonthEnumId=json.StartingMonthEnumId;
    			 toEntity.IsEnable=json.IsEnable;
    			 toEntity.IsOpenBulkRegistration=json.IsOpenBulkRegistration;
    			 toEntity.IsOpenOnlineRegistration=json.IsOpenOnlineRegistration;
    			 toEntity.IsCloseStudentProfileRegistration=json.IsCloseStudentProfileRegistration;
    			 toEntity.CanCancelCompletedSemesterCourse=json.CanCancelCompletedSemesterCourse;
                 toEntity.RegistrationMessage = json.RegistrationMessage?.Trim() ?? json.RegistrationMessage;
    			 toEntity.RegistrationAllowedPaymentDueUpto=json.RegistrationAllowedPaymentDueUpto;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_Semester> entityList, IList<Aca_SemesterJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_SemesterJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_SemesterJson> jsonList, ICollection<Aca_Semester> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_Semester obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_Semester.GetNew();
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
