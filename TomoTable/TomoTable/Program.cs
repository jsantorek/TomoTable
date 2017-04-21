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

            string networkfile = "network.bin";

            for (int number = 0; number <= 23; number++)
            {
                inputs.Add(FileManager.DATAtoIN(string.Format(@"training\input\{0} bb.txt", number)));
                targets.Add(FileManager.BMPtoOUT(string.Format(@"training\expected\{0}.bmp", number)));
            }

            //tests.Add(FileManager.DATAtoIN(@"training\input\23 bb.txt"));
            tests = inputs;


            NeuralNetwork net = new NeuralNetwork(1500);
            //NeuralNetwork net = NeuralNetwork.LoadFromFile(networkfile);

            net.Train(0.1, 10, inputs, targets);

            //net.SaveToFile(networkfile);
            
            for(int i = 0; i < tests.Count; i++)
            {
                FileManager.OUTtoBMP(string.Format(@"training\output\{0}.bmp", i), net.Compute(tests[i]));
            }

            Console.WriteLine("Done!");
            while (true) ;
        }
    }
}
