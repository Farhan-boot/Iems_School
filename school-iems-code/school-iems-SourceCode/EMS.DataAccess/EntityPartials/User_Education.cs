using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class User_Education
    {
        #region Enum Property
        //public enum EducationTypeEnum
        //{
        //    Other = 0,
        //    General = 1,
        //    GCE = 2
        //}
        public enum DegreeEquivalentEnum
        {
            LowerPscEquivalent = 0,
            Psc5YearEquivalent = 1,
            Jsc8YearEquivalent = 2,
            Ssc10YearEquivalent = 3,
            Hsc12YearEquivalent = 4,
            //DiplomaEquivalent = 5,
            BachelorEquivalent = 5,
            MasterEquivalent = 6,
            PhDDoctoralEquivalent = 7,//Doctoral
            Other = 8,

        }
        /// <summary>
        /// not in db, putting value as text in column DegreeGroupMajor
        /// </summary>
        public enum DegreeGroupMajorEnum
        {
            Science = 1,
            Arts = 2,
            Commerce = 3,
            DiplomaOrTechnical = 4,
            Other = 5

        }
        public enum ResultSystemEnum
        {
            GPA = 0,
            FirstDivision = 1,
            SecondDivision = 2,
            ThirdDivision = 3,
            MarkSystem = 4
        }
        #endregion Enum Property
        #region Enum Property
        //[DataMember]
        //public EducationTypeEnum EducationType
        //{
        //    get
        //    {
        //        return (EducationTypeEnum)EducationTypeEnumId;
        //    }
        //    set
        //    {
        //        EducationTypeEnumId = (Byte)value;
        //    }
        //}
        public ResultSystemEnum ResultSystem
        {
            get { return (ResultSystemEnum)ResultSystemEnumId; }
            set
            {
                ResultSystemEnumId = (Byte)value;
            }
        }
        [DataMember]
        public DegreeEquivalentEnum DegreeEquivalent
        {
            get
            {
                return (DegreeEquivalentEnum)DegreeEquivalentEnumId;
            }
            set
            {
                DegreeEquivalentEnumId = (Byte)value;
            }
        }
        [DataMember]
        public DegreeGroupMajorEnum DegreeGroupMajor
        {
            get { return (DegreeGroupMajorEnum)DegreeGroupMajorEnumId; }
            set
            {
                DegreeGroupMajorEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        #region Constants
        public const string Property_IsDisable = "IsDisable";
        public const string Property_BoardTypeEnumId = "BoardTypeEnumId";
        public const string Property_IsRemove = "IsRemove";
        #endregion

        public static List<User_Education> ValidedEducationList(int userId, List<User_Education> educationList)
        {
            if (educationList==null)
            {
                educationList=new List<User_Education>();
            }
            if (educationList.All(x => x.DegreeEquivalent != User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent))
            {
                User_Education newEducationsSsc = User_Education.GetNew(1);
                newEducationsSsc.UserId = userId;
                //newEducationsSsc.EducationType = User_Education.EducationTypeEnum.General;
                newEducationsSsc.DegreeTitle = "Secondary School Certificate";
                newEducationsSsc.DegreeEquivalent = User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent;
                newEducationsSsc.DegreeCategoryId = 601; //for auto selected SSC 
                educationList.Add(newEducationsSsc);
            }
            if (educationList.All(x => x.DegreeEquivalent != User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent))
            {
                User_Education newEducationsHsc = User_Education.GetNew(2);
                newEducationsHsc.UserId = userId;
                //newEducationsHsc.EducationType=User_Education.EducationTypeEnum.General;
                newEducationsHsc.DegreeEquivalent = User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent;
                newEducationsHsc.DegreeTitle = "Higher Secondary Certificate";
                newEducationsHsc.DegreeCategoryId = 602; //for auto selected HSC
                educationList.Add(newEducationsHsc);

            }
         

            return educationList;
        }
        private static void GetNewExtra(User_Education obj)
        {
            
        }

    }
}
