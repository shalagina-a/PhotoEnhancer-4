using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public class HueParameters : IParameters
    {
        [ParameterInfo(Name = "Сдвиг оттенка",
                    MinValue = 0,
                    MaxValue = 360,
                    DefaultValue = 0,
                    Increment = 1)]
        public double Shift {  get; set; }
    }
}
