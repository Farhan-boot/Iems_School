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
	public partial class Invt_PurchaseJson
	{
        #region Custom Variables
        public List<Invt_PurchaseDetailJson> Invt_PurchaseDetailListJson { get; set; }
        public Invt_SupplierJson Invt_SupplierJson { get; set; }
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
		private static void MapExtra(Invt_Purchase entity, Invt_PurchaseJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;

             if (entity.Invt_PurchaseDetail != null)
             {
                 toJson.Invt_PurchaseDetailListJson = new List<Invt_PurchaseDetailJson>();
                 entity.Invt_PurchaseDetail.Map(toJson.Invt_PurchaseDetailListJson);
             }

             if (entity.Invt_Supplier != null)
             {
                 toJson.Invt_SupplierJson = new Invt_SupplierJson();
                 entity.Invt_Supplier.Map(toJson.Invt_SupplierJson);
             }

            
        }

        private static void MapExtra(Invt_PurchaseJson json, Invt_Purchase toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;

             if (json.Invt_PurchaseDetailListJson != null)
             {
                 toEntity.Invt_PurchaseDetail = new List<Invt_PurchaseDetail>();
                 json.Invt_PurchaseDetailListJson.Map(toEntity.Invt_PurchaseDetail);
             }
        }
	}
    #endregion
}
