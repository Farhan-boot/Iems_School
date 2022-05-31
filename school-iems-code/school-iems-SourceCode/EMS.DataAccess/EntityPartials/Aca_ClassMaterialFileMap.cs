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
    public partial class Aca_ClassMaterialFileMap
    {
        #region Enum declaration
        #endregion Enum Property


        public string UploaderName
        {
            get
            {
                if (this.User_Account != null)
                {
                    return User_Account.FullName;
                }
                return "";
            }
        }



        private static void GetNewExtra(Aca_ClassMaterialFileMap obj)
        {

        }

    }
}
