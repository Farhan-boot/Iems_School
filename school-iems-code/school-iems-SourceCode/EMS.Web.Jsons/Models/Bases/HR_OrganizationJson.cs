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
	public partial class HR_OrganizationJson 
	{
          public String Id { get; set; }
          public String Name { get; set; }
          public String ShortName { get; set; }
          public Int32 OrganizationNo { get; set; }
          public String Description { get; set; }
          public String Address { get; set; }
          public String ShortAddress { get; set; }
          public String MapEmbedCode { get; set; }
          public String Email { get; set; }
          public String Mobile { get; set; }
          public String Phone { get; set; }
          public String Fax { get; set; }
          public String Website { get; set; }
          public DateTime Founded { get; set; }
          public String LogoWithoutNameUrl { get; set; }
          public String LogoWithShortNameUrl { get; set; }
          public String LogoWithLongNameUrl { get; set; }
          public String LogoWithoutNameBlackWhiteUrl { get; set; }
          public String FeviconUrl { get; set; }
          public Byte TypeEnumId { get; set; }
          public string Type { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
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
	   
		public static void Map(this HR_Organization entity, HR_OrganizationJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Name = entity.Name ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.OrganizationNo = entity.OrganizationNo ; 
			 toJson.Description = entity.Description ; 
			 toJson.Address = entity.Address ; 
			 toJson.ShortAddress = entity.ShortAddress ; 
			 toJson.MapEmbedCode = entity.MapEmbedCode ; 
			 toJson.Email = entity.Email ; 
			 toJson.Mobile = entity.Mobile ; 
			 toJson.Phone = entity.Phone ; 
			 toJson.Fax = entity.Fax ; 
			 toJson.Website = entity.Website ; 
			 toJson.Founded = entity.Founded ; 
			 toJson.LogoWithoutNameUrl = entity.LogoWithoutNameUrl ; 
			 toJson.LogoWithShortNameUrl = entity.LogoWithShortNameUrl ; 
			 toJson.LogoWithLongNameUrl = entity.LogoWithLongNameUrl ; 
			 toJson.LogoWithoutNameBlackWhiteUrl = entity.LogoWithoutNameBlackWhiteUrl ; 
			 toJson.FeviconUrl = entity.FeviconUrl ; 
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

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this HR_OrganizationJson json, HR_Organization toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
    			 toEntity.OrganizationNo=json.OrganizationNo;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
                 toEntity.Address = json.Address?.Trim() ?? json.Address;
                 toEntity.ShortAddress = json.ShortAddress?.Trim() ?? json.ShortAddress;
                 toEntity.MapEmbedCode = json.MapEmbedCode?.Trim() ?? json.MapEmbedCode;
                 toEntity.Email = json.Email?.Trim() ?? json.Email;
                 toEntity.Mobile = json.Mobile?.Trim() ?? json.Mobile;
                 toEntity.Phone = json.Phone?.Trim() ?? json.Phone;
                 toEntity.Fax = json.Fax?.Trim() ?? json.Fax;
                 toEntity.Website = json.Website?.Trim() ?? json.Website;
    			 toEntity.Founded=json.Founded;
                 toEntity.LogoWithoutNameUrl = json.LogoWithoutNameUrl?.Trim() ?? json.LogoWithoutNameUrl;
                 toEntity.LogoWithShortNameUrl = json.LogoWithShortNameUrl?.Trim() ?? json.LogoWithShortNameUrl;
                 toEntity.LogoWithLongNameUrl = json.LogoWithLongNameUrl?.Trim() ?? json.LogoWithLongNameUrl;
                 toEntity.LogoWithoutNameBlackWhiteUrl = json.LogoWithoutNameBlackWhiteUrl?.Trim() ?? json.LogoWithoutNameBlackWhiteUrl;
                 toEntity.FeviconUrl = json.FeviconUrl?.Trim() ?? json.FeviconUrl;
    			 toEntity.TypeEnumId=json.TypeEnumId;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<HR_Organization> entityList, IList<HR_OrganizationJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new HR_OrganizationJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<HR_OrganizationJson> jsonList, ICollection<HR_Organization> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                HR_Organization obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = HR_Organization.GetNew();
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
