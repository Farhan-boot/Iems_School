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
        [Permission(2)]
        public sealed class HumanResourceArea
        {
            public const int CanViewDashboard = 2;
            public const int AllDepartmentManager = 21;
            [Permission(201)]
            public sealed class EmployeeManager
            {
                [Permission(20101)]
                public sealed class Employee
                {
                    public const int CanView = 2010101;
                    public const int CanAdd = 2010102;
                    public const int CanEdit = 2010103;
                    public const int CanDelete = 2010104;

                    public const int CanApproval = 2010105;
                    public const int CanChangeStatus = 2010106;
                    public const int CanResetPassword = 2010107;
                    public const int CanUploadProfilePicture = 2010108;
                    public const int CanRemoveProfilePicture = 2010109;
                    public const int CanLoginToPortal = 2010110;

                }
                [Permission(20102)]
                public sealed class Education
                {
                    public const int CanView = 2010201;
                    public const int CanAdd = 2010202;
                    public const int CanEdit = 2010203;
                    public const int CanDelete = 2010204;

                }


            }
            [Permission(202)]
            public sealed class RankManager
            {
                public const int CanView = 2010201;
                public const int CanAdd = 2010202;
                public const int CanEdit = 2010203;
                public const int CanDelete = 2010204;
            }
            [Permission(203)]
            public sealed class PositionManager
            {
                public const int CanView = 2010301;
                public const int CanAdd = 2010302;
                public const int CanEdit = 2010303;
                public const int CanDelete = 2010304;
            }

            [Permission(204)]
            public sealed class Payroll
            {
                public const int CanView = 2010401;
                public const int CanAdd = 2010402;
                public const int CanEdit = 2010403;
                public const int CanDeletePermanently = 2010404;
                public const int CanTrash = 2010405;
                public const int CanUnTrash = 2010406;
            }

            [Permission(205)]
            public sealed class SalaryTemplate
            {
                public const int CanView = 2010501;
                public const int CanAdd = 2010502;
                public const int CanEdit = 2010503;
                public const int CanDelete = 2010504;
                public const int CanTrash = 2010505;
                public const int CanUnTrash = 2010506;
                public const int CanAddDetails = 2010507;
                public const int CanEditDetails = 2010508;
                public const int CanDeleteDetails = 2010509;
            }
            [Permission(206)]
            public sealed class SalarySettings
            {
                public const int CanView = 2010601;
                public const int CanAdd = 2010602;
                public const int CanEdit = 2010603;
                public const int CanDelete = 2010604;
                public const int CanTrash = 2010605;
                public const int CanUnTrash = 2010606;
                public const int CanAddDetails = 2010607;
                public const int CanEditDetails = 2010608;
                public const int CanDeleteDetails = 2010609;
            }

            [Permission(207)]
            public sealed class MonthlyPayslip
            {
                public const int CanView = 2010701;
                public const int CanAdd = 2010702;
                public const int CanEdit = 2010703;
                public const int CanDelete = 2010704;
                public const int CanTrash = 2010705;
                public const int CanUnTrash = 2010706;
                public const int CanAddDetails = 2010707;
                public const int CanEditDetails = 2010708;
                public const int CanDeleteDetails = 2010709;
            }

            [Permission(208)]
            public sealed class Reports
            {
                public const int CanViewMonthlySalaryDetailsReport = 2010801;
                public const int CanViewMonthlySalarySummaryReport = 2010802;
                public const int CanViewMonthlySalaryWithBankDetails = 2010803;
                public const int CanViewYearlySalarySummaryReport = 2010804;
            }
        }
    }
}
