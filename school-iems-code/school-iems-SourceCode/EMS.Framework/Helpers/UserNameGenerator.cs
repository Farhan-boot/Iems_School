using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.Framework.Helpers
{
    public class UserNameGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yearPrefix"></param>
        /// <param name="middleNumber"></param>
        /// <param name="studentQuotaSuffix"></param>
        /// <returns></returns>
        public static string GenerateStudentUserName(DateTime yearPrefix, int middleNumber, HR_Department studentDepartment)
        {
            int suffix = studentDepartment.DepartmentNo;
            //switch (studentQuotaSuffix)
            //{
            //    case User_Student.StudentQuotaEnum.General:
            //    case User_Student.StudentQuotaEnum.ChildrenOfFreedomFighters:
            //    case User_Student.StudentQuotaEnum.ChildrenOfMilitaryPersonnel:
            //    case User_Student.StudentQuotaEnum.TribalCitizen:
            //        suffix = (int)EnumCollection.StudentUserNameSuffixEnum.CivilStudent;//civil
            //        break;
            //    case User_Student.StudentQuotaEnum.MilitaryOfficer:
            //        suffix = (int)EnumCollection.StudentUserNameSuffixEnum.MilitaryOfficerStudent;//military
            //        break;
            //    case User_Student.StudentQuotaEnum.MilitaryCadate:
            //        suffix = (int)EnumCollection.StudentUserNameSuffixEnum.BMA_Cadate;//military
            //        break;
            //    case User_Student.StudentQuotaEnum.ForeignStudent:
            //        suffix = (int)EnumCollection.StudentUserNameSuffixEnum.ForeignStudent;//military
            //        break;
            //    default:
            //        suffix = (int)EnumCollection.StudentUserNameSuffixEnum.CivilStudent;
            //        break;
            //}

            var user = yearPrefix.ToString("yy") + "-" + middleNumber.ToStringPadLeft(4, '0') + "-" + suffix;
            return user;
        }

        //private string GenerateUsername(User_Account userAccount)
        //{
        //    var result = string.Empty;
        //    var userEmployee = DbInstance.User_Employee.SingleOrDefault(x => x.UserId == userAccount.Id);
        //    if (userEmployee != null)
        //    {
        //        result += userEmployee.JoiningDate.ToString("yy");//Joining Date Year 2 digit added
        //        result += userAccount.CategoryEnumId + "-";//User category 1=Army, 2=Navy, 3=AirForce, 4=Civil
        //        //TODO SN
        //        result += DbInstance.User_Employee.Count().ToStringPadLeft(4, '0') + "-";//Serial Number
        //        result += userEmployee.JobClassEnumId;//Job Class 1st,2nd,3rd,4th
        //    }

        //    return result;
        //}

        /// <summary>
        /// xxx-xxxx-x
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        /// /TODO copied from employee username generate. Implement for Student here.
        //public bool GenerateStudentUserName(User_Account userAccount, out string result)
        //{
        //    result = string.Empty;

        //    if (userAccount == null)
        //    {
        //        result = "User can't be null!";
        //        return false;
        //    }
        //    try
        //    {
        //        var userEmployee = DbInstance.User_Employee.Include(x=>x.User_Account).SingleOrDefault(x => x.UserId == userAccount.Id);
        //        if (userEmployee == null)
        //        {
        //            result = "No employee found in employee database!";
        //            return false;
        //        }
        //        var joiningYear = userEmployee.User_Account.JoiningDate.ToString("yy");//Joining Date Year 2 digit added
        //        var joiningMonth = userEmployee.User_Account.JoiningDate.ToString("MM"); ;//User category 1=Army, 2=Navy, 3=AirForce, 4=Civil
        //        var empClass = userEmployee.JobClassEnumId;//Job Class 1st,2nd,3rd,4th

        //        var userNameLike = $"%{joiningMonth}-%-{empClass}"; //%4-%-1
        //        var query = $"SELECT * FROM [User_Account] WHERE [UserName] LIKE '{userNameLike}' and id!={userAccount.Id} ORDER BY [Id] DESC";

        //        var lastEmp = DbInstance.User_Account
        //             .SqlQuery(query)
        //            .FirstOrDefault(); //.ToList<User_Account>()

        //        if (lastEmp == null)
        //        {
        //            result = $"{joiningYear}{joiningMonth}-0001-{empClass}";
        //            return true;
        //        }
        //        string serial = lastEmp.UserName.Split('-')[1];
        //        int serialNo = int.Parse(serial) + 1;
        //        //making more unique
        //        for (int i = 0; i < 100; i++)
        //        {
        //            result = $"{joiningYear}{joiningMonth}-{serialNo.ToStringPadLeft(4, '0')}-{empClass}";
        //            string s = result;
        //            if (DbInstance.User_Account.Any(x => x.UserName.Equals(s, StringComparison.OrdinalIgnoreCase) && x.Id != userAccount.Id))
        //            {
        //                serialNo++;//making unique
        //            }
        //            else
        //            {
        //                break;//break when no user found in db
        //            }
        //        }

        //    }
        //    catch (Exception exception)
        //    {
        //        result = exception.ToString();
        //        return false;
        //    }
        //    return true;
        //}

        //private string GenerateUsername(User_Account userAccount)
        //{
        //    var result = string.Empty;
        //    var userEmployee = DbInstance.User_Employee.SingleOrDefault(x => x.UserId == userAccount.Id);
        //    if (userEmployee != null)
        //    {
        //        result += userEmployee.JoiningDate.ToString("yy");//Joining Date Year 2 digit added
        //        result += userAccount.CategoryEnumId + "-";//User category 1=Army, 2=Navy, 3=AirForce, 4=Civil
        //        //TODO SN
        //        result += DbInstance.User_Employee.Count().ToStringPadLeft(4, '0') + "-";//Serial Number
        //        result += userEmployee.JobClassEnumId;//Job Class 1st,2nd,3rd,4th
        //    }
        //    return result;
        //}

        /// <summary>
        /// YYMM01-001
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool GenerateEmpUserName(DateTime joiningDate, int employeeCategoryEnumId, EmsDbContext DbInstance, out string result)
        {
            result = string.Empty;

            if (DbInstance == null)
            {
                result = "User can't be null!";
                return false;
            }
            try
            {

                var joiningYear = joiningDate.ToString("yy");//Joining Date Year 2 digit added
                var joiningMonth = joiningDate.ToString("MM");
                var empCategory = employeeCategoryEnumId; //Admin/Faculty/Supporting //12031-003

                var userNameLike = $"{joiningYear}%{empCategory}-%"; //%4-%-1
                var query = $"SELECT * FROM [User_Account] WHERE  UserTypeEnumId={(byte)User_Account.UserTypeEnum.Employee} and [UserName] LIKE '{userNameLike}'";

                var lastEmp = DbInstance
                    .User_Account
                    .SqlQuery(query)
                    .FirstOrDefault(); //.ToList<User_Account>()

                if (lastEmp == null)
                {
                    result = $"{joiningYear}{joiningMonth}{empCategory}-001";
                    return true;
                }
                string serial = lastEmp.UserName.Split('-')[1];
                int serialNo = int.Parse(serial) + 1;
                //making more unique
                for (int i = 0; i < 100; i++)
                {
                    result = $"{joiningYear}{joiningMonth}{empCategory}-{serialNo.ToStringPadLeft(3, '0')}";
                    string s = result;
                    if (DbInstance.User_Account.Any(x => x.UserName.Equals(s, StringComparison.OrdinalIgnoreCase)))
                    {
                        serialNo++;//making unique
                    }
                    else
                    {
                        break;//break when no user found in db
                    }
                }

            }
            catch (Exception exception)
            {
                result = exception.ToString();
                return false;
            }
            return true;
        }


    }
}
