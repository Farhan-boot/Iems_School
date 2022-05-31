using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Web.UI.Models
{
    public class ResetConfirmViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public string EmailTitle { get; set; }
        public string EmailAddress { get; set; }
        public long UserId { get; set; }
    }
}
