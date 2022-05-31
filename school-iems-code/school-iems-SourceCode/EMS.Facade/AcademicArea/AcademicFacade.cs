using EMS.DataAccess.Data;
using EMS.Facade.Academic;
using EMS.Framework.Objects;

namespace EMS.Facade.AcademicArea
{
    public class AcademicAreaFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;
        public AcademicAreaFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
       {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
        }
        public StudentFacade StudentFacade
        {
            get
            {
                return new StudentFacade(_emsDbContext, _profile);
            }
        }
        //public CourseFacade CourseFacade
        //{
        //    get
        //    {
        //        return new CourseFacade(_emsDbContext, _profile);
        //    }
        //}
        public OfferedCourseFacade OfferedCourseFacade
        {
            get
            {
                return new OfferedCourseFacade(_emsDbContext, _profile);
            }
        }
        public CurriculumFacade CurriculumFacade
        {
            get
            {
                return new CurriculumFacade(_emsDbContext, _profile);
            }
        }
        public CurriculumCourseFacade CurriculumCourseFacade
        {
            get
            {
                return new CurriculumCourseFacade(_emsDbContext, _profile);
            }
        }
        public ClassFacade ClassFacade
        {
            get
            {
                return new ClassFacade(_emsDbContext, _profile);
            }
        }
        public SemesterFacade SemesterFacade
        {
            get
            {
                return new SemesterFacade(_emsDbContext, _profile);
            }
        }
        public ProgramFacade ProgramFacade
        {
            get
            {
                return new ProgramFacade(_emsDbContext, _profile);
            }
        }
        public MarkDistributionPolicyFacade MarkDistributionPolicyFacade
        {
            get { return new MarkDistributionPolicyFacade(_emsDbContext, _profile); }
        }

    }
}
