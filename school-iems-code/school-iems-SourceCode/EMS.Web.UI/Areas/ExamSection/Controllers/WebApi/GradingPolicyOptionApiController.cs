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
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;


namespace EMS.Web.UI.Areas.ExamSection.Controllers.WebApi
{

	public class GradingPolicyOptionApiController : EmployeeBaseApiController
	{
		public GradingPolicyOptionApiController()
		{  }
	   
		#region GradingPolicyOption 
		#region List View Api
		/// <summary>
		/// 
		/// </summary>
		/// <param name="textkey">text to search</param>
		/// <param name="pageSize"></param>
		/// <param name="pageNo"></param>
		/// <returns></returns>
		public HttpListResult<Aca_GradingPolicyOptionJson> GetPagedGradingPolicyOption(string textkey, int? pageSize, int? pageNo
		   ,Int64 gradingPolicyId= 0      
		 )
		{
			string error = string.Empty;
			int count = 0;
			var result = new HttpListResult<Aca_GradingPolicyOptionJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				using (Aca_GradingPolicyOptionDataService gradingPolicyOptionDataService = new Aca_GradingPolicyOptionDataService(DbInstance, HttpUtil.Profile))
				{
				  IQueryable<Aca_GradingPolicyOption> query = DbInstance.Aca_GradingPolicyOption.OrderByDescending(x => x.Id);
					if (textkey.IsValid())
					{
						query = query.Where(q => q.Grade.ToLower().Contains(textkey.ToLower()));
					}
					if (gradingPolicyId > 0)
					{
						query = query.Where(q => q.GradingPolicyId == gradingPolicyId);
					}

					var entities = gradingPolicyOptionDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
					var jsons = new List<Aca_GradingPolicyOptionJson>();
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
		public HttpListResult<Aca_GradingPolicyOptionJson> GetGradingPolicyOptionListDataExtra()
		{
			string error = string.Empty;
			var result = new HttpListResult<Aca_GradingPolicyOptionJson>();
			try
			{
				//Aca_GradingPolicyOptionDataService gradingPolicyOptionDataService =
				//    new Aca_GradingPolicyOptionDataService(DbInstance, HttpUtil.Profile);
				//DropDown Option Enum List
				
				result.DataExtra.LimitOperatorEnumList = EnumUtil.GetEnumList(typeof(Aca_GradingPolicyOption.LimitOperatorEnum));
				//DropDown Option Tables

				result.DataExtra.GradingPolicyList = DbInstance.Aca_GradingPolicy.AsEnumerable().Select(t => new
				{ Id = t.Id, Name = t.Name }).ToList();
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
		private HttpListResult<Aca_GradingPolicyOptionJson> GetAllGradingPolicyOption()
		{
			string error = string.Empty;
			var result = new HttpListResult<Aca_GradingPolicyOptionJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				using (Aca_GradingPolicyOptionDataService gradingPolicyOptionDataService = new Aca_GradingPolicyOptionDataService(DbInstance, HttpUtil.Profile))
				{
					var jsons = new List<Aca_GradingPolicyOptionJson>();
					var entities = gradingPolicyOptionDataService.GetAll(out error);
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
		/// <summary>
		/// Todo pending to implement bulk save
		/// </summary>
		/// <param name="jsonList"></param>
		/// <returns></returns>
		[HttpPost]
		private HttpListResult<Aca_GradingPolicyOptionJson> SaveGradingPolicyOptionList(IList<Aca_GradingPolicyOptionJson> jsonList)
		{
			string error = string.Empty;
			var result = new HttpListResult<Aca_GradingPolicyOptionJson>();
            if ( !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				using (Aca_GradingPolicyOptionDataService gradingPolicyOptionDataService = new Aca_GradingPolicyOptionDataService(DbInstance, HttpUtil.Profile))
				{
					var entityListReceived = new List<Aca_GradingPolicyOption>();
					var dbAttachedListEntity = new List<Aca_GradingPolicyOption>();
					jsonList.Map(entityListReceived);

					//foreach (var entity in entityListReceived)
					//{

					//}
					//if (gradingPolicyOptionDataService.Save(entity, ref dbAttachedListEntity, out error))
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

		#endregion

		#region Add/Edit View Api
		#region Get
		public HttpResult<Aca_GradingPolicyOptionJson> GetGradingPolicyOptionById(long id = 0)
		{
			string error = string.Empty;
			var result = new HttpResult<Aca_GradingPolicyOptionJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				using (Aca_GradingPolicyOptionDataService gradingPolicyOptionDataService = new Aca_GradingPolicyOptionDataService(DbInstance, HttpUtil.Profile))
				{
					var json = new Aca_GradingPolicyOptionJson();
					Aca_GradingPolicyOption entity;
					if (id <= 0)
					{
						entity = Aca_GradingPolicyOption.GetNew();
					}
					else
					{
						entity = gradingPolicyOptionDataService.GetById(id);
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
		private HttpResult<Aca_GradingPolicyOptionJson> GetGradingPolicyOptionByIdWithChild(long id)
		{
			string error = string.Empty;
			var result = new HttpResult<Aca_GradingPolicyOptionJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				using (Aca_GradingPolicyOptionDataService gradingPolicyOptionDataService = new Aca_GradingPolicyOptionDataService(DbInstance, HttpUtil.Profile))
				{
					var json = new Aca_GradingPolicyOptionJson();
					Aca_GradingPolicyOption entity;
					if (id <= 0)
					{
						entity = Aca_GradingPolicyOption.GetNew();
					}
					else
					{
						List<string> includeTables = new List<string>();
						//Todo Implement latter
						//includeTables.Add("Aca_GradingPolicyOption");
						//includeTables.Add("Aca_GradingPolicyOption");

						entity = gradingPolicyOptionDataService.GetById(id, includeTables.ToArray());
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
		public HttpListResult<Aca_GradingPolicyOptionJson> GetGradingPolicyOptionDataExtra()
		{
			string error = string.Empty;
			var result = new HttpListResult<Aca_GradingPolicyOptionJson>();
			try
			{
				//Aca_GradingPolicyOptionDataService gradingPolicyOptionDataService =
				//new Aca_GradingPolicyOptionDataService(DbInstance, HttpUtil.Profile);
				//DropDown Option Enum List
				
				result.DataExtra.LimitOperatorEnumList = EnumUtil.GetEnumList(typeof(Aca_GradingPolicyOption.LimitOperatorEnum));
				//DropDown Option Tables

				result.DataExtra.GradingPolicyList = DbInstance.Aca_GradingPolicy.AsEnumerable().Select(t => new
				{ Id = t.Id, Name = t.Name }).ToList();
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
		public HttpResult<Aca_GradingPolicyOptionJson> SaveGradingPolicyOption(Aca_GradingPolicyOptionJson json)
		{
			string error = string.Empty;
			var result = new HttpResult<Aca_GradingPolicyOptionJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				 var entityReceived = new Aca_GradingPolicyOption();
				 var dbAttachedEntity = new Aca_GradingPolicyOption();
				json.Map(entityReceived);
				using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
				{
					if (SaveGradingPolicyOptionLogic(entityReceived, ref dbAttachedEntity, out error))
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
		private HttpResult<Aca_GradingPolicyOptionJson> SaveGradingPolicyOption2(Aca_GradingPolicyOptionJson json)
		{
			string error = string.Empty;
			var result = new HttpResult<Aca_GradingPolicyOptionJson>();
			try
			{
				using (Aca_GradingPolicyOptionDataService gradingPolicyOptionDataService = new Aca_GradingPolicyOptionDataService(DbInstance, HttpUtil.Profile))
				{
					var entityReceived = new Aca_GradingPolicyOption();
					var dbAttachedEntity = new Aca_GradingPolicyOption();
					json.Map(entityReceived);
					if (gradingPolicyOptionDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
		public HttpResult<Aca_GradingPolicyOptionJson> GetDeleteGradingPolicyOptionById(long id)
		{
			string error = string.Empty;
			var result = new HttpResult<Aca_GradingPolicyOptionJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanDelete, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
			{
				using (Aca_GradingPolicyOptionDataService gradingPolicyOptionDataService = new Aca_GradingPolicyOptionDataService(DbInstance, HttpUtil.Profile))
				{
					if (!gradingPolicyOptionDataService.DeleteById(id, out error))
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
		#endregion

		#region Local Method (should be always private)
		private bool IsValidToSaveGradingPolicyOption(Aca_GradingPolicyOption newObj, out string error)
		{
			error = "";
			if (!newObj.Grade.IsValid())
			{
				error += "Grade is not valid.";
				return false;
			}
			if (newObj.Grade.Length>50)
			{
				error += "Grade exceeded its maximum length 50.";
				return false;
			}
			//if (newObj.GradePoint==null)
			//{
			//    error += "Grade Point required.";
			//    return false;
			//}
			if (newObj.Description.IsValid() && newObj.Description.Length>256)
			{
				error += "Description exceeded its maximum length 256.";
				return false;
			}
			if (newObj.GradingPolicyId<=0)
			{
				error += "Please select valid Grading Policy.";
				return false;
			}
			//if (newObj.ScoreStartLimit==null)
			//{
			//    error += "Score Start Limit required.";
			//    return false;
			//}

			//if (newObj.LimitOperatorEnumId==null)
			//{
			//    error += "Limit Operator Enum Id required.";
			//    return false;
			//}
			//if (newObj.SerNo==null)
			//{
			//    error += "Ser No required.";
			//    return false;
			//}
			//if (newObj.IsCountCredit==null)
			//{
			//    error += "Is Count Credit required.";
			//    return false;
			//}
			//if (newObj.IsCountGPA==null)
			//{
			//    error += "Is Count G P A required.";
			//    return false;
			//}
			//if (newObj.IsDisplayScore==null)
			//{
			//    error += "Is Display Score required.";
			//    return false;
			//}
			//if (newObj.IsIncomplete==null)
			//{
			//    error += "Is Incomplete required.";
			//    return false;
			//}
			//if (newObj.IsWithdrawn==null)
			//{
			//    error += "Is Withdrawn required.";
			//    return false;
			//}
			//if (newObj.IsContinuation==null)
			//{
			//    error += "Is Continuation required.";
			//    return false;
			//}
			//TODO write your custom validation here.
			//var res =  DbInstance.Aca_GradingPolicyOption.Any(x => x.Id != newObj.Id );
			//if (res)
			//{
				//error = "A GradingPolicyOption already exists!";
				//return false;
			//}
			return true;
		}
		private bool SaveGradingPolicyOptionLogic(Aca_GradingPolicyOption newObj,ref Aca_GradingPolicyOption dbAddedObj, out string error)
		{
			error = string.Empty;
			if (newObj == null)
			{
				error = "Grading Policy Option to save can't be null!";
				return false;
			}

			if ( !IsValidToSaveGradingPolicyOption(newObj, out error))
				return false;

			bool isNewObject = true;
			Aca_GradingPolicyOption objToSave = null;

			if (newObj.Id != 0)
			{
				objToSave = DbInstance.Aca_GradingPolicyOption.SingleOrDefault(x => x.Id == newObj.Id);
				isNewObject = false;
			}
			else
			{    
				newObj.Id = BigInt.NewBigInt();
			}
			if (objToSave == null)
			{   //new object
				isNewObject = true;
				objToSave = Aca_GradingPolicyOption.GetNew(newObj.Id);
				DbInstance.Aca_GradingPolicyOption.Add(objToSave);
			   
			   
			}
			//checking permission, enable it with correction
			if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanAdd,
					out error))
			{
				return false;
			}
			else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanEdit,
				   out error))
			{
				return false;
			}

			//binding object  
			objToSave.Grade =  newObj.Grade ;
		   objToSave.GradePoint =  newObj.GradePoint ;
		   objToSave.Description =  newObj.Description ;
		   objToSave.GradingPolicyId =  newObj.GradingPolicyId ;
		   objToSave.ScoreStartLimit =  newObj.ScoreStartLimit ;
		   objToSave.ScoreEndLimit =  newObj.ScoreEndLimit ;
		   objToSave.LimitOperatorEnumId =  newObj.LimitOperatorEnumId ;
		   objToSave.SerNo =  newObj.SerNo ;
		   objToSave.IsCountCredit =  newObj.IsCountCredit ;
		   objToSave.IsCountGPA =  newObj.IsCountGPA ;
		   objToSave.IsDisplayScore =  newObj.IsDisplayScore ;
		   objToSave.IsIncomplete =  newObj.IsIncomplete ;
		   objToSave.IsWithdrawn =  newObj.IsWithdrawn ;
		   objToSave.IsContinuation =  newObj.IsContinuation ;
		   
		   
		   dbAddedObj = objToSave;
		   
			//here save any child table
			return true;
		}
		#endregion
		#endregion

	}
}
