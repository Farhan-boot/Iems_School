using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Web.Jsons.Models
{
    public class EmployeeJson
    {
        public User_EmployeeJson Employee { get; set; }
        //TODO  User_AccountJson will be the custom object
        public User_AccountJson Account { get; set; }
        public string ConfirmPassword { get; set; }
        public User_ImageJson Image { get; set; }
        public List<User_ContactAddressJson> ContactAddressList { get; set; }
        public List<User_ContactEmailJson> ContactEmailList { get; set; }
        public List<User_ContactNumberJson> ContactNumberList { get; set; }
        //public List<User_EducationJson> EducationList { get; set; }
        public List<HR_AppointmentHistoryJson> AppointmentHistoryList { get; set; }
        public string ImageElementId { get; set; }
        public string TokenElementId { get; set; }
        public string InputElementId { get; set; }
    }
}
