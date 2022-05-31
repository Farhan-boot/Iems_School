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
	public partial class Aca_ClassTakenByEmployeeJson 
	{
          public String Id { get; set; }
          public Int32 EmployeeId { get; set; }
          public String ClassId { get; set; }
          public Byte RoleEnumId { get; set; }
          public string Role { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public Byte SectionEnumId { get; set; }
          public string Section { get; set; }
          public String Remarks { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public String History { get; set; }
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
	   
		public static void Map(this Aca_ClassTakenByEmployee entity, Aca_ClassTakenByEmployeeJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.EmployeeId = entity.EmployeeId ; 
			 toJson.ClassId = entity.ClassId.ToString() ; 
			 toJson.RoleEnumId = entity.RoleEnumId ; 
             if(entity.RoleEnumId!=null)
             toJson.Role = entity.Role.ToString().AddSpacesToSentence();
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.SectionEnumId = entity.SectionEnumId ; 
             if(entity.SectionEnumId!=null)
             toJson.Section = entity.Section.ToString().AddSpacesToSentence();
			 toJson.Remarks = entity.Remarks ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.History = entity.History ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ClassTakenByEmployeeJson json, Aca_ClassTakenByEmployee toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.EmployeeId=json.EmployeeId;
                 toEntity.ClassId = long.TryParse(json.ClassId, out output) ? output : 0;
    			 toEntity.RoleEnumId=json.RoleEnumId;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.SectionEnumId=json.SectionEnumId;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;
                 toEntity.History = json.History?.Trim() ?? json.History;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_ClassTakenByEmployee> entityList, IList<Aca_ClassTakenByEmployeeJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_ClassTakenByEmployeeJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ClassTakenByEmployeeJson> jsonList, ICollection<Aca_ClassTakenByEmployee> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_ClassTakenByEmployee obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_ClassTakenByEmployee.GetNew();
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
