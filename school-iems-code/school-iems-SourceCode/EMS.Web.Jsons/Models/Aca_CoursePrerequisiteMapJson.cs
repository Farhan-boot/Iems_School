using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
    public static partial class EntityMapper
    {
        private static void MapExtra(Aca_CoursePrerequisiteMap entity, Aca_CoursePrerequisiteMapJson toJson)
        {
            
            toJson.IsSelected = entity.IsSelected;
            toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
        }

        private static void MapExtra(Aca_CoursePrerequisiteMapJson json, Aca_CoursePrerequisiteMap toEntity)
        {
            toEntity.IsSelected = json.IsSelected;
            toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
    }
}
