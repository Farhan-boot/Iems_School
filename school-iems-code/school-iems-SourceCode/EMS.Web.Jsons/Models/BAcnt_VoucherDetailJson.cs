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
	public partial class BAcnt_VoucherDetailJson
	{
	    #region Custom Variables
        public string LedgerName { get; set; }
        public byte TransactionTypeEnumId { get; set; }
        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(BAcnt_VoucherDetail entity, BAcnt_VoucherDetailJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;

             if (entity.IsDebited)
             {
                 toJson.TransactionTypeEnumId =(byte) BAcnt_VoucherDetail.TransactionTypeEnum.Dr;
             }
             else
             {
                toJson.TransactionTypeEnumId = (byte)BAcnt_VoucherDetail.TransactionTypeEnum.Cr;
             }
        }

        private static void MapExtra(BAcnt_VoucherDetailJson json, BAcnt_VoucherDetail toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}
