using EMS.DataAccess.Data;
using EMS.Framework.Objects;

namespace EMS.Facade.HrArea
{
    public class HrAreaFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;
        public HrAreaFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
       {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
        }

        public EmployeeFacade OfferedCourseFacade
        {
            get
            {
                return new EmployeeFacade(_emsDbContext, _profile);
            }
        }
        public EmployeeFacade EmployeeFacade
        {
            get { return new EmployeeFacade(_emsDbContext, _profile); }
        }
        public PayrollFacade PayrollFacade
        {
            get { return new PayrollFacade(_emsDbContext, _profile); }
        }
    }
}
