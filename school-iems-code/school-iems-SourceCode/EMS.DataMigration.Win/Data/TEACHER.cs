//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMS.DataMigration.Win.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TEACHER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TEACHER()
        {
            this.T_EDUCATION = new HashSet<T_EDUCATION>();
        }
    
        public string ID { get; set; }
        public string FNAME { get; set; }
        public string MNAME { get; set; }
        public string LNAME { get; set; }
        public string NNAME { get; set; }
        public string FATHER_NAME { get; set; }
        public string MAILING_ADDRESS { get; set; }
        public string PARMANENT_ADDRERSS { get; set; }
        public string PHONE { get; set; }
        public string MOBILE { get; set; }
        public string SEX { get; set; }
        public string RELIGION { get; set; }
        public string MOTHER_NAME { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string BLOOD_GROUP { get; set; }
        public string NATIONALITY { get; set; }
        public string PRIMARY_MAJOR { get; set; }
        public string SECENDARY_MAJOR { get; set; }
        public string SHIFT { get; set; }
        public string AID_PERCENT { get; set; }
        public string REASON_OF_AID { get; set; }
        public Nullable<double> PROGRAM_ID { get; set; }
        public Nullable<double> SEMESTER_ID { get; set; }
        public Nullable<System.DateTime> ADMISSION_DATE { get; set; }
        public Nullable<double> YEAR { get; set; }
        public string PROGRAM { get; set; }
        public string STSESSION { get; set; }
        public Nullable<double> TOTAL_COURSE_FEE { get; set; }
        public Nullable<double> TOTAL_SEMESTER { get; set; }
        public Nullable<double> ADMISSION_FEE { get; set; }
        public Nullable<double> EACH_SEMESTER_FEE { get; set; }
        public Nullable<double> WAIVER_AMOUNT { get; set; }
        public string HIDE { get; set; }
        public string GEMAIL { get; set; }
        public string GMOBILE { get; set; }
        public string GPHONE { get; set; }
        public string GADDRESS { get; set; }
        public string RG { get; set; }
        public string LG { get; set; }
        public string COUNTRY_OF_BIRTH { get; set; }
        public Nullable<double> DISTRICT { get; set; }
        public Nullable<double> THANA { get; set; }
        public string EMAIL { get; set; }
        public string M_CELL { get; set; }
        public string F_CELL { get; set; }
        public string ADMISSION_TYPE { get; set; }
        public string USER_ID { get; set; }
        public string PASSWORD { get; set; }
        public byte[] PHOTO { get; set; }
        public string EDU_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_EDUCATION> T_EDUCATION { get; set; }
    }
}
