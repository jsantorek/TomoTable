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

            for (int number = 0; number < 23; number++)
            {
                inputs.Add(FileManager.DATAtoIN(string.Format(@"training\input\{0} bb.txt", number)));
                targets.Add(FileManager.BMPtoOUT(string.Format(@"training\expected\{0}.bmp", number)));
            }

            tests.Add(FileManager.DATAtoIN(@"training\input\23 bb.txt"));



            NeuralNetwork net = new NeuralNetwork(2);

            net.Train(0.1, 1000, inputs, targets);

            foreach ( var test in tests)
            {
                FileManager.OUTtoBMP(@"asdf.bmp",net.Compute(test));
            }
            while (true) ;
        }
    }
}
