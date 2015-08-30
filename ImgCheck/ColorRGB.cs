using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImgCheck
{
    public class ColorRGB
    {
        public byte Red;
        public byte Green;
        public byte Blue;
        public double Hue;
        public double Saturation;
        public double Brightness;

        public ColorRGB()
        {
            
        }

        public ColorRGB(Color value)
        {
            this.Red = value.R;
            this.Green = value.G;
            this.Blue = value.B;

            this.Hue = value.GetHue();
            this.Saturation = value.GetSaturation();
            this.Brightness = value.GetBrightness();
        }

        public ColorRGB(double hue, double saturation, double brightness)
        {
            var color = HSB2RGB(hue, saturation, brightness);

            this.Red = color.R;
            this.Green = color.G;
            this.Blue = color.B;

            this.Hue = color.GetHue();
            this.Saturation = color.GetSaturation();
            this.Brightness = color.GetBrightness();
        }

        public static implicit operator Color(ColorRGB rgb)
        {
            Color c = Color.FromArgb(rgb.Red, rgb.Green, rgb.Blue);
            return c;
        }

        public static explicit operator ColorRGB(Color c)
        {
            return new ColorRGB(c);
        }

        public string RGBString
        {
            get
            {
                return string.Format("R:{0} G:{1} B:{2}", this.Red, this.Green, this.Blue);
            }
        }

        public string HLSString
        {
            get
            {
                return string.Format("H:{0} L:{1} S:{2}", this.Hue, this.Brightness, this.Saturation);
            }
        }

        public static ColorRGB HSL2RGB(double h, double sl, double l)
        {
            double v;
            double r, g, b;

            r = l;   // default to gray
            g = l;
            b = l;
            v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = l + l - v;
                sv = (v - m) / v;
                h *= 6.0;
                sextant = (int)h;
                fract = h - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }

            return new ColorRGB(Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255)));
        }

        public static Color HSB2RGB(double hue, double saturation, double brightness)
        {
            double red = 0, green = 0, blue = 0;

            if (saturation == 0)
            {
                red = green = blue = brightness;
            }
            else
            {
                // the color wheel consists of 6 sectors. Figure out which sector you're in.
                double sectorPos = hue / 60.0;
                int sectorNumber = (int)(Math.Floor(sectorPos));
                // get the fractional part of the sector
                double fractionalSector = sectorPos - sectorNumber;

                // calculate values for the three axes of the color. 
                double p = brightness * (1.0 - saturation);
                double q = brightness * (1.0 - (saturation * fractionalSector));
                double t = brightness * (1.0 - (saturation * (1 - fractionalSector)));

                // assign the fractional colors to r, g, and b based on the sector the angle is in.
                switch (sectorNumber)
                {
                    case 0:
                        red = brightness;
                        green = t;
                        blue = p;
                        break;
                    case 1:
                        red = q;
                        green = brightness;
                        blue = p;
                        break;
                    case 2:
                        red = p;
                        green = brightness;
                        blue = t;
                        break;
                    case 3:
                        red = p;
                        green = q;
                        blue = brightness;
                        break;
                    case 4:
                        red = t;
                        green = p;
                        blue = brightness;
                        break;
                    case 5:
                        red = brightness;
                        green = p;
                        blue = q;
                        break;
                }
            }

            return Color.FromArgb((int)(red * 255), (int)(green * 255), (int)(blue * 255));
        }
    }
}
