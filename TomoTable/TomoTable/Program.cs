using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoTable
{
    static class Program
    {

        static void Main(string[] args)
        {
            List<double[]> inputs = new List<double[]>();
            List<double[]> targets = new List<double[]>();
            List<double[]> tests = new List<double[]>();

            inputs.Add(new double[] { 1.0, 12.0 });
            targets.Add(new double[] { 0.0 });

            inputs.Add(new double[] { 3.0, 5.6 });
            targets.Add(new double[] { 0.0 });

            inputs.Add(new double[] { 1.1, 5.9 });
            targets.Add(new double[] { 0.0 });

            inputs.Add(new double[] { 11.0, 0.0 });
            targets.Add(new double[] { 1.0 });

            inputs.Add(new double[] { 9.0, 3.2 });
            targets.Add(new double[] { 1.0 });

            inputs.Add(new double[] { 9.007, 0.12 });
            targets.Add(new double[] { 1.0 });

            tests.Add(new double [] { 34.2, 4.3 });
            tests.Add(new double[] { 11.4, 0.3 });
            tests.Add(new double[] { 0.0002, 5.3 });
            tests.Add(new double[] { 2.4, 3.3 });


            NeuralNetwork net = new NeuralNetwork(2);

            net.Train(0.1, 500, inputs, targets);

            foreach ( var test in tests)
            {
                Console.WriteLine(net.Compute(test)[0]);
            }
            while (true) ;
        }
    }
}
