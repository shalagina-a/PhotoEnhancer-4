using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer.Filters.Transformations
{
    public class ShiftTransformer : ITransformer<ShiftParameters>
    {
        double shift_percent;
        Size oldSize;
        public Size ResultSize { get; private set; }

        public void Initialize(Size oldSize, ShiftParameters parameters)
        {
            shift_percent = parameters.ShiftPercentage / 100;
            this.oldSize = oldSize;
            ResultSize = oldSize;
        }

        public Point? MapPoint(Point newPoint)
        {
            var y = 0;

            if (newPoint.Y >= oldSize.Height * shift_percent)
                y = (int)(newPoint.Y - oldSize.Height * shift_percent);
            else
                y = (int)Math.Abs(newPoint.Y - oldSize.Height * (shift_percent - 1));


            return new Point(newPoint.X, y);
        }
    }
}