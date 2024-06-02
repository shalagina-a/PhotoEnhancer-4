using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer.Data
{
    static public class Palettes
    {
        public static List<List<double[]>> ListOfPalletes = new List<List<double[]>>();

        static Palettes()
        {
            for (int k = 2; k <= 8; k++)
            {
                List<double> channel_intensities = new List<double>();

                for (int t = 2; t <= k + 1; t++)
                {
                    channel_intensities.Add((t - 2.0) / (k - 1));
                }

                List<double[]> ChannelsList = new List<double[]>();

                for (int r = 0; r < channel_intensities.Count; r++)
                {
                    for (int g = 0; g < channel_intensities.Count; g++)
                    {
                        for (int b = 0; b < channel_intensities.Count; b++)
                        {
                            double[] array = new double[] { channel_intensities[r], channel_intensities[g], channel_intensities[b] };
                            ChannelsList.Add(array);
                        }
                    }
                }

                ListOfPalletes.Add(ChannelsList);
            }
        }

        public static double ColorGradient(Pixel p, Pixel k)
        {
            return (3 * Math.Pow(k.R - p.R, 2) + 6 * Math.Pow(k.G - p.G, 2) + Math.Pow(k.B - p.B, 2));
        }
    }
}