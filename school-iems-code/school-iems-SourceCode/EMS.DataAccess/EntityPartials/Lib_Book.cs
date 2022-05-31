using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Lib_Book
	{
         #region Enum Property
            public enum BookStatusEnum
        {
            UnChanged = 0,
            Replaced = 1
        }
            public enum BindingTypeEnum
        {
            Hard = 0,
            Paper = 1,
            Soft = 2
        }
        public enum SourceEnum
        {
            Manual = 0,
            LibraryOfCongress = 1,
            UtsLibrary = 2
        }
        public enum CategoryEnum
        {
            Text = 0,
            Reference = 1,
            General = 2,
            Defence = 3,
            Government = 4,
            ContinuingResources = 5,

        }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public BookStatusEnum BookStatus
        {
            get
            {
                return (BookStatusEnum)BookStatusEnumId;
            }
            set
            {
                BookStatusEnumId = (Byte)value;
            }
        }
        [DataMember]
        public BindingTypeEnum BindingType
        {
            get
            {
                return (BindingTypeEnum)BindingTypeEnumId;
            }
            set
            {
                BindingTypeEnumId = (Byte)value;
            }
        }
        [DataMember]
        public SourceEnum Source
        {
            get
            {
                return (SourceEnum)SourceEnumId;
            }
            set
            {
                SourceEnumId = (Byte)value;
            }
        }
        [DataMember]
        public CategoryEnum Category
        {
            get
            {
                return (CategoryEnum)CategoryEnumId;
            }
            set
            {
                CategoryEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private byte[] _imageFile;
        public byte[] ImageFile
        {
            get { return _imageFile; }
            set { _imageFile = value; }   //OnPropertyChanged("ImageFile");
        }
        
        public List<Lib_BookSubject> Lib_BookSubjectList { get; set; }
        
        public List<Lib_BookAuthor> Lib_BookAuthorList { get; set; }
        private static void  GetNewExtra(Lib_Book obj)
          {
          
          }
        
	}
}
