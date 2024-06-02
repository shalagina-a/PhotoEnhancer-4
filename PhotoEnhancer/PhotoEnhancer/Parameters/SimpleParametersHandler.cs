using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PhotoEnhancer
{
    public class SimpleParametersHandler<TParameters> : IParametersHandler<TParameters>
        where TParameters : IParameters, new()
    {
        public TParameters CreateParameters(double[] values)
        {
            var parameters = new TParameters();

            var properties = parameters
                .GetType()
                .GetProperties()
                .Where(p => p.GetCustomAttributes<ParameterInfo>().Count() > 0)
                .ToArray();

            if (properties.Length != values.Length)
                throw new ArgumentException();

            for (var i = 0; i < values.Length; i++)
                properties[i].SetValue(parameters, values[i]);

            return parameters;
        }

        public ParameterInfo[] GetDescription() =>
            typeof(TParameters)
            .GetProperties()
            .Select(p => p.GetCustomAttributes<ParameterInfo>())
            .Where(a => a.Count() > 0)
            .SelectMany(x => x)
            .Cast<ParameterInfo>()
            .ToArray();
    }
}
