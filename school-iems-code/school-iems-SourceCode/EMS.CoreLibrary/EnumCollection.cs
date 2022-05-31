using System;

namespace EMS.CoreLibrary
{

    public class EnumCollection
    {
        /// <summary>
        /// UserGroupID Admin= 1, Employee= 2, Customer= 3
        /// </summary>

        public class Common
        {
            public enum BloodGroupEnum
            {
                Unknown=0,
                O_Negative = 1,
                O_Positive = 2,
                A_Negative = 3,
                A_Positive = 4,
                B_Negative = 5,
                B_Positive = 6,
                AB_Negative = 7,
                AB_Positive = 8,
            }

            //Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday
            public enum DayEnum
            {
                EveryDay = 0,
                Sunday = 1,
                Monday = 2,
                Tuesday = 3,
                Wednesday = 4,
                Thursday = 5,
                Friday = 6,
                Saturday = 7,
            }
            // January, February, March, April, May, June, July, August, September, October, November, December
            public enum MonthEnum
            {
                //None = 0,
                January = 1,
                February = 2,
                March = 3,
                April = 4,
                May = 5,
                June = 6,
                July = 7,
                August = 8,
                September = 9,
                October = 10,
                November = 11,
                December = 12,
            }
            public enum PrivacyEnum
            {
                Public = 0,
                StudentParentEmployee = 1,
                EmployeeOnly = 2,
                HR_Only = 3,
                Custom = 4
            }
        }

        public enum StudentUserNameSuffixEnum
        {
            CivilStudent = 1,
            MilitaryOfficerStudent = 2,
            BMA_Cadate = 3,
            ForeignStudent = 4
        }

        public enum UserCredentialAuditsTypeEnum
        {
            UnAuthenticate = 0,
            Authenticate = 1
        }


        //[Flags]
        //public enum UserStatusEnum
        //{
        //    NotActive = 0,
        //    Active = 1
        //}
     

        public enum InvigilatorRankEnum
        {
            Lecturer = 0,
            SeniorLecturer = 1,
            AssociateProfessor = 2,
            AssistantProfessor = 3,
            Professor = 4,
            Other = 5
        }
        //public enum FloorEnum
        //{
        //    GroundFloor = 0,
        //    Floor1 = 1,
        //    Floor2 = 2,
        //    Floor3 = 3,
        //    Floor4 = 4,
        //    Floor5 = 5,
        //    Floor6 = 6,
        //    Floor7 = 7,
        //    Floor8 = 8,
        //    Floor9 = 9,
        //    Floor10 = 10,
        //    Floor11 = 11,
        //    Floor12 = 12,
        //    Floor13 = 13,
        //    Floor14 = 14,
        //    Floor15 = 15
        //}
        //public enum CenterStatusEnum
        //{
        //    Available = 0,
        //    NotAvailable = 1,
        //    NotExist = 2
        //}
        //public enum CenterTypeEnum
        //{
        //    University = 0,
        //    College = 1,
        //    School = 2,
        //    SchoolAndCollege = 3,
        //    Other=4
        //}
        /// <summary>
        /// as ems rank database  OfficerCadet = 12,
        /// </summary>
        
       

        public class AdmissionUG
        {
            #region AdmissionUG_Applicant

            public enum JobTypeEnum
            {
                None = 0,
                NonGovernmentService = 1,
                InGovernmentService = 2,
                InBangladeshMilitary = 3,
                PersonalBusiness = 4,
                GovernmentServiceRetired = 5,
                NonGovernmentServiceRetired = 6,
                Other = 7,

            }

            public enum ParentsPrimaryJobStatusEnum
            {
                None = 0,
                NonGovernmentService = 1,
                NonGovernmentServiceRetired = 2,
                InGovernmentService = 3,
                InBangladeshMilitary = 4,
                GovernmentServiceRetired = 5,
                //BangladeshMilitaryRetired = 6
            }

            public enum AdmissionApplicantTypeEnum
            {
                ApplicantThroughExam = 0,
                ApplicantByRule = 1
            }

            public enum EducationBoardEnum
            {
                None = 0,
                Barisal = 1,
                Chittagong = 2,
                Comilla = 3,
                Dhaka = 4,
                Dinajpur = 5,
                Jessore = 6,
                Rajshahi = 7,
                Sylhet = 8,
                Madrasah = 9,
                Technical = 10,
                DIBS = 11
            }

            public enum EducationTypeEnum
            {
                Other = 0,
                General = 1,
                GCE = 2
            }
            public enum ApplicationStatusEnum
            {
                Pending = 0,
                SelectedForExam = 1,
                NotSelectedForExam = 2
            }
            public enum ApplicantSelectionStatusEnum
            {
                NotPublished = 0,
                NotSelectedForAdmission = 1,
                SelectedForAdmission = 2,
                SelectedInWaiting = 3
            }
            public enum AdmissionResultStatusEnum
            {
                NotPublished = 0,
                Passed = 1,
                Failed = 2
            }
            public enum AdmissionStatusEnum
            {
                NotAdmitted = 0,
                Admitted = 1,
                AdmissionWithdraw = 2,
                Terminated = 3,
                FailToAdmitInDueDate = 4
            }

            public enum PreRegistrationStatusEnum
            {
                Incomplete = 0,
                Step1PersonalInfoSaved = 1,
                Step2DepartmentChoiceSaved = 2,
                Step3PaymentTypeSaved = 3,
                Confirmed = 100,
            }
            public enum FormUnitEnum
            {
                None = 0,
                A = 1,
                B = 2,
                AB = 3,
            }
            public enum UnitShiftStatusEnum
            {
                None = 0,
                AToB = 1,
                BToA = 2
            }
            public enum DeptAllotableTypeEnum
            {
                AutoAllotable = 0,
                NotAutoAllotable = 1,
                ForceAllotFirstChoice = 2
            }
            public enum DeptMigUpDownStatusEnum
            {
                None = 0,
                NotChanged = 1,
                Upgrade = 2,
                Downgrade = 3
            }
            public enum QuotaEnum
            {
                General = 0,
                ChildrenOfMilitaryPersonnel = 1,
                ChildrenOfFreedomFighters = 2,
                TribalCitizen = 3,
            }
            public enum ShortQuotaEnum
            {
                GC = 0,
                MW = 1,
                FF = 2,
                TC = 3,
            }
            public enum ExamAttendanceStatusEnum
            {
                None = 0,
                Present = 1,
                Absent = 2,
                Cancel = 3,
            }
            public enum CsvFileTypeEnum
            {
                Any = 0,
                GeneralApplicant = 1,
                GceApplicant = 2
            }
            #endregion AdmissionUG_Applicant

            #region AdmissionUG_ExamSetting
            //public enum ExamLevelTypeEnum
            //{
            //    UnderGraduate = 0,
            //    Graduate = 1,
            //    PostGraduate = 2
            //}
            //public enum ExamTypeEnum
            //{
            //    Written = 0,
            //    Viva = 1,
            //    Online = 2
            //}
            //public enum CircularStatusEnum
            //{
            //    Upcoming = 0,
            //    Current = 1,
            //    Expired = 2,
            //    Invalid = 3,

            //}
            //public enum ReportEnum
            //{
            //    AllReports = 0,
            //    UgAdm001 = 1,
            //    UgAdm002 = 2,
            //    UgAdm003 = 3,
            //    UgAdm004 = 4,
            //    UgAdm005 = 5,
            //    UgAdm006 = 6,
            //    Ugoh001 = 7,
            //    MainForm = 8,
            //    MainForm2 = 9

            //}


            #endregion AdmissionUG_ExamSetting
        }

        public class Accounting
        {
            public enum OnlinePaymentStatusEnum
            {

                Initiated = 0,
                Success = 1,
                Failed = 2,
                InvalidInitiateTry = 4,
                InvalidSuccessTry = 5,
            }
            //public enum TransactionStatusEnum
            //{
            //    Initial = 0,
            //    ProceedToBank = 1,
            //    Sussess = 2,
            //    Failed = 3
            //}
        }


        #region CMS
        //public enum PostTypeEnum
        //{
        //    Attachment = 0,
        //    Post = 1,
        //    Page = 2,
        //    NavMenuItem = 3,
        //    Feedback = 4
        //}
        //public enum PostStatusEnum
        //{
        //    UnPublished=0,
        //    Published=1,
        //    PendingReview=2,
        //    Private=3,
        //    Protected=4
        //}
        //public enum CommentStatusEnum
        //{
        //    Closed=0,
        //    Public=1,
        //    Private=2
        //}
        //public enum Priority 
        //{
        //    High=0,
        //    Normal=1,
        //    Low=2
        //}
        //public enum OrderTypeEnum
        //{
        //    Ascending = 0,
        //    Descendion = 1
        //}
        //public enum CommentingStatusEnum
        //{
        //    Hold = 0,
        //    Approve = 1,
        //    Spam = 2,
        //    Trash = 3
        //}
        //public enum ImageSize
        //{
        //    Thumb = 1,
        //    Small = 2,
        //    Medium = 3,
        //    Large = 4
        //}
        #endregion CMS
        public enum UsersAreaPermissionEnum
        {
            AdminArea = 1,
            HumanResourceArea = 2,
            AdmissionArea = 3,
            AcademicArea = 4,
            LibraryArea = 5,
            InventoryArea = 6,
            AccountingArea = 7,
        }
    }
}
