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
	public partial class User_StudentJson 
	{
          public Int32 Id { get; set; }
          public Int32 UserId { get; set; }
          public String ClassRollNo { get; set; }
          public String RegistrationNo { get; set; }
          public String RegistrationSession { get; set; }
          public Int32 GradingPolicyId { get; set; }
          public Single CGPA { get; set; }
          public Single CourseCompleted { get; set; }
          public Single CreditCompleted { get; set; }
          public Single IncompleteCredits { get; set; }
          public Single CreditTransfer { get; set; }
          public Single CreditWaiver { get; set; }
          public Nullable<Int32> LevelTermId { get; set; }
          public Int32 ProgramId { get; set; }
          public Int32 ContinuingBatchId { get; set; }
          public Byte ClassTimingGroupEnumId { get; set; }
          public string ClassTimingGroup { get; set; }
          public Int32 FeeCodeId { get; set; }
          public Boolean IsFeesVerified { get; set; }
          public String CurriculumId { get; set; }
          public String MajorCurriculumId { get; set; }
          public String SecondMajorCurriculumId { get; set; }
          public String MinorCurriculumId { get; set; }
          public String ElectiveCurriculumId { get; set; }
          public Nullable<Int32> StudentQuotaNameId { get; set; }
          public Byte EnrollmentStatusEnumId { get; set; }
          public string EnrollmentStatus { get; set; }
          public Byte EnrollmentTypeEnumId { get; set; }
          public string EnrollmentType { get; set; }
          public Nullable<Int32> ParentsPrimaryJobTypeId { get; set; }
          public String FatherMobile { get; set; }
          public String MotherMobile { get; set; }
          public Int32 AdmissionExamId { get; set; }
          public String AdmissionTestRollNo { get; set; }
          public String AdmissionFormNo { get; set; }
          public Byte AdmissionStatusEnumId { get; set; }
          public string AdmissionStatus { get; set; }
          public String AdmissionRemark { get; set; }
          public Boolean IsAdmissionFeePaid { get; set; }
          public String CertificateRegistrationNo { get; set; }
          public Nullable<DateTime> ProvisionalCertificateIssueDate { get; set; }
          public Nullable<DateTime> OriginalCertificateIssueDate { get; set; }
          public Nullable<DateTime> OriginalTranscriptIssueDate { get; set; }
          public String GraduationSemesterId { get; set; }
          public String DegreeOptained { get; set; }
          public String MajorMinorDegreeText { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public Single PerCreditFee { get; set; }
          public Int32 ClassSectionId { get; set; }
          public String TranscriptSerialNo { get; set; }
          public String OriginalCertificateSerialNo { get; set; }
          public String OriginalCertificateVerifyNo { get; set; }
          public String ProvisionalCertificateSerialNo { get; set; }
          public String ProvisionalCertificateVerifyNo { get; set; }
          public Int32 JoiningBatchId { get; set; }
          public Nullable<Int32> PreviousProgramId { get; set; }
          public String PreviousStudentUserName { get; set; }
          public Int32 SemesterDurationEnumId { get; set; }
          public string SemesterDuration { get; set; }
          public Boolean IsRequiredCGPACalculation { get; set; }
          public Nullable<DateTime> LastCGPASyncTime { get; set; }
          public Boolean ForceDisableOnlineRegistration { get; set; }
          public Boolean EnableOpenCreditRegistration { get; set; }
          public String UgcUniqueId { get; set; }
          public Boolean IsProfileEditLock { get; set; }
          public Nullable<Int32> MaxEligibleRegistrationSemesterCount { get; set; }
          public String DiscontinueSemesterId { get; set; }
          public Single DefaultEnrollmentPeriod { get; set; }
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
	   
		public static void Map(this User_Student entity, User_StudentJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.UserId = entity.UserId ; 
			 toJson.ClassRollNo = entity.ClassRollNo ; 
			 toJson.RegistrationNo = entity.RegistrationNo ; 
			 toJson.RegistrationSession = entity.RegistrationSession ; 
			 toJson.GradingPolicyId = entity.GradingPolicyId ; 
			 toJson.CGPA = entity.CGPA ; 
			 toJson.CourseCompleted = entity.CourseCompleted ; 
			 toJson.CreditCompleted = entity.CreditCompleted ; 
			 toJson.IncompleteCredits = entity.IncompleteCredits ; 
			 toJson.CreditTransfer = entity.CreditTransfer ; 
			 toJson.CreditWaiver = entity.CreditWaiver ; 
			 toJson.LevelTermId = entity.LevelTermId ; 
			 toJson.ProgramId = entity.ProgramId ; 
			 toJson.ContinuingBatchId = entity.ContinuingBatchId ; 
			 toJson.ClassTimingGroupEnumId = entity.ClassTimingGroupEnumId ; 
             if(entity.ClassTimingGroupEnumId!=null)
             toJson.ClassTimingGroup = entity.ClassTimingGroup.ToString().AddSpacesToSentence();
			 toJson.FeeCodeId = entity.FeeCodeId ; 
			 toJson.IsFeesVerified = entity.IsFeesVerified ; 
			 toJson.CurriculumId = entity.CurriculumId.ToString() ; 
             if(entity.MajorCurriculumId!=null)
			 toJson.MajorCurriculumId = entity.MajorCurriculumId.ToString() ; 
             if(entity.SecondMajorCurriculumId!=null)
			 toJson.SecondMajorCurriculumId = entity.SecondMajorCurriculumId.ToString() ; 
             if(entity.MinorCurriculumId!=null)
			 toJson.MinorCurriculumId = entity.MinorCurriculumId.ToString() ; 
             if(entity.ElectiveCurriculumId!=null)
			 toJson.ElectiveCurriculumId = entity.ElectiveCurriculumId.ToString() ; 
			 toJson.StudentQuotaNameId = entity.StudentQuotaNameId ; 
			 toJson.EnrollmentStatusEnumId = entity.EnrollmentStatusEnumId ; 
             if(entity.EnrollmentStatusEnumId!=null)
             toJson.EnrollmentStatus = entity.EnrollmentStatus.ToString().AddSpacesToSentence();
			 toJson.EnrollmentTypeEnumId = entity.EnrollmentTypeEnumId ; 
             if(entity.EnrollmentTypeEnumId!=null)
             toJson.EnrollmentType = entity.EnrollmentType.ToString().AddSpacesToSentence();
			 toJson.ParentsPrimaryJobTypeId = entity.ParentsPrimaryJobTypeId ; 
			 toJson.FatherMobile = entity.FatherMobile ; 
			 toJson.MotherMobile = entity.MotherMobile ; 
			 toJson.AdmissionExamId = entity.AdmissionExamId ; 
			 toJson.AdmissionTestRollNo = entity.AdmissionTestRollNo ; 
			 toJson.AdmissionFormNo = entity.AdmissionFormNo ; 
			 toJson.AdmissionStatusEnumId = entity.AdmissionStatusEnumId ; 
             if(entity.AdmissionStatusEnumId!=null)
             toJson.AdmissionStatus = entity.AdmissionStatus.ToString().AddSpacesToSentence();
			 toJson.AdmissionRemark = entity.AdmissionRemark ; 
			 toJson.IsAdmissionFeePaid = entity.IsAdmissionFeePaid ; 
			 toJson.CertificateRegistrationNo = entity.CertificateRegistrationNo ; 
			 toJson.ProvisionalCertificateIssueDate = entity.ProvisionalCertificateIssueDate ; 
			 toJson.OriginalCertificateIssueDate = entity.OriginalCertificateIssueDate ; 
			 toJson.OriginalTranscriptIssueDate = entity.OriginalTranscriptIssueDate ; 
             if(entity.GraduationSemesterId!=null)
			 toJson.GraduationSemesterId = entity.GraduationSemesterId.ToString() ; 
			 toJson.DegreeOptained = entity.DegreeOptained ; 
			 toJson.MajorMinorDegreeText = entity.MajorMinorDegreeText ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.PerCreditFee = entity.PerCreditFee ; 
			 toJson.ClassSectionId = entity.ClassSectionId ; 
			 toJson.TranscriptSerialNo = entity.TranscriptSerialNo ; 
			 toJson.OriginalCertificateSerialNo = entity.OriginalCertificateSerialNo ; 
			 toJson.OriginalCertificateVerifyNo = entity.OriginalCertificateVerifyNo ; 
			 toJson.ProvisionalCertificateSerialNo = entity.ProvisionalCertificateSerialNo ; 
			 toJson.ProvisionalCertificateVerifyNo = entity.ProvisionalCertificateVerifyNo ; 
			 toJson.JoiningBatchId = entity.JoiningBatchId ; 
			 toJson.PreviousProgramId = entity.PreviousProgramId ; 
			 toJson.PreviousStudentUserName = entity.PreviousStudentUserName ; 
			 toJson.SemesterDurationEnumId = entity.SemesterDurationEnumId ; 
             if(entity.SemesterDurationEnumId!=null)
             toJson.SemesterDuration = entity.SemesterDuration.ToString().AddSpacesToSentence();
			 toJson.IsRequiredCGPACalculation = entity.IsRequiredCGPACalculation ; 
			 toJson.LastCGPASyncTime = entity.LastCGPASyncTime ; 
			 toJson.ForceDisableOnlineRegistration = entity.ForceDisableOnlineRegistration ; 
			 toJson.EnableOpenCreditRegistration = entity.EnableOpenCreditRegistration ; 
			 toJson.UgcUniqueId = entity.UgcUniqueId ; 
			 toJson.IsProfileEditLock = entity.IsProfileEditLock ; 
			 toJson.MaxEligibleRegistrationSemesterCount = entity.MaxEligibleRegistrationSemesterCount ; 
             if(entity.DiscontinueSemesterId!=null)
			 toJson.DiscontinueSemesterId = entity.DiscontinueSemesterId.ToString() ; 
			 toJson.DefaultEnrollmentPeriod = entity.DefaultEnrollmentPeriod ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this User_StudentJson json, User_Student toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.UserId=json.UserId;
                 toEntity.ClassRollNo = json.ClassRollNo?.Trim() ?? json.ClassRollNo;
                 toEntity.RegistrationNo = json.RegistrationNo?.Trim() ?? json.RegistrationNo;
                 toEntity.RegistrationSession = json.RegistrationSession?.Trim() ?? json.RegistrationSession;
    			 toEntity.GradingPolicyId=json.GradingPolicyId;
    			 toEntity.CGPA=json.CGPA;
    			 toEntity.CourseCompleted=json.CourseCompleted;
    			 toEntity.CreditCompleted=json.CreditCompleted;
    			 toEntity.IncompleteCredits=json.IncompleteCredits;
    			 toEntity.CreditTransfer=json.CreditTransfer;
    			 toEntity.CreditWaiver=json.CreditWaiver;
    			 toEntity.LevelTermId=json.LevelTermId;
    			 toEntity.ProgramId=json.ProgramId;
    			 toEntity.ContinuingBatchId=json.ContinuingBatchId;
    			 toEntity.ClassTimingGroupEnumId=json.ClassTimingGroupEnumId;
    			 toEntity.FeeCodeId=json.FeeCodeId;
    			 toEntity.IsFeesVerified=json.IsFeesVerified;
                 toEntity.CurriculumId = long.TryParse(json.CurriculumId, out output) ? output : 0;
                 toEntity.MajorCurriculumId = long.TryParse(json.MajorCurriculumId, out output) ? output : (Int64?)null;
                 toEntity.SecondMajorCurriculumId = long.TryParse(json.SecondMajorCurriculumId, out output) ? output : (Int64?)null;
                 toEntity.MinorCurriculumId = long.TryParse(json.MinorCurriculumId, out output) ? output : (Int64?)null;
                 toEntity.ElectiveCurriculumId = long.TryParse(json.ElectiveCurriculumId, out output) ? output : (Int64?)null;
    			 toEntity.StudentQuotaNameId=json.StudentQuotaNameId;
    			 toEntity.EnrollmentStatusEnumId=json.EnrollmentStatusEnumId;
    			 toEntity.EnrollmentTypeEnumId=json.EnrollmentTypeEnumId;
    			 toEntity.ParentsPrimaryJobTypeId=json.ParentsPrimaryJobTypeId;
                 toEntity.FatherMobile = json.FatherMobile?.Trim() ?? json.FatherMobile;
                 toEntity.MotherMobile = json.MotherMobile?.Trim() ?? json.MotherMobile;
    			 toEntity.AdmissionExamId=json.AdmissionExamId;
                 toEntity.AdmissionTestRollNo = json.AdmissionTestRollNo?.Trim() ?? json.AdmissionTestRollNo;
                 toEntity.AdmissionFormNo = json.AdmissionFormNo?.Trim() ?? json.AdmissionFormNo;
    			 toEntity.AdmissionStatusEnumId=json.AdmissionStatusEnumId;
                 toEntity.AdmissionRemark = json.AdmissionRemark?.Trim() ?? json.AdmissionRemark;
    			 toEntity.IsAdmissionFeePaid=json.IsAdmissionFeePaid;
                 toEntity.CertificateRegistrationNo = json.CertificateRegistrationNo?.Trim() ?? json.CertificateRegistrationNo;
    			 toEntity.ProvisionalCertificateIssueDate=json.ProvisionalCertificateIssueDate;
    			 toEntity.OriginalCertificateIssueDate=json.OriginalCertificateIssueDate;
    			 toEntity.OriginalTranscriptIssueDate=json.OriginalTranscriptIssueDate;
                 toEntity.GraduationSemesterId = long.TryParse(json.GraduationSemesterId, out output) ? output : (Int64?)null;
                 toEntity.DegreeOptained = json.DegreeOptained?.Trim() ?? json.DegreeOptained;
                 toEntity.MajorMinorDegreeText = json.MajorMinorDegreeText?.Trim() ?? json.MajorMinorDegreeText;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.PerCreditFee=json.PerCreditFee;
    			 toEntity.ClassSectionId=json.ClassSectionId;
                 toEntity.TranscriptSerialNo = json.TranscriptSerialNo?.Trim() ?? json.TranscriptSerialNo;
                 toEntity.OriginalCertificateSerialNo = json.OriginalCertificateSerialNo?.Trim() ?? json.OriginalCertificateSerialNo;
                 toEntity.OriginalCertificateVerifyNo = json.OriginalCertificateVerifyNo?.Trim() ?? json.OriginalCertificateVerifyNo;
                 toEntity.ProvisionalCertificateSerialNo = json.ProvisionalCertificateSerialNo?.Trim() ?? json.ProvisionalCertificateSerialNo;
                 toEntity.ProvisionalCertificateVerifyNo = json.ProvisionalCertificateVerifyNo?.Trim() ?? json.ProvisionalCertificateVerifyNo;
    			 toEntity.JoiningBatchId=json.JoiningBatchId;
    			 toEntity.PreviousProgramId=json.PreviousProgramId;
                 toEntity.PreviousStudentUserName = json.PreviousStudentUserName?.Trim() ?? json.PreviousStudentUserName;
    			 toEntity.SemesterDurationEnumId=json.SemesterDurationEnumId;
    			 toEntity.IsRequiredCGPACalculation=json.IsRequiredCGPACalculation;
    			 toEntity.LastCGPASyncTime=json.LastCGPASyncTime;
    			 toEntity.ForceDisableOnlineRegistration=json.ForceDisableOnlineRegistration;
    			 toEntity.EnableOpenCreditRegistration=json.EnableOpenCreditRegistration;
                 toEntity.UgcUniqueId = json.UgcUniqueId?.Trim() ?? json.UgcUniqueId;
    			 toEntity.IsProfileEditLock=json.IsProfileEditLock;
    			 toEntity.MaxEligibleRegistrationSemesterCount=json.MaxEligibleRegistrationSemesterCount;
                 toEntity.DiscontinueSemesterId = long.TryParse(json.DiscontinueSemesterId, out output) ? output : (Int64?)null;
    			 toEntity.DefaultEnrollmentPeriod=json.DefaultEnrollmentPeriod;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<User_Student> entityList, IList<User_StudentJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 User_StudentJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new User_StudentJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<User_StudentJson> jsonList, ICollection<User_Student> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    User_Student obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = User_Student.GetNew();
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
