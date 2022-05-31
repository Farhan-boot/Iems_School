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
    
    public partial class HR_MonthlyPayslipDetails
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HR_MonthlyPayslipDetails()
        {
            this.HR_MonthlyPayslipDetails1 = new HashSet<HR_MonthlyPayslipDetails>();
        }
    
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int MonthlyPayslipId { get; set; }
        public string Name { get; set; }
        public byte SalaryTypeEnumId { get; set; }
        public byte CategoryTypeEnumId { get; set; }
        public float OrderNo { get; set; }
        public float Value { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public long LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> ParentId { get; set; }
    
        public virtual HR_MonthlyPayslip HR_MonthlyPayslip { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HR_MonthlyPayslipDetails> HR_MonthlyPayslipDetails1 { get; set; }
        public virtual HR_MonthlyPayslipDetails HR_MonthlyPayslipDetails2 { get; set; }
        public virtual User_Employee User_Employee { get; set; }
    }
}