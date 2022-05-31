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
        [Permission(4)]
        public sealed class AcademicArea
        {
            public const int CanViewDashboard = 4;
            public const int AllDepartmentManager = 41;
            [Permission(401)]
            public sealed class ProgramManager
            {
                public const int CanView = 40101;
                public const int CanAdd = 40102;
                public const int CanEdit = 40103;
                public const int CanDelete = 40104;
            }
            [Permission(402)]
            public sealed class CurriculumManager
            {
                [Permission(40201)]
                public sealed class Curriculum
                {
                    public const int CanView = 4020101;
                    public const int CanAdd = 4020102;
                    public const int CanEdit = 4020103;
                    public const int CanDelete = 4020104;
                    public const int CanDuplicateCurriculumWithCourses = 4020105;

                }
                [Permission(40202)]
                public sealed class CurriculumCourse
                {
                    public const int CanView = 4020201;
                    public const int CanAdd = 4020202;
                    public const int CanEdit = 4020203;
                    public const int CanDelete = 4020204;

                    public const int CanForceEdit = 4020205;
                    public const int CanAddPreRequisite = 4020206;
                    public const int CanDeletePreRequisite = 4020207;
                }
                [Permission(40203)]
                public sealed class BaseCurriculumCourse
                {
                    public const int CanView = 4020301;
                    public const int CanAdd = 4020302;
                    public const int CanEdit = 4020303;
                    public const int CanDelete = 4020304;

                    public const int CanForceEdit = 4020305;
                }
                [Permission(40204)]
                public sealed class CurriculumElectiveGroup
                {
                    public const int CanView = 4020401;
                    public const int CanAdd = 4020402;
                    public const int CanEdit = 4020403;
                    public const int CanDelete = 4020404;
                }
            }
            [Permission(403)]
            public sealed class SemesterManager
            {
                [Permission(40301)]
                public sealed class Semester
                {
                    public const int CanView = 4030101;
                    public const int CanAdd = 4030102;
                    public const int CanEdit = 4030103;
                    public const int CanDelete = 4030104;
                }

                [Permission(40302)]
                public sealed class StudyLevelTerm
                {
                    public const int CanView = 4030201;
                    public const int CanAdd = 4030202;
                    public const int CanEdit = 4030203;
                    public const int CanDelete = 4030204;
                }

                //[Permission(40303)]
                //public sealed class StudyLevel
                //{
                //    public const int CanView = 4090201;
                //    public const int CanAdd = 4090202;
                //    public const int CanEdit = 4090203;
                //    public const int CanDelete = 4090204;
                //}
                //[Permission(40303)]
                //public sealed class StudyTerm
                //{
                //    public const int CanView = 4090201;
                //    public const int CanAdd = 4090202;
                //    public const int CanEdit = 4090203;
                //    public const int CanDelete = 4090204;
                //}
            }
            //[Permission(404)]
            //public sealed class OfferedCourseManager
            //{
            //    public const int CanView = 40401;
            //    public const int CanAdd = 40402;
            //    public const int CanEdit = 40403;
            //    public const int CanDelete = 40404;
            //}
            [Permission(405)]
            public sealed class OfferedClassManager
            {
                [Permission(40501)]
                public sealed class OfferedClass
                {
                    public const int CanView = 4050101;
                    public const int CanAdd = 4050102;
                    public const int CanEdit = 4050103;
                    public const int CanDelete = 4050104;
                    public const int CanForceEdit = 4050105;
                    public const int CanOfferAnotherSection = 4050106;
                    public const int CanOfferedFromPreviousSemester = 4050107;
                    public const int CanViewReport = 4050108;
                    public const int CanOfferBulkCourse = 4050109;
                    public const int CanTrashUnTrash = 4050110;
                    public const int CanForceDelete = 4050111;

                }
                [Permission(40502)]//StudentCourseRegistration
                public sealed class StudentCourseRegistration
                {
                    public const int CanView = 4050201;
                    public const int CanAdd = 4050202;
                    public const int CanEdit = 4050203;
                    public const int CanPermanentDelete = 4050204;
                    public const int CanDrop = 4050205;
                    public const int CanUnDrop = 4050206;
                    public const int CanCancel = 4050207;
                    public const int CanUnCancel = 4050208;
                    public const int CanViewReport = 4050209;
                    public const int CanAddFromBulkRegistration = 4050210;
                    public const int CanAddAsRetakeOrImprovement = 4050211;
                    public const int CanAddCompletedCourse = 4050212;
                    public const int CanDropUnDropCompletedSemester = 4050213;
                    public const int CanCancelUnCancelCompletedSemester = 4050214;
                }
                [Permission(40503)]
                public sealed class TeacherEnrollment
                {
                    public const int CanView = 4050301;
                    public const int CanAdd = 4050302;
                    public const int CanEdit = 4050303;
                    public const int CanDelete = 4050304;
                    //[Permission(4050301)]
                    //public sealed class Faculty
                    //{
                    //    public const int CanView = 405030101;
                    //    public const int CanAdd = 405030102;
                    //    public const int CanEdit = 405030103;
                    //    public const int CanDelete = 405030104;
                    //}
                    //[Permission(4050302)]
                    //public sealed class Examiner
                    //{
                    //    public const int CanView = 405030201;
                    //    public const int CanAdd = 405030202;
                    //    public const int CanEdit = 405030203;
                    //    public const int CanDelete = 405030204;
                    //}
                    //[Permission(4050303)]
                    //public sealed class Scrutinizer
                    //{
                    //    public const int CanView = 405030301;
                    //    public const int CanAdd = 405030302;
                    //    public const int CanEdit = 405030303;
                    //    public const int CanDelete = 405030304;
                    //}
                }
                [Permission(40504)]
                public sealed class ClassResultManager
                {
                    [Permission(4050401)]
                    public sealed class ClassResult
                    {
                        public const int CanViewAllMarks = 405040101;
                        public const int CanUpdateMarks = 405040102;
                        public const int CanDeleteMarks = 405040103;
                        public const int CanPrint30PercentMarks = 405040104;
                        public const int CanPrint70PercentMarks = 405040105;
                        public const int CanPrint100PercentMarks = 405040106;
                    }

                    [Permission(4050402)]
                    public sealed class ClassResultSettings
                    {
                        public const int CanView = 405040201;
                        public const int CanAdd = 405040202;
                        public const int CanUpdate = 405040203;
                        public const int CanDelete = 405040204;
                        public const int CanLockUnlock = 405040205;
                    }
                }

                [Permission(40505)]
                public sealed class ClassRoutine
                {
                    public const int CanView = 4050501;
                    public const int CanAdd = 4050502;
                    public const int CanEdit = 4050503;
                    public const int CanDelete = 4050504;
                    public const int CanAllocateAnyRoom = 4050505;
                }


                [Permission(40506)]
                public sealed class ClassAttendance
                {
                    public const int CanViewAttendance = 4050601;
                    public const int CanAddEditAttendance = 4050602;
                    public const int CanDeleteAttendance = 4050604;
                    public const int CanDeleteAttendanceSmsLog = 4050605;
                }

            }
            //[Permission(406)]
            //public sealed class PolicyManager
            //{
            //    [Permission(40601)]
            //    public sealed class MarkDistributionPolicy
            //    {
            //        public const int CanView = 4060101;
            //        public const int CanAdd = 4060102;
            //        public const int CanEdit = 4060103;
            //        public const int CanDelete = 4060104;
            //    }
            //    [Permission(40602)]
            //    public sealed class GradingPolicy
            //    {
            //        public const int CanView = 4060201;
            //        public const int CanAdd = 4060202;
            //        public const int CanEdit = 4060203;
            //        public const int CanDelete = 4060204;
            //    }
            //}

        }
    }
}
