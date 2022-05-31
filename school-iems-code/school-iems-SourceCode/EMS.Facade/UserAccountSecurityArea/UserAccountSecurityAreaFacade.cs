using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;

namespace EMS.Facade.UserAccountSecurityArea
{
    public class UserAccountSecurityAreaFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;
        public UserAccountSecurityAreaFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
        }

        public VerificationAuditFacade VerificationAuditFacade
        {
            get
            {
                return new VerificationAuditFacade(_emsDbContext, _profile);
            }
        }

    }
}
