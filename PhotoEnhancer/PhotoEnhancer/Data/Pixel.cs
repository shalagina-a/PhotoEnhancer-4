using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public struct Pixel
    {
        private double h;
        public double R
        {
            get => h;
            set => h = CheckValue(value);
        }

        private double g;
        public double G
        {
            get => g;
            set => g = CheckValue(value);
        }

        private double b;
        public double B
        {
            get => b;
            set => b = CheckValue(value);
        }

        public Pixel(double red, double green, double blue) : this()
        {
            R = red;
            G = green;
            B = blue;
        }

        private double CheckValue(double val)
        {
            if (val > 1 || val < 0)
                throw new ArgumentException("Неверное значение для интенсивности канала");

            return val;
        }

        public static Pixel operator *(Pixel p, double k)
        {
            var result = new Pixel();
            result.R = TrimChannel(p.R * k);
            result.G = TrimChannel(p.G * k);
            result.B = TrimChannel(p.B * k);

            return result;
        }

        public static Pixel operator *(double k, Pixel p) => p * k;


        public static double TrimChannel(double channel)
        {
            if (channel < 0) return 0;

            if (channel > 1) return 1;

            return channel;
        }

    }
}
