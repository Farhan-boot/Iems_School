using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Utils;

namespace EMS.Facade
{
    public class UserFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;
        private User_AccountDataService accountService = null;
        private User_EmployeeDataService employeeService = null;
        public UserFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
            accountService = new User_AccountDataService(emsDbContext, usersProfile);
            employeeService = new User_EmployeeDataService(emsDbContext, usersProfile);
        }

        #region User
        public ICollection<User_Account> GetUserList()
        {
            return HttpUtil.DbContext.User_Account.ToList();
        }
        public bool SaveAccount(User_Account obj, out string error)
        {
            var res = accountService.Save(obj, out error);
            return res;
        }
        public bool SaveEmployee(User_Employee obj, out string error)
        {
            var res = employeeService.Save(obj, out error);
            return res;
        }

        #endregion User

        #region UserRole
        public List<UAC_Role> GetAllUserRoleList()
        {
            return DbInstance.UAC_Role.ToList();
        }

        public UAC_Role GetUserRoleById(long id)
        {
            return DbInstance.UAC_Role.Find(id);
        }
        #endregion UserRole
    }
}
