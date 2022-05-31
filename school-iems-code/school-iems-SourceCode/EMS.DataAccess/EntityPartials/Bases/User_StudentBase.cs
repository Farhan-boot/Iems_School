//File: Entity Partial Base
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace EMS.DataAccess.Data
{

    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
    public partial class User_Student
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_UserId = "UserId";		            
		public const string Property_ClassRollNo = "ClassRollNo";		            
		public const string Property_RegistrationNo = "RegistrationNo";		            
		public const string Property_RegistrationSession = "RegistrationSession";		            
		public const string Property_GradingPolicyId = "GradingPolicyId";		            
		public const string Property_CGPA = "CGPA";		            
		public const string Property_CourseCompleted = "CourseCompleted";		            
		public const string Property_CreditCompleted = "CreditCompleted";		            
		public const string Property_IncompleteCredits = "IncompleteCredits";		            
		public const string Property_CreditTransfer = "CreditTransfer";		            
		public const string Property_CreditWaiver = "CreditWaiver";		            
		public const string Property_LevelTermId = "LevelTermId";		            
		public const string Property_ProgramId = "ProgramId";		            
		public const string Property_ContinuingBatchId = "ContinuingBatchId";		            
		public const string Property_ClassTimingGroupEnumId = "ClassTimingGroupEnumId";		            
        public const string Property_ClassTimingGroup = "ClassTimingGroup";
		public const string Property_FeeCodeId = "FeeCodeId";		            
		public const string Property_IsFeesVerified = "IsFeesVerified";		            
		public const string Property_CurriculumId = "CurriculumId";		            
		public const string Property_MajorCurriculumId = "MajorCurriculumId";		            
		public const string Property_SecondMajorCurriculumId = "SecondMajorCurriculumId";		            
		public const string Property_MinorCurriculumId = "MinorCurriculumId";		            
		public const string Property_ElectiveCurriculumId = "ElectiveCurriculumId";		            
		public const string Property_StudentQuotaNameId = "StudentQuotaNameId";		            
		public const string Property_EnrollmentStatusEnumId = "EnrollmentStatusEnumId";		            
        public const string Property_EnrollmentStatus = "EnrollmentStatus";
		public const string Property_EnrollmentTypeEnumId = "EnrollmentTypeEnumId";		            
        public const string Property_EnrollmentType = "EnrollmentType";
		public const string Property_ParentsPrimaryJobTypeId = "ParentsPrimaryJobTypeId";		            
		public const string Property_FatherMobile = "FatherMobile";		            
		public const string Property_MotherMobile = "MotherMobile";		            
		public const string Property_AdmissionExamId = "AdmissionExamId";		            
		public const string Property_AdmissionTestRollNo = "AdmissionTestRollNo";		            
		public const string Property_AdmissionFormNo = "AdmissionFormNo";		            
		public const string Property_AdmissionStatusEnumId = "AdmissionStatusEnumId";		            
        public const string Property_AdmissionStatus = "AdmissionStatus";
		public const string Property_AdmissionRemark = "AdmissionRemark";		            
		public const string Property_IsAdmissionFeePaid = "IsAdmissionFeePaid";		            
		public const string Property_CertificateRegistrationNo = "CertificateRegistrationNo";		            
		public const string Property_ProvisionalCertificateIssueDate = "ProvisionalCertificateIssueDate";		            
		public const string Property_OriginalCertificateIssueDate = "OriginalCertificateIssueDate";		            
		public const string Property_OriginalTranscriptIssueDate = "OriginalTranscriptIssueDate";		            
		public const string Property_GraduationSemesterId = "GraduationSemesterId";		            
		public const string Property_DegreeOptained = "DegreeOptained";		            
		public const string Property_MajorMinorDegreeText = "MajorMinorDegreeText";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_PerCreditFee = "PerCreditFee";		            
		public const string Property_ClassSectionId = "ClassSectionId";		            
		public const string Property_TranscriptSerialNo = "TranscriptSerialNo";		            
		public const string Property_OriginalCertificateSerialNo = "OriginalCertificateSerialNo";		            
		public const string Property_OriginalCertificateVerifyNo = "OriginalCertificateVerifyNo";		            
		public const string Property_ProvisionalCertificateSerialNo = "ProvisionalCertificateSerialNo";		            
		public const string Property_ProvisionalCertificateVerifyNo = "ProvisionalCertificateVerifyNo";		            
		public const string Property_JoiningBatchId = "JoiningBatchId";		            
		public const string Property_PreviousProgramId = "PreviousProgramId";		            
		public const string Property_PreviousStudentUserName = "PreviousStudentUserName";		            
		public const string Property_SemesterDurationEnumId = "SemesterDurationEnumId";		            
        public const string Property_SemesterDuration = "SemesterDuration";
		public const string Property_IsRequiredCGPACalculation = "IsRequiredCGPACalculation";		            
		public const string Property_LastCGPASyncTime = "LastCGPASyncTime";		            
		public const string Property_ForceDisableOnlineRegistration = "ForceDisableOnlineRegistration";		            
		public const string Property_EnableOpenCreditRegistration = "EnableOpenCreditRegistration";		            
		public const string Property_UgcUniqueId = "UgcUniqueId";		            
		public const string Property_IsProfileEditLock = "IsProfileEditLock";		            
		public const string Property_MaxEligibleRegistrationSemesterCount = "MaxEligibleRegistrationSemesterCount";		            
		public const string Property_DiscontinueSemesterId = "DiscontinueSemesterId";		            
		public const string Property_DefaultEnrollmentPeriod = "DefaultEnrollmentPeriod";		            
              
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public const string Property_IsSelected = "IsSelected";
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public const string Property_IsAlreadyAdded = "IsAlreadyAdded";
	  #endregion
      
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public bool IsAlreadyAdded = true;
      
     
     
      
        public static User_Student GetNew(Int32 id=0)
        {
           User_Student obj = new User_Student();
               obj.Id = id;
                obj.UserId = 0 ; 
                obj.ClassRollNo = string.Empty ; 
                obj.RegistrationNo = null ; 
                obj.RegistrationSession = null ; 
                obj.GradingPolicyId = 0 ; 
                obj.CGPA = 0.0F ; 
                obj.CourseCompleted = 0.0F ; 
                obj.CreditCompleted = 0.0F ; 
                obj.IncompleteCredits = 0.0F ; 
                obj.CreditTransfer = 0.0F ; 
                obj.CreditWaiver = 0.0F ; 
                obj.LevelTermId = null ; 
                obj.ProgramId = 0 ; 
                obj.ContinuingBatchId = 0 ; 
                obj.ClassTimingGroupEnumId = 0 ; 
                obj.FeeCodeId = 0 ; 
                obj.IsFeesVerified = false ; 
                obj.CurriculumId = 0 ; 
                obj.MajorCurriculumId = null ; 
                obj.SecondMajorCurriculumId = null ; 
                obj.MinorCurriculumId = null ; 
                obj.ElectiveCurriculumId = null ; 
                obj.StudentQuotaNameId = null ; 
                obj.EnrollmentStatusEnumId = 0 ; 
                obj.EnrollmentTypeEnumId = 0 ; 
                obj.ParentsPrimaryJobTypeId = null ; 
                obj.FatherMobile = null ; 
                obj.MotherMobile = null ; 
                obj.AdmissionExamId = 0 ; 
                obj.AdmissionTestRollNo = null ; 
                obj.AdmissionFormNo = null ; 
                obj.AdmissionStatusEnumId = 0 ; 
                obj.AdmissionRemark = null ; 
                obj.IsAdmissionFeePaid = false ; 
                obj.CertificateRegistrationNo = null ; 
                obj.ProvisionalCertificateIssueDate = null ; 
                obj.OriginalCertificateIssueDate = null ; 
                obj.OriginalTranscriptIssueDate = null ; 
                obj.GraduationSemesterId = null ; 
                obj.DegreeOptained = null ; 
                obj.MajorMinorDegreeText = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.PerCreditFee = 0.0F ; 
                obj.ClassSectionId = 0 ; 
                obj.TranscriptSerialNo = null ; 
                obj.OriginalCertificateSerialNo = null ; 
                obj.OriginalCertificateVerifyNo = null ; 
                obj.ProvisionalCertificateSerialNo = null ; 
                obj.ProvisionalCertificateVerifyNo = null ; 
                obj.JoiningBatchId = 0 ; 
                obj.PreviousProgramId = null ; 
                obj.PreviousStudentUserName = null ; 
                obj.SemesterDurationEnumId = 0 ; 
                obj.IsRequiredCGPACalculation = false ; 
                obj.LastCGPASyncTime = null ; 
                obj.ForceDisableOnlineRegistration = false ; 
                obj.EnableOpenCreditRegistration = false ; 
                obj.UgcUniqueId = null ; 
                obj.IsProfileEditLock = false ; 
                obj.MaxEligibleRegistrationSemesterCount = null ; 
                obj.DiscontinueSemesterId = null ; 
                obj.DefaultEnrollmentPeriod = 0.0F ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
