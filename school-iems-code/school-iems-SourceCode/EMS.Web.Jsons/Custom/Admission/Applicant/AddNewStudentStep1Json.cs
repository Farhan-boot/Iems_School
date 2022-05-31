using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Framework.Settings;
using EMS.Web.Jsons.Models;

namespace EMS.Web.Jsons.Custom.Admission.Applicant
{
    public class AddNewStudentStep1Json
    {
        /// <summary>
        /// Need to set on after create
        /// </summary>
        public String Id { get; set; }//hidden
        /// <summary>
        /// User Input, check validation.
        /// </summary>
        public String UserName { get; set; }
        public String UgcUniqueId { get; set; }
        /// <summary>
        ///  User Input, check validation.
        /// </summary>
        public String FullName { get; set; }
        /// <summary>
        ///  User Input, check validation.
        /// </summary>
        public int AdmissionExamId { get; set; }
        /// <summary>
        ///  User Input, check validation.
        /// </summary>
        public String ProgramId { get; set; }
        public String CurriculumId { get; set; }
        /// <summary>
        ///  User Input, check validation.
        /// </summary>
        public Int32 FeeCodeId { get; set; }
        /// <summary>
        ///  User Input, check validation.
        /// </summary>
        public Byte EnrollmentTypeEnumId { get; set; }
        public String PreviousProgramId { get; set; }
        public String PreviousStudentUserName { get; set; }
        public bool IsFullManualId { get; set; }
        public bool IsAutoRegisterCourses { get; set; }
        public static int StudentIdGenerateTypeId { get; set; }
        public float SscGpa { get; set; }
        public int SscDegreeCategoryId { get; set; }
        public bool SscIsGolden { get; set; }
        public float HscGpa { get; set; }
        public int HscDegreeCategoryId { get; set; }
        public bool HscIsGolden { get; set; }
        public float AverageGpa { get; set; }
        public bool AverageIsGolden { get; set; }

        public SiteSettings.StudentIdGenerateTypeEnum StudentIdGenerateType
        {
            get
            {
                return (SiteSettings.StudentIdGenerateTypeEnum)StudentIdGenerateTypeId;
            }
            set
            {
                StudentIdGenerateTypeId = (Byte)value;
            }
        }
        /// <summary>
        /// User Input, check validation.
        /// </summary>
        public Byte GenderEnumId { get; set; }

        /// <summary>
        ///  User Input, check validation.
        /// </summary>
        public Int32 ContinuingBatchId { get; set; }
        public Int32 CampusId { get; set; }
        /// <summary>
        /// User Input, check validation.
        /// </summary>
        public int ClassSectionId { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string Remark { get; set; }

        public string MaxUserName { get; set; }
        public string SuggestNextUserName { get; set; }

        //public DateTime DateAdmitted { get; set; }

        //public string IdPrefix { get; set; }
        //public Int32 DepartmentName { get; set; }
        //public Int32 DepartmentCode { get; set; }
        //public Byte ClassTimingGroupEnumId { get; set; }
        //public String JoiningSemesterName { get; set; }
        //public String JoiningSemesterId { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public string Gender { get; set; }
        //public string BloodGroup { get; set; }
        //public String FatherName { get; set; }
        //public String MotherName { get; set; }
        //public String EmergencyMobile { get; set; }
        //public String UserMobile { get; set; }



    }
}
