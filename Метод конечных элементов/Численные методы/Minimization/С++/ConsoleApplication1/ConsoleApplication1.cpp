#include <iostream>
using namespace std;

double Fn(double x)
{
    double a = pow(x, 2);
    return a;
}
double min(double Func(double x), double E, double A, double B)
{
    while ((abs(B-A)/2 > E))
    {
        double d = abs((B - A) / 2) / 4;
        double x = (A + B) / 2;
        double x1 = x + d;
        double x2 = x - d;
        if (Func(x1) < Func(x2))
        {
            A = x2;
        }
        else
        {
            B = x1;
        }
    }
    return (A + B) / 2;
}


//  функция !! для 2-х переменных
double F(int N, // количество неизвестных
    double X[]) // параметры
{
    // DIMENSION Х(2)
    double A = X[0];
    double B = X[1];
    return A * A + 100 * B * B;
}

//****************
void MINIF(int N, // количество неизвестных
    double FUNCT(int N, double x[]), // функция
    double X[], // на входе - начальное приближение, на выходе - решение
    double S[], 
    double *FX)
{
    //******* Доп. переменные ********* 
    double F;
    //*********************************
    for (int k = 0; k < N; k++)
    {
        X[k] = X[k] + S[k];
        F = FUNCT(N, X);
        if (F >= *FX)
        {
            X[k] = X[k] - S[k] - S[k];
            F = FUNCT(N, X);
            if (F >= *FX)
            {
                X[k] = X[k] + S[k];
                continue;
            }
            else
            {
                *FX = F;
                S[k] = -S[k];
                continue;
            }
        }
        else
        {
            *FX = F;
            continue;
        }
    }
}

//  Вспомогательная функция
void Lable6(int N, double X[], double Y[], double S[], double *TETA)
{
    //(6)
    for (int k = 0; k < N; k++)
    {
        if (S[k] * (Y[k] - X[k]) < 0)
            S[k] = -S[k];
        *TETA = X[k];
        X[k] = Y[k];
        Y[k] = Y[k] + Y[k] - *TETA;
    }
}

//    Функция минимизации
void MINF(int N, // количество неизвестных
    double FUNCT(int N, double x[]), // функция
    double VOISIN, // окрестность начального приближения для поиска решения
    double EPSIL, // нижняя граница величины шага
    double X[], // на входе - начальное приближение, на выходе - решение
    double *FX)
{
    //******* Доп. переменные *********
    double TETA;
    double R0 = 0.1;
    double FY;
    double DELTA;

    bool s6_2 = false;
    //*********************************
    double Y[100], S[100];
    if (N > 100)
        return;
    DELTA = VOISIN;
    R0 = 0.1;
    for (int k = 0; k < N; k++)
        S[k] = DELTA;
    *FX = FUNCT(N, X);
    
    while (DELTA < EPSIL)  //(2)                             // ?
    {
        FY = *FX; //       здесь цикл
        for (int k = 0; k < N; k++)
            Y[k] = X[k];
        //ITER=IТER+l
        MINIF(N, FUNCT, Y, S, FX);                          // ?!
        if (FY >= *FX) //else goto (6)
        {
            if (DELTA < EPSIL) // (4)
                return;
            DELTA = R0 * DELTA;
            for (int k = 0; k < N; k++)
                S[k] = R0 * S[k];
            // goto 2
        }

        //state6_2 = false;
        bool flag = true;
        while (!flag)   //(~6)
        {
            flag = false;

            Lable6(N, X, Y, S, &TETA);  //(6)                                                 
            *FX = FY;
            FY = FUNCT(N, Y);
            MINIF(N, FUNCT, Y, S, FX);
            s6_2 = FY < *FX;
            if (FY < *FX) //else goto 2
            {
                //bool flag = false;
                for (int k = 0; k < N; k++)
                {
                    if (abs(Y[k] - X[k]) > 0.5 * abs(S[k]))
                    {
                        flag = true; // goto 6
                        break;
                    }
                }
                if (flag)
                    continue;
            }
            else
                break;
        }
        if (!s6_2)
            continue;                                       // !!
    }
    // goto 4 ОК
}


void Method(int mas[])
{
    for (int i = 0; i < 4; i++)
    {
        mas[i]++;
    }
}

int main()
{
//    cout << min(Fn, 0.001, -1, 1) << endl;
/*    int arr[] = {1, 2, 3, 4};
    Method(arr);
    for (int i = 0; i < 4; i++)  
        cout << arr[i] << endl;    */
    double x[2] = { 100.0, 200.0 };
    double FX = 0;
    MINF(2, F, 10.0, 0.01, x, &FX);
    cout << x[0] << ",   " << x[1] << endl;
    cout << FX << endl;
    system("pause");
}