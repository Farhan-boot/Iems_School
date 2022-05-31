using EMS.DataAccess.Data;
using EMS.Facade.HrArea;
using EMS.Framework.Objects;

namespace EMS.Facade.EmployeeArea
{
    public class EmployeeAreaFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;
        public EmployeeAreaFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
        }

        
      //public EmployeeFacade EmployeeFacade
      //  {
      //      get
      //      {
      //          return new EmployeeFacade(_emsDbContext, _profile);
      //      }
      //  }

    }
}
