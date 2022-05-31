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
	public partial class BAcnt_LedgerJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String Code { get; set; }
          public String CodeName { get; set; }
          public Byte TypeEnumId { get; set; }
          public string Type { get; set; }
          public Nullable<Int32> ParentId { get; set; }
          public Boolean IsGroup { get; set; }
          public Nullable<Double> OpeningBalance { get; set; }
          public Nullable<DateTime> OpeningDate { get; set; }
          public String Remark { get; set; }
          public String History { get; set; }
          public Boolean IsDeleted { get; set; }
          public Int32 BranchId { get; set; }
          public Int32 CompanyId { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsBank { get; set; }
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
	   
		public static void Map(this BAcnt_Ledger entity, BAcnt_LedgerJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.Code = entity.Code ; 
			 toJson.CodeName = entity.CodeName ; 
			 toJson.TypeEnumId = entity.TypeEnumId ; 
             if(entity.TypeEnumId!=null)
             toJson.Type = entity.Type.ToString().AddSpacesToSentence();
			 toJson.ParentId = entity.ParentId ; 
			 toJson.IsGroup = entity.IsGroup ; 
			 toJson.OpeningBalance = entity.OpeningBalance ; 
			 toJson.OpeningDate = entity.OpeningDate ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.History = entity.History ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.BranchId = entity.BranchId ; 
			 toJson.CompanyId = entity.CompanyId ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsBank = entity.IsBank ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this BAcnt_LedgerJson json, BAcnt_Ledger toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.Code = json.Code?.Trim() ?? json.Code;
                 toEntity.CodeName = json.CodeName?.Trim() ?? json.CodeName;
    			 toEntity.TypeEnumId=json.TypeEnumId;
    			 toEntity.ParentId=json.ParentId;
    			 toEntity.IsGroup=json.IsGroup;
    			 toEntity.OpeningBalance=json.OpeningBalance;
    			 toEntity.OpeningDate=json.OpeningDate;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.BranchId=json.BranchId;
    			 toEntity.CompanyId=json.CompanyId;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsBank=json.IsBank;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<BAcnt_Ledger> entityList, IList<BAcnt_LedgerJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 BAcnt_LedgerJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new BAcnt_LedgerJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<BAcnt_LedgerJson> jsonList, ICollection<BAcnt_Ledger> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    BAcnt_Ledger obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = BAcnt_Ledger.GetNew();
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
