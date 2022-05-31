using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Web.Jsons.Custom.Academic
{
    public class StudentSemesterWiseResultJson
    {
        public class ResultJson
        {
            public string CourseName { get; set; }
            public string CourseCode { get; set; }
            public float CreditLoad { get; set; }
            public byte RegistrationStatusEnumId { get; set; }
            public string RegistrationStatus { get; set; }
            public string Grade { get; set; }
            public float GradePoint { get; set; }
            public string MidtermMark { get; set; }

            public ResultJson()
            {
                CourseName = String.Empty;
                CourseCode = String.Empty;
                CreditLoad = 0.0F;
                RegistrationStatus = String.Empty;
                Grade = String.Empty;
                GradePoint = 0.0F;
                MidtermMark = String.Empty;
            }

        }

    }
}
