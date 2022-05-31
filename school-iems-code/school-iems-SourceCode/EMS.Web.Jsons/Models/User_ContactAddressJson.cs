using System;
using System.Collections.Generic;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
	public partial class User_ContactAddressJson
	{
        public string FullPresentAddress { get; set; }   //employee->studentList
        public string FullPermanentAddress { get; set; }   //employee->studentList
    }

    public static partial class EntityMapper
    {
        private static void MapExtra(User_ContactAddress entity, User_ContactAddressJson toJson)
        {
           
            if (entity.ContactAddressType == User_ContactAddress.ContactAddressTypeEnum.Present)
                toJson.FullPresentAddress = entity.ToString();

          
            if (entity.ContactAddressType == User_ContactAddress.ContactAddressTypeEnum.Permanent)
                toJson.FullPermanentAddress = entity.ToString();
        }

        private static void MapExtra(User_ContactAddressJson json, User_ContactAddress toEntity)
        {


        }
    }
}
