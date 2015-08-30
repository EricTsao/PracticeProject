using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImgCheck
{
    public class ColorData
    {
        public ColorData(ColorRGB color) {
            MyColor = color;
            Count = 1;
        }

        public ColorRGB MyColor { get; set; }
        public int Count { get; set; }
        public string RGBString
        {
            get
            {
                return string.Format("R:{0} G:{1} B:{2}", MyColor.Red, MyColor.Green, MyColor.Blue);
            }
        }
        public string HLSString
        {
            get
            {
                return string.Format("H:{0} S:{1} B:{2}", MyColor.Hue, MyColor.Saturation, MyColor.Brightness);
            }
        }
    }
}
