using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Framework.Permissions;

namespace EMS.Web.Jsons.Custom.BasicAccounting
{
    public class LedgerTransactionJson
    {
        public int LedgerId { get; set; }
        public string LedgerName { get; set; }
        public string LedgerParentCode { get; set; }
        public string LedgerCodeName { get; set; }

        // Opening Balance
        public double PeriodOpeningBalance { get; set; }

        public double PeriodOpeningDebitBalance { get; set; }
        public double PeriodOpeningCreditBalance { get; set; }


        // Period Beginning Debit and Credit Amount
        public double PeriodBeginningDebitBalance { get; set; }
        public double PeriodBeginningCreditBalance { get; set; }

        // Period Debit and Credit Amount
        public double PeriodTotalDebitAmount { get; set; }
        public double PeriodTotalCreditAmount { get; set; }

        // Closing Balance
        public double PeriodClosingDebitBalance { get; set; }
        public double PeriodClosingCreditBalance { get; set; }

        public List<VoucherJson> VoucherJsonList { get; set; }

        public static LedgerTransactionJson GetNew()
        {
            var obj = new LedgerTransactionJson();
            obj.VoucherJsonList = new List<VoucherJson>();
            return obj;
        }
    }
    public class VoucherJson
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        /// <summary>
        /// Ledger Name
        /// </summary>
        public string Particular { get; set; }
        /// <summary>
        /// Ledger Code Name
        /// </summary>
        public string CodeName { get; set; }
        public string VoucherType { get; set; }
        public string VoucherNo { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public double VoucherTotalAmount { get; set; }
        public string BranchName { get; set; }
        public string Narration { get; set; }
        public double SelectedLedgerCrAmount { get; set; }
        public double SelectedLedgerDrAmount { get; set; }
        public double SelectedLedgerBalance { get; set; }
        public string BalanceType { get; set; }
        public List<VoucherDetailsJson> VoucherDetailsJsonList { get; set; }

        public static VoucherJson GetNew()
        {
            var obj = new VoucherJson();
            obj.VoucherDetailsJsonList = new List<VoucherDetailsJson>();
            return obj;

        }


    }
    public class VoucherDetailsJson
    {
        public int Id { get; set; }
        /// <summary>
        /// Ledger Code
        /// </summary>
        public string Particular { get; set; }
        /// <summary>
        /// Ledger Code Name
        /// </summary>
        public string CodeName { get; set; }
        public double DebitAmount { get; set; }
        public double CreditAmount { get; set; }
    }
}
