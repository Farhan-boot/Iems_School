using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Web.Jsons.Models
{
    public class ClassStudentsJson
    {
        public List<User_StudentJson> User_StudentJsonList { get; set; }
        public long ClassId { get; set; }
        public byte EnrollTypeEnumId { get; set; }
        public byte RegistrationStatusEnumId { get; set; }
        public byte StatusEnumId { get; set; }
    }
}
