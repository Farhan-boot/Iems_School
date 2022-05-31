using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Web.UI.Models
{
    public class ResetViewModel
    {
        [Display(Name = "User ID")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
    }
}
