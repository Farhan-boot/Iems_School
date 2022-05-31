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
        //TODO have to fix right permission for EMS. Not worked/used.
        [Permission(7)]
        public sealed class AccountingArea
        {
            public const int CanViewDashboard = 7;
            [Permission(701)]
            public sealed class BasicAccounting
            {
                public const int CanViewDashboard = 701;
                [Permission(70101)]
                public sealed class Ledger
                {
                    public const int CanView = 7010101;
                    public const int CanAdd = 7010102;
                    public const int CanEdit = 7010103;
                    public const int CanDelete = 7010104;
                    public const int CanChangeGroupStatus = 7010105;
                    public const int CanDeleteGroup = 7010106;
                    public const int CanDownload = 7010107;
                }

                [Permission(70102)]
                public sealed class Voucher
                {
                    public const int CanView = 7010201;
                    public const int CanAdd = 7010202;
                    public const int CanEdit = 7010203;
                    public const int CanPost = 7010204;
                    public const int CanUnpost = 7010205;
                    public const int CanDelete = 7010206;
                    public const int CanTrash = 7010207;
                }

                [Permission(70103)]
                public sealed class Reports
                {
                    public const int CanViewBalanceSheet = 7010301;
                    public const int CanViewIncomeStatement = 7010302;
                    public const int CanViewChartOfAccounts = 7010303;
                    public const int CanViewUGCReports = 7010304;
                    public const int CanViewDayBook = 7010305;
                    public const int CanViewUgcStaticReports = 7010399;

                }
                [Permission(70104)]
                public sealed class LedgerTransactions
                {
                    public const int CanViewLedgerTransactions = 7010401;
                }

                [Permission(70105)]
                public sealed class CompanyAccount
                {
                    public const int CanView = 7010501;
                    public const int CanAdd = 7010502;
                    public const int CanEdit = 7010503;
                    public const int CanDelete = 7010504;
                }

                [Permission(70106)]
                public sealed class Branch
                {
                    public const int CanView = 7010601;
                    public const int CanAdd = 7010602;
                    public const int CanEdit = 7010603;
                    public const int CanDelete = 7010604;
                }

                /*[Permission(70103)]
                public sealed class Journals
                {
                    public const int CanView = 70301;
                    public const int CanSave = 70302;
                    public const int CanPost = 70303;
                    public const int CanUnpost = 70304;
                    public const int CanDelete = 70305;
                }

                [Permission(70106)]
                public sealed class DayBooks
                {
                    public const int CanViewDayBook = 70601;
                    public const int CanPrintDayBook = 70602;
                }*/


            }

            [Permission(707)]
            public sealed class FeeCode
            {
                public const int CanView = 70701;
                public const int CanAdd = 70702;
                public const int CanEdit = 70703;
                public const int CanDelete = 70704;

            }

            [Permission(708)]
            public sealed class OfficialBank
            {
                public const int CanView = 70801;
                public const int CanAdd = 70802;
                public const int CanEdit = 70803;
                public const int CanDelete = 70804;
            }

            [Permission(709)]
            public sealed class ParticularName
            {
                public const int CanView = 70901;
                public const int CanAdd = 70902;
                public const int CanEdit = 70903;
                public const int CanDelete = 70904;

            }

            [Permission(710)]
            public sealed class ScholarshipType
            {
                public const int CanView = 71001;
                public const int CanAdd = 71002;
                public const int CanEdit = 71003;
                public const int CanDelete = 71004;

            }
            [Permission(711)]
            public sealed class StudentDiscountAndScholarship
            {
                public const int CanView = 71101;
                public const int CanAdd = 71102;
                public const int CanEdit = 71103;
                public const int CanDelete = 71104;
                public const int CanLockScholarship = 71105;
                public const int CanUnlockScholarship = 71106;
                public const int CanRecalculatePerCreditFee = 71107;
            }
            [Permission(712)]
            public sealed class StudentTransaction
            {
                public const int CanView = 71201;
                public const int CanAddDebit = 71202;
                public const int CanEditDebit = 71203;
                public const int CanDeleteDebit = 71204;

                public const int CanAddCredit = 71205;
                public const int CanEditCredit = 71206;
                public const int CanDeleteCredit = 71207;

                public const int CanHardDeleteTransaction = 71208;

                public const int CanRecalculateBalance = 71209;
                public const int CanRecalculateSemesterPayment = 71210;

                public const int CanPrintMoneyReceipt = 71211;
                public const int CanPrintPaySlip = 71212;
                public const int CanRegenerateStudentBill = 71213;

            }

            [Permission(713)]
            public sealed class StudentCollectionReports
            {
                public const int CanViewCollectionReports = 71301;
                public const int CanViewSemesterBill = 71302;
                public const int CanViewCollectionSummary = 71303;
                public const int CanViewStudentDueReport = 71304;
                public const int CanViewIncomeForecastReport = 71305;
                public const int CanViewBillingSummary = 71306;
                public const int CanViewWaiverSummary = 71307;
                public const int CanViewBillingReports = 71308;
                public const int CanViewWaiverReports = 71309;
                public const int CanViewFullPackageWaiverReports = 71310;
                public const int CanViewFullPackageSummary = 71311;
            }
            [Permission(714)]
            public sealed class StudentCollectionVoucher
            {
                public const int CanView = 71401;
                public const int CanAdd = 71402;
                public const int CanEdit = 71403;
                public const int CanClose = 71404;
                public const int CanDelete = 71405;
                public const int CanTrash = 71406;
                public const int CanPost = 71407;
                public const int CanUnpost = 71408;

            }

            [Permission(715)]
            public sealed class BankIntegration
            {
                [Permission(71501)]
                public sealed class EBL
                {
                    public const int CanUploadDropBoxExcel = 7150101;
                }
                public const int CanUploadBankPaymentExcel = 71502;
            }

            [Permission(716)]
            public sealed class SemesterWiseScholarship
            {
                public const int CanView = 71601;
                public const int CanAdd = 71602;
                public const int CanEdit = 71603;
                public const int CanDelete = 71604;
                public const int CanTrash = 71605;
                public const int CanUnTrash = 71606;
                public const int CanLock = 71607;
                public const int CanUnlock = 71608;
                public const int CanViewFirstSemesterScholarship = 71609;
                public const int CanLockFirstSemesterScholarship = 71610;
                public const int CanUnlockFirstSemesterScholarship = 71611;
                public const int CanCheckScholarshipEligibility = 71612;

            }
            [Permission(717)]
            public sealed class DataMigration
            {
                public const int CanUploadBillExcel = 71701;
            }


        }
    }
}
