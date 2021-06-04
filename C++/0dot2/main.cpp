#include <iostream>
#include <vector>
#include <fstream>
#include <ostream>
#include <string>

#include "neuralnet.h"


using namespace std;


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
    //Загружаем тесты
    fin = ifstream("tests.txt");
    vector<vector<double>> tests;

    int testCount = 14;
    int inputCount = INPUT[0];
    int outputCount = INPUT[INPUT.size()-1];
    for(int x = 0; x < testCount; x++)
    {
        vector<double> test;
        double value1;
        string value2;
        for(int y = 0; y < inputCount + outputCount;y++)
        {
            fin >> value1;
            test.push_back(value1);
        }
        tests.push_back(test);
    }
    cout << "CREATE" << endl;
    NeuralNet nn(INPUT);
    cout << "CREATE" << endl;
    cout << nn.getWrong(tests) << endl;
    cout << "a" << endl;
    double min = 4;
    long int k = 0;
    nn.LoadWeight("weight.txt");
    while(k != 100)
    {
        k++;
        nn.MORRelearn(tests,0.7,0.3);
        cout << nn.getWrong(tests) << endl;
    }
    nn.SaveWeight("weight.txt");
    //nn.PrintWeight();
    string path;
        cout << "Ready" << endl;
    while(true)
    {
        int a1,a2;
        cout << "INPUT TEST PATH:" << endl;
        vector<double> test;
        for(int x = 0; x < INPUT[INPUT.size()-1];x++)
        {
            cin >> a1;
            test.push_back(a1);
        }
        vector<double> b = nn.Go(test);
        for(int x = 0; x < b.size();x++)
        {
            cout << x << " : " << b[x] << endl;
        }
        //nn.PrintNeurons();

    }
    return 0;
}
// Напишем нейронную сеть способную ебашить оператор xor и сам проставлю коэфы
// Потом попробую натренировать ее
