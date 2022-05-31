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
	public partial class HR_SalaryTemplateJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public Boolean IsAutoCalcSalaryFromAttend { get; set; }
          public Boolean IsEnable { get; set; }
          public String History { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public Int32 OrderNo { get; set; }
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
	   
		public static void Map(this HR_SalaryTemplate entity, HR_SalaryTemplateJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.IsAutoCalcSalaryFromAttend = entity.IsAutoCalcSalaryFromAttend ; 
			 toJson.IsEnable = entity.IsEnable ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.OrderNo = entity.OrderNo ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_SalaryTemplateJson json, HR_SalaryTemplate toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.IsAutoCalcSalaryFromAttend=json.IsAutoCalcSalaryFromAttend;
    			 toEntity.IsEnable=json.IsEnable;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.OrderNo=json.OrderNo;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_SalaryTemplate> entityList, IList<HR_SalaryTemplateJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_SalaryTemplateJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_SalaryTemplateJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_SalaryTemplateJson> jsonList, ICollection<HR_SalaryTemplate> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_SalaryTemplate obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_SalaryTemplate.GetNew();
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
