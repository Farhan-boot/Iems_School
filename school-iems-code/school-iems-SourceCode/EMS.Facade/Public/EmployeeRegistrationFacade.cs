using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;

namespace EMS.Facade.Public
{
    public class EmployeeRegistrationFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;
        private User_AccountDataService accountService = null;
        private User_ContactAddressDataService contactAddressService = null;
        private User_ContactNumberDataService contactNumberService = null;
        private User_ContactEmailDataService contactEmailService = null;
        private User_EmployeeDataService employeeService = null;
        private HR_AppointmentHistoryDataService appointmentHistoryService = null;
        public EmployeeRegistrationFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
            accountService = new User_AccountDataService(emsDbContext, usersProfile);
            contactAddressService = new User_ContactAddressDataService(emsDbContext, usersProfile);
            contactNumberService = new User_ContactNumberDataService(emsDbContext, usersProfile);
            contactEmailService = new User_ContactEmailDataService(emsDbContext, usersProfile);
            employeeService = new User_EmployeeDataService(emsDbContext, usersProfile);
            appointmentHistoryService = new HR_AppointmentHistoryDataService(emsDbContext, usersProfile);
        }

        public bool SaveAccount(User_Account obj, out string error, DbContextTransaction scope)
        {
            try
            {
                if (!accountService.Save(obj, out error, scope))
                {
                    scope.Rollback();
                    return false;
                }
                DbInstance.SaveChanges();
                if (obj.User_ContactAddress.Count != 0)
                {
                    foreach (var userContactAddress in obj.User_ContactAddress)
                    {
                        if (!contactAddressService.Save(userContactAddress, out error, scope))
                        {
                            scope.Rollback();
                            return false;
                        }
                        DbInstance.SaveChanges();
                    }
                }
                if (obj.User_ContactNumber.Count != 0)
                {
                    foreach (var userContactNumber in obj.User_ContactNumber)
                    {
                        if (userContactNumber.ContactNumber.IsValid())
                        {
                            if (!contactNumberService.Save(userContactNumber, out error, scope))
                            {
                                scope.Rollback();
                                return false;
                            }
                            DbInstance.SaveChanges();
                        }
                    }
                }
                if (obj.User_ContactEmail.Count != 0)
                {
                    foreach (var userContactEmail in obj.User_ContactEmail)
                    {
                        if (userContactEmail.ContactEmail.IsValid())
                        {
                            if (!contactEmailService.Save(userContactEmail, out error, scope))
                            {
                                scope.Rollback();
                                return false;
                            }
                            DbInstance.SaveChanges();
                        }
                    }
                }
                //scope.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //log error
                error = ex.ToString();
                scope.Rollback();
                return false;
            }
        }
        public bool SaveEmployee(User_Employee obj, out string error, DbContextTransaction scope)
        {
            try
            {
                if (!employeeService.Save(obj, out error, scope))
                {
                    scope.Rollback();
                    return false;
                }
                DbInstance.SaveChanges();
                if (obj.HR_AppointmentHistory.Count != 0)
                {
                    foreach (var hrAppointmentHistory in obj.HR_AppointmentHistory)
                    {
                        if (!appointmentHistoryService.Save(hrAppointmentHistory, out error, scope))
                        {
                            scope.Rollback();
                            return false;
                        }
                        DbInstance.SaveChanges();
                    }
                }
                //scope.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //log error
                error = ex.ToString();
                scope.Rollback();
                return false;
            }
        }
    }
}
