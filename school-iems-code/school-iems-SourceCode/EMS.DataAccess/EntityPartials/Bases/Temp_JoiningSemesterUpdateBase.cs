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
    public partial class Temp_JoiningSemesterUpdate
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ProgramId = "ProgramId";		            
		public const string Property_ProgramSTitle = "ProgramSTitle";		            
		public const string Property_StartAdmSemesterIdFor6M = "StartAdmSemesterIdFor6M";		            
		public const string Property_EndAdmSemesterIdFor6M = "EndAdmSemesterIdFor6M";		            
		public const string Property_StartAdmSemesterIdFor4M = "StartAdmSemesterIdFor4M";		            
		public const string Property_EndAdmSemesterIdFor4M = "EndAdmSemesterIdFor4M";		            
              
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
      
     
     
      
        public static Temp_JoiningSemesterUpdate GetNew(Int32 id=0)
        {
           Temp_JoiningSemesterUpdate obj = new Temp_JoiningSemesterUpdate();
               obj.Id = id;
                obj.ProgramId = 0 ; 
                obj.ProgramSTitle = string.Empty ; 
                obj.StartAdmSemesterIdFor6M = null ; 
                obj.EndAdmSemesterIdFor6M = null ; 
                obj.StartAdmSemesterIdFor4M = null ; 
                obj.EndAdmSemesterIdFor4M = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
