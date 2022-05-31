using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Admission.Controllers.WebApi
{
    public class AdvanceSearchApiController : EmployeeBaseApiController
    {
        #region Advance search Models

        public class AdvanceSearchResultJson
        {
            public int StudentId { get; set; }
            public string Name { get; set; }
            public int ProgramName { get; set; }
        }

        #endregion
        public AdvanceSearchApiController()
        {

        }

        [HttpGet]
        public HttpListResult<AdvanceSearchResultJson> GetSuggestionById(int id)
        {
            var result = new HttpListResult<AdvanceSearchResultJson>();

            try
            {
                var suggestionList = DbInstance.User_Student.Include(x=>x.User_Account)
                    .Select(x => new AdvanceSearchResultJson()
                    {
                        StudentId = x.Id,
                        Name = x.User_Account.FullName,
                        ProgramName = x.ProgramId
                    }).Take(10).ToList();

                result.Data = suggestionList;
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Errors.Add(e.GetBaseException().Message);
            }
            return result;
        }
    }
}