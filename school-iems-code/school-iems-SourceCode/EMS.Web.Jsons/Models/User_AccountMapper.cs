//File: Json Partial Mapper
//DateUpdate: Sunday, June 26, 2016
using System;
using System.Collections.Generic;
using System.Linq;
using EMS.CoreLibrary.Helpers;
using EMS.Framework;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;


namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
    {
        const string DateFormat = "dd/MM/yyyy";
        private static void MapExtra(User_Account entity, User_AccountJson toJson)
        {
            toJson.DateOfBirthString = entity.DateOfBirth.ToString(DateFormat);

        }

        private static void MapExtra(User_AccountJson json, User_Account toEntity)
        {
            var defaultDate = DateTime.Now;
            var dobDate = DateTimeHelper.ToDateTime(json.DateOfBirthString);
            toEntity.DateOfBirth = dobDate ?? defaultDate;
            //toEntity.LeavingDate = json.IsGraduated ? json.DateGraduated : null;

            //var joiningDate = DateTimeHelper.ToDateTime(json.JoiningDateString);
            //var leavingDate = DateTimeHelper.ToDateTime(json.LeavingDateString);
            //toEntity.JoiningDate = joiningDate ?? defaultDate;
            //toEntity.LeavingDate = leavingDate;
        }
	}
}

