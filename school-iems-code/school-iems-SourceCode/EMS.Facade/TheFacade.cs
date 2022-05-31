using EMS.DataAccess.Data;
using EMS.Facade.Academic;
using EMS.Facade.AcademicArea;
using EMS.Facade.AdminArea;
using EMS.Facade.AdmissionArea;
using EMS.Facade.BasicAccountingArea;
using EMS.Facade.CommunicationLog;
using EMS.Facade.EmployeeArea;
using EMS.Facade.HrArea;
using EMS.Facade.StudentArea;

using EMS.Facade.Library;
using EMS.Facade.UserAccountSecurityArea;
//using EMS.Facade.Admission.UG;
//using EMS.Facade.Payment;
//using EMS.Facade.PreAdmission.UG;
using EMS.Framework.Objects;
using EMS.Framework.Utils;


namespace EMS.Facade
{

    public class TheFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;

        /// <summary>
        /// Be careful to use this method!!!!
        /// </summary>
        public TheFacade()
        {
            _emsDbContext = HttpUtil.DbContext;
            _profile = HttpUtil.Profile;
        }
        public TheFacade(EmsDbContext emsDbContext, UserProfile profile)
        {
            _emsDbContext = emsDbContext;
            _profile = profile;
        }

        public UserCredentialFacade UserCredentialFacade
        {
            get
            {
                return new UserCredentialFacade(_emsDbContext, _profile);
            }
        }
        public UserFacade UserFacade
        {
            get { return new UserFacade(_emsDbContext, _profile); }
        }
        public LibraryAreaFacade LibraryFacade
        {
            get { return new LibraryAreaFacade(_emsDbContext, _profile); }
        }
        public AcademicAreaFacade AcademicFacade
        {
            get { return new AcademicAreaFacade(_emsDbContext, _profile); }
        }


        public EmployeeAreaFacade EmployeeAreaFacade
        {
            get { return new EmployeeAreaFacade(_emsDbContext, _profile); }
        }
        public HrAreaFacade HrAreaFacade
        {
            get { return new HrAreaFacade(_emsDbContext, _profile); }
        }
        

            public SmsFacade SmsFacade
        {
            get { return new SmsFacade(_emsDbContext, _profile); }
        }

        public StudentAreaFacade StudentAreaFacade
        {
            get { return new StudentAreaFacade(_emsDbContext, _profile); }
        }
        public StudentAdmissionFacade StudentAdmissionFacade
        {
            get { return new StudentAdmissionFacade(_emsDbContext, _profile); }
        }

        public BasicAccountingFacade BasicAccountingFacade
        {
            get { return new BasicAccountingFacade(_emsDbContext, _profile); }
        }

        public UserAccountSecurityAreaFacade UserAccountSecurityAreaFacade
        {
            get { return new UserAccountSecurityAreaFacade(_emsDbContext, _profile); }
        }

    }
}
