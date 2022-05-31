using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;

namespace EMS.Web.Jsons.Custom.Academic
{
    public class RegisteredCourseJson
    {
        public long ClassId { get; set; }
        public long SemesterId { get; set; }
        public long CurriculumCourseId { get; set; }
        public long BaseCourseId { get; set; }
        public float? GradePoint { get; set; }
        public byte RegistrationStatusEnumId { get; set; }
        public bool IsPassed { get; set; }
    }
}
