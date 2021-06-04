#include <cmath>
#include <random>
#include <ctime>
#include <iostream>
#include <fstream>
#include <ostream>
#include "neuralnet.h"
using namespace std;

mt19937 gen(time(0));
uniform_real_distribution<> urd(-1,1);

void NeuralNet::Neuron::ActivateBySig()
{
    value = (1 / (1 + pow(2.71828, -value)));
}

NeuralNet::NeuralNet(vector<int> _layersSize){
    layerCount = _layersSize.size();
    layersSize = _layersSize;
    output = vector<double>(_layersSize[layerCount-1]);
    weight = vector<vector<vector<Weight>>>(layerCount-1);

    for(int x = 0; x < layerCount;x++)
        layers.push_back(Layer(layersSize[x]));

    for(int x = 0; x < layerCount-1;x++)
    {
        weight[x] = vector<vector<Weight>>(layersSize[x]);
        for(int y = 0; y < layersSize[x];y++)
        {
            weight[x][y] = vector<Weight>(layersSize[x+1]);
            for(int z = 0; z < layersSize[x+1];z++)
            {
                weight[x][y][z] = urd(gen);
            }
        }
    }
}

vector<double> NeuralNet::Go(vector<double> const &data)
{
    ClearNeuron();
    for(int x = 0; x < layersSize[0];x++){
        layers[0].layer[x] = data[x];
    }

    for(int x = 0; x < layerCount-1;x++){
        for(int y = 0; y < layersSize[x+1];y++){
            for(int z = 0; z < layersSize[x];z++){
                layers[x+1].layer[y].value += (layers[x].layer[z].value*weight[x][z][y].value);
            }
            layers[x+1].layer[y].ActivateBySig();
        }
    }

    for(int x = 0; x < layersSize[layerCount-1]; x++){
        output[x] = layers[layerCount-1].layer[x].value;
    }
    return output;
}

void NeuralNet::MORRelearn(vector<vector<double>> const &tests,double E,double a)
{
    for(int z = 0; z < tests.size();z++){
        Go(tests[z]);
        for(int x = 0; x < layers[layers.size()-1].layer.size();x++){
            _ideal = tests[z][layers[0].layer.size()+x];
            layers[layers.size()-1].layer[x].delta = (_ideal-output[x])*((1-output[x])*output[x]);
        }
        for(int x = layers.size()-2;x > 0;x--){
            for(int y = 0; y < layersSize[x]; y++){
                double sumOut = 0;
                for(int c = 0; c < layersSize[x+1];c++)
                    sumOut += weight[x][y][c].value * layers[x+1].layer[c].delta;

                layers[x].layer[y].delta = ((1-layers[x].layer[y].value)*layers[x].layer[y].value)*sumOut;
                for(int c = 0; c < layersSize[x+1];c++){
                    weight[x][y][c].value += E*layers[x].layer[y].value*layers[x+1].layer[c].delta+
                            a*weight[x][y][c].delta;
                    weight[x][y][c].delta = E*layers[x].layer[y].value*layers[x+1].layer[c].delta+
                            a*weight[x][y][c].delta;
                }
            }
        }
    }
}

double NeuralNet::getWrong(vector<vector<double>> &tests)
{
    double errorValue = 0;
    for(int x = 0; x < tests.size();x++)
    {
        Go(tests[x]);
        //cout <<tests[x][tests[x].size()-1] << "  t:n  " << Go(tests[x]) << endl;
        for(int y = 0; y < layersSize[layersSize.size()-1];y++)
        {
            errorValue += pow(tests[x][layersSize[0]+y]-output[y],2)*tests[x][layersSize[0]+y];
        }
    }
    errorValue/=tests.size();
    return errorValue;
}

void NeuralNet::PrintNeurons()
{
    for(int x = 0; x < layerCount;x++)
    {
        cout<<"Layer: " << x << endl;
        for(int y = 0; y < layersSize[x];y++)
        {
            cout << layers[x].layer[y].value << endl;
        }
        cout << endl << endl;
    }
}

void NeuralNet::PrintWeight()
{
    for(int x = 0; x < layerCount-1;x++)
    {
        cout << "Layer: " << x << endl;
        for(int y = 0; y < layersSize[x];y++)
        {
            cout << "Neuron: " << y << endl;
            for(int z = 0; z < layersSize[x+1];z++)
            {
                cout << weight[x][y][z].value << " ";
            }
            cout << endl;
        }
    }
}

void NeuralNet::ClearNeuron()
{
    for(int x = 1; x < layerCount;x++)
    {
        for(int y = 0; y < layersSize[x];y++)
        {
            layers[x].layer[y].value = 0;
        }
    }
}

void NeuralNet::SaveWeight(string path)
{
    ofstream fout;
    fout.open(path);
    for(int x = 0; x < weight.size();x++)
    {
        for(int y = 0; y < weight[x].size();y++)
        {
            for(int z = 0; z < weight[x][y].size();z++)
            {
                fout << weight[x][y][z].value << " ";
            }
        }
    }
    fout.close();
}

void NeuralNet::LoadWeight(string path)
{
    ifstream fout;
    fout.open(path);
    for(int x = 0; x < weight.size();x++)
    {
        for(int y = 0; y < weight[x].size();y++)
        {
            for(int z = 0; z < weight[x][y].size();z++)
            {
                fout >> weight[x][y][z].value;
            }
        }
    }
    fout.close();
}
