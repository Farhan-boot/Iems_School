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
    public partial class Aca_OnlineExam
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_ProgramId = "ProgramId";		            
		public const string Property_StartTime = "StartTime";		            
		public const string Property_EndTime = "EndTime";		            
		public const string Property_HasNegativeMarking = "HasNegativeMarking";		            
		public const string Property_NegativeMarkPercentage = "NegativeMarkPercentage";		            
		public const string Property_IsOnlineExamActive = "IsOnlineExamActive";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_History = "History";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsActive = "IsActive";		            
		public const string Property_IsResultPublished = "IsResultPublished";		            
		public const string Property_TotalMarks = "TotalMarks";		            
		public const string Property_CurriculumCourseId = "CurriculumCourseId";		            
              
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
      
     
     
      
        public static Aca_OnlineExam GetNew(Int32 id=0)
        {
           Aca_OnlineExam obj = new Aca_OnlineExam();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.ProgramId = 0 ; 
                obj.StartTime = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.EndTime = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.HasNegativeMarking = false ; 
                obj.NegativeMarkPercentage = null ; 
                obj.IsOnlineExamActive = false ; 
                obj.Remark = null ; 
                obj.History = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = null ; 
                obj.LastChangedById = 1 ;
                obj.IsActive = false ; 
                obj.IsResultPublished = false ; 
                obj.TotalMarks = 0.0F ; 
                obj.CurriculumCourseId = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
