using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Framework.Attributes;

namespace EMS.Framework.Permissions
{
    public partial class PermissionCollection
    {
        [Permission(8)]
        public sealed class ExamArea
        {
            public const int CanViewDashboard = 8;
            public const int AllDepartmentManager = 81;

            //801
            [Permission(40604)]
            public sealed class ClassResultManager
            {
                [Permission(4060401)]
                public sealed class ClassResult
                {
                    public const int CanViewClassMarks = 406040101;
                    public const int CanUpdateMarks = 406040102;
                    public const int CanGenerateFinalGrade = 406040103;
                    public const int CanPrintMarksSheet = 406040104;
                    //public const int CanPrint70PercentMarks = 406040105;
                    //public const int CanPrint100PercentMarks = 406040106;
                    public const int ResultSubmissionReportAudit = 406040105;
                    public const int ResultChangeHistory = 406040106;
                    public const int CanDeleteMarks = 406040107; // Not Use
                    public const int CanUseSingleScreenMarkEntry = 406040108; // todo Temporary Use for Testing

                }

                [Permission(4060402)]
                public sealed class ClassResultMarkSettings
                {
                    public const int CanView = 406040201;
                    public const int CanAdd = 406040202;
                    public const int CanUpdate = 406040203;
                    public const int CanDelete = 406040204;
                    public const int CanLockUnlock = 406040205;
                }
            }
            //802
            [Permission(407)]
            public sealed class PolicyManager
            {
                [Permission(40701)]
                public sealed class MarkDistributionPolicy
                {
                    public const int CanView = 4070101;
                    public const int CanAdd = 4070102;
                    public const int CanEdit = 4070103;
                    public const int CanDelete = 4070104;
                }
                [Permission(40702)]
                public sealed class GradingPolicy
                {
                    public const int CanView = 4070201;
                    public const int CanAdd = 4070202;
                    public const int CanEdit = 4070203;
                    public const int CanDelete = 4070204;
                }
            }
            //803
            [Permission(408)]
            public sealed class ExamManager
            {
                [Permission(40801)]
                public sealed class Exam
                {
                    public const int CanView = 4080101;
                    public const int CanAdd = 4080102;
                    public const int CanEdit = 4080103;
                    public const int CanDelete = 4080104;
                    public const int CanUploadExamRollCSV = 4080105;
                    public const int CanPrintAdmitCard = 4080106;
                    public const int CanPrintExamAttendanceSheet = 4080107;
                    public const int CanPrintDegreeCompleteReport = 4080108;
                    public const int CanPrintGradeSummarySheet = 4080109;
                    public const int CanEditResultPublishingSettings = 4080110;
                }

            }
            //804
            [Permission(804)]
            public sealed class TranscriptCertificateManager
            {
                
                public const int CanViewStudentGradeSheet = 80401;
                public const int CanEditMarkGrade = 80402;
                public const int CanUpdateCertificateInfo = 80403;
                public const int CanPrintOfficialCertificate = 80404;
                public const int CanPrintUnofficialCertificate = 80405;
                public const int CanPrintOfficialTranscript = 80406;
                public const int CanPrintUnofficialTranscript = 80407;
                public const int CanPrintTestimonial = 80408;
                public const int CanRecalculateCGPA = 80409;
                public const int CanAutoRecalculateDropRetake = 80410;

            }

            //805
            [Permission(805)]
            public sealed class CreditTransferManager
            {
                [Permission(80501)]
                public sealed class CreditTransfer
                {
                    public const int CanView = 8050101;
                    public const int CanAdd = 8050102;
                    public const int CanEdit = 8050103;
                    public const int CanDelete = 8050104;
                }
                [Permission(80502)]
                public sealed class CreditTransferCourses
                {
                    public const int CanView = 8050201;
                    public const int CanAdd = 8050202;
                    public const int CanEdit = 8050203;
                    public const int CanDelete = 8050204;
                }
            }

            [Permission(806)]
            public sealed class SuppleManager
            {

                public const int CanView = 80601;
                public const int CanApply = 80602; // Pending
                public const int CanApprove = 80603; 
                public const int CanDisapprove = 80604; 
                public const int CanCancel = 80605;

                public const int CanAddExaminer = 80606;
                public const int CanTrashExaminer = 80607;
                public const int CanUnTrashExaminer = 80608;
                public const int CanPrintAttendanceSheet = 80609;
                public const int CanPrintAdmitCard = 80610;

                public const int CanMarkInputLock = 80611;
                public const int CanMarkInputUnlock = 80612;
                public const int CanGenerateGrades = 80613;
                
            }

            [Permission(807)]
            public sealed class ResultBlockUnblock
            {
                public const int CanView = 80701;
                public const int CanAdd = 80702;
                public const int CanEdit = 80703;
                public const int CanDelete = 80704;
            }



        }
    }
}
