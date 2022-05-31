using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Custom.Admission.AdmissionExam
{
    public class DefaultSettingsJson
    {
        public bool IsEnableProgramWiseBatchMap { get; set; }
        public List<ProgramWiseBatchMapJson> ProgramWiseBatchMapJsonList { get; set; }

        public DefaultSettingsJson()
        {
            IsEnableProgramWiseBatchMap = false;
            ProgramWiseBatchMapJsonList = new List<ProgramWiseBatchMapJson>();
        }
    }

    public class ProgramWiseBatchMapJson
    {
        public int ProgramId { get; set; }
        public int BatchId { get; set; }
    }
}
