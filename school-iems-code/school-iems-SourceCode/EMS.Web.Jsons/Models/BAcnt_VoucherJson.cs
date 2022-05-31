//File: Json Model Partial
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public partial class BAcnt_VoucherJson
	{
	    #region Custom Variables

        public List<BAcnt_VoucherDetailJson> BAcnt_VoucherDetailListJson { get; set; }
        public byte VoucherTypeEnumId { get; set; }
        public double VoucherTotalAmount { get; set; }
        public string VoucherType { get; set; }
        public string OfficialBankName { get; set; }
        public string BranchName { get; set; }
        public string CreateByName { get; set; }
        public string LastChangedByName { get; set; }
        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(BAcnt_Voucher entity, BAcnt_VoucherJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;

             if (entity.BAcnt_VoucherDetail!=null)
             {
                 toJson.BAcnt_VoucherDetailListJson = new List<BAcnt_VoucherDetailJson>();
                 entity.BAcnt_VoucherDetail.Map(toJson.BAcnt_VoucherDetailListJson);
                 toJson.VoucherTotalAmount = entity.BAcnt_VoucherDetail
                     .Where(x=>!x.IsDeleted && x.IsDebited)
                     .Sum(x => x.DebitAmount);
             }

             if (entity.BAcnt_VoucherType!=null)
             {
                 toJson.VoucherTypeEnumId = entity.BAcnt_VoucherType.TypeEnumId;
             }
             if (entity.BAcnt_VoucherType != null)
             {
                 toJson.VoucherTypeEnumId = entity.BAcnt_VoucherType.TypeEnumId;
                 toJson.VoucherType = entity.BAcnt_VoucherType.Name;
             }

             if (entity.BAcnt_Branch!=null)
             {
                 toJson.BranchName = entity.BAcnt_Branch.Name;
             }
             if (entity.Acnt_OfficialBank != null)
             {
                 toJson.OfficialBankName = entity.Acnt_OfficialBank.Name;
             }

        }

        private static void MapExtra(BAcnt_VoucherJson json, BAcnt_Voucher toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;

             json.BAcnt_VoucherDetailListJson?.Map(toEntity.BAcnt_VoucherDetail);
        }
	}
    #endregion
}
