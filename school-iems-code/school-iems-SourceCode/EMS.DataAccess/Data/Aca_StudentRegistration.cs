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
    
    public partial class Aca_StudentRegistration
    {
        public int Id { get; set; }
        public long SemesterId { get; set; }
        public int LevelId { get; set; }
        public int StudentId { get; set; }
        public string RegistrationNo { get; set; }
        public string IPAddress { get; set; }
        public string HostAddress { get; set; }
        public byte RegStatusEnumId { get; set; }
        public int FeeCodeId { get; set; }
        public int BankId { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public long LastChangedById { get; set; }
    
        public virtual Aca_StudyLevel Aca_StudyLevel { get; set; }
        public virtual User_Student User_Student { get; set; }
    }
}
