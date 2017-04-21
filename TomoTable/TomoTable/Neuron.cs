using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomoTable
{
    [Serializable]
    class Neuron
    {
        #region -- Properties --
        public List<Synapse> InputSynapses { get; set; }
        public List<Synapse> OutputSynapses { get; set; }
        //public double Bias { get; set; }
        //public double BiasDelta { get; set; }
        public double Gradient { get; set; }
        public double Value { get; set; }
        private IActivationFunction ActivationFunction;
        #endregion

        #region -- Constructors --
        public Neuron(IActivationFunction af)
        {
            InputSynapses = new List<Synapse>();
            OutputSynapses = new List<Synapse>();
            //Bias = NeuralNetwork.GetRandom();
            ActivationFunction = af;
        }

        public Neuron(IEnumerable<Neuron> inputNeurons, IActivationFunction af)
            : this(af)
        {
            foreach (var inputNeuron in inputNeurons)
            {
                var synapse = new Synapse(inputNeuron, this);
                inputNeuron.OutputSynapses.Add(synapse);
                InputSynapses.Add(synapse);
            }
        }
        #endregion

        #region -- Values & Weights --
        public virtual double CalculateValue()
        {
            return Value = ActivationFunction.f(InputSynapses.Sum(a => a.Weight * a.InputNeuron.Value));// + Bias);
        }

        public double CalculateError(double target)
        {
            return target - Value;
        }

        public double CalculateGradient(double? target = null)
        {
            if (target == null)
                return Gradient = OutputSynapses.Sum(a => a.OutputNeuron.Gradient * a.Weight) * ActivationFunction.df(Value);

            return Gradient = CalculateError(target.Value) * ActivationFunction.df(Value);
        }

        public void UpdateWeights(double learnRate, double momentum)
        {
            //var prevDelta = BiasDelta;
            //BiasDelta = learnRate * Gradient;
            //Bias += BiasDelta + momentum * prevDelta;
            foreach (var synapse in InputSynapses)
            {
                var prevDelta = synapse.WeightDelta;
                synapse.WeightDelta = learnRate * Gradient * synapse.InputNeuron.Value;
                synapse.Weight += synapse.WeightDelta + momentum * prevDelta;
            }
        }
        #endregion

    }
}
