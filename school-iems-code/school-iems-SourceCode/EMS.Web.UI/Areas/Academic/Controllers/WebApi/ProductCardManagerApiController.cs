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


namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{
    public class ProductCardManagerApiController : EmployeeBaseApiController
    {

        public HttpResult<PurchaseInformationJson> GetItemInformation(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<PurchaseInformationJson>();

            try
            {
                var json = new PurchaseInformationJson();
                result.DataExtra.PurchaseInformation = json;
                result.DataExtra.Product = new ProductJson();

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }



        [HttpPost]
        public HttpResult<PurchaseInformationJson> SaveItemInformation(PurchaseInformationJson purchaseInformationJson)
        {
            string error = string.Empty;
            var result = new HttpResult<PurchaseInformationJson>();
            //if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanAdd, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanEdit, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanForceEdit, out error))
            //{
            //    result.HasError = true;
            //    result.Errors.Add(error);
            //    return result;
            //}
            //try
            //{
            //    //var entityReceived = new Aca_Course();
            //    //var dbAttachedEntity = new Aca_Course();
            //    //json.Map(entityReceived);
            //    using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
            //    {
            //        if (SaveCourseLogic(purchaseInformationJson, ref dbAttachedEntity, out error))
            //        {
            //            DbInstance.SaveChanges();
            //            scope.Commit();

            //            dbAttachedEntity.Map(json);
            //            result.Data = json; //return object
            //        }
            //        else
            //        {
            //            result.HasError = true;
            //            result.Errors.Add(error);
            //            scope.Rollback();
            //        }
            //    }
            //}

            //catch (Exception ex)
            //{
            //    result.HasError = true;
            //    result.Errors.Add(ex.GetBaseException().Message.ToString());
            //}
            return result;
        }




        //private bool SaveCourseLogic(PurchaseInformationJson newObj, ref Aca_Course dbAddedObj, out string error)
        //{
        //    error = string.Empty;
        //    if (newObj == null)
        //    {
        //        error = "Purchase Information to save can't be null!";
        //        return false;
        //    }

        //    if (!IsValidToSaveCourse(newObj, out error))
        //        return false;

        //    bool isNewObject = true;
        //    PurchaseInformationJson objToSave = null;

        //    if (newObj.Id != 0)
        //    {
        //        objToSave = DbInstance.Aca_Course.SingleOrDefault(x => x.Id == newObj.Id);
        //        isNewObject = false;
        //    }
        //    else
        //    {
        //        newObj.Id = BigInt.NewBigInt();
        //    }
        //    if (objToSave == null)
        //    {
        //        //new object
        //        isNewObject = true;
        //        objToSave = Aca_Course.GetNew(newObj.Id);
        //        DbInstance.Aca_Course.Add(objToSave);
        //        objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
        //        objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
        //    }
        //    else
        //    {
        //        var res = DbInstance.Aca_CurriculumCourse.Any(x => x.CourseId == newObj.Id);

        //        bool isPermit = PermissionUtil.HasPermission(
        //            PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanForceEdit);
        //        if (res && !isPermit)
        //        {
        //            var msg = " Because this Course is already used in Curriculum Course.";
        //            //if (objToSave.DepartmentId != newObj.DepartmentId)
        //            //{
        //            //    error = "Course Department can't be editable." + msg;
        //            //    return false;
        //            //}
        //            if (objToSave.CourseCategoryEnumId != newObj.CourseCategoryEnumId)
        //            {
        //                error = "Course Category can't be editable." + msg;
        //                return false;
        //            }
        //            if (!objToSave.CreditLoad.Equals(newObj.CreditLoad))
        //            {
        //                error = "Course Credit Hour can't be editable." + msg;
        //                return false;
        //            }
        //            if (!objToSave.CreditHour.Equals(newObj.CreditHour))
        //            {
        //                error = "Course Contact Hour can't be editable." + msg;
        //                return false;
        //            }
        //        }
        //    }
        //    //checking permission, enable it with correction
        //    /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CourseManager.Course.CanAdd,
        //            out error))
        //    {
        //        return false;
        //    }
        //    else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CourseManager.Course.CanEdit,
        //           out error))
        //    {
        //        return false;
        //    }*/

        //    //binding object  
        //    objToSave.Code = newObj.Code.Trim();
        //    objToSave.Name = newObj.Name.Trim();
        //    objToSave.Description = newObj.Description;
        //    objToSave.CreditLoad = newObj.CreditLoad;
        //    objToSave.CreditHour = newObj.CreditHour;
        //    objToSave.CourseCategoryEnumId = newObj.CourseCategoryEnumId;
        //    objToSave.IsDeleted = newObj.IsDeleted;
        //    objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
        //    objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
        //    dbAddedObj = objToSave;

        //    //here save any child table
        //    return true;
        //}





    }
    public class ProductJson
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string BatchNo { get; set; }
        public string AttachDocumentPath { get; set; }
        public Decimal TotalPurchasePrice { get; set; }
        public int UnitTypeEnumId { get; set; }
        public Decimal Unit { get; set; }
        public int WarhouseId { get; set; }
    }
    public class PurchaseInformationJson
    {
        public PurchaseInformationJson()
        {
            ProductList = new List<ProductJson>();
        }
        public int Id { get; set; }
        public string PurchaseCode { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public string InvoiceNo { get; set; }
        public List<ProductJson> ProductList { get; set; }
    }
}