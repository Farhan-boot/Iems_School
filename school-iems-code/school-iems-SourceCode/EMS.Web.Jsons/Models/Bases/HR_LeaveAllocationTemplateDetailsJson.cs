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
	public partial class HR_LeaveAllocationTemplateDetailsJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public Int32 SalaryTemplateId { get; set; }
          public Byte LeaveTypeEnumId { get; set; }
          public string LeaveType { get; set; }
          public Int32 AllowedDays { get; set; }
          public Single WorkingHourPerDays { get; set; }
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
	   
		public static void Map(this HR_LeaveAllocationTemplateDetails entity, HR_LeaveAllocationTemplateDetailsJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.SalaryTemplateId = entity.SalaryTemplateId ; 
			 toJson.LeaveTypeEnumId = entity.LeaveTypeEnumId ; 
             if(entity.LeaveTypeEnumId!=null)
             toJson.LeaveType = entity.LeaveType.ToString().AddSpacesToSentence();
			 toJson.AllowedDays = entity.AllowedDays ; 
			 toJson.WorkingHourPerDays = entity.WorkingHourPerDays ; 
			 toJson.IsPaid = entity.IsPaid ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_LeaveAllocationTemplateDetailsJson json, HR_LeaveAllocationTemplateDetails toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.SalaryTemplateId=json.SalaryTemplateId;
    			 toEntity.LeaveTypeEnumId=json.LeaveTypeEnumId;
    			 toEntity.AllowedDays=json.AllowedDays;
    			 toEntity.WorkingHourPerDays=json.WorkingHourPerDays;
    			 toEntity.IsPaid=json.IsPaid;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_LeaveAllocationTemplateDetails> entityList, IList<HR_LeaveAllocationTemplateDetailsJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_LeaveAllocationTemplateDetailsJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_LeaveAllocationTemplateDetailsJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_LeaveAllocationTemplateDetailsJson> jsonList, ICollection<HR_LeaveAllocationTemplateDetails> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_LeaveAllocationTemplateDetails obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_LeaveAllocationTemplateDetails.GetNew();
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
