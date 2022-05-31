using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;
using EMS.Web.UI.Areas.Academic.Models;
using Microsoft.Ajax.Utilities;

namespace EMS.Web.UI.Areas.Academic.Controllers
{
    public class ClassManagerController : EmployeeBaseController
    {
        // GET: Academic/ClassManager
        public ActionResult ClassAddEdit()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult ClassList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult NonDeptClassList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult BulkClass()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanOfferBulkCourse))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult BulkCourseOfferingManger()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult PrintAllAttendance(long classId)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassAttendance.CanViewAttendance))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }

            var model = new ClassAttendanceViewModel();
            Aca_Class acaClass = HttpUtil.DbContext.Aca_Class
                .Include("HR_Department")
                .Include("Aca_ClassSection")
                .Include("Aca_CurriculumCourse")
                .Include("Aca_CurriculumCourse.Aca_Curriculum")
                .Include("Aca_Semester")
                .Include( x=>x.Aca_StudyLevelTerm)
                //.Include("Aca_ClassRoutine")
                //.Include("Aca_ClassRoutine.General_Room")
                //.Include("Aca_ClassRoutine.General_Room.General_Building")
                //.Include("Aca_ClassRoutine.General_Room.General_Building.General_Campus")
                .SingleOrDefault(x => x.Id == classId);

            if (acaClass != null)
            {
                model.Course = acaClass;
                model.ClassAttendanceList = HttpUtil.DbContext.Aca_ClassAttendance
                    .Where(x => x.ClassId == classId)
                    .ToList();
                model.ClassAttendanceSettingList = HttpUtil.DbContext.Aca_ClassAttendanceSetting
                    .Where(x => x.ClassId == classId)
                    .ToList();
                model.ClassStudentList = HttpUtil.DbContext.Aca_ClassTakenByStudent
                    .Include("User_Student")
                    .Include("User_Student.User_Account")
                    //.OrderByDescending(x => x.User_Student.RegistrationSession)
                    .OrderByDescending(x => x.User_Student.AdmissionExamId)
                    .ThenBy(x => x.User_Student.ClassRollNo)
                    .Where(x => x.ClassId == classId
                    && x.EnrollTypeEnumId == (byte)Aca_ClassTakenByStudent.EnrollTypeEnum.Regular)
                    .ToList();
                model.ClassFacultyList = HttpUtil.DbContext.Aca_ClassTakenByEmployee
                    .Include("User_Employee")
                    .Include("User_Employee.User_Account")
                    .OrderByDescending(x => x.User_Employee.User_Account.MaritalStatusEnumId)
                    .ThenBy(x => x.User_Employee.PositionId)
                    .Where(x => x.ClassId == classId
                    && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty)
                    .ToList();
            }
            return View(model);
        }
        public ActionResult ClassTestMarks(long classId)
        {
            try
            {
                var model = new ClassTestMarksViewModel();
                string error;
                if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassResultManager.ClassResult.CanPrint30PercentMarks, out error))
                {
                    ViewBag.Error = error;
                    return View();
                }
                Aca_Class acaClass = HttpUtil.DbContext.Aca_Class
                    .Include("HR_Department")
                    .Include("Aca_ClassSection")
                    .Include("Aca_CurriculumCourse")
                    .Include("Aca_CurriculumCourse.Aca_Curriculum")
                    .Include("Aca_Semester")
                    .Include("Aca_StudyLevelTerm")
                    .Include("Aca_ClassRoutine")
                    .Include("Aca_ClassRoutine.General_Room")
                    .Include("Aca_ClassRoutine.General_Room.General_Building")
                    .Include("Aca_ClassRoutine.General_Room.General_Building.General_Campus")
                    .SingleOrDefault(x => x.Id == classId);

                if (acaClass != null)
                {
                    model.Course = acaClass;
                    model.ClassStudentList = HttpUtil.DbContext.Aca_ClassTakenByStudent
                        .Include("User_Student")
                        .Include("User_Student.User_Account")
                        //.OrderByDescending(x => x.User_Student.RegistrationSession)
                        .OrderByDescending(x => x.User_Student.AdmissionExamId)
                        .ThenBy(x => x.User_Student.ClassRollNo)
                        .Where(x => x.ClassId == classId
                        && x.EnrollTypeEnumId == (byte)Aca_ClassTakenByStudent.EnrollTypeEnum.Regular)
                        .ToList();
                    model.ClassFacultyList = HttpUtil.DbContext.Aca_ClassTakenByEmployee
                        .Include("User_Employee")
                        .Include("User_Employee.User_Account")
                        .OrderByDescending(x => x.User_Employee.User_Account.MaritalStatusEnumId)
                        .ThenBy(x => x.User_Employee.PositionId)
                        .Where(x => x.ClassId == classId
                        && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty)
                        .ToList();
                   
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.GetBaseException();
                return View();
            }
        }
    }
}