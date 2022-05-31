using System.Collections.Generic;
using EMS.Web.Jsons.Models;

namespace EMS.Web.Jsons.Custom.HR
{
    public class HrEmployeeJson
    {
        public User_EmployeeJson Employee { get; set; }
        public User_AccountJson Account { get; set; }
        public string ConfirmPassword { get; set; }
        public User_ImageJson Image { get; set; }
        public List<User_ContactAddressJson> ContactAddressList { get; set; }
        public List<User_ContactEmailJson> ContactEmailList { get; set; }
        public List<User_ContactNumberJson> ContactNumberList { get; set; }
        public List<User_EducationJson> EducationList { get; set; }
        public List<HR_AppointmentHistoryJson> AppointmentHistoryList { get; set; }
    }
}
