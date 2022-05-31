//File: Entity Partial
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    public partial class General_File
    {
        #region Enum declaration
        [Flags]
        public enum TypeEnum
        {
            ClassNote = 0,
            ProfilePicture = 1,
            Assignment = 2,
            Book = 3,

        }
        [DataMember]
        public TypeEnum Type
        {
            get { return (TypeEnum)TypeEnumId; }
            set
            {
                TypeEnumId = (Byte)value;
            }
        }
        #endregion Enum Property
        public string FileSize => FileHelper.FileSize(this.SizeInByte,2);

        private static void GetNewExtra(General_File obj)
        {

        }

    }
}
