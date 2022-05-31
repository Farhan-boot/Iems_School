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
	public partial class BAcnt_VoucherDetailJson 
	{
          public Int32 Id { get; set; }
          public Int32 LedgerId { get; set; }
          public Double DebitAmount { get; set; }
          public Double CreditAmount { get; set; }
          public Boolean IsDebited { get; set; }
          public Int32 VoucherId { get; set; }
          public String Remark { get; set; }
          public String History { get; set; }
          public Boolean IsDeleted { get; set; }
          public String Narration { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
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
	   
		public static void Map(this BAcnt_VoucherDetail entity, BAcnt_VoucherDetailJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.LedgerId = entity.LedgerId ; 
			 toJson.DebitAmount = entity.DebitAmount ; 
			 toJson.CreditAmount = entity.CreditAmount ; 
			 toJson.IsDebited = entity.IsDebited ; 
			 toJson.VoucherId = entity.VoucherId ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.History = entity.History ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.Narration = entity.Narration ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this BAcnt_VoucherDetailJson json, BAcnt_VoucherDetail toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.LedgerId=json.LedgerId;
    			 toEntity.DebitAmount=json.DebitAmount;
    			 toEntity.CreditAmount=json.CreditAmount;
    			 toEntity.IsDebited=json.IsDebited;
    			 toEntity.VoucherId=json.VoucherId;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.IsDeleted=json.IsDeleted;
                 toEntity.Narration = json.Narration?.Trim() ?? json.Narration;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<BAcnt_VoucherDetail> entityList, IList<BAcnt_VoucherDetailJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 BAcnt_VoucherDetailJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new BAcnt_VoucherDetailJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<BAcnt_VoucherDetailJson> jsonList, ICollection<BAcnt_VoucherDetail> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    BAcnt_VoucherDetail obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = BAcnt_VoucherDetail.GetNew();
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
