using System.Web.Http;
using EMS.DataAccess.Data;
using EMS.Facade;
using EMS.Framework.Objects;
using EMS.Framework.Utils;

namespace EMS.Web.Framework.Bases.WebApi
{
   public class PublicBaseApiController: BaseApiController
    {
       public PublicBaseApiController()
       {

        }
    
        protected TheFacade Facade { get {return new TheFacade(DbInstance, new UserProfile() { Name = "Anonymous", UserId = -1, UserName = "Anonymous" }); } }
    
       
        
    }
}
