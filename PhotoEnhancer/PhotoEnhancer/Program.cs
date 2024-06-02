using PhotoEnhancer.Data;
using PhotoEnhancer.Filters;
using PhotoEnhancer.Filters.Transformations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoEnhancer
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();
            //mainForm.AddFilter(new LighteningFilter());
            //mainForm.AddFilter(new GrayScaleFilter());
            //mainForm.AddFilter(new HueFilter());

            mainForm.AddFilter(new PixelFilter<LighteningParameters>(
                "Осветление/затемнение",
                (p, parameters) => p * parameters.Coefficient
                ));

            mainForm.AddFilter(new PixelFilter<EmptyParameters>(
                "Оттенки серого",
                (p, parameters) =>
                {
                    var lightness = 0.3 * p.R + 0.6 * p.G + 0.1 * p.B;
                    return new Pixel(lightness, lightness, lightness);
                }
                ));

            mainForm.AddFilter(new PixelFilter<PosterizationParameters>(
               "Постеризация",
               (p, parameters) =>
               {
                   double min_function = 1000;

                   List<double[]> ChannelsList = Palettes.ListOfPalletes[(int)parameters.GradationsNumber - 2];

                   Pixel result_pixel = new Pixel();
                   for (int a = 0; a < ChannelsList.Count; a++)
                   {
                       Pixel k = new Pixel();
                       k.R = ChannelsList[a][0]; k.G = ChannelsList[a][1]; k.B = ChannelsList[a][2];
                       double new_function = Palettes.ColorGradient(p, k);
                       if (new_function < min_function)
                       {
                           min_function = new_function;
                           result_pixel = k;
                       }
                   }
                   return result_pixel;
               }
               ));


            mainForm.AddFilter(new PixelFilter<HueParameters>(
                "Оттенок",
                (p, parameters) =>
                {
                    var q = Convertors.RGBToHSL(p);

                    var hue = q.H * 360 + parameters.Shift;

                    if (hue >= 360)
                        hue -= 360;

                    return Convertors.HSLToRGB(new PixelHSL(hue / 360, q.S, q.L));
                }
                ));

            mainForm.AddFilter(new TransformFilter(
                "Отражение по горизонтали",
                size => size,
                (point, size) => new Point(size.Width - point.X - 1, point.Y)
                ));

            mainForm.AddFilter(new TransformFilter(
                "Поворот на 90° против ч.с.",
                size => new Size(size.Height, size.Width),
                (point, size) => new Point(size.Width - point.Y - 1, point.X)
                ));

            mainForm.AddFilter(new TransformFilter<RotationParameters>(
                "Поворот на произвольный угол", new RotationTransformer()));

            mainForm.AddFilter(new TransformFilter<EmptyParameters>(
                "Мозаика (против часовой стрелки)", new Mosaic()));

            mainForm.AddFilter(new TransformFilter<ShiftParameters>(
                "Сдвиг вниз", new ShiftTransformer()));

            Application.Run(mainForm);
        }
    }
}
