using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public class TransformFilter : TransformFilter<EmptyParameters>
    {
        public TransformFilter(string name, 
            Func<Size, Size> sizeTransform, 
            Func<Point, Size, Point> pointTransform) : 
            base(name, new SimpleTransformer(sizeTransform, pointTransform)) {}
    }
}
