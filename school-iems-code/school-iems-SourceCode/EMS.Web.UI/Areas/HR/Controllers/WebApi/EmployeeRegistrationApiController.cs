using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Framework.Objects;
using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.HR.Controllers.WebApi
{
    public class EmployeeRegistrationApiController : ApiController
    {
        EmsDbContext DbInstance = HttpUtil.DbContext;
        // GET: EmployeeRegistrationApi
        public HttpResult<EmployeeJson> GetEmployee()
        {
            var result = new HttpResult<EmployeeJson>();
            var json = new EmployeeJson();
            //UserDetails
            User_Account userAccount = User_Account.GetNew(BigInt.NewBigInt());
            userAccount.Map(json.Account);
            //Employee Details
            User_Employee userEmployee = User_Employee.GetNew(BigInt.NewBigInt());
            userEmployee.Map(json.Employee);
            //Image
            User_Image userImage = User_Image.GetNew(BigInt.NewBigInt());
            userImage.Map(json.Image);
            //Address
            List<User_ContactAddress> contactAddresses = new List<User_ContactAddress>();
            User_ContactAddress addressPresent = User_ContactAddress.GetNew(BigInt.NewBigInt());
            addressPresent.ContactAddressTypeEnumId = (byte)User_ContactAddress.ContactAddressTypeEnum.Present;
            contactAddresses.Add(addressPresent);
            User_ContactAddress addressPermanent = User_ContactAddress.GetNew(BigInt.NewBigInt());
            addressPermanent.ContactAddressTypeEnumId = (byte)User_ContactAddress.ContactAddressTypeEnum.Permanent;
            contactAddresses.Add(addressPermanent);
            contactAddresses.Map(json.ContactAddressList);
            //Email
            List<User_ContactEmail> contactEmails = new List<User_ContactEmail>();
            User_ContactEmail emailHome = User_ContactEmail.GetNew(BigInt.NewBigInt());
            emailHome.ContactEmailTypeEnumId = (byte)User_ContactEmail.ContactEmailTypeEnum.Home;
            contactEmails.Add(emailHome);
            User_ContactEmail emailWork = User_ContactEmail.GetNew(BigInt.NewBigInt());
            emailWork.ContactEmailTypeEnumId = (byte)User_ContactEmail.ContactEmailTypeEnum.Office;
            contactEmails.Add(emailWork);
            contactEmails.Map(json.ContactEmailList);
            //Mobile
            List<User_ContactNumber> contactNumbers = new List<User_ContactNumber>();
            User_ContactNumber numberMobile = User_ContactNumber.GetNew(BigInt.NewBigInt());
            numberMobile.ContactNumberTypeEnumId = (byte)User_ContactNumber.ContactNumberTypeEnum.Mobile;
            contactNumbers.Add(numberMobile);
            User_ContactNumber numberOfficeMobile = User_ContactNumber.GetNew(BigInt.NewBigInt());
            numberOfficeMobile.ContactNumberTypeEnumId = (byte)User_ContactNumber.ContactNumberTypeEnum.OfficeMobile;
            contactNumbers.Add(numberOfficeMobile);
            User_ContactNumber numberIntercom = User_ContactNumber.GetNew(BigInt.NewBigInt());
            numberIntercom.ContactNumberTypeEnumId = (byte)User_ContactNumber.ContactNumberTypeEnum.OfficeIntercom;
            contactNumbers.Add(numberIntercom);
            User_ContactNumber numberIntercomArmy = User_ContactNumber.GetNew(BigInt.NewBigInt());
            numberIntercomArmy.ContactNumberTypeEnumId = (byte)User_ContactNumber.ContactNumberTypeEnum.OfficeIntercomArmy;
            contactNumbers.Add(numberIntercomArmy);
            User_ContactNumber numberHomePhone = User_ContactNumber.GetNew(BigInt.NewBigInt());
            numberHomePhone.ContactNumberTypeEnumId = (byte)User_ContactNumber.ContactNumberTypeEnum.HomePhone;
            contactNumbers.Add(numberHomePhone);
            User_ContactNumber numberOfficePhone = User_ContactNumber.GetNew(BigInt.NewBigInt());
            numberOfficePhone.ContactNumberTypeEnumId = (byte)User_ContactNumber.ContactNumberTypeEnum.OfficePhone;
            contactNumbers.Add(numberOfficePhone);
            contactNumbers.Map(json.ContactNumberList);
            //Education
            List<User_Education> educations = new List<User_Education>();
            User_Education diplomaEquivalent = User_Education.GetNew(BigInt.NewBigInt());
            diplomaEquivalent.EducationTypeEnumId = (byte)User_Education.DegreeEquivalentEnum.DiplomaEquivalent;
            educations.Add(diplomaEquivalent);
            User_Education sscEquivalent = User_Education.GetNew(BigInt.NewBigInt());
            sscEquivalent.EducationTypeEnumId = (byte)User_Education.DegreeEquivalentEnum.SscEquivalent;
            educations.Add(sscEquivalent);
            User_Education hscEquivalent = User_Education.GetNew(BigInt.NewBigInt());
            hscEquivalent.EducationTypeEnumId = (byte)User_Education.DegreeEquivalentEnum.HscEquivalent;
            educations.Add(hscEquivalent);
            User_Education bachelorEquivalent = User_Education.GetNew(BigInt.NewBigInt());
            bachelorEquivalent.EducationTypeEnumId = (byte)User_Education.DegreeEquivalentEnum.BachelorEquivalent;
            educations.Add(bachelorEquivalent);
            User_Education masterEquivalent = User_Education.GetNew(BigInt.NewBigInt());
            masterEquivalent.EducationTypeEnumId = (byte)User_Education.DegreeEquivalentEnum.MasterEquivalent;
            educations.Add(masterEquivalent);
            User_Education doctoralEquivalent = User_Education.GetNew(BigInt.NewBigInt());
            doctoralEquivalent.EducationTypeEnumId = (byte)User_Education.DegreeEquivalentEnum.DoctoralEquivalent;
            educations.Add(doctoralEquivalent);
            educations.Map(json.EducationList);
            //Appointments
            List<HR_Appointment> appointments = new List<HR_Appointment>();
            HR_Appointment appointment = HR_Appointment.GetNew(BigInt.NewBigInt());
            appointments.Add(appointment);

            result.Data = json;
            return result;
        }
        public HttpResult<User_EmployeeJson> GetEmployeeDataExtra()
        {
            var result = new HttpResult<User_EmployeeJson>();
            result.DataExtra.EmployeeCategoryEnumList = EnumUtil.GetEnumList(typeof(User_Employee.EmployeeCategoryEnum));
            result.DataExtra.EmployeeTypeEnumList = EnumUtil.GetEnumList(typeof(User_Employee.EmployeeTypeEnum));
            result.DataExtra.ServiceStatusEnumList = EnumUtil.GetEnumList(typeof(User_Employee.ServiceStatusEnum));
            result.DataExtra.ServiceTypeEnumList = EnumUtil.GetEnumList(typeof(User_Employee.ServiceTypeEnum));
            result.DataExtra.UserStatusEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserStatusEnum));
            result.DataExtra.BloodGroupEnumList = EnumUtil.GetEnumList(typeof(User_Account.BloodGroupEnum));
            result.DataExtra.GenderEnumList = EnumUtil.GetEnumList(typeof(User_Account.GenderEnum));
            result.DataExtra.MaritalStatusEnumList = EnumUtil.GetEnumList(typeof(User_Account.MaritalStatusEnum));
            result.DataExtra.ReligionEnumList = EnumUtil.GetEnumList(typeof(User_Account.ReligionEnum));
            
            var depts = DbInstance.HR_Department.ToList();
            result.DataExtra.DepartmentList = depts.AsEnumerable()
                .Where(x => x.TypeEnumId == 2)
                .Select(t => new
                {
                    Id = t.Id.ToString(),
                    Name = t.ShortName
                });
            return result;
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResult<EmployeeJson> SaveEmployee(EmployeeJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<EmployeeJson>();
            var entity = new User_Employee();
            //json.Map(entity);
            //if (Facade.UserFacade.SaveClass(entity, out error))
            //{
            //    entity.Map(json);
            //    result.Data = json;
            //}
            //else
            //{
            //    result.HasError = true;
            //    result.Errors.Add(error);
            //}
            return result;
        }
    }
}