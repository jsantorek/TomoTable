using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoTable
{
    [Serializable]
    class NeuralNetwork
    {
        // length of output vector, related to image resolution
        private static int OutputSize = FileManager.OutputHeight * FileManager.OutputWidth;

        // length of input vector
        private static int InputSize = 496;

        // learning constants
        private double LearningSpeed;
        private double LearningMomentum;

        private List<Neuron> InputLayer;
        private List<Neuron> HiddenLayer;
        private List<Neuron> OutputLayer;

        #region -- Globals --
        private static readonly Random Random = new Random();
        #endregion

        #region -- Constructor --
        public NeuralNetwork(int hiddenSize, double? learnRate = null, double? momentum = null)
        {
            LearningSpeed = learnRate ?? 0.3;
            LearningMomentum = momentum ?? 0.8;
            InputLayer = new List<Neuron>();
            HiddenLayer = new List<Neuron>();
            OutputLayer = new List<Neuron>();

            Logistic logistic = new Logistic();

            for (var i = 0; i < InputSize; i++)
                InputLayer.Add(new Neuron(logistic as IActivationFunction));

            for (var i = 0; i < hiddenSize; i++)
                HiddenLayer.Add(new Neuron(InputLayer, logistic as IActivationFunction));

            for (var i = 0; i < OutputSize; i++)
                OutputLayer.Add(new Neuron(HiddenLayer, logistic as IActivationFunction));
        }
        #endregion

        /// <summary>
        /// Training routine.
        /// Returns true if mean square error decreases below threshold within epoch limit.
        /// Otherwise returns false.
        /// </summary>
        /// <param name="maxError"> error threshold </param>
        /// <param name="maxEpochs"> maximum number of training epochs </param>
        /// <param name="input"> input vector </param>
        /// <param name="output"> expected output vector </param>
        /// <returns></returns>
        public bool Train(double maxError, int maxEpochs, List<double []> inputsList, List<double []> targetsList)
        {
            var error = 1.0;
            var numEpochs = 0;

            while (numEpochs < maxEpochs)
            {
                var errors = new List<double>();
                using (var iEnumerator = inputsList.GetEnumerator())
                using (var tEnumerator = targetsList.GetEnumerator())
                while (iEnumerator.MoveNext() && tEnumerator.MoveNext())
                {
                    double [] input = iEnumerator.Current;
                    double [] target = tEnumerator.Current;

                    ForwardPropagate(input);
                    BackPropagate(target);
                    errors.Add(CalculateError(target));
                }
                numEpochs++;
                error = errors.Average();
                Console.WriteLine("epoch: " + numEpochs + ", error: " + error);
                if (error < maxError)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Evaluates given input signal with current state of network.
        /// </summary>
        /// <param name="input"> input to be evaluated </param>
        /// <returns> output of network's computations </returns>
        public double[] Compute(params double[] inputs)
        {
            ForwardPropagate(inputs);
            return OutputLayer.Select(a => a.Value).ToArray();
        }

        /// <summary>
        /// Function saving current state of neural network to file specified.
        /// </summary>
        /// <param name="fpath"> path where network should be saved </param>
        public void SaveToFile(string fpath)
        {
            Console.WriteLine("Saving file to " + fpath);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (Stream stream = File.Open(fpath, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, this);
            }
            sw.Stop();
            Console.WriteLine(String.Format("File saved, time: {0}", sw.Elapsed));
            return;
        }

        /// <summary>
        /// Restores network from file.
        /// </summary>
        /// <param name="fpath"> path to file in which network was saved </param>
        static public NeuralNetwork LoadFromFile(string fpath)
        {
            Console.WriteLine("Loading file from " + fpath);
            Stopwatch sw = new Stopwatch();
            Object loaded;
            sw.Start();
            using (Stream stream = File.Open(fpath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                loaded = binaryFormatter.Deserialize(stream);
            }
            sw.Stop();
            Console.WriteLine(String.Format("File loaded, time: {0}", sw.Elapsed));
            return (NeuralNetwork)loaded;
        }

        private void ForwardPropagate(params double[] inputs)
        {
            var i = 0;
            InputLayer.ForEach(a => a.Value = inputs[i++]);
            HiddenLayer.ForEach(a => a.CalculateValue());
            OutputLayer.ForEach(a => a.CalculateValue());
        }

        private void BackPropagate(params double[] targets)
        {
            var i = 0;
            OutputLayer.ForEach(a => a.CalculateGradient(targets[i++]));
            HiddenLayer.ForEach(a => a.CalculateGradient());
            HiddenLayer.ForEach(a => a.UpdateWeights(LearningSpeed, LearningMomentum));
            OutputLayer.ForEach(a => a.UpdateWeights(LearningSpeed, LearningMomentum));
        }

        private double CalculateError(params double[] targets)
        {
            var i = 0;
            return OutputLayer.Sum(a => Math.Abs(a.CalculateError(targets[i++])))/FileManager.OutputMax;
        }

        #region -- Helpers --
        public static double GetRandom()
        {
            return 2 * Random.NextDouble() - 1;
        }
        #endregion
    }
}
