using System.Collections.Generic;
using EMS.Web.Jsons.Models;

namespace EMS.Web.Jsons.Custom.Academic
{
    public class AcademicStudentJson
    {
        public User_StudentJson Student { get; set; }
        public User_AccountJson Account { get; set; }
        //todo remove
        public User_ImageJson Image { get; set; }
        public List<User_ContactAddressJson> ContactAddressList { get; set; }
        //todo remove
        public List<User_ContactEmailJson> ContactEmailList { get; set; }
        //todo remove
        public List<User_ContactNumberJson> ContactNumberList { get; set; }
        public List<User_EducationJson> EducationList { get; set; }
    }
}
