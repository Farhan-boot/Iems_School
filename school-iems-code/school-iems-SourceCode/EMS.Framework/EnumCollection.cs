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

        }
        [Flags]
        public enum UserTypeEnum
        {
            None = 0,
            Student = 1,
            Faculty = 2,
            Admin = 3,
            Officer = 4,
            Staff = 5,
            Applicant = 6,
            Invalid = -1
        }

        [Flags]
        public enum UserAuthorizeTypeEnum
        {
            None = 0,
            Student = 1,
            Employee = 2,
            Admin = 3,
            Applicant = 4
        }
        public enum GenderEnum
        {
            Male = 0,
            Female = 1,
            Other = 3
        }
        public enum NationalityEnum
        {
            Bangladeshi = 0,
            Foreign = 300,

        }
        public enum JobTypeEnum
        {
            None = 0,
            GovernmentService = 1,
            SemiGovernmentService = 2,
            PrivateService = 3,
            Businessman = 4,
            BangladeshMilitary = 5,
            SelfEmployed = 6,
            Housemaid = 7,
            Retired=8,
            UnEmployed = 9,

        }
        public enum ReligionTypeEnum
        {
            Islam = 0,
            Christianity = 1,
            Hinduism = 2,
            Buddhism = 3,
            Nonreligious = 4,
            Other = 100
        }

        public enum BloodGroupEnum
        {
            O_Negetive = 0,
            O_Positive = 1,
            A_Negetive = 2,
            A_Positive = 3,
            B_Negetive = 4,
            B_Positive = 5,
            AB_Negetive = 6,
            AB_Positive = 7,
            Other = 8
        }
        public enum MaritalStatusEnum
        {
            Single = 0,
            Married = 1,
            Separated = 2,
            Divorced = 3,
            Widowed = 4,
            Other = 5
        }

        public class AdmissionUG
        {
            public enum ApplicationTypeEnum
            {
                General = 0,
                Gce = 1,

            }
            public enum ApplicationStatusEnum
            {
                None = 0,
                Applied = 1,
                SelectedForExam = 2,
                NotSelectedForExam = 3
            }
            public enum ApplicantStatusEnum
            {
                NotSelected = 0,
                SelectedForAdmission = 1,
                InWaiting = 3
            }
            public enum FormUnitEnum
            {
                A = 1,
                B = 2,
                AB = 3
            }

            public enum QuotaEnum
            {
                General = 0,
                ChildrenOfMilitaryPersonnel = 1,
                ChildrenOfFreedomFighters = 2,
                TribalCitizen = 3,
                ForeignStudents = 4
            }

            public enum AdmissionResultStatusEnum
            {
                None = 0,
                Passed=1,
                Failed=2,
            }

            public enum AdmissionStatusEnum
            {
                None=0,
                Admitted=1,
                NotAdmitted=2
            }

            public enum UploadCsvFileTypeEnum
            {
                Any=0,
                GenApplicant=1,
                GceApplicant=2
            }
        }

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
    }
}
