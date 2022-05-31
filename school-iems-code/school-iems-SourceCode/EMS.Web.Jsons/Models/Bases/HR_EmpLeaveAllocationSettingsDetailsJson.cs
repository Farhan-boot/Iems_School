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
	public partial class HR_EmpLeaveAllocationSettingsDetailsJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public Int32 EmpLeaveAllocationSettingsId { get; set; }
          public Byte LeaveTypeEnumId { get; set; }
          public string LeaveType { get; set; }
          public Int32 AllowedDays { get; set; }
          public Single WorkingHourPerDays { get; set; }
          public Single HourUsed { get; set; }
          public Boolean IsPaid { get; set; }
          public String Remarks { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
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
	   
		public static void Map(this HR_EmpLeaveAllocationSettingsDetails entity, HR_EmpLeaveAllocationSettingsDetailsJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.EmpLeaveAllocationSettingsId = entity.EmpLeaveAllocationSettingsId ; 
			 toJson.LeaveTypeEnumId = entity.LeaveTypeEnumId ; 
             if(entity.LeaveTypeEnumId!=null)
             toJson.LeaveType = entity.LeaveType.ToString().AddSpacesToSentence();
			 toJson.AllowedDays = entity.AllowedDays ; 
			 toJson.WorkingHourPerDays = entity.WorkingHourPerDays ; 
			 toJson.HourUsed = entity.HourUsed ; 
			 toJson.IsPaid = entity.IsPaid ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_EmpLeaveAllocationSettingsDetailsJson json, HR_EmpLeaveAllocationSettingsDetails toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.EmpLeaveAllocationSettingsId=json.EmpLeaveAllocationSettingsId;
    			 toEntity.LeaveTypeEnumId=json.LeaveTypeEnumId;
    			 toEntity.AllowedDays=json.AllowedDays;
    			 toEntity.WorkingHourPerDays=json.WorkingHourPerDays;
    			 toEntity.HourUsed=json.HourUsed;
    			 toEntity.IsPaid=json.IsPaid;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_EmpLeaveAllocationSettingsDetails> entityList, IList<HR_EmpLeaveAllocationSettingsDetailsJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_EmpLeaveAllocationSettingsDetailsJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_EmpLeaveAllocationSettingsDetailsJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_EmpLeaveAllocationSettingsDetailsJson> jsonList, ICollection<HR_EmpLeaveAllocationSettingsDetails> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_EmpLeaveAllocationSettingsDetails obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_EmpLeaveAllocationSettingsDetails.GetNew();
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
