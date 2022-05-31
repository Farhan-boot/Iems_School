using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;

namespace EMS.Web.Jsons.Custom.Registration
{
    public class BulkStudentRegistrationJson
    {
        public class CommonParametersJson
        {
            public long SemesterId { get; set; }
            public int ProgramId { get; set; }
            public long StudyLevelTermId { get; set; }
            public int BatchId { get; set; }
            public int CampusId { get; set; }
            public int ClassSectionId { get; set; }
        }

        public CommonParametersJson ParameterJson { get; set; }
        public List<Aca_ClassJson> ClassListJson { get; set; }
        public List<User_StudentJson> StudentListJson { get; set; }

        public static BulkStudentRegistrationJson GetNew()
        {
            var  obj=new BulkStudentRegistrationJson();
            obj.ParameterJson = new CommonParametersJson();
            obj.ClassListJson =new List<Aca_ClassJson>();
            obj.StudentListJson =new List<User_StudentJson>();
            return obj;
        }



    }




    //section status
    //student count
    //capacity
    //routine
    //curriculmn course details
    //class details
    //Registration Status


    //ClassName
    //ClassCode
    //Credit
    //Category
    //StringRoutineList
    //SectionStatusEnum +Value
    //Capacity
    //StudentCount
    //RegistrationStatus
    //IsAlreadyAdded
    //ClassId
    //CurriculmCourseId
}
