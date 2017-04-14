using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoTable
{
    class NeuralNetwork
    {
        // length of input vectors
        public static int InputSize = 31;

        // length of output vector, related to image resolution
        private static int OutputSize = FileManager.x * FileManager.y;

        // learning constants
        private double LearningSpeed;
        private double LearningMomentum;

        private bool isBiased = true;

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

            for (var i = 0; i < InputSize; i++)
                InputLayer.Add(new Neuron(new Logistic() as IActivationFunction));

            for (var i = 0; i < hiddenSize; i++)
                HiddenLayer.Add(new Neuron(InputLayer, new Logistic() as IActivationFunction));

            for (var i = 0; i < OutputSize; i++)
                OutputLayer.Add(new Neuron(HiddenLayer, new Logistic() as IActivationFunction));
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
        public bool train(double maxError, int maxEpochs, List<List<double>> inputsList, List<List<double>> outputsList)
        {
            return false;
        }

        /// <summary>
        /// Function evaluating given input signal with current state of network.
        /// </summary>
        /// <param name="input"> input to be evaluated </param>
        /// <returns></returns>
        public List<double> evaluate(List<double> input)
        {
            return null;
        }

        /// <summary>
        /// Function returning a list of results from previously evaluated by network input
        /// </summary>
        /// <returns></returns>
        public List<double> getLastOutput()
        {
            return null;
        }

        /// <summary>
        /// Function saving current state of neural network to file specified.
        /// </summary>
        /// <param name="fpath"> path where network should be saved </param>
        public void save(string fpath)
        {

        }

        /// <summary>
        /// Constructs new network in state that is loaded from file specified.
        /// </summary>
        /// <param name="fpath"> path to file containing saved network </param>
        /// <returns></returns>
        public NeuralNetwork load(string fpath)
        {

            return null;
        }

        private void fwdPropagate()
        {
            return;
        }

        private void bckPropagate(List<double> expected)
        {

        }

        private void feedInput(List<double> input)
        {
            return;
        }

        #region -- Helpers --
        public static double GetRandom()
        {
            return 2 * Random.NextDouble() - 1;
        }
        #endregion
    }
}
