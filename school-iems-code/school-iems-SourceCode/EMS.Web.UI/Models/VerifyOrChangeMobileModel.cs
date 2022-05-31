using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMS.Web.UI.Models
{
    public class VerifyOrChangeMobileModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Number")]
        public string Mobile { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        public string FullName { get; set; }
        public string UserName { get; set; }

        public string Code { get; set; }

        public bool IsNeedEmailVerify { get; set; }


    }
}