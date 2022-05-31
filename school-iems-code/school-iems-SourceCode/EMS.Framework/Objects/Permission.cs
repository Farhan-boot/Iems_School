using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Framework.Objects
{
    public class Permission
    {
        public int No { get; set; }
        public string Title { get; set; }

        public int ParentNo { get; set; }

        public List<Permission> ChildPermissions { get; set; }

        public bool? IsChecked { get; set; }

        public bool IsExpanded { get; set; }
    }
}
