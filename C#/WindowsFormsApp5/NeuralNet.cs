using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApp5
{
    class NeuralNet
    {
        public class NeuronNetwork
        {
            Random rnd = new Random();
            struct Neuron
            {
                public double value;
                public double delta;
                public Neuron(double value) { this.value = value; delta = 0; }
                public void ActivateBySig()
                {
                    value = (1 / (1 + Math.Pow(2.71828, -value)));
                }
            };

            class Weight
            {
                public double value;
                public double delta;
                public Weight(double value) { this.value = value; delta = 0; }
            };

            struct Layer
            {
                public Neuron[] layer;
                public Layer(int layerSize)
                {
                    layer = new Neuron[layerSize];
                    for (int x = 0; x < layerSize; x++)
                    {
                        layer[x] = new Neuron(0);
                    }
                }
            };

            int layerCount;
            int[] layersSize;
            Layer[] layers;
            List<List<List<Weight>>> weights;
            public double[] output;
            public NeuronNetwork(int[] _layersSize)
            {
                layers = new Layer[_layersSize.Length];
                layerCount = _layersSize.Length;
                layersSize = _layersSize;
                weights = new List<List<List<Weight>>>();
                output = new double[_layersSize[_layersSize.Length - 1]];

                for (int x = 0; x < layerCount; x++)
                    layers[x] = new Layer(layersSize[x]);

                int k = 0;
                for (int x = 0; x < layerCount - 1; x++)
                {
                    weights.Add(new List<List<Weight>>());
                    for (int y = 0; y < layersSize[x]; y++)
                    {
                        weights[x].Add(new List<Weight>());
                        for (int z = 0; z < layersSize[x + 1]; z++)
                        {
                            weights[x][y].Add(new Weight(rnd.NextDouble()));   //Рандом нужен  
                            //weight[x][y][z] = k;
                            //k++;
                        }
                    }
                }
            }

            public double[] Go(List<double> data)
            {
                ClearNeuron();
                for (int x = 0; x < layersSize[0]; x++)
                {
                    layers[0].layer[x].value = data[x];
                    //layers[0].layer[x].ActivateBySig();
                }
                for (int x = 0; x < layerCount - 1; x++)
                {
                    for (int y = 0; y < layersSize[x + 1]; y++)
                    {
                        for (int z = 0; z < layersSize[x]; z++)
                        {
                            layers[x + 1].layer[y].value += (layers[x].layer[z].value * weights[x][z][y].value);
                        }
                        layers[x + 1].layer[y].ActivateBySig();
                    }
                }
                for (int x = 0; x < layersSize[layerCount - 1]; x++)
                {
                    //layers[layerCount-1].layer[x].ActivateBySig();
                    this.output[x] = layers[layerCount - 1].layer[x].value;
                }
                return output;
            }

            public double getWrong(List<List<double>> tests)
            {
                double errorValue = 0;
                for (int x = 0; x < tests.Count; x++)
                {
                    Go(tests[x]);
                    //cout <<tests[x][tests[x].size()-1] << "  t:n  " << Go(tests[x]) << endl;
                    for (int y = 0; y < layersSize[layersSize.Count() - 1]; y++)
                    {
                        errorValue += Math.Pow(tests[x][layersSize[0] + y] - output[y], 2);
                    }
                }
                errorValue /= tests.Count;
                return errorValue;
            }
            void ClearNeuron()
            {
                for (int x = 1; x < layerCount; x++)
                {
                    for (int y = 0; y < layersSize[x]; y++)
                    {
                        layers[x].layer[y].value = 0;
                    }
                }
            }

            public void SaveWeight(string path)
            {
                FileStream file1 = new FileStream(path, FileMode.OpenOrCreate); //создаем файловый поток
                StreamWriter writer = new StreamWriter(file1);
                for (int x = 0; x < weights.Count; x++)
                {
                    for (int y = 0; y < weights[x].Count; y++)
                    {
                        for (int z = 0; z < weights[x][y].Count; z++)
                        {
                            writer.Write(weights[x][y][z].value + " ");
                        }
                    }
                }
                writer.Close();
            }

            public void LoadWeight(string path)
            {
                FileStream file1 = new FileStream(path, FileMode.OpenOrCreate); //создаем файловый поток
                StreamReader reader = new StreamReader(file1);
                string[] a = reader.ReadToEnd().Trim(' ').Replace('.',',').Split(' ');
                int c = 0;
                double summ = 0;
                for (int x = 0; x < weights.Count; x++)
                {
                    for (int y = 0; y < weights[x].Count; y++)
                    {
                        for (int z = 0; z < weights[x][y].Count; z++)
                        {
                            weights[x][y][z].value = Convert.ToDouble(a[c]);
                            summ += Convert.ToDouble(a[c]);
                            c++;
                        }
                    }
                } 
                reader.Close();
            }
        }
    }
}
