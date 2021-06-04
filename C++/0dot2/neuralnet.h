#ifndef NEURALNET_H
#define NEURALNET_H
#include <vector>
#include <string>

class NeuralNet
{
    struct Neuron{
        double value;
        double delta;
        Neuron(double value):value(value),delta(0){}
        void ActivateBySig();
        double DerSig()
        {
            return (1-value)*((1-value)*value);
        }
    };

    struct Weight
    {
        double value;
        double delta;
        Weight():value(0),delta(0){}
        Weight(double value):value(value),delta(0){}
    };

    struct Layer
    {
        std::vector<Neuron> layer;
        Layer(int layerSize)
        {
            for(int x = 0; x < layerSize;x++)
            {
                layer.push_back(Neuron(0));
            }
        }
    };
    std::vector<Layer> layers;
    std::vector<std::vector<std::vector<Weight>>> weight;
public:
    int layerCount;
    std::vector<int> layersSize;
    std::vector<double> output;
    NeuralNet(std::vector<int> _layerSize);
    std::vector<double> Go(std::vector<double> const &data);
private:
    double _ideal;
    void ClearNeuron();
public:
    void MORRelearn(std::vector<std::vector<double>> const &tests,double E,double a);
    double getWrong(std::vector<std::vector<double>> &tests);
    void PrintNeurons();
    void PrintWeight();
    void SaveWeight(std::string path);
    void LoadWeight(std::string path);
};

#endif // NEURALNET_H
