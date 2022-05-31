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
    public class ProductCategoryManagerApiController : EmployeeBaseApiController
    {
        public HttpResult<ProductCategoryCustomJson> GetProductCategoryById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<ProductCategoryCustomJson>();
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
                var json = new ProductCategoryCustomJson();
                Inv_ProductCategory entity;
                if (id <= 0)
                {
                    entity = new Inv_ProductCategory();
                    entity.LastChanged = DateTime.Now;
                }
                else
                {
                    //entity = voucherDataService.GetById(id, "BAcnt_VoucherDetail");
                    entity = DbInstance.Inv_ProductCategory
                        .Include(x => x.Inv_ProductCategoryDetails)
                        .SingleOrDefault(x => x.Id == id);
                }
                //entity.Map(json);
                json.Id = entity.Id;
                json.Title = entity.Title;
                json.ParentId = entity.ParentId;
                json.Description = entity.Description;
                json.AssetTypeEnumId = entity.AssetTypeEnumId;
                json.CreateDate = entity.CreateDate;
                json.CreateById = entity.CreateById;
                json.LastChanged = entity.LastChanged;
                json.LastChangedById = entity.LastChangedById;
                json.IsDeleted = entity.IsDeleted;
                //New Object
                json.Name = entity.Name;
                json.ProductCode = entity.ProductCode;
                json.IsBarCoded = entity.IsBarCoded;
                json.DefaultStoreId = entity.DefaultStoreId;
                json.UnitTypeEnumId = entity.UnitTypeEnumId;
                json.SubTitle = entity.SubTitle;
                json.CategoryUnitEnumId = entity.CategoryUnitEnumId;
                json.CategoryTypeEnumId = entity.CategoryTypeEnumId;
                json.BarcodeTypeEnumId = entity.BarcodeTypeEnumId;
                json.WarningQuantity = entity.WarningQuantity;
                json.Remark = entity.Remark;
                json.ParentCategoryId = entity.ParentCategoryId;
                json.StoreId = entity.StoreId;


                foreach (var productDtl in entity.Inv_ProductCategoryDetails)
                {
                    var obj = new Inv_ProductCategoryDetailsJson();
                    obj.Id = productDtl.Id;
                    obj.ProductCategoryId = productDtl.ProductCategoryId;
                    obj.Unit = productDtl.Unit;
                    obj.UnitTypeEnumId = productDtl.UnitTypeEnumId;
                    obj.WarrningQuantity = productDtl.WarrningQuantity;
                    obj.Details = productDtl.Details;
                    obj.HasBarcode = productDtl.HasBarcode;
                    obj.LastChanged = productDtl.LastChanged;
                    obj.LastChangedById = productDtl.LastChangedById;
                    obj.IsDeleted = productDtl.IsDeleted;

                    json.ProductCategoryDetailsJson=obj;
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

        public HttpListResult<ProductCategoryCustomJson> GetProductCategoryDataExtra(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpListResult<ProductCategoryCustomJson>();
            try
            {
                result.DataExtra.AssetTypeList = EnumUtil.GetEnumList(typeof(Inv_ProductCategory.AssetTypeEnum));
                result.DataExtra.UnitTypeEnumList = EnumUtil.GetEnumList(typeof(Inv_PurchaseRequisitionDetail.UnitTypeEnum));
                //DropDown Option Tables

                result.DataExtra.StoreList = DbInstance.Inv_Store.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Title }).ToList();

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

        public HttpResult<ProductCategoryCustomJson> SaveProductCategory(ProductCategoryCustomJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<ProductCategoryCustomJson>();
            //if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanAdd, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanEdit, out error))
            //{
            //    result.HasError = true;
            //    result.Errors.Add(error);
            //    return result;
            //}
            try
            {
                var entityProductCategoryReceived = new Inv_ProductCategory();
                var dbAttachedProductCategoryEntity = new Inv_ProductCategory();
                //json.Map(entityVoucherReceived);
                entityProductCategoryReceived.Id = json.Id;
                entityProductCategoryReceived.Title = json.Title;
                entityProductCategoryReceived.ParentId = json.ParentId;
                entityProductCategoryReceived.Description = json.Description;
                entityProductCategoryReceived.AssetTypeEnumId = json.AssetTypeEnumId;
                entityProductCategoryReceived.CreateDate = json.CreateDate;
                entityProductCategoryReceived.CreateById = json.CreateById;
                entityProductCategoryReceived.LastChanged = json.LastChanged;
                entityProductCategoryReceived.LastChangedById = json.LastChangedById;
                entityProductCategoryReceived.IsDeleted = false;
                //New Proparty
                entityProductCategoryReceived.Name = json.Name; ;
                entityProductCategoryReceived.ProductCode = json.ProductCode;
                entityProductCategoryReceived.IsBarCoded = json.IsBarCoded;
                entityProductCategoryReceived.DefaultStoreId = json.DefaultStoreId;
                entityProductCategoryReceived.UnitTypeEnumId = json.UnitTypeEnumId;
                entityProductCategoryReceived.SubTitle = json.SubTitle;
                entityProductCategoryReceived.CategoryUnitEnumId = json.CategoryUnitEnumId;
                entityProductCategoryReceived.CategoryTypeEnumId = json.CategoryTypeEnumId;
                entityProductCategoryReceived.BarcodeTypeEnumId = json.BarcodeTypeEnumId;
                entityProductCategoryReceived.WarningQuantity = json.WarningQuantity;
                entityProductCategoryReceived.Remark = json.Remark;
                entityProductCategoryReceived.ParentCategoryId = json.ParentCategoryId;
                entityProductCategoryReceived.StoreId = json.StoreId;


                // foreach (var ProductDtl in json.ProductCategoryDetailsJson.Id)
                //{
                var obj = new Inv_ProductCategoryDetails();
                    obj.Id = json.ProductCategoryDetailsJson.Id;
                    obj.Unit = json.ProductCategoryDetailsJson.Unit;
                    obj.UnitTypeEnumId = json.ProductCategoryDetailsJson.UnitTypeEnumId;
                    obj.WarrningQuantity = json.ProductCategoryDetailsJson.WarrningQuantity;
                    obj.Details = json.ProductCategoryDetailsJson.Details;
                    obj.HasBarcode = json.ProductCategoryDetailsJson.HasBarcode;

                    entityProductCategoryReceived.Inv_ProductCategoryDetails.Add(obj);
                //}




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
                    if (!SaveProductCategoryLogic(entityProductCategoryReceived, ref dbAttachedProductCategoryEntity, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                        return result;
                    }

                    //Save Changes for get Voucher Id
                    DbInstance.SaveChanges();

                    var dbAttachedVoucherDtlEntity = new Inv_ProductCategoryDetails();

                    foreach (var entityVoucherDtlReceived in entityProductCategoryReceived.Inv_ProductCategoryDetails.ToList())
                    {
                        entityVoucherDtlReceived.ProductCategoryId = dbAttachedProductCategoryEntity.Id;

                        if (!SaveProductCategoryDetailsLogic(entityVoucherDtlReceived, ref dbAttachedVoucherDtlEntity, out error))
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
        private bool IsValidToSaveProductCategory(Inv_ProductCategory newObj, out string error)
        {
            error = "";
            if (!newObj.Title.IsValid())
            {
                error += "Title is not valid.";
                return false;
            }
            if (newObj.Title.Length > 100)
            {
                error += "Title exceeded its maximum length 100.";
                return false;
            }

            if (!newObj.Description.IsValid())
            {
                error += "Description is not valid.";
                return false;
            }
            if (newObj.Description.Length > 250)
            {
                error += "Description exceeded its maximum length 250.";
                return false;
            }

            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_ProductCategory.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A ProductCategory already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveProductCategoryLogic(Inv_ProductCategory newObj, ref Inv_ProductCategory dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Product Category to save can't be null!";
                return false;
            }

            if (!IsValidToSaveProductCategory(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_ProductCategory objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_ProductCategory.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_ProductCategory.GetNew(newObj.Id);
                DbInstance.Inv_ProductCategory.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.Title = newObj.Title;
            objToSave.ParentId = newObj.ParentId;
            objToSave.Description = newObj.Description;
            objToSave.AssetTypeEnumId = newObj.AssetTypeEnumId;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            //New Object
            objToSave.Name = newObj.Name;
            objToSave.ProductCode = newObj.ProductCode;
            objToSave.IsBarCoded = newObj.IsBarCoded;
            objToSave.DefaultStoreId = newObj.DefaultStoreId;
            objToSave.UnitTypeEnumId = newObj.UnitTypeEnumId;
            objToSave.SubTitle = newObj.SubTitle;
            objToSave.CategoryUnitEnumId = newObj.CategoryUnitEnumId;
            objToSave.CategoryTypeEnumId = newObj.CategoryTypeEnumId;
            objToSave.BarcodeTypeEnumId = newObj.BarcodeTypeEnumId;
            objToSave.WarningQuantity = newObj.WarningQuantity;
            objToSave.Remark = newObj.Remark;
            objToSave.ParentCategoryId = newObj.ParentCategoryId;
            objToSave.StoreId = newObj.StoreId;

            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }


        private bool IsValidToSaveProductCategoryDetails(Inv_ProductCategoryDetails newObj, out string error)
        {
            error = "";
            if (newObj.ProductCategoryId <= 0)
            {
                error += "Please select valid Product Category.";
                return false;
            }



            if (newObj.Details.IsValid() && newObj.Details.Length > 250)
            {
                error += "Details exceeded its maximum length 250.";
                return false;
            }

            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_ProductCategoryDetails.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A ProductCategoryDetails already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveProductCategoryDetailsLogic(Inv_ProductCategoryDetails newObj, ref Inv_ProductCategoryDetails dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Product Category Details to save can't be null!";
                return false;
            }

            if (!IsValidToSaveProductCategoryDetails(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_ProductCategoryDetails objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_ProductCategoryDetails.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_ProductCategoryDetails.GetNew(newObj.Id);
                DbInstance.Inv_ProductCategoryDetails.Add(objToSave);


            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.ProductCategoryId = newObj.ProductCategoryId;
            objToSave.Unit = newObj.Unit;
            objToSave.UnitTypeEnumId = newObj.UnitTypeEnumId;
            objToSave.WarrningQuantity = newObj.WarrningQuantity;
            objToSave.Details = newObj.Details;
            objToSave.HasBarcode = newObj.HasBarcode;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }

        #endregion

        public HttpListResult<Inv_ProductCategoryJson> GetPagedProductCategory(string textkey, int? pageSize, int? pageNo
        )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_ProductCategoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_ProductCategoryDataService productCategoryDataService = new Inv_ProductCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<Inv_ProductCategory> query = DbInstance.Inv_ProductCategory.OrderByDescending(x => x.Id).Where(x => x.IsDeleted == false);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Title.ToLower().Contains(textkey.ToLower()));
                    }

                    var entities = productCategoryDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_ProductCategoryJson>();
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


        public HttpResult<Inv_ProductCategoryJson> GetDeleteProductById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ProductCategoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {


               var pro = DbInstance.Inv_ProductCategory.SingleOrDefault(x => x.Id == id);
               pro.IsDeleted = true;
               //DbInstance.SaveChanges();

               var proDtls = DbInstance.Inv_ProductCategoryDetails.Where(x => x.ProductCategoryId == id);
               foreach (var dtl in proDtls)
               {
                   dtl.IsDeleted = true;
               }
               DbInstance.SaveChanges();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }


    }

    public class ProductCategoryCustomJson
    {
        public Int32 Id { get; set; }
        public String Title { get; set; }
        public Nullable<Int32> ParentId { get; set; }
        public String Description { get; set; }
        public Nullable<Int32> AssetTypeEnumId { get; set; }
        public string AssetType { get; set; }
        public DateTime CreateDate { get; set; }
        public Int32 CreateById { get; set; }
        public DateTime LastChanged { get; set; }
        public Int32 LastChangedById { get; set; }
        public Boolean IsDeleted { get; set; }
        //New Proparty
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public Nullable<bool> IsBarCoded { get; set; }
        public Nullable<int> DefaultStoreId { get; set; }
        public Nullable<int> UnitTypeEnumId { get; set; }
        public string SubTitle { get; set; }
        public Nullable<int> CategoryUnitEnumId { get; set; }
        public Nullable<int> CategoryTypeEnumId { get; set; }
        public Nullable<int> BarcodeTypeEnumId { get; set; }
        public Nullable<int> WarningQuantity { get; set; }
        public string Remark { get; set; }
        public Nullable<int> ParentCategoryId { get; set; }
        public Nullable<int> StoreId { get; set; }



        public Inv_ProductCategoryDetailsJson ProductCategoryDetailsJson { get; set; }
        public ProductCategoryCustomJson()
        {
            ProductCategoryDetailsJson = new Inv_ProductCategoryDetailsJson();
        }

    }
}