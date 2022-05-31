using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary.Helpers;
using EMS.Framework.Settings;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Employee.Controllers
{
    public class ClassManagementController : EmployeeBaseController
    {
        // GET: Employee/ClassManagement
        public ActionResult Index()
        {
            //coping settings marks
            //using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            //{
            //    var classComponent =
            //        DbInstance.Aca_ResultComponentSetting.Where(x => x.TestTypeEnumId == 0 || x.TestTypeEnumId == 1)
            //            .ToList();
            //    var settingsToUpdate = classComponent.Where(x => x.TestTypeEnumId == 1).ToList();
            //    foreach (var settings in settingsToUpdate)
            //    {
            //        var rightMark =
            //            classComponent.FirstOrDefault(x => x.TestTypeEnumId == 0 && x.ClassId == settings.ClassId);
            //        if (rightMark != null) settings.TotalMark = rightMark.TotalMark;
            //    }

            //    DbInstance.SaveChanges();
            //    scope.Commit();

            //}

            return View();
        }
        public ActionResult GenerateClassResult()
        {

            return View();
        }
        public ActionResult PrintOnlyFinalGradeSheet()
        {

            return View();
        }
        public ActionResult DownloadClassMaterial(long id)
        {
            var errorMessage = Encoding.ASCII.GetBytes("Sorry the Document you are looking for is not available.");
            string fileName = $"FileNotFound.txt";
            try
            {
                var fileMap = DbInstance.Aca_ClassMaterialFileMap.Include(x=>x.General_File).SingleOrDefault(x => x.Id==id);
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