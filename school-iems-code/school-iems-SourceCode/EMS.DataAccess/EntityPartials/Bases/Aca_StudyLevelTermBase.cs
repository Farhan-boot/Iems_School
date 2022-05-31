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
    public partial class Aca_StudyLevelTerm
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_Name2 = "Name2";		            
		public const string Property_ShortName = "ShortName";		            
		public const string Property_LevelId = "LevelId";		            
		public const string Property_TermId = "TermId";		            
		public const string Property_SemesterDurationEnumId = "SemesterDurationEnumId";		            
        public const string Property_SemesterDuration = "SemesterDuration";
              
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
      
     
     
      
        public static Aca_StudyLevelTerm GetNew(Int32 id=0)
        {
           Aca_StudyLevelTerm obj = new Aca_StudyLevelTerm();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.Name2 = string.Empty ; 
                obj.ShortName = string.Empty ; 
                obj.LevelId = 0 ; 
                obj.TermId = 0 ; 
                obj.SemesterDurationEnumId = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
