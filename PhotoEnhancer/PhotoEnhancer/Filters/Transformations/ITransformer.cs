using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public interface ITransformer<TParameters>
        where TParameters : IParameters, new()
    {
        Size ResultSize { get; }
        Point? MapPoint(Point newPoint);
        void Initialize(Size oldSize, TParameters parameters);
    }
}
