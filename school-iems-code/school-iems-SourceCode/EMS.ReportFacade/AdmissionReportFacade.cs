using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.ReportFacade;
using Telerik.Reporting;

namespace EMS.ReportFacade
{
    public class AdmissionReportFacade : BaseReportFacade
    {
        public AdmissionReportFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }
        // public Telerik.Reporting.ReportSource Report { get; private set; }
        public object GetSeatLableByRollByExam(AdmUG_ExamSetting examSettings, out string message)
        {
            message = "";
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionModule.Undergraduate.Applicant.CanView,out message))
            //{
            //    message = "You are not allowed to view this report!";
            //    return null;
            //}
            try
            {
                // throw new Exception("test exception");
                if (examSettings == null)
                {
                    message = "Please select valid exam to generate this report!";
                    return null;
                }
                //examSettings = Facade.UgAdmissionFacade.GetCurrentExamSetting();
                List<AdmUG_Applicant> validApplicants =
                    Facade.UgAdmissionFacade.GetApplicantListApearedInExamByExamId(examSettings.Id);

                Telerik.Reporting.ReportSource report =
                    new EMS.Reporting.Admission.AdmissionReports().GetSeatLabelByRoll(validApplicants, examSettings, out message);

                return report;
            }
            catch (Exception ex)
            {
                message = "Oops Something went wrong!" + ex.ToString();
            }
            return null;
        }



    }
}
