//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Diagnostics;
using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.CoreLibrary.Helpers;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;


namespace EMS.Web.UI.Areas.BasicAccounting.Controllers.WebApi
{

    public class LedgerApiController : EmployeeBaseApiController
	{
        public LedgerApiController()
        {  }
       
        #region Ledger 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<BAcnt_LedgerJson> GetPagedLedger(string textkey, int? pageSize, int? pageNo
           ,Int32 branchId= 0      
           ,Int32 companyId= 0      
         )
        {
            //pageSize = 2500;
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<BAcnt_LedgerJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (BAcnt_LedgerDataService ledgerDataService = new BAcnt_LedgerDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<BAcnt_Ledger> query = DbInstance.BAcnt_Ledger.OrderBy(x => x.Code);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (branchId>0)
                    {
                        query = query.Where(q => q.BranchId== branchId);
                    }  
                    if (companyId>0)
                    {
                        query = query.Where(q => q.CompanyId== companyId);
                    }  
                 
                    var entities = ledgerDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<BAcnt_LedgerJson>();
                    entities.Map(jsons);
                    result.Data = jsons;
                    result.Count = count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        /// <summary>
        /// Caution: Very Dangerous to call When table have huge data!
        /// </summary>
        /// <returns></returns>
        public HttpListResult<BAcnt_LedgerJson> GetAllLedger(int branchId= 0)
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_LedgerJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            /*if (branchId==0)
            {
                result.HasError = true;
                result.Errors.Add("Please select valid branch.");
                return result;
            }*/

            try
            {

                var currentCompanyId = Facade.BasicAccountingFacade.CompanyFacade.GetCurrentCompanyId();

                if (currentCompanyId==null)
                {
                    result.HasError = true;
                    result.Errors.Add("Valid company not found!");
                    return result;
                }
                // Valid Branch Checking
                if (branchId>0)
                {
                    var branch = DbInstance.BAcnt_Branch
                        .SingleOrDefault(x => x.Id == branchId
                                              && x.CompanyId == currentCompanyId
                                              && x.IsEnable && !x.IsDeleted);
                    if (branch == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("Invalid branch!");
                        return result;
                    }
                }
                // Get Ledger List
                var ledgerList = DbInstance.BAcnt_Ledger
                    .Where(x=>x.CompanyId== currentCompanyId
                              && !x.IsDeleted
                              ).ToList();

                /*
                 * Todo
                 * if ledger List not found in database then, create first parent Ledger Group
                 */

                //Call Get Ledger Tree
                var treeList = Facade.BasicAccountingFacade.LedgerFacade.GetLedgerTreeList(out error,ledgerList,branchId);
                if (treeList==null)
                {
                    result.HasError = true;
                    result.Errors.Add($"{error}");
                    return result;
                }
                var ledgerJsonList = new List<BAcnt_LedgerJson>();
                treeList.Map(ledgerJsonList);
                result.Data = ledgerJsonList;
                result.Count = ledgerJsonList.Count;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult<BAcnt_LedgerJson> GetLedgerById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_LedgerJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_LedgerDataService ledgerDataService = new BAcnt_LedgerDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new BAcnt_LedgerJson();
                    BAcnt_Ledger entity;
                    if (id <= 0)
                    {
                        entity = BAcnt_Ledger.GetNew();
                    }
                    else
                    {
                        entity = ledgerDataService.GetById(id);
                    }
                    entity.Map(json);
                    result.Data = json;

                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        private HttpResult<BAcnt_LedgerJson> GetLedgerByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_LedgerJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_LedgerDataService ledgerDataService = new BAcnt_LedgerDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new BAcnt_LedgerJson();
                    BAcnt_Ledger entity;
                    if (id <= 0)
                    {
                        entity = BAcnt_Ledger.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("BAcnt_Ledger");
                        //includeTables.Add("BAcnt_Ledger");

                        entity = ledgerDataService.GetById(id, includeTables.ToArray());
                    }
                    entity.Map(json);
                    result.Data = json;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<BAcnt_LedgerJson> GetLedgerListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_LedgerJson>();
            try
            {
                //BAcnt_LedgerDataService ledgerDataService =
                //    new BAcnt_LedgerDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(BAcnt_Ledger.TypeEnum));
                //DropDown Option Tables

                var currentCompanyId = Facade.BasicAccountingFacade.CompanyFacade.GetCurrentCompanyId();

                var branchList= DbInstance.BAcnt_Branch
                    .Where(x => x.CompanyId == currentCompanyId && x.IsEnable && !x.IsDeleted)
                    .AsEnumerable().Select(t => new
                        { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.BranchList = branchList;
                result.DataExtra.SelectedBranchId = branchList[0].Id;


                //result.DataExtra.CompanyAccountList = DbInstance.BAcnt_CompanyAccount.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();

                // Empty Ledger Json, for UI use
                var emptyLedgerEntity =  BAcnt_Ledger.GetNew();
                var emptyLedgerJson = new BAcnt_LedgerJson();
                emptyLedgerEntity.Map(emptyLedgerJson);
                result.DataExtra.EmptyLedgerJson = emptyLedgerJson;


            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<BAcnt_LedgerJson> GetLedgerDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_LedgerJson>();
            try
            {
                //BAcnt_LedgerDataService ledgerDataService =
                //    new BAcnt_LedgerDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(BAcnt_Ledger.TypeEnum));
                //DropDown Option Tables

                result.DataExtra.BranchList = DbInstance.BAcnt_Branch.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

                //result.DataExtra.CompanyAccountList = DbInstance.BAcnt_CompanyAccount.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        #endregion

        #region Save/Delete
        [HttpPost]
        public HttpResult<BAcnt_LedgerJson> SaveLedger(BAcnt_LedgerJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_LedgerJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new BAcnt_Ledger();
                 var dbAttachedEntity = new BAcnt_Ledger();
                json.Map(entityReceived);

                var branch = DbInstance.BAcnt_Branch.Find(entityReceived.BranchId);
                if (branch==null)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid branch!");
                    return result;
                }

                // set Company Id
                entityReceived.CompanyId = branch.CompanyId;

                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveLedgerLogic(entityReceived, ref dbAttachedEntity, out error))
                    {
                        DbInstance.SaveChanges();
                        scope.Commit();

                        dbAttachedEntity.Map(json);
                        result.Data = json; //return object
                    }
                    else
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                    }
                }
            }

            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        
        /*[HttpPost]
        private HttpResult<BAcnt_LedgerJson> SaveLedger2(BAcnt_LedgerJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_LedgerJson>();
            try
            {
                using (BAcnt_LedgerDataService ledgerDataService = new BAcnt_LedgerDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new BAcnt_Ledger();
                    var dbAttachedEntity = new BAcnt_Ledger();
                    json.Map(entityReceived);
                    if (ledgerDataService.Save(entityReceived, ref dbAttachedEntity, out error))
                    {
                        dbAttachedEntity.Map(json);
                        result.Data = json;//return object
                    }
                    else
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }*/
        /// <summary>
        /// Todo pending to implement bulk save
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [HttpPost]
        private HttpListResult<BAcnt_LedgerJson> SaveLedgerList(IList<BAcnt_LedgerJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_LedgerJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_LedgerDataService ledgerDataService = new BAcnt_LedgerDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<BAcnt_Ledger>();
                    var dbAttachedListEntity = new List<BAcnt_Ledger>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (ledgerDataService.Save(entity, ref dbAttachedListEntity, out error))
                    //{
                    //    dbAttachedListEntity.Map(jsonList);
                    //    result.Data = jsonList;//return object
                    //}
                    //else
                    //{
                    //    result.HasError = true;
                    //    result.Errors.Add(error);
                    //}
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpResult<BAcnt_LedgerJson> GetDeleteLedgerById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_LedgerJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanDelete, out error)
            && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanDeleteGroup, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {

                var ledger = DbInstance.BAcnt_Ledger.Find(id);

                // start Check only for group
                if (ledger != null && ledger.IsGroup)
                {
                    if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanDeleteGroup, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add("you are not permitted to do delete group.");
                        return result;
                    }

                    var hasChild = DbInstance.BAcnt_Ledger.Any(x => x.ParentId == id);
                    if (hasChild)
                    {
                        result.HasError = true;
                        result.Errors.Add("This data has child(s), first delete those!");
                        return result;
                    }
                } // end Check only for group


                using (BAcnt_LedgerDataService ledgerDataService = new BAcnt_LedgerDataService(DbInstance, HttpUtil.Profile))
                {
                    ledgerDataService.Delete(ledger, out error);
                    if (!ledgerDataService.DeleteById(id, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        #endregion

        #region Local Method (should be always private)
        private bool IsValidToSaveLedger(BAcnt_Ledger newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>255)
            {
                error += "Name exceeded its maximum length 255.";
                return false;
            }
            if (newObj.Code.IsValid() && newObj.Code.Length>50)
            {
                error += "Code exceeded its maximum length 50.";
                return false;
            }
            if (newObj.CodeName.IsValid() && newObj.CodeName.Length>50)
            {
                error += "Internal Code exceeded its maximum length 50.";
                return false;
            }
            if (newObj.TypeEnumId==null)
            {
                error += "Please select valid Type.";
                return false;
            }

            if (newObj.IsGroup==null)
            {
                error += "Is Group required.";
                return false;
            }

            if (newObj.OpeningDate!=null && !newObj.OpeningDate.IsValid())
            {
                newObj.OpeningDate=null;//just put null,if its nullable and not valid.
               //error += "Opening Date is not valid.";
               //return false;
            }


            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            if (newObj.BranchId<=0)
            {
                error += "Please select valid Branch.";
                return false;
            }
            if (newObj.CompanyId<=0)
            {
                error += "Please select valid Company.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.BAcnt_Ledger.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Ledger already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveLedgerLogic(BAcnt_Ledger newObj,ref BAcnt_Ledger dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Ledger to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveLedger(newObj, out error))
                return false;

            bool isNewObject = true;
            BAcnt_Ledger objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.BAcnt_Ledger.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = BAcnt_Ledger.GetNew(newObj.Id);
                DbInstance.BAcnt_Ledger.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }

            // Add Permission Check
            if (isNewObject)
            {
                if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanAdd,
                    out error))
                {
                    return false;
                }
                if (newObj.IsGroup && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanChangeGroupStatus,
                        out error))
                {
                    error = "you are not permitted to make a group.";
                    return false;
                }
               
            }
            //Edit Permission Check 
            else if (!isNewObject)
            {
                if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanEdit,
                    out error))
                {
                    return false;
                }

                if (objToSave.IsGroup!= newObj.IsGroup
                    && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanChangeGroupStatus,
                        out error))
                {
                    error = "you are not permitted to do change group status.";
                    return false;
                }
            }
            

            // Check has any child, then can't be change IsGroup
            if (!isNewObject && objToSave.IsGroup && !newObj.IsGroup)
            {
                var hasChild = DbInstance.BAcnt_Ledger.Any(x => x.ParentId == newObj.Id);
                if (hasChild)
                {
                    error = "this group has child(s), first delete those.";
                    return false;
                }


            }

            //Maximum group create in each ledger type
            int noOfParent = GetNumberOfParent(newObj.ParentId);

            if (newObj.IsGroup && noOfParent>3)
            {
                error = "Maximum 3 parent create for group.";
                return false;
            }

            if (!newObj.IsGroup && noOfParent < 4)
            {
                error = "Minimum 4 parents group create then create ledger.";
                return false;
            }

            // Group or ledger code range
            if (newObj.IsGroup && !IsValidLedgerGroupCodeRange(newObj,out error))
            {
                return false;
            }



            //binding object  
            objToSave.Name =  newObj.Name ;
            if (newObj.IsGroup)
            {
                objToSave.Code = newObj.Code;
                objToSave.CodeName = "";
            }
            else
            {
                objToSave.Code = "";
                objToSave.CodeName = newObj.CodeName;
            }
          
           

            //Set Code Name
           if (!newObj.CodeName.IsValid())
            {
                objToSave.CodeName = newObj.Type.ToString().ToTitleCase();
            }
          
            objToSave.TypeEnumId =  newObj.TypeEnumId ;
            objToSave.ParentId =  newObj.ParentId ;
            objToSave.IsGroup =  newObj.IsGroup ;
            objToSave.IsBank = newObj.IsBank;
            objToSave.OpeningBalance =  newObj.OpeningBalance ;
            objToSave.OpeningDate =  newObj.OpeningDate ;
            objToSave.Remark =  newObj.Remark ;
            objToSave.History =  newObj.History ;
            objToSave.IsDeleted =  newObj.IsDeleted ;
            objToSave.BranchId =  newObj.BranchId ;
            objToSave.CompanyId =  newObj.CompanyId ;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
           dbAddedObj = objToSave;
           
            //here save any child table
            return true;
        }

        private int GetNumberOfParent(int? parentId,int numberOfParent=0)
        {
            numberOfParent++;
            var ledger = DbInstance.BAcnt_Ledger.SingleOrDefault(x => x.Id == parentId);
            if (ledger==null)
            {
                return numberOfParent;
            }

            if (ledger.ParentId==null)
            {
                return numberOfParent;
            }

            parentId = ledger.ParentId;
            
            numberOfParent= GetNumberOfParent(parentId, numberOfParent);

            return numberOfParent;
        }

        private bool IsValidLedgerGroupCodeRange(BAcnt_Ledger ledger,out string error)
        {
            error=String.Empty;
            long code;
            if (!long.TryParse(ledger.Code, out code))
            {
                error = "Invalid Code";
                return false;
            }

            //Asset
            if (ledger.TypeEnumId==(byte)BAcnt_Ledger.TypeEnum.Asset && !(code>= 100000 && code< 200000))
            {
                error = "Invalid Cod, 'Asset' type ledger/group code range 100000 to 199999";
                return false;
            }
            //Liability
            if (ledger.TypeEnumId == (byte)BAcnt_Ledger.TypeEnum.Liability && !(code >= 200000 && code < 300000))
            {
                error = "Invalid Cod, 'Liability' type ledger/group code range 200000 to 299999";
                return false;
            }
            //Income
            if (ledger.TypeEnumId == (byte)BAcnt_Ledger.TypeEnum.Income && !(code >= 300000 && code < 400000))
            {
                error = "Invalid Cod, 'Income' type ledger/group code range 300000 to 399999";
                return false;
            }
            //Expense
            if (ledger.TypeEnumId == (byte)BAcnt_Ledger.TypeEnum.Expense && !(code >= 400000 && code < 500000))
            {
                error = "Invalid Cod, 'Expense' type ledger/group code range 400000 to 499999";
                return false;
            }
            //Equity
            if (ledger.TypeEnumId == (byte)BAcnt_Ledger.TypeEnum.Equity && !(code >= 500000 && code < 600000))
            {
                error = "Invalid Cod, 'Equity' type ledger/group code range 500000 to 599999";
                return false;
            }

            return true;
        }
        #endregion
        #endregion

	}
}
