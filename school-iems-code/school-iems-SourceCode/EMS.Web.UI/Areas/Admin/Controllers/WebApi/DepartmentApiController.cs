//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.CoreLibrary.Helpers;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;


namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{

	public class DepartmentApiController : EmployeeBaseApiController
	{
		public DepartmentApiController()
		{  }

		#region Department 
		#region Get Api
		/// <summary>
		/// 
		/// </summary>
		/// <param name="textkey">text to search</param>
		/// <param name="pageSize"></param>
		/// <param name="pageNo"></param>
		/// <returns></returns>
		public HttpListResult<HR_DepartmentJson> GetPagedDepartment(string textkey, int? pageSize, int? pageNo
		   , Int64 orgId = 0
            , int TypeEnumId=-1
             , int StatusEnumId = -1
         )
		{
			string error = string.Empty;
			int count = 0;
			var result = new HttpListResult<HR_DepartmentJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
			{
				using (HR_DepartmentDataService departmentDataService = new HR_DepartmentDataService(DbInstance, HttpUtil.Profile))
				{
					IQueryable<HR_Department> query = DbInstance.HR_Department.OrderBy(x => x.Id);
					if (textkey.IsValid())
					{
						query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
					}
					if (orgId > 0)
					{
						query = query.Where(q => q.OrgId == orgId);
					}
                    if (TypeEnumId > -1)
					{
						query = query.Where(q => q.TypeEnumId == TypeEnumId);
					}
                    if (StatusEnumId > -1)
					{
						query = query.Where(q => q.StatusEnumId == StatusEnumId);
					}

					var entities = departmentDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
					var jsons = new List<HR_DepartmentJson>();
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
		private HttpListResult<HR_DepartmentJson> GetAllDepartment()
		{
			string error = string.Empty;
			var result = new HttpListResult<HR_DepartmentJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				using (HR_DepartmentDataService departmentDataService = new HR_DepartmentDataService(DbInstance, HttpUtil.Profile))
				{
					var jsons = new List<HR_DepartmentJson>();
					var entities = departmentDataService.GetAll(out error);
					entities.Map(jsons);
					result.Data = jsons;
					result.Count = jsons.Count;
				}
			}
			catch (Exception ex)
			{
				result.HasError = true;
				result.Errors.Add(ex.GetBaseException().Message.ToString());
			}
			return result;
		}
		public HttpResult<HR_DepartmentJson> GetDepartmentById(Int32 id = 0)
		{
			string error = string.Empty;
			var result = new HttpResult<HR_DepartmentJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				using (HR_DepartmentDataService departmentDataService = new HR_DepartmentDataService(DbInstance, HttpUtil.Profile))
				{
					var json = new HR_DepartmentJson();
					HR_Department entity;
					if (id <= 0)
					{
						entity = HR_Department.GetNew();
					}
					else
					{
						entity = departmentDataService.GetById(id);
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
		private HttpResult<HR_DepartmentJson> GetDepartmentByIdWithChild(Int32 id)
		{
			string error = string.Empty;
			var result = new HttpResult<HR_DepartmentJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				using (HR_DepartmentDataService departmentDataService = new HR_DepartmentDataService(DbInstance, HttpUtil.Profile))
				{
					var json = new HR_DepartmentJson();
					HR_Department entity;
					if (id <= 0)
					{
						entity = HR_Department.GetNew();
					}
					else
					{
						List<string> includeTables = new List<string>();
						//Todo Implement latter
						//includeTables.Add("HR_Department");
						//includeTables.Add("HR_Department");

						entity = departmentDataService.GetById(id, includeTables.ToArray());
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
		public HttpListResult<HR_DepartmentJson> GetDepartmentListDataExtra()
		{
			string error = string.Empty;
			var result = new HttpListResult<HR_DepartmentJson>();
			try
			{
				//HR_DepartmentDataService departmentDataService =
				//    new HR_DepartmentDataService(DbInstance, HttpUtil.Profile);
				//DropDown Option Enum List

				result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(HR_Department.TypeEnum));
				result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(HR_Department.StatusEnum));
                //DropDown Option Tables

                result.DataExtra.OrganizationList = DbInstance.HR_Organization.AsEnumerable().Select(t => new
                { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
			catch (Exception ex)
			{
				result.HasError = true;
				result.Errors.Add(ex.GetBaseException().Message.ToString());
			}
			return result;
		}
		public HttpListResult<HR_DepartmentJson> GetDepartmentDataExtra()
		{
			string error = string.Empty;
			var result = new HttpListResult<HR_DepartmentJson>();
			try
			{
				//HR_DepartmentDataService departmentDataService =
				//    new HR_DepartmentDataService(DbInstance, HttpUtil.Profile);
				//DropDown Option Enum List

				result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(HR_Department.TypeEnum));
				result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(HR_Department.StatusEnum));
                //DropDown Option Tables

                result.DataExtra.OrganizationList = DbInstance.HR_Organization.AsEnumerable().Select(t => new
                { Id = t.Id.ToString(), Name = t.Name }).ToList();
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
		public HttpResult<HR_DepartmentJson> SaveDepartment(HR_DepartmentJson json)
		{
			string error = string.Empty;
			var result = new HttpResult<HR_DepartmentJson>();
            //optional permission, check permission in the SaveDepartmentLogic insted
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				var entityReceived = new HR_Department();
				var dbAttachedEntity = new HR_Department();
				json.Map(entityReceived);
				using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
				{
					if (SaveDepartmentLogic(entityReceived, ref dbAttachedEntity, out error))
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
		private HttpResult<HR_DepartmentJson> SaveDepartment2(HR_DepartmentJson json)
		{
			string error = string.Empty;
			var result = new HttpResult<HR_DepartmentJson>();
			try
			{
				using (HR_DepartmentDataService departmentDataService = new HR_DepartmentDataService(DbInstance, HttpUtil.Profile))
				{
					var entityReceived = new HR_Department();
					var dbAttachedEntity = new HR_Department();
					json.Map(entityReceived);
					if (departmentDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
		private HttpListResult<HR_DepartmentJson> SaveDepartmentList(IList<HR_DepartmentJson> jsonList)
		{
			string error = string.Empty;
			var result = new HttpListResult<HR_DepartmentJson>();
            //todo enable it while you need the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				using (HR_DepartmentDataService departmentDataService = new HR_DepartmentDataService(DbInstance, HttpUtil.Profile))
				{
					var entityListReceived = new List<HR_Department>();
					var dbAttachedListEntity = new List<HR_Department>();
					jsonList.Map(entityListReceived);

					//foreach (var entity in entityListReceived)
					//{

					//}
					//if (departmentDataService.Save(entity, ref dbAttachedListEntity, out error))
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
		public HttpResult<HR_DepartmentJson> GetDeleteDepartmentById(Int32 id)
		{
			string error = string.Empty;
			var result = new HttpResult<HR_DepartmentJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				using (HR_DepartmentDataService departmentDataService = new HR_DepartmentDataService(DbInstance, HttpUtil.Profile))
				{
					if (!departmentDataService.DeleteById(id, out error))
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
		private bool IsValidToSaveDepartment(HR_Department newObj, out string error)
		{
			error = "";
			if (!newObj.Name.IsValid())
			{
				error += "Name is not valid.";
				return false;
			}
			if (newObj.Name.Length > 150)
			{
				error += "Name exceeded its maximum length 150.";
				return false;
			}
			if (!newObj.ShortName.IsValid())
			{
				error += "Short Name is not valid.";
				return false;
			}
			if (newObj.ShortName.Length > 50)
			{
				error += "Short Name exceeded its maximum length 50.";
				return false;
			}
			if (newObj.DepartmentNo == null)
			{
				error += "Department No required.";
				return false;
			}
			if (newObj.ParentDeptId == null)
			{
				error += "Parent Dept Id required.";
				return false;
			}
			if (!newObj.Description.IsValid())
			{
				error += "Description is not valid.";
				return false;
			}
			if (newObj.Description.Length > 256)
			{
				error += "Description exceeded its maximum length 256.";
				return false;
			}
			if (!newObj.Address.IsValid())
			{
				error += "Address is not valid.";
				return false;
			}
			if (newObj.Address.Length > 256)
			{
				error += "Address exceeded its maximum length 256.";
				return false;
			}
			if (!newObj.MapEmbedCode.IsValid())
			{
				error += "Map Embed Code is not valid.";
				return false;
			}
			if (newObj.MapEmbedCode == null)
			{
				error += "Map Embed Code required.";
				return false;
			}
			if (!newObj.Email.IsValid())
			{
				error += "Email is not valid.";
				return false;
			}
			if (newObj.Email.Length > 50)
			{
				error += "Email exceeded its maximum length 50.";
				return false;
			}
			if (!newObj.Mobile.IsValid())
			{
				error += "Mobile is not valid.";
				return false;
			}
			if (newObj.Mobile.Length > 20)
			{
				error += "Mobile exceeded its maximum length 20.";
				return false;
			}
			if (!newObj.Phone.IsValid())
			{
				error += "Phone is not valid.";
				return false;
			}
			if (newObj.Phone.Length > 20)
			{
				error += "Phone exceeded its maximum length 20.";
				return false;
			}
			if (!newObj.Fax.IsValid())
			{
				error += "Fax is not valid.";
				return false;
			}
			if (newObj.Fax.Length > 20)
			{
				error += "Fax exceeded its maximum length 20.";
				return false;
			}
			if (!newObj.Website.IsValid())
			{
				error += "Website is not valid.";
				return false;
			}
			if (newObj.Website.Length > 50)
			{
				error += "Website exceeded its maximum length 50.";
				return false;
			}
			if (!newObj.Founded.IsValid())
			{
				error += "Founded is not valid.";
				return false;
			}
			if (newObj.TypeEnumId == null)
			{
				error += "Please select valid Type.";
				return false;
			}
			if (newObj.StatusEnumId == null)
			{
				error += "Please select valid Status.";
				return false;
			}
			if (newObj.Priority == null)
			{
				error += "Priority required.";
				return false;
			}
			if (newObj.OrgId <= 0)
			{
			    newObj.OrgId = 1;
			    //error += "Please select valid Org.";
			    //return false;
			}
			if (newObj.IsDeleted == null)
			{
				error += "Is Deleted required.";
				return false;
			}
			//TODO write your custom validation here.
			//var res =  DbInstance.HR_Department.Any(x => x.Id != newObj.Id );
			//if (res)
			//{
			//error = "A Department already exists!";
			//return false;
			//}
			return true;
		}
		private bool SaveDepartmentLogic(HR_Department newObj, ref HR_Department dbAddedObj, out string error)
		{
			error = string.Empty;
			if (newObj == null)
			{
				error = "Department to save can't be null!";
				return false;
			}

			if (!IsValidToSaveDepartment(newObj, out error))
				return false;

			bool isNewObject = true;
			HR_Department objToSave = null;

			if (newObj.Id != 0)
			{
				objToSave = DbInstance.HR_Department.SingleOrDefault(x => x.Id == newObj.Id);
				isNewObject = false;
			}
			if (objToSave == null)
			{   //new object
				isNewObject = true;
				objToSave = HR_Department.GetNew(newObj.Id);
				DbInstance.HR_Department.Add(objToSave);
				objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
				objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
			}
            //todo mandatory fix checking permission, enable it with correction
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Department.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name = newObj.Name;
			objToSave.ShortName = newObj.ShortName;
			objToSave.DepartmentNo = newObj.DepartmentNo;
			objToSave.ParentDeptId = newObj.ParentDeptId;
			objToSave.Description = newObj.Description;
			objToSave.Address = newObj.Address;
			objToSave.MapEmbedCode = newObj.MapEmbedCode;
			objToSave.Email = newObj.Email;
			objToSave.Mobile = newObj.Mobile;
			objToSave.Phone = newObj.Phone;
			objToSave.Fax = newObj.Fax;
			objToSave.Website = newObj.Website;
			objToSave.Founded = newObj.Founded;
			objToSave.TypeEnumId = newObj.TypeEnumId;
			objToSave.StatusEnumId = newObj.StatusEnumId;
			objToSave.Priority = newObj.Priority;
			objToSave.OrgId = newObj.OrgId;
			objToSave.IsDeleted = newObj.IsDeleted;
			objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
			objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
			dbAddedObj = objToSave;

			//here save any child table
			return true;
		}
		#endregion
		#endregion

	}
}
