using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoTable
{
    interface IActivationFunction
    {
        public abstract double f(double x);
        public abstract double df(double x);
    }
}
