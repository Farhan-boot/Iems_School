using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using EMS.Facade;
using System.Web;
using System.Web.Mvc;

namespace EMS.Web.UI.Areas.ExamSection.Controllers.WebApi
{
    public class BulkAdmitCardPrintApiController : EmployeeBaseApiController
    {
        public HttpListResult<Aca_SemesterJson> GetBulkAdmitCardPrintDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_SemesterJson>();
            //if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Course.CanView, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Course.CanAdd, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Course.CanEdit, out error))
            //{
            //    result.HasError = true;
            //    result.Errors.Add(error);
            //    return result;
            //}
            try
            {

                result.DataExtra.SemesterList = DbInstance.Aca_Semester
                    .Where(x=>x.IsEnable)
                    .OrderByDescending(x => x.Id)
                    //.ThenBy(x => x.Aca_StudyLevelTerm.Aca_StudyLevel.Id)
                    //.ThenBy(x => x.Aca_StudyLevelTerm.Aca_StudyTerm.Id)
                    .AsEnumerable().Select(t => new { Id = t.Id, Name = t.Name, SemesterDurationEnumId = t.SemesterDurationEnumId }).ToList();

                //result.DataExtra.StudyLevelTermList = DbInstance.Aca_StudyLevelTerm.AsEnumerable().Select(t => new
                //{ Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable()
                    .OrderBy(x => x.ShortName)
                    .Where(x => !x.IsDeleted)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name, ShortName = t.ShortName, ShortTitle = t.ShortTitle})
                    .ToList();

                //result.DataExtra.AdmitCardDownloadTypeEnumList = EnumUtil.GetEnumList(typeof(ReportController.AdmitCardDownloadTypeEnum));
                //result.DataExtra.DeptBatchList = DbInstance.Aca_DeptBatch.AsEnumerable()
                //    .OrderBy(x => x.BatchNo)
                //    .Select(t => new
                //    { Id = t.Id, Name = t.Name })
                //    .ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }


        public HttpResult GetBatchListByProgramIdSemesterId(
            int semesterId = 0
            , Int64 programId = 0
            , Int64 ContinuingBatchId = 0
            //, bool isOnlyRegisteredStd=false
            )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpResult();

            //if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.StudentCollectionReports.CanViewStudentDueReport, out error))
            //{
            //    result.HasError = true;
            //    result.HasViewPermission = false;
            //    result.Errors.Add(error);
            //    return result;
            //}
            if (semesterId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select Valid Semester!");
                return result;
            }

            if (programId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select Valid Program!");
                return result;
            }
            //if (ContinuingBatchId <= 0)
            //{
            //    result.HasError = true;
            //    result.Errors.Add("Please Select Only One Batch at a Time!");
            //    return result;
            //}
            try
            {

                var query = DbInstance.Aca_ClassTakenByStudent
                    .Where(x =>
                        x.EnrollTypeEnumId == (byte)Aca_ClassTakenByStudent.EnrollTypeEnum.Regular
                        &&
                        (x.RegistrationStatusEnumId != (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.Cancelled)
                    );

                if (programId > 0)
                {
                    query = query.Where(q => q.Aca_Class.Aca_CurriculumCourse.Aca_Curriculum.ProgramId == programId);
                }

                if (semesterId > 0)
                {
                    query = query.Where(q => q.Aca_Class.SemesterId == semesterId);
                }


                //var batchList = DbInstance.Aca_ClassTakenByStudent
                //                        .Where(q => q.Aca_Class.Aca_CurriculumCourse.Aca_Curriculum.ProgramId == programId)
                //                        .Where(q => q.Aca_Class.SemesterId == semesterId)
                //                        .Where(x => x.EnrollTypeEnumId == (byte)Aca_ClassTakenByStudent.EnrollTypeEnum.Regular
                //                        && (x.RegistrationStatusEnumId != (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.Cancelled)
                //                        )
                //                        .Include(x => x.User_Student)
                //                        .Include(x => x.User_Student.Aca_DeptBatch).DistinctBy(x => x.User_Student.ContinuingBatchId)
                //       ?.Select(x => x.User_Student.Aca_DeptBatch)?.ToList()
                //                        .ToList();




                //List<Aca_DeptBatch> batchList = query
                //    .Include(x => x.User_Student)
                //    .Include(x => x.User_Student.Aca_DeptBatch)
                //    .DistinctBy(x => x.User_Student.ContinuingBatchId)?.Select(x => x.User_Student.Aca_DeptBatch)?.ToList();

                var batchListids = query
                    .Include(x => x.User_Student)
                    //.Include(x => x.User_Student.Aca_DeptBatch)
                    .DistinctBy(x => x.User_Student.ContinuingBatchId)
                    ?.Select(x => x.User_Student.ContinuingBatchId)?.ToList();
                if (batchListids == null || batchListids.Count <= 0)
                {
                    result.DataExtra.DeptBatchList = null;

                    result.HasError = true;
                    result.Errors.Add("No Enrolled Student Found in this Program!");
                    return result;

                }

                var batchList = DbInstance.Aca_DeptBatch
                    .Join(batchListids, all => all.Id, req => req, (all, req) => all)
                    .ToList()
                    .AsEnumerable()
                    .OrderBy(x => x.BatchNo)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name })
                    .ToList(); ;

                //.Join(studentIdList, all => all.Id, req => req, (all, req) => all)
                result.DataExtra.DeptBatchList = batchList;

                //result.Data = "";

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
    }
}