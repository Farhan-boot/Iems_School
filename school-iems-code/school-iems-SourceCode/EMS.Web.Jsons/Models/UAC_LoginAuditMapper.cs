//File: Json Partial Mapper
using System;
using System.Collections.Generic;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;
using EMS.Framework.Utils;
using EMS.Web.Jsons.Models;


namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(UAC_LoginAudit entity, UAC_LoginAuditJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
            toJson.Password = string.Empty;
            var user = HttpUtil.DbContext.User_Account.Find(entity.UserId);
		    if (user != null)
		    {
                toJson.Password = user.FullName;
		    }
            
        }

        private static void MapExtra(UAC_LoginAuditJson json, UAC_LoginAudit toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
}

