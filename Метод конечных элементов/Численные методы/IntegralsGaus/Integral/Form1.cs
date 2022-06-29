using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace Integral
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            zgIntegral.GraphPane.CurveList.Clear();
            zgIntegral.GraphPane.XAxis.Title.Text = "x";
            zgIntegral.GraphPane.YAxis.Title.Text = "y";
            zgIntegral.GraphPane.XAxis.Cross = 0.0;
            zgIntegral.GraphPane.YAxis.Cross = 0.0;
            zgIntegral.AxisChange();
            zgIntegral.Invalidate();
            zgIntegral.GraphPane.Title.Text = "Интеграл I = ";
        }

        static double fn3(double x)
        {
            return 8 * x - 3;
        }

        static double fn2(double x)
        {
            return x * x + 2 * x - 5;
        }

        static double fnSin(double x)
        {
            return Math.Sin(x);
        }

        static double fn(double x)
        {
            return x * x;
        }

       // public delegate double Func(double x);

        private void button1_Click(object sender, EventArgs e)
        {
            double res = 0;
            zgIntegral.GraphPane.CurveList.Clear();
            zgIntegral.GraphPane.XAxis.Title.Text = "x";
            zgIntegral.GraphPane.YAxis.Title.Text = "y";
            zgIntegral.GraphPane.XAxis.Cross = 0.0;
            zgIntegral.GraphPane.YAxis.Cross = 0.0;
            zgIntegral.AxisChange();
            zgIntegral.Invalidate();

            GausMethodIntegral.Function function = fn;
            if (cbVariants.Text == "")
                return;
            if (cbVariants.Text == "x^2")
                function = fn;
            if (cbVariants.Text == "Sin(x)")
                function = fnSin;
            if (cbVariants.Text == "x^2 + 2x - 5")
                function = fn2;
            if (cbVariants.Text == "8x - 3")
                function = fn3;
            double A, B;
            int N;
            try
            {
                A = double.Parse(tbA.Text);
                B = double.Parse(tbB.Text);
                N = int.Parse(tbN.Text);
            }
            catch
            {
                MessageBox.Show("Сам дурак");
                return;
            }
            GausMethodIntegral.GausMethod gausMethod = new GausMethodIntegral.GausMethod();

            res = Math.Abs(gausMethod.Solve(function, A, B, N));

            PointPairList list = new PointPairList();
            for(double x = A; x <= B; x += 0.01)
            {
                list.Add(x, function(x));
            }

            LineItem line2 = zgIntegral.GraphPane.AddCurve("", list, Color.Black, SymbolType.None);
            line2.Line.Width = 2;
            line2.Line.Fill = new Fill(Color.AliceBlue);
            zgIntegral.RestoreScale(zgIntegral.GraphPane);

            zgIntegral.GraphPane.XAxis.Title.Text = "x";
            zgIntegral.GraphPane.YAxis.Title.Text = "y";
            zgIntegral.GraphPane.XAxis.Cross = 0.0;
            zgIntegral.GraphPane.YAxis.Cross = 0.0;
            zgIntegral.GraphPane.XAxis.Title.IsVisible = false;
            zgIntegral.GraphPane.YAxis.Title.IsVisible = false;
            zgIntegral.GraphPane.YAxis.Scale.IsSkipLastLabel = true;
            zgIntegral.GraphPane.YAxis.Scale.IsSkipCrossLabel = true;
            zgIntegral.GraphPane.XAxis.Scale.IsSkipLastLabel = true;
            zgIntegral.GraphPane.XAxis.Scale.IsSkipCrossLabel = true;
            zgIntegral.GraphPane.Title.Text = "Интеграл I = " + res.ToString();
            //zgIntegral.GraphPane.YAxis.MajorGrid.IsZeroLine = true;
            zgIntegral.AxisChange();
            zgIntegral.Invalidate();
            tbRes.Text = res.ToString();
        }
    }
}

namespace GausMethodIntegral
{
    public delegate double Function(double x);

    //  Класс вспомогательных математических процедур и функций 
    abstract public class Methods
    {
        //  Вычисление системы линейных уравнений методом Гаусса
        public static double[] LinSystemGauss(double[/*строка*/,/*столбец*/] system, double[] add)
        {
            int N = system.GetLength(0);
            for (int i = 0; i < N; i++) //строки
            {
                for (int j = 0; j < i; j++) //столбцы
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

        //  Нахождение определённого интеграла целой рациональной функции n-й степени
        protected double PowIntegral(int pow, double a, double b)
        {
            return Math.Pow(b, pow + 1) / (pow + 1) - Math.Pow(a, pow + 1) / (pow + 1);
        }

        //  Вычисление факториала
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

        //  Поиск корней полинома Эрмитта
        protected double Ermit(int i, int n)
        {
            double[,] poly = new double[n / 2 + 1, 2];
            for (int j = 0; j <= n / 2; j++)
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
            double eps = 0.001f; // допустимая погрешность вычислений
            double e = 0.001f; // шаг итерации
            double ax = -bx;
            double[] res = new double[mp];
            double FuncX = 0;
            for (int I = 0; I < mp; I++)
                res[I] = ax;
            for (double X = ax; X <= bx; X += e)
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
                if ((Math.Abs(FuncX) < Math.Abs(rk)) && (FuncX) < eps)
                    res[k] = X;
            }
            return res[i - 1];
        }
    }

    //  Класс для вычислений определённых интегралов квадратурой Гаусса
    public class GausMethod : Methods
    {
        public static int N;        //  Порядок вычисления
        public double a, b;         //  Границы интегрирования
        public Function function;   //  Ссылка на функцию

        //  Ввод значений, проверка, выдача решения
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

        //  Вычисление определённых интегралов квадратурой Гаусса методом Гаусса-Эрмитта
        public double gfn()
        {
            double r = 0;
            double[] x = new double[N];
            for (int p = 0; p < N; p++)
            {
                x[p] = Ermit(p + 1, N);
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
