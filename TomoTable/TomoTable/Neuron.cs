using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoTable
{
    class Neuron
    {
        private double [] weights, inputs, last_dWeigts;
        private double output, error, sum;

        public Neuron(int n)
        {
            Random rnd = new Random();
            inputs = Enumerable
                .Repeat(0.0, n)
                .ToArray();
            weights = Enumerable
                .Repeat(0.0, n)
                .Select(i => rnd.NextDouble())
                .ToArray();
            output = 0.0;
            error = 0.0;
        }

        public void Feed(double [] newinput)
        {
            for(int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = newinput[i];
            }
        }

        public void Feed(double newinput, int index)
        {
            inputs[index] = newinput;
        }

        public void Update()
        {
            sum = 0.0;
            for(int i = 0; i < weights.Length; i++)
            {
                sum += weights[i] * inputs[i];
            }
            output = F(sum);
        }

        public void Adjust()
        {

        }

        public double GetOutput()
        {
            return output;
        }

        private double F(double x)
        {
            return 0.0;
        }

        private double DF(double x)
        {
            return 0.0;
        }
    }
}
