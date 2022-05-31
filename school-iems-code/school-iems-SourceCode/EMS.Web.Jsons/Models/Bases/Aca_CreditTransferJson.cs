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
	public partial class Aca_CreditTransferJson 
	{
          public Int32 Id { get; set; }
          public Int32 StudentId { get; set; }
          public Int32 PreviousUniversityId { get; set; }
          public Single PreviousCGPA { get; set; }
          public Nullable<Single> CGPAAccepted { get; set; }
          public Single CreditAccepted { get; set; }
          public String FormerStudentId { get; set; }
          public String FormerProgramName { get; set; }
          public String FormerMajor { get; set; }
          public String FormerMinor { get; set; }
          public Boolean IsApproved { get; set; }
          public Nullable<Int32> ApprovedById { get; set; }
          public Boolean IsCalculateWithCurrentCGPA { get; set; }
          public Boolean IsPrintCourcesInTranscript { get; set; }
          public String Reason { get; set; }
          public String Remarks { get; set; }
          public Int32 OrderNo { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsInternalDepartmentChange { get; set; }
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
	   
		public static void Map(this Aca_CreditTransfer entity, Aca_CreditTransferJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.StudentId = entity.StudentId ; 
			 toJson.PreviousUniversityId = entity.PreviousUniversityId ; 
			 toJson.PreviousCGPA = entity.PreviousCGPA ; 
			 toJson.CGPAAccepted = entity.CGPAAccepted ; 
			 toJson.CreditAccepted = entity.CreditAccepted ; 
			 toJson.FormerStudentId = entity.FormerStudentId ; 
			 toJson.FormerProgramName = entity.FormerProgramName ; 
			 toJson.FormerMajor = entity.FormerMajor ; 
			 toJson.FormerMinor = entity.FormerMinor ; 
			 toJson.IsApproved = entity.IsApproved ; 
			 toJson.ApprovedById = entity.ApprovedById ; 
			 toJson.IsCalculateWithCurrentCGPA = entity.IsCalculateWithCurrentCGPA ; 
			 toJson.IsPrintCourcesInTranscript = entity.IsPrintCourcesInTranscript ; 
			 toJson.Reason = entity.Reason ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.OrderNo = entity.OrderNo ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsInternalDepartmentChange = entity.IsInternalDepartmentChange ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_CreditTransferJson json, Aca_CreditTransfer toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.StudentId=json.StudentId;
    			 toEntity.PreviousUniversityId=json.PreviousUniversityId;
    			 toEntity.PreviousCGPA=json.PreviousCGPA;
    			 toEntity.CGPAAccepted=json.CGPAAccepted;
    			 toEntity.CreditAccepted=json.CreditAccepted;
                 toEntity.FormerStudentId = json.FormerStudentId?.Trim() ?? json.FormerStudentId;
                 toEntity.FormerProgramName = json.FormerProgramName?.Trim() ?? json.FormerProgramName;
                 toEntity.FormerMajor = json.FormerMajor?.Trim() ?? json.FormerMajor;
                 toEntity.FormerMinor = json.FormerMinor?.Trim() ?? json.FormerMinor;
    			 toEntity.IsApproved=json.IsApproved;
    			 toEntity.ApprovedById=json.ApprovedById;
    			 toEntity.IsCalculateWithCurrentCGPA=json.IsCalculateWithCurrentCGPA;
    			 toEntity.IsPrintCourcesInTranscript=json.IsPrintCourcesInTranscript;
                 toEntity.Reason = json.Reason?.Trim() ?? json.Reason;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
    			 toEntity.OrderNo=json.OrderNo;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsInternalDepartmentChange=json.IsInternalDepartmentChange;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_CreditTransfer> entityList, IList<Aca_CreditTransferJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_CreditTransferJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_CreditTransferJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_CreditTransferJson> jsonList, ICollection<Aca_CreditTransfer> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_CreditTransfer obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_CreditTransfer.GetNew();
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
