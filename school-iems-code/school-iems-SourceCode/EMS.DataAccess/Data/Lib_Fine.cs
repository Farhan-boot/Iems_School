//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMS.DataAccess.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lib_Fine
    {
        public long Id { get; set; }
        public int BorrowerId { get; set; }
        public byte LibraryFineTypeEnumId { get; set; }
        public bool IsCredit { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public long LastChangedById { get; set; }
    
        public virtual Lib_Borrower Lib_Borrower { get; set; }
    }
}
