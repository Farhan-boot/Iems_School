using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Web.Jsons.Custom.HR.Employee
{
    public class AddNewEmployeeStep1Json
    {
        /// <summary>
        /// Need to set on after create
        /// </summary>
        public String Id { get; set; }//hidden
        /// <summary>
        /// User Input, check validation.
        /// </summary>
        public String UserName { get; set; }
        /// <summary>
        ///  User Input, check validation.
        /// </summary>
        public String FullName { get; set; }
        /// <summary>
        ///  User Input, check validation.
        /// </summary>
        public string UserEmail { get; set; }
        public string UserMobile { get; set; }
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// User Input, check validation.
        /// </summary>
        public Byte GenderEnumId { get; set; }
        /// <summary>
        ///  User Input, check validation.
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        ///  User Input, check validation.
        /// </summary>
        public int PositionId { get; set; }
        /// <summary>
        ///  User Input, check validation.
        /// </summary>
        public int EmployeeCategoryEnumId { get; set; }
        public int JobTypeEnumId { get; set; }
        public int EmployeeTypeEnumId { get; set; }
        public int JobClassEnumId { get; set; }
        public int WorkingStatusEnumId { get; set; }
        public int JoiningSemesterId { get; set; }
        public DateTime JoiningDate { get; set; }

        public bool IsAutoUserName { get; set; }
      

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
