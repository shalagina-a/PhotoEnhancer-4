using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public class TransformFilter<TParameters> : ParametrizedFilter<TParameters>
        where TParameters : IParameters, new()
    {
        ITransformer<TParameters> transformer;

         public TransformFilter(string name, ITransformer<TParameters> transformer)
        {
            this.name = name;
            this.transformer = transformer;
        }

        public override Photo Process(Photo original, TParameters parameters)
        {
            var oldSize = new Size(original.Width,original.Height);
            transformer.Initialize(oldSize, parameters);
            var newSize = transformer.ResultSize;
            var result = new Photo(newSize.Width, newSize.Height);

            for (var x = 0; x < newSize.Width; x++)
                for (var y = 0; y < newSize.Height; y++)
                { 
                    var oldPoint = transformer.MapPoint(new Point(x, y));

                    if(oldPoint.HasValue)
                        result[x, y] = original[oldPoint.Value.X, oldPoint.Value.Y];
                }

            return result;
        }
    }
}
