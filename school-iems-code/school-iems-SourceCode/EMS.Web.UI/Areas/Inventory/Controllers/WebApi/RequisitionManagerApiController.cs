using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Facade.AcademicArea;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;
using System.Data.Entity.Core.Objects;
using MvcSiteMapProvider.Linq;

namespace EMS.Web.UI.Areas.Inventory.Controllers.WebApi
{
    public class RequisitionManagerApiController : EmployeeBaseApiController
    {
        
        public HttpResult<RequisitionCustomJson> GetRequisitionById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<RequisitionCustomJson>();
            //todo fix the permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanView, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanAdd, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanEdit, out error))
            //{
            //    result.HasError = true;
            //    result.HasViewPermission = false;
            //    result.Errors.Add(error);
            //    return result;
            //}
            try
            {
                var json = new RequisitionCustomJson();
                Inv_Requisition entity;
                if (id <= 0)
                {
                    entity = new Inv_Requisition();
                    entity.LastChanged = DateTime.Now;
                }
                else
                {
                    //entity = voucherDataService.GetById(id, "BAcnt_VoucherDetail");
                    entity = DbInstance.Inv_Requisition
                        .Include(x => x.Inv_RequisitionDetails)
                        .SingleOrDefault(x => x.Id == id);
                }
                //entity.Map(json);
                json.Id = entity.Id;
                json.RequestedByEmployeeId = entity.RequestedByEmployeeId;
                json.RequireDate = entity.RequireDate;
                json.RequireDate = entity.RequireDate;
                json.Purpose = entity.Purpose;
                json.Remark = entity.Remark;
                json.ReferencedByEmployeeId = entity.ReferencedByEmployeeId;
                json.IssuedOrReleaseByUserId = entity.IssuedOrReleaseByUserId;
                json.ApprovedByAdminId = entity.ApprovedByAdminId;
                json.ReceivedByEmployeeId = entity.ReceivedByEmployeeId;
                json.Status = entity.Status;
                json.CreateDate = entity.CreateDate;
                json.CreateById = entity.CreateById;
                json.LastChanged = entity.LastChanged;
                json.LastChangedById = entity.LastChangedById;
                json.IsDeleted = entity.IsDeleted;
                //new Proparty
                json.RequisitionStatusId = entity.RequisitionStatusId;
                json.ApprovedById = entity.ApprovedById;
                json.DeliveredById = entity.DeliveredById;
                json.ItemCategoryId = entity.ItemCategoryId;
                json.Quantity = entity.Quantity;

                foreach (var requisitionDtl in entity.Inv_RequisitionDetails)
                {
                    var obj = new Inv_RequisitionDetailsJson();
                    obj.Id = requisitionDtl.Id;
                    obj.RequisitionId = requisitionDtl.RequisitionId;
                    obj.Quantity = requisitionDtl.Quantity;
                    obj.ItemName = requisitionDtl.ItemName;
                    obj.ItemId = requisitionDtl.ItemId;
                    obj.Detail = requisitionDtl.Detail;
                    obj.Quantity = requisitionDtl.Quantity;
                    obj.ApprovedQuantity = requisitionDtl.ApprovedQuantity;
                    obj.LastChanged = requisitionDtl.LastChanged;
                    obj.LastChangedById = requisitionDtl.LastChangedById;
                    obj.IsDeleted = requisitionDtl.IsDeleted;
                    //new Proparty
                    obj.RequisitionNo = requisitionDtl.RequisitionNo;
                    obj.RequisitionByBPId = requisitionDtl.RequisitionByBPId;
                    obj.RequisitionByName = requisitionDtl.RequisitionByName;
                    obj.RequisitionByRank = requisitionDtl.RequisitionByRank;
                    obj.RequestedByDepartment = requisitionDtl.RequestedByDepartment;
                    obj.RequestedByDepartmentArea = requisitionDtl.RequestedByDepartmentArea;
                    obj.RequisitionDate = requisitionDtl.RequisitionDate;
                    obj.RequierDate = requisitionDtl.RequierDate;
                    obj.ApprovedByBPId = requisitionDtl.ApprovedByBPId;
                    obj.ApprovedByRank = requisitionDtl.ApprovedByRank;
                    obj.ApprovedByDepartment = requisitionDtl.ApprovedByDepartment;
                    obj.StatusEnumId = requisitionDtl.StatusEnumId;
                    obj.Purpose = requisitionDtl.Purpose;

                    json.RequisitionDetailsJson.Add(obj);
                }

                result.Data = json;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }


        public HttpListResult<RequisitionCustomJson> GetRequisitionDataExtra(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpListResult<RequisitionCustomJson>();
            try
            {
                //result.DataExtra.AssetTypeList = EnumUtil.GetEnumList(typeof(Inv_ProductCategory.AssetTypeEnum));
                //DropDown Option Tables

                result.DataExtra.ItemList = DbInstance.Inv_RequisitionDetails.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.ItemName }).ToList();

                result.DataExtra.UserList = DbInstance.User_Account.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.FullName }).ToList();

                //result.DataExtra.SupplierList = DbInstance.Invt_Supplier.AsEnumerable().Select(t => new
                //{ Id = t.Id, Name = t.Name + "( " + t.Phone + " )" }).ToList();

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }


        ////////#region Save/Delete
        [HttpPost]

        public HttpResult<RequisitionCustomJson> SaveRequisition(RequisitionCustomJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<RequisitionCustomJson>();
            //if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanAdd, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanEdit, out error))
            //{
            //    result.HasError = true;
            //    result.Errors.Add(error);
            //    return result;
            //}
            try
            {
                var entityRequisitionReceived = new Inv_Requisition();
                var dbAttachedRequisitionEntity = new Inv_Requisition();
                //json.Map(entityVoucherReceived);
                entityRequisitionReceived.Id = json.Id;
                entityRequisitionReceived.RequisitionDate = json.RequisitionDate;
                entityRequisitionReceived.RequestedByEmployeeId = json.RequestedByEmployeeId;
                entityRequisitionReceived.RequireDate = json.RequireDate;
                entityRequisitionReceived.Purpose = json.Purpose;
                entityRequisitionReceived.Remark = json.Remark;
                entityRequisitionReceived.ReferencedByEmployeeId = json.ReferencedByEmployeeId;
                entityRequisitionReceived.IssuedOrReleaseByUserId = json.IssuedOrReleaseByUserId;
                entityRequisitionReceived.ApprovedByAdminId = json.ApprovedByAdminId;
                entityRequisitionReceived.ReceivedByEmployeeId = json.ReceivedByEmployeeId;
                entityRequisitionReceived.Status = json.Status;
                entityRequisitionReceived.CreateDate = json.CreateDate;
                entityRequisitionReceived.CreateById = json.CreateById;
                entityRequisitionReceived.LastChanged = json.LastChanged;
                entityRequisitionReceived.LastChangedById = json.LastChangedById;
                entityRequisitionReceived.IsDeleted = false;
                //New proparty
                entityRequisitionReceived.RequisitionStatusId = json.RequisitionStatusId;
                entityRequisitionReceived.ApprovedById = json.ApprovedById;
                entityRequisitionReceived.DeliveredById = json.DeliveredById;
                entityRequisitionReceived.ItemCategoryId = json.ItemCategoryId;
                entityRequisitionReceived.Quantity = json.Quantity;

                foreach (var RequisitionDtl in json.RequisitionDetailsJson)
                {
                    var obj = new Inv_RequisitionDetails();
                    obj.Id = RequisitionDtl.Id;
                    obj.RequisitionId = RequisitionDtl.RequisitionId;
                    obj.ItemName = RequisitionDtl.ItemName;
                    obj.ItemId = RequisitionDtl.ItemId;
                    obj.Detail = RequisitionDtl.Detail;
                    obj.Quantity = RequisitionDtl.Quantity;
                    obj.ApprovedQuantity = RequisitionDtl.ApprovedQuantity;
                    obj.LastChanged = RequisitionDtl.LastChanged;
                    obj.LastChangedById = RequisitionDtl.LastChangedById;
                    obj.IsDeleted = RequisitionDtl.IsDeleted;
                    //New Proparty
                    obj.RequisitionNo = RequisitionDtl.RequisitionNo;
                    obj.RequisitionByBPId = RequisitionDtl.RequisitionByBPId;
                    obj.RequisitionByName = RequisitionDtl.RequisitionByName;
                    obj.RequisitionByRank = RequisitionDtl.RequisitionByRank;
                    obj.RequestedByDepartment = RequisitionDtl.RequestedByDepartment;
                    obj.RequestedByDepartmentArea = RequisitionDtl.RequestedByDepartmentArea;
                    obj.RequisitionDate = RequisitionDtl.RequisitionDate;
                    obj.RequierDate = RequisitionDtl.RequierDate;
                    obj.ApprovedByBPId = RequisitionDtl.ApprovedByBPId;
                    obj.ApprovedByRank = RequisitionDtl.ApprovedByRank;
                    obj.ApprovedByDepartment = RequisitionDtl.ApprovedByDepartment;
                    obj.StatusEnumId = RequisitionDtl.StatusEnumId;
                    obj.Purpose = RequisitionDtl.Purpose;

                    entityRequisitionReceived.Inv_RequisitionDetails.Add(obj);
                }




                //var branch = DbInstance.Inv_ProductCategory.SingleOrDefault(x => x.Id == entityVoucherReceived.Id);
                //if (branch == null)
                //{
                //    result.HasError = true;
                //    result.Errors.Add("Please select valid Branch.");
                //    return result;
                //}
                // set Company Id
                //entityVoucherReceived.CompanyId = branch.CompanyId;

                //todo solve next time
                #region Temporary value set

                //entityVoucherReceived.VoucherStatusEnumId = (byte)BAcnt_Voucher.VoucherStatusEnum.NotPosted;
                //entityVoucherReceived.JournalTypeEnumId = (byte)BAcnt_Voucher.JournalTypeEnum.Voucher;

                #endregion

                //bool isAnyDrTransHasZero = entityVoucherReceived.BAcnt_VoucherDetail
                //    .Any(x => x.IsDebited && x.DebitAmount == 0);

                //bool isAnyCrTransHasZero = entityVoucherReceived.BAcnt_VoucherDetail
                //    .Any(x => !x.IsDebited && x.CreditAmount == 0);

                //if (isAnyDrTransHasZero || isAnyCrTransHasZero)
                //{
                //    result.HasError = true;
                //    result.Errors.Add("Any transaction(s) are can not be zero(0).");
                //    return result;
                //}

                //var totalDebitAmount = entityVoucherReceived.BAcnt_VoucherDetail.Where(x => x.IsDebited && !x.IsDeleted)
                //    .Sum(x => x.DebitAmount);

                //var totalCreditAmount = entityVoucherReceived.BAcnt_VoucherDetail.Where(x => !x.IsDebited && !x.IsDeleted)
                //    .Sum(x => x.CreditAmount);

                //if (totalDebitAmount == 0 && totalCreditAmount == 0)
                //{
                //    result.HasError = true;
                //    result.Errors.Add("Invalid amount.");
                //    return result;
                //}

                //if (totalDebitAmount != totalCreditAmount)
                //{
                //    result.HasError = true;
                //    result.Errors.Add("Total debit amount & total credit amount must be same.");
                //    return result;
                //}


                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    // Save Changes Voucher
                    if (!SaveRequisitionLogic(entityRequisitionReceived, ref dbAttachedRequisitionEntity, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                        return result;
                    }

                    //Save Changes for get Voucher Id
                    DbInstance.SaveChanges();

                    var dbAttachedRequisitionDtlEntity = new Inv_RequisitionDetails();

                    foreach (var entityRequisitionDtlReceived in entityRequisitionReceived.Inv_RequisitionDetails.ToList())
                    {
                        entityRequisitionDtlReceived.RequisitionId = dbAttachedRequisitionEntity.Id;

                        if (!SaveRequisitionDetailsLogic(entityRequisitionDtlReceived, ref dbAttachedRequisitionDtlEntity, out error))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                            scope.Rollback();
                            return result;

                        }

                    }

                    DbInstance.SaveChanges();
                    scope.Commit();

                    // dbAttachedVoucherEntity.Map(json);
                    result.Data = json; //return object
                }
            }

            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }


        #region Local Method (should be always private)
        private bool IsValidToSaveRequisition(Inv_Requisition newObj, out string error)
                {
                    error = "";

                    if (newObj.RequisitionDate != null && !newObj.RequisitionDate.IsValid())
                    {
                        newObj.RequisitionDate = null;//just put null,if its nullable and not valid.
                                                      //error += "Requisition Date is not valid.";
                                                      //return false;
                    }
                    if (newObj.RequireDate != null && !newObj.RequireDate.IsValid())
                    {
                        newObj.RequireDate = null;//just put null,if its nullable and not valid.
                                                  //error += "Require Date is not valid.";
                                                  //return false;
                    }
                    if (newObj.Purpose.IsValid() && newObj.Purpose.Length > 500)
                    {
                        error += "Purpose exceeded its maximum length 500.";
                        return false;
                    }
                    if (newObj.Remark.IsValid() && newObj.Remark.Length > 500)
                    {
                        error += "Remark exceeded its maximum length 500.";
                        return false;
                    }





                    if (newObj.IsDeleted == null)
                    {
                        error += "Is Deleted required.";
                        return false;
                    }
                    //TODO write your custom validation here.
                    //var res =  DbInstance.Inv_Requisition.Any(x => x.Id != newObj.Id );
                    //if (res)
                    //{
                    //error = "A Requisition already exists!";
                    //return false;
                    //}
                    return true;
                }
                private bool SaveRequisitionLogic(Inv_Requisition newObj, ref Inv_Requisition dbAddedObj, out string error)
                {
                    error = string.Empty;
                    if (newObj == null)
                    {
                        error = "Requisition to save can't be null!";
                        return false;
                    }

                    if (!IsValidToSaveRequisition(newObj, out error))
                        return false;

                    bool isNewObject = true;
                    Inv_Requisition objToSave = null;

                    if (newObj.Id != 0)
                    {
                        objToSave = DbInstance.Inv_Requisition.SingleOrDefault(x => x.Id == newObj.Id);
                        isNewObject = false;
                    }
                    if (objToSave == null)
                    {   //new object
                        isNewObject = true;
                        objToSave = Inv_Requisition.GetNew(newObj.Id);
                        DbInstance.Inv_Requisition.Add(objToSave);
                        objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                        objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
                    }
                    //todo mandatory fix checking permission, enable it with correction
                    /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd,
                            out error))
                    {
                        return false;
                    }
                    else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit,
                           out error))
                    {
                        return false;
                    }*/

                    //binding object  
                    objToSave.RequestedByEmployeeId = newObj.RequestedByEmployeeId;
                    objToSave.RequisitionDate = newObj.RequisitionDate;
                    objToSave.RequireDate = newObj.RequireDate;
                    objToSave.Purpose = newObj.Purpose;
                    objToSave.Remark = newObj.Remark;
                    objToSave.ReferencedByEmployeeId = newObj.ReferencedByEmployeeId;
                    objToSave.IssuedOrReleaseByUserId = newObj.IssuedOrReleaseByUserId;
                    objToSave.ApprovedByAdminId = newObj.ApprovedByAdminId;
                    objToSave.ReceivedByEmployeeId = newObj.ReceivedByEmployeeId;
                    objToSave.Status = newObj.Status;
                    objToSave.IsDeleted = newObj.IsDeleted;
                    objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
                    objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
                    //New binding object
                    objToSave.RequisitionStatusId = newObj.RequisitionStatusId;
                    objToSave.ApprovedById = newObj.ApprovedById;
                    objToSave.DeliveredById = newObj.DeliveredById;
                    objToSave.ItemCategoryId = newObj.ItemCategoryId;
                    objToSave.Quantity = newObj.Quantity;

            dbAddedObj = objToSave;

                    //here save any child table
                    return true;
                }


                private bool IsValidToSaveRequisitionDetails(Inv_RequisitionDetails newObj, out string error)
                {
                    error = "";

                    if (newObj.ItemName.IsValid() && newObj.ItemName.Length > 50)
                    {
                        error += "Item Name exceeded its maximum length 50.";
                        return false;
                    }

                    if (newObj.Detail.IsValid() && newObj.Detail.Length > 500)
                    {
                        error += "Detail exceeded its maximum length 500.";
                        return false;
                    }


                    if (newObj.IsDeleted == null)
                    {
                        error += "Is Deleted required.";
                        return false;
                    }
                    //TODO write your custom validation here.
                    //var res =  DbInstance.Inv_RequisitionDetails.Any(x => x.Id != newObj.Id );
                    //if (res)
                    //{
                    //error = "A RequisitionDetails already exists!";
                    //return false;
                    //}
                    return true;
                }
                private bool SaveRequisitionDetailsLogic(Inv_RequisitionDetails newObj, ref Inv_RequisitionDetails dbAddedObj, out string error)
                {
                    error = string.Empty;
                    if (newObj == null)
                    {
                        error = "Requisition Details to save can't be null!";
                        return false;
                    }

                    if (!IsValidToSaveRequisitionDetails(newObj, out error))
                        return false;

                    bool isNewObject = true;
                    Inv_RequisitionDetails objToSave = null;

                    if (newObj.Id != 0)
                    {
                        objToSave = DbInstance.Inv_RequisitionDetails.SingleOrDefault(x => x.Id == newObj.Id);
                        isNewObject = false;
                    }
                    if (objToSave == null)
                    {   //new object
                        isNewObject = true;
                        objToSave = Inv_RequisitionDetails.GetNew(newObj.Id);
                        DbInstance.Inv_RequisitionDetails.Add(objToSave);


                    }
                    //todo mandatory fix checking permission, enable it with correction
                    /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanAdd,
                            out error))
                    {
                        return false;
                    }
                    else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanEdit,
                           out error))
                    {
                        return false;
                    }*/

                    //binding object  
                    objToSave.RequisitionId = newObj.RequisitionId;
                    objToSave.ItemName = newObj.ItemName;
                    objToSave.ItemId = newObj.ItemId;
                    objToSave.Detail = newObj.Detail;
                    objToSave.Quantity = newObj.Quantity;
                    objToSave.ApprovedQuantity = newObj.ApprovedQuantity;
                    objToSave.IsDeleted = newObj.IsDeleted;
                    objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
                    objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            //New binding object  
            objToSave.RequisitionNo = newObj.RequisitionNo;
            objToSave.RequisitionByBPId = newObj.RequisitionByBPId;
            objToSave.RequisitionByName = newObj.RequisitionByName;
            objToSave.RequisitionByRank = newObj.RequisitionByRank;
            objToSave.RequestedByDepartment = newObj.RequestedByDepartment;
            objToSave.RequestedByDepartmentArea = newObj.RequestedByDepartmentArea;
            objToSave.RequisitionDate = newObj.RequisitionDate;
            objToSave.RequierDate = newObj.RequierDate;
            objToSave.ApprovedByBPId = newObj.ApprovedByBPId;
            objToSave.ApprovedByRank = newObj.ApprovedByRank;
            objToSave.ApprovedByDepartment = newObj.ApprovedByDepartment;
            objToSave.StatusEnumId = newObj.StatusEnumId;
            objToSave.Purpose = newObj.Purpose;
            dbAddedObj = objToSave;

                    //here save any child table
                    return true;
                }

                #endregion

                //public HttpListResult<Inv_ProductCategoryJson> GetPagedProductCategory(string textkey, int? pageSize, int? pageNo
                //)
                //{
                //    string error = string.Empty;
                //    int count = 0;
                //    var result = new HttpListResult<Inv_ProductCategoryJson>();
                //    //todo fix the permission
                //    /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanView, out error)
                //        && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanAdd, out error)
                //        && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanEdit, out error))
                //    {
                //        result.HasError = true;
                //         result.HasViewPermission = false;
                //        result.Errors.Add(error);
                //        return result;
                //    }*/

                //    try
                //    {
                //        using (Inv_ProductCategoryDataService productCategoryDataService = new Inv_ProductCategoryDataService(DbInstance, HttpUtil.Profile))
                //        {
                //            IQueryable<Inv_ProductCategory> query = DbInstance.Inv_ProductCategory.OrderByDescending(x => x.Id).Where(x => x.IsDeleted == false);
                //            if (textkey.IsValid())
                //            {
                //                query = query.Where(q => q.Title.ToLower().Contains(textkey.ToLower()));
                //            }

                //            var entities = productCategoryDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                //            var jsons = new List<Inv_ProductCategoryJson>();
                //            entities.Map(jsons);

                //            result.Data = jsons;
                //            result.Count = count;
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        result.HasError = true;
                //        result.Errors.Add(ex.GetBaseException().Message.ToString());
                //    }
                //    return result;
                //}

            }

    public class RequisitionCustomJson
    {
        public Int32 Id { get; set; }
        public Nullable<Int32> RequestedByEmployeeId { get; set; }
        public Nullable<DateTime> RequisitionDate { get; set; }
        public Nullable<DateTime> RequireDate { get; set; }
        public String Purpose { get; set; }
        public String Remark { get; set; }
        public Nullable<Int32> ReferencedByEmployeeId { get; set; }
        public Nullable<Int32> IssuedOrReleaseByUserId { get; set; }
        public Nullable<Int32> ApprovedByAdminId { get; set; }
        public Nullable<Int32> ReceivedByEmployeeId { get; set; }
        public Nullable<Int32> Status { get; set; }
        public DateTime CreateDate { get; set; }
        public Int32 CreateById { get; set; }
        public DateTime LastChanged { get; set; }
        public Int32 LastChangedById { get; set; }
        public Boolean IsDeleted { get; set; }
        //New Proparty
        public Nullable<int> RequisitionStatusId { get; set; }
        public Nullable<int> ApprovedById { get; set; }
        public Nullable<int> DeliveredById { get; set; }
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<int> Quantity { get; set; }

        public List<Inv_RequisitionDetailsJson> RequisitionDetailsJson { get; set; }
        public RequisitionCustomJson()
        {
            RequisitionDetailsJson = new List<Inv_RequisitionDetailsJson>();
        }

    }
}