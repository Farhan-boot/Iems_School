using System;
using System.Linq;
using System.Web.Mvc;
using EMS.DataAccess;
using EMS.DataAccess.Data;
using EMS.Facade;
using EMS.Framework.Utils;
using EMS.ReportFacade;
namespace EMS.Web.Framework.Bases
{
   public class BaseController:Controller
    {
        protected EmsDbContext DbInstance { get; }
        public BaseController()
        {
            DbInstance= HttpUtil.DbContext;
          
        }
        
        protected TheFacade Facade { get { return new TheFacade(DbInstance, HttpUtil.Profile); } }

        protected TheReoprtFacade ReoprtFacade { get { return new TheReoprtFacade(DbInstance, HttpUtil.Profile); } }

    }
}

