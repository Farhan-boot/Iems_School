using System;

namespace EMS.Framework.Objects
{
    [Serializable]
    public class UserProfile
    {
        /// <summary>
        /// ID of User_Employee/User_Student Table, Not User_Account Table.
        /// </summary>
        //public int Id { get; set; }

        /// <summary>
        /// ID of User_Account Table
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// ID of User_Employee Table
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// ID of User_Student Table
        /// </summary>
        public int StudentId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string RankName { get; set; }
        public byte UserTypeId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long CampusId { get; set; }
        public long DepartmentId { get; set; }
        public string InstituteKey { get; set; }
        public DateTime? LoginTime { get; set; }
       
    }
}
