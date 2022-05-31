using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;

namespace EMS.Facade.Library
{
    public class BorrowerFacade   :BaseFacade
    {
        public BorrowerFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        public BookFacade BookFacade
        {
            get { return new BookFacade(DbInstance, Profile); }
        }



        

    }
}
