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
    public partial class Aca_ExamRoll
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_StudentId = "StudentId";		            
		public const string Property_ExamId = "ExamId";		            
		public const string Property_ExamRoll = "ExamRoll";		            
              
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
      
     
     
      
        public static Aca_ExamRoll GetNew(Int64 id=0)
        {
           Aca_ExamRoll obj = new Aca_ExamRoll();
               obj.Id = id;
                obj.StudentId = 0 ; 
                obj.ExamId = 0 ; 
                obj.ExamRoll = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
