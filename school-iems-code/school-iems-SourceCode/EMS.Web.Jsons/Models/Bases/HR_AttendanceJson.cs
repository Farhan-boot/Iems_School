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
	public partial class HR_AttendanceJson 
	{
          public Int32 Id { get; set; }
          public Int32 EmployeeId { get; set; }
          public Byte PunchTypeEnumId { get; set; }
          public string PunchType { get; set; }
          public DateTime PunchTime { get; set; }
          public Byte PunchModeEnumId { get; set; }
          public string PunchMode { get; set; }
          public Nullable<Int32> DeviceId { get; set; }
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
	   
		public static void Map(this HR_Attendance entity, HR_AttendanceJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.EmployeeId = entity.EmployeeId ; 
			 toJson.PunchTypeEnumId = entity.PunchTypeEnumId ; 
             if(entity.PunchTypeEnumId!=null)
             toJson.PunchType = entity.PunchType.ToString().AddSpacesToSentence();
			 toJson.PunchTime = entity.PunchTime ; 
			 toJson.PunchModeEnumId = entity.PunchModeEnumId ; 
             if(entity.PunchModeEnumId!=null)
             toJson.PunchMode = entity.PunchMode.ToString().AddSpacesToSentence();
			 toJson.DeviceId = entity.DeviceId ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_AttendanceJson json, HR_Attendance toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.EmployeeId=json.EmployeeId;
    			 toEntity.PunchTypeEnumId=json.PunchTypeEnumId;
    			 toEntity.PunchTime=json.PunchTime;
    			 toEntity.PunchModeEnumId=json.PunchModeEnumId;
    			 toEntity.DeviceId=json.DeviceId;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_Attendance> entityList, IList<HR_AttendanceJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_AttendanceJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_AttendanceJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_AttendanceJson> jsonList, ICollection<HR_Attendance> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_Attendance obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_Attendance.GetNew();
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
