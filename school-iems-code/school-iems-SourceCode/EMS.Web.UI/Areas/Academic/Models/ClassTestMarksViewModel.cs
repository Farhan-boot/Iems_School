using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;

namespace EMS.Web.UI.Areas.Academic.Models
{
    public class ClassTestMarksViewModel
    {
        public Aca_Class Course { get; set; }
        public List<Aca_ClassTakenByStudent> ClassStudentList { get; set; }
        public List<Aca_ClassTakenByEmployee> ClassFacultyList { get; set; }

    }
}
