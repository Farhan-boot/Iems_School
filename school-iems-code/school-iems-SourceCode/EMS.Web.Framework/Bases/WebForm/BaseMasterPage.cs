using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EMS.DataAccess;
using EMS.DataAccess.Data;
using EMS.Facade;
using EMS.Framework.Utils;

namespace EMS.Web.Framework.Bases.WebForm
{
   public class BaseMasterPage : System.Web.UI.MasterPage
    {

        public string GetUrlParam(string name)
        {
            // return _URL[name];
            return HttpContext.Current.Request.Params[name];
        }

        protected EmsDbContext DbInstance { get; }
        public BaseMasterPage()
        {
            DbInstance = HttpUtil.DbContext;

        }
        protected TheFacade Facade { get { return new TheFacade(DbInstance, HttpUtil.Profile); } }


    }
}
