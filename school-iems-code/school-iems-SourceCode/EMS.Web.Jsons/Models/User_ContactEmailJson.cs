using System;
using System.Collections.Generic;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
    public partial class User_ContactEmailJson
    {
        #region Custom Variables

        #endregion
    }
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
    {
        private static void MapExtra(User_ContactEmail entity, Web.Jsons.Models.User_ContactEmailJson toJson)
        {

        }

        private static void MapExtra(Web.Jsons.Models.User_ContactEmailJson json, User_ContactEmail toEntity)
        {
            if (json.ContactEmail != null) toEntity.ContactEmail = json.ContactEmail.Trim().ToLower();
        }
    }
}
