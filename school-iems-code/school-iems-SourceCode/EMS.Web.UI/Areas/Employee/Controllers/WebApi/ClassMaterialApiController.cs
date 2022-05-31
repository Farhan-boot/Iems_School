using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.ServiceModel.Channels;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
using EMS.Web.Jsons.Models;
using EMS.Web.UI.Areas.HR.Controllers.WebApi;

namespace EMS.Web.UI.Areas.Employee.Controllers.WebApi
{
    public class ClassMaterialApiController : EmployeeBaseApiController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_ClassMaterialFileMapJson> GetPagedClassMaterialFileMap(
            string textkey
            , int? pageSize
            , int? pageNo
            , Int64 classId = 0

        )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_ClassMaterialFileMapJson>();
            if (classId <= 0)
            {
                error = "Please select valid Class to view Class Material(s)!";
                result.HasError = true;

                result.Errors.Add(error);
                return result;
            }
            try
            {
                var res = DbInstance.Aca_ClassTakenByEmployee
                    .Any(x => x.ClassId == classId 
                              && !x.IsDeleted
                              && x.EmployeeId == HttpUtil.Profile.EmployeeId && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty);
                if (!res)
                {
                    error = "You don't have permission view Class Material(s) of this class, you are not faculty of this Class!";
                    result.HasError = true;

                    result.Errors.Add(error);
                    return result;
                }

                using (Aca_ClassMaterialFileMapDataService classMaterialFileMapDataService = new Aca_ClassMaterialFileMapDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<Aca_ClassMaterialFileMap> query = DbInstance.Aca_ClassMaterialFileMap
                        .Include(x => x.General_File)
                        .Include(x => x.User_Account)
                        .OrderByDescending(x => x.General_File.CreateDate);
                    if (classId > 0)
                    {
                        query = query.Where(q => q.ClassId == classId);
                    }

                    var entities = classMaterialFileMapDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_ClassMaterialFileMapJson>();
                    entities.Map(jsons);

                    result.Data = jsons;
                    result.Count = count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        [System.Web.Mvc.HttpPost]
        public HttpResult UploadClassMaterial(HttpRequestMessage request)
        {
            string error = string.Empty;
            var result = new HttpResult();
            var classId = HttpUtil.GetQueryString("classId").ToInt64();
            var Request = HttpContext.Current.Request;

            var res = DbInstance.Aca_ClassTakenByEmployee
                .Any(x => x.ClassId == classId 
                          && !x.IsDeleted
                          && x.EmployeeId == HttpUtil.Profile.EmployeeId && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty);

            if (!res)
            {
                error = "You don't have permission to upload Class Material for this class, you are not faculty of this Class!";
                result.HasError = true;

                result.Errors.Add(error);
                return result;
            }
            List<string> uploadFileNames = new List<string>();
            List<string> uploadFilePath = new List<string>();

            if (classId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("No Class Selected for upload Materials!");
                return result;
            }
            //check file exist, file size, file extensions 
            if (!IsValidToUploadFile(Request.Files, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var scope = DbInstance.Database.BeginTransaction();

            try
            {

                foreach (string file in Request.Files)
                {
                    if (Request.Files[file] == null || Request.Files[file].ContentLength == 0) continue;

                    var extension = Path.GetExtension(Request.Files[file].FileName);
                    var newFileId = BigInt.NewBigInt();
                    var newFileName = newFileId + "" + extension;
                    string relativeFolderUrl = DiskStorageSettings.ItemFolderNames.ClassMaterialFolderName + "/" +
                                               DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                    var relativeFolderDiskPath = relativeFolderUrl.Replace("/", "\\");
                    var fullDiskPathFolder = DiskStorageSettings.RootStoragePath + relativeFolderUrl.Replace("/", "\\");

                    Directory.CreateDirectory(fullDiskPathFolder);

                    string fullFilePath = fullDiskPathFolder + newFileName; //for save file.
                    var relativeFileUrl = relativeFolderUrl + newFileName; //for save in DB
                    var relativeFileDiskPath = relativeFolderDiskPath + newFileName; //for save in DB


                    if (File.Exists(fullFilePath))
                    {
                        File.Delete(fullFilePath);
                    }
                    Request.Files[file].SaveAs(fullFilePath);
                    uploadFilePath.Add(fullFilePath);

                    var doc = General_File.GetNew(newFileId);
                    doc.Id = newFileId;
                    doc.TypeEnumId = (int)General_File.TypeEnum.ClassNote;
                    doc.Name = Request.Files[file].FileName;
                    doc.DiskPath = relativeFileDiskPath;
                    //doc.DownloadURL = relativeFileUrl;
                    doc.IsDeleted = false;
                    doc.ContentType = Request.Files[file].ContentType;
                    doc.SizeInByte = Request.Files[file].ContentLength;
                    doc.CreateDate = DateTime.Now;
                    doc.CreateById = HttpUtil.Profile.UserId;
                    doc.LastChanged = DateTime.Now;
                    doc.LastChangedById = HttpUtil.Profile.UserId;
                    DbInstance.General_File.Add(doc);

                    Aca_ClassMaterialFileMap classMaterialFileMap = Aca_ClassMaterialFileMap.GetNew(newFileId);
                    classMaterialFileMap.FileId = newFileId;
                    classMaterialFileMap.ClassId = classId;
                    classMaterialFileMap.UserId = HttpUtil.Profile.UserId;
                    DbInstance.Aca_ClassMaterialFileMap.Add(classMaterialFileMap);

                    uploadFileNames.Add(Request.Files[file].FileName);
                }
                DbInstance.SaveChanges();
                scope.Commit();
                //send SMS
                //send Email to all student of this class
                SentSmsEmailToStudent(uploadFileNames, classId);

                return result;
            }
            catch (Exception ex)
            {
                //deleting all files
                foreach (string fullFilePath in uploadFilePath)
                {
                    if (File.Exists(fullFilePath))
                    {
                        File.Delete(fullFilePath);
                    }
                }
                if (scope.UnderlyingTransaction.Connection != null)
                {
                    scope.Rollback();
                    scope.Dispose();
                }
                result.HasError = true;
                result.Errors.Add("Sorry some problem occurred while uploading Class Materials, please try again later.! "+ex.GetBaseException());
                return result;
            }


        }

        public HttpResult<Aca_ClassMaterialFileMapJson> GetDeleteClassMaterialFileMapById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassMaterialFileMapJson>();
            if (id <= 0)
            {
                error = "Invalid Class Material selected to Delete!";
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var scope = DbInstance.Database.BeginTransaction();
            try
            {
                var fileMaterialMap = DbInstance.Aca_ClassMaterialFileMap.Include(x => x.General_File).SingleOrDefault(x => x.Id == id);
                if (fileMaterialMap == null)
                {
                    error = "Invalid Class Material selected for Delete! Please reload the page and try again.";
                    result.HasError = true;
                    result.Errors.Add(error);
                    return result;
                }
                if (fileMaterialMap.UserId != HttpUtil.Profile.UserId)
                {
                    error = "You don't have permission to Delete this Class Material of this class, you are not owner of this Class Material!";
                    result.HasError = true;

                    result.Errors.Add(error);
                    return result;
                }

                var res = DbInstance.Aca_ClassTakenByEmployee
                    .Any(x => x.ClassId == fileMaterialMap.ClassId 
                              && !x.IsDeleted
                              && x.EmployeeId == HttpUtil.Profile.EmployeeId && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty);
                if (!res)
                {
                    error = "You don't have permission to Delete this Class Material of this class, you are not faculty of this Class!";
                    result.HasError = true;

                    result.Errors.Add(error);
                    return result;
                }

                General_File file = DbInstance.General_File.Find(fileMaterialMap.FileId);
                if (file != null)
                {
                    var fullDiskPathFolder = Path.Combine(DiskStorageSettings.RootStoragePath, file.DiskPath);
                    if (File.Exists(fullDiskPathFolder))
                    {
                        File.Delete(fullDiskPathFolder); 
                    }
                    DbInstance.Aca_ClassMaterialFileMap.Remove(fileMaterialMap);
                    DbInstance.General_File.Remove(file);
                    DbInstance.SaveChanges();
                    scope.Commit();
                }
            }
            catch (Exception ex)
            {
                if (scope.UnderlyingTransaction.Connection != null)
                {
                    scope.Rollback();
                }
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        #region Local Method(should be always private)
        bool SentSmsEmailToStudent(List<string> fileNames, long classId)
        {

            return false;
        }
        bool IsValidToUploadFile(HttpFileCollection files, out string error)
        {
            error = "";

            if (files == null || files.Count == 0)
            {
                error = ("No file found to process!");
                return false;
            }
            foreach (string file in files)
            {
                if (files[file] == null || files[file].ContentLength == 0) continue;

                if (files[file].ContentLength > SiteSettings.Instance.MaxFileUploadSizeInByte)
                {
                    error = ("1 or more File's size limit exceeded, please follow the instructions!");
                    return false;
                }

                var extension = Path.GetExtension(files[file].FileName);
                if (extension != null && !SiteSettings.Instance.AllowedFileExtensions.Contains(extension.ToLower()))
                {
                    error = ("1 or more File type(s) are not allowed to upload, please follow the instructions!");
                    return false;
                }
            }
            return true;

        }
        public static void ValidatePath(string directoryPath)
        {
            FileInfo fileInfo = new FileInfo(directoryPath);
            Directory.CreateDirectory(fileInfo.DirectoryName);
        }



        #endregion
    }
}
