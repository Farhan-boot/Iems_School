using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using HIMS.UMS.Entities;
using HIMS.UMS.Objects;
using HIMS.UMS.Reports.AdmissionReport.ReportFiles;
using HIMS.UMS.Reports.AdmissionReport.Source;
using HIMS.UMS.Reports.Properties;


namespace HIMS.UMS.Reports.AdmissionReport
{
    public class AdmissionReports : BaseReport
    {

        public rptApplicantCount GetApplicantSummeryReport(List<ProgramCount> pcList, AdmissionExam exam,
                                                           string printedBy)
        {
            var dataset = new dsProgramsTotalFormSaleCount();
            foreach (ProgramCount pc in pcList)
            {
                var row = dataset.ProgramsCount.NewProgramsCountRow();
                row.ExamSlot = exam.Title;
                row.ID = pc.Major.ID;
                row.Title = pc.Major.AdmissionProgramTitle;
                row.IsGraduate = pc.Major.EducationLevel2 == Major.EducationLevelEnum.Graduate;
                row.TotalSold = pc.TotalFormSold;
                row.TotalSubmitted = pc.TotalFormSubmitted;
                row.Type = pc.Major.EducationLevel2 == Major.EducationLevelEnum.Graduate ? "Graduate" : "Undergraduate";
                row.PrintedBy = printedBy;
                row.ExamDate = exam.ExamDate.HasValue ? exam.ExamDate.Value.ToString("dd-MMM-yyyy") : "N/A";
                dataset.ProgramsCount.AddProgramsCountRow(row);
            }

            var rpt = new rptApplicantCount {DataSource = dataset};
            return rpt;
        }

        public rptAdmissionAccountsOrReport GetAdmissionAccountsOrReport(DateTime startDate, DateTime endDate,
                                                                         IEnumerable<Applicant> applicants,
                                                                         string printedBy)
        {
            dsApplicants dataset = new dsApplicants();

            foreach (Applicant applicant in applicants)
            {
                dataset.Applicants.AddApplicantsRow(applicant.ID,
                                                    applicant.FullName.ToUpper(),
                                                    applicant.Roll,
                                                    applicant.ORNumber,
                                                    applicant.FormFee,
                                                    "E.F",
                                                    "Cash",
                                                    applicant.SellingDate.ToString("dd-MMM-yyyy"),
                                                    startDate.ToString("dd-MMM-yyyy"),
                                                    endDate.ToString("dd-MMM-yyyy"),
                                                    applicant.SoldBy.ToString(),
                                                    applicant.SoldByUserNameAndFullName,
                                                    printedBy,
                                                    applicant.ExamIDSource.Title);
            }

            rptAdmissionAccountsOrReport rpt = new rptAdmissionAccountsOrReport();
            rpt.DataSource = dataset;
            return rpt;
        }

        public rptApplicantsWithRemarks GetApplicantsWithRemarksReport(ObservableCollection<Applicant> applicants,
                                                                       string printedBy)
        {
            dsApplicantsWithRemarks dataset = new dsApplicantsWithRemarks();
            foreach (Applicant applicant in applicants)
            {
                string program = string.Empty;
                Major admissionProgram = applicant.AdmissionProgramIDSource;
                if (admissionProgram != null)
                {
                    program = admissionProgram.AdmissionProgramTitle;
                }

                string yesno = string.Empty;
                if (applicant.Submitted)
                {
                    yesno = "Yes";
                }
                else
                {
                    yesno = "No";
                }

                //dataset.ProgramsCount.AddProgramsCountRow(pc.ID, pc.Title, pc.IsGraduate, pc.TotalFormSold, pc.TotalFormSubmitted, _Application.NextAdmissionExam.Title, pc.IsGraduate ? "Graduate" : "Undergraduate");
                dataset.Applicants.AddApplicantsRow(applicant.ID.ToString(), applicant.Roll, applicant.FullName, program,
                                                    yesno, applicant.SellingDate.Date.ToString("dd-MMM-yyyy"),
                                                    applicant.Remarks, printedBy);
            }

            rptApplicantsWithRemarks rpt = new rptApplicantsWithRemarks();
            rpt.DataSource = dataset;
            return rpt;
        }

        public rptApplicantsByExamReport GetApplicantsByExamReport(List<ApplicantInfo> applicantInfos,
                                                                   List<AdmissionResultProgramMap>
                                                                       admissionResultProgramMaps,List<Major> majors, AdmissionExam exam,
                                                                   string printedBy)
        {
            var dataset = new dsApplicantsByExam();
            foreach (ApplicantInfo applicantInfo in applicantInfos)
            {
                var row = dataset.Applicants.NewApplicantsRow();
                row.Roll = applicantInfo.Roll;
                row.FullName = applicantInfo.FullName;
                row.ExamTitle = exam.Title;
                row.HasSubmitted = Convert(applicantInfo.Submitted);

                row.WrittenPassed = Convert(applicantInfo.WrittenResult);
                row.VivaPassed = Convert(applicantInfo.VivaResult);
                int passenum = 0;
                if (applicantInfo.PassedTypeID != null)
                {
                    passenum = applicantInfo.PassedTypeID ?? 0;
                }
                row.Result = ((AdmissionResult.PassTypeEnum)passenum).ToString();   

                row.HasRegistered = Convert(applicantInfo.IsRegistered);
                row.AppliedProgram = applicantInfo.AdmissionProgramTitle;

                List<AdmissionResultProgramMap> arpMaps =
                    admissionResultProgramMaps.Where(mp => mp.ApplicantID == applicantInfo.ID).ToList();

                string passpro = string.Empty;
                foreach (AdmissionResultProgramMap admissionResultProgramMap in arpMaps)
                {
                    Major m = majors.SingleOrDefault(mj => mj.ID == admissionResultProgramMap.ProgramID);
                    if (m != null)
                        passpro += m.AdmissionProgramTitle + ", ";
                }
                row.PassedProgram = passpro;
                row.Phone = applicantInfo.Phone1+" " + applicantInfo.Phone2;

                row.PrintedBy = printedBy;
                dataset.Applicants.AddApplicantsRow(row);
            }

            var rpt = new rptApplicantsByExamReport {DataSource = dataset};
            return rpt;
        }

        public static string Convert(object b)
        {
            if (b == null)
                return "";

            if (((bool)b))
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }
    }

    ////if (_selectedUser.ID == -1)
    ////    {
    ////        //Generate for all
    ////        e.Result = new AccountReportManager().GetORReport(_startDate, _endDate);
    ////    }
    ////    else
    ////    {
    ////        e.Result = new AccountReportManager().GetORReport(_selectedUser.ID, _startDate, _endDate);
    ////    }}

    //public ReportClass GetORReport(int userId, DateTime startDate, DateTime endDate)
    //{
    //    using (AdmissionService.AdmissionService service = ServiceProxyFactory.AdmissionService)
    //    {
    //        List<Applicant> app = GlobalFunctions.ConvertToApplicants(service.GetApplicantsListBySoldBy_Date(userId, startDate, endDate).Results);

    //        dsApplicants dataset = new dsApplicants();

    //        dataset.AdmissionExam.AddAdmissionExamRow(_Application.NextAdmissionExam.ID, _Application.NextAdmissionExam.Title, startDate.Date, endDate.Date);

    //        foreach (Applicant apli in app)
    //        {
    //            dsApplicants.ApplicantsRow row = dataset.Applicants.NewApplicantsRow();
    //            row.ID = apli.ID;
    //            row.FullName = apli.FullName;
    //            row.Roll = apli.Roll;
    //            row.Exam = apli.ExamID;
    //            row.ORNumber = apli.ORNumber;
    //            row.Amount = apli.FormFee;
    //            row.HeadOfAC_CodeNo = "E.F";
    //            row.Remarks = "Cash";

    //            dataset.Applicants.AddApplicantsRow(row);
    //        }

    //        crORReport report = new crORReport();
    //        report.SetDataSource(dataset);
    //        return report;
    //    }
    //}

    //public ReportClass GetORReport(DateTime startDate, DateTime endDate)
    //{
    //    using (AdmissionService.AdmissionService service = ServiceProxyFactory.AdmissionService)
    //    {
    //        List<Applicant> app = GlobalFunctions.ConvertToApplicants(service.GetApplicantsByFormSellingDate(startDate, endDate).Results);

    //        dsApplicants dataset = new dsApplicants();

    //        dataset.AdmissionExam.AddAdmissionExamRow(_Application.NextAdmissionExam.ID, _Application.NextAdmissionExam.Title, startDate.Date, endDate.Date);

    //        foreach (Applicant apli in app)
    //        {
    //            dsApplicants.ApplicantsRow row = dataset.Applicants.NewApplicantsRow();
    //            row.ID = apli.ID;
    //            row.FullName = apli.FullName;
    //            row.Roll = apli.Roll;
    //            row.Exam = apli.ExamID;
    //            row.ORNumber = apli.ORNumber;
    //            row.Amount = apli.FormFee;
    //            row.HeadOfAC_CodeNo = "E.F";
    //            row.Remarks = "Cash";

    //            dataset.Applicants.AddApplicantsRow(row);
    //        }

    //        crORReport report = new crORReport();
    //        report.SetDataSource(dataset);
    //        return report;
    //    }
    //}


}
