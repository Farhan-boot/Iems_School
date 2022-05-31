using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Reporting.Admission.ReportFiles;
using EMS.Reporting.Admission.Source;
using Telerik.Reporting;

namespace EMS.Reporting.Admission
{
    public class AdmissionReports
    {

        public ReportSource GetSeatLabelByRoll(List<AdmUG_Applicant> applicants, AdmUG_ExamSetting exam,
            out string message)
        {
            message = null;
            dsApplicantSeatLableByRoll dataset = new dsApplicantSeatLableByRoll();


            decimal numberToIterate = (decimal) applicants.Count/3;
            var newRow = Math.Ceiling(numberToIterate);
            var lastRowColCount = applicants.Count%3;

            var rowNum = 0;
            for (int i = 0; i < newRow; i ++)
            {
                //[datasetfilename].[InsideTableName]Row= dataset.[InsideTableName].New[InsideTableName]Row();
                dsApplicantSeatLableByRoll.ApplicantSeatLableByRollRow row =
                    dataset.ApplicantSeatLableByRoll.NewApplicantSeatLableByRollRow();
                row.ExamName = exam.ShortName.ToUpper();

                try
                {
                    row.RollNo1 = applicants[rowNum + 0].AdmissionTestRollNumber.ToString().PadLeft(4, '0');

                    row.RollNo2 = applicants[rowNum + 1].AdmissionTestRollNumber.ToString().PadLeft(4, '0');

                    row.RollNo3 = applicants[rowNum + 2].AdmissionTestRollNumber.ToString().PadLeft(4, '0');
                    //Debug.WriteLine($"row {i+1}: {row.RollNo1} {row.RollNo2} {row.RollNo3}");
                }
                catch (Exception ex)
                {

                    Debug.WriteLine(ex.ToString());
                }

                dataset.ApplicantSeatLableByRoll.AddApplicantSeatLableByRollRow(row);
                rowNum += 3;
            }
            rptApplicantSeatLableByRoll creport = new rptApplicantSeatLableByRoll();
            creport.DataSource = dataset;

            return creport;

        }

        public ReportSource GetApplicantExamAttendanceSheetReport(List<AdmUG_Applicant> applicants,
            AdmUG_ExamSetting exam, out string message)
        {
            message = null;
            return null;
        }
    }


}
