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
	public partial class HR_DepartmentJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String ShortName { get; set; }
          public Int32 DepartmentNo { get; set; }
          public String ParentDeptId { get; set; }
          public String Description { get; set; }
          public String Address { get; set; }
          public String MapEmbedCode { get; set; }
          public String Email { get; set; }
          public String Mobile { get; set; }
          public String Phone { get; set; }
          public String Fax { get; set; }
          public String Website { get; set; }
          public DateTime Founded { get; set; }
          public Int32 TypeEnumId { get; set; }
          public string Type { get; set; }
          public Int32 StatusEnumId { get; set; }
          public string Status { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Int32 Priority { get; set; }
          public String OrgId { get; set; }
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
	   
		public static void Map(this HR_Department entity, HR_DepartmentJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.DepartmentNo = entity.DepartmentNo ; 
			 toJson.ParentDeptId = entity.ParentDeptId.ToString() ; 
			 toJson.Description = entity.Description ; 
			 toJson.Address = entity.Address ; 
			 toJson.MapEmbedCode = entity.MapEmbedCode ; 
			 toJson.Email = entity.Email ; 
			 toJson.Mobile = entity.Mobile ; 
			 toJson.Phone = entity.Phone ; 
			 toJson.Fax = entity.Fax ; 
			 toJson.Website = entity.Website ; 
			 toJson.Founded = entity.Founded ; 
			 toJson.TypeEnumId = entity.TypeEnumId ; 
             if(entity.TypeEnumId!=null)
             toJson.Type = entity.Type.ToString().AddSpacesToSentence();
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.Priority = entity.Priority ; 
			 toJson.OrgId = entity.OrgId.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_DepartmentJson json, HR_Department toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
    			 toEntity.DepartmentNo=json.DepartmentNo;
                 toEntity.ParentDeptId = long.TryParse(json.ParentDeptId, out output) ? output : 0;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
                 toEntity.Address = json.Address?.Trim() ?? json.Address;
                 toEntity.MapEmbedCode = json.MapEmbedCode?.Trim() ?? json.MapEmbedCode;
                 toEntity.Email = json.Email?.Trim() ?? json.Email;
                 toEntity.Mobile = json.Mobile?.Trim() ?? json.Mobile;
                 toEntity.Phone = json.Phone?.Trim() ?? json.Phone;
                 toEntity.Fax = json.Fax?.Trim() ?? json.Fax;
                 toEntity.Website = json.Website?.Trim() ?? json.Website;
    			 toEntity.Founded=json.Founded;
    			 toEntity.TypeEnumId=json.TypeEnumId;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.Priority=json.Priority;
                 toEntity.OrgId = long.TryParse(json.OrgId, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_Department> entityList, IList<HR_DepartmentJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 HR_DepartmentJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new HR_DepartmentJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_DepartmentJson> jsonList, ICollection<HR_Department> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    HR_Department obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = HR_Department.GetNew();
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
