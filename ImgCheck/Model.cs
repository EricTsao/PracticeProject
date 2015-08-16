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
                return string.Format("R:{0} G:{1} B:{2}", MyColor.R, MyColor.G, MyColor.B);
            }
        }
        public string HLSString
        {
            get
            {
                var color = (Color)MyColor;
                double hue = Math.Round(color.GetHue() /360, 2, MidpointRounding.AwayFromZero);
                double lightness = Math.Round(color.GetBrightness(), 1, MidpointRounding.AwayFromZero);
                double saturation = Math.Round(color.GetSaturation(), 1, MidpointRounding.AwayFromZero);

                return string.Format("H:{0} L:{1} S:{2}", hue, lightness, saturation);
            }
        }
    }
}
