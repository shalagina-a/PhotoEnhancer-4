using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public static class Convertors
    {
        public static Photo BitmapToPhoto(Bitmap bmp)
        {
            var photo = new Photo(bmp.Width, bmp.Height);

            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++) 
                {
                    var p = bmp.GetPixel(x, y);

                    photo[x, y] = new Pixel(
                        p.R / 255.0,
                        p.G / 255.0,
                        p.B / 255.0
                        );  

                    //photo[x,y].R = p.R / 255.0;
                    //photo[x,y].G = p.G / 255.0;
                    //photo[x,y].B = p.B / 255.0;
                }

            return photo;
        }

        public static Bitmap PhotoToBitmap(Photo photo) 
        { 
            var bmp = new Bitmap(photo.Width, photo.Height);

            for(int x = 0;x < bmp.Width; x++)
                for(int y = 0;y < bmp.Height; y++)
                    bmp.SetPixel(x, y, 
                        Color.FromArgb(
                            (int)Math.Round(photo[x,y].R * 255), 
                            (int)Math.Round(photo[x,y].G * 255),
                            (int)Math.Round(photo[x,y].B * 255)
                            )
                        );

            return bmp;
        }

        public static PixelHSL RGBToHSL(Pixel p)
        {
            //R, G and B input range = 0 ÷ 255
            //H, S and L output range = 0 ÷ 1.0

            var varMin = Math.Min(Math.Min(p.R, p.G), p.B);    
            var varMax = Math.Max(Math.Max(p.R, p.G), p.B);
            var delta = varMax - varMin;                   

            var lightness = (varMax + varMin) / 2;

            double hue = 0, saturation = 0; //This is a gray, no chroma...

            if (delta != 0)   //Chromatic data...                  
            {
                if (lightness < 0.5)
                    saturation = delta / (varMax + varMin);
                else
                    saturation = delta / (2 - varMax - varMin);

                var delR = (((varMax - p.R) / 6) + (delta / 2)) / delta;
                var delG = (((varMax - p.G) / 6) + (delta / 2)) / delta;
                var delB = (((varMax - p.B) / 6) + (delta / 2)) / delta;

                if (p.R == varMax)
                    hue = delB - delG;
                else if (p.G == varMax)
                    hue = (1.0 / 3) + delR - delB;
                else if (p.B == varMax)
                    hue = (2.0 / 3) + delG - delR;

                if (hue < 0) hue += 1;
                if (hue > 1) hue -= 1;
            }

            return new PixelHSL(hue, saturation, lightness);
        }

        public static Pixel HSLToRGB(PixelHSL pixelHSL)
        {
            //H, S and L input range = 0 ÷ 1.0
            //R, G and B output range = 0 ÷ 1.0

            double red = 0, green = 0, blue = 0;
            double p, q;

            if (pixelHSL.S == 0)
            {

                red = pixelHSL.L;
                green = pixelHSL.L;
                blue = pixelHSL.L;
            }
            else
            {
                if (pixelHSL.L < 0.5)
                    q = pixelHSL.L * (1 + pixelHSL.S);
                else
                    q = (pixelHSL.L + pixelHSL.S) - (pixelHSL.S * pixelHSL.L);

                p = 2 * pixelHSL.L - q;

                red = ChanelFromHue(p, q, pixelHSL.H + (1.0 / 3));
                green = ChanelFromHue(p, q, pixelHSL.H);
                blue = ChanelFromHue(p, q, pixelHSL.H - (1.0 / 3));
            }
            
            return new Pixel(red, green, blue);
        }

        public static double ChanelFromHue(double p, double q, double variatedHue)
        {
            if (variatedHue < 0) 
                variatedHue += 1;

            if (variatedHue > 1) 
                variatedHue -= 1;

            if ((6 * variatedHue) < 1) 
                return p + (q - p) * 6 * variatedHue;

            if ((2 * variatedHue) < 1)
                return q;

            if ((3 * variatedHue) < 2)
                return p + (q - p) * ((2.0 / 3) - variatedHue) * 6;

            return p;
        }

    }
}
