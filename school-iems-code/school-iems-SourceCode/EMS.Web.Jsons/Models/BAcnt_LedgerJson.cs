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
	public partial class BAcnt_LedgerJson
	{
	    #region Custom Variables
        public float ChildTotalDebit { get; set; }
        public float ChildTotalCredit { get; set; }
        public int OrderNo { get; set; }
        public string ParentName { get; set; }
        public string RowString { get; set; }

        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(BAcnt_Ledger entity, BAcnt_LedgerJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
             toJson.RowString = entity.RowString;
             toJson.ChildTotalDebit = entity.ChildTotalDebit;
             toJson.ChildTotalCredit = entity.ChildTotalCredit;

        }

        private static void MapExtra(BAcnt_LedgerJson json, BAcnt_Ledger toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}
