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
	public partial class HR_MonthlyPayslipJson 
	{
          public Int32 Id { get; set; }
          public Int32 EmployeeId { get; set; }
          public Int32 PayrollId { get; set; }
          public Int32 SalarySettingsId { get; set; }
          public Single TotalAddition { get; set; }
          public Single TotalDeduction { get; set; }
          public Boolean IsDraft { get; set; }
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
	   
		public static void Map(this HR_MonthlyPayslip entity, HR_MonthlyPayslipJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.EmployeeId = entity.EmployeeId ; 
			 toJson.PayrollId = entity.PayrollId ; 
			 toJson.SalarySettingsId = entity.SalarySettingsId ; 
			 toJson.TotalAddition = entity.TotalAddition ; 
			 toJson.TotalDeduction = entity.TotalDeduction ; 
			 toJson.IsDraft = entity.IsDraft ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_MonthlyPayslipJson json, HR_MonthlyPayslip toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.EmployeeId=json.EmployeeId;
    			 toEntity.PayrollId=json.PayrollId;
    			 toEntity.SalarySettingsId=json.SalarySettingsId;
    			 toEntity.TotalAddition=json.TotalAddition;
    			 toEntity.TotalDeduction=json.TotalDeduction;
    			 toEntity.IsDraft=json.IsDraft;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_MonthlyPayslip> entityList, IList<HR_MonthlyPayslipJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_MonthlyPayslipJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_MonthlyPayslipJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_MonthlyPayslipJson> jsonList, ICollection<HR_MonthlyPayslip> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_MonthlyPayslip obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_MonthlyPayslip.GetNew();
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
