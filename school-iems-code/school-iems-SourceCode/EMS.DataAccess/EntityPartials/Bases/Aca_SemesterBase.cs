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
    public partial class Aca_Semester
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_SemesterNo = "SemesterNo";		            
		public const string Property_Name = "Name";		            
		public const string Property_TermId = "TermId";		            
		public const string Property_StartDate = "StartDate";		            
		public const string Property_EndDate = "EndDate";		            
		public const string Property_Year = "Year";		            
		public const string Property_AcademicSession = "AcademicSession";		            
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_ProgramTypeEnumId = "ProgramTypeEnumId";		            
        public const string Property_ProgramType = "ProgramType";
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_IsRegistrationSemester = "IsRegistrationSemester";		            
		public const string Property_EnabledRegistrationTypeEnumId = "EnabledRegistrationTypeEnumId";		            
        public const string Property_EnabledRegistrationType = "EnabledRegistrationType";
		public const string Property_RegistrationStartDate = "RegistrationStartDate";		            
		public const string Property_RegistrationEndDate = "RegistrationEndDate";		            
		public const string Property_IsEnableLateRegFee = "IsEnableLateRegFee";		            
		public const string Property_LateRegFeeStartDate = "LateRegFeeStartDate";		            
		public const string Property_LateRegFeeAmount = "LateRegFeeAmount";		            
		public const string Property_MaximumUGCredit = "MaximumUGCredit";		            
		public const string Property_MinimumUGCredit = "MinimumUGCredit";		            
		public const string Property_MaximumPGCredit = "MaximumPGCredit";		            
		public const string Property_MinimumPGCredit = "MinimumPGCredit";		            
		public const string Property_ExamPermitPublishDate = "ExamPermitPublishDate";		            
		public const string Property_CurrentDropPercentage = "CurrentDropPercentage";		            
		public const string Property_NewStudentIdPrefix = "NewStudentIdPrefix";		            
		public const string Property_NewStudentSuffix = "NewStudentSuffix";		            
		public const string Property_SectionTolarange = "SectionTolarange";		            
		public const string Property_IsTpeEnable = "IsTpeEnable";		            
		public const string Property_IsForceTpeSubmit = "IsForceTpeSubmit";		            
		public const string Property_MinimumGradePointForRetake = "MinimumGradePointForRetake";		            
		public const string Property_SemesterDurationEnumId = "SemesterDurationEnumId";		            
        public const string Property_SemesterDuration = "SemesterDuration";
		public const string Property_StartingMonthEnumId = "StartingMonthEnumId";		            
        public const string Property_StartingMonth = "StartingMonth";
		public const string Property_IsEnable = "IsEnable";		            
		public const string Property_IsOpenBulkRegistration = "IsOpenBulkRegistration";		            
		public const string Property_IsOpenOnlineRegistration = "IsOpenOnlineRegistration";		            
		public const string Property_IsCloseStudentProfileRegistration = "IsCloseStudentProfileRegistration";		            
		public const string Property_CanCancelCompletedSemesterCourse = "CanCancelCompletedSemesterCourse";		            
		public const string Property_RegistrationMessage = "RegistrationMessage";		            
		public const string Property_RegistrationAllowedPaymentDueUpto = "RegistrationAllowedPaymentDueUpto";		            
              
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
      
     
     
      
        public static Aca_Semester GetNew(Int64 id=0)
        {
           Aca_Semester obj = new Aca_Semester();
               obj.Id = id;
                obj.SemesterNo = 0 ; 
                obj.Name = string.Empty ; 
                obj.TermId = 0 ; 
                obj.StartDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.EndDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.Year = 0 ; 
                obj.AcademicSession = string.Empty ; 
                obj.StatusEnumId = 0 ; 
                obj.ProgramTypeEnumId = 0 ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsRegistrationSemester = false ; 
                obj.EnabledRegistrationTypeEnumId = 0 ; 
                obj.RegistrationStartDate = null ; 
                obj.RegistrationEndDate = null ; 
                obj.IsEnableLateRegFee = false ; 
                obj.LateRegFeeStartDate = null ; 
                obj.LateRegFeeAmount = 0.0F ; 
                obj.MaximumUGCredit = 0.0F ; 
                obj.MinimumUGCredit = 0.0F ; 
                obj.MaximumPGCredit = 0.0F ; 
                obj.MinimumPGCredit = 0.0F ; 
                obj.ExamPermitPublishDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CurrentDropPercentage = 0.0F ; 
                obj.NewStudentIdPrefix = 0 ; 
                obj.NewStudentSuffix = 0 ; 
                obj.SectionTolarange = 0 ; 
                obj.IsTpeEnable = false ; 
                obj.IsForceTpeSubmit = false ; 
                obj.MinimumGradePointForRetake = 0.0F ; 
                obj.SemesterDurationEnumId = 0 ; 
                obj.StartingMonthEnumId = null ; 
                obj.IsEnable = false ; 
                obj.IsOpenBulkRegistration = false ; 
                obj.IsOpenOnlineRegistration = false ; 
                obj.IsCloseStudentProfileRegistration = false ; 
                obj.CanCancelCompletedSemesterCourse = false ; 
                obj.RegistrationMessage = null ; 
                obj.RegistrationAllowedPaymentDueUpto = 0.0F ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
