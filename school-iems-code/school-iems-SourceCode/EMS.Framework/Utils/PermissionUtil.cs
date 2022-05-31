using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EMS.Framework.Attributes;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;

namespace EMS.Framework.Utils
{
    public class PermissionUtil
    {
        public static List<Permission> Permissions { get; set; }
        static PermissionUtil()
        {
            Permissions = GetPermissions();
        }

        public static List<Permission> GetPermissions()
        {
            List<Permission> permissionList = new List<Permission>();
            Type[] parents = typeof(PermissionCollection).GetNestedTypes().Where(t => t.IsSealed).ToArray();
            foreach (var parent in parents)
            {
                    permissionList.Add(BuildTree(parent, null));
            }
            return permissionList;

        }
        private static Permission BuildTree(Type type, int? parentId)
        {
            var parent = new Permission();

            var attr = type.GetCustomAttribute(typeof(PermissionAttribute)) as PermissionAttribute ?? new PermissionAttribute(-99);
            int permissionNo = attr.PermissionNo;

            parent.No = permissionNo;
            parent.Title = type.Name;
            parent.ParentNo = parentId ?? 0;
            parent.IsChecked = false;
            parent.IsExpanded = false;

            parent.ChildPermissions = new List<Permission>();
            var nestedChilds = type.GetNestedTypes().ToArray();
            FieldInfo[] fields = type.GetFields();

            foreach (var field in fields)
            {
                var child = new Permission();
                child.No = Convert.ToInt32(field.GetValue(field));
                child.Title = field.Name;
                child.ParentNo = parent.No;
                child.IsChecked = false;
                child.IsExpanded = false;
                parent.ChildPermissions.Add(child);
            }

            foreach (var child in nestedChilds)
            {
                parent.ChildPermissions.Add(BuildTree(child, parent.No));
            }
            return parent;
        }

        public static bool HasPermission(int permissionNo)
        {
            string error = string.Empty;

            return HasPermission(permissionNo, out error);

        }

        public static bool HasPermission(int permissionNo, out string error)
        {
            error = string.Empty;

            if (HttpUtil.Profile == null)
            {
                error = "Your session has been expired please login again.";
                return false;
            }

            var db = HttpUtil.DbContext;
            var query = db.UAC_RoleUserMap.Where(ur => ur.UserId == HttpUtil.Profile.UserId).ToList().Select(ur => ur.RoleId);
            //from ur in db.UAC_RoleUserMap
            //where ur.UserId == HttpUtil.Profile.UserId
            //select ur.RoleId;

            var permissions =
                db.UAC_RolePremissionMap.Where(rp => query.Contains(rp.RoleId) && rp.PermissionNo == permissionNo)
                    .Select(rp => rp.PermissionNo);

            //from rp in db.UAC_RolePremissionMap
            //where query.Contains(rp.RoleId) && rp.PermissionNo == permissionNo
            //select rp.PermissionNo;

            var hasPermission = permissions.Any();

            if (!hasPermission)
            {
                error = "Sorry, you are not permitted to do this task.";
                return false;
            }
            return true;
        }

        #region Default Permission for Installation
        public static List<Permission> GetAuthorPermissions()
        {
            Type[] parents = typeof(PermissionCollection).GetNestedTypes().Where(t => t.IsSealed).ToArray();
            return parents.Select(parent => BuildTree(parent, null)).Where(t => t.ParentNo > 20000 && t.ParentNo < 30000).ToList();

        }
        public static List<Permission> GetEditorPermissions()
        {
            Type[] parents = typeof(PermissionCollection).GetNestedTypes().Where(t => t.IsSealed).ToArray();
            return parents.Select(parent => BuildTree(parent, null)).Where(t => t.ParentNo > 20000 && t.ParentNo < 20500).ToList();

        }
        public static List<Permission> GetContributerPermissions()
        {
            Type[] parents = typeof(PermissionCollection).GetNestedTypes().Where(t => t.IsSealed).ToArray();
            return parents.Select(parent => BuildTree(parent, null)).Where(t => t.ParentNo > 20000 && t.ParentNo < 20300).ToList();
        }
        #endregion Default Permission for Installation
    }
}
