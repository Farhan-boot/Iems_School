using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;
using Microsoft.Ajax.Utilities;
using MvcSiteMapProvider.Linq;
using System.Diagnostics;
using EMS.DataService;
using System.Data;

namespace EMS.Web.UI.Areas.Admission.Controllers.WebApi
{
    public class PreviousEducationReportApiController : EmployeeBaseApiController
    {
        public class PreviousEducationReportJson
        {
            public int AccountId { get; set; }
            public int StudentId { get; set; }
            public string ClassRollNo { get; set; }
            public string Name { get; set; }
            public string ProgramName { get; set; }
            public int? BatchName { get; set; }
            public string SscGroup { get; set; }
            public float? SscGpa { get; set; }
            public string HscGroup { get; set; }
            public float? HscGpa { get; set; }

        }

        #region Full Package Waiver Details Report
        public HttpListResult<PreviousEducationReportJson> GetPagedPreviousEducation( Int64 semesterId = 0
           , Int32 programId = 0
        )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<PreviousEducationReportJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Report.CanViewPreviousEducationReport, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            if (semesterId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Select a Valid Semester.");
                return result;
            }

            try
            {

                IQueryable<User_Student> query = DbInstance.User_Student
                    .Include(x => x.User_Account)
                    .Include(x => x.User_Account.User_Education);

                if (semesterId > 0)
                {
                    query = query.Where(x => x.User_Account.JoiningSemesterId == semesterId);
                }

                if (programId > 0)
                {
                    query = query.Where(x => x.ProgramId == programId);
                }

                var studentList = query.ToList();

                var studentListWithResult = new List<PreviousEducationReportJson>();

                foreach (var student in studentList)
                {
                    var studentWithResult = new PreviousEducationReportJson();
                    
                    studentWithResult.AccountId = student.UserId;
                    studentWithResult.StudentId = student.Id;
                    studentWithResult.ClassRollNo = student.User_Account.UserName;
                    studentWithResult.Name = student.User_Account.FullName;
                    studentWithResult.ProgramName = DbInstance.Aca_Program.FirstOrDefault(x=>x.Id == student.ProgramId)?.ShortTitle;
                    studentWithResult.BatchName = DbInstance.Aca_DeptBatch.FirstOrDefault(x=>x.Id == student.JoiningBatchId)?.BatchNo;

                    var sscResult = student.User_Account.User_Education.OrderByDescending(x=>x.ObtainedGpaOrMarks).FirstOrDefault(x =>
                        x.DegreeEquivalentEnumId == (byte) User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent);

                    if (sscResult != null)
                    {
                        studentWithResult.SscGroup = sscResult.DegreeGroupMajorEnumId == 0 ? "" : ((User_Education.DegreeGroupMajorEnum)sscResult.DegreeGroupMajorEnumId).ToString().AddSpacesToSentence();
                        studentWithResult.SscGpa = sscResult.ObtainedGpaOrMarks;
                    }

                    var hscResult = student.User_Account.User_Education.OrderByDescending(x => x.ObtainedGpaOrMarks).FirstOrDefault(x =>
                        x.DegreeEquivalentEnumId == (byte)User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent);

                    if (hscResult != null)
                    {
                        studentWithResult.HscGroup = hscResult.DegreeGroupMajorEnumId==0?"":((User_Education.DegreeGroupMajorEnum)hscResult.DegreeGroupMajorEnumId).ToString().AddSpacesToSentence();
                        studentWithResult.HscGpa = hscResult.ObtainedGpaOrMarks;
                    }

                    studentListWithResult.Add(studentWithResult);
                }

                result.Data = studentListWithResult;
                result.Count = count;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult GetPreviousEducationDataExtra()
        {
            string error = string.Empty;
            var result = new HttpResult();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Report.CanViewPreviousEducationReport, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                result.DataExtra.SemesterList = Aca_Semester.GetDropdownList(DbInstance);

                result.DataExtra.ProgramList = DbInstance.Aca_Program
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.ShortTitle)
                    .AsEnumerable().Select(t => new
                    {
                        Id = t.Id,
                        Name = t.ShortTitle,
                    }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        #endregion
    }
}