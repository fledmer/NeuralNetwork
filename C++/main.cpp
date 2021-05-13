#include <iostream>
#include <vector>
#include <fstream>
#include <ostream>
#include <string>
#include <random>
#include <thread>


using namespace std;

mt19937 gen(time(0));
uniform_real_distribution<> urd(-1,1);

class NeuronNetwork
{
    struct Neuron{
        double value;
        double delta;
        Neuron(double value):value(value),delta(0){}
        void ActivateBySig()
        {
            value = (1 / (1 + pow(2.71828, -value)));
        }
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
        vector<Neuron> layer;
        Layer(int layerSize)
        {
            for(int x = 0; x < layerSize;x++)
            {
                layer.push_back(Neuron(0));
            }
        }
    };

public:
    int layerCount;
    vector<int> layersSize;
    vector<Layer> layers;
    vector<vector<vector<Weight>>> weight;
    vector<double> output;
    NeuronNetwork(vector<int> _layersSize)
    {
        layerCount = _layersSize.size();
        layersSize = _layersSize;
        output = vector<double>(_layersSize[layerCount-1]);
        weight = vector<vector<vector<Weight>>>(layerCount-1);

        for(int x = 0; x < layerCount;x++)
            layers.push_back(Layer(layersSize[x]));

        int k = 0;
        for(int x = 0; x < layerCount-1;x++)
        {
            weight[x] = vector<vector<Weight>>(layersSize[x]);
            for(int y = 0; y < layersSize[x];y++)
            {
                weight[x][y] = vector<Weight>(layersSize[x+1]);
                for(int z = 0; z < layersSize[x+1];z++)
                {
                    weight[x][y][z] = urd(gen);
                    //weight[x][y][z] = k;
                    //k++;
                }
            }
        }
    }

    double Go(vector<double> data)
    {
        ClearNeuron();
        for(int x = 0; x < layersSize[0];x++)
        {
            layers[0].layer[x] = data[x];
            //layers[0].layer[x].ActivateBySig();
        }
        for(int x = 0; x < layerCount-1;x++)
        {
            for(int y = 0; y < layersSize[x+1];y++)
            {
                for(int z = 0; z < layersSize[x];z++)
                {
                    layers[x+1].layer[y].value += (layers[x].layer[z].value*weight[x][z][y].value);
                }
                layers[x+1].layer[y].ActivateBySig();
            }
        }
        for(int x = 0; x < layersSize[layerCount-1]; x++)
        {
            //layers[layerCount-1].layer[x].ActivateBySig();
            output[x] = layers[layerCount-1].layer[x].value;
        }
        return output[0];
    }

private:
    double _output;
    double _ideal;
public:
    void MORRelearn(vector<vector<double>> tests,double E,double a)
    {
        for(int z = 0; z < tests.size();z++)
        {
            _output = Go(tests[z]);
            _ideal = tests[z][tests[z].size()-1];
            layers[layers.size()-1].layer[0].delta = (_ideal-_output)*((1-_output)*_output);
            for(int x = layers.size()-2;x > 0;x--)
            {
                for(int y = 0; y < layersSize[x]; y++)
                {
                    //cout << x << endl;
                    double sumOut = 0;
                    for(int c = 0; c < layersSize[x+1];c++)
                        sumOut += weight[x][y][c].value * layers[x+1].layer[c].delta;
                    //cout << x << endl;
                    layers[x].layer[y].delta = ((1-layers[x].layer[y].value)*layers[x].layer[y].value)*sumOut;
                    for(int c = 0; c < layersSize[x+1];c++)
                    {
                        weight[x][y][c].value += E*layers[x].layer[y].value*layers[x+1].layer[c].delta+
                                a*weight[x][y][c].delta;
                        weight[x][y][c].delta = E*layers[x].layer[y].value*layers[x+1].layer[c].delta+
                                a*weight[x][y][c].delta;
                    }
                }
            }
        }
    }

    double getWrong(vector<vector<double>> tests)
    {
        double errorValue = 0;
        for(int x = 0; x < tests.size();x++)
        {
            //cout <<tests[x][tests[x].size()-1] << "  t:n  " << Go(tests[x]) << endl;
            errorValue += pow(tests[x][tests[x].size()-1]-Go(tests[x]),2);
        }
        errorValue/=tests.size();
        return errorValue;
    }

    void PrintNeurons()
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

    void PrintWeight()
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

    void ClearNeuron()
    {
        for(int x = 1; x < layerCount;x++)
        {
            for(int y = 0; y < layersSize[x];y++)
            {
                layers[x].layer[y].value = 0;
            }
        }
    }

    void SaveWeight(string path)
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

    void LoadWeight(string path)
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
};




int main()
{ 
    ifstream fin("core.txt");   //Загружаем кол-во ядер
    int CORE_VALUE = 1;
    fin >> CORE_VALUE;
    fin.close();

    fin = ifstream("struct.txt"); //Загружаем структура сети
    vector<int> INPUT;
    int c = 1;
    while(fin >> c)
    {
        INPUT.push_back(c);
    }
    fin.close();

    NeuronNetwork nn(INPUT);

    int testCount = 4;
    int inputCount = 2;

    fin = ifstream("tests.txt");
    vector<vector<double>> tests;

    for(int x = 0; x < testCount; x++)
    {
        vector<double> test;
        double value1;
        string value2;
        for(int y = 0; y < inputCount + 1;y++)
        {
            fin >> value1;
            test.push_back(value1);
        }
        tests.push_back(test);
    }

    cout << nn.getWrong(tests) << endl;
    double min = 4;
    int k = 0;
    while(true)
    {
        k++;
        nn.MORRelearn(tests,0.7,0.6);
        double wrong = nn.getWrong(tests);
        if(wrong < min)
        {
            min = wrong;
        }
        if(k == 100000)
        {
            if(min < 0.01)
                break;
            cout << min << endl;
            k=0;
        }
    }
    nn.SaveWeight("weight.txt");
    /*
    vector<NeuronNetwork> NNL;
    for(int x = 0; x < CORE_VALUE;x++)
        NNL.push_back(NeuronNetwork(INPUT));
    bestWeight = NNL[0].weight;
    vector<thread> NNT;
    for(int x = 0; x < CORE_VALUE;x++)
    {
        NNT.push_back(thread(Teach,NNL[x]));
    }
    for(int x = 0; x < CORE_VALUE;x++)
    {
        NNT[x].join();
    }
    */

    nn.PrintWeight();
    while(true)
    {
        cout << "INPUT 2 NUMBER:" << endl;
        double value1,value2;
        cin >> value1 >> value2;
        vector<double> a = {double(value1),double(value2)};
        cout << nn.Go(a) << endl;
        cout << endl;
        nn.PrintNeurons();

    }
    return 0;
}


// Напишем нейронную сеть способную ебашить оператор xor и сам проставлю коэфы
// Потом попробую натренировать ее
