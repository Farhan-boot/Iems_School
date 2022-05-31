using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Facade.AcademicArea;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
using EMS.Framework.Settings;
using EMS.Web.Jsons.Custom.Registration;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;
using Microsoft.Ajax.Utilities;

namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{
    public class RollAssignedReportApiController : EmployeeBaseApiController
    {

        #region Roll Assigned Report Json
        public class RollJson
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public List<AssignedUserJson> AssignedUserList { get; set; }

            public RollJson()
            {
                AssignedUserList = new List<AssignedUserJson>();
            }
        }

        public class AssignedUserJson
        {
            public int AccountId { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string DepartmentName { get; set; }
            public byte UserStatusEnumId { get; set; }
            public string UserStatus
            {
                get
                {
                    return ((User_Account.UserStatusEnum)UserStatusEnumId).ToString().AddSpacesToSentence();
                }
            }

            public DateTime AssignedDate { get; set; }
            public string AssignedByName { get; set; }
            public DateTime ExpiredDate { get; set; }
        }

        #endregion

        public HttpListResult<bool> GetRollAssignedReportDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<bool>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserRole.CanViewAssignedReport, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                result.DataExtra.UserStatusEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserStatusEnum));
                result.DataExtra.DepartmentList = DbInstance.HR_Department
                    .OrderBy(x => x.Name).AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,// + " (" + t.ShortName + ")",
                        Type = t.Type.ToString().AddSpacesToSentence()
                    });
                result.DataExtra.RollList = DbInstance.UAC_Role
                    .Where(x=>x.Id !=1)
                    .OrderBy(x => x.Name).AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name
                    });
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpListResult<RollJson> GetRollAssignedList(
           int rollId = 0,
           int userStatusEnumId = -1,
           int departmentId = 0
        )
        {
            var result = new HttpListResult<RollJson>();
            string error = String.Empty;

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserRole.CanViewAssignedReport, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                var rollAssignedUserList = new List<RollJson>();

                #region Getting Roll List

                if (rollId <= 0)
                {
                    rollAssignedUserList = DbInstance.UAC_Role.Where(x=> x.Id != 1).Select(x => new RollJson()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    }).ToList();
                }
                else
                {
                    rollAssignedUserList = DbInstance.UAC_Role.Where(x=>x.Id == rollId && x.Id != 1).Select(x => new RollJson()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    }).ToList();
                }

                if (rollAssignedUserList.Count <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("No Roll Found");
                    return result;
                }

                #endregion

                #region Getting Assigned User

                IQueryable<UAC_RoleUserMap> query = DbInstance.UAC_RoleUserMap.Include(x => x.User_Account).Include(x=>x.User_Account.HR_Department);

                if (rollId > 0)
                {
                    query = query.Where(x => x.RoleId == rollId);
                }

                if (departmentId > 0)
                {
                    query = query.Where(q => q.User_Account.DepartmentId == departmentId);
                }

                if (userStatusEnumId > -1)
                {
                    query = query.Where(x => x.User_Account.UserStatusEnumId == userStatusEnumId);
                }

                var assignedUserList = query.OrderBy(x => x.User_Account.Id).ToList();

                #endregion

                #region Mapping Assigned User according to rolls.

                foreach (var roll in rollAssignedUserList)
                {
                    var assignedUsersInThisRoll = assignedUserList.Where(x => x.RoleId == roll.Id).Select(x =>
                        new AssignedUserJson()
                        {
                            AccountId = x.UserId,
                            UserName = x.User_Account.UserName,
                            FullName = x.User_Account.FullName,
                            DepartmentName = x.User_Account.HR_Department.Name,
                            UserStatusEnumId = x.User_Account.UserStatusEnumId,
                            ExpiredDate = x.EndDate,
                            AssignedDate = x.CreateDate,
                            AssignedByName = DbInstance.User_Account.FirstOrDefault(q => q.Id == x.CreateById)?.FullName
                        }).ToList();
                    roll.AssignedUserList.AddRange(assignedUsersInThisRoll);
                }
                
                #endregion

                result.Data = rollAssignedUserList;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message);
            }

            return result;
        }
    }
}
