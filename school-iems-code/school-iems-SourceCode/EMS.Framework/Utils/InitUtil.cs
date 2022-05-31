using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Routing;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework;
using EMS.Framework.Permissions;
using EMS.Framework.Helpers;
using EMS.Framework.Objects;

namespace EMS.Framework.Utils
{
    public static class InitUtil
    {
        public static void InitCredentials()
        {
            return;
            var db = HttpUtil.DbContext;
            //db.Database.CreateIfNotExists();
            if (db.User_Account.Any())
                return;

            string password = string.Empty;
            string passwordSalt = string.Empty;
            PasswordHashHelper.CreateHash("Vues#$007", ref password, ref passwordSalt);
            var userCredential = User_Account.GetNew(1);
            userCredential.FullName = "System Administrator";
            userCredential.UserName = "psdeveloper";
            userCredential.Password = password;
            userCredential.PasswordSalt = passwordSalt;
            userCredential.CreateDate = DateTime.Now;
            userCredential.LastChanged = DateTime.Now;
            userCredential.DateOfBirth = DateTime.Now;
            userCredential.UserStatusEnumId = (int)User_Account.UserStatusEnum.Active;
            userCredential.UserTypeEnumId = (int)User_Account.UserTypeEnum.Employee;
            userCredential.CreateById = 1;
            userCredential.LastChangedById = 1;
            db.User_Account.Add(userCredential);
            db.SaveChanges();

            var role = new UAC_Role()
            {
                Id = 1,
                Name = "Super Admin",
                Description = "This is Super Admin",
                CreateDate = DateTime.Now,
                LastChanged = DateTime.Now,
                LastChangedById = 0
            };
            db.UAC_Role.Add(role);
            db.SaveChanges();

            var userinRole = new UAC_RoleUserMap()
            {
                Id = 1,
                RoleId = role.Id,
                UserId = userCredential.Id,
                StartDate = DateTimeHelper.SqlMinDateTime,
                EndDate = DateTimeHelper.SqlMaxDateTime,
                LastChanged = DateTime.Now,
                CreateDate = DateTime.Now,
                LastChangedById = 0
            };

            db.UAC_RoleUserMap.Add(userinRole);
            db.SaveChanges();


            var role2 = new UAC_Role()
            {
                Id = 2,
                Name = "Admin",
                Description = "This is Admin",
                CreateDate = DateTime.Now,
                LastChanged = DateTime.Now,
                LastChangedById = 0
            };
            var role3 = new UAC_Role()
            {
                Id = 3,
                Name = "Employee",
                Description = "This is Employee",
                CreateDate = DateTime.Now,
                LastChanged = DateTime.Now,
                LastChangedById = 0
            };
            var role4 = new UAC_Role()
            {
                Id = 4,
                Name = "Student",
                Description = "This is Student",
                CreateDate = DateTime.Now,
                LastChanged = DateTime.Now,
                LastChangedById = 0
            };
            db.UAC_Role.Add(role2);
            db.UAC_Role.Add(role3);
            db.UAC_Role.Add(role4);
            db.SaveChanges();

            //AddRole(PermissionUtil.Permissions, role, db);
            //db.SaveChanges();
        }

        private static void AddRole(List<Permission> permissions, UAC_Role role, EmsDbContext _db)
        {
            var i = 1;
            foreach (var permission in permissions)
            {
                _db.UAC_RolePremissionMap.Add(new UAC_RolePremissionMap()
                {
                    Id = i,
                    PermissionNo = permission.No,
                    LastChanged = DateTime.Now,
                    LastChangedById = 0,
                    RoleId = role.Id
                });
                if (permission.ChildPermissions != null)
                    AddRole(permission.ChildPermissions, role, _db);
                i++;
            }

        }
    }
}