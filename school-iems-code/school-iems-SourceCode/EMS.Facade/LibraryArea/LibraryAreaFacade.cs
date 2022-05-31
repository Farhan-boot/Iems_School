using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;

namespace EMS.Facade.Library
{
   public class LibraryAreaFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;
        public LibraryAreaFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
       {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
        }
        public BookFacade BookFacade
        {
            get
            {
                return new BookFacade(_emsDbContext, _profile);
            }
        }
        public BorrowerFacade BorrowerFacade
        {
            get
            {
                return new BorrowerFacade(_emsDbContext, _profile);
            }
        }
    }
}
