using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace TomoTable
{
    class FileManager
    {
        // .bmp resolution
        public static int OutputWidth = 64;
        public static int OutputHeight = 32;

        /// <summary>
        /// Function converting .bmp file into output data of neural network.
        /// If .bmp file is shorter than network's output, rest of array is filled with zeroes.
        /// </summary>
        /// <param name="fpath"> path to .bmp file </param>
        /// <returns></returns>
        public static double [] BMPtoOUT(string fpath)
        {
            System.Drawing.Bitmap OutputImage = new System.Drawing.Bitmap(fpath);
            List<double> OutputTable = new List<double>(OutputWidth*OutputHeight);

            if (OutputImage.Width != OutputWidth || OutputImage.Height != OutputHeight)
            {
                throw new System.IO.IOException(); //maybe write up a new exception?
            }

            for (int i = 0; i < OutputImage.Width; i++)
            {
                for (int j = 0; j < OutputImage.Height; j++)
                {
                    Color originalPixel = OutputImage.GetPixel(i, j); 
                    OutputTable.Add(ToOutput(originalPixel));
                }
            }

            return OutputTable.ToArray();
        }

        /// <summary>
        /// Function converting output data of neural network into .bmp file.
        /// </summary>
        /// <param name="fpath"> path to file to be created </param>
        /// <param name="output"> output data of neural network </param>
        public static void OUTtoBMP(string fpath, double [] output)
        {
            Bitmap OutputImage = new Bitmap(OutputWidth, OutputHeight);

            for (int i = 0; i < OutputWidth; i++)
            {
                for (int j = 0; j < OutputHeight; j++)
                { 
                    OutputImage.SetPixel(i, j, ToColor(output[(i*OutputHeight)+j]));
                }
            }
            OutputImage.Save(fpath);
        }


        /// <summary>
        /// Function converting data read from table into into input for neural network.
        /// </summary>
        /// <param name="fpath"> path of file containing table readings in matrix form </param>
        public static double [] DATAtoIN(string fpath)
        {
            List<double> inputData = new List<double>();

            using (TextReader reader = File.OpenText(fpath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] numbers = line.Split(' ');
                    foreach (string number in numbers)
                    {
                        if (!double.TryParse(number, out double value))
                        {
                            continue;
                            //throw new System.IO.IOException(); //maybe a new one?
                        }
                        else if (value != 0) //the 0 values are ultimately useless and the neural network really doesn't care either way
                        {
                            inputData.Add(value);
                        }
                    }
                }
            }
            return inputData.ToArray();
        }

        /// <summary>
        /// Converts output value of single neuron (values between 0.0 and 1.0) into color.
        /// </summary>
        /// <param name="o"> output of single neuron </param>
        /// <returns></returns>
        private static Color ToColor(double o)
        {
            int color = (int)(o * 255);
            return Color.FromArgb(color, color, color);
        }

        /// <summary>
        /// Converts color from .bmp file into expected output of single neuron (value between 0.0 and 1.0)
        /// </summary>
        /// <param name="c">A Color read fom an image output</param>
        /// <returns></returns>
        private static double ToOutput(Color c)
        {
            return (((c.R * 0.3) + (c.G * 0.59) + (c.B * 0.11)) / 255);
        }
    }
}
