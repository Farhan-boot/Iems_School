using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Utils;
using EMS.Models.JsonModel;
using EMS.Reporting.Admission.PreAdmission.ReportFiles;
using EMS.Reporting.Admission.ReportFiles;
using EMS.Reporting.Admission.Source;
using Telerik.Reporting;

namespace EMS.Reporting.Admission.PreAdmission
{
    public class PreAdmissionReports
    {
        public ReportSource GetFinalAdmissionForms(List<AdmUG_Applicant> applicants, List<Department> departments, EnumCollection.AdmissionUG.ReportEnum reportName, out string message)
        {
            message = null;
            var isRequiredGovtJobCertificate = false;

            dsUG_Applicant dataset = new dsUG_Applicant();
            ReportBook reportBook = new ReportBook();

            var rowNum = 0;
            foreach (var applicant in applicants)
            {
                string tik = "\u2714";
                string cross = "\u2716";
                dsUG_Applicant.UG_ApplicantRow row =
                    dataset.UG_Applicant.NewUG_ApplicantRow();
                row.AdmissionTestRollNumber = applicant.AdmissionTestRollNumber.ToStringPadLeft(4, '0');
                row.FromSerial = applicant.FormSerial.ToStringPadLeft(5, '0');
                row.UserId = applicant.UserId;
                row.AddressPermanentCareOf = applicant.AddressPermanentCareOf;
                row.AddressPermanentCareOf = applicant.AddressPermanentCareOf;
                row.PermanentAddress = applicant.AddressPermanent;
                row.PresentAddress = applicant.AddressPresent;
                row.PermanentAddress1 = "P.S: "+applicant.AddressPermanentPoliceStation + ", Dist: " + applicant.AddressPermanentDistrict + ", Post Code:" + applicant.AddressPermanentPostCode;
                row.PresentAddress1 = "P.S: " + applicant.AddressPresentPoliceStation + ", Dist: " + applicant.AddressPresentDistrict + ", Post Code:" + applicant.AddressPresentPostCode;
                
                row.AddressPermanent = applicant.AddressPermanentCareOf + ", " + applicant.AddressPermanent + ", " + applicant.AddressPermanentPoliceStation + ", " + applicant.AddressPermanentDistrict + "-" + applicant.AddressPermanentPostCode;
                row.AddressPresent = applicant.AddressPresentCareOf + ", " + applicant.AddressPresent + ", " + applicant.AddressPresentPoliceStation + ", " + applicant.AddressPresentDistrict + "-" + applicant.AddressPresentPostCode;
                row.ApplicantType = applicant.ApplicantType.ToString().AddSpacesToSentence();
                row.PhotoDiskPath = applicant.PhotoDiskPath;

                row.SSC_Roll = applicant.SSC_Roll.ToString();
                row.SSC_Board = applicant.SSC_Board.ToUpper();
                row.SSC_Year = applicant.SSC_Year.ToString();
                row.SSC_InstituteName = applicant.SSC_InstituteName.ToUpper();
                row.SSC_GPA = applicant.SSC_GPA.ToString("#0.00");

                row.HSC_Roll = applicant.HSC_Roll.ToString();

                row.HSC_Board = applicant.HSC_Board.ToUpper();
                row.HSC_Year = applicant.HSC_Year.ToString();
                row.HSC_InstituteName = applicant.HSC_InstituteName.ToUpper();
                row.HSC_GPA = applicant.HSC_GPA.ToString("#0.00");

                if (applicant.ApplicantType == EnumCollection.AdmissionUG.EducationTypeEnum.General)
                {
                    row.Exam1 = "SSC";
                    row.Exam2 = "HSC";
                }
                else if (applicant.ApplicantType == EnumCollection.AdmissionUG.EducationTypeEnum.GCE)
                {
                    row.Exam1 = "O Level";
                    row.Exam2 = "A Level";
                    if (applicant.SSC_GPA <= 0)
                    {
                        row.SSC_GPA = "";
                    }
                    if (applicant.HSC_GPA <= 0)
                    {
                        row.HSC_GPA = "";
                    }
                    if (applicant.SSC_Roll <= 0)
                    {
                        row.SSC_Roll = "";
                    }
                    if (applicant.HSC_Roll <= 0)
                    {
                        row.HSC_Roll = "";
                    }
                    if (applicant.SSC_Year <= 0)
                    {
                        row.SSC_Year = "";
                    }
                    if (applicant.HSC_Year <= 0)
                    {
                        row.HSC_Year = "";
                    }
                }
                else
                {
                    row.Exam1 = "SSC/Equivalent";
                    row.Exam2 = "HSC/Equivalent";
                }

                row.Quota = applicant.IsQuotaApproved ? applicant.Quota.ToString().AddSpacesToSentence() : EnumCollection.AdmissionUG.QuotaEnum.General.ToString();

                if (applicant.ApprovedQuota == EnumCollection.AdmissionUG.QuotaEnum.General)
                {
                    row.TikQuotaGeneral = tik;
                    row.TikQuotaChildrenOfFreedomFighters = cross;
                    row.TikQuotaChildrenOfMilitaryPersonnel = cross;
                    row.TikQuotaTribalCitizen = cross;
                }
                else if (applicant.ApprovedQuota == EnumCollection.AdmissionUG.QuotaEnum.ChildrenOfFreedomFighters)
                {
                    row.TikQuotaChildrenOfFreedomFighters = tik;
                    row.TikQuotaGeneral = cross;
                    row.TikQuotaChildrenOfMilitaryPersonnel = cross;
                    row.TikQuotaTribalCitizen = cross;
                }
                else if (applicant.ApprovedQuota == EnumCollection.AdmissionUG.QuotaEnum.ChildrenOfMilitaryPersonnel)
                {
                    row.TikQuotaGeneral = cross;
                    row.TikQuotaChildrenOfFreedomFighters = cross;
                    row.TikQuotaChildrenOfMilitaryPersonnel = tik;
                    row.TikQuotaTribalCitizen = cross;
                }
                else if (applicant.ApprovedQuota == EnumCollection.AdmissionUG.QuotaEnum.TribalCitizen)
                {
                    row.TikQuotaGeneral = cross;
                    row.TikQuotaChildrenOfFreedomFighters = cross;
                    row.TikQuotaChildrenOfMilitaryPersonnel = cross;
                    row.TikQuotaTribalCitizen = tik;
                }
                row.FormUnit = applicant.FormUnit.ToString().AddAndSpacesToSentence();
                row.SelectedPassedInUnit = applicant.SelectedPassedInUnit.ToString().AddAndSpacesToSentence();
                row.Name = applicant.Name.ToUpper();
                row.NameInBangla = applicant.FullNameBangla;
                row.DateOfBirth = applicant.DateOfBirth.ToString("dd/MM/yyyy");
                row.Email = applicant.Email;
                row.Mobile = applicant.Mobile;
                row.Nationality = applicant.Nationality;
                if (applicant.NationalId > 0)
                {
                    row.NationalId = applicant.NationalId.ToString();
                }
                row.MaritalStatus = applicant.MaritalStatus.ToString().AddAndSpacesToSentence();
                row.Religion = applicant.Religion.ToString().AddAndSpacesToSentence();
                row.Gender = applicant.GenderName.ToString().AddAndSpacesToSentence();
                row.BloodGroup = applicant.BloodGroup.ToString().AddBloodGroupSignToSentence();

                row.HSC_FatherName = applicant.HSC_FatherName.ToUpper();
                row.HSC_MotherName = applicant.HSC_MotherName.ToUpper();
                row.FathersMobile = applicant.FathersMobile;
                row.MothersMobile = applicant.MothersMobile;
                row.FathersOccupation = applicant.FathersJobType.ToString().AddSpacesToSentence();
                row.MothersOccupation = applicant.MothersJobType.ToString().AddSpacesToSentence();
                row.LocalGuardianName = applicant.LocalGuardianName;
                row.LocalGuardianAddress = applicant.LocalGuardianAddress;
                if (applicant.LocalGuardianContactNo == "0")
                {
                    row.LocalGuardianContactNo = applicant.LocalGuardianContactNo;
                }

                row.ParentsJobCategory = applicant.ParentsPrimaryJobStatus.ToString().AddSpacesToSentence();
                double tuitionFee = 0.00;
                double transprotationDevFee = 1500.00;
                double wavierAmount = 0.00;
                double totalAmount = 0.00;
                if (applicant.ParentsPrimaryJobStatusId > (int)EnumCollection.ParentsPrimaryJobStatusEnum.NonGovernmentServiceRetired)
                {
                    isRequiredGovtJobCertificate = applicant.ParentsPrimaryJobStatusId == (int)EnumCollection.ParentsPrimaryJobStatusEnum.InGovernmentService;
                    tuitionFee = 79500.00;
                }
                else
                {
                    tuitionFee = 94500.00;
                }
                if (applicant.AdmissionTestRollNumber == 2571
                    || applicant.AdmissionTestRollNumber == 4781
                    || applicant.AdmissionTestRollNumber == 2210
                    || applicant.AdmissionTestRollNumber == 0417
                    || applicant.AdmissionTestRollNumber == 5009)
                {
                    wavierAmount = 50000.00;
                }
                totalAmount = tuitionFee + transprotationDevFee - wavierAmount;
                row.TuitionFeeAmount = tuitionFee.ToString("C", new CultureInfo("bn-BD"));
                row.TransprotationDevFeeAmount = transprotationDevFee.ToString("C", new CultureInfo("bn-BD"));
                row.WavierAmount = "-" + wavierAmount.ToString("C", new CultureInfo("bn-BD"));
                row.TotalAmount = totalAmount.ToString("C", new CultureInfo("bn-BD"));
                row.UnitAMerit = applicant.UnitA_MeritCombine.ToString();
                row.UnitBMerit = applicant.UnitB_MeritCombine.ToString();

                try
                {
                    var depChoice = applicant.AdmUG_DepartmentChoice.ToList();


                    if (departments.Any() && depChoice.Any())
                    {
                        var ce = departments.SingleOrDefault(d => d.DepartmentNo == 11);
                        if (ce != null)
                        {
                            row.DeptCE = depChoice.SingleOrDefault(d => d.DepartmentId == ce.Id).Priority.ToString();
                        }
                        var DeptEECE = departments.SingleOrDefault(d => d.DepartmentNo == 16);
                        if (DeptEECE != null)
                        {
                            row.DeptEECE = depChoice.SingleOrDefault(d => d.DepartmentId == DeptEECE.Id).Priority.ToString();
                        }
                        var DeptCSE = departments.SingleOrDefault(d => d.DepartmentNo == 14);
                        if (DeptCSE != null)
                        {
                            row.DeptCSE = depChoice.SingleOrDefault(d => d.DepartmentId == DeptCSE.Id).Priority.ToString();
                        }
                        var DeptME = departments.SingleOrDefault(d => d.DepartmentNo == 18);
                        if (DeptME != null)
                        {
                            row.DeptME = depChoice.SingleOrDefault(d => d.DepartmentId == DeptME.Id).Priority.ToString();
                        }
                        var DeptNSE = departments.SingleOrDefault(d => d.DepartmentNo == 28);
                        if (DeptNSE != null)
                        {
                            row.DeptNSE = depChoice.SingleOrDefault(d => d.DepartmentId == DeptNSE.Id).Priority.ToString();
                        }

                        var DeptBME = departments.SingleOrDefault(d => d.DepartmentNo == 26);
                        if (DeptBME != null)
                        {
                            row.DeptBME = depChoice.SingleOrDefault(d => d.DepartmentId == DeptBME.Id).Priority.ToString();
                        }
                        var DeptEWCE = departments.SingleOrDefault(d => d.DepartmentNo == 30);
                        if (DeptEWCE != null)
                        {
                            row.DeptEWCE = depChoice.SingleOrDefault(d => d.DepartmentId == DeptEWCE.Id).Priority.ToString();
                        }

                        var DeptARCH = departments.SingleOrDefault(d => d.DepartmentNo == 32);
                        if (DeptARCH != null)
                        {
                            row.DeptARCH = depChoice.SingleOrDefault(d => d.DepartmentId == DeptARCH.Id).Priority.ToString();
                        }
                        else
                        {
                            row.DeptARCH = "N/A";
                        }
                        var DeptPME = departments.SingleOrDefault(d => d.DepartmentNo == 34);
                        if (DeptPME != null)
                        {
                            row.DeptPME = depChoice.SingleOrDefault(d => d.DepartmentId == DeptPME.Id).Priority.ToString();
                        }
                        var DeptIPE = departments.SingleOrDefault(d => d.DepartmentNo == 36);
                        if (DeptIPE != null)
                        {
                            row.DeptIPE = depChoice.SingleOrDefault(d => d.DepartmentId == DeptIPE.Id).Priority.ToString();
                        }
                        var DeptAE = departments.SingleOrDefault(d => d.DepartmentNo == 22);
                        if (DeptAE != null)
                        {
                            row.DeptAE = depChoice.SingleOrDefault(d => d.DepartmentId == DeptAE.Id).Priority.ToString();
                        }
                        var DeptNAME = departments.SingleOrDefault(d => d.DepartmentNo == 24);
                        if (DeptNAME != null)
                        {
                            row.DeptNAME = depChoice.SingleOrDefault(d => d.DepartmentId == DeptNAME.Id).Priority.ToString();
                        }
                    }
                }
                catch (Exception)
                {
                    
                   
                }
                //fill department choice
               

               
                row.AdmissionTestRollNumberBangla = StringHelper.ConvertEnglishToNativeDigits(applicant.AdmissionTestRollNumber.ToString(), new CultureInfo("bn-BD"));
                row.PrintedBy = HttpUtil.Profile.Name;
                row.PrintedCopy = $"({SiteSettings.Instance.InstituteShortName} COPY)";

                dataset.UG_Applicant.AddUG_ApplicantRow(row);
            }

         

            //rptFinalAdmissionForms creport = new rptFinalAdmissionForms();
            //creport.DataSource = dataset;
            //reportBook.Reports.Add(creport);
            if (reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports || reportName == EnumCollection.AdmissionUG.ReportEnum.UgAdm001)
            {
                rptMainAdmissionForm creportMainAdmissionForm = new rptMainAdmissionForm();
                creportMainAdmissionForm.DataSource = dataset;
                reportBook.Reports.Add(creportMainAdmissionForm);
            }
            if (reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports || reportName == EnumCollection.AdmissionUG.ReportEnum.UgAdm002)
            {
                rptBUPA4SizeForm creportBupa4SizeForm = new rptBUPA4SizeForm();
                creportBupa4SizeForm.DataSource = dataset;
                reportBook.Reports.Add(creportBupa4SizeForm);
            }
            if (reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports || reportName == EnumCollection.AdmissionUG.ReportEnum.UgAdm003)
            {
                rptPaymentSlip creportPaymentSlip = new rptPaymentSlip();
                creportPaymentSlip.DataSource = dataset;
                reportBook.Reports.Add(creportPaymentSlip);
            }
            if (reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports || reportName == EnumCollection.AdmissionUG.ReportEnum.UgAdm004)
            {
                rptDocumentReceiptAndDeparmentChoiceForm creportDocumentReceiptAndDeparmentChoiceForm = new rptDocumentReceiptAndDeparmentChoiceForm();
                creportDocumentReceiptAndDeparmentChoiceForm.DataSource = dataset;
                reportBook.Reports.Add(creportDocumentReceiptAndDeparmentChoiceForm);

                dsUG_Applicant dataset2 = (dsUG_Applicant)dataset.Copy();
                foreach (var row in dataset2.UG_Applicant)
                {
                    row.PrintedCopy = "(STUDENT COPY)";
                    row.AcceptChanges();
                }
                rptDocumentReceiptAndDeparmentChoiceForm creportDocumentReceiptAndDeparmentChoiceForm2 = new rptDocumentReceiptAndDeparmentChoiceForm();
                creportDocumentReceiptAndDeparmentChoiceForm2.DataSource = dataset2;
                reportBook.Reports.Add(creportDocumentReceiptAndDeparmentChoiceForm2);
            }
            if (reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports || reportName == EnumCollection.AdmissionUG.ReportEnum.UgAdm005)
            {
                rptMedicalClearanceForm creportMedicalClearanceForm = new rptMedicalClearanceForm();
                creportMedicalClearanceForm.DataSource = dataset;
                reportBook.Reports.Add(creportMedicalClearanceForm);
            }
            if ((reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports ) || reportName == EnumCollection.AdmissionUG.ReportEnum.UgAdm006)
            {
                rptGovtEmployeesCertificateBangla creportGovtSalaryCertificateBangla = new rptGovtEmployeesCertificateBangla();
                creportGovtSalaryCertificateBangla.DataSource = dataset;
                reportBook.Reports.Add(creportGovtSalaryCertificateBangla);
            }
            if (reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports || reportName == EnumCollection.AdmissionUG.ReportEnum.Ugoh001)
            {
                rptApplicationFormForSeatInHall creportApplicationFormForSeatInHall = new rptApplicationFormForSeatInHall();
                creportApplicationFormForSeatInHall.DataSource = dataset;
                reportBook.Reports.Add(creportApplicationFormForSeatInHall);
            }

            if (reportName == EnumCollection.AdmissionUG.ReportEnum.MistMainForm)
            {
                rptFinalAdmissionForms creportrptFinalAdmissionForms = new rptFinalAdmissionForms();


                //creportrptFinalAdmissionForms
                creportrptFinalAdmissionForms.DataSource = dataset;
                reportBook.Reports.Add(creportrptFinalAdmissionForms);
            }
            if ( reportName == EnumCollection.AdmissionUG.ReportEnum.BupMainForm)
            {
                //rptBUPRegistrationForm creportBUPRegistrationForm = new rptBUPRegistrationForm();
                //creportBUPRegistrationForm.DataSource = dataset;
                //reportBook.Reports.Add(creportBUPRegistrationForm);
                rptFinal2AdmissionForms creportFinal2AdmissionForm = new rptFinal2AdmissionForms();
                creportFinal2AdmissionForm.DataSource = dataset;
                reportBook.Reports.Add(creportFinal2AdmissionForm);
                rptFinal3AdmissionForms creportFinal3AdmissionForm = new rptFinal3AdmissionForms();
                creportFinal3AdmissionForm.DataSource = dataset;
                reportBook.Reports.Add(creportFinal3AdmissionForm);
            }

            return reportBook;
        }
        public ReportSource GetFinalAdmissionForms(List<AdmUG_ApplicantByRule> applicants, List<Department> departments, EnumCollection.AdmissionUG.ReportEnum reportName, out string message)
        {
            message = null;
            var isRequiredGovtJobCertificate = false;

            dsUG_Applicant dataset = new dsUG_Applicant();
            ReportBook reportBook = new ReportBook();
            var rowNum = 0;
            foreach (var applicant in applicants)
            {
                string tik = "\u2714";
                string cross = "\u2716";
                dsUG_Applicant.UG_ApplicantRow row = dataset.UG_Applicant.NewUG_ApplicantRow();
                row.AdmissionTestRollNumber = applicant.RollNo.ToStringPadLeft(4, '0');
                row.FromSerial = applicant.ServiceNo;
                row.UserId = applicant.Username;
                row.AddressPermanentCareOf = applicant.AddressPermanentCareOf;
                row.AddressPermanentCareOf = applicant.AddressPermanentCareOf;
                row.PermanentAddress = applicant.AddressPermanent;
                row.PresentAddress = applicant.AddressPresent;
                row.PermanentAddress1 = "P.S: "+applicant.AddressPermanentPoliceStation + ", Dist: " + applicant.AddressPermanentDistrict + ", Country: " + applicant.AddressPermanentCountry + ", Post Code:" + applicant.AddressPermanentPostCode;
                row.PresentAddress1 = "P.S: " + applicant.AddressPresentPoliceStation + ", Dist: " + applicant.AddressPresentDistrict + ", Country: " + applicant.AddressPresentCountry + ", Post Code:" + applicant.AddressPresentPostCode;
                
                row.AddressPermanent = applicant.AddressPermanentCareOf + ", " + applicant.AddressPermanent + ", " + applicant.AddressPermanentPoliceStation + ", " + applicant.AddressPermanentDistrict + ", " + applicant.AddressPermanentCountry + " - " + applicant.AddressPermanentPostCode;
                row.AddressPresent = applicant.AddressPresentCareOf + ", " + applicant.AddressPresent + ", " + applicant.AddressPresentPoliceStation + ", " + applicant.AddressPresentDistrict + ", " + applicant.AddressPermanentCountry + " - " + applicant.AddressPresentPostCode;
                row.ApplicantType = applicant.Quota.ToString().AddSpacesToSentence();
                row.PhotoDiskPath = applicant.PhotoDiskPath;

                row.SSC_Roll = applicant.SSC_Roll.ToString();
                row.SSC_Board = applicant.SSC_Board.ToString().ToUpper();
                row.SSC_Year = applicant.SSC_Year.ToString();
                row.SSC_InstituteName = applicant.SSC_InstituteName.ToUpper();
                row.SSC_GPA = applicant.SSC_GPA.ToString("#0.00");

                row.HSC_Roll = applicant.HSC_Roll.ToString();

                row.HSC_Board = applicant.HSC_Board.ToString().ToUpper();
                row.HSC_Year = applicant.HSC_Year.ToString();
                row.HSC_InstituteName = applicant.HSC_InstituteName.ToUpper();
                row.HSC_GPA = applicant.HSC_GPA.ToString("#0.00");

                if (applicant.EducationType == EnumCollection.AdmissionUG.EducationTypeEnum.General)
                {
                    row.Exam1 = "SSC";
                    row.Exam2 = "HSC";
                }
                else if (applicant.EducationType == EnumCollection.AdmissionUG.EducationTypeEnum.GCE)
                {
                    row.Exam1 = "O Level";
                    row.Exam2 = "A Level";
                    if (applicant.SSC_GPA <= 0)
                    {
                        row.SSC_GPA = "";
                    }
                    if (applicant.HSC_GPA <= 0)
                    {
                        row.HSC_GPA = "";
                    }
                    if (applicant.SSC_Roll <= 0)
                    {
                        row.SSC_Roll = "";
                    }
                    if (applicant.HSC_Roll <= 0)
                    {
                        row.HSC_Roll = "";
                    }
                    if (applicant.SSC_Year <= 0)
                    {
                        row.SSC_Year = "";
                    }
                    if (applicant.HSC_Year <= 0)
                    {
                        row.HSC_Year = "";
                    }
                }
                else
                {
                    row.Exam1 = "SSC/Equivalent";
                    row.Exam2 = "HSC/Equivalent";
                }

                row.Quota = applicant.Quota.ToString().AddSpacesToSentence();
                row.TikQuotaGeneral = cross;
                row.TikQuotaChildrenOfFreedomFighters = cross;
                row.TikQuotaChildrenOfMilitaryPersonnel = cross;
                row.TikQuotaTribalCitizen = cross;

                row.FormUnit = "N/A";
                row.SelectedPassedInUnit = "N/A";
                row.Name = applicant.Name.ToUpper();
                row.NameInBangla = applicant.FullNameBangla;
                row.DateOfBirth = applicant.DateOfBirth.ToString("dd/MM/yyyy");
                row.Email = applicant.Email;
                row.Mobile = applicant.Mobile;
                row.Nationality = applicant.Nationality;
                if (applicant.NationalId > 0)
                {
                    row.NationalId = applicant.NationalId.ToString();
                }
                row.MaritalStatus = applicant.MaritalStatus.ToString().AddAndSpacesToSentence();
                row.Religion = applicant.Religion.ToString().AddAndSpacesToSentence();
                row.Gender = applicant.Gender.ToString().AddAndSpacesToSentence();
                row.BloodGroup = applicant.BloodGroup.ToString().AddBloodGroupSignToSentence();

                row.HSC_FatherName = applicant.FathersName.ToUpper();
                row.HSC_MotherName = applicant.MothersName.ToUpper();
                row.FathersMobile = applicant.FathersMobile;
                row.MothersMobile = applicant.MothersMobile;
                row.FathersOccupation = applicant.FathersJobType.ToString().AddSpacesToSentence();
                row.MothersOccupation = applicant.MothersJobType.ToString().AddSpacesToSentence();
                row.LocalGuardianName = applicant.LocalGuardianName;
                row.LocalGuardianAddress = applicant.LocalGuardianAddress;
                if (applicant.LocalGuardianContactNo == "0")
                {
                    row.LocalGuardianContactNo = applicant.LocalGuardianContactNo;
                }

                row.ParentsJobCategory = applicant.ParentsPrimaryJobStatus.ToString().AddSpacesToSentence();
                double tuitionFee = 0.00;
                double transprotationDevFee = 1500.00;
                double wavierAmount = 0.00;
                double totalAmount = 0.00;
                if (applicant.ParentsPrimaryJobStatusId > (int)EnumCollection.ParentsPrimaryJobStatusEnum.NonGovernmentServiceRetired)
                {
                    isRequiredGovtJobCertificate = applicant.ParentsPrimaryJobStatusId == (int)EnumCollection.ParentsPrimaryJobStatusEnum.InGovernmentService;
                    tuitionFee = 79500.00;
                }
                else
                {
                    tuitionFee = 94500.00;
                }
                if (applicant.RollNo == 2571
                    || applicant.RollNo == 4781
                    || applicant.RollNo == 2210
                    || applicant.RollNo == 0417
                    || applicant.RollNo == 5009)
                {
                    wavierAmount = 50000.00;
                }
                totalAmount = tuitionFee + transprotationDevFee - wavierAmount;
                row.TuitionFeeAmount = tuitionFee.ToString("C", new CultureInfo("bn-BD"));
                row.TransprotationDevFeeAmount = transprotationDevFee.ToString("C", new CultureInfo("bn-BD"));
                row.WavierAmount = "-" + wavierAmount.ToString("C", new CultureInfo("bn-BD"));
                row.TotalAmount = totalAmount.ToString("C", new CultureInfo("bn-BD"));
                row.UnitAMerit = "N/A";
                row.UnitBMerit = "N/A";

                try
                {
                    var depChoice = applicant.AdmUG_DepartmentChoiceByRule.ToList();


                    if (departments.Any() && depChoice.Any())
                    {
                        var ce = departments.SingleOrDefault(d => d.DepartmentNo == 11);
                        if (ce != null)
                        {
                            row.DeptCE = depChoice.SingleOrDefault(d => d.DepartmentId == ce.Id).Priority.ToString();
                        }
                        var DeptEECE = departments.SingleOrDefault(d => d.DepartmentNo == 16);
                        if (DeptEECE != null)
                        {
                            row.DeptEECE = depChoice.SingleOrDefault(d => d.DepartmentId == DeptEECE.Id).Priority.ToString();
                        }
                        var DeptCSE = departments.SingleOrDefault(d => d.DepartmentNo == 14);
                        if (DeptCSE != null)
                        {
                            row.DeptCSE = depChoice.SingleOrDefault(d => d.DepartmentId == DeptCSE.Id).Priority.ToString();
                        }
                        var DeptME = departments.SingleOrDefault(d => d.DepartmentNo == 18);
                        if (DeptME != null)
                        {
                            row.DeptME = depChoice.SingleOrDefault(d => d.DepartmentId == DeptME.Id).Priority.ToString();
                        }
                        var DeptNSE = departments.SingleOrDefault(d => d.DepartmentNo == 28);
                        if (DeptNSE != null)
                        {
                            row.DeptNSE = depChoice.SingleOrDefault(d => d.DepartmentId == DeptNSE.Id).Priority.ToString();
                        }

                        var DeptBME = departments.SingleOrDefault(d => d.DepartmentNo == 26);
                        if (DeptBME != null)
                        {
                            row.DeptBME = depChoice.SingleOrDefault(d => d.DepartmentId == DeptBME.Id).Priority.ToString();
                        }
                        var DeptEWCE = departments.SingleOrDefault(d => d.DepartmentNo == 30);
                        if (DeptEWCE != null)
                        {
                            row.DeptEWCE = depChoice.SingleOrDefault(d => d.DepartmentId == DeptEWCE.Id).Priority.ToString();
                        }

                        var DeptARCH = departments.SingleOrDefault(d => d.DepartmentNo == 32);
                        if (DeptARCH != null)
                        {
                            row.DeptARCH = depChoice.SingleOrDefault(d => d.DepartmentId == DeptARCH.Id).Priority.ToString();
                        }
                        else
                        {
                            row.DeptARCH = "N/A";
                        }
                        var DeptPME = departments.SingleOrDefault(d => d.DepartmentNo == 34);
                        if (DeptPME != null)
                        {
                            row.DeptPME = depChoice.SingleOrDefault(d => d.DepartmentId == DeptPME.Id).Priority.ToString();
                        }
                        var DeptIPE = departments.SingleOrDefault(d => d.DepartmentNo == 36);
                        if (DeptIPE != null)
                        {
                            row.DeptIPE = depChoice.SingleOrDefault(d => d.DepartmentId == DeptIPE.Id).Priority.ToString();
                        }
                        var DeptAE = departments.SingleOrDefault(d => d.DepartmentNo == 22);
                        if (DeptAE != null)
                        {
                            row.DeptAE = depChoice.SingleOrDefault(d => d.DepartmentId == DeptAE.Id).Priority.ToString();
                        }
                        var DeptNAME = departments.SingleOrDefault(d => d.DepartmentNo == 24);
                        if (DeptNAME != null)
                        {
                            row.DeptNAME = depChoice.SingleOrDefault(d => d.DepartmentId == DeptNAME.Id).Priority.ToString();
                        }
                    }
                }
                catch (Exception)
                {
                    
                   
                }
                //fill department choice
               

               
                row.AdmissionTestRollNumberBangla = StringHelper.ConvertEnglishToNativeDigits(applicant.RollNo.ToString(), new CultureInfo("bn-BD"));
                row.PrintedBy = HttpUtil.Profile.Name;
                row.PrintedCopy = $"({SiteSettings.Instance.InstituteShortName} COPY)";

                dataset.UG_Applicant.AddUG_ApplicantRow(row);
            }

         

            //rptFinalAdmissionForms creport = new rptFinalAdmissionForms();
            //creport.DataSource = dataset;
            //reportBook.Reports.Add(creport);
            if (reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports || reportName == EnumCollection.AdmissionUG.ReportEnum.UgAdm001)
            {
                rptMainAdmissionForm creportMainAdmissionForm = new rptMainAdmissionForm();
                creportMainAdmissionForm.DataSource = dataset;
                reportBook.Reports.Add(creportMainAdmissionForm);
            }
            if (reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports || reportName == EnumCollection.AdmissionUG.ReportEnum.UgAdm002)
            {
                rptBUPA4SizeForm creportBupa4SizeForm = new rptBUPA4SizeForm();
                creportBupa4SizeForm.DataSource = dataset;
                reportBook.Reports.Add(creportBupa4SizeForm);
            }
            if (reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports || reportName == EnumCollection.AdmissionUG.ReportEnum.UgAdm003)
            {
                rptPaymentSlip creportPaymentSlip = new rptPaymentSlip();
                creportPaymentSlip.DataSource = dataset;
                reportBook.Reports.Add(creportPaymentSlip);
            }
            if (reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports || reportName == EnumCollection.AdmissionUG.ReportEnum.UgAdm004)
            {
                rptDocumentReceiptAndDeparmentChoiceForm creportDocumentReceiptAndDeparmentChoiceForm = new rptDocumentReceiptAndDeparmentChoiceForm();
                creportDocumentReceiptAndDeparmentChoiceForm.DataSource = dataset;
                reportBook.Reports.Add(creportDocumentReceiptAndDeparmentChoiceForm);

                dsUG_Applicant dataset2 = (dsUG_Applicant)dataset.Copy();
                foreach (var row in dataset2.UG_Applicant)
                {
                    row.PrintedCopy = "(STUDENT COPY)";
                    row.AcceptChanges();
                }
                rptDocumentReceiptAndDeparmentChoiceForm creportDocumentReceiptAndDeparmentChoiceForm2 = new rptDocumentReceiptAndDeparmentChoiceForm();
                creportDocumentReceiptAndDeparmentChoiceForm2.DataSource = dataset2;
                reportBook.Reports.Add(creportDocumentReceiptAndDeparmentChoiceForm2);
            }
            if (reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports || reportName == EnumCollection.AdmissionUG.ReportEnum.UgAdm005)
            {
                rptMedicalClearanceForm creportMedicalClearanceForm = new rptMedicalClearanceForm();
                creportMedicalClearanceForm.DataSource = dataset;
                reportBook.Reports.Add(creportMedicalClearanceForm);
            }
            if ((reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports ) || reportName == EnumCollection.AdmissionUG.ReportEnum.UgAdm006)
            {
                rptGovtEmployeesCertificateBangla creportGovtSalaryCertificateBangla = new rptGovtEmployeesCertificateBangla();
                creportGovtSalaryCertificateBangla.DataSource = dataset;
                reportBook.Reports.Add(creportGovtSalaryCertificateBangla);
            }
            if (reportName == EnumCollection.AdmissionUG.ReportEnum.AllReports || reportName == EnumCollection.AdmissionUG.ReportEnum.Ugoh001)
            {
                rptApplicationFormForSeatInHall creportApplicationFormForSeatInHall = new rptApplicationFormForSeatInHall();
                creportApplicationFormForSeatInHall.DataSource = dataset;
                reportBook.Reports.Add(creportApplicationFormForSeatInHall);
            }

            if (reportName == EnumCollection.AdmissionUG.ReportEnum.MistMainForm)
            {
                rptFinalAdmissionForms creportrptFinalAdmissionForms = new rptFinalAdmissionForms();


                //creportrptFinalAdmissionForms
                creportrptFinalAdmissionForms.DataSource = dataset;
                reportBook.Reports.Add(creportrptFinalAdmissionForms);
            }
            if ( reportName == EnumCollection.AdmissionUG.ReportEnum.BupMainForm)
            {
                //rptBUPRegistrationForm creportBUPRegistrationForm = new rptBUPRegistrationForm();
                //creportBUPRegistrationForm.DataSource = dataset;
                //reportBook.Reports.Add(creportBUPRegistrationForm);
                rptFinal2AdmissionForms creportFinal2AdmissionForm = new rptFinal2AdmissionForms();
                creportFinal2AdmissionForm.DataSource = dataset;
                reportBook.Reports.Add(creportFinal2AdmissionForm);
                rptFinal3AdmissionForms creportFinal3AdmissionForm = new rptFinal3AdmissionForms();
                creportFinal3AdmissionForm.DataSource = dataset;
                reportBook.Reports.Add(creportFinal3AdmissionForm);
            }

            return reportBook;
        }
    }
}
