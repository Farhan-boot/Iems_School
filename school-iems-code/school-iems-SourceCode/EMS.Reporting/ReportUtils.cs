using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.Reporting.Drawing;

namespace EMS.Reporting
{
    public class ReportUtils
    {

        //demo code
        public static Unit TextBoxAutoFontSize(string value, object reportitem)
        {

            Telerik.Reporting.Processing.TextBox tb = reportitem as Telerik.Reporting.Processing.TextBox;

            //the textBox item width in pixels
            float textBoxWidth = tb.Width.Value;

            //the fontsize should be in points
            string fontname = tb.Style.Font.Name;
            float fontsize = tb.Style.Font.Size.Value;

            Size size = TextRenderer.MeasureText(value, new System.Drawing.Font(fontname, fontsize));
            try
            {
                while ((float)size.Width + 10 > textBoxWidth && fontsize > 0.1)
                {
                    fontsize--;
                    if (fontsize<=3)
                    {
                        fontsize = 3;
                       break; 
                    }
                    size = TextRenderer.MeasureText(value, new System.Drawing.Font(fontname, fontsize));
                }
            }
            catch (Exception ex)
            {

            }
            Unit result = new Unit(fontsize, UnitType.Pixel); return result;

        }

    }
}
