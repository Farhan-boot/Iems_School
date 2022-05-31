using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;

namespace EMS.DataService
{
    public class BaseDataService : IDisposable
    {
        //public EmsDbContext DbInstance => DataManager.Instance;

        protected EmsDbContext DbInstance { get; }

        protected UserProfile Profile { get; }
        DbContextTransaction TransactionScope { get; }
        public BaseDataService(EmsDbContext emsDbContext, UserProfile usersProfile)
        {
            DbInstance = emsDbContext;
            Profile = usersProfile;
        }

        //public BaseDataService(EmsDbContext emsDbContext, UserProfile usersProfile, DbContextTransaction transactionScope)
        //{
        //    DbInstance = emsDbContext;
        //    Profile = usersProfile;
        //    TransactionScope = transactionScope;
        //}

        public void Dispose()
        {
        }
    }
}