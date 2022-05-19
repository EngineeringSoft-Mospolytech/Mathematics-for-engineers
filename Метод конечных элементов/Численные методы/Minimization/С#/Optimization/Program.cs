using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization
{
    class Program
    {
        delegate double function(int N, double[] X);
        //function FN = F;

        //  функция !! для 2-х переменных
        static double F(int N, // количество неизвестных
            double[] X) // параметры
        {
            // DIMENSION Х(2)
            double A = X[0];
            double B = X[1];
            return A * A + 100 * B * B;
        }

        //****************
        static void MINIF(int N, // количество неизвестных
            function FUNCT, // функция
            double[] X, // на входе - начальное приближение, на выходе - решение
            double[] S,
            ref double FX)
        {
            //******* Доп. переменные ********* 
            double F;
            //*********************************
            for (int k = 0; k < N; k++)
            {
                X[k] = X[k] + S[k];
                F = FUNCT(N, X);
                if (F >= FX)
                {
                    X[k] = X[k] - S[k] - S[k];
                    F = FUNCT(N, X);
                    if (F >= FX)
                    {
                        X[k] = X[k] + S[k];
                        continue;
                    }
                    else
                    {
                        FX = F;
                        S[k] = -S[k];
                        continue;
                    }
                }
                else
                {
                    FX = F;
                    continue;
                }
            }
        }

        //  Вспомогательная функция
        static void Lable6(int N, double[] X, double[] Y, double[] S, ref double TETA)
        {
            //(6)
            for (int k = 0; k < N; k++)
            {
                if (S[k] * (Y[k] - X[k]) < 0)
                    S[k] = -S[k];
                TETA = X[k];
                X[k] = Y[k];
                Y[k] = Y[k] + Y[k] - TETA;
            }
        }

        //    Функция минимизации
        static void MINF(int N, // количество неизвестных
            function FUNCT, // функция
            double VOISIN, // окрестность начального приближения для поиска решения
            double EPSIL, // нижняя граница величины шага
            double[] X, // на входе - начальное приближение, на выходе - решение
            ref double FX)
        {
            //******* Доп. переменные *********
            double TETA = 0;
            double R0 = 0.1;
            double FY;
            double DELTA;

            bool s6_2 = false;
            //*********************************
            double[] Y = new double[100];
            double[] S = new double[100];
            if (N > 100)
                return;
            DELTA = VOISIN;
            R0 = 0.1;
            for (int k = 0; k < N; k++)
                S[k] = DELTA;
            FX = FUNCT(N, X);

            while (DELTA < EPSIL)  //(2)                             // ?
            {
                FY = FX; //       здесь цикл
                for (int k = 0; k < N; k++)
                    Y[k] = X[k];
                //ITER=IТER+l
                MINIF(N, FUNCT, Y, S, ref FX);                          // ?!
                if (FY >= FX) //else goto (6)
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

                    Lable6(N, X, Y, S, ref TETA);  //(6)                                                 
                    FX = FY;
                    FY = FUNCT(N, Y);
                    MINIF(N, FUNCT, Y, S, ref FX);
                    s6_2 = FY < FX;
                    if (FY < FX) //else goto 2
                    {
                        //bool flag = false;
                        for (int k = 0; k < N; k++)
                        {
                            if (Math.Abs(Y[k] - X[k]) > 0.5 * Math.Abs(S[k]))
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


        static void Main(string[] args)
        {
            double[] x = { 100.0, 200.0 };
            double FX = 0;
            MINF(2, F, 10.0, 0.01, x, ref FX);
            Console.WriteLine(x[0].ToString() + ",   " + x[1].ToString());
            Console.ReadKey();
        }
    }
}
