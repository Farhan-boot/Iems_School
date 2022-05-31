using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;

namespace EMS.ReportFacade
{
    public class PreAdmissionReportFacade : BaseReportFacade
    {
        public PreAdmissionReportFacade(EmsDbContext emsDbContext, UserProfile usersProfile)
            : base(emsDbContext, usersProfile)
        {
        }
        private bool IsValidToPrintReport(AdmUG_Applicant objToSave, out string error)
        {
            error = null;

            //cant save if student not selected for admission
            if (objToSave.SelectedPassedInUnit == EnumCollection.AdmissionUG.FormUnitEnum.None )
            {
                error = "Sorry! you are not allowed to do this task.";
                return false;
            }
            //cant save if confirmd pre-registration
            if (objToSave.PreRegistrationStatus != EnumCollection.AdmissionUG.PreRegistrationStatusEnum.Confirmed)
            {
                error = "Sorry! you are not allowed to do this task.";
                return false;
            }
            return true;
        }
        private bool IsValidToPrintReport(AdmUG_ApplicantByRule objToSave, out string error)
        {
            error = null;
            //cant save if confirmd pre-registration
            if (objToSave.PreRegistrationStatus != EnumCollection.AdmissionUG.PreRegistrationStatusEnum.Confirmed)
            {
                error = "Sorry! you are not allowed to do this task.";
                return false;
            }
            return true;
        }

        public object GetFinalAdmissionFormsByApplicantIdForApplicant(long id, EnumCollection.AdmissionUG.ReportEnum retortType, EnumCollection.AdmissionApplicantTypeEnum typeId, out string message)
        {
            message = null;
            if (id != Profile.UserId)
            {
                message = "Invalid Permission";
                return null;
            }
            AdmUG_Applicant applicant = null;
            AdmUG_ApplicantByRule applicantByRule = null;
            if (typeId == EnumCollection.AdmissionApplicantTypeEnum.ApplicantByRule)
            {
                applicantByRule = DbInstance.AdmUG_ApplicantByRule.Include(p => p.AdmUG_DepartmentChoiceByRule).SingleOrDefault(x => x.Id == id);
            }
            else
            {
                applicant = DbInstance.AdmUG_Applicant.Include(p => p.AdmUG_DepartmentChoice).SingleOrDefault(x => x.Id == id);
            }

            if (applicant == null && applicantByRule == null)
            {
                message = "Invalid Applicant";
                return null;
            }
            
            object rept;
            if (typeId == EnumCollection.AdmissionApplicantTypeEnum.ApplicantByRule)
            {
                if (retortType != EnumCollection.AdmissionUG.ReportEnum.UgAdm006)
                {
                    if (!IsValidToPrintReport(applicantByRule, out message))
                        return null;
                }
                rept = GetFinalAdmissionFormsByApplicantId(applicantByRule, retortType, out message);
            }
            else
            {
                if (retortType != EnumCollection.AdmissionUG.ReportEnum.UgAdm006)
                {
                    if (!IsValidToPrintReport(applicant, out message))
                        return null;
                }
                rept = GetFinalAdmissionFormsByApplicantId(applicant, retortType, out message);
            }

            return rept;

        }

        public object GetFinalAdmissionFormsByApplicantIdForAdmin(long id, EnumCollection.AdmissionUG.ReportEnum retortType, EnumCollection.AdmissionApplicantTypeEnum typeId, out string message)
        {
            message = null;
            //check user permission
            //if (id != Profile.UserId)
            //{
            //    message = "Invalid Permission";
            //    return null;
            //}
            AdmUG_Applicant applicant = null;
            AdmUG_ApplicantByRule applicantByRule = null;
            if (typeId== EnumCollection.AdmissionApplicantTypeEnum.ApplicantByRule)
            {
                applicantByRule = DbInstance.AdmUG_ApplicantByRule.Include(p => p.AdmUG_DepartmentChoiceByRule).SingleOrDefault(x => x.Id == id);
            }
            else {
                applicant = DbInstance.AdmUG_Applicant.Include(p => p.AdmUG_DepartmentChoice).SingleOrDefault(x => x.Id == id);
            }
            
            if (applicant == null && applicantByRule == null)
            {
                message = "Invalid Applicant";
                return null;
            }
            object rept;
            if (typeId == EnumCollection.AdmissionApplicantTypeEnum.ApplicantByRule)
            {
                rept = GetFinalAdmissionFormsByApplicantId(applicantByRule, retortType, out message);
            }
            else
            {
                rept = GetFinalAdmissionFormsByApplicantId(applicant, retortType, out message);
            }
            
            return rept;

        }


        private object GetFinalAdmissionFormsByApplicantId(AdmUG_Applicant applicant, EnumCollection.AdmissionUG.ReportEnum retortType, out string message)
        {
            message = null;
            //AdmUG_Applicant applicant = Facade.UgAdmissionFacade.GetApplicantById(id); .Include(p=>p.AdmUG_DepartmentChoice)

            var dept = Facade.DepartmentManageFacade.GetDepartmentChoiceListByApplicant(applicant, out message);

            var applicantList =   new List<AdmUG_Applicant>();
            applicantList.Add(applicant);

            Telerik.Reporting.ReportSource report =
                    new EMS.Reporting.Admission.PreAdmission.PreAdmissionReports().GetFinalAdmissionForms(applicantList, dept, retortType,  out message);

            return report;

        }
        private object GetFinalAdmissionFormsByApplicantId(AdmUG_ApplicantByRule applicant, EnumCollection.AdmissionUG.ReportEnum retortType, out string message)
        {
            message = null;
            //AdmUG_Applicant applicant = Facade.UgAdmissionFacade.GetApplicantById(id); .Include(p=>p.AdmUG_DepartmentChoice)

            var dept = Facade.DepartmentManageByRuleFacade.GetDepartmentChoiceByRuleListByApplicantByRule(applicant, out message);

            var applicantList =   new List<AdmUG_ApplicantByRule>();
            applicantList.Add(applicant);

            Telerik.Reporting.ReportSource report =
                    new EMS.Reporting.Admission.PreAdmission.PreAdmissionReports().GetFinalAdmissionForms(applicantList, dept, retortType,  out message);

            return report;

        }
    }
}
