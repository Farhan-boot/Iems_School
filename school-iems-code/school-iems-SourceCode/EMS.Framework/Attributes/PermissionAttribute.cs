using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Framework.Attributes
{
    public class PermissionAttribute : Attribute
    {
        private readonly int _permissionNo;

        public PermissionAttribute(int permissionNo)
        {
            _permissionNo = permissionNo;
        }

        public int PermissionNo
        {
            get { return _permissionNo; }
        }
    }
}
