using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public interface IParametersHandler<TParameters>
        where TParameters : IParameters, new()
    {
        ParameterInfo[] GetDescription();
        TParameters CreateParameters(double[] values);
    }
}
