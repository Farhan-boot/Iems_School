using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.ReportFacade;

namespace EMS.ReportFacade
{
     public class TheReoprtFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;

         public TheReoprtFacade( EmsDbContext emsDbContext, UserProfile profile)
         {
             _profile = profile;
             _emsDbContext = emsDbContext;
         }

        //public AdmissionReportFacade AdmissionReport
        //{ get { return new AdmissionReportFacade(_emsDbContext, _profile);} }
        //public PreAdmissionReportFacade PreAdmissionReport
        //{ get { return new PreAdmissionReportFacade(_emsDbContext, _profile); } }
        public LibraryReportFacade LibraryReport
        { get { return new LibraryReportFacade(_emsDbContext, _profile); } }
    }
}
