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
    
    public partial class Aca_ProgramTakenByStudent
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public int StudentId { get; set; }
        public string RollNo { get; set; }
        public string Remark { get; set; }
        public string History { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Aca_Program Aca_Program { get; set; }
        public virtual User_Student User_Student { get; set; }
    }
}
