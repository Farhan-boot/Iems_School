using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Framework.Attributes;

namespace EMS.Framework.Permissions
{
    public partial class PermissionCollection
    {
        //TODO have to fix right permission for EMS. Not worked/used.
        [Permission(6)]
        public sealed class InventoryArea
        {
        }
    }
}
