using System.Web.Http;
using EMS.DataAccess.Data;
using EMS.Facade;
using EMS.Framework.Attributes;
using EMS.Framework.Utils;

namespace EMS.Web.Framework.Bases.WebApi
{


    [EmsWebApiAuthorize(User_Account.UserTypeEnum.Guardian)]
    public class GuardianApiController : BaseApiController
    {
        public GuardianApiController()
        {

        }
        protected TheFacade Facade { get { return new TheFacade(DbInstance, HttpUtil.Profile); } }
    }
}
