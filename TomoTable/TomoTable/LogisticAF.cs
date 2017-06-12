using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoTable
{
    [Serializable]
    class LogisticAF : IActivationFunction
    {
        public double f(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public double df(double x)
        {
            return f(x) * (1 - f(x));
        }
    }
}
