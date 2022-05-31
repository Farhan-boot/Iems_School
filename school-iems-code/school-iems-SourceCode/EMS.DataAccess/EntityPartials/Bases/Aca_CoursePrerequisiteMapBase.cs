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
    public partial class Aca_CoursePrerequisiteMap
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_CourseId = "CourseId";		            
		public const string Property_PrerequisiteCourseId = "PrerequisiteCourseId";		            
              
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
      
     
     
      
        public static Aca_CoursePrerequisiteMap GetNew(Int32 id=0)
        {
           Aca_CoursePrerequisiteMap obj = new Aca_CoursePrerequisiteMap();
               obj.Id = id;
                obj.CourseId = 0 ; 
                obj.PrerequisiteCourseId = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
