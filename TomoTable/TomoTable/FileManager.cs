using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoTable
{
    static class FileManager
    {
        // .bmp resolution
        public static int x = 64;
        public static int y = 32;

        /// <summary>
        /// Function converting .bmp file into output data of neural network.
        /// If .bmp file is shorter than network's output, rest of array is filled with zeroes.
        /// </summary>
        /// <param name="fpath"> path to .bmp file </param>
        /// <returns></returns>
        public static double [] BMPtoOUT(string fpath)
        {

            return null;
        }

        /// <summary>
        /// Function converting output data of neural network into .bmp file.
        /// </summary>
        /// <param name="fpath"> path to file to be created </param>
        /// <param name="output"> output data of neural network </param>
        public static void OUTtoBMP(string fpath, double [] output)
        {

        }


        /// <summary>
        /// Function converting data read from table into into input for neural network.
        /// </summary>
        /// <param name="fpath"> path of file containing table readings in matrix form </param>
        public static void DATAtoIN(string fpath)
        {

        }

        /// <summary>
        /// Converts output value of single neuron (values between 0.0 and 1.0) into color.
        /// </summary>
        /// <param name="o"> output of single neuron </param>
        /// <returns></returns>
        private static double ToColor(double o)
        {
            return 0.0;
        }

        /// <summary>
        /// Converts color from .bmp file into expected output of single neuron (value between 0.0 and 1.0)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static double ToOutput(double c)
        {

            return 0.0;
        }
    }
}
