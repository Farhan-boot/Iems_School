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
    public partial class Aca_HomeWork
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Title = "Title";		            
		public const string Property_DueTime = "DueTime";		            
		public const string Property_CloseTime = "CloseTime";		            
		public const string Property_GroupEnumId = "GroupEnumId";		            
        public const string Property_Group = "Group";
		public const string Property_HomeworkTypeEnumId = "HomeworkTypeEnumId";		            
        public const string Property_HomeworkType = "HomeworkType";
		public const string Property_HomeworkKey = "HomeworkKey";		            
		public const string Property_Description = "Description";		            
		public const string Property_TeacherId = "TeacherId";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_History = "History";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_ProgramId = "ProgramId";		            
		public const string Property_CourseId = "CourseId";		            
		public const string Property_Mark = "Mark";		            
		public const string Property_TeacherHomeworkTypeEnumId = "TeacherHomeworkTypeEnumId";		            
        public const string Property_TeacherHomeworkType = "TeacherHomeworkType";
              
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
      
     
     
      
        public static Aca_HomeWork GetNew(Int32 id=0)
        {
           Aca_HomeWork obj = new Aca_HomeWork();
               obj.Id = id;
                obj.Title = string.Empty ; 
                obj.DueTime = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CloseTime = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.GroupEnumId = null ; 
                obj.HomeworkTypeEnumId = 0 ; 
                obj.HomeworkKey = string.Empty ; 
                obj.Description = null ; 
                obj.TeacherId = 0 ; 
                obj.Remark = null ; 
                obj.History = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.ProgramId = 0 ; 
                obj.CourseId = 0 ; 
                obj.Mark = 0.0F ; 
                obj.TeacherHomeworkTypeEnumId = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
