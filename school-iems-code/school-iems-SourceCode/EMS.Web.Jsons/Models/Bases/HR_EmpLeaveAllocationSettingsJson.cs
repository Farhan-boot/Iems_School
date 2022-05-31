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
	public partial class HR_EmpLeaveAllocationSettingsJson 
	{
          public Int32 Id { get; set; }
          public Int32 EmployeeId { get; set; }
          public Single TotalLeaveHour { get; set; }
          public Boolean IsCurrent { get; set; }
          public DateTime EffectiveFrom { get; set; }
          public DateTime EffectiveTill { get; set; }
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
	   
		public static void Map(this HR_EmpLeaveAllocationSettings entity, HR_EmpLeaveAllocationSettingsJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.EmployeeId = entity.EmployeeId ; 
			 toJson.TotalLeaveHour = entity.TotalLeaveHour ; 
			 toJson.IsCurrent = entity.IsCurrent ; 
			 toJson.EffectiveFrom = entity.EffectiveFrom ; 
			 toJson.EffectiveTill = entity.EffectiveTill ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_EmpLeaveAllocationSettingsJson json, HR_EmpLeaveAllocationSettings toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.EmployeeId=json.EmployeeId;
    			 toEntity.TotalLeaveHour=json.TotalLeaveHour;
    			 toEntity.IsCurrent=json.IsCurrent;
    			 toEntity.EffectiveFrom=json.EffectiveFrom;
    			 toEntity.EffectiveTill=json.EffectiveTill;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_EmpLeaveAllocationSettings> entityList, IList<HR_EmpLeaveAllocationSettingsJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_EmpLeaveAllocationSettingsJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_EmpLeaveAllocationSettingsJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_EmpLeaveAllocationSettingsJson> jsonList, ICollection<HR_EmpLeaveAllocationSettings> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_EmpLeaveAllocationSettings obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_EmpLeaveAllocationSettings.GetNew();
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
