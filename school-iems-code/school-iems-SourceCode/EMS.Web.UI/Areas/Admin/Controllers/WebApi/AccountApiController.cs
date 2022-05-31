using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Facade;

using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{
    public class AccountApiController : EmployeeBaseApiController
    {
        private User_AccountDataService accountDataService = null;

        public AccountApiController()
        {
           // accountDataService =  new AccountDataService(DbInstance, HttpUtil.Profile);
        }
        // GET: api/AccountApi/5
        public HttpResult<User_AccountJson>   GetAccountById(int id = 0)
        {
            
            string error = string.Empty;
            HttpResult<User_AccountJson> result = new HttpResult<User_AccountJson>();
            User_AccountJson json = new User_AccountJson();
            User_Account entity;
            if (id <= 0)
            {
                entity = User_Account.GetNew();
            }
            else
            {
                entity = accountDataService.GetById(id);
                //List<Lib_BookSubject> subjects = entity.Lib_BookSubjectMap.Select(subjectMap => subjectMap.Lib_BookSubject).ToList();
                //entity.Lib_BookSubjectList = subjects;
                //List<Lib_BookAuthor> authors = entity.Lib_BookAuthorMap.Select(subjectMap => subjectMap.Lib_BookAuthor).ToList();
                //entity.Lib_BookAuthorList = authors;
                //entity.Map(json);
            }
            if (error == string.Empty)
            {
                //entity.Map(json);
                result.Data = json;
            }
            else
            {
                result.Errors.Add(error);
            }
            return result;
        }
        public string GetCourseDataExtra(long id=0)
        {
            return "value";
        }
        public string SaveCourse(long id = 0)
        {
            return "value";
        }
        public HttpListResult<Lib_BookJson> GetAccountList(string title, string barcode, int? pageSize, int? pageNo)
        {
            int count = 0;
            var jsons = new List<Lib_BookJson>();
            var entities = Facade.LibraryFacade.BookFacade.GetBookList(title, barcode, pageSize, pageNo, ref count);
            entities.Map(jsons);
            var result = new HttpListResult<Lib_BookJson>();
            result.Data = jsons;
            result.Count = count;
            return result;
        }
        //public List<AdmUG_ApplicantByRuleJson> Get()
        //{
        //    var entities = Facade.UgAdmissionFacade.GetCurrentApplicantByRuleList();
        //    var jsons = new List<AdmUG_ApplicantByRuleJson>();
        //    entities.Map(jsons);
        //    return jsons;
        //}
        public HttpResult<Aca_ClassJson> Post(Aca_ClassJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassJson>();
            var entity = new Aca_Class();
            json.Map(entity);
            if (Facade.AcademicFacade.ClassFacade.SaveClass(entity, out error))
            {
                entity.Map(json);
                result.Data = json;
            }
            else
            {
                result.HasError = true;
                result.Errors.Add(error);
            }
            return result;
        }


        // DELETE: api/AccountApi/5
        public void Delete(int id)
        {
        }
    }
}
