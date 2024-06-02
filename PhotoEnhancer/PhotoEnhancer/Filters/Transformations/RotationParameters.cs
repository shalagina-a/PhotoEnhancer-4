using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public class RotationParameters : IParameters
    {
        [ParameterInfo(Name = "Угол в °",
                    MinValue = -360,
                    MaxValue = 360,
                    DefaultValue = 0,
                    Increment = 5)]
        public double AngleInDegrees {  get; set; }
    }
}
