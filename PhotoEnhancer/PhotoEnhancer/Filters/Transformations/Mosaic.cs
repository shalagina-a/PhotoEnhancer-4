using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer.Filters.Transformations
{
    public class Mosaic : ITransformer<EmptyParameters>
    {
        Size oldSize;
        public Size ResultSize { get; private set; }

        public void Initialize(Size oldSize, EmptyParameters parameters)
        {
            this.oldSize = oldSize;
            ResultSize = new Size(oldSize.Width * 2, oldSize.Height * 2);
        }

        public Point? MapPoint(Point newPoint)
        {
            var p = newPoint;
            var x = 0;
            var y = 0;

            if (p.X >= oldSize.Width && p.Y < oldSize.Height)
            {
                x = p.X - oldSize.Width; y = p.Y;
            }

            else if (p.X < oldSize.Width && p.Y < oldSize.Height)
            {
                x = oldSize.Width - p.X - 1; y = p.Y;
            }

            else if (p.X < oldSize.Width && p.Y >= oldSize.Height)
            {
                x = oldSize.Width - p.X - 1; y = ResultSize.Height - p.Y - 1;
            }

            else
            {
                x = p.X - oldSize.Width; y = ResultSize.Height - p.Y - 1;
            }

            return new Point(x, y);

        }
    }
}
