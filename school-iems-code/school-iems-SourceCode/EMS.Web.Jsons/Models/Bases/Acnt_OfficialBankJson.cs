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
	public partial class Acnt_OfficialBankJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String ShortName { get; set; }
          public String Code { get; set; }
          public String AcName { get; set; }
          public String AcNo { get; set; }
          public String BranchName { get; set; }
          public String Address { get; set; }
          public Byte TypeEnumId { get; set; }
          public string Type { get; set; }
          public Int32 OrderNo { get; set; }
          public Boolean IsActive { get; set; }
          public Boolean IsDeleted { get; set; }
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
	   
		public static void Map(this Acnt_OfficialBank entity, Acnt_OfficialBankJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.Code = entity.Code ; 
			 toJson.AcName = entity.AcName ; 
			 toJson.AcNo = entity.AcNo ; 
			 toJson.BranchName = entity.BranchName ; 
			 toJson.Address = entity.Address ; 
			 toJson.TypeEnumId = entity.TypeEnumId ; 
             if(entity.TypeEnumId!=null)
             toJson.Type = entity.Type.ToString().AddSpacesToSentence();
			 toJson.OrderNo = entity.OrderNo ; 
			 toJson.IsActive = entity.IsActive ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Acnt_OfficialBankJson json, Acnt_OfficialBank toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
                 toEntity.Code = json.Code?.Trim() ?? json.Code;
                 toEntity.AcName = json.AcName?.Trim() ?? json.AcName;
                 toEntity.AcNo = json.AcNo?.Trim() ?? json.AcNo;
                 toEntity.BranchName = json.BranchName?.Trim() ?? json.BranchName;
                 toEntity.Address = json.Address?.Trim() ?? json.Address;
    			 toEntity.TypeEnumId=json.TypeEnumId;
    			 toEntity.OrderNo=json.OrderNo;
    			 toEntity.IsActive=json.IsActive;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Acnt_OfficialBank> entityList, IList<Acnt_OfficialBankJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Acnt_OfficialBankJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Acnt_OfficialBankJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Acnt_OfficialBankJson> jsonList, ICollection<Acnt_OfficialBank> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Acnt_OfficialBank obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Acnt_OfficialBank.GetNew();
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
