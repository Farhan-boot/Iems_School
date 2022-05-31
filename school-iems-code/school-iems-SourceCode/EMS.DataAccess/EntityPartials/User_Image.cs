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
    public partial class User_Image
	{
          #region Enum Property
           [Flags]
           public enum ImageTypeEnum
           {
                none = 0,
           }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public ImageTypeEnum ImageType
        {
            get
            {
                return (ImageTypeEnum)ImageTypeEnumId;
            }
            set
            {
                ImageTypeEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void  GetNewExtra(User_Image obj)
          {
          
          }
        
	}
}
