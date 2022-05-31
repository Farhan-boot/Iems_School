//File: Entity Partial
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    /// <summary>
    /// You can add your custom code here. General
    /// </summary>
    public partial class Aca_MarkDistributionPolicyComponent
    {
        #region Enum declaration
        [Flags]
        public enum TestTypeEnum
        {
            MidTerm = 0,
            FinalTerm = 1,
            ClassTest = 2,
            Attendance = 3,
            Observation = 4,
            AssesmentOrExam = 5,
            //Term_2 = 6,
               
        }
        #endregion Enum declaration
        #region Enum Property
        [DataMember]
        public TestTypeEnum TestType
        {
            get { return  (TestTypeEnum)TestTypeEnumId ; }
            set
            {
                TestTypeEnumId = (Byte)value;
            }
        }

        #endregion Enum Property

        private static void GetNewExtra(Aca_MarkDistributionPolicyComponent obj)
        {

        }

    }
}
