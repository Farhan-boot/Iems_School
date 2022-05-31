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
    
    public partial class Aca_HomeWork
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aca_HomeWork()
        {
            this.Aca_HomeWorkSubmission = new HashSet<Aca_HomeWorkSubmission>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public System.DateTime DueTime { get; set; }
        public System.DateTime CloseTime { get; set; }
        public Nullable<int> GroupEnumId { get; set; }
        public int HomeworkTypeEnumId { get; set; }
        public string HomeworkKey { get; set; }
        public string Description { get; set; }
        public int TeacherId { get; set; }
        public string Remark { get; set; }
        public string History { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
        public int ProgramId { get; set; }
        public long CourseId { get; set; }
        public float Mark { get; set; }
        public byte TeacherHomeworkTypeEnumId { get; set; }
    
        public virtual Aca_Course Aca_Course { get; set; }
        public virtual Aca_Program Aca_Program { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aca_HomeWorkSubmission> Aca_HomeWorkSubmission { get; set; }
    }
}
