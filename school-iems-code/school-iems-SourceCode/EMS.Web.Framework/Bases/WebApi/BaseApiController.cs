using System.Web.Http;
using EMS.DataAccess;
using EMS.DataAccess.Data;
using EMS.Facade;
using EMS.Framework.Attributes;
using EMS.Framework.Utils;

namespace EMS.Web.Framework.Bases.WebApi
{
#if DEBUG
    //Console.WriteLine("Mode=Debug"); 
#else
    //Console.WriteLine("Mode=Release"); 
     
#endif
    //[ValidateWebApiAntiForgery]
    public class BaseApiController: ApiController
    {
        protected EmsDbContext DbInstance { get; }
        public BaseApiController()
        {
            DbInstance = HttpUtil.DbContext;
        }

    }
}
