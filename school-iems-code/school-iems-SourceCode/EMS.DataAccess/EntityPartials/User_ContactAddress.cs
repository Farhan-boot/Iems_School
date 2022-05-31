using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class User_ContactAddress
    {
        #region Enum Property
        public enum ContactAddressTypeEnum
        {
            Permanent = 0,
            Present = 1,
            Work = 2// remove
        }
        //public enum PrivacyEnum
        //{
        //    Public = 0,
        //    StudentParentEmployee = 1,
        //    EmployeeOnly = 2,
        //    HrOnly = 3,
        //    Custom = 4
        //}
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public ContactAddressTypeEnum ContactAddressType
        {
            get
            {
                return (ContactAddressTypeEnum)ContactAddressTypeEnumId;
            }
            set
            {
                ContactAddressTypeEnumId = (Byte)value;
            }
        }
       
        #endregion Enum Property

        public override string ToString()
        {
            //todo need to recode
            var fullAddress = "";//$"{ContactAddressType.ToString().AddSpacesToSentence()}:";
            if (Address1.IsValid())
            {
                fullAddress += $"{Address1};";
            }
            if (Address2.IsValid())
            {
                fullAddress += $" {Address2};";
            }
            //if (RoadNo.IsValid())
            //{
            //    fullAddress += $"R:{RoadNo};";
            //}
            //if (AreaInfo.IsValid())
            //{
            //    fullAddress += $"{AreaInfo};";
            //}
            if (PostOffice.IsValid())
            {
                fullAddress += $" {PostOffice};";
            }
            if (PoliceStation.IsValid())
            {
                fullAddress += $" {PoliceStation};";
            }
            if (District.IsValid())
            {
                fullAddress += $"{District};";
            }
            //if (PostCode>0)
            //{
            //    fullAddress += $"-{PostCode};";
            //}
            if (General_Country!=null)
            {
                fullAddress += $"{General_Country.Name}.";
            }
            else if(CountryId==1)
            {
                fullAddress += $"Bangladesh.";
            }

            return fullAddress;
        }
        public static List<User_ContactAddress> ValidedPresentPermanentAddress(int userId, List<User_ContactAddress> contatAddressList)
        {
            if (contatAddressList == null)
            {
                contatAddressList = new List<User_ContactAddress>();
            }
            if (contatAddressList.All(x => x.ContactAddressType != User_ContactAddress.ContactAddressTypeEnum.Present))//if no present address
            {
                User_ContactAddress newContactAddressPresent = User_ContactAddress.GetNew();
                newContactAddressPresent.UserId = userId;
                newContactAddressPresent.ContactAddressTypeEnumId = (byte)User_ContactAddress.ContactAddressTypeEnum.Present;
                contatAddressList.Add(newContactAddressPresent);
            }
            if (contatAddressList.All(x => x.ContactAddressType != User_ContactAddress.ContactAddressTypeEnum.Permanent))//if no permanent address
            {
                User_ContactAddress newContactAddressPermanent = User_ContactAddress.GetNew();
                newContactAddressPermanent.UserId = userId;
                newContactAddressPermanent.ContactAddressTypeEnumId = (byte)User_ContactAddress.ContactAddressTypeEnum.Permanent;
                contatAddressList.Add(newContactAddressPermanent);
            }
            return contatAddressList;
        }
        private static void GetNewExtra(User_ContactAddress obj)
        {
            obj.ContactAddressTypeEnumId = (byte)ContactAddressTypeEnum.Present;
            
            obj.CountryId = 1;
        }

     
    }
}
