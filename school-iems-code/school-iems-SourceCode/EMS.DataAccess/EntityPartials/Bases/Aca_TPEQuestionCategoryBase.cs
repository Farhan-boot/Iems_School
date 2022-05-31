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
    public partial class Aca_TPEQuestionCategory
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_LetterId = "LetterId";		            
		public const string Property_CateGoryTitle = "CateGoryTitle";		            
              
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
      
     
     
      
        public static Aca_TPEQuestionCategory GetNew(Int32 id=0)
        {
           Aca_TPEQuestionCategory obj = new Aca_TPEQuestionCategory();
               obj.Id = id;
                obj.LetterId = string.Empty ; 
                obj.CateGoryTitle = string.Empty ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
