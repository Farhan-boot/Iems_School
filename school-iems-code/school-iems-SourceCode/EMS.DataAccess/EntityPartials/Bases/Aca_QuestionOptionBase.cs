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
    public partial class Aca_QuestionOption
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_QuestionBankId = "QuestionBankId";		            
		public const string Property_Name = "Name";		            
		public const string Property_OptionImagePath = "OptionImagePath";		            
		public const string Property_IsCorrectAnswer = "IsCorrectAnswer";		            
		public const string Property_OrderId = "OrderId";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsActive = "IsActive";		            
              
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
      
     
     
      
        public static Aca_QuestionOption GetNew(Int32 id=0)
        {
           Aca_QuestionOption obj = new Aca_QuestionOption();
               obj.Id = id;
                obj.QuestionBankId = 0 ; 
                obj.Name = null ; 
                obj.OptionImagePath = null ; 
                obj.IsCorrectAnswer = false ; 
                obj.OrderId = 0 ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = null ; 
                obj.LastChangedById = 1 ;
                obj.IsActive = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
