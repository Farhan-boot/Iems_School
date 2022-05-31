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
    public partial class Adm_AdmissionExam
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_ShortName = "ShortName";		            
		public const string Property_SessionName = "SessionName";		            
		public const string Property_StudentIdPrefix = "StudentIdPrefix";		            
		public const string Property_StudentIdSuffix = "StudentIdSuffix";		            
		public const string Property_ProgramTypeEnumId = "ProgramTypeEnumId";		            
        public const string Property_ProgramType = "ProgramType";
		public const string Property_CircularStatusEnumId = "CircularStatusEnumId";		            
        public const string Property_CircularStatus = "CircularStatus";
		public const string Property_SemesterId = "SemesterId";		            
		public const string Property_OnlineFormFillupStartDate = "OnlineFormFillupStartDate";		            
		public const string Property_OnlineFormFillupEndDate = "OnlineFormFillupEndDate";		            
		public const string Property_OnlineAdmitCardPublishDate = "OnlineAdmitCardPublishDate";		            
		public const string Property_OnlineAdmitCardLockDate = "OnlineAdmitCardLockDate";		            
		public const string Property_ExamDate = "ExamDate";		            
		public const string Property_ExamResultPublishDate = "ExamResultPublishDate";		            
		public const string Property_AdmissionStartDate = "AdmissionStartDate";		            
		public const string Property_AmissionFormsDownloadStartDate = "AmissionFormsDownloadStartDate";		            
		public const string Property_AmissionFormsDownloadEndDate = "AmissionFormsDownloadEndDate";		            
		public const string Property_CircularNoticeHtml = "CircularNoticeHtml";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_DefaultSettingsJson = "DefaultSettingsJson";		            
		public const string Property_IsEnable = "IsEnable";		            
		public const string Property_UgcIdPrefix = "UgcIdPrefix";		            
              
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
      
     
     
      
        public static Adm_AdmissionExam GetNew(Int32 id=0)
        {
           Adm_AdmissionExam obj = new Adm_AdmissionExam();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.ShortName = null ; 
                obj.SessionName = string.Empty ; 
                obj.StudentIdPrefix = string.Empty ; 
                obj.StudentIdSuffix = null ; 
                obj.ProgramTypeEnumId = 0 ; 
                obj.CircularStatusEnumId = 0 ; 
                obj.SemesterId = 0 ; 
                obj.OnlineFormFillupStartDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.OnlineFormFillupEndDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.OnlineAdmitCardPublishDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.OnlineAdmitCardLockDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.ExamDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.ExamResultPublishDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.AdmissionStartDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.AmissionFormsDownloadStartDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.AmissionFormsDownloadEndDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CircularNoticeHtml = null ; 
                obj.Remark = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.DefaultSettingsJson = null ; 
                obj.IsEnable = false ; 
                obj.UgcIdPrefix = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}
