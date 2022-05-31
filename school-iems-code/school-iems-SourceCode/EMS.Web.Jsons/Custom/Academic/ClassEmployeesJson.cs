using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Web.Jsons.Models
{
    public class ClassEmployeesJson
    {
        public List<User_EmployeeJson> User_EmployeeJsonList { get; set; }
        public long ClassId { get; set; }
        public byte RoleEnumId { get; set; }
        public byte SectionEnumId { get; set; }
        public byte StatusEnumId { get; set; }

        public ClassEmployeesJson()
        {
            User_EmployeeJsonList = new List<User_EmployeeJson>();
        }
    }
}
