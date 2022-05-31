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
	public partial class Invt_PurchaseDetailJson
    {
        #region Custom Variables
        public Invt_ItemJson Invt_ItemJson { get; set; }
        //public Invt_PurchaseJson Invt_PurchaseJson { get; set; }
        public Invt_WarhouseJson Invt_WarhouseJson { get; set; }
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
		private static void MapExtra(Invt_PurchaseDetail entity, Invt_PurchaseDetailJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;

             if (entity.Invt_Item != null)
             {
                 toJson.Invt_ItemJson = new Invt_ItemJson();
                 entity.Invt_Item.Map(toJson.Invt_ItemJson);
             }
            //if (entity.Invt_Purchase != null)
            //{
            //    toJson.Invt_PurchaseJson = new Invt_PurchaseJson();
            //    entity.Invt_Purchase.Map(toJson.Invt_PurchaseJson);
            //}
            if (entity.Invt_Warhouse != null)
            {
                toJson.Invt_WarhouseJson = new Invt_WarhouseJson();
                entity.Invt_Warhouse.Map(toJson.Invt_WarhouseJson);
            }
        }

        private static void MapExtra(Invt_PurchaseDetailJson json, Invt_PurchaseDetail toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}
