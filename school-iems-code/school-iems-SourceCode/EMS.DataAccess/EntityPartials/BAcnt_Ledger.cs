//File: Entity Partial
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    /// <summary>
    /// You can add your custom code here.
    /// 
    /// </summary>
    public partial class BAcnt_Ledger
	{
        public float ChildTotalDebit { get; set; }
        public float ChildTotalCredit { get; set; }
        public int OrderNo { get; set; }
        public string RowString { get; set; }

        #region Enum declaration
           [Flags]
           public enum TypeEnum
           {
                Asset = 0,
                Liability = 1,
                Income = 2,
                Expense = 3,
                Equity = 4
            }
            [DataMember]
            public TypeEnum Type
            {
                get{return (TypeEnum)TypeEnumId;}
                set
                {
                    TypeEnumId = (Byte)value;
                }
            }
        #endregion Enum Property



        #region Ledger Transection Calculation (Not Use)
        //private List<BAcnt_Ledger> _items = new List<BAcnt_Ledger>();
        //private bool _isInCutState;
        //private double? _periodOpeningBalance;
        //private double? _periodTotalDebit;
        //private double? _periodTotalCredit;


        /// <summary>
        /// 
        /// </summary>
        public List<BAcnt_Ledger> Items
        {
            get;
            set;
            /*get { return _items; }
            set { _items = value; }*/
        }

        
        /// <summary>
        /// 
        /// </summary>
        public double Debit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Credit { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public double SumOfDebit
        {
            get
            {
                return
                    Items.Count == 0
                        ? Debit
                        : Items.Sum(i => i.SumOfCredit);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double SumOfCredit
        {
            get { return Items.Count == 0 ? Credit : Items.Sum(i => i.SumOfCredit); }
        }

        /// <summary>
        /// 
        /// </summary>
        public double SumOfBalance
        {
            get { return Math.Abs(SumOfDebit - SumOfCredit); }
        }


        /// <summary>
        /// 
        /// </summary>
        public double? LedgerDebitBalance //OpeningDebitBalance
        {
            get
            {
                if (SumOfBalanceWithoutAbsolute > 0)
                {
                    return SumOfBalance;
                }

                // return 0;
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? LedgerCreditBalance  //OpeningCreditBalance
        {
            get
            {
                if (SumOfBalanceWithoutAbsolute < 0)
                {
                    return SumOfBalance;
                }

                //return 0;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double SumOfBalanceWithoutAbsolute
        {
            get { return SumOfDebit - SumOfCredit; }
        }


        /// <summary>
        /// This properties are used for Trial Balance Sheet Report
        /// </summary>
        public float CurrentMonthDebitBalance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float CurrentMonthCreditBalance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float PreviousMonthDebitBalance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float PreviousMonthCreditBalance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TotalBeginYearDebitBalance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TotalBeginYearCreditBalance { get; set; }

        

        /// <summary>
        /// 
        /// </summary>
        public double? PeriodOpeningBalance
        {
            get;
            set;
            /*get
            {
                return _periodOpeningBalance;
            }
            set
            {
                _periodOpeningBalance = value;
            }*/
        }

        /// <summary>
        /// 
        /// </summary>
        public double? PeriodBegingingDebitBalance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? PeriodBegingingCreditBalance { get; set; }

        public double? PeriodClosingDebitBalnace { get; set; }

        public double? PeriodClosingCreditBalnace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double? PeriodTotalDebit
        {
            get;
            set;
            /*get { return _periodTotalDebit; }
            set { _periodTotalDebit = value; }*/
        }

        /// <summary>
        /// 
        /// </summary>
        public double? PeriodTotalCredit
        {
            get;
            set;
            /*get { return _periodTotalCredit; }
            set { _periodTotalCredit = value; }*/
        }

        /// <summary>
        /// 
        /// </summary>
        /*public double? TotalDebitBalance // ClosingDebitBalance
        {
            get
            {
                if (IsGroup)
                    return null;


                return Type == TypeEnum.Asset || Type == TypeEnum.Expense ? SumOfDebit - (OpenningBalance.HasValue ? OpenningBalance.Value : 0) : SumOfDebit;
            }
        }*/

        /// <summary>
        /// 
        /// </summary>
        /*public double? TotalCreditBalance // ClosingCreditBalance
        {
            get
            {
                if (IsGroup)
                    return null;

                return Type == TypeEnum.Income || Type == TypeEnum.Liability ? SumOfCredit - (OpenningBalance.HasValue ? OpenningBalance.Value : 0) : SumOfCredit;
            }
        }*/

        #endregion






        private static void  GetNewExtra(BAcnt_Ledger obj)
          {
          
          }


          #region Constants
            public const string Property_ChildTotalDebit = "ChildTotalDebit";
            public const string Property_ChildTotalCredit = "ChildTotalCredit";

          #endregion

    }
}
