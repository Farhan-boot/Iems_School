using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EMS.DataAccess.Data;
using EMS.Facade.AcademicArea;
using EMS.Framework.Objects;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Jsons.Models;
using MvcSiteMapProvider.Linq;

namespace EMS.Facade.AdmissionArea
{
    public class StudentAdmissionFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;
        public StudentAdmissionFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
        }
      
      

        

        

    }
}
