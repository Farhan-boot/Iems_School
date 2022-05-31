using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Framework.Attributes;

namespace EMS.Framework.Permissions
{
    public partial class PermissionCollection
    {
        [Permission(1)]
        public sealed class AdminArea
        {
            public const int CanViewDashboard = 1;
            [Permission(101)]
            public sealed class UserCredential
            {
                [Permission(10101)]
                public sealed class UserAccess
                {
                    public const int CanView = 1010101;
                    public const int CanAdd = 1010102;
                    public const int CanEdit = 1010103;
                    public const int CanDelete = 1010104;

                    public const int CanResetPassword = 1010105;
                    public const int CanChangeStatus = 1010106;
                    public const int CanResetLoginFailedCount = 1010107;

                    public const int CanManageRole = 1010108;
                }

                [Permission(10102)]
                public sealed class UserRole
                {
                    public const int CanView = 1010201;
                    public const int CanAdd = 1010202;
                    public const int CanEdit = 1010203;
                    public const int CanDelete = 1010204;

                    public const int CanChangePermissions = 1010205;
                    public const int CanViewAssignedReport = 1010206;
                }

                //[Permission(10103)]
                //public sealed class UserAccess
                //{
                //    public const int CanView = 1010301;
                //    public const int CanAdd = 1010302;
                //    public const int CanEdit = 1010303;
                //    public const int CanDelete = 1010304;
                //}

                [Permission(10103)]
                public sealed class AuditManager
                {
                    public const int CanViewUserNameChangeAudit = 1010301;
                    public const int CanViewPasswordChangeAudit = 1010302;
                    public const int CanViewLoginAudit = 1010303;
                    public const int CanDeleteAuditLog = 1010304;
                }
            }
            [Permission(102)]
            public sealed class Organization
            {
                public const int CanView = 10201;
                public const int CanAdd = 10202;
                public const int CanEdit = 10203;
                public const int CanDelete = 10204;
            }
            [Permission(103)]
            public sealed class Department
            {
                public const int CanView = 10301;
                public const int CanAdd = 10302;
                public const int CanEdit = 10303;
                public const int CanDelete = 10304;
            }
            [Permission(104)]
            public sealed class Campus
            {
                public const int CanView = 10401;
                public const int CanAdd = 10402;
                public const int CanEdit = 10403;
                public const int CanDelete = 10404;
            }
            [Permission(105)]
            public sealed class Building
            {
                public const int CanView = 10501;
                public const int CanAdd = 10502;
                public const int CanEdit = 10503;
                public const int CanDelete = 10504;
            }
            [Permission(106)]
            public sealed class Floor
            {
                public const int CanView = 10601;
                public const int CanAdd = 10602;
                public const int CanEdit = 10603;
                public const int CanDelete = 10604;
            }
            [Permission(107)]
            public sealed class Room
            {
                public const int CanView = 10701;
                public const int CanAdd = 10702;
                public const int CanEdit = 10703;
                public const int CanDelete = 10704;
            }
            [Permission(108)]
            public sealed class District
            {
                public const int CanView = 10801;
                public const int CanAdd = 10802;
                public const int CanEdit = 10803;
                public const int CanDelete = 10804;
            }
            [Permission(109)]
            public sealed class PoliceStation
            {
                public const int CanView = 10901;
                public const int CanAdd = 10902;
                public const int CanEdit = 10903;
                public const int CanDelete = 10904;
            }
            [Permission(110)]
            public sealed class PostOffice
            {
                public const int CanView = 11001;
                public const int CanAdd = 11002;
                public const int CanEdit = 11003;
                public const int CanDelete = 11004;
            }
            [Permission(111)]
            public sealed class Country
            {
                public const int CanView = 11101;
                public const int CanAdd = 11102;
                public const int CanEdit = 11103;
                public const int CanDelete = 11104;
            }

            [Permission(112)]
            public sealed class Division
            {
                public const int CanView = 11201;
                public const int CanAdd = 11202;
                public const int CanEdit = 11203;
                public const int CanDelete = 11204;
            }

            [Permission(113)]
            public sealed class Relationship
            {
                public const int CanView = 11301;
                public const int CanAdd = 11302;
                public const int CanEdit = 11303;
                public const int CanDelete = 11304;
            }

            [Permission(114)]
            public sealed class CreditTransferInstitute
            {
                public const int CanView = 11401;
                public const int CanAdd = 11402;
                public const int CanEdit = 11403;
                public const int CanDelete = 11404;
            }
            [Permission(115)]
            public sealed class SiteSettingsManager
            {
                public const int CanView = 11501;
                public const int CanEdit = 11502;
            }

        }
    }
}
