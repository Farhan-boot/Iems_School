using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;
using EMS.Web.UI.Areas.Student.Models;
using MvcSiteMapProvider.Linq;

namespace EMS.Web.UI.Areas.Student.Controllers
{
    public class CourseController : StudentBaseController
    {
        // GET: Student/CoursePane
        private ActionResult Index()
        {
            var presentSemester = HttpUtil.DbContext.Aca_Semester
                .Single(x=>x.StatusEnumId==(byte)Aca_Semester.StatusEnum.Ongoing);

            List<Aca_ClassTakenByStudent> model = HttpUtil.DbContext.Aca_ClassTakenByStudent
                .Include("Aca_Class")
                .Include("Aca_Class.Aca_CurriculumCourse")
                .Include("Aca_Class.Aca_CurriculumCourse.Aca_Curriculum")
                          .Include(x => x.Aca_Class.Aca_Semester)
                .Include(x => x.Aca_Class.Aca_StudyLevelTerm)
                .Include("Aca_Class.Aca_ClassRoutine")
                .Include("Aca_Class.Aca_ClassRoutine.General_Room")
                .Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Building")
                .Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Building.General_Campus")
                .Where(x => x.StudentId == HttpUtil.Profile.StudentId 
                //&& x.Aca_Class.SemesterId == presentSemester.Id
                )
                .OrderByDescending(x => x.Aca_Class.Aca_Semester.Year)
                .ThenByDescending(x => x.Aca_Class.Aca_Semester.TermId)
                .ThenBy(x => x.Aca_Class.Code)
                .ToList();
            return View(model);
        }
        public ActionResult CoursePane(long id=0)
        {
            var model = new CourseViewModel();
            //check is profile Registered
            User_Account user = HttpUtil.DbContext.User_Account.Find(HttpUtil.Profile.UserId);
            ViewBag.IsProfileRegistered = true;

            if (user==null)// && user.IsMigrate
            {
                ViewBag.IsProfileRegistered = !user.IsMigrate;
                return View(model);
            }
            
            model.CourseResultViewModel=new CourseResultViewModel();
            List<Aca_ClassTakenByStudent> classTakenByStudentList = HttpUtil.DbContext.Aca_ClassTakenByStudent
                .Include("Aca_Class")
                .Include("Aca_Class.Aca_ClassSection")
                .Include("Aca_Class.Aca_CurriculumCourse")
                .Include("Aca_Class.Aca_CurriculumCourse.Aca_Curriculum")
                .Include(x=>x.Aca_Class.Aca_Semester)
                .Include(x => x.Aca_Class.Aca_StudyLevelTerm)
                .Include("Aca_Class.Aca_ClassRoutine")
                .Include("Aca_Class.Aca_ClassRoutine.General_Room")
                .Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Building")
                .Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Building.General_Campus")
                .Where(x => x.StudentId == HttpUtil.Profile.StudentId && x.ClassId == id).ToList();

            var acaClassTakenByStudent = classTakenByStudentList.FirstOrDefault();
            if (acaClassTakenByStudent != null)
            {
                model.Course = acaClassTakenByStudent.Aca_Class;

                if (SiteSettings.Instance.Student.Class.AttendancesCanView)
                {
                    model.ClassAttendanceList = HttpUtil.DbContext.Aca_ClassAttendance
                        .Include("Aca_ClassAttendanceSetting")
                        .Where(x => x.ClassId == model.Course.Id && x.StudentId == HttpUtil.Profile.StudentId)
                        .ToList();
                    foreach (var attendance in model.ClassAttendanceList)
                    {
                        var faculty = HttpUtil.DbContext.User_Employee
                            .Include("User_Account")
                            .Single(x => x.Id == attendance.Aca_ClassAttendanceSetting.EmployeeId);
                        attendance.EmployeeName = faculty.FullName;
                    }
                }
                
               
                //End Class Result
                if (SiteSettings.Instance.Student.Class.CourseMaterialsCanView)
                {
                    model.ClassMaterialList = HttpUtil.DbContext.Aca_ClassMaterialFileMap
                        .Include(x => x.General_File)
                        .Include(x => x.User_Account)
                        .OrderByDescending(x => x.General_File.CreateDate)
                        .Where(x => x.ClassId == model.Course.Id)
                        .ToList();
                }

                if (SiteSettings.Instance.Student.Class.CourseNoticesCanView)
                {
                    model.ClassNoticeList = HttpUtil.DbContext.Aca_ClassNotice
                        .OrderByDescending(x => x.PostDate)
                        .Where(x => x.ClassId == model.Course.Id)
                        .ToList();
                }
                    
            }
            return View(model);
        }
        public ActionResult ClassAttendance(long id)
        {
            var model = new List<Aca_ClassAttendance>();
            return PartialView(model);
        }
        private ActionResult ClassAttendanceSmsLog()
        {
            return View();
        }

        public ActionResult ClassResults(long id)
        {
            var model = new CourseResultViewModel();
            return PartialView(model);
        }
        public ActionResult ClassMaterials(long id)
        {
            var model = new List<Aca_ClassMaterialFileMap>();
            return PartialView(model);
        }
        public ActionResult DownloadClassMaterial(long id)
        {
            var errorMessage = Encoding.ASCII.GetBytes("Sorry the Document you are looking for is not available.");
            string fileName = $"FileNotFound.txt";
            try
            {
                var fileMap = DbInstance.Aca_ClassMaterialFileMap.Include(x => x.General_File).SingleOrDefault(x => x.Id == id);
                if (fileMap == null)
                {
                    return File(errorMessage, MimeTypeHelper.GetMimeType(".txt"), fileName);
                }
                var fullDiskPathFolder = Path.Combine(DiskStorageSettings.RootStoragePath, fileMap.General_File.DiskPath);

                if (!System.IO.File.Exists(fullDiskPathFolder))
                {
                    return File(errorMessage, MimeTypeHelper.GetMimeType(".txt"), fileName);
                }

                var fileResult = new FilePathResult(fullDiskPathFolder, fileMap.General_File.ContentType);
                fileResult.FileDownloadName = fileMap.General_File.Name;

                return fileResult;
            }
            catch (Exception e)
            {
                return File(errorMessage, MimeTypeHelper.GetMimeType(".txt"), fileName);
            }
        }
        

    }
}