using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            return Math.Pow(b, pow - 1) / (pow - 1) - Math.Pow(a, pow - 1) / (pow - 1);
        }

        private int factorial(int n)
        {
            int a = 1;
            for (int i = 1; i <= n; i++)
                a *= i;
            return a;
        }

        /*        protected double Gauss_Legandr(double u, int p)
                {
                    double r = 0;
                    double ni = factorial(p);
                    for (int k = 1; k <= p; k++)
                    {
                        double sc1 = Math.Pow(Math.Pow((u + 1), p), k);
                        double sc2 = Math.Pow(Math.Pow((u - 1), p), p-k);
                        r += ni / (factorial(k) * factorial(p - k)) * sc1 * sc2;

                    }
                        return r;
                }-*/
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

        protected double Gauss_Ermit(double u)
        {
            return Math.Exp(-Math.Pow(u,2));
        }
    }

    public class GausMethod: Methods
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
            double[] x = new double[2*N];
            for (int p = 0; p < 2*N; p++)
            {
                x[p] = Gauss_Legandr(p, N);
            }
            double[,] Xi = new double[N, N];
            double[] add = new double[N];
            for(int i = 0; i < N; i++)
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

namespace IntegralsGaus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
