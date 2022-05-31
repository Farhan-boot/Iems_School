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
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;


namespace EMS.Web.UI.Areas.Employee.Controllers.WebApi
{

	public class ClassNoticeApiController : EmployeeBaseApiController
	{
		public ClassNoticeApiController()
		{  }
	   
		#region ClassNotice 
		#region Get Api
		/// <summary>
		/// 
		/// </summary>
		/// <param name="textkey">text to search</param>
		/// <param name="pageSize"></param>
		/// <param name="pageNo"></param>
		/// <returns></returns>
		public HttpListResult<Aca_ClassNoticeJson> GetPagedClassNotice(string textkey, int? pageSize, int? pageNo
		   ,Int64 classId= 0      
		   ,Int64 employeeId= 0      
		 )
		{
			string error = string.Empty;
			int count = 0;
			var result = new HttpListResult<Aca_ClassNoticeJson>();
			try
			{
				using (Aca_ClassNoticeDataService classNoticeDataService = new Aca_ClassNoticeDataService(DbInstance, HttpUtil.Profile))
				{
				  IQueryable<Aca_ClassNotice> query = DbInstance.Aca_ClassNotice
					.Include(x => x.User_Employee)
					.Include(x => x.User_Employee.User_Account)
					.OrderByDescending(x => x.PostDate);
					if (textkey.IsValid())
					{
						query = query.Where(q => q.Subject.ToLower().Contains(textkey.ToLower()));
					}
					if (classId>0)
					{
						query = query.Where(q => q.ClassId== classId);
					}  
					if (employeeId>0)
					{
						query = query.Where(q => q.EmployeeId== employeeId);
					}  
				 
					var entities = classNoticeDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
					var jsons = new List<Aca_ClassNoticeJson>();
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
		private HttpListResult<Aca_ClassNoticeJson> GetAllClassNotice()
		{
			string error = string.Empty;
			var result = new HttpListResult<Aca_ClassNoticeJson>();
			try
			{
				using (Aca_ClassNoticeDataService classNoticeDataService = new Aca_ClassNoticeDataService(DbInstance, HttpUtil.Profile))
				{
					var jsons = new List<Aca_ClassNoticeJson>();
					var entities = classNoticeDataService.GetAll(out error);
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
		public HttpResult<Aca_ClassNoticeJson> GetClassNoticeById(long id = 0)
		{
			string error = string.Empty;
			var result = new HttpResult<Aca_ClassNoticeJson>();
			try
			{
				using (Aca_ClassNoticeDataService classNoticeDataService = new Aca_ClassNoticeDataService(DbInstance, HttpUtil.Profile))
				{
					var json = new Aca_ClassNoticeJson();
					Aca_ClassNotice entity;
					if (id <= 0)
					{
						entity = Aca_ClassNotice.GetNew();
						entity.EmployeeId = HttpUtil.Profile.EmployeeId;
					}
					else
					{
						entity = classNoticeDataService.GetById(id);
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
		private HttpResult<Aca_ClassNoticeJson> GetClassNoticeByIdWithChild(long id)
		{
			string error = string.Empty;
			var result = new HttpResult<Aca_ClassNoticeJson>();
			try
			{
				using (Aca_ClassNoticeDataService classNoticeDataService = new Aca_ClassNoticeDataService(DbInstance, HttpUtil.Profile))
				{
					var json = new Aca_ClassNoticeJson();
					Aca_ClassNotice entity;
					if (id <= 0)
					{
						entity = Aca_ClassNotice.GetNew();
					}
					else
					{
						List<string> includeTables = new List<string>();
						//Todo Implement latter
						//includeTables.Add("Aca_ClassNotice");
						//includeTables.Add("Aca_ClassNotice");

						entity = classNoticeDataService.GetById(id, includeTables.ToArray());
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
		public HttpListResult<Aca_ClassNoticeJson> GetClassNoticeListDataExtra()
		{
			string error = string.Empty;
			var result = new HttpListResult<Aca_ClassNoticeJson>();
			try
			{
				//Aca_ClassNoticeDataService classNoticeDataService =
				//    new Aca_ClassNoticeDataService(DbInstance, HttpUtil.Profile);
				//DropDown Option Enum List
				
				//DropDown Option Tables
				 
				//result.DataExtra.ClassList = DbInstance.Aca_Class.AsEnumerable().Select(t => new
				//{ Id = t.Id.ToString(),Name = t.Name }).ToList();
				//result.DataExtra.EmployeeList = DbInstance.User_Employee.AsEnumerable().Select(t => new
				//{ Id = t.Id.ToString(),Name = t.Name }).ToList();
			}
			catch (Exception ex)
			{
				result.HasError = true;
				result.Errors.Add(ex.GetBaseException().Message.ToString());
			}
			return result;
		}
		public HttpListResult<Aca_ClassNoticeJson> GetClassNoticeDataExtra()
		{
			string error = string.Empty;
			var result = new HttpListResult<Aca_ClassNoticeJson>();
			try
			{
				//Aca_ClassNoticeDataService classNoticeDataService =
				//    new Aca_ClassNoticeDataService(DbInstance, HttpUtil.Profile);
				//DropDown Option Enum List
				
				//DropDown Option Tables
				 
				//result.DataExtra.ClassList = DbInstance.Aca_Class.AsEnumerable().Select(t => new
				//{ Id = t.Id.ToString(),Name = t.Name }).ToList();
				//result.DataExtra.EmployeeList = DbInstance.User_Employee.AsEnumerable().Select(t => new
				//{ Id = t.Id.ToString(),Name = t.Name }).ToList();
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
		public HttpResult<Aca_ClassNoticeJson> SaveClassNotice(Aca_ClassNoticeJson json)
		{
			string error = string.Empty;
			var result = new HttpResult<Aca_ClassNoticeJson>();
			if (json==null || json.ClassId.Equals("0"))
			{
				error = "Invalid Class selected Save Class Notice!";
				result.HasError = true;

				result.Errors.Add(error);
				return result;
			}
			try
			{

				var entityReceived = new Aca_ClassNotice();
			
				json.Map(entityReceived);
				if (!IsValidToSaveClassNotice(entityReceived, out error))
				{
					error = "Invalid Class selected Save Class Notice!";
					result.HasError = true;

					result.Errors.Add(error);
					return result;
				}
				var res = DbInstance.Aca_ClassTakenByEmployee
					.Any(x => x.ClassId == entityReceived.ClassId 
                              && !x.IsDeleted
                              && x.EmployeeId == HttpUtil.Profile.EmployeeId && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty);
				if (!res)
				{
					error = "You don't have permission to Save Class Notice, you are not faculty of this Class!";
					result.HasError = true;
					result.Errors.Add(error);
					return result;
				}

				Aca_ClassNotice objToSave = null;

				if (entityReceived.Id != 0)
				{
					objToSave = DbInstance.Aca_ClassNotice.SingleOrDefault(x => x.Id == entityReceived.Id);
				}
				else
				{
					entityReceived.Id = BigInt.NewBigInt();
				}
				if (objToSave == null)
				{   //new object
					objToSave = Aca_ClassNotice.GetNew(entityReceived.Id);
					DbInstance.Aca_ClassNotice.Add(objToSave);
					objToSave.PostDate = DateTime.Now;
					objToSave.EmployeeId = HttpUtil.Profile.EmployeeId;
					objToSave.CreateById = HttpUtil.Profile.UserId;
					objToSave.CreateDate = DateTime.Now;
				}
				else
				{
					if (objToSave.EmployeeId != HttpUtil.Profile.EmployeeId)
					{
						error = "You don't have permission to Edit this Class Notice, you are not owner of this Class Notice!";
						result.HasError = true;

						result.Errors.Add(error);
						return result;
					}
				}
				//binding object  
				objToSave.Subject = entityReceived.Subject;
				objToSave.Description = entityReceived.Description;
				//objToSave.PostDate =  newObj.PostDate ;
				objToSave.ClassId = entityReceived.ClassId;
				//objToSave.EmployeeId =  newObj.EmployeeId ;
				objToSave.LastChangedById = HttpUtil.Profile.UserId;
				objToSave.LastChanged = DateTime.Now;
				DbInstance.SaveChanges();

				objToSave.Map(json);
				result.Data = json; //return object
				return result;

			}

			catch (Exception ex)
			{
				result.HasError = true;
				result.Errors.Add(ex.GetBaseException().Message.ToString());
				return result;
			}
		  
		}
		
		/*[HttpPost]
		private HttpResult<Aca_ClassNoticeJson> SaveClassNotice2(Aca_ClassNoticeJson json)
		{
			string error = string.Empty;
			var result = new HttpResult<Aca_ClassNoticeJson>();
			try
			{
				using (Aca_ClassNoticeDataService classNoticeDataService = new Aca_ClassNoticeDataService(DbInstance, HttpUtil.Profile))
				{
					var entityReceived = new Aca_ClassNotice();
					var dbAttachedEntity = new Aca_ClassNotice();
					json.Map(entityReceived);
					if (classNoticeDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
		private HttpListResult<Aca_ClassNoticeJson> SaveClassNoticeList(IList<Aca_ClassNoticeJson> jsonList)
		{
			string error = string.Empty;
			var result = new HttpListResult<Aca_ClassNoticeJson>();
			try
			{
				using (Aca_ClassNoticeDataService classNoticeDataService = new Aca_ClassNoticeDataService(DbInstance, HttpUtil.Profile))
				{
					var entityListReceived = new List<Aca_ClassNotice>();
					var dbAttachedListEntity = new List<Aca_ClassNotice>();
					jsonList.Map(entityListReceived);

					//foreach (var entity in entityListReceived)
					//{

					//}
					//if (classNoticeDataService.Save(entity, ref dbAttachedListEntity, out error))
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
		public HttpResult<Aca_ClassNoticeJson> GetDeleteClassNoticeById(long id)
		{
			string error = string.Empty;
			var result = new HttpResult<Aca_ClassNoticeJson>();
			if (id <= 0)
			{
				error = "Invalid Class Notice selected to Delete!";
				result.HasError = true;
				result.Errors.Add(error);
				return result;
			}
			try
			{
				var classNotice = DbInstance.Aca_ClassNotice.Find( id);
				if (classNotice == null)
				{
					error = "Invalid Class Notice selected for Delete! Please reload the page and try again.";
					result.HasError = true;
					result.Errors.Add(error);
					return result;
				}
				if (classNotice.EmployeeId != HttpUtil.Profile.EmployeeId)
				{
					error = "You don't have permission to Delete this Class Notice of this class, you are not owner of this Class Notice!";
					result.HasError = true;

					result.Errors.Add(error);
					return result;
				}

				var res = DbInstance.Aca_ClassTakenByEmployee
					.Any(x => x.ClassId == classNotice.ClassId
                              && !x.IsDeleted
                              && x.EmployeeId == HttpUtil.Profile.EmployeeId && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty);
				if (!res)
				{
					error = "You don't have permission to Delete this Class Notice of this class, you are not faculty of this Class!";
					result.HasError = true;

					result.Errors.Add(error);
					return result;
				}

				using (Aca_ClassNoticeDataService classNoticeDataService = new Aca_ClassNoticeDataService(DbInstance, HttpUtil.Profile))
				{
					if (!classNoticeDataService.DeleteById(id, out error))
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
		private bool IsValidToSaveClassNotice(Aca_ClassNotice newObj, out string error)
		{
			error = "";
			if (newObj == null )
			{
				error = "Invalid Class selected Save Class Notice!";
				return false;
			}
			if (!newObj.Subject.IsValid())
			{
				error += "Subject is not valid.";
				return false;
			}
			if (newObj.Subject.Length>500)
			{
				error += "Subject exceeded its maximum length 500.";
				return false;
			}
			if (!newObj.Description.IsValid())
			{
				error += "Description is not valid.";
				return false;
			}
			if (newObj.Description==null)
			{
				error += "Description required.";
				return false;
			}
			if (!newObj.PostDate.IsValid())
			{
				newObj.PostDate = DateTime.Now;
			}
			if (newObj.ClassId<=0)
			{
				error += "Please select valid Class.";
				return false;
			}
			if (newObj.EmployeeId<=0)
			{
				newObj.EmployeeId = HttpUtil.Profile.EmployeeId;
			}
			else if (newObj.EmployeeId!= HttpUtil.Profile.EmployeeId)
			{
				error += "You can update you Notice only. You can't update others Notices!";
				return false;
			}
			//TODO write your custom validation here.
			//var res =  DbInstance.Aca_ClassNotice.Any(x => x.Id != newObj.Id );
			//if (res)
			//{
				//error = "A ClassNotice already exists!";
				//return false;
			//}
			return true;
		}
		private bool SaveClassNoticeLogic(Aca_ClassNotice newObj,ref Aca_ClassNotice dbAddedObj, out string error)
		{
			error = string.Empty;
			if (newObj == null)
			{
				error = "Class Notice to save can't be null!";
				return false;
			}

			if ( !IsValidToSaveClassNotice(newObj, out error))
				return false;

			bool isNewObject = true;
			Aca_ClassNotice objToSave = null;

			if (newObj.Id != 0)
			{
				objToSave = DbInstance.Aca_ClassNotice.SingleOrDefault(x => x.Id == newObj.Id);
				isNewObject = false;
			}
			else
			{    
				newObj.Id = BigInt.NewBigInt();
			}
			if (objToSave == null)
			{   //new object
				isNewObject = true;
				objToSave = Aca_ClassNotice.GetNew(newObj.Id);
				DbInstance.Aca_ClassNotice.Add(objToSave);
				objToSave.PostDate = DateTime.Now;
				objToSave.EmployeeId = HttpUtil.Profile.EmployeeId;
				objToSave.CreateById = HttpUtil.Profile.UserId;
				objToSave.CreateDate = DateTime.Now;
			}

			//binding object  
		   objToSave.Subject =  newObj.Subject ;
		   objToSave.Description =  newObj.Description ;
		   //objToSave.PostDate =  newObj.PostDate ;
		   objToSave.ClassId =  newObj.ClassId ;
		   //objToSave.EmployeeId =  newObj.EmployeeId ;
		   objToSave.LastChangedById = HttpUtil.Profile.UserId;
		   objToSave.LastChanged = DateTime.Now;
		   dbAddedObj = objToSave;
		   
			//here save any child table
			return true;
		}
		#endregion
		#endregion

	}
}
