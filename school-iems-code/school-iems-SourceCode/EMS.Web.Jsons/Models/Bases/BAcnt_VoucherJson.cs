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
	public partial class BAcnt_VoucherJson 
	{
          public Int32 Id { get; set; }
          public DateTime Date { get; set; }
          public String VoucherNo { get; set; }
          public Int32 VoucherTypeId { get; set; }
          public Byte VoucherStatusEnumId { get; set; }
          public string VoucherStatus { get; set; }
          public String Remark { get; set; }
          public String History { get; set; }
          public Boolean IsDeleted { get; set; }
          public String Narration { get; set; }
          public Byte JournalTypeEnumId { get; set; }
          public string JournalType { get; set; }
          public Nullable<Int32> BankId { get; set; }
          public String ManualSlipId { get; set; }
          public String ChequeNo { get; set; }
          public String ChequeDate { get; set; }
          public Int32 BranchId { get; set; }
          public Int32 CompanyId { get; set; }
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
	   
		public static void Map(this BAcnt_Voucher entity, BAcnt_VoucherJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Date = entity.Date ; 
			 toJson.VoucherNo = entity.VoucherNo ; 
			 toJson.VoucherTypeId = entity.VoucherTypeId ; 
			 toJson.VoucherStatusEnumId = entity.VoucherStatusEnumId ; 
             if(entity.VoucherStatusEnumId!=null)
             toJson.VoucherStatus = entity.VoucherStatus.ToString().AddSpacesToSentence();
			 toJson.Remark = entity.Remark ; 
			 toJson.History = entity.History ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.Narration = entity.Narration ; 
			 toJson.JournalTypeEnumId = entity.JournalTypeEnumId ; 
             if(entity.JournalTypeEnumId!=null)
             toJson.JournalType = entity.JournalType.ToString().AddSpacesToSentence();
			 toJson.BankId = entity.BankId ; 
			 toJson.ManualSlipId = entity.ManualSlipId ; 
			 toJson.ChequeNo = entity.ChequeNo ; 
			 toJson.ChequeDate = entity.ChequeDate ; 
			 toJson.BranchId = entity.BranchId ; 
			 toJson.CompanyId = entity.CompanyId ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this BAcnt_VoucherJson json, BAcnt_Voucher toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.Date=json.Date;
                 toEntity.VoucherNo = json.VoucherNo?.Trim() ?? json.VoucherNo;
    			 toEntity.VoucherTypeId=json.VoucherTypeId;
    			 toEntity.VoucherStatusEnumId=json.VoucherStatusEnumId;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.IsDeleted=json.IsDeleted;
                 toEntity.Narration = json.Narration?.Trim() ?? json.Narration;
    			 toEntity.JournalTypeEnumId=json.JournalTypeEnumId;
    			 toEntity.BankId=json.BankId;
                 toEntity.ManualSlipId = json.ManualSlipId?.Trim() ?? json.ManualSlipId;
                 toEntity.ChequeNo = json.ChequeNo?.Trim() ?? json.ChequeNo;
                 toEntity.ChequeDate = json.ChequeDate?.Trim() ?? json.ChequeDate;
    			 toEntity.BranchId=json.BranchId;
    			 toEntity.CompanyId=json.CompanyId;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<BAcnt_Voucher> entityList, IList<BAcnt_VoucherJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 BAcnt_VoucherJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new BAcnt_VoucherJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<BAcnt_VoucherJson> jsonList, ICollection<BAcnt_Voucher> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    BAcnt_Voucher obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = BAcnt_Voucher.GetNew();
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
