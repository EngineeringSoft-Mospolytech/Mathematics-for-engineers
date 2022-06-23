using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GausMethodIntegral
{
    public delegate double Function(double x);

    abstract public class Methods
    {

        public static double[] LinSystemGauss(double[/*строка*/,/*столбец*/] system, double[] add)
        {
            int N = system.GetLength(0);
            for (int i = 0; i < N; i++)//строки
            {
                for (int j = 0; j < i; j++)//столбцы
                {
                    double a = -1;
                    int k;
                    for (k = i - 1; k >= 0; k--)
                    {
                        if (system[k, j] != 0)
                        {
                            a = system[i, j] / system[k, j];
                            break;
                        }
                    }

                    for (int c = 0; c < N; c++)
                    {
                        system[i, c] -= system[k, c] * a;
                    }
                    add[i] -= add[k] * a;
                }
            }
            double[] res = new double[N];
            for (int i = N - 1; i >= 0; i--)
            {
                double a = add[i];
                for (int j = N - 1; j > i; j--)
                {
                    a -= system[i, j] * res[j];
                }
                res[i] = a / system[i, i];
            }
            return res;
        }

        protected double PowIntegral(int pow, double a, double b)
        {
            return Math.Pow(b, pow + 1) / (pow + 1) - Math.Pow(a, pow + 1) / (pow + 1);
        }

        private int factorial(int n)
        {
            int a = 1;
            for (int i = 1; i <= n; i++)
                a *= i;
            return a;
        }

        protected double Gauss_Legandr(int i, int n)
        {
            return Math.Cos((Math.PI * (4 * i - 1)) / (4 * n + 2));
        }

        protected double Gauss_Chebyshev(double u)
        {
            return 1.0 / (Math.Sqrt(1.0 - Math.Pow(u, 2)));
        }

        protected double Gauss_Lagerr(double u)
        {
            return Math.Exp(-u);
        }

/*        protected double Gauss_Ermit(int i, int n)
        {
            double ret = 0;
            char f = 'n';
            double[,] poly = new double[n / 2 + 1, 2];
            for (int j = 0; j <= n / 2; j++)
            {
                poly[j, 0] = Math.Pow(-1, j) * (factorial(n) * Math.Pow(2, n - 2 * j)) / (factorial(j) * factorial(n - 2 * j));
                poly[j, 1] = n - 2 * j;
            }
            //поиск отрезков
            double max = poly[1, 0];
            for (int j = 1; j <= n / 2; j++)
            {
                if (Math.Abs(poly[j, 0]) > Math.Abs(max))
                    max = poly[j, 0];
            }
            double bx = 1 + Math.Abs(max) / Math.Abs(poly[0, 0]);
            double ax = -bx;
            double delta = 0.1f; //шаг локализации корня при грубом поиске
            double eps = 0.00001f; //допустимая погрешность вычислений
            int cnt = 1;
            while (cnt <= i)
            {
                cnt++;
                if (f == 'r')
                    ax = ret + 80 * eps;
                else if (f == 'l')
                    bx = ret - 80 * eps;
                for (double X = ax; X <= bx; X += delta) //цикл грубого поиска
                {
                    double FuncX = 0;
                    double FuncXdel = 0;
                    for (int I = 0; I < n / 2 + 1; I++)
                    {
                        FuncX += poly[I, 0] * Math.Pow(X, poly[I, 1]);
                        FuncXdel += poly[I, 0] * Math.Pow(X + delta, poly[I, 1]);
                    }
                    //признак того что на интервале есть корень
                    if (FuncX * FuncXdel < 0)
                    {
                        double a = X; //левая граница интервала уточнения корня
                        double b = X + delta; //правая граница интервала уточнения корня
                                              //цикл уточнения корня
                        while (Math.Abs(b - a) > eps)
                        {
                            double c = (a + b) / 2.0; //середина очередного интервала
                                                      //признак нахождения корня в левом интервале
                            double FuncC = 0;
                            double FuncA = 0;
                            for (int I = 0; I < n / 2 + 1; I++)
                            {
                                FuncC += poly[I, 0] * Math.Pow(c, poly[I, 1]);
                                FuncA += poly[I, 0] * Math.Pow(a + delta, poly[I, 1]);
                            }
                            if (FuncC * FuncA < 0)
                            {
                                b = c; //отбрасываем правый интервал
                                f = 'r';
                            }
                            else
                            {
                                a = c; //отбрасываем левый интервал
                                f = 'l';
                            }
                        }
                        //вывод сообщения о том что найден корень
                        ret = (a + b) / 2.0;
                    }
                }
            }
            return ret;
        }
*/
        protected double Gauss_Ermit(int i, int n)
        {
            double[,] poly = new double[n / 2 + 1, 2];
            for(int j = 0; j <= n/2; j++)
            {
                poly[j, 0] = Math.Pow(-1, j) * (factorial(n) * Math.Pow(2, n - 2 * j)) / (factorial(j) * factorial(n - 2 * j));
                poly[j, 1] = n - 2 * j;
            }
            //поиск отрезков
            double max = poly[1, 0];
            int mp = (int)poly[0, 1];
            for (int j = 1; j <= n / 2; j++)
            {
                if (Math.Abs(poly[j, 0]) > Math.Abs(max))
                    max = poly[j, 0];
                if (mp < (int)poly[j, 1])
                    mp = (int)poly[j, 1];
            }
            double bx = 1 + Math.Abs(max) / Math.Abs(poly[0, 0]);
            double eps = 0.00001f; //допустимая погрешность вычислений
            double ax = -bx;
            double[] res = new double[mp];
            double FuncX = 0;
            for (int I = 0; I < mp; I++)
                res[I] = ax;
            for (double X = ax; X <= bx; X += eps)
            {
                FuncX = 0;
                for (int I = 0; I < n / 2 + 1; I++)
                {
                    FuncX += poly[I, 0] * Math.Pow(X, poly[I, 1]);
                }
                int k = 0;//max element
                for (int I = 1; I < mp; I++)
                {
                    double fk = 0;
                    double ik = 0;
                    for (int J = 0; J < n / 2 + 1; J++)
                    {
                        fk += poly[J, 0] * Math.Pow(res[k], poly[J, 1]);
                        ik += poly[J, 0] * Math.Pow(res[I], poly[J, 1]);
                    }
                    if (Math.Abs(fk) < Math.Abs(ik))
                    {
                        k = I;
                    }
                }
                double rk = 0;
                for (int J = 0; J < n / 2 + 1; J++)
                {
                    rk += poly[J, 0] * Math.Pow(res[k], poly[J, 1]);
                }
                if (Math.Abs(FuncX) < Math.Abs(rk))
                res[k] = X;
            }
            return res[i-1];           
        }
    }


    public class GausMethod : Methods
    {
        public static int N;
        public double a, b;
        public Function function;

        public double Solve(Function f, double A, double B, int n)
        {
            N = n;
            a = A;
            b = B;
            function = f;
            if (A >= B || n < 2)
                return 0;
            else
                return gfn();
        }

        public double gfn()
        {
            double r = 0;
            double[] x = new double[N];
            for (int p = 0; p < N; p++)
            {
                x[p] = Gauss_Ermit(p+1, N);
            }
            double[,] Xi = new double[N, N];
            double[] add = new double[N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Xi[i, j] = Math.Pow(x[j], i);
                }
                add[i] = PowIntegral(i, a, b);
            }
            double[] c = LinSystemGauss(Xi, add);
            for (int i = 0; i < N; i++)
            {
                r += c[i] * function(x[i]);
            }
            return r;
        }
    }
}

namespace MathM
{
    /*            int N = 8;
                double[,] x = new double[N, 3];
                double[,] sys = { { 1, 2, 5 }, { 3, -1 , -2}, { 2, -5, 1} };
                double[] a = { 1, -18 , 6};
                double[,] sys1 = { { 1, 2}, { 3, -1 }};
                double[] a1 = { 1, -18};
                double[] r = LinSystemGauss(sys1, a1);           
                for (int i = 0; i < r.Length; i++)
                {
                    Console.Write(r[i].ToString() + "   ");
                }*/
    class Program
    {
        static double fn(double x)
        {
            return x*x;
        }

        static void Main(string[] args)
        {
            GausMethodIntegral.GausMethod gausMethod = new GausMethodIntegral.GausMethod();
            double res = gausMethod.Solve(fn, -1, 1, 4);
            Console.WriteLine(res.ToString());
            Console.ReadKey();
        }
    }
}


