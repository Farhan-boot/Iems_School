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
    public class InventoryManagerApiController : EmployeeBaseApiController
    {
        
        public HttpResult<InventoryCustomJson> GetInventoryById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<InventoryCustomJson>();
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
                var json = new InventoryCustomJson();
                Inv_Inventory entity;
                if (id <= 0)
                {
                    entity = new Inv_Inventory();
                    entity.LastChanged = DateTime.Now;
                }
                else
                {
                    //entity = voucherDataService.GetById(id, "BAcnt_VoucherDetail");
                    entity = DbInstance.Inv_Inventory
                        .Include(x => x.Inv_InventoryDetails)
                        .SingleOrDefault(x => x.Id == id);
                    //entity.Map(json);
                    json.Id = entity.Id;
                    json.StoreId = entity.StoreId;
                    json.VoucherNo = entity.VoucherNo;
                    json.VoucherDate = entity.VoucherDate;
                    json.InvoiceNo = entity.InvoiceNo;
                    json.InvoiceDate = entity.InvoiceDate;
                    json.PurchaseOrderNo = entity.PurchaseOrderNo;
                    json.PurchaseOrderDate = entity.PurchaseOrderDate;
                    json.ChalanNo = entity.ChalanNo;
                    json.ChalanDate = entity.ChalanDate;
                    json.ReceivedDate = entity.ReceivedDate;
                    json.ReceivedBy = entity.ReceivedBy;
                    json.SupplierId = entity.SupplierId;
                    json.Remark = entity.Remark;
                    json.History = entity.History;
                    json.LastChangedById = entity.LastChangedById;
                    json.IsDeleted = entity.IsDeleted;

                    foreach (var inventoryDtl in entity.Inv_InventoryDetails)
                    {
                        var obj = new Inv_InventoryDetailsJson();
                        obj.Id = inventoryDtl.Id;
                        obj.InventoryId = inventoryDtl.InventoryId;
                        obj.ProductCategoryId = inventoryDtl.ProductCategoryId;
                        obj.Quantity = inventoryDtl.Quantity;
                        obj.UnitPrice = inventoryDtl.UnitPrice;
                        obj.WarrentyStartDate = inventoryDtl.WarrentyStartDate;
                        obj.WarrentyExpairDate = inventoryDtl.WarrentyExpairDate;
                        obj.Description = inventoryDtl.Description;
                        obj.OwnBarcode = inventoryDtl.OwnBarcode;
                        obj.OurBarcode = inventoryDtl.OurBarcode;
                        obj.StatusEnumId = inventoryDtl.StatusEnumId;
                        obj.Remark = inventoryDtl.Remark;
                        obj.IsBarcoded = inventoryDtl.IsBarcoded;

                        obj.LastChanged = inventoryDtl.LastChanged;
                        obj.LastChangedById = inventoryDtl.LastChangedById;
                        obj.IsDeleted = inventoryDtl.IsDeleted;

                        json.InventoryDetailsJson.Add(obj);
                    }
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

        public HttpListResult<InventoryCustomJson> GetInventoryDataExtra(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpListResult<InventoryCustomJson>();
            try
            {
                //result.DataExtra.AssetTypeList = EnumUtil.GetEnumList(typeof(Inv_ProductCategory.AssetTypeEnum));
                //DropDown Option Tables

                result.DataExtra.StatusList = EnumUtil.GetEnumList(typeof(Inv_InventoryDetails.StatusEnum));

                //DropDown Option Tables

                result.DataExtra.SupplierList = DbInstance.Inv_Supplier.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.CompanyName }).ToList();

                result.DataExtra.UserList = DbInstance.User_Account.AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.FullName }).ToList();

                result.DataExtra.CategoryList = DbInstance.Inv_ProductCategory.AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.ItemList = DbInstance.Inv_Item.AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.ItemName }).ToList();

                result.DataExtra.StoreList = DbInstance.Inv_Store.AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.Name }).ToList();


                //result.DataExtra.WarhouseList = DbInstance.Invt_Warhouse.AsEnumerable().Select(t => new
                //{ Id = t.Id, Name = t.Name }).ToList();

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


        //#region Save/Delete
        [HttpPost]

        public HttpResult<InventoryCustomJson> SaveInventory(InventoryCustomJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<InventoryCustomJson>();
            //if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanAdd, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanEdit, out error))
            //{
            //    result.HasError = true;
            //    result.Errors.Add(error);
            //    return result;
            //}
            try
            {
                var entityInventoryReceived = new Inv_Inventory();
                var dbAttachedInventoryEntity = new Inv_Inventory();
                //json.Map(entityVoucherReceived);
                entityInventoryReceived.Id = json.Id;
                entityInventoryReceived.StoreId = json.StoreId;
                entityInventoryReceived.VoucherNo = json.VoucherNo;
                entityInventoryReceived.VoucherDate = json.VoucherDate;
                entityInventoryReceived.InvoiceNo = json.InvoiceNo;
                entityInventoryReceived.InvoiceDate = json.InvoiceDate;
                entityInventoryReceived.PurchaseOrderNo = json.PurchaseOrderNo;
                entityInventoryReceived.PurchaseOrderDate = json.PurchaseOrderDate;
                entityInventoryReceived.ChalanNo = json.ChalanNo;
                entityInventoryReceived.ChalanDate = json.ChalanDate;
                entityInventoryReceived.ReceivedDate = json.ReceivedDate;
                entityInventoryReceived.ReceivedBy = json.ReceivedBy;
                entityInventoryReceived.SupplierId = json.SupplierId;
                entityInventoryReceived.Remark = json.Remark;
                entityInventoryReceived.History = json.History;

                entityInventoryReceived.CreateDate = json.CreateDate;
                entityInventoryReceived.CreateById = json.CreateById;
                entityInventoryReceived.LastChanged = json.LastChanged;
                entityInventoryReceived.LastChangedById = json.LastChangedById;
                entityInventoryReceived.IsDeleted = false;

                foreach (var InventoryDtl in json.InventoryDetailsJson)
                {
                    var obj = new Inv_InventoryDetails();
                    obj.Id = InventoryDtl.Id;
                    obj.InventoryId = InventoryDtl.InventoryId;
                    obj.ProductCategoryId = InventoryDtl.ProductCategoryId;
                    obj.Quantity = InventoryDtl.Quantity;
                    obj.UnitPrice = InventoryDtl.UnitPrice;
                    obj.WarrentyStartDate = InventoryDtl.WarrentyStartDate;
                    obj.WarrentyExpairDate = InventoryDtl.WarrentyExpairDate;
                    obj.Description = InventoryDtl.Description;
                    obj.OwnBarcode = InventoryDtl.OwnBarcode;
                    obj.OurBarcode = InventoryDtl.OurBarcode;
                    obj.StatusEnumId = InventoryDtl.StatusEnumId;
                    obj.Remark = InventoryDtl.Remark;
                    obj.IsBarcoded = InventoryDtl.IsBarcoded;

                    obj.LastChanged = InventoryDtl.LastChanged;
                    obj.LastChangedById = InventoryDtl.LastChangedById;
                    obj.IsDeleted = InventoryDtl.IsDeleted;

                    entityInventoryReceived.Inv_InventoryDetails.Add(obj);
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
                    if (!SaveInventoryLogic(entityInventoryReceived, ref dbAttachedInventoryEntity, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                        //return result;
                    }

                    //Save Changes for get Voucher Id
                    DbInstance.SaveChanges();

                    var dbAttachedInventoryDtlEntity = new Inv_InventoryDetails();

                    foreach (var entityInventoryDtlReceived in entityInventoryReceived.Inv_InventoryDetails.ToList())
                    {
                        entityInventoryDtlReceived.InventoryId = dbAttachedInventoryEntity.Id;

                        if (!SaveInventoryDetailsLogic(entityInventoryDtlReceived, ref dbAttachedInventoryDtlEntity, out error))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                            scope.Rollback();
                            return result;

                        }

                    }

                    DbInstance.SaveChanges();
                    scope.Commit();

                    // dbAttachedInventoryEntity.Map(json);
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
        private bool IsValidToSaveInventory(Inv_Inventory newObj, out string error)
        {
            error = "";
            if (newObj.StoreId == null)
            {
                error += "Store Id required.";
                return false;
            }
            if (newObj.VoucherNo.IsValid() && newObj.VoucherNo.Length > 50)
            {
                error += "Voucher No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.VoucherDate != null && !newObj.VoucherDate.IsValid())
            {
                newObj.VoucherDate = null;//just put null,if its nullable and not valid.
                                          //error += "Voucher Date is not valid.";
                                          //return false;
            }
            if (newObj.InvoiceNo.IsValid() && newObj.InvoiceNo.Length > 50)
            {
                error += "Invoice No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.InvoiceDate != null && !newObj.InvoiceDate.IsValid())
            {
                newObj.InvoiceDate = null;//just put null,if its nullable and not valid.
                                          //error += "Invoice Date is not valid.";
                                          //return false;
            }

            if (newObj.PurchaseOrderDate != null && !newObj.PurchaseOrderDate.IsValid())
            {
                newObj.PurchaseOrderDate = null;//just put null,if its nullable and not valid.
                                                //error += "Purchase Order Date is not valid.";
                                                //return false;
            }
            if (newObj.ChalanNo.IsValid() && newObj.ChalanNo.Length > 50)
            {
                error += "Chalan No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.ChalanDate != null && !newObj.ChalanDate.IsValid())
            {
                newObj.ChalanDate = null;//just put null,if its nullable and not valid.
                                         //error += "Chalan Date is not valid.";
                                         //return false;
            }
            if (newObj.ReceivedDate != null && !newObj.ReceivedDate.IsValid())
            {
                newObj.ReceivedDate = null;//just put null,if its nullable and not valid.
                                           //error += "Received Date is not valid.";
                                           //return false;
            }



            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }

            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_Inventory.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A Inventory already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveInventoryLogic(Inv_Inventory newObj, ref Inv_Inventory dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Inventory to save can't be null!";
                return false;
            }

            if (!IsValidToSaveInventory(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_Inventory objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_Inventory.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_Inventory.GetNew(newObj.Id);
                DbInstance.Inv_Inventory.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.StoreId = newObj.StoreId;
            objToSave.VoucherNo = newObj.VoucherNo;
            objToSave.VoucherDate = newObj.VoucherDate;
            objToSave.InvoiceNo = newObj.InvoiceNo;
            objToSave.InvoiceDate = newObj.InvoiceDate;
            objToSave.PurchaseOrderNo = newObj.PurchaseOrderNo;
            objToSave.PurchaseOrderDate = newObj.PurchaseOrderDate;
            objToSave.ChalanNo = newObj.ChalanNo;
            objToSave.ChalanDate = newObj.ChalanDate;
            objToSave.ReceivedDate = newObj.ReceivedDate;
            objToSave.ReceivedBy = newObj.ReceivedBy;
            objToSave.SupplierId = newObj.SupplierId;
            objToSave.Remark = newObj.Remark;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.History = newObj.History;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }


        private bool IsValidToSaveInventoryDetails(Inv_InventoryDetails newObj, out string error)
        {
            error = "";




            if (newObj.WarrentyStartDate != null && !newObj.WarrentyStartDate.IsValid())
            {
                newObj.WarrentyStartDate = null;//just put null,if its nullable and not valid.
                                                //error += "Warrenty Start Date is not valid.";
                                                //return false;
            }
            if (newObj.WarrentyExpairDate != null && !newObj.WarrentyExpairDate.IsValid())
            {
                newObj.WarrentyExpairDate = null;//just put null,if its nullable and not valid.
                                                 //error += "Warrenty Expair Date is not valid.";
                                                 //return false;
            }
            if (newObj.Description.IsValid() && newObj.Description.Length > 50)
            {
                error += "Description exceeded its maximum length 50.";
                return false;
            }
            if (newObj.OwnBarcode.IsValid() && newObj.OwnBarcode.Length > 50)
            {
                error += "Own Barcode exceeded its maximum length 50.";
                return false;
            }
            if (newObj.OurBarcode.IsValid() && newObj.OurBarcode.Length > 50)
            {
                error += "Our Barcode exceeded its maximum length 50.";
                return false;
            }



            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_InventoryDetails.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A InventoryDetails already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveInventoryDetailsLogic(Inv_InventoryDetails newObj, ref Inv_InventoryDetails dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Inventory Details to save can't be null!";
                return false;
            }

            if (!IsValidToSaveInventoryDetails(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_InventoryDetails objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_InventoryDetails.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_InventoryDetails.GetNew(newObj.Id);
                DbInstance.Inv_InventoryDetails.Add(objToSave);


            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.InventoryId = newObj.InventoryId;
            objToSave.ProductCategoryId = newObj.ProductCategoryId;
            objToSave.Quantity = newObj.Quantity;
            objToSave.UnitPrice = newObj.UnitPrice;
            objToSave.WarrentyStartDate = newObj.WarrentyStartDate;
            objToSave.WarrentyExpairDate = newObj.WarrentyExpairDate;
            objToSave.Description = newObj.Description;
            objToSave.OwnBarcode = newObj.OwnBarcode;
            objToSave.OurBarcode = newObj.OurBarcode;
            objToSave.StatusEnumId = newObj.StatusEnumId;
            objToSave.Remark = newObj.Remark;
            objToSave.IsBarcoded = newObj.IsBarcoded;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }

        #endregion

        public HttpResult<Inv_InventoryJson> GetDeleteInventoryById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_InventoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                //using (Inv_InventoryDataService inventoryDataService = new Inv_InventoryDataService(DbInstance, HttpUtil.Profile))
                //{
                //    if (!inventoryDataService.DeleteById(id, out error))
                //    {
                //        result.HasError = true;
                //        result.Errors.Add(error);
                //    }
                //}

                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    try
                    {
                        var deleteInvDtlsObj = DbInstance.Inv_InventoryDetails.Where(x => x.Id == id);
                        DbInstance.Inv_InventoryDetails.RemoveRange(deleteInvDtlsObj);

                        var deleteInvObj = DbInstance.Inv_Inventory.SingleOrDefault(x => x.Id == id);
                        DbInstance.Inv_Inventory.Remove(deleteInvObj);
                        DbInstance.SaveChanges();
                        scope.Commit();
                    }
                    catch (Exception x)
                    {
                        scope.Rollback();
                        Console.WriteLine(x);
                        throw;
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

    }

    public class InventoryCustomJson
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string VoucherNo { get; set; }
        public Nullable<System.DateTime> VoucherDate { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string PurchaseOrderNo { get; set; }
        public Nullable<System.DateTime> PurchaseOrderDate { get; set; }
        public string ChalanNo { get; set; }
        public Nullable<System.DateTime> ChalanDate { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public Nullable<int> ReceivedBy { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public string Remark { get; set; }
        public string History { get; set; }


        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
        public bool IsDeleted { get; set; }



        public List<Inv_InventoryDetailsJson> InventoryDetailsJson { get; set; }
        public InventoryCustomJson()
        {
            InventoryDetailsJson = new List<Inv_InventoryDetailsJson>();
        }

    }
}