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
    
    public partial class Lib_BookSubject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lib_BookSubject()
        {
            this.Lib_BookSubjectMap = new HashSet<Lib_BookSubjectMap>();
        }
    
        public long Id { get; set; }
        public string SubjectName { get; set; }
        public byte StatusEnumId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lib_BookSubjectMap> Lib_BookSubjectMap { get; set; }
    }
}
