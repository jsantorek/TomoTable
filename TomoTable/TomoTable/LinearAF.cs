using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoTable
{
    [Serializable]
    class LinearAF : IActivationFunction
    {
        public double df(double x)
        {
            return 1;
        }

        public double f(double x)
        {
            return x;
        }
    }
}
