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
	public partial class Aca_MarkDistributionPolicyComponentJson
	{
        #region Custom Variables

        public List<Aca_MarkDistributionPolicyBreakdownJson> Aca_MarkDistributionPolicyBreakdownJson { get; set; }
        #endregion
    }
    public static partial class EntityMapper
    {
        private static void MapExtra(Aca_MarkDistributionPolicyComponent entity, Aca_MarkDistributionPolicyComponentJson toJson)
        {
            toJson.Aca_MarkDistributionPolicyBreakdownJson = new List<Aca_MarkDistributionPolicyBreakdownJson>();
            if (entity.Aca_MarkDistributionPolicyBreakdown != null && entity.Aca_MarkDistributionPolicyBreakdown.Count > 0)
            {
                entity.Aca_MarkDistributionPolicyBreakdown.Map(toJson.Aca_MarkDistributionPolicyBreakdownJson);
            }
            toJson.IsSelected = entity.IsSelected;
            toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
        }

        private static void MapExtra(Aca_MarkDistributionPolicyComponentJson json, Aca_MarkDistributionPolicyComponent toEntity)
        {
            toEntity.Aca_MarkDistributionPolicyBreakdown = new List<Aca_MarkDistributionPolicyBreakdown>();
            if (json.Aca_MarkDistributionPolicyBreakdownJson != null &&
                json.Aca_MarkDistributionPolicyBreakdownJson.Count > 0)
            {
                json.Aca_MarkDistributionPolicyBreakdownJson.Map(toEntity.Aca_MarkDistributionPolicyBreakdown);
            }
            toEntity.IsSelected = json.IsSelected;
            toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
    }
}
