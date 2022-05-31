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
	public partial class HR_PositionJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String ShortName { get; set; }
          public String LocalName { get; set; }
          public String AlternativeName { get; set; }
          public String AlternativeShortName { get; set; }
          public String Description { get; set; }
          public Int32 Priority { get; set; }
          public Byte JobClassEnumId { get; set; }
          public string JobClass { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public Nullable<Int32> SalaryTemplateId { get; set; }
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
	   
		public static void Map(this HR_Position entity, HR_PositionJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.LocalName = entity.LocalName ; 
			 toJson.AlternativeName = entity.AlternativeName ; 
			 toJson.AlternativeShortName = entity.AlternativeShortName ; 
			 toJson.Description = entity.Description ; 
			 toJson.Priority = entity.Priority ; 
			 toJson.JobClassEnumId = entity.JobClassEnumId ; 
             if(entity.JobClassEnumId!=null)
             toJson.JobClass = entity.JobClass.ToString().AddSpacesToSentence();
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.SalaryTemplateId = entity.SalaryTemplateId ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_PositionJson json, HR_Position toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
                 toEntity.LocalName = json.LocalName?.Trim() ?? json.LocalName;
                 toEntity.AlternativeName = json.AlternativeName?.Trim() ?? json.AlternativeName;
                 toEntity.AlternativeShortName = json.AlternativeShortName?.Trim() ?? json.AlternativeShortName;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
    			 toEntity.Priority=json.Priority;
    			 toEntity.JobClassEnumId=json.JobClassEnumId;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.SalaryTemplateId=json.SalaryTemplateId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_Position> entityList, IList<HR_PositionJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_PositionJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_PositionJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_PositionJson> jsonList, ICollection<HR_Position> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_Position obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_Position.GetNew();
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
