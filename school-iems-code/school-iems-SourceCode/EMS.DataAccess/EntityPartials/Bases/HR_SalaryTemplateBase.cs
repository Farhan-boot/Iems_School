﻿//File: Entity Partial Base
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace EMS.DataAccess.Data
{

    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
    public partial class HR_SalaryTemplate
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_IsAutoCalcSalaryFromAttend = "IsAutoCalcSalaryFromAttend";		            
		public const string Property_IsEnable = "IsEnable";		            
		public const string Property_History = "History";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_OrderNo = "OrderNo";		            
              
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
      
     
     
      
        public static HR_SalaryTemplate GetNew(Int32 id=0)
        {
           HR_SalaryTemplate obj = new HR_SalaryTemplate();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.IsAutoCalcSalaryFromAttend = false ; 
                obj.IsEnable = false ; 
                obj.History = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.OrderNo = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
