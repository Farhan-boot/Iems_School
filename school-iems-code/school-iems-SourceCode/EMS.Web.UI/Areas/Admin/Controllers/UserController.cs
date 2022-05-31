using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;
using EMS.Framework.Helpers;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.UI.Areas.Admin.Models;
using MvcSiteMapProvider.Linq;

namespace EMS.Web.UI.Areas.Admin.Controllers
{
    [EmsMvcAuthorize(User_Account.UserTypeEnum.Employee)]
    public class UserController : BaseController
    {
        #region User
        // GET: Admin/User
        public ActionResult Index(string searchkey = "", int pageNo = 1, int pageSize = 50)
        {
            if (!HttpUtil.IsSupperAdmin())
                if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserAccess.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            var result = new MvcModelListResult<UserModel>();
            var _db = HttpUtil.DbContext;
            IQueryable<User_Account> query = from uc in _db.User_Account select uc;

            if (!string.IsNullOrEmpty(searchkey)
                && !string.IsNullOrWhiteSpace(searchkey))
            {
                query = from c in query
                        where SqlFunctions.StringConvert((double)c.Id).Contains(searchkey)
                        || c.FullName.Contains(searchkey)
                        || c.UserName.Contains(searchkey)
                        select c;
            }

            //disallow view super admin
            if (!HttpUtil.IsSupperAdmin())
            {    
                query = query.Where(x => x.Id != HttpUtil.SuperAdminId || !x.UserName.Equals(HttpUtil.SuperAdminUserName, StringComparison.InvariantCultureIgnoreCase));
            }
           
            result.Count = query.Count();

            pageNo--;
            query = (from q in query
                     orderby q.Id
                     select q)
                    .Skip(pageNo * pageSize)
                .Take(pageSize);

            //if (!string.IsNullOrEmpty(includes)
            //  && !string.IsNullOrWhiteSpace(includes))
            //{
            //    var incs = includes.Split(',');

            //    query = incs.Aggregate(query,
            //    (current, i) => current.Include(i));
            //}

            var UserModels = new List<UserModel>();
            query.Map(UserModels);

            result.Data = UserModels;

            ViewBag.UserCredentialResult = result;

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View();
        }

        public ActionResult Delete(long id, string searchkey = "", int pageNo = 1, int pageSize = 50)
        {
            string error = string.Empty;
            var db = HttpUtil.DbContext;

            var uc = db.User_Account.FirstOrDefault(ucc => ucc.Id == id);
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserAccess.CanDelete, out error))
            {
                TempData["ErrorMessage"] = error;
                return RedirectToAction("Index", new { searchkey = searchkey, pageNo = pageNo, pageSize = pageSize });
            }
         
            if (uc != null)
            {
                if (!HttpUtil.IsSupperAdmin())
                {
                    //disallow delete super admin
                    if (HttpUtil.IsSupperAdmin(uc.UserName, uc.Id))
                    {
                        TempData["ErrorMessage"] = "OOPS! Something Went Wrong";
                        return RedirectToAction("Index", new { searchkey = searchkey, pageNo = pageNo, pageSize = pageSize });
                    }
                }
                try
                {
                    db.User_Account.Remove(uc);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Operation Completed";
                }
                catch (Exception exception)
                {
                    TempData["ErrorMessage"] = "OOPS! Something Went Wrong";
                }
            }

            return RedirectToAction("Index", new { searchkey = searchkey, pageNo = pageNo, pageSize = pageSize });
        }

        public ActionResult Update(long id)
        {
            if (!HttpUtil.IsSupperAdmin())
                if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserAccess.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            var userModel = new UserModel() { UserId = -1 };
            //disallow super admin
            if (!HttpUtil.IsSupperAdmin() && id == 1)
            {
                ViewBag.ErrorMessage = "Invalid User!";
                return View(userModel);

            }
            var _db = HttpUtil.DbContext;
            var userCredential = _db.User_Account.FirstOrDefault(uc => uc.Id == id);

            if (userCredential != null)
                userCredential.Map(userModel);

            return View(userModel);
        }

        [HttpPost]
        public ActionResult Update(UserModel model, string command, int[] roles)
        {
            var _db = HttpUtil.DbContext;
            string error = string.Empty;
            var userModel = new UserModel() { UserId = -1 };
            //disallow super admin
            if (!HttpUtil.IsSupperAdmin() && model.UserId == 1)
            {
                ViewBag.ErrorMessage = "Invalid User!";
                return View(userModel);

            }
            switch (command)
            {
                case "Save":
                    {
                        if (!HttpUtil.IsSupperAdmin())
                            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserAccess.CanEdit, out error))
                        {
                            ViewBag.ErrorMessage = error;
                            break;
                        }
                        SaveCommand(model);
                        break;
                    }

                case "Approve/Disapprove":
                case "Lock/Unlock":
                    {
                        if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserAccess.CanChangeStatus, out error))
                        {
                            ViewBag.ErrorMessage = error;
                            break;
                        }
                        UpdateCommand(model, command);
                        break;
                    }
                case "Reset":
                    {
                        if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserAccess.CanResetPassword, out error))
                        {
                            ViewBag.ErrorMessage = error;
                            break;
                        }
                        UpdateCommand(model, command);
                        break;
                    }
                case "FailedPasswordAttemptCount":
                case "FailedPasswordAnswerAttemptCount":
                    {
                        if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserAccess.CanResetLoginFailedCount, out error))
                        {
                            ViewBag.ErrorMessage = error;
                            break;
                        }
                        UpdateCommand(model, command);
                        break;
                    }
                case "UserRole":
                    {
                        if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserAccess.CanManageRole, out error))
                        {
                            ViewBag.ErrorMessage = error;
                            break;
                        }
                        SaveUAC_Role(model, roles);
                        break;
                    }
                default:
                    ViewBag.ErrorMessage = "Invalid Command";
                    break;
            }

            ModelState.Clear();

            var userCredential = _db.User_Account.FirstOrDefault(uc => uc.Id == model.UserId);
            var UserModel = new UserModel() { UserId = -1 };

            if (userCredential != null)
                userCredential.Map(UserModel);

            return View(UserModel);
        }

        private void SaveUAC_Role(UserModel model, int[] newRoleList)
        {
            var db = HttpUtil.DbContext;

            var user = db.User_Account.Include("UAC_RoleUserMap").FirstOrDefault(r => r.Id == model.UserId);

            if (user == null)
            {
                ViewBag.ErrorMessage = "Invalid User";
                return;
            }

            using (var context = db.Database.BeginTransaction())
            {
                try
                {
                    var currentRoleUserMapList = user.UAC_RoleUserMap.ToList();
                    foreach (var currentRoleUserMap in currentRoleUserMapList)
                    {
                        if (newRoleList!=null)
                        {
                            if (newRoleList.Any(roleId => roleId == currentRoleUserMap.RoleId))
                                continue;
                        }

                        db.UAC_RoleUserMap.Remove(currentRoleUserMap);
                    }

                    if (newRoleList != null) {
                        foreach (var role in newRoleList)
                        {
                            if (user.UAC_RoleUserMap.Any(r => r.RoleId == role))
                                continue;

                            db.UAC_RoleUserMap.Add(new UAC_RoleUserMap()
                            {
                                CreateDate = DateTime.Now,
                                CreateById = HttpUtil.Profile.UserId,
                                LastChanged = DateTime.Now,
                                LastChangedById = HttpUtil.Profile.UserId,
                                Id = BigInt.NewBigInt(),
                                RoleId = role,
                                UserId = user.Id,
                                StartDate = DateTimeHelper.SqlMinDateTime,
                                EndDate = DateTimeHelper.SqlMaxDateTime
                            });
                        }
                    }

                    db.SaveChanges();
                    context.Commit();

                    ViewBag.SuccessMessage = "Operation Completed";
                }
                catch (DbEntityValidationException ex)
                {
                    var sb = new StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error1 in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error1.PropertyName, error1.ErrorMessage);
                            sb.AppendLine();
                        }
                    }
                    ViewBag.ErrorMessage = "OOPS! Something Went Wrong";
                    context.Rollback();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "OOPS! Something Went Wrong";
                    context.Rollback();
                }
            }
        }

        private void SaveCommand(UserModel model)
        {
            var _db = HttpUtil.DbContext;
            string error = string.Empty;

            var user = _db.User_Account.FirstOrDefault(uc => uc.Id == model.UserId);
            //disallow edit in super admin
            if (user!=null &&!HttpUtil.IsSupperAdmin())
            {
                //disallow delete super admin
                if (HttpUtil.IsSupperAdmin(user.UserName, user.Id))
                {
                    ViewBag.ErrorMessage = "Invalid User";
                    return;
                }
            }
            bool isNew = false;
            int newPassword = 0;
            if (user == null)
            {
                isNew = true;
                newPassword = new Random(DateTime.Now.Millisecond).Next(100000, 999999);
                string newPasswordHash = string.Empty;
                string newPasswordSaltHash = string.Empty;
                PasswordHashHelper.CreateHash(newPassword.ToString(), ref newPasswordHash,
                    ref newPasswordSaltHash);

                user = User_Account.GetNew();
            }

            model.Map(user);

            if (!this.SaveUser(user, out error))
            {
                ViewBag.ErrorMessage = error;
                return;
            }

            ViewBag.SuccessMessage = "Operation Completed";
            if (isNew)
                ViewBag.NewPassword = newPassword;
            user.Map(model);
        }

        private void UpdateCommand(UserModel model, string command)
        {
            var _db = HttpUtil.DbContext;
            string error = string.Empty;

            var userCredential = _db.User_Account.FirstOrDefault(uc => uc.Id == model.UserId);
            if (userCredential == null)
            {
                ViewBag.ErrorMessage = "Invalid User";
                return;
            }
       
            if (!HttpUtil.IsSupperAdmin())
            {
                //disallow delete super admin
                if (HttpUtil.IsSupperAdmin(userCredential.UserName, userCredential.Id))
                {
                    ViewBag.ErrorMessage = "Invalid User";
                    return;
                }
            }

            int newPassword = 0;
            bool isReset = false;

            switch (command)
            {
                case "Approve/Disapprove":
                    {
                        userCredential.IsApproved = !userCredential.IsApproved;
                        break;
                    }
                case "Lock/Unlock":
                    {
                        if (userCredential.UserStatus==User_Account.UserStatusEnum.Active)
                        {
                            userCredential.UserStatusEnumId = (byte)User_Account.UserStatusEnum.Inactive;
                        }
                        else
                        {
                            userCredential.UserStatusEnumId = (byte)User_Account.UserStatusEnum.Active;
                        }
                       
                        break;
                    }
                case "Reset":
                    {
                        isReset = true;
                        newPassword = new Random(DateTime.Now.Millisecond).Next(100000, 999999);
                        string newPasswordHash = string.Empty;
                        string newPasswordSaltHash = string.Empty;
                        PasswordHashHelper.CreateHash(newPassword.ToString(), ref newPasswordHash,
                            ref newPasswordSaltHash);

                        userCredential.Password = newPasswordHash;
                        userCredential.PasswordSalt = newPasswordSaltHash;
                        userCredential.LastPasswordChangedDate = DateTime.Now;
                        break;
                    }
                case "FailedPasswordAttemptCount":
                    {
                        userCredential.FailedPasswordAttemptCount = 0;
                        break;
                    }
                case "FailedPasswordAnswerAttemptCount":
        {
                        userCredential.FailedPasswordAnswerAttemptCount = 0;
                        break;
                    }
            }

            if (!this.SaveUser(userCredential, out error))
            {
                ViewBag.ErrorMessage = error;
                return;
            }

            ViewBag.SuccessMessage = "Operation Completed";
            if (isReset)
                ViewBag.NewPassword = newPassword;
            userCredential.Map(model);
        }

        private bool SaveUser(User_Account userCredential, out string error)
        {
            var _db = HttpUtil.DbContext;
            error = string.Empty;

            using (var context = _db.Database.BeginTransaction())
            {
                try
                {
                    User_Account obj = _db.User_Account.SingleOrDefault(cm => cm.Id == userCredential.Id);
                    if (obj == null)
                    {
                        obj = new User_Account();
                        obj.CreateDate =DateTime.Now;
                        _db.User_Account.Add(obj);
                    }

                    CopyUtil.Copy(userCredential, obj);

                    if (!obj.UserName.IsValid())
                    {
                        error = "User ID Can Not Be Empty";
                        return false;
                    }

                    if (!obj.FullName.IsValid())
                    {
                        error = "Full Name Can Not Be Empty";
                        return false;
                    }

                    if (_db.User_Account.Any(uc => uc.UserName == obj.UserName && uc.Id != obj.Id))
                    {
                        error = "Entered User ID Is Already Exist";
                        return false;
                    }

                    obj.LastChanged = DateTime.Now;
                    obj.LastChangedById = HttpUtil.Profile.UserId;

                    _db.SaveChanges();
                    context.Commit();

                    CopyUtil.CopyBack(obj, userCredential);

                    return true;
                }
                catch (DbEntityValidationException ex)
                {
                    var sb = new StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error1 in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error1.PropertyName, error1.ErrorMessage);
                            sb.AppendLine();
                        }
                    }
                    error = "OOPS! Something Went Wrong";
                    context.Rollback();
                    return false;
                }
                catch (Exception ex)
                {
                    error = "OOPS! Something Went Wrong";
                    context.Rollback();
                    return false;
                }
            }
        }

        #endregion User

        #region User Role
        public ActionResult Roles()
        {
            return View();
        }

        public ActionResult RollAssignedReport()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserRole.CanViewAssignedReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

        public ActionResult RoleList(string searchkey = "", int pageNo = 1, int pageSize = 50)
        {
            if (!HttpUtil.IsSupperAdmin())
                if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserRole.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            var result = new MvcModelListResult<UAC_Role>();
            var _db = HttpUtil.DbContext;
            IQueryable<UAC_Role> query = from r in _db.UAC_Role select r;

            if (!string.IsNullOrEmpty(searchkey)
                && !string.IsNullOrWhiteSpace(searchkey))
        {
                query = from c in query
                        where SqlFunctions.StringConvert((double)c.Id).Contains(searchkey)
                        || c.Name.Contains(searchkey)
                        select c;
            }
            //disallow edit in super admin
            if ( !HttpUtil.IsSupperAdmin())
            {
                //excluding SuperAdmin
                query = query.Where(x => x.Id !=1);

            }
            result.Count = query.Count();

            pageNo--;
            query = (from q in query
                     orderby q.Id
                     select q)
                    .Skip(pageNo * pageSize)
                .Take(pageSize);

            //if (!string.IsNullOrEmpty(includes)
            //  && !string.IsNullOrWhiteSpace(includes))
            //{
            //    var incs = includes.Split(',');

            //    query = incs.Aggregate(query,
            //    (current, i) => current.Include(i));
            //}


            result.Data = query.ToList();

            ViewBag.RoleResult = result;

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View();
        }

        public ActionResult RoleDelete(long id, string searchkey = "", int pageNo = 1, int pageSize = 50)
        {

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserRole.CanDelete))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            //disallow edit in super admin
            if (!HttpUtil.IsSupperAdmin() && id == 1)
            {
                //excluding SuperAdmin
                TempData["ErrorMessage"] = "Inavalid Id!";
                return RedirectToAction("Index", new { searchkey = searchkey, pageNo = pageNo, pageSize = pageSize });

            }
            string error = string.Empty;
            var db = HttpUtil.DbContext;

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserRole.CanDelete, out error))
            {
                TempData["ErrorMessage"] = error;
                return RedirectToAction("Index", new { searchkey = searchkey, pageNo = pageNo, pageSize = pageSize });
            }

            var role = db.UAC_Role.FirstOrDefault(ucc => ucc.Id == id);
            if (role != null)
            {
                try
                {
                
                    db.UAC_Role.Remove(role);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Operation Completed";
                }
                catch (Exception exception)
                {
                    TempData["ErrorMessage"] = "OOPS! Something Went Wrong";
                }
            }

            return RedirectToAction("Index", new { searchkey = searchkey, pageNo = pageNo, pageSize = pageSize });
        }

        public ActionResult RoleUpdate(long id)
        {
            if (!HttpUtil.IsSupperAdmin())
                if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserRole.CanEdit))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            //disallow edit in super admin
            if (!HttpUtil.IsSupperAdmin() && id == 1)
            {
                //excluding SuperAdmin
                TempData["ErrorMessage"] = "Inavalid Id!";
                 return View("");

            }
            var _db = HttpUtil.DbContext;
            var role = _db.UAC_Role.Include("UAC_RolePremissionMap").FirstOrDefault(uc => uc.Id == id);

            if (role == null)
                role = new UAC_Role() { Id = -1, UAC_RolePremissionMap = new Collection<UAC_RolePremissionMap>() };

            return View(role);
        }

        [HttpPost]
        public ActionResult RoleUpdate(UAC_Role model, string command, int[] permissions)
        {
            string error = string.Empty;
            //disallow edit in super admin
            if (!HttpUtil.IsSupperAdmin() && model.Id == 1)
            {
                //excluding SuperAdmin
                TempData["ErrorMessage"] = "Inavalid Id!";
                return View("");

            }
            var db = HttpUtil.DbContext;
            switch (command)
            {
                case "Save":
                    {
                        if (!HttpUtil.IsSupperAdmin())
                            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserRole.CanEdit, out error))
                        {
                            ViewBag.ErrorMessage = error;
                            break;
                        }
                        SaveCommand(model);
                        break;
                    }
                case "SavePermission":
                    {
                        if (!HttpUtil.IsSupperAdmin())
                            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserRole.CanChangePermissions, out error))
                        {
                            ViewBag.ErrorMessage = error;
                            break;
                        }
                        SavePermissionCommand(model, permissions);
                        break;
                    }
                default:
                    ViewBag.ErrorMessage = "Invalid Command";
                    break;
            }
            if (model.UAC_RolePremissionMap == null|| model.UAC_RolePremissionMap.Count<=0)
                model.UAC_RolePremissionMap = db.UAC_RolePremissionMap.Where(rp => rp.RoleId == model.Id).ToList();

            ModelState.Clear();
            return View(model);
        }

        public ActionResult RoleUser(long id)
        {
            if (!HttpUtil.IsSupperAdmin())
                if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.UserRole.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }//disallow edit in super admin
            if (!HttpUtil.IsSupperAdmin() && id == 1)
            {
                //excluding SuperAdmin
                id = -1;

            }

            List<UAC_RoleUserMap> roleUserList = HttpUtil.DbContext.UAC_RoleUserMap
                .Include("User_Account")
                .Include("User_Account.User_Employee")
                .Include("User_Account.User_Student")
                .Include("User_Account.Hr_Department")
                .Include("UAC_Role")
                .Where(uc => uc.RoleId == id)
                .OrderBy(x=>x.User_Account.UserTypeEnumId)
                .ToList();

            return View(roleUserList);
        }

        private void SavePermissionCommand(UAC_Role model, int[] permissions)
        {
            var db = HttpUtil.DbContext;

            var role = db.UAC_Role.Include("UAC_RolePremissionMap").FirstOrDefault(r => r.Id == model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = "Invalid Role";
                return;
            }
            //disallow super admin
            if (!HttpUtil.IsSupperAdmin() && role.Id == 1)
            {
                ViewBag.ErrorMessage = "Invalid Role";
                return;

            }

            using (var context = db.Database.BeginTransaction())
            {
                try
                {
                    var currentPermissions = role.UAC_RolePremissionMap.ToList();
                    foreach (var cp in currentPermissions)
                    {
                        if (permissions.Any(rp => rp == cp.PermissionNo))
                            continue;

                        db.UAC_RolePremissionMap.Remove(cp);
                    }

                    foreach (var permission in permissions)
                    {
                        if (role.UAC_RolePremissionMap.Any(rp => rp.PermissionNo == permission))
                            continue;

                        db.UAC_RolePremissionMap.Add(new UAC_RolePremissionMap()
                        {
                            Id = BigInt.NewBigInt(),
                            CreateDate = DateTime.Now,
                            LastChanged = DateTime.Now,
                            LastChangedById = HttpUtil.Profile.UserId,
                            RoleId = role.Id,
                            PermissionNo = permission
                        });
                    }

                    db.SaveChanges();
                    context.Commit();

                    CopyUtil.CopyBack(role, model);
                    ViewBag.SuccessMessage = "Operation Completed";
                }
                catch (DbEntityValidationException ex)
                {
                    var sb = new StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error1 in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error1.PropertyName, error1.ErrorMessage);
                            sb.AppendLine();
                        }
                    }
                    ViewBag.ErrorMessage = "OOPS! Something Went Wrong";
                    context.Rollback();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "OOPS! Something Went Wrong";
                    context.Rollback();
                }
            }

        }

        private void SaveCommand(UAC_Role model)
        {
            var context = HttpUtil.DbContext;

            var role = context.UAC_Role.FirstOrDefault(r => r.Id == model.Id);

            if (role == null)
            {
                role = new UAC_Role();
                role.Id = context.UAC_Role.Count()+1;
                while (context.UAC_Role.Any(uc => uc.Id == role.Id))
                {
                    role.Id += 1;
                }
                //if ()
                //{
                //    ViewBag.ErrorMessage = "Entered Role No Is Already Exist";
                //    return;
                //}
                role.CreateDate = DateTime.Now;
                context.UAC_Role.Add(role);
            }
            //disallow super admin
            if (!HttpUtil.IsSupperAdmin() && role.Id == 1)
            {
                ViewBag.ErrorMessage = "Invalid Role";
                return;

            }
            try
            {
                CopyUtil.Copy(model, role);
                role.LastChanged = DateTime.Now;
                role.LastChangedById = HttpUtil.Profile.UserId;


                if (!role.Id.ToString().IsInt64())
                {
                    ViewBag.ErrorMessage = "Invalid Id";
                    return;
                }

                if (!role.Name.IsValid())
                {
                    ViewBag.ErrorMessage = "Name Can Not Be Empty";
                    return;
                }

                if (context.UAC_Role.Any(uc => uc.Name == role.Name && uc.Id != role.Id))
                {
                    ViewBag.ErrorMessage = "Entered Name Is Already Exist";
                    return;
                }

                context.SaveChanges();
                CopyUtil.CopyBack(role, model);
                ViewBag.SuccessMessage = "Operation Completed";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "OOPS! Something Went Wrong";
            }
        }
        #endregion User Role
    }
}