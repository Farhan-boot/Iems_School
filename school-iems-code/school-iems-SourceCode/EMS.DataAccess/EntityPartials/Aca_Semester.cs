using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Aca_Semester
    {
        #region Enum Property
        //public enum ProgramTypeEnum
        //{
        //    All = 0,
        //    Undergraduate = 1,
        //    Postgraduate = 2
        //}
        public Aca_Program.ProgramTypeEnum ProgramType
        {
            get { return (Aca_Program.ProgramTypeEnum)ProgramTypeEnumId; }
            set
            {
                ProgramTypeEnumId = (Byte)value;
            }
        }
        public enum StatusEnum
        {
            Completed = 0,
            Ongoing = 1,
            Upcomming = 2
        }
        public enum EnabledRegistrationTypeEnum
        {
            None = 0,
            Preregistration = 1,
            FinalRegistration = 2
        }
        public enum SemesterDurationEnum
        {
            _4M= 4,
            _6M= 6
        }


        [DataMember]
        public StatusEnum Status
        {
            get
            {
                return (StatusEnum)StatusEnumId;
            }
            set
            {
                StatusEnumId = (Byte)value;
            }
        }

        [DataMember]
        public SemesterDurationEnum SemesterDuration
        {
            get
            {
                return (SemesterDurationEnum)SemesterDurationEnumId;
            }
            set
            {
                SemesterDurationEnumId = (int)value;
            }
        }

        public static IList<EnumUtil.EnumInfo> SemesterDurationEnumDropDownList
        {
            get
            {
                return EnumUtil.GetEnumList(typeof(SemesterDurationEnum));
            }
        }

        [DataMember]
        public EnumCollection.Common.MonthEnum StartingMonth
        {
            get
            {
                return (EnumCollection.Common.MonthEnum) StartingMonthEnumId;
            }
            set
            {
                StartingMonthEnumId = (int)value;
            }
        }

        
        public EnabledRegistrationTypeEnum EnabledRegistrationType
        {
            get
            {
                //var today = DateTime.Now;
                ////this.PreRegistrationStartDate
                //if (IsTpeEnable)
                //{
                //}
                return (EnabledRegistrationTypeEnum)EnabledRegistrationTypeEnumId;
            }
            set
            {
                EnabledRegistrationTypeEnumId = (Byte)value;
            }
        }

        #endregion Enum Property

        private static void GetNewExtra(Aca_Semester obj)
        {
            obj.TermId = 1;
            obj.StartDate = DateTime.Now;
            obj.EndDate = DateTime.Now.AddMonths(6);
            obj.Year = (short)DateTime.Now.Year;
            obj.AcademicSession = DateTime.Now.AddYears(-1).Year + "-" + DateTime.Now.Year;
        }

        public class DropdownJson
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public byte StatusEnumId { get; set; }
            public string Status { get; set; }
            public int SemesterDurationEnumId { get; set; }
        }
        /// <summary>
        /// to get all semester list pass true as the 2nd parameter.
        /// </summary>
        /// <param name="dbInstance"></param>
        /// <param name="isGetAll"></param>
        /// <returns></returns>
        public static List<DropdownJson> GetDropdownList(EmsDbContext dbInstance , bool isGetAll = false)
        {
            IQueryable<Aca_Semester> query = dbInstance.Aca_Semester
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id);

            if (!isGetAll)
            {
                query = query.Where(x => x.IsEnable);
            }

            var semesterList =  query.AsEnumerable().Select(t => new DropdownJson
            { Id = t.Id.ToString(), Name = t.Name,StatusEnumId = t.StatusEnumId,Status = t.Status.ToString() ,SemesterDurationEnumId =  t.SemesterDurationEnumId}).ToList();

            return semesterList;
        }

    }
}
