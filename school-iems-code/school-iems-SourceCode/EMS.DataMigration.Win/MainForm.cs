using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataMigration.Win.BulkUpdate;
using EMS.DataMigration.Win.Data;
using EMS.DataMigration.Win.Utils;
using EMS.Facade;
using EMS.Framework.Utils;

namespace EMS.DataMigration.Win
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public delegate void CancelTask();
        public static event CancelTask OnCancelTask;
        private void MainForm_Load(object sender, EventArgs e)
        {

            BaseMigrator.OnLogReport += BaseMigrator_OnLogReport;
            BaseMigrator.OnTaskCompleted += BaseMigrator_OnTaskCompleted;
        }


        void DisableButons()
        {

            btnCancelTask.Enabled = true;

            gbAction.Enabled = false;
            gbActions.Enabled = false;
        }
        void EnableButons()
        {
            //btnProfile.Enabled = true;
            //btnAddress.Enabled = true;
            //btnDiscount.Enabled = true;
            btnCancelTask.Enabled = false;
            gbAction.Enabled = true;
            gbActions.Enabled = true;
        }
        #region BGTask
        private void StudentSync_Click(object sender, EventArgs e)
        {
            DisableButons();
            var migrator = new StudentProfileMigrator("Profile");
            migrator.DoWork(1);
        }
        private void AddressSync_Click(object sender, EventArgs e)
        {
            DisableButons();
            var migrator = new StudentProfileMigrator("Address");
            migrator.DoWork(2);
        }
        private void EduSync_Click(object sender, EventArgs e)
        {
            DisableButons();
            var migrator = new StudentProfileMigrator("Address");
            migrator.DoWork(3);
        }
        private void FeecodeAndPolicySync_Click(object sender, EventArgs e)
        {
            PackageMigrator pack = new PackageMigrator("FeeCode");
            pack.DoWork(1);
        }
        private void DiscountSync_Click(object sender, EventArgs e)
        {
            DisableButons();
            PackageMigrator pack = new PackageMigrator("Doscount");
            pack.DoWork(2);

        }

        #endregion
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (bgWorker.IsBusy)
            //{
            //    bgWorker.CancelAsync();


            //}

        }
        private void BaseMigrator_OnTaskCompleted(string message, bool isError = false)
        {
            EnableButons();

            MessageBox.Show(message);
        }

        private void BaseMigrator_OnLogReport(int pecentageDone, string message, bool isError = false)
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {

                //richTextBox.Text = log +"\r\n"+ richTextBox.Text;
                lblResult.Text = progressBar.Value.ToString();
                richTextBox.AppendText(message + "\n");

                progressBar.Value = pecentageDone;
            }));
        }
        private void LogReport(string message)
        {
            Debug.WriteLine(message + "\n");
            BeginInvoke(new MethodInvoker(delegate ()
            {
                richTextBox.AppendText(message + "\n");
                
            }));
        }

        private void btnCancelTask_Click(object sender, EventArgs e)
        {
            if (OnCancelTask != null) OnCancelTask();
        }

        private void btnMigrateTrnsCr_Click(object sender, EventArgs e)
        {
            DisableButons();
            AcntCreditMigration credit = new AcntCreditMigration("Transaction (Cr.)");
            credit.DoWork(1);
        }

        private void btnMigrateTrnsDr_Click(object sender, EventArgs e)
        {
            DisableButons();
            AcntDebitMigrator debit = new AcntDebitMigrator("Transaction (Dr.)");
            debit.DoWork(1);
        }
        protected TheFacade Facade { get { return new TheFacade(HttpUtil.DbContext, HttpUtil.Profile); } }
        private void btnTestMail_Click(object sender, EventArgs e)
        {
            //MailGunEmailSender.SendSimpleMessage();
            //Console.WriteLine(MailGunEmailSender.SendSimpleMessage().Content.ToString());

            //var test=  Facade.AccountingFacade.StudentPayment.GetVoucherForManualCollectionByUserId(1);
            //RemoveDuplicateBaseCourse();
            string error = String.Empty;
            //Facade.StudentPaymentFacade.GenerateSemesterFeesThisToForward(1725, 201201, out error);



        }
        void RemoveDuplicateBaseCourse()
        {
            EmsDbContext dbContext = new EmsDbContext();

            var baseCourse = dbContext.Aca_Course.ToList();

            var curriculumnCourse = dbContext.Aca_CurriculumCourse.Include(x=>x.Aca_Curriculum).ToList();

            var uniqCourseList=new List<Aca_Course>();

            var baseCourseToRemove = new List<Aca_Course>();

            foreach (var course in baseCourse)
            {
                var existCourse = uniqCourseList.SingleOrDefault(x =>
                        x.Code.ToLower() == course.Code.ToLower() && x.CreditLoad == course.CreditLoad
                        //&& x.Name.ToLower() == course.Name.ToLower()
                        && x.CourseCategoryEnumId == course.CourseCategoryEnumId
                        && x.DepartmentId == course.DepartmentId);
                        ;//&& x.Name.ToLower() != "XXX-XXX".ToLower() 

                if (existCourse!=null)
                {
                    //var previousMainList = curriculumnCourse.Where(x => x.CourseId == existCourse.Id).ToList();//tmp
                    LogReport($"Base Uniq Course\t\t{existCourse.Id}\t{existCourse.Code}\t{existCourse.Name}");
                    //foreach (var curCourse in previousMainList)
                    //{
                    //    Debug.WriteLine($"{curCourse.CurriculumId}\t{curCourse.Aca_Curriculum.ShortName}\t{curCourse.Id}\t{existCourse.Code}\t{existCourse.Name}");//tmp

                    //}
                    //Already exist and need to remove

                    LogReport($"Base Cours to Remove\t\t{course.Id}\t{course.Code}\t{course.Name}");
                    var courseList = curriculumnCourse.Where(x => x.CourseId == course.Id).ToList();
             
                    foreach (var curCourse in courseList)
                    {
                        curCourse.CourseId = existCourse.Id;
                        LogReport($"{curCourse.CurriculumId}\t{curCourse.Aca_Curriculum.ShortName}\t{curCourse.Id}\t{existCourse.Code}\t{existCourse.Name}");

                    }
                    baseCourseToRemove.Add(course);
                    //baseCourseToRemove.Add(dupCourse);//temp
                }
                else
                {
                    uniqCourseList.Add(course);
                
                }
                //course.Name = course.Name.ToLower().ToTitleCase().Replace(" Of ", " of ").Replace(" And ", " and ").Replace(" Is ", " is ").Replace(" The ", " the ").Replace(" Ii ", " II ").Replace(" Ii ", " II ").Replace(" Iii ", " III ").Replace(" Iv ", " IV ").Replace(" Vi ", " VI ").Replace(" Vii ", " VII ").Replace(" Viii ", " VIII ").Replace(" Ia ", " IA ").Replace(" Ib ", " IB ").Replace(" Ic ", " IC ");
                //course.Description = course.Description.ToLower().ToTitleCase().Replace(" Of ", " of ").Replace(" And ", " and ").Replace(" Is ", " is ").Replace(" The ", " the ").Replace(" Ii ", " II ").Replace(" Ii ", " II ").Replace(" Iii ", " III ").Replace(" Iv ", " IV ").Replace(" Vi ", " VI ").Replace(" Vii ", " VII ").Replace(" Viii ", " VIII ").Replace(" Ia ", " IA ").Replace(" Ib ", " IB ").Replace(" Ic ", " IC ");

            }
            //formatting naame
            //foreach (var curCourse in curriculumnCourse)
            //{
            //    curCourse.Name = curCourse.Name.Trim().ToLower().ToTitleCase().Replace(" Of ", " of ").Replace(" And ", " and ").Replace(" Is ", " is ").Replace(" The ", " the ").Replace(" Ii ", " II ").Replace(" Ii ", " II ").Replace(" Iii ", " III ").Replace(" Iv ", " IV ").Replace(" Vi ", " VI ").Replace(" Vii ", " VII ").Replace(" Viii ", " VIII ").Replace(" Ia ", " IA ").Replace(" Ib ", " IB ").Replace(" Ic ", " IC ");
            //    curCourse.Description = curCourse.Description.ToLower().ToTitleCase().Replace(" Of ", " of ").Replace(" And ", " and ").Replace(" Is ", " is ").Replace(" The ", " the ").Replace(" Ii ", " II ").Replace(" Ii ", " II ").Replace(" Iii ", " III ").Replace(" Iv ", " IV ").Replace(" Vi ", " VI ").Replace(" Vii ", " VII ").Replace(" Viii ", " VIII ").Replace(" Ia ", " IA ").Replace(" Ib ", " IB ").Replace(" Ic ", " IC ");
            //}

            //foreach (var bCourse in baseCourseToRemove)
            //{
            //    var dupCourseList = curriculumnCourse.Where(x => x.CourseId == bCourse.Id).ToList();


            //    foreach (var curCourse in dupCourseList)
            //    {


            //        if (curCourse != null)
            //        {
            //            Debug.WriteLine($"{curCourse.Id}\t{bCourse.Code}\t{bCourse.Name}\t{curCourse.CurriculumId}");
            //        }
            //        else
            //        {
            //            Debug.WriteLine($"{bCourse.Id}\t{bCourse.Code}\t{bCourse.Name}\tN\\A");
            //        }

            //    }

            //}



            dbContext.Aca_Course.RemoveRange(baseCourseToRemove);
            dbContext.SaveChanges();

        }

        private void btnMigrateReg_Click(object sender, EventArgs e)
        {
            DisableButons();
            RegistrationMigration reg = new RegistrationMigration("Registration");
            reg.DoWork(1);
        }

        private void btnCourseMapping_Click(object sender, EventArgs e)
        {
            //Not Use Now...
            DisableButons();
            BaseCourseMapping baseCourse = new BaseCourseMapping("Base Course Mapping By Curriculum Course");
            baseCourse.DoWork(1);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            txtTest.Text = txtTest.Text.ValidateName();
            //txtTest.Text = txtTest.Text.ValidateMobile();
        }

        private void gbDeleteActions_Enter(object sender, EventArgs e)
        {

        }

        private void btnDeleteFeecode_Click(object sender, EventArgs e)
        {

        }

        private void btnAllStdBillReGen_Click(object sender, EventArgs e)
        {

            DisableButons();
            RegenerateSemesterBill debit = new RegenerateSemesterBill("Regenerate Semester Bill (Dr.)");
            debit.DoWork(1);

            /*
            Debug.WriteLine($"Start");

            EmsDbContext dbContext = new EmsDbContext();

            var studentList = dbContext.User_Student
                .Include(x => x.Aca_Program)
                .Include(x => x.Aca_Program.Acnt_OfficialBank) //for Bank Name
                .Include(x => x.Acnt_FeeCode)
                .Include(x => x.Acnt_FeeCode.Acnt_FeeCodePolicy) //for Bill Generate
                .Where(x => x.Id == 1477).ToList();


            var semesterList = dbContext.Aca_Semester.OrderBy(x => x.Year).ThenBy(x => x.TermId).ToList();

            string error=String.Empty;

            using (var scope=dbContext.Database.BeginTransaction())
            {
                foreach (var student in studentList)
                {
                    var classTakenByStudentList = dbContext.Aca_ClassTakenByStudent
                        .Include(x => x.Aca_Class)
                        .Include(x => x.Aca_Class.Aca_Semester)
                        .Include(x => x.Aca_Class.Aca_CurriculumCourse) // Bill Generate by Credit
                        .Where(x => x.StudentId == student.Id
                        ).ToList();


                    foreach (var semester in semesterList)
                    {
                        if (!Facade.AccountingFacade.StudentPayment.GenerateSemesterFeesThisToForward(student.Id, student,
                            semester.Id, semester, classTakenByStudentList, out error))
                        {
                            var x = student;
                            Debug.WriteLine($"Id:{x.Id}\tSId:{x.ClassRollNo}\tSemId:{semester.Id}\terror:{error}");
                        }


                    }//end semester loop

                    Debug.WriteLine($"Done Id:{student.Id}");
                }//end student list loop  

                scope.Commit();
                MessageBox.Show("Done");
            }

            */

        }

        private void btnRegenPerCredit_Click(object sender, EventArgs e)
        {
            DisableButons();
            RegenerateSemesterBill debit = new RegenerateSemesterBill("Regenerate Per-Credit!");
            debit.DoWork(2);
        }

        private void btnRecalculateBalance_Click(object sender, EventArgs e)
        {
            DisableButons();
            RegenerateSemesterBill debit = new RegenerateSemesterBill("Recalculate Balance!");
            debit.DoWork(3);
        }

        private void btnCreditTransfer_Click(object sender, EventArgs e)
        {
            DisableButons();
            CreditTransferMigrator creditTransfer = new CreditTransferMigrator("Credit Transfer!");
            creditTransfer.DoWork(1);
        }

        private void btnGrade_Click(object sender, EventArgs e)
        {
            DisableButons();
            RegenerateSemesterBill debit = new RegenerateSemesterBill("Recalculate Grade!");
            debit.DoWork(4);
        }

        private void btnClassProgramIdMap_Click(object sender, EventArgs e)
        {

            DisableButons();
            //RegenerateSemesterBill debit = new RegenerateSemesterBill("Regenerate Semester Bill (Dr.)");
            //debit.DoWork(1);


            LogReport($"Start Class Program Id Map");

            try
            {
                EmsDbContext dbContext = new EmsDbContext();

                var classList = dbContext.Aca_Class.ToList();
                var curriculumCourseList = dbContext.Aca_CurriculumCourse.ToList();
                var curriculumList = dbContext.Aca_Curriculum.ToList();

                using (var scope = dbContext.Database.BeginTransaction())
                {
                    LogReport($"Start Loop");
                    foreach (var acaClass in classList)
                    {
                        var curriculumCourse = curriculumCourseList.SingleOrDefault(x => x.Id == acaClass.CurriculumCourseId);
                        if (curriculumCourse == null)
                        {
                            LogReport($"Curriculum Course Not Found for ClassId={acaClass.Id}");
                            continue;
                        }
                        var curriculum = curriculumList.SingleOrDefault(x => x.Id == curriculumCourse.CurriculumId);
                        if (curriculum == null)
                        {
                            LogReport($"Curriculum Not Found for CurriculumCourseId={curriculumCourse.Id}");
                            continue;
                        }

                        acaClass.ProgramId = curriculum.ProgramId;

                        //Debug.WriteLine($"Done {acaClass.Id}");
                    }
                    LogReport($"End Loop");



                    LogReport($"Db Save Changes");
                    dbContext.SaveChanges();
                    scope.Commit();
                    MessageBox.Show("Done");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.GetBaseException()}");
                LogReport(ex.GetBaseException().ToString());
            }
            

        }

        private void btnMissingMigrationResult_Click(object sender, EventArgs e)
        {
            DisableButons();
            ResultReMigrator debit = new ResultReMigrator("Missing Migration Result Re Migration!");
            debit.DoWork(1);
        }

        private void btnMigrationReport_Click(object sender, EventArgs e)
        {
            DisableButons();
            //MigrationReport report = new MigrationReport("Migration Report");
            //report.DoWork(1);
        }

        private void btnMissingMigrationReg_Click(object sender, EventArgs e)
        {
            DisableButons();
            //ResultReMigrator reg = new ResultReMigrator("Missing Migration Registration Re Migration");
            //reg.DoWork(2);
        }

        private void btnTemp_Click(object sender, EventArgs e)
        {
            DisableButons();
            //ResultReMigrator reg = new ResultReMigrator("Missing Migration Temp Function Calling");
            //reg.DoWork(0);
        }

        private void btnContAssesReMigration_Click(object sender, EventArgs e)
        {
            DisableButons();
            //ResultReMigrator reg = new ResultReMigrator("Continuous Assessment ReMigration");
            //reg.DoWork(3);
        }


        private void btnClassSectionMapping_Click(object sender, EventArgs e)
        {
            DisableButons();
            var classSection = new ClassSectionMapping("Start Class Section Mapping");
            classSection.DoWork(0);
        }



        private void btnGenAdmFee_Click(object sender, EventArgs e)
        {
            DisableButons();
            RegenerateSemesterBill debit = new RegenerateSemesterBill("Generate Admission Fee.");
            debit.DoWork(5);
        }

        private void btnGenerateSemWiseDiscount_Click(object sender, EventArgs e)
        {
            DisableButons();
            RegenerateSemesterBill debit = new RegenerateSemesterBill("Generate Semester Wise Discount.");
            debit.DoWork(6);
        }

        private void btnScholarshipPercentToRealAmountCalculator_Click(object sender, EventArgs e)
        {
            DisableButons();
            RegenerateSemesterBill debit = new RegenerateSemesterBill("Update Std Scholarship PercentAmount to RealAmount");
            debit.DoWork(7);
        }

        private void btnSmuctStudentCompare_Click(object sender, EventArgs e)
        {
            int reportTypeEnumId=0;
            reportTypeEnumId=int.TryParse(txtReportTypeEnumId.Text,out reportTypeEnumId)? reportTypeEnumId:0;

            if (reportTypeEnumId<=0)
            {
                MessageBox.Show("Invalid Report Type Enum Id");
                return;
            }

            DisableButons();
            var studentCompare = new SmuctStudentCompare("Smuct Student Compare", reportTypeEnumId);
            studentCompare.DoWork(0);
        }

        private void btnRegNoUpdate_Click(object sender, EventArgs e)
        {
            DisableButons();
            SmuctStudentCompare debit = new SmuctStudentCompare("Student Registration No Update");
            debit.DoWork(1);
        }

        private void btnRegCourseCancel_Click(object sender, EventArgs e)
        {
            DisableButons();
            RegistrationMigration obj = new RegistrationMigration("Reg Course Cancel.");
            obj.DoWork(2);
            
        }

        private void btnPreviousEducationReport_Click(object sender, EventArgs e)
        {
            DisableButons();
            Report obj = new Report("Previous Education Report.");
            obj.DoWork(1);
        }


        private void btnCourseNotFoundReport_Click(object sender, EventArgs e)
        {
            DisableButons();
            GenericResultMigrator genMigrator = new GenericResultMigrator("Registration Migration");
            genMigrator.DoWork(3);
        }

        private void ckBoxIsRegSave_CheckedChanged(object sender, EventArgs e)
        {
            MigrationSettings.IsSaveRegData = ckBoxIsRegSave.Checked;
        }
        private void btnGenericRegMigrat_Click(object sender, EventArgs e)
        {
            DisableButons();
            GenericResultMigrator genMigrator = new GenericResultMigrator("Registration Migration");
            genMigrator.DoWork(1);
        }

        private void ckBoxIsResultSave_CheckedChanged(object sender, EventArgs e)
        {
            MigrationSettings.IsSaveResultData = ckBoxIsResultSave.Checked;
        }

        private void btnGenericResultMigrat_Click(object sender, EventArgs e)
        {
            DisableButons();
            GenericResultMigrator genMigrator = new GenericResultMigrator("Result Migration");
            genMigrator.DoWork(2);
        }

        private void btnDataMapping_Click(object sender, EventArgs e)
        {
            DisableButons();
            GenericResultMigrator genMigrator = new GenericResultMigrator("Student Data Mapping");
            genMigrator.DoWork(0);
        }

        private void BtnBacklogDataMapping_Click(object sender, EventArgs e)
        {
            DisableButons();
            //BacklogResultMigrator genMigrator = new BacklogResultMigrator("Student Data Mapping For Backlog");
            //genMigrator.DoWork(0);
        }

        private void ckBoxIsBacklogRegSave_CheckedChanged(object sender, EventArgs e)
        {
            MigrationSettings.IsSaveBacklogRegData = ckBoxIsBacklogRegSave.Checked;
        }
        private void ckBoxIsBillGenerate_CheckedChanged(object sender, EventArgs e)
        {
            MigrationSettings.IsBillGenerate = ckBoxIsBillGenerate.Checked;
        }

        private void btnBacklogRegMigrat_Click(object sender, EventArgs e)
        {
            DisableButons();
            //BacklogResultMigrator genMigrator = new BacklogResultMigrator("Backlog Registration Migration");
            //genMigrator.DoWork(1);
        }

        private void ckBoxIsBacklogResultSave_CheckedChanged(object sender, EventArgs e)
        {
            MigrationSettings.IsSaveBacklogResultData = ckBoxIsBacklogResultSave.Checked;
        }

        private void btnBacklogResultMigrat_Click(object sender, EventArgs e)
        {
            DisableButons();
            //BacklogResultMigrator genMigrator = new BacklogResultMigrator("Backlog Result Migration");
            //genMigrator.DoWork(2);
        }

        private void gbGenericMigration_Enter(object sender, EventArgs e)
        {

        }

        private void btnRecalculateDropForRetake_Click(object sender, EventArgs e)
        {
            DisableButons();
            GenericResultMigrator genMigrator = new GenericResultMigrator("Recalculate Drop For Retake");
            genMigrator.DoWork(4);
        }

        private void GradeAndMarkMismatch_Click(object sender, EventArgs e)
        {
            DisableButons();
            GenericResultMigrator genMigrator = new GenericResultMigrator("Grade & Mark Mismatch.");
            genMigrator.DoWork(5);
        }

        private void btnDeleteDuplicateStdInClass_Click(object sender, EventArgs e)
        {
            DisableButons();
            GenericResultMigrator genMigrator = new GenericResultMigrator("Delete Duplicate Student In Class.");
            genMigrator.DoWork(6);
        }

        private void btnClassTakenResultUpdate_Click(object sender, EventArgs e)
        {
            DisableButons();
            GenericResultMigrator genMigrator = new GenericResultMigrator("Class Taken By Student Result Update.");
            genMigrator.DoWork(7);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnFeeCodeView_Click(object sender, EventArgs e)
        {
            DisableButons();
            RegenerateSemesterBill debit = new RegenerateSemesterBill("Fee Code Viewer");
            debit.DoWork(8);
        }

        private void btnScholarshipCreateFromResult_Click(object sender, EventArgs e)
        {
            DisableButons();
            RegenerateSemesterBill debit = new RegenerateSemesterBill("Start Scholarship Create From Result");
            debit.DoWork(9);
        }

        private void btnScholarshipRenew_Click(object sender, EventArgs e)
        {
            DisableButons();
            RegenerateSemesterBill debit = new RegenerateSemesterBill("Start Scholarship Renew");
            debit.DoWork(10);
        }

        private void btnUpdateStdJoiningSemester_Click(object sender, EventArgs e)
        {
            DisableButons();
            var debit = new StudentProfileMigrator("Update Student Joining Semester");
            debit.DoWork(4);
        }

        private void btnStudentFeeCodeMap_Click(object sender, EventArgs e)
        {
            DisableButons();
            var debit = new StudentProfileMigrator("Update Student Joining Semester");
            debit.DoWork(5);
        }

        private void btnScholarshipView_Click(object sender, EventArgs e)
        {
            DisableButons();
            RegenerateSemesterBill debit = new RegenerateSemesterBill("Start Scholarship View");
            debit.DoWork(11);
        }






        //void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{

        //    if (e.UserState != null)
        //    {
        //        var log = e.UserState as string;
        //        //richTextBox.Text = log +"\r\n"+ richTextBox.Text;
        //        lblResult.Text = progressBar.Value.ToString();
        //        richTextBox.AppendText(log + "\n");
        //    }

        //    progressBar.Value = e.ProgressPercentage;
        //}
        //public void AppendTextBox(string value)
        //{
        //    if (InvokeRequired)
        //    {
        //        this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
        //        return;
        //    }
        //    richTextBox.Text += value;
        //}



    }
}
