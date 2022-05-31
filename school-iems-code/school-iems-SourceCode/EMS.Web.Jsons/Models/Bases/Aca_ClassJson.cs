//File:Json Model Base Partial
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

using EMS.Framework;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
	public partial class Aca_ClassJson 
	{
          public String Id { get; set; }
          public Int32 ProgramId { get; set; }
          public Int32 DepartmentId { get; set; }
          public String CurriculumCourseId { get; set; }
          public String Code { get; set; }
          public String Name { get; set; }
          public Int32 ClassNo { get; set; }
          public Int32 ClassSectionId { get; set; }
          public Int32 StudyLevelTermId { get; set; }
          public String SemesterId { get; set; }
          public Byte ProgramTypeEnumId { get; set; }
          public string ProgramType { get; set; }
          public Int16 RegularCapacity { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public Nullable<Int32> CampusId { get; set; }
          public String RegularExamMarkDistributionPolicyId { get; set; }
          public String ReferredExamMarkDistributionPolicyId { get; set; }
          public String BacklogExamMarkDistributionPolicyId { get; set; }
          public Nullable<Int32> StudentBatchId { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public Boolean IsDasbleOnlineRegistration { get; set; }
          public String History { get; set; }
          public Boolean IsMidTermHardCopySubmitted { get; set; }
          public Boolean IsFinalTermHardCopySubmitted { get; set; }
          public Boolean IsBlockStudentPanelResult { get; set; }
          public Boolean IsSpecialUnlock { get; set; }
          public Nullable<DateTime> UnlockExpiryDate { get; set; }
          public String LockUnlockHistory { get; set; }
          public Boolean FacultyCanAddRoutine { get; set; }
          public Boolean FacultyCanEditRoutine { get; set; }
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public bool IsAlreadyAdded { get; set; }
	}
    #region Mapper
     /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
	public static partial class EntityMapper
	{
	   
		public static void Map(this Aca_Class entity, Aca_ClassJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.ProgramId = entity.ProgramId ; 
			 toJson.DepartmentId = entity.DepartmentId ; 
			 toJson.CurriculumCourseId = entity.CurriculumCourseId.ToString() ; 
			 toJson.Code = entity.Code ; 
			 toJson.Name = entity.Name ; 
			 toJson.ClassNo = entity.ClassNo ; 
			 toJson.ClassSectionId = entity.ClassSectionId ; 
			 toJson.StudyLevelTermId = entity.StudyLevelTermId ; 
			 toJson.SemesterId = entity.SemesterId.ToString() ; 
			 toJson.ProgramTypeEnumId = entity.ProgramTypeEnumId ; 
             if(entity.ProgramTypeEnumId!=null)
             toJson.ProgramType = entity.ProgramType.ToString().AddSpacesToSentence();
			 toJson.RegularCapacity = entity.RegularCapacity ; 
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.CampusId = entity.CampusId ; 
			 toJson.RegularExamMarkDistributionPolicyId = entity.RegularExamMarkDistributionPolicyId.ToString() ; 
             if(entity.ReferredExamMarkDistributionPolicyId!=null)
			 toJson.ReferredExamMarkDistributionPolicyId = entity.ReferredExamMarkDistributionPolicyId.ToString() ; 
             if(entity.BacklogExamMarkDistributionPolicyId!=null)
			 toJson.BacklogExamMarkDistributionPolicyId = entity.BacklogExamMarkDistributionPolicyId.ToString() ; 
			 toJson.StudentBatchId = entity.StudentBatchId ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.IsDasbleOnlineRegistration = entity.IsDasbleOnlineRegistration ; 
			 toJson.History = entity.History ; 
			 toJson.IsMidTermHardCopySubmitted = entity.IsMidTermHardCopySubmitted ; 
			 toJson.IsFinalTermHardCopySubmitted = entity.IsFinalTermHardCopySubmitted ; 
			 toJson.IsBlockStudentPanelResult = entity.IsBlockStudentPanelResult ; 
			 toJson.IsSpecialUnlock = entity.IsSpecialUnlock ; 
			 toJson.UnlockExpiryDate = entity.UnlockExpiryDate ; 
			 toJson.LockUnlockHistory = entity.LockUnlockHistory ; 
			 toJson.FacultyCanAddRoutine = entity.FacultyCanAddRoutine ; 
			 toJson.FacultyCanEditRoutine = entity.FacultyCanEditRoutine ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ClassJson json, Aca_Class toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.ProgramId=json.ProgramId;
    			 toEntity.DepartmentId=json.DepartmentId;
                 toEntity.CurriculumCourseId = long.TryParse(json.CurriculumCourseId, out output) ? output : 0;
                 toEntity.Code = json.Code?.Trim() ?? json.Code;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.ClassNo=json.ClassNo;
    			 toEntity.ClassSectionId=json.ClassSectionId;
    			 toEntity.StudyLevelTermId=json.StudyLevelTermId;
                 toEntity.SemesterId = long.TryParse(json.SemesterId, out output) ? output : 0;
    			 toEntity.ProgramTypeEnumId=json.ProgramTypeEnumId;
    			 toEntity.RegularCapacity=json.RegularCapacity;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.CampusId=json.CampusId;
                 toEntity.RegularExamMarkDistributionPolicyId = long.TryParse(json.RegularExamMarkDistributionPolicyId, out output) ? output : 0;
                 toEntity.ReferredExamMarkDistributionPolicyId = long.TryParse(json.ReferredExamMarkDistributionPolicyId, out output) ? output : (Int64?)null;
                 toEntity.BacklogExamMarkDistributionPolicyId = long.TryParse(json.BacklogExamMarkDistributionPolicyId, out output) ? output : (Int64?)null;
    			 toEntity.StudentBatchId=json.StudentBatchId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.IsDasbleOnlineRegistration=json.IsDasbleOnlineRegistration;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.IsMidTermHardCopySubmitted=json.IsMidTermHardCopySubmitted;
    			 toEntity.IsFinalTermHardCopySubmitted=json.IsFinalTermHardCopySubmitted;
    			 toEntity.IsBlockStudentPanelResult=json.IsBlockStudentPanelResult;
    			 toEntity.IsSpecialUnlock=json.IsSpecialUnlock;
    			 toEntity.UnlockExpiryDate=json.UnlockExpiryDate;
                 toEntity.LockUnlockHistory = json.LockUnlockHistory?.Trim() ?? json.LockUnlockHistory;
    			 toEntity.FacultyCanAddRoutine=json.FacultyCanAddRoutine;
    			 toEntity.FacultyCanEditRoutine=json.FacultyCanEditRoutine;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_Class> entityList, IList<Aca_ClassJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_ClassJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ClassJson> jsonList, ICollection<Aca_Class> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_Class obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_Class.GetNew();
                    json.Map(obj);
                    toEntityList.Add(obj);
                    
                }
                else
                {
                   json.Map(obj);
                }
            }
        }
	}
    #endregion
}
