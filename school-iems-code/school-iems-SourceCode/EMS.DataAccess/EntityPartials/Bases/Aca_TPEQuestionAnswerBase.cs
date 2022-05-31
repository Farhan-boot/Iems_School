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
    public partial class Aca_TPEQuestionAnswer
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_QuestionId = "QuestionId";		            
		public const string Property_AnswerTitle = "AnswerTitle";		            
		public const string Property_Rating = "Rating";		            
              
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
      
     
     
      
        public static Aca_TPEQuestionAnswer GetNew(Int32 id=0)
        {
           Aca_TPEQuestionAnswer obj = new Aca_TPEQuestionAnswer();
               obj.Id = id;
                obj.QuestionId = 0 ; 
                obj.AnswerTitle = string.Empty ; 
                obj.Rating = 0.0F ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
