using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Framework.Utils;

namespace EMS.Web.UI.Areas.Admin.Models
{
    public class UserModel
    {
        public long UserId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string MobileNo { get; set; }
        public string DateOfBirth { get; set; }
        public int GenderId { get; set; }
        public string ProfileImageLocation { get; set; }
        public int UserStatusId { get; set; }
        public int UserTypeId { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        public string LastLoginDate { get; set; }

        public string LastPasswordChangedDate { get; set; }

        public string LastLockoutDate { get; set; }

        public int FailedPasswordAttemptCount { get; set; }

        public int FailedPasswordAnswerAttemptCount { get; set; }
    }

    public static class UserCredentialMapper
    {
        #region UserCredential -> UserCredentialModel

        public static void Map(this IEnumerable<User_Account> sourceCollection, IList<UserModel> destinationCollection)
        {
            if (sourceCollection == null)
                return;

            if (destinationCollection == null)
                return;

            foreach (var c in sourceCollection)
            {
                var userCredentialModel = new UserModel();
                c.Map(userCredentialModel);
                destinationCollection.Add(userCredentialModel);

            }
        }

        public static void Map(this User_Account sourceObject, UserModel destinationObject)
        {
            if (sourceObject == null || destinationObject == null)
                return;

            destinationObject.UserId = sourceObject.Id;
            destinationObject.FullName = sourceObject.FullName;
            destinationObject.UserName = sourceObject.UserName;
            destinationObject.IsApproved = sourceObject.IsApproved;
            //destinationObject.IsLockedOut = sourceObject.IsLockedOut;
            destinationObject.LastLoginDate = sourceObject.LastLoginDate.ToString();
            destinationObject.LastPasswordChangedDate = sourceObject.LastPasswordChangedDate.ToString();
            destinationObject.LastLockoutDate = sourceObject.LastLockoutDate.ToString();
            destinationObject.FailedPasswordAttemptCount = sourceObject.FailedPasswordAttemptCount;
            destinationObject.FailedPasswordAnswerAttemptCount = sourceObject.FailedPasswordAnswerAttemptCount;
        }

        #endregion

        #region UserCredentialModel -> UserCredential

        public static void Map(this IEnumerable<UserModel> sourceCollection, IList<User_Account> destinationCollection)
        {
            if (sourceCollection == null)
                return;

            if (destinationCollection == null)
                return;

            foreach (var c in sourceCollection)
            {
                var userCredential = new User_Account();
                c.Map(userCredential);
                destinationCollection.Add(userCredential);

            }
        }

        public static void Map(this UserModel sourceObject, User_Account destinationObject)
        {
            if (sourceObject == null || destinationObject == null)
                return;

            destinationObject.FullName = sourceObject.FullName;
            destinationObject.UserName = sourceObject.UserName;
            //destinationObject.DateOfBirth = Convert.ToDateTime(sourceObject.DateOfBirth);
            //destinationObject.GenderId = sourceObject.GenderId;
            //destinationObject.ProfileImageLocation = sourceObject.ProfileImageLocation;
            //destinationObject.UserStatusId = sourceObject.UserStatusId;
            //destinationObject.UserTypeId = sourceObject.UserTypeId;
        }

        #endregion
    }
}
