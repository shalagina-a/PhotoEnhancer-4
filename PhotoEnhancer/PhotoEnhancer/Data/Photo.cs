using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public class Photo
    {
        private Pixel[,] data;

        public int Width => data.GetLength(0);
        public int Height => data.GetLength(1);

        public Pixel this[int x, int y]
        {
            get => data[x, y];
            set => data[x, y] = value;
        }

        public Photo(int width, int height)
        {
            if (width <= 0 || height <= 0) 
                throw new ArgumentException("Размеры дожны быть положительные");

            data = new Pixel[width, height];

            for(int x = 0; x < width; x++)
                for(int y = 0; y < height; y++)
                    data[x,y] = new Pixel();
        }
    }
}
