using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Utils;

namespace EMS.Facade
{
    public class BaseFacade : IDisposable
    {
        //public EmsDbContext DbInstance => DataManager.Instance;

        protected EmsDbContext DbInstance { get; }

        protected UserProfile Profile { get; }

        public BaseFacade(EmsDbContext emsDbContext, UserProfile usersProfile)
        {
            DbInstance = emsDbContext;
            Profile = usersProfile;
        }




        public void Dispose()
        {
        }
    }
}