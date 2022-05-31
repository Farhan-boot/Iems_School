using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Facade;
using EMS.Framework.Objects;
using EMS.Framework.Utils;

namespace EMS.ReportFacade
{
    public class BaseReportFacade : IDisposable
    {
        //public EmsDbContext DbInstance => DataManager.Instance;

        protected EmsDbContext DbInstance { get; }

        protected UserProfile Profile { get; }
        protected TheFacade Facade { get { return new TheFacade(DbInstance, Profile); } }

        public BaseReportFacade(EmsDbContext emsDbContext, UserProfile usersProfile)
        {
            DbInstance = emsDbContext;
            Profile = usersProfile;
        }




        public void Dispose()
        {
        }
      
    }
}
