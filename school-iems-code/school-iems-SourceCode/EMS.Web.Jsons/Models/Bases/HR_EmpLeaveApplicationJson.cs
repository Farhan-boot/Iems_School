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
	public partial class HR_EmpLeaveApplicationJson 
	{
          public Int32 Id { get; set; }
          public Int32 LeaveAllocationSettingsId { get; set; }
          public Int32 EmployeeId { get; set; }
          public Byte LeaveTypeEnumId { get; set; }
          public string LeaveType { get; set; }
          public DateTime LeaveFrom { get; set; }
          public DateTime LeaveTill { get; set; }
          public Single LeaveHour { get; set; }
          public String LeaveReason { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public Nullable<Int32> RecommendByEmployeeId { get; set; }
          public String Remarks { get; set; }
          public String History { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
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
	   
		public static void Map(this HR_EmpLeaveApplication entity, HR_EmpLeaveApplicationJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.LeaveAllocationSettingsId = entity.LeaveAllocationSettingsId ; 
			 toJson.EmployeeId = entity.EmployeeId ; 
			 toJson.LeaveTypeEnumId = entity.LeaveTypeEnumId ; 
             if(entity.LeaveTypeEnumId!=null)
             toJson.LeaveType = entity.LeaveType.ToString().AddSpacesToSentence();
			 toJson.LeaveFrom = entity.LeaveFrom ; 
			 toJson.LeaveTill = entity.LeaveTill ; 
			 toJson.LeaveHour = entity.LeaveHour ; 
			 toJson.LeaveReason = entity.LeaveReason ; 
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.RecommendByEmployeeId = entity.RecommendByEmployeeId ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_EmpLeaveApplicationJson json, HR_EmpLeaveApplication toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.LeaveAllocationSettingsId=json.LeaveAllocationSettingsId;
    			 toEntity.EmployeeId=json.EmployeeId;
    			 toEntity.LeaveTypeEnumId=json.LeaveTypeEnumId;
    			 toEntity.LeaveFrom=json.LeaveFrom;
    			 toEntity.LeaveTill=json.LeaveTill;
    			 toEntity.LeaveHour=json.LeaveHour;
                 toEntity.LeaveReason = json.LeaveReason?.Trim() ?? json.LeaveReason;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.RecommendByEmployeeId=json.RecommendByEmployeeId;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_EmpLeaveApplication> entityList, IList<HR_EmpLeaveApplicationJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_EmpLeaveApplicationJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_EmpLeaveApplicationJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_EmpLeaveApplicationJson> jsonList, ICollection<HR_EmpLeaveApplication> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_EmpLeaveApplication obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_EmpLeaveApplication.GetNew();
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
