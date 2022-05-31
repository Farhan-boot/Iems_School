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
	public partial class Aca_MarkDistributionPolicyJson
	{
	    #region Custom Variables
       public  List<Aca_MarkDistributionPolicyComponentJson> Aca_MarkDistributionPolicyComponentJson { get; set; }
        #endregion
    }
    public static partial class EntityMapper
    {
        private static void MapExtra(Aca_MarkDistributionPolicy entity, Aca_MarkDistributionPolicyJson toJson)
        {
            toJson.Aca_MarkDistributionPolicyComponentJson = new List<Aca_MarkDistributionPolicyComponentJson>();
            if (entity.Aca_MarkDistributionPolicyComponent != null && entity.Aca_MarkDistributionPolicyComponent.Count > 0)
            {
                entity.Aca_MarkDistributionPolicyComponent.Map(toJson.Aca_MarkDistributionPolicyComponentJson);
            }
            toJson.IsSelected = entity.IsSelected;
            toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
        }

        private static void MapExtra(Aca_MarkDistributionPolicyJson json, Aca_MarkDistributionPolicy toEntity)
        {
            toEntity.Aca_MarkDistributionPolicyComponent = new List<Aca_MarkDistributionPolicyComponent>();
            if (json.Aca_MarkDistributionPolicyComponentJson != null &&
                json.Aca_MarkDistributionPolicyComponentJson.Count > 0)
            {
                json.Aca_MarkDistributionPolicyComponentJson.Map(toEntity.Aca_MarkDistributionPolicyComponent);
            }
            toEntity.IsSelected = json.IsSelected;
            toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
    }

}
