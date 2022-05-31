using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;

namespace EMS.Web.UI.Areas.Student.Models
{
    public class CourseViewModel
    {
        public Aca_Class Course { get; set; }
        public List<Aca_ClassAttendance> ClassAttendanceList { get; set; }
        public CourseResultViewModel CourseResultViewModel { get; set; }
        public List<Aca_ClassMaterialFileMap> ClassMaterialList { get; set; }
        public List<Aca_ClassNotice> ClassNoticeList { get; set; }
    }
}
