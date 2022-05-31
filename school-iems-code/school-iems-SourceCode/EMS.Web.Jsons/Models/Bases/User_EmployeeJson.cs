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
	public partial class User_EmployeeJson 
	{
          public Int32 Id { get; set; }
          public Int32 UserId { get; set; }
          public Byte EmployeeCategoryEnumId { get; set; }
          public string EmployeeCategory { get; set; }
          public Byte EmployeeTypeEnumId { get; set; }
          public string EmployeeType { get; set; }
          public Byte JobClassEnumId { get; set; }
          public string JobClass { get; set; }
          public Byte JobTypeEnumId { get; set; }
          public string JobType { get; set; }
          public Byte WorkingStatusEnumId { get; set; }
          public string WorkingStatus { get; set; }
          public String TinNumber { get; set; }
          public String ImmediateSuperVisorId { get; set; }
          public Boolean IsAcademician { get; set; }
          public Int32 IncrementMonthEnumId { get; set; }
          public string IncrementMonth { get; set; }
          public Int32 JoiningDepartmentId { get; set; }
          public Nullable<Int32> PositionId { get; set; }
          public Boolean HasAdminAccess { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Nullable<Int32> SalaryTemplateId { get; set; }
          public Nullable<Int32> SalaryGradeEnumId { get; set; }
          public string SalaryGrade { get; set; }
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
	   
		public static void Map(this User_Employee entity, User_EmployeeJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.UserId = entity.UserId ; 
			 toJson.EmployeeCategoryEnumId = entity.EmployeeCategoryEnumId ; 
             if(entity.EmployeeCategoryEnumId!=null)
             toJson.EmployeeCategory = entity.EmployeeCategory.ToString().AddSpacesToSentence();
			 toJson.EmployeeTypeEnumId = entity.EmployeeTypeEnumId ; 
             if(entity.EmployeeTypeEnumId!=null)
             toJson.EmployeeType = entity.EmployeeType.ToString().AddSpacesToSentence();
			 toJson.JobClassEnumId = entity.JobClassEnumId ; 
             if(entity.JobClassEnumId!=null)
             toJson.JobClass = entity.JobClass.ToString().AddSpacesToSentence();
			 toJson.JobTypeEnumId = entity.JobTypeEnumId ; 
             if(entity.JobTypeEnumId!=null)
             toJson.JobType = entity.JobType.ToString().AddSpacesToSentence();
			 toJson.WorkingStatusEnumId = entity.WorkingStatusEnumId ; 
             if(entity.WorkingStatusEnumId!=null)
             toJson.WorkingStatus = entity.WorkingStatus.ToString().AddSpacesToSentence();
			 toJson.TinNumber = entity.TinNumber ; 
             if(entity.ImmediateSuperVisorId!=null)
			 toJson.ImmediateSuperVisorId = entity.ImmediateSuperVisorId.ToString() ; 
			 toJson.IsAcademician = entity.IsAcademician ; 
			 toJson.IncrementMonthEnumId = entity.IncrementMonthEnumId ; 
             if(entity.IncrementMonthEnumId!=null)
             toJson.IncrementMonth = entity.IncrementMonth.ToString().AddSpacesToSentence();
			 toJson.JoiningDepartmentId = entity.JoiningDepartmentId ; 
			 toJson.PositionId = entity.PositionId ; 
			 toJson.HasAdminAccess = entity.HasAdminAccess ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.SalaryTemplateId = entity.SalaryTemplateId ; 
			 toJson.SalaryGradeEnumId = entity.SalaryGradeEnumId ; 
             if(entity.SalaryGradeEnumId!=null)
             toJson.SalaryGrade = entity.SalaryGrade.ToString().AddSpacesToSentence();

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this User_EmployeeJson json, User_Employee toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.UserId=json.UserId;
    			 toEntity.EmployeeCategoryEnumId=json.EmployeeCategoryEnumId;
    			 toEntity.EmployeeTypeEnumId=json.EmployeeTypeEnumId;
    			 toEntity.JobClassEnumId=json.JobClassEnumId;
    			 toEntity.JobTypeEnumId=json.JobTypeEnumId;
    			 toEntity.WorkingStatusEnumId=json.WorkingStatusEnumId;
                 toEntity.TinNumber = json.TinNumber?.Trim() ?? json.TinNumber;
                 toEntity.ImmediateSuperVisorId = long.TryParse(json.ImmediateSuperVisorId, out output) ? output : (Int64?)null;
    			 toEntity.IsAcademician=json.IsAcademician;
    			 toEntity.IncrementMonthEnumId=json.IncrementMonthEnumId;
    			 toEntity.JoiningDepartmentId=json.JoiningDepartmentId;
    			 toEntity.PositionId=json.PositionId;
    			 toEntity.HasAdminAccess=json.HasAdminAccess;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.SalaryTemplateId=json.SalaryTemplateId;
    			 toEntity.SalaryGradeEnumId=json.SalaryGradeEnumId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<User_Employee> entityList, IList<User_EmployeeJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 User_EmployeeJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new User_EmployeeJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<User_EmployeeJson> jsonList, ICollection<User_Employee> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    User_Employee obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = User_Employee.GetNew();
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
