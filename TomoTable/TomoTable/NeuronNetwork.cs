using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoTable
{
    // TODO : check if iterating through list with foreach ensures (its absolutely crucial) exactly the same order on each iteration, if not change it
    class NeuronNetwork
    {
        // length of input vectors
        public static int iSize = 31;

        // length of output vector, related to image resolution
        static int oSize = FileManager.x * FileManager.y;

        // numbers of neurons in each layer in consecutive order including input and output layers
        private static int [] layerSizes = { 32, 10, 10, oSize };

        // learning constants
        private double learningSpeed = 0.3;
        private double learningMomentum = 0.5;

        private List<List<Neuron>> neurons;

        public NeuronNetwork()
        {
            for(int nLayer = 0, lastLayerSize = iSize; nLayer < layerSizes.Length; nLayer++)
            {
                int layerSize = layerSizes[nLayer];

                // I think initializing with capacitance saves some memory, this is probably not neccessary
                List<Neuron> layer = new List<Neuron>(layerSize);

                for(int nNeuron = 0; nNeuron < layerSize; nNeuron++)
                {
                    layer.Add(new Neuron(lastLayerSize));
                }
                lastLayerSize = layerSize;
                neurons.Add(layer);
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
        public bool Train(double maxError, int maxEpochs, List<List<double>> inputsList, List<List<double>> outputsList)
        {
            // primitive input and test data check, no error handling
            if (maxError < 0.0 || inputsList.Count != outputsList.Count)
                return false;
            for (int training = 0; training < inputsList.Count; training++)
            {
                if (inputsList[training].Count != iSize * layerSizes[0] || outputsList[training].Count != oSize)
                    return false;
            }

            for (int epoch = 0; epoch < maxEpochs; epoch++)
            {
                double error = 0.0;

                // in each epoch run every available training once
                for(int training = 0; training < inputsList.Count; training++)
                {
                    FeedInput(inputsList[training]);
                    ForwardPropagate();
                    BackPropagate(outputsList[training]);
                    //error += getMSE(); how to get error?
                    error += 1.0;
                }

                // calculate average Mean Square Error from this epoch
                error /= inputsList.Count;
                if (error < maxError)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Function evaluating given input signal with current state of network.
        /// </summary>
        /// <param name="input"> input to be evaluated </param>
        /// <returns></returns>
        public List<double> Evaluate(List<double> input)
        {
            // primitive check if the input is propperly formatted
            if (input.Count != iSize * layerSizes[0])
                return null;
            FeedInput(input);
            ForwardPropagate();
            return GetLastOutput();
        }

        /// <summary>
        /// Function returning a list of results from previously evaluated by network input
        /// </summary>
        /// <returns></returns>
        public List<double> GetLastOutput()
        {
            // I think initializing with capacitance saves some memory, this is probably not neccessary
            List<double> output = new List<double>(oSize);
            int outputLayer = neurons.Count - 1;
            for (int i = 0; i<neurons[outputLayer].Count; i++)
            {
                output.Add(neurons[outputLayer][i].GetOutput());
            }
            return output;
        }

        /// <summary>
        /// Function saving current state of neural network to file specified.
        /// </summary>
        /// <param name="fpath"> path where network should be saved </param>
        public void Save(string fpath)
        {

        }

        /// <summary>
        /// Constructs new network in state that is loaded from file specified.
        /// </summary>
        /// <param name="fpath"> path to file containing saved network </param>
        /// <returns></returns>
        public NeuronNetwork Load(string fpath)
        {

            return null;
        }

        private void ForwardPropagate()
        {
            // iterate through every layer in order
            for (int layer_i = 0; layer_i < neurons.Count; layer_i++)
            {
                // iterate thorugh every neuron in given layer and update it with new input
                for (int neuron_i = 0; neuron_i < neurons[layer_i].Count; neuron_i++)
                {
                    neurons[layer_i][neuron_i].Update();

                    // if this is not output layer...
                    if(layer_i < neurons[layer_i].Count-1)
                    {
                        double output = neurons[layer_i][neuron_i].GetOutput();
                        // pass new calculated value with propper index into each neuron in next layer
                        for (int neuronNext_i = 0; neuronNext_i < neurons[layer_i+1].Count; neuronNext_i++)
                        {
                            neurons[layer_i+1][neuronNext_i].Feed(output, neuron_i);
                        }
                    }
                }
            }
        }

        private void BackPropagate(List<double> expected)
        {

        }

        private void FeedInput(List<double> input)
        {
            for (int i = 0; i < neurons[0].Count; i++)
            {
                // TODO : make sure this returns propper ranges, should be:
                // 0-31, 32-63, 64-95...
                neurons[0][i].Feed(input.GetRange(i * iSize, iSize).ToArray());
            }
        }
    }
}
