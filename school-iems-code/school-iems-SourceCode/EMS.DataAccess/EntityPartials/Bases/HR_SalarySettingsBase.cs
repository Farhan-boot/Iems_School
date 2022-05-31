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
    public partial class HR_SalarySettings
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_SalaryTypeEnumId = "SalaryTypeEnumId";		            
        public const string Property_SalaryType = "SalaryType";
		public const string Property_EmployeeId = "EmployeeId";		            
		public const string Property_EffectiveFrom = "EffectiveFrom";		            
		public const string Property_IsCurrent = "IsCurrent";		            
		public const string Property_IsExcludedFromAutoGeneration = "IsExcludedFromAutoGeneration";		            
		public const string Property_History = "History";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_NextPromotionDate = "NextPromotionDate";		            
              
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
      
     
     
      
        public static HR_SalarySettings GetNew(Int32 id=0)
        {
           HR_SalarySettings obj = new HR_SalarySettings();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.SalaryTypeEnumId = 0 ; 
                obj.EmployeeId = 0 ; 
                obj.EffectiveFrom = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.IsCurrent = false ; 
                obj.IsExcludedFromAutoGeneration = false ; 
                obj.History = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.NextPromotionDate = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
