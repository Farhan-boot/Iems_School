using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Aca_Curriculum
	{
        #region Enum Property
        //public enum TypeEnum
        //{
        //    Core = 0,
        //    Specialization = 1,
        //    Elective = 2,

        //}
        #endregion Enum Property
        #region Enum Property
        //[DataMember]
        //public TypeEnum Type
        //{
        //    get
        //    {
        //        return (TypeEnum)TypeEnumId;
        //    }
        //    set
        //    {
        //        TypeEnumId = (Byte)value;
        //    }
        //}
        #endregion Enum Property

        private static void  GetNewExtra(Aca_Curriculum obj)
        {
            obj.Session = DateTime.Now.Year.ToString()+"-"+DateTime.Now.AddYears(3).Year.ToString();
            obj.IsValid = true;
            obj.IsOffering = true;
        }
        
	}
}
