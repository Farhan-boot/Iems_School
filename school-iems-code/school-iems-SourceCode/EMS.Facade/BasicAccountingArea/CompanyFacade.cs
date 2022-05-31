using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;

namespace EMS.Facade.BasicAccountingArea
{
    public class CompanyFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;

        public CompanyFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region Common Function

        /// <summary>
        /// return current company id or null
        /// </summary>
        /// <returns></returns>
        public int? GetCurrentCompanyId()
        {
            var currentCompany = DbInstance.BAcnt_CompanyAccount.SingleOrDefault(x => x.IsCurrent);
            return currentCompany?.Id;
        }
        #endregion

    }
}
