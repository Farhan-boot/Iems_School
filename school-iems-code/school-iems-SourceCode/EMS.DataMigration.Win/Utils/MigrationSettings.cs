using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DataMigration.Win.Utils
{
    public class MigrationSettings
    {
        private static MigrationSettings _instance;
        public static MigrationSettings Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = new MigrationSettings();

                return _instance;
            }
        }
        public static bool IsSaveRegData { get; set; }
        public static bool IsBillGenerate { get; set; }
        public static bool IsSaveResultData { get; set; }

        //Backlog
        public static bool IsSaveBacklogRegData { get; set; }
        public static bool IsSaveBacklogResultData { get; set; }

        
    }
}
