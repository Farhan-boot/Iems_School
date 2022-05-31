using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Framework.Attributes;

namespace EMS.Framework.Permissions
{
    public partial class PermissionCollection
    {
        [Permission(3)]
        public sealed class AdmissionArea
        {
            public const int CanViewDashboard = 3;
            [Permission(301)]
            public sealed class StudentManager
            {

                [Permission(30101)]
                public sealed class Student
                {
                    public const int CanView = 3010101;
                    public const int CanAdd = 3010102;
                    public const int CanEdit = 3010103;
                    public const int CanDelete = 3010104;
                    public const int CanTrash = 3010105;

                    public const int CanConfirmAdmission = 3010106;
                    public const int CanCancelAdmission = 3010107;
                    public const int CanResetPassword = 3010108;

                    public const int CanChangeUserName = 3010109;
                    public const int CanChangeClassRoll = 3010110;

                    public const int CanChangeProgram = 3010111;

                    public const int CanPrintForm = 3010112;

                    public const int CanPrintSlip = 3010113;

                    public const int CanViewPassword = 3010114;
                    public const int CanSendEmail = 3010115;
                    public const int CanUploadProfilePicture = 3010116;
                    public const int CanRemoveProfilePicture = 3010117;
                    public const int CanChangeStudentName = 3010118;
                    public const int CanChangeCurriculum = 3010119;
                    public const int CanChangeFeeCode = 3010120;
                    public const int CanLoginToPortal = 3010121;
                    public const int CanViewLog = 3010122;
                    //public const int CanViewGradeSheet = 3010113;
                    //public const int CanEditMarkGrade = 3010114;
                    //public const int CanUpdateCertificateInfo = 3010115;
                    //public const int CanPrintOfficialCertificate = 3010116;
                    //public const int CanPrintUnofficialCertificate = 3010117;
                    //public const int CanPrintOfficialTranscript = 3010118;
                    //public const int CanPrintUnofficialTranscript = 3010119;
                    //public const int CanPrintTestimonial = 3010120;


                }

                [Permission(30102)]
                public sealed class Education
                {
                    public const int CanView = 3010201;
                    public const int CanAdd = 3010202;
                    public const int CanEdit = 3010203;
                    public const int CanDelete = 3010204;

                }
                [Permission(30103)]
                public sealed class Report
                {
                    public const int CanViewPreviousEducationReport = 3010301;
                    public const int CanViewProfileChangeAuditReport = 3010302;
                }
            }

            [Permission(302)]
            public sealed class AdmissionExam
            {
                public const int CanView = 30201;
                public const int CanAdd = 30202;
                public const int CanEdit = 30203;
                public const int CanDelete = 30204;
            }


            //[Permission(303)]
            //public sealed class ExamCenter
            //{
            //    public const int CanView = 30301;
            //    public const int CanAdd = 30302;
            //    public const int CanEdit = 30303;
            //    public const int CanDelete = 30304;
            //}
            //[Permission(304)]
            //public sealed class ExamCenterBuilding
            //{
            //    public const int CanView = 30401;
            //    public const int CanAdd = 30402;
            //    public const int CanEdit = 30403;
            //    public const int CanDelete = 30404;
            //}
            //[Permission(305)]
            //public sealed class ExamCenterRoom
            //{
            //    public const int CanView = 30501;
            //    public const int CanAdd = 30502;
            //    public const int CanEdit = 30503;
            //    public const int CanDelete = 30504;
            //}
            //[Permission(306)]
            //public sealed class ExamCenterInvigilator
            //{
            //    public const int CanView = 30601;
            //    public const int CanAdd = 30602;
            //    public const int CanEdit = 30603;
            //    public const int CanDelete = 30604;
            //}

            [Permission(307)]
            public sealed class DegreeCategory
            {
                public const int CanView = 30701;
                public const int CanAdd = 30702;
                public const int CanEdit = 30703;
                public const int CanDelete = 30704;
            }

            [Permission(308)]
            public sealed class EducationBoard
            {
                public const int CanView = 30801;
                public const int CanAdd = 30802;
                public const int CanEdit = 30803;
                public const int CanDelete = 30804;
            }

            //[Permission(309)]
            //public sealed class AdmissionExam
            //{
            //    public const int CanView = 30901;
            //    public const int CanAdd = 30902;
            //    public const int CanEdit = 30903;
            //    public const int CanDelete = 30904;
            //}
            [Permission(310)]
            public sealed class StudentQuotaName
            {
                public const int CanView = 31001;
                public const int CanAdd = 31002;
                public const int CanEdit = 31003;
                public const int CanDelete = 31004;
            }

            [Permission(311)]
            public sealed class ParentsPrimaryJobType
            {
                public const int CanView = 31101;
                public const int CanAdd = 31102;
                public const int CanEdit = 31103;
                public const int CanDelete = 31104;
            }
            [Permission(312)]
            public sealed class Report
            {
                public const int CanViewAdmissionSummary = 31201;
                public const int CanViewUgcStaticAdmissionReport = 31299;

                //public const int CanEdit = 31103;
                //public const int CanDelete = 31104;

                //public const int CanPrint30PercentMarks = 406040104;
                //public const int CanPrint70PercentMarks = 406040105;
                //public const int CanPrint100PercentMarks = 406040106;
            }

            

        }
    }
}


