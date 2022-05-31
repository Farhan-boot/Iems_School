using System.Web.Http;
using EMS.DataAccess.Data;
using EMS.Facade;
using EMS.Framework.Attributes;
using EMS.Framework.Utils;

namespace EMS.Web.Framework.Bases.WebApi
{


    [EmsWebApiAuthorize(User_Account.UserTypeEnum.Employee)]
    public class EmployeeBaseApiController : BaseApiController
    {
        public EmployeeBaseApiController()
        {

        }
        protected TheFacade Facade { get { return new TheFacade(DbInstance, HttpUtil.Profile); } }
    }
}
