using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Security.Cryptography.X509Certificates;
using EMS.DataAccess.Data;
using EMS.Facade.Academic;
using EMS.Framework.Objects;
using System.Linq;
using EMS.CoreLibrary.Helpers;

namespace EMS.Facade.BasicAccountingArea
{
    public class BasicAccountingFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;
        public BasicAccountingFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
        }

        public CompanyFacade CompanyFacade
        {
            get
            {
                return new CompanyFacade(_emsDbContext, _profile);
            }
        }
        public LedgerFacade LedgerFacade
        {
            get
            {
                return new LedgerFacade(_emsDbContext, _profile);
            }
        }

        public LedgerTransactionFacade LedgerTransactionFacade
        {
            get
            {
                return new LedgerTransactionFacade(_emsDbContext, _profile);
            }
        }

    }
}
