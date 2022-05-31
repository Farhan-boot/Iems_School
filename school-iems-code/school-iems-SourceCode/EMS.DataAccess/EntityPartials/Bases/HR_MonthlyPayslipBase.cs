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
    public partial class HR_MonthlyPayslip
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_EmployeeId = "EmployeeId";		            
		public const string Property_PayrollId = "PayrollId";		            
		public const string Property_SalarySettingsId = "SalarySettingsId";		            
		public const string Property_TotalAddition = "TotalAddition";		            
		public const string Property_TotalDeduction = "TotalDeduction";		            
		public const string Property_IsDraft = "IsDraft";		            
		public const string Property_Remarks = "Remarks";		            
		public const string Property_History = "History";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
              
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
      
     
     
      
        public static HR_MonthlyPayslip GetNew(Int32 id=0)
        {
           HR_MonthlyPayslip obj = new HR_MonthlyPayslip();
               obj.Id = id;
                obj.EmployeeId = 0 ; 
                obj.PayrollId = 0 ; 
                obj.SalarySettingsId = 0 ; 
                obj.TotalAddition = 0.0F ; 
                obj.TotalDeduction = 0.0F ; 
                obj.IsDraft = false ; 
                obj.Remarks = null ; 
                obj.History = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
