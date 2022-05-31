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
    public partial class Aca_GradingPolicyOption
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Grade = "Grade";		            
		public const string Property_GradePoint = "GradePoint";		            
		public const string Property_Description = "Description";		            
		public const string Property_GradingPolicyId = "GradingPolicyId";		            
		public const string Property_ScoreStartLimit = "ScoreStartLimit";		            
		public const string Property_ScoreEndLimit = "ScoreEndLimit";		            
		public const string Property_LimitOperatorEnumId = "LimitOperatorEnumId";		            
        public const string Property_LimitOperator = "LimitOperator";
		public const string Property_SerNo = "SerNo";		            
		public const string Property_IsCountCredit = "IsCountCredit";		            
		public const string Property_IsCountGPA = "IsCountGPA";		            
		public const string Property_IsDisplayScore = "IsDisplayScore";		            
		public const string Property_IsIncomplete = "IsIncomplete";		            
		public const string Property_IsWithdrawn = "IsWithdrawn";		            
		public const string Property_IsContinuation = "IsContinuation";		            
              
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
      
     
     
      
        public static Aca_GradingPolicyOption GetNew(Int64 id=0)
        {
           Aca_GradingPolicyOption obj = new Aca_GradingPolicyOption();
               obj.Id = id;
                obj.Grade = string.Empty ; 
                obj.GradePoint = 0.0F ; 
                obj.Description = null ; 
                obj.GradingPolicyId = 0 ; 
                obj.ScoreStartLimit = 0.0F ; 
                obj.ScoreEndLimit = null ; 
                obj.LimitOperatorEnumId = 0 ; 
                obj.SerNo = 0 ; 
                obj.IsCountCredit = false ; 
                obj.IsCountGPA = false ; 
                obj.IsDisplayScore = false ; 
                obj.IsIncomplete = false ; 
                obj.IsWithdrawn = false ; 
                obj.IsContinuation = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
