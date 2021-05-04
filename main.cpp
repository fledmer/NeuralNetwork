#include <iostream>
#include <vector>
#include <fstream>
#include <ostream>
#include <string>
#include <random>
#include <thread>


using namespace std;

mt19937 gen(time(0));
uniform_real_distribution<> urd(-1, 1);

class NeuronNetwork
{
    struct Neuron{
        double value;
        Neuron(double value):value(value){}
        void ActivateBySig()
        {
            value = (1 / (1 + pow(2.71828, -value)))*2-1;
        }
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
    vector<vector<vector<double>>> weight;
    vector<double> output;
    NeuronNetwork(vector<int> _layersSize)
    {
        layerCount = _layersSize.size();
        layersSize = _layersSize;
        output = vector<double>(_layersSize[layerCount-1]);
        weight = vector<vector<vector<double>>>(layerCount-1);

        for(int x = 0; x < layerCount;x++)
            layers.push_back(Layer(layersSize[x]));

        int k = 0;
        for(int x = 0; x < layerCount-1;x++)
        {
            weight[x] = vector<vector<double>>(layersSize[x]);
            for(int y = 0; y < layersSize[x];y++)
            {
                weight[x][y] = vector<double>(layersSize[x+1]);
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
            for(int y = 0; y < layersSize[x];y++)
            {
                for(int z = 0; z < layersSize[x+1];z++)
                {
                    layers[x+1].layer[z].value += (layers[x].layer[y].value*weight[x][y][z]);
                    layers[x+1].layer[z].ActivateBySig();
                }
            }
        }
        for(int x = 0; x < layersSize[layerCount-1]; x++)
        {
            //layers[layerCount-1].layer[x].ActivateBySig();
            output[x] = layers[layerCount-1].layer[x].value;
        }
        return layers[layerCount-1].layer[0].value;
    }

    void RandomRelearn()
    {
        for(int x = 0; x < layerCount-1;x++)
        {
            for(int y = 0; y < layersSize[x];y++)
            {
                for(int z = 0; z < layersSize[x+1];z++)
                {
                        weight[x][y][z] += urd(gen);
                }
            }
        }
    }

    void RandomRelearn2()
    {
        for(int x = 0; x < layerCount-1;x++)
        {
            for(int y = 0; y < layersSize[x];y++)
            {
                for(int z = 0; z < layersSize[x+1];z++)
                {
                    weight[x][y][z] += urd(gen);
                }
            }
        }
    }

    double getWrong()
    {
        return 0;
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
                    cout << weight[x][y][z] << " ";
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
                    fout << weight[x][y][z] << " ";
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
                    fout >> weight[x][y][z];
                }
            }
        }
        fout.close();
    }
};

double minErrorsum = 4;
vector<vector<vector<double>>> bestWeight;
int timer_old = 0;
int timer_new = 0;
double maxCorrectAns = 0;

void Teach(NeuronNetwork nn)
{
    const double wrongParam = 0.1;
    int testCount = 4;
    int inputCount = 2;
    int inputAnsCount = 1;
    ifstream fin("tests.txt");
    vector<vector<double>> tests;
    for(int x = 0; x < testCount; x++)
    {
        vector<double> test;
        double value1;
        string value2;
        for(int y = 0; y < inputCount + inputAnsCount;y++)
        {
            fin >> value1;
            test.push_back(value1);
        }
        tests.push_back(test);
    }
    //Тесты
    //Обучение
    long long int AgeCount = 0;
    double errorValue = 0;
    int correctAns = 0;
    double errorSumm = 0;
    int iterWithoutchange = 0;
    urd = uniform_real_distribution<>(-1000000,1000000);
    do
    {
        AgeCount++;
        correctAns = 0;
        errorSumm = 0;
        nn.weight = bestWeight;
        nn.RandomRelearn();
        for(int x = 0; x < testCount;x++)
        {
            nn.Go(tests[x]);
            errorValue = pow(nn.output[0] - tests[x][tests[x].size()-1],2);
            errorSumm += errorValue;
            if(errorValue < wrongParam)
                correctAns++;
        }
        errorValue/=testCount;
        if(errorSumm < minErrorsum)
        {
            maxCorrectAns = correctAns;
            minErrorsum = errorSumm;
            iterWithoutchange = 0;
            cout << "FIND " << minErrorsum << endl;
        }

        if(AgeCount == 100000)
        {
          iterWithoutchange++;
          timer_old = timer_new;
          timer_new = clock();
          cout << "Correct Ansver: " << maxCorrectAns << " Min ErorSum " << minErrorsum
               << " TIME: " << clock()-timer_old << endl;
          AgeCount = 0;
        }
        if(maxCorrectAns == 4)
        {
            cout <<"END"<< endl;
            nn.weight = bestWeight;
            nn.SaveWeight("2.txt");
            exit(0);
            break;
        }
    }
    while(true);
}


int main()
{ 
    ifstream fin("core.txt");
    int CORE_VALUE = 1;
    fin >> CORE_VALUE;
    fin.close();

    fin = ifstream("struct.txt");
    vector<int> INPUT;
    int c = 1;
    while(fin >> c)
    {
        INPUT.push_back(c);
    }
    fin.close();

    NeuronNetwork nn(INPUT);
    nn.LoadWeight("1.txt");


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


    while(true)
    {
        cout << "INPUT 2 NUMBER:" << endl;
        double value1,value2;
        cin >> value1 >> value2;
        vector<double> a = {double(value1),double(value2)};
        //cout << nn.Go(a) << endl;
        cout << endl;
        //nn.PrintNeurons();

    }
    return 0;
}


// Напишем нейронную сеть способную ебашить оператор xor и сам проставлю коэфы
// Потом попробую натренировать ее
