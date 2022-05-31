using System;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;

namespace EMS.Web.Jsons.Models
{
	public partial class User_EmployeeJson
	{
        #region Custom Variables
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Category { get; set; }

        public bool IsApproved { get; set; }
        public string DeptName { get; set; }
        public string DeptShortName { get; set; }
        public string JoiningDateString { get; set; }
        public string LeavingDateString { get; set; }
        public string Password { get; set; }//todo temp need To remove
        public string PositionName { get; set; }
        public string PositionShortName { get; set; }

        public string RankName { get; set; }//todo removae
        public string RankCategory { get; set; }
        public string RankJobClass { get; set; }
  

        #endregion
    }
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
    {
        private static void MapExtra(User_Employee entity, User_EmployeeJson toJson)
        {
         
            if (entity.User_Account != null)
            {
                toJson.UserName = entity.User_Account.UserName;
                toJson.FullName = entity.User_Account.FullName;

                if (!entity.User_Account.PasswordSalt.IsValid())
                {
                    toJson.Password = entity.User_Account.Password;
                }

                if (!HttpUtil.IsSupperAdmin() && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanResetPassword))
                {
                    toJson.Password =String.Empty;
                }
                
                //toJson.Category = entity.User_Account.Category.ToString().AddSpacesToSentence();
                toJson.IsApproved = entity.User_Account.IsApproved;
                toJson.IsSelected = entity.IsSelected;
                toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
                toJson.JoiningDateString = entity.User_Account.JoiningDate.ToString(DateFormat);
                toJson.LeavingDateString = entity.User_Account.LeavingDate != null ? Convert.ToDateTime(entity.User_Account.LeavingDate).ToString(DateFormat) : entity.User_Account.LeavingDate.ToString();
                if (entity.HR_Position != null)
                {
                    toJson.PositionName = entity.HR_Position.Name;
                    toJson.PositionShortName = entity.HR_Position.ShortName; //entity.User_Account.User_Rank.Category.ToString().AddSpacesToSentence();
                    //toJson.RankJobClass = entity.User_Account.User_Rank.JobClass.ToString().AddSpacesToSentence();
                }

                if (entity.User_Account.HR_Department != null)
                {
                    toJson.DeptName = entity.User_Account.HR_Department.Name;
                    toJson.DeptShortName = entity.User_Account.HR_Department.ShortName;
                }
            }
            //if (entity.HR_Department != null)
            //{
            //    toJson.DeptName = entity.HR_Department.Name;
            //    toJson.DeptShortName = entity.HR_Department.ShortName;
            //}
        }

        private static void MapExtra(User_EmployeeJson json, User_Employee toEntity)
        {
        
        }
    }
}
