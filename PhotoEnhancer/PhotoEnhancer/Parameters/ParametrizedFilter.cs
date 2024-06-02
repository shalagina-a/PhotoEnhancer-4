using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PhotoEnhancer
{
    public abstract class ParametrizedFilter<TParameters> : IFilter
        where TParameters : IParameters, new()
    {
        protected string name;

        IParametersHandler<TParameters> handler = new StaticParametersHandler<TParameters>();   

        public ParameterInfo[] GetParametersInfo() 
            => handler.GetDescription();

        public Photo Process(Photo original, double[] values)
        {
            var parameters = handler.CreateParameters(values);

            return Process(original, parameters);
        }

        public abstract Photo Process(Photo original, TParameters parameters);

        public override string ToString()
        {
            return name;
        }
    }
}
