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
    public partial class Aca_CreditTransfer
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_StudentId = "StudentId";		            
		public const string Property_PreviousUniversityId = "PreviousUniversityId";		            
		public const string Property_PreviousCGPA = "PreviousCGPA";		            
		public const string Property_CGPAAccepted = "CGPAAccepted";		            
		public const string Property_CreditAccepted = "CreditAccepted";		            
		public const string Property_FormerStudentId = "FormerStudentId";		            
		public const string Property_FormerProgramName = "FormerProgramName";		            
		public const string Property_FormerMajor = "FormerMajor";		            
		public const string Property_FormerMinor = "FormerMinor";		            
		public const string Property_IsApproved = "IsApproved";		            
		public const string Property_ApprovedById = "ApprovedById";		            
		public const string Property_IsCalculateWithCurrentCGPA = "IsCalculateWithCurrentCGPA";		            
		public const string Property_IsPrintCourcesInTranscript = "IsPrintCourcesInTranscript";		            
		public const string Property_Reason = "Reason";		            
		public const string Property_Remarks = "Remarks";		            
		public const string Property_OrderNo = "OrderNo";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsInternalDepartmentChange = "IsInternalDepartmentChange";		            
              
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
      
     
     
      
        public static Aca_CreditTransfer GetNew(Int32 id=0)
        {
           Aca_CreditTransfer obj = new Aca_CreditTransfer();
               obj.Id = id;
                obj.StudentId = 0 ; 
                obj.PreviousUniversityId = 0 ; 
                obj.PreviousCGPA = 0.0F ; 
                obj.CGPAAccepted = null ; 
                obj.CreditAccepted = 0.0F ; 
                obj.FormerStudentId = null ; 
                obj.FormerProgramName = string.Empty ; 
                obj.FormerMajor = null ; 
                obj.FormerMinor = null ; 
                obj.IsApproved = false ; 
                obj.ApprovedById = null ; 
                obj.IsCalculateWithCurrentCGPA = false ; 
                obj.IsPrintCourcesInTranscript = false ; 
                obj.Reason = null ; 
                obj.Remarks = null ; 
                obj.OrderNo = 0 ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsInternalDepartmentChange = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
