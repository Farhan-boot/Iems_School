using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMS.Web.UI.Models
{
    public class VerifyOrChangeEmailModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        public string FullName { get; set; }
        public string UserName { get; set; }

        public string Code { get; set; }

        public bool IsNeedMobileVerify { get; set; }


    }
}