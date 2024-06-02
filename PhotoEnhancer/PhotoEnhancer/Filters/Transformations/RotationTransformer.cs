using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer.Filters.Transformations
{
    public class RotationTransformer : ITransformer<RotationParameters>
    {
        double alpha;
        Size oldSize;
        public Size ResultSize { get; private set; }

        public void Initialize(Size oldSize, RotationParameters parameters)
        {
            this.oldSize = oldSize;
            alpha = parameters.AngleInDegrees * Math.PI / 180;
            ResultSize = new Size(
                    (int)(oldSize.Width * Math.Abs(Math.Cos(alpha)) + oldSize.Height * Math.Abs(Math.Sin(alpha))),
                    (int)(oldSize.Width * Math.Abs(Math.Sin(alpha)) + oldSize.Height * Math.Abs(Math.Cos(alpha)))
                    );
        }

        public Point? MapPoint(Point newPoint)
        {
            var p = new Point(newPoint.X - ResultSize.Width / 2, newPoint.Y - ResultSize.Height / 2);
            var x = (int)(oldSize.Width / 2 + p.X * Math.Cos(alpha) - p.Y * Math.Sin(alpha));
            var y = (int)(oldSize.Height / 2 + p.X * Math.Sin(alpha) + p.Y * Math.Cos(alpha));

            if (x < 0 || x >= oldSize.Width || y < 0 || y >= oldSize.Height)
                return null;

            return new Point(x, y);
        }
    }
}
