using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;
using Newtonsoft.Json.Linq;

namespace EMS.Web.UI.Areas.HR.Controllers.WebApi
{
    public class EmployeeClassApiController : EmployeeBaseApiController
    {
        public HttpListResult<Aca_SemesterJson> GetFacultySemesterClassListByEmpId(long id)
        {

            var result = new HttpListResult<Aca_SemesterJson>();

            string error = string.Empty;
          
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanView, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
               var json = new List<Aca_SemesterJson>();
                var semesterList = Facade.AcademicFacade.ClassFacade.GetSemesterListByTeacherIdTypeId(id);
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

        public HttpListResult<Aca_ClassJson> GetClassListBySemesterId(long empId,long semesterId)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanView, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {

                var json = new List<Aca_ClassJson>();
                var classList = Facade.AcademicFacade.ClassFacade.GetClassListByTeacherIdSemesterId(empId, semesterId);

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
    }
}
