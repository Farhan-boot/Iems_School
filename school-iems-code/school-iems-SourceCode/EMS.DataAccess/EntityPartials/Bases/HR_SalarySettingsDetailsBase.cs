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
    public partial class HR_SalarySettingsDetails
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_SalarySettingsId = "SalarySettingsId";		            
		public const string Property_Name = "Name";		            
		public const string Property_SalaryTypeEnumId = "SalaryTypeEnumId";		            
        public const string Property_SalaryType = "SalaryType";
		public const string Property_CategoryTypeEnumId = "CategoryTypeEnumId";		            
        public const string Property_CategoryType = "CategoryType";
		public const string Property_OrderNo = "OrderNo";		            
		public const string Property_Value = "Value";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_ParentId = "ParentId";		            
              
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
      
     
     
      
        public static HR_SalarySettingsDetails GetNew(Int32 id=0)
        {
           HR_SalarySettingsDetails obj = new HR_SalarySettingsDetails();
               obj.Id = id;
                obj.SalarySettingsId = 0 ; 
                obj.Name = string.Empty ; 
                obj.SalaryTypeEnumId = 0 ; 
                obj.CategoryTypeEnumId = 0 ; 
                obj.OrderNo = 0.0F ; 
                obj.Value = 0.0F ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.ParentId = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
