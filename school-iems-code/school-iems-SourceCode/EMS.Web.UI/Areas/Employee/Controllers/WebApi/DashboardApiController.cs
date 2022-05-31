using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;
using Newtonsoft.Json.Linq;

namespace EMS.Web.UI.Areas.Employee.Controllers.WebApi
{
    public class DashboardApiController : EmployeeBaseApiController
    {
        public HttpListResult<Aca_SemesterJson> GetFacultySemesterClassList()
        {

            var result = new HttpListResult<Aca_SemesterJson>();
            try
            {
               
               var json = new List<Aca_SemesterJson>();
                var semesterList = Facade.AcademicFacade.ClassFacade.GetSemesterListByTeacherIdTypeId(HttpUtil.Profile.EmployeeId);
                semesterList.Map(json);
                result.Data = json;
               
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }

        public HttpListResult<Aca_ClassJson> GetClassListBySemesterId(long id)
        {

            var result = new HttpListResult<Aca_ClassJson>();
            try
            {

                var json = new List<Aca_ClassJson>();
                var classList = Facade.AcademicFacade.ClassFacade.GetClassListByTeacherIdSemesterId(HttpUtil.Profile.EmployeeId, id);

                classList.Map(json);
                result.Data = json;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }

        //test code
        public HttpListResult<Aca_SemesterJson> GetDate(DateTime id)
        {

            var result = new HttpListResult<Aca_SemesterJson>();
            try
            {

                var json = new List<Aca_SemesterJson>();

                result.DataExtra.Date = DateTime.Now.AddDays(-1);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message.ToString());
            }
            return result;
        }
        //test code
        [HttpPost]
        public HttpListResult<Aca_SemesterJson> StopDate(object date)
        {

            var result = new HttpListResult<Aca_SemesterJson>();
            try
            {

                var json = new List<Aca_SemesterJson>();

                result.DataExtra.Date = DateTime.Now.AddDays(-1);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message.ToString());
            }
            return result;
        }
    }
}
