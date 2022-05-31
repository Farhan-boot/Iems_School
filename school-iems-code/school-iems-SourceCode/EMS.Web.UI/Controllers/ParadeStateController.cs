using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary;
using EMS.DataAccess.Data;

using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;
using EMS.Web.Jsons.Custom.Student;

namespace EMS.Web.UI.Controllers
{

    public class ParadeStateController : EmployeeBaseController
    {
        // GET: ParadeState
        private ActionResult Index()
        {
            return View();
        }
        private ActionResult Student()
        {
            //var startDateTime = DateTime.Today.AddSeconds(1);
            //var endDateTime = startDateTime.AddHours(23).AddMinutes(59).AddSeconds(58);
            //var model = new List<StudentParadeStateModel>();
            //var deptList = DbInstance.HR_Department
            //    .Where(x => x.TypeEnumId == (byte)HR_Department.TypeEnum.AcademicDepartment)
            //    .ToList();
            //var levelList = DbInstance.Aca_StudyLevel.ToList();
            //foreach (var dept in deptList)
            //{
            //    foreach (var level in levelList)
            //    {
            //        if (DbInstance.User_Student.Any(x => x.DepartmentId == dept.Id && x.LevelId == level.Id))
            //        {
            //            var paradeState = new StudentParadeStateModel();
            //            paradeState.Department = dept.ShortName;
            //            paradeState.Level = level.Id;
            //            paradeState.TotalMilitary = DbInstance.User_Student
            //                .Count(x => x.DepartmentId == dept.Id
            //                && x.LevelId == level.Id
            //                && x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing
            //                && x.User_Account.CampusId == 1
            //                //&& x.User_Account.CategoryEnumId != (byte)User_Account.UserCategoryEnum.Civil
            //                );
            //            paradeState.TotalCivil = DbInstance.User_Student
            //                .Count(x => x.DepartmentId == dept.Id
            //                && x.LevelId == level.Id
            //                && x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing
            //                && x.User_Account.CampusId == 1
            //                //&& x.User_Account.CategoryEnumId == (byte)User_Account.UserCategoryEnum.Civil
            //                );
            //            paradeState.PresentMilitary = DbInstance.User_Student
            //                .Count(x => x.DepartmentId == dept.Id
            //                && x.LevelId == level.Id
            //                && x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing
            //                && x.User_Account.CampusId == 1
            //                //&& x.User_Account.CategoryEnumId != (byte)User_Account.UserCategoryEnum.Civil
            //                && x.Aca_ClassAttendance.Count(y => y.StartTime >= startDateTime && y.StartTime <= endDateTime && y.IsAbsent == false) > 0
            //                );
            //            paradeState.PresentCivil = DbInstance.User_Student
            //                .Count(x => x.DepartmentId == dept.Id
            //                && x.LevelId == level.Id
            //                && x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing
            //                && x.User_Account.CampusId == 1
            //                //&& x.User_Account.CategoryEnumId == (byte)User_Account.UserCategoryEnum.Civil
            //                && x.Aca_ClassAttendance.Count(y => y.StartTime >= startDateTime && y.StartTime <= endDateTime && y.IsAbsent == false) > 0
            //                );
            //            paradeState.AbsentMilitary = DbInstance.User_Student
            //                .Count(x => x.DepartmentId == dept.Id
            //                && x.LevelId == level.Id
            //                && x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing
            //                && x.User_Account.CampusId == 1
            //                //&& x.User_Account.CategoryEnumId != (byte)User_Account.UserCategoryEnum.Civil
            //                && x.Aca_ClassAttendance.Count(y => y.StartTime >= startDateTime && y.StartTime <= endDateTime && y.IsAbsent == false) == 0
            //                );
            //            paradeState.AbsentCivil = DbInstance.User_Student
            //                .Count(x => x.DepartmentId == dept.Id
            //                && x.LevelId == level.Id
            //                && x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing
            //                && x.User_Account.CampusId == 1
            //                //&& x.User_Account.CategoryEnumId == (byte)User_Account.UserCategoryEnum.Civil
            //                && x.Aca_ClassAttendance.Count(y => y.StartTime >= startDateTime && y.StartTime <= endDateTime && y.IsAbsent == false) == 0
            //                );
            //            paradeState.AbsentSIQ = DbInstance.User_Student
            //                .Count(x => x.DepartmentId == dept.Id
            //                && x.LevelId == level.Id
            //                && x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing
            //                && x.User_Account.CampusId == 1
            //                //&& x.User_Account.CategoryEnumId != (byte)User_Account.UserCategoryEnum.Civil
            //                && x.Aca_ClassAttendance.Count(y => y.StartTime >= startDateTime && y.StartTime <= endDateTime && y.IsAbsent
            //                && y.ReasonEnumId == (byte)Aca_ClassAttendance.ReasonEnum.SIQ) > 0
            //                );
            //            paradeState.AbsentCMH = DbInstance.User_Student
            //                .Count(x => x.DepartmentId == dept.Id
            //                && x.LevelId == level.Id
            //                && x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing
            //                && x.User_Account.CampusId == 1
            //                //&& x.User_Account.CategoryEnumId != (byte)User_Account.UserCategoryEnum.Civil
            //                && x.Aca_ClassAttendance.Count(y => y.StartTime >= startDateTime && y.StartTime <= endDateTime && y.IsAbsent
            //                && y.ReasonEnumId == (byte)Aca_ClassAttendance.ReasonEnum.CMH) > 0
            //                );
            //            paradeState.AbsentRSick = DbInstance.User_Student
            //                .Count(x => x.DepartmentId == dept.Id
            //                && x.LevelId == level.Id
            //                && x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing
            //                && x.User_Account.CampusId == 1
            //                && x.Aca_ClassAttendance.Count(y => y.StartTime >= startDateTime && y.StartTime <= endDateTime && y.IsAbsent
            //                && y.ReasonEnumId == (byte)Aca_ClassAttendance.ReasonEnum.RequestSick) > 0
            //                );
            //            paradeState.AbsentLeave = DbInstance.User_Student
            //                .Count(x => x.DepartmentId == dept.Id
            //                && x.LevelId == level.Id
            //                && x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing
            //                && x.User_Account.CampusId == 1
            //                && x.Aca_ClassAttendance.Count(y => y.StartTime >= startDateTime && y.StartTime <= endDateTime && y.IsAbsent
            //                && y.ReasonEnumId == (byte)Aca_ClassAttendance.ReasonEnum.Leave) > 0
            //                );
            //            paradeState.AbsentUnknown = paradeState.AbsentMilitary + paradeState.AbsentCivil -
            //                                        paradeState.AbsentSIQ - paradeState.AbsentCMH -
            //                                        paradeState.AbsentRSick - paradeState.AbsentLeave;

            //            model.Add(paradeState);
            //        }
            //    }
            //}
            return View("");
        }
    }
}