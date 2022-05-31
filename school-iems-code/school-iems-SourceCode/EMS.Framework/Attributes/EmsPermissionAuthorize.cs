using System;
using System.Linq;
using System.Web.Mvc;
using EMS.Framework.Utils;

namespace EMS.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class EmsPermissionAuthorize : FilterAttribute, IAuthorizationFilter
    {
        private readonly int _permissionNo;


        public EmsPermissionAuthorize(int permissionNo)
        {
            _permissionNo = permissionNo;
            throw new NotImplementedException("Need To implement it properly, its has no security!");
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var contex = HttpUtil.DbContext;
            var obj =  contex.User_Account.SingleOrDefault(ac => ac.UserName.Equals(HttpUtil.Profile.UserName));

            //if (_type != obj.UserType)
            //    filterContext.Result = new HttpUnauthorizedResult();

        }
    }
}
