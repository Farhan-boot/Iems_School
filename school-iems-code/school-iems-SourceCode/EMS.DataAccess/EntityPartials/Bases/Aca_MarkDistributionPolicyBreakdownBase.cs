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
    public partial class Aca_MarkDistributionPolicyBreakdown
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_ComponentId = "ComponentId";		            
		public const string Property_DefaultTotalMark = "DefaultTotalMark";		            
		public const string Property_ConvertPercentage = "ConvertPercentage";		            
		public const string Property_IsFixedPolicy = "IsFixedPolicy";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_SerNo = "SerNo";		            
              
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
      
     
     
      
        public static Aca_MarkDistributionPolicyBreakdown GetNew(Int64 id=0)
        {
           Aca_MarkDistributionPolicyBreakdown obj = new Aca_MarkDistributionPolicyBreakdown();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.ComponentId = 0 ; 
                obj.DefaultTotalMark = 0.0F ; 
                obj.ConvertPercentage = 0.0F ; 
                obj.IsFixedPolicy = false ; 
                obj.Remark = null ; 
                obj.IsDeleted = false ; 
                obj.SerNo = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
