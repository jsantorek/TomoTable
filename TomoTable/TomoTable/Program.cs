using System;
using System.Collections.Generic;
using System.IO;
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

            string networkFile = "network.bin";

            string[] inputFiles = Directory.GetFiles(@"training\input\");

            foreach(String file in inputFiles)
            {
                String testName = Path.GetFileNameWithoutExtension(file);
                Console.WriteLine(testName);
                inputs.Add(FileManager.DATAtoIN(@"training\input\"+testName +".txt"));
                targets.Add(FileManager.BMPtoOUT(@"training\expected\"+testName +".bmp"));
            }

            //tests.Add(FileManager.DATAtoIN(@"training\input\23 bb.txt"));
            tests = inputs;

            double lSpeed = 0.6, lMom = 0.95;
            NeuralNetwork net = new NeuralNetwork(750, lSpeed, lMom);
            //NeuralNetwork net = NeuralNetwork.LoadFromFile(networkFile);

            net.Train(0.1, 10, inputs, targets);

            //net.SaveToFile(networkFile);

            for (int i = 0; i < tests.Count; i++)
            {
                FileManager.OUTtoBMP(string.Format(@"training\output\{0}.bmp", i), net.Compute(tests[i]));
            }

            Console.WriteLine("Done!");
            while (true) ;
        }
    }
}
