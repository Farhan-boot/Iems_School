using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HIMS.UMS.Entities;
using HIMS.UMS.Reports.StudentReports.GradeStatusByCurriculumReport.ReportFiles;
using HIMS.UMS.Reports.StudentReports.GradeStatusByCurriculumReport.Source;

namespace HIMS.UMS.Reports.StudentReports.GradeStatusByCurriculumReport
{
    public class GradeStatusByCurriculumReportManager
    {
        private readonly Users _student;
        private readonly IList<CourseTakenByStudent> _courses;
        private readonly IList<CourseMap> _coreMap;
        private readonly IList<CourseMap> _majorMap;
        private readonly IList<CourseMap> _secondMajorMap;
        private readonly IList<CourseMap> _minorMap;
        private readonly IList<CourseMap> _electiveMap;
        private readonly IList<TransferedCourse> _transferedCourses;
        private readonly IList<TransferredStudent> _transferredStudent;

        public GradeStatusByCurriculumReportManager(Entities.Users student, IEnumerable<CourseTakenByStudent> courses,
            IList<Entities.CourseMap> coreMap,
            IList<Entities.CourseMap> majorMap,
            IList<Entities.CourseMap> secondMajorMap,
            IList<Entities.CourseMap> minorMap,
            IList<Entities.CourseMap> electiveMap,
            IList<TransferedCourse> transferedCourses,
            IList<TransferredStudent> transferredStudent
            )
        {
            _student = student;
            _courses = new List<CourseTakenByStudent>(courses);
            _coreMap = coreMap;
            _majorMap = majorMap;
            _secondMajorMap = secondMajorMap;
            _minorMap = minorMap;
            _electiveMap = electiveMap;
            _transferedCourses = transferedCourses;
            _transferredStudent = transferredStudent;
        }

        float GetTransfarredCredit(IList<CourseMap> maps)
        {
            if (maps == null)
                return 0;

            float totalTransfarredCredit = 0;
            foreach (var tc in _transferedCourses)
            {
                if (maps.All(m => m.CourseID != tc.CourseID))
                    continue;

                totalTransfarredCredit += tc.CourseIDSource.LecCredit;
            }
            return totalTransfarredCredit;
        }

        float GetCreditCompleted(IEnumerable<CourseMap> maps)
        {
            if (maps == null)
                return 0;

            float totalCreditCompleted = 0;
            foreach (CourseTakenByStudent ctbs in _courses)
            {
                if (maps.All(m => m.CourseID != ctbs.CourseID))
                    continue;

                //If Course Not Dropped , Not NC, Not Retaken. Then Count the Credit.
                if (ctbs.Dropped || ctbs.NonCredit || ctbs.Retake || ctbs.UnofficialWithdraw)
                    continue;

                //If the Course Leccredit is Zero count it as NC.
                if (ctbs.CourseIDSource.LecCredit == 0)
                    continue;

                if (ctbs.FinalGrade == null)
                    continue;

                if (!ctbs.FinalGrade.IsPassed())
                    continue;

                totalCreditCompleted += ctbs.CourseIDSource.LecCredit;
            }

            return totalCreditCompleted;
        }

        public object GetReport()
        {
            var dataset = new dsGradeStatusByCurriculumReport();

            /*Fill Core Curriculum*/
            float coreCreditCompleted = GetCreditCompleted(_coreMap);
            float coreTransfarred = GetTransfarredCredit(_coreMap);
            foreach (CourseMap courseMap in _coreMap.OrderBy(o => o.Semester))
            {
                dsGradeStatusByCurriculumReport.MainRow row = dataset.Main.NewMainRow();
                row.ID = Guid.NewGuid();
                row.CurriculumName = "Core Curriculum";
                FillStudentInfo(row);
                FillCourseMap(row, courseMap);

             
                dataset.Main.AddMainRow(row);
            }

            float majorCreditCompleted = GetCreditCompleted(_majorMap);
            float majorTransfarred = GetTransfarredCredit(_majorMap);

            if (_majorMap != null)
            {
               
                /*Fill Major Curriculum*/
                foreach (CourseMap courseMap in _majorMap)
                {
                    dsGradeStatusByCurriculumReport.MainRow row = dataset.Main.NewMainRow();
                    row.ID = Guid.NewGuid();
                    if (_student.Student.ProgramID == 4177)
                    {
                        row.CurriculumName = "Specialization Curriculum";
                    }
                    else
                    {
                        row.CurriculumName = "Major Curriculum";
                    }                    
                    FillStudentInfo(row);
                    FillCourseMap(row, courseMap);

                    
                    row.SemesterNo = 0;
                    dataset.Main.AddMainRow(row);

                }
            }

            float secondMajorCreditCompleted = GetCreditCompleted(_secondMajorMap);
            float secondMajorTransfarred = GetTransfarredCredit(_secondMajorMap);

            if (_secondMajorMap != null)
            {
               
                /*Fill Second Major Curriculum*/
                foreach (CourseMap courseMap in _secondMajorMap)
                {
                    dsGradeStatusByCurriculumReport.MainRow row = dataset.Main.NewMainRow();
                    row.ID = Guid.NewGuid();
                    row.CurriculumName = "Second Major Curriculum";
                    FillStudentInfo(row);
                    FillCourseMap(row, courseMap);

                  
                    row.SemesterNo = 0;
                    dataset.Main.AddMainRow(row);
                }
            }

            float minorCreditCompleted = GetCreditCompleted(_minorMap);
            float minorMajorTransfarred = GetTransfarredCredit(_minorMap);

            if (_minorMap != null)
            {
               
                /*Fill Minor Curriculum*/
                foreach (CourseMap courseMap in _minorMap)
                {
                    dsGradeStatusByCurriculumReport.MainRow row = dataset.Main.NewMainRow();
                    row.ID = Guid.NewGuid();
                    row.CurriculumName = "Minor Curriculum";
                    FillStudentInfo(row);
                    FillCourseMap(row, courseMap);

               
                    row.SemesterNo = 0;
                    dataset.Main.AddMainRow(row);
                }
            }

            float electiveCreditCompleted = GetCreditCompleted(_electiveMap);
            float electiveTransfarred = GetTransfarredCredit(_electiveMap);

            if (_electiveMap != null)
            {
               
                /*Fill Elective Curriculum*/
                foreach (CourseMap courseMap in _electiveMap)
                {
                    dsGradeStatusByCurriculumReport.MainRow row = dataset.Main.NewMainRow();
                    row.ID = Guid.NewGuid();
                    row.CurriculumName = "Elective Curriculum";
                    FillStudentInfo(row);
                    FillCourseMap(row, courseMap);

                  
                    row.SemesterNo = 0;
                    dataset.Main.AddMainRow(row);
                }
            }


            foreach (CourseTakenByStudent ctbs in _courses)
            {
                dsGradeStatusByCurriculumReport.MainRow unmappedrow = dataset.Main.NewMainRow();
                FillStudentInfo(unmappedrow);
                unmappedrow.ID = Guid.NewGuid();
                unmappedrow.CurriculumName = "Unmapped";
                unmappedrow.CourseCode = "-";
                unmappedrow.CourseName = ctbs.AttendedCourseIDSource.Title;
                unmappedrow.SemesterNo = 0;
                unmappedrow.CurriculumID = 0;
                unmappedrow.AttendedCourseDetail = string.Format("{0} [{1}]", ctbs.SemesterIDSource.Title, GetGrade(ctbs));
                dataset.Main.AddMainRow(unmappedrow);
            }

            foreach (dsGradeStatusByCurriculumReport.MainRow row in dataset.Main.Rows)
            {
                if (_student.Student.CoreCurriculum != null)
                {
                    row.CoreCurriculumRequiredCredit = _student.Student.CoreCurriculum.CreditRequireForGraduation.Value.ToString();
                    row.CoreCurriculumCreditCompleted = coreCreditCompleted.ToString();
                    row.CoreCurriculumTransfarred = coreTransfarred.ToString();
                    row.CoreCurriculumRemaining = (_student.Student.CoreCurriculum.CreditRequireForGraduation.Value - coreCreditCompleted - coreTransfarred).ToString();
                }

                if (_student.Student.FirstMajorCurriculum != null)
                {
                    row.MajorCurriculumRequiedCredit = _student.Student.FirstMajorCurriculum.CreditRequireForGraduation.Value.ToString();
                    row.MajorCurriculumCreditCompleted = majorCreditCompleted.ToString();
                    row.MajorCurriculumTransfarred = majorTransfarred.ToString();
                    row.MajorCurriculumRemaining = (_student.Student.FirstMajorCurriculum.CreditRequireForGraduation.Value - majorCreditCompleted - majorTransfarred).ToString();
                }

                if (_student.Student.SecondMajorCurriculum != null)
                {
                    row.SecondMajorCurriculumRequiredCredit = _student.Student.SecondMajorCurriculum.CreditRequireForGraduation.Value.ToString();
                    row.SecondMajorCurriculumCreditComplated = secondMajorCreditCompleted.ToString();
                    row.SecondMajorCurriculumTransfarred = secondMajorTransfarred.ToString();
                    row.SecondMajorCurriculumRemaining = (_student.Student.SecondMajorCurriculum.CreditRequireForGraduation.Value - secondMajorCreditCompleted - secondMajorTransfarred).ToString();
                }

                if (_student.Student.MinorCurriculum != null)
                {
                    row.MinorCurriculumRequiredCredit = _student.Student.MinorCurriculum.CreditRequireForGraduation.Value.ToString();
                    row.MinorCurriculumCreditCompleted = minorCreditCompleted.ToString();
                    row.MinorCurriculumTransfarred = minorMajorTransfarred.ToString();
                    row.MinorCurriculumRemaining = (_student.Student.MinorCurriculum.CreditRequireForGraduation.Value - minorCreditCompleted - minorMajorTransfarred).ToString();
                }

                if (_student.Student.ElectiveCurriculum != null)
                {
                    row.ElectiveCurriculumRequiredCredit = _student.Student.ElectiveCurriculum.CreditRequireForGraduation.Value.ToString();
                    row.ElectiveCurriculumCreditCompleted = electiveCreditCompleted.ToString();
                    row.ElectiveCurriculumTransfarred = electiveTransfarred.ToString();
                    row.ElectiveCurriculumRemaining = (_student.Student.ElectiveCurriculum.CreditRequireForGraduation.Value - electiveCreditCompleted - electiveTransfarred).ToString();
                }

            }

            var report = new rptGradeStatusByCurriculumReport();
            report.DataSource = dataset;
            return report;
        }

        void FillStudentInfo(dsGradeStatusByCurriculumReport.MainRow courseRow)
        {
            courseRow.StudentID = _student.Student.StudentID;
            courseRow.StudentName = _student.UserFullName;
            courseRow.CGPA = _student.Student.CGPA;
            courseRow.CreditCompleted = _student.Student.CreditCompleted;
            courseRow.CourseCompleted = _student.Student.CourseCompleted;

            courseRow.CoreCurriculum = _student.Student.CoreCurriculum != null ?
               _student.Student.CoreCurriculum.Title : "None";

            courseRow.MajorCurriculum = _student.Student.FirstMajorCurriculum != null ?
                _student.Student.FirstMajorCurriculum.Title : "None";

            courseRow.SecondMajorCurriculum = _student.Student.SecondMajorCurriculum != null ?
                _student.Student.SecondMajorCurriculum.Title : "None";

            courseRow.MinorCurriculum = _student.Student.MinorCurriculum != null ?
                _student.Student.MinorCurriculum.Title : "None";

            courseRow.ElectiveCurriculum = _student.Student.ElectiveCurriculum != null ?
                _student.Student.ElectiveCurriculum.Title : "None";

            courseRow.MajorCurriculumTitle = _student.Student.ProgramID == 4177 ? "Specialization" : "Major";  
        }

        void FillCourseMap(dsGradeStatusByCurriculumReport.MainRow row, CourseMap courseMap)
        {
            row.CurriculumID = courseMap.CurriculumID;
            row.SemesterNo = courseMap.Semester;
            row.CourseCode = courseMap.CourseCode;
            row.CourseName = courseMap.Title;

            FillResult(courseMap, row);
        }

        private void FillResult(CourseMap courseMap, dsGradeStatusByCurriculumReport.MainRow row)
        {
            string auditRemark = string.Empty;

            var info = new StringBuilder("");

            IList<CourseTakenByStudent> coursesToRemove = new List<CourseTakenByStudent>();
            foreach (var ctbs in _courses.Where(c => c.CourseID == courseMap.CourseID))
            {
                info.Append((ctbs.CourseID != ctbs.AttendedCourseID ? ctbs.AttendedCourseIDSource.Title : "")
                               + " (" + ctbs.SemesterIDSource.Title + ")" + " [" + GetGrade(ctbs) + "]" + Environment.NewLine);

                coursesToRemove.Add(ctbs);
            }

            bool hasTransfarred = _transferedCourses.Any(t => t.CourseID == courseMap.CourseID);
            if (hasTransfarred)
            {
                row.CourseCompletedStatus = 1;
            }
            else
            {
                foreach (var ctbs in _courses.Where(c => c.CourseID == courseMap.CourseID))
                {
                    if (ctbs.Dropped)
                        continue;

                    if (!ctbs.TotalCourseMark.HasValue || ctbs.Result == null)
                        continue;

                    if (!ctbs.FinalGrade.IsPassed())
                        continue;

                    row.CourseCompletedStatus = 1;

                    if (ctbs.TotalCourseMark != ctbs.Result.TotalMark)
                    {
                        row.CourseRemark += "Mark Mismatch;";
                        row.CourseCompletedStatus = 0;
                    }

                    if ( ctbs.Result.ResultSubmissionStatus==null || ctbs.Result.ResultSubmissionStatus.ResultSubmissionStatusStatus != ResultSubmissionStatus.ResultSubmissionStatusEnum.Confirmed)
                    {
                        row.CourseRemark += "Submission Problem;";
                        row.CourseCompletedStatus = 0;
                    }

                    //check audit lock
                    if (!ctbs.Result.IsLocked.HasValue || !ctbs.Result.IsLocked.Value)
                    {
                        row.Audited = "N";
                        if (string.IsNullOrEmpty(auditRemark))
                            auditRemark = "- Some Course(s) not Audited";
                    }
                    else
                    {
                        row.Audited = "Y";
                    }
                }
            }

            if (!string.IsNullOrEmpty(auditRemark))
                row.Remark += auditRemark;
            else
                row.Remark += "- Audited and Locked";

            foreach (var obj in coursesToRemove)
                _courses.Remove(obj);

            //it means course not taken, check if there is any tranfarred courses.
            info.Append(GetTransferredCourse(courseMap.CourseID));

            row.AttendedCourseDetail = info.ToString();
        }

        private string GetTransferredCourse(int courseID)
        {
            if (_transferedCourses == null) return string.Empty;

            var info = new StringBuilder("");
            foreach (TransferedCourse tc in _transferedCourses.Where(t => t.CourseID == courseID))
            {
                info.Append(string.Format("Transferred: {0}( Credit{1} )" + " [ Detail: {2} ]",
                    tc.TransferedCourse, tc.TransferedCourseCredit.ToString("N2"), tc.Detail));
            }

            return info.ToString();
        }

        public string GetGrade(CourseTakenByStudent courseTaken)
        {
            if (courseTaken.Dropped)
                return "W";

            return courseTaken.FinalGrade != null ? courseTaken.FinalGrade.Grade : " - ";
        }
    }
}
