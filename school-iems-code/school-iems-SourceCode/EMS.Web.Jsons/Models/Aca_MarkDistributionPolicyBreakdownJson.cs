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
	public partial class Aca_MarkDistributionPolicyBreakdownJson
	{
        #region Custom Variables


        #endregion
    }
    public static partial class EntityMapper
    {
        private static void MapExtra(Aca_MarkDistributionPolicyBreakdown entity, Aca_MarkDistributionPolicyBreakdownJson toJson)
        {
            toJson.IsSelected = entity.IsSelected;
            toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
        }

        private static void MapExtra(Aca_MarkDistributionPolicyBreakdownJson json, Aca_MarkDistributionPolicyBreakdown toEntity)
        {
            toEntity.IsSelected = json.IsSelected;
            toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
    }
}
