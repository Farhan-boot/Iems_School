//File: DataService for Aca_MarkDistributionPolicyBreakdown
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using EMS.CoreLibrary.Helpers;
using EMS.Framework.Objects;
using EMS.Framework.Utils;
using EMS.DataAccess.Data;
using EMS.DataService.Bases;

namespace EMS.DataService
{
    public class Aca_MarkDistributionPolicyBreakdownDataService : Aca_MarkDistributionPolicyBreakdownDataServiceBase
    {
        public Aca_MarkDistributionPolicyBreakdownDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidMarkDistributionPolicyBreakdownObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(Aca_MarkDistributionPolicyBreakdown newObj, out string error)
        {
            error = "";
            //comment out this line if you need custom validation for fields.
            if (!base.IsValidToSave(newObj, out error))
            {
                return false;
            }
            //ToDO write your custom field validation here.
            return true;

        }
        /// <summary>
        /// Must implement permission for save MarkDistributionPolicyBreakdown.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.AcademicModule.MarkDistributionPolicyBreakdown.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get MarkDistributionPolicyBreakdown
        /// <summary>
        /// TODO need to implement.
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="count"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<Aca_MarkDistributionPolicyBreakdown> GetPagedList(string searchKey, int? pageSize, int? pageNo, ref int count, out string error)
        {
            throw new NotImplementedException();
            IQueryable<Aca_MarkDistributionPolicyBreakdown> query = (from q in DbInstance.Aca_MarkDistributionPolicyBreakdown select q)
                                            //.Include("Aca_MarkDistributionPolicyBreakdown")
                                            //.OrderBy(x => x.FullName)
                                            ;
            //if (searchKey.IsValid() )
            //{
            //    query = from q in query
            //            where q.FullName.Contains(searchKey)
            //            select q;
            //}
            error = "";
            return base.GetPagedList(query, pageSize, pageNo, ref count, out error);
        }
        #endregion

        #region Custom Save MarkDistributionPolicyBreakdown
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(Aca_MarkDistributionPolicyBreakdown objToSave, bool isNewObject, out string error)
        {
            error = "";
            //TODO write your custom validation here.
            return true;
        }
        /// <summary>
        /// an example to implement custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="transactionScope"></param>
        /// <param name="error"></param>
        /// <param name="columnsToCopy"></param>
        /// <returns></returns>
        private bool SaveExtraExample(Aca_MarkDistributionPolicyBreakdown newObj, Aca_MarkDistributionPolicyBreakdown objToSave, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //commenting base
            // base.SaveExtra(newObj, objToSave, isNewObject, out error);
            //ToDO write your custom business here
            CopyUtil.CopySelectedColumns(newObj, objToSave, columnsToCopy);
            //objToSave.Column = newObj.Column;
            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        public void SaveCustomExample(Aca_MarkDistributionPolicyBreakdown newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, Aca_MarkDistributionPolicyBreakdown.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, Aca_MarkDistributionPolicyBreakdown.Property_ABC);//customizing default save method using delegate.
            Save(newObj, SaveExtraExample, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}

