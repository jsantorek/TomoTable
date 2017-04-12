using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoTable
{
    class NeuronNetwork
    {

        // length of input vectors
        public static int iSize = 32;

        // length of output vector
        static int oSize = 2048;

        private List<double> all_inputs;
        private List<List<Neuron>> neurons;

        NeuronNetwork()
        {
            int n_layers = 4;
            for (int i = 0; i <= n_layers; i++)
            {
                int n_inlayer = (i == 0) ? iSize : (i == n_layers) ? oSize : 10;

            }
        }

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
        public bool train(double maxError, int maxEpochs, List<double> input, List<double> output)
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
        public NeuronNetwork load(string fpath)
        {

            return null;
        }

        private void fwdPropagate()
        {

        }

        private void bckPropagate()
        {

        }

        private void feedInput()
        {

        }
    }
}
