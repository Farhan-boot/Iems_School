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
	public partial class Aca_CreditTransferJson
	{
        #region Custom Variables
        public string PreviousUniversity { get; set; }


        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(Aca_CreditTransfer entity, Aca_CreditTransferJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
             
            if (entity.Aca_CreditTransferInstitute != null)
            {
                toJson.PreviousUniversity = entity.Aca_CreditTransferInstitute.Name;
            }
            


        }

        private static void MapExtra(Aca_CreditTransferJson json, Aca_CreditTransfer toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}
