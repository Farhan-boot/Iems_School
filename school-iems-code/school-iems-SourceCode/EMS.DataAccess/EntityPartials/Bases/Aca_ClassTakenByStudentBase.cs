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
    public partial class Aca_ClassTakenByStudent
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_StudentId = "StudentId";		            
		public const string Property_ClassId = "ClassId";		            
		public const string Property_EnrollTypeEnumId = "EnrollTypeEnumId";		            
        public const string Property_EnrollType = "EnrollType";
		public const string Property_RegistrationStatusEnumId = "RegistrationStatusEnumId";		            
        public const string Property_RegistrationStatus = "RegistrationStatus";
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_TotalMark = "TotalMark";		            
		public const string Property_GradeId = "GradeId";		            
		public const string Property_Remarks = "Remarks";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_History = "History";		            
		public const string Property_MidTermAdmitCardSecretNo = "MidTermAdmitCardSecretNo";		            
		public const string Property_FinaltermAdmitCardSecretNo = "FinaltermAdmitCardSecretNo";		            
		public const string Property_IsPassed = "IsPassed";		            
		public const string Property_IsBlockResult = "IsBlockResult";		            
		public const string Property_BlockResultReason = "BlockResultReason";		            
		public const string Property_IsBlockRetake = "IsBlockRetake";		            
		public const string Property_BlockRetakeReason = "BlockRetakeReason";		            
		public const string Property_IsBlockSuppleApply = "IsBlockSuppleApply";		            
		public const string Property_BlockSuppleApplyReason = "BlockSuppleApplyReason";		            
		public const string Property_IsShowTranscriptAsNonCredit = "IsShowTranscriptAsNonCredit";		            
		public const string Property_GradePoint = "GradePoint";		            
              
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
      
     
     
      
        public static Aca_ClassTakenByStudent GetNew(Int64 id=0)
        {
           Aca_ClassTakenByStudent obj = new Aca_ClassTakenByStudent();
               obj.Id = id;
                obj.StudentId = 0 ; 
                obj.ClassId = 0 ; 
                obj.EnrollTypeEnumId = 0 ; 
                obj.RegistrationStatusEnumId = 0 ; 
                obj.StatusEnumId = 0 ; 
                obj.TotalMark = null ; 
                obj.GradeId = null ; 
                obj.Remarks = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.History = null ; 
                obj.MidTermAdmitCardSecretNo = null ; 
                obj.FinaltermAdmitCardSecretNo = null ; 
                obj.IsPassed = false ; 
                obj.IsBlockResult = false ; 
                obj.BlockResultReason = null ; 
                obj.IsBlockRetake = false ; 
                obj.BlockRetakeReason = null ; 
                obj.IsBlockSuppleApply = false ; 
                obj.BlockSuppleApplyReason = null ; 
                obj.IsShowTranscriptAsNonCredit = false ; 
                obj.GradePoint = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
