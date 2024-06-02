using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public class SimpleTransformer : ITransformer<EmptyParameters>
    {
        Func<Size, Size> sizeTransform;
        Func<Point, Size, Point> pointTransform;
        Size oldSize;

        public Size ResultSize { get; private set; }

        public SimpleTransformer(Func<Size, Size> sizeTransform,
            Func<Point, Size, Point> pointTransform)
        { 
            this.sizeTransform = sizeTransform;
            this.pointTransform = pointTransform;
        }  

        public void Initialize(Size oldSize, EmptyParameters parameters)
        {
            this.oldSize = oldSize;
            ResultSize = sizeTransform(oldSize);
        }

        public Point? MapPoint(Point newPoint) => pointTransform(newPoint, oldSize);
    }
}
