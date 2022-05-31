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
    public partial class Aca_Class
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ProgramId = "ProgramId";		            
		public const string Property_DepartmentId = "DepartmentId";		            
		public const string Property_CurriculumCourseId = "CurriculumCourseId";		            
		public const string Property_Code = "Code";		            
		public const string Property_Name = "Name";		            
		public const string Property_ClassNo = "ClassNo";		            
		public const string Property_ClassSectionId = "ClassSectionId";		            
		public const string Property_StudyLevelTermId = "StudyLevelTermId";		            
		public const string Property_SemesterId = "SemesterId";		            
		public const string Property_ProgramTypeEnumId = "ProgramTypeEnumId";		            
        public const string Property_ProgramType = "ProgramType";
		public const string Property_RegularCapacity = "RegularCapacity";		            
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_CampusId = "CampusId";		            
		public const string Property_RegularExamMarkDistributionPolicyId = "RegularExamMarkDistributionPolicyId";		            
		public const string Property_ReferredExamMarkDistributionPolicyId = "ReferredExamMarkDistributionPolicyId";		            
		public const string Property_BacklogExamMarkDistributionPolicyId = "BacklogExamMarkDistributionPolicyId";		            
		public const string Property_StudentBatchId = "StudentBatchId";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_IsDasbleOnlineRegistration = "IsDasbleOnlineRegistration";		            
		public const string Property_History = "History";		            
		public const string Property_IsMidTermHardCopySubmitted = "IsMidTermHardCopySubmitted";		            
		public const string Property_IsFinalTermHardCopySubmitted = "IsFinalTermHardCopySubmitted";		            
		public const string Property_IsBlockStudentPanelResult = "IsBlockStudentPanelResult";		            
		public const string Property_IsSpecialUnlock = "IsSpecialUnlock";		            
		public const string Property_UnlockExpiryDate = "UnlockExpiryDate";		            
		public const string Property_LockUnlockHistory = "LockUnlockHistory";		            
		public const string Property_FacultyCanAddRoutine = "FacultyCanAddRoutine";		            
		public const string Property_FacultyCanEditRoutine = "FacultyCanEditRoutine";		            
              
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
      
     
     
      
        public static Aca_Class GetNew(Int64 id=0)
        {
           Aca_Class obj = new Aca_Class();
               obj.Id = id;
                obj.ProgramId = 0 ; 
                obj.DepartmentId = 0 ; 
                obj.CurriculumCourseId = 0 ; 
                obj.Code = string.Empty ; 
                obj.Name = string.Empty ; 
                obj.ClassNo = 0 ; 
                obj.ClassSectionId = 0 ; 
                obj.StudyLevelTermId = 0 ; 
                obj.SemesterId = 0 ; 
                obj.ProgramTypeEnumId = 0 ; 
                obj.RegularCapacity = 0 ; 
                obj.StatusEnumId = 0 ; 
                obj.CampusId = null ; 
                obj.RegularExamMarkDistributionPolicyId = 0 ; 
                obj.ReferredExamMarkDistributionPolicyId = null ; 
                obj.BacklogExamMarkDistributionPolicyId = null ; 
                obj.StudentBatchId = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsDasbleOnlineRegistration = false ; 
                obj.History = null ; 
                obj.IsMidTermHardCopySubmitted = false ; 
                obj.IsFinalTermHardCopySubmitted = false ; 
                obj.IsBlockStudentPanelResult = false ; 
                obj.IsSpecialUnlock = false ; 
                obj.UnlockExpiryDate = null ; 
                obj.LockUnlockHistory = null ; 
                obj.FacultyCanAddRoutine = false ; 
                obj.FacultyCanEditRoutine = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
