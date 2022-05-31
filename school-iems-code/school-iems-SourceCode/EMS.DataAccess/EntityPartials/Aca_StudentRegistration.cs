//File: Entity Partial
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    public partial class Aca_StudentRegistration
    {
        #region Enum declaration
        [Flags]
        public enum RegStatusEnum
        {
            None = 0,
            PreRegPending = 1,
            PreRegCompleted = 2,
            PreRegCancelled = 3,
            RegPending = 4,
            RegCompleted = 5,
            RegCancelled = 6
        }
        [DataMember]
        public RegStatusEnum RegStatus
        {
            get { return (RegStatusEnum)RegStatusEnumId; }
            set
            {
                RegStatusEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(Aca_StudentRegistration obj)
        {

        }

    }
}
