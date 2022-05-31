using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMS.CoreLibrary.Helpers;

namespace EMS.Web.Framework.WebFormHelper.Controls
{
    [ToolboxData("<{0}:EnumDropDownList runat=server></{0}:EnumDropDownList>")]
    public class EnumDropDownList:System.Web.UI.WebControls.DropDownList
    {
        //public void Populate(string typeName)
        //{
        //    Populate(ReflectionHelper.GetType(typeName));
        //}
        public void Populate(Type type,bool clear=true)
        {
            //DropDownList ddl = new DropDownList();
            if (clear)
            {
                this.Items.Clear();
            }
            if (type == null) return;
                IList<EnumUtil.EnumInfo> enumInfos= EnumUtil.GetEnumList(type);

            foreach (var enumInfo in enumInfos)
            {
                ListItem item = new ListItem
                {
                    //Selected = false,
                    Value = enumInfo.Id.ToString(),
                    Text = enumInfo.Name.AddSpacesToSentence()
            };
                this.Items.Add(item);
            }
            this.SelectedIndex = 0;
        }

        public int SelectedValue2
        {
            get
            {
                return int.Parse(this.SelectedValue);
            }
            set
            {
               
                this.SelectedValue = value.ToString();
            }
        }
        //public void ClearSelection()
        //{
        //    // Clear previous selection
        //    list.SelectedIndex = -1;
        //    foreach (ListItem item in list.Items)
        //        item.Selected = false;
        //}
    }
}
