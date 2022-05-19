using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Isoline
{
    public partial class Isoline : Form
    {


        static int N = 19; //must be odd
        double alpha = 0, beta = 0, gamma = 0; //alpha, beta, gamma
        bool onmove = false;
        Point startpos;
        delegate double func(double x, double y);

        int Z = 5;
        double[,] arr;
        MarchingSquare masq;
        int z0;

        public Isoline()
        {
            InitializeComponent();

            ContourIines.Text = Z.ToString();

            ChartGraphic.ChartAreas[0].AxisX.Interval = 1;
            ChartGraphic.ChartAreas[0].AxisY.Interval = 1;
            ChartGraphic.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            ChartGraphic.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;         

            update();
            init();

            ComboBoxlines.SelectedIndex = 1;

            ChartGraphic.Legends.Clear();

            
        }
        void update()
        {
            ChartGraphic.ChartAreas[0].AxisX.Minimum = -N / 2;
            ChartGraphic.ChartAreas[0].AxisY.Minimum = -N / 2;
            ChartGraphic.ChartAreas[0].AxisX.Maximum = N / 2;
            ChartGraphic.ChartAreas[0].AxisY.Maximum = N / 2;
        }
        void init() // +++++++
        {
            arr = function(N);
            masq = new MarchingSquare(N);
            z0 = 2 * N * 3 + 3;

            for (int i = 0; i < 2 * N * 3; i++)
            {
                ChartGraphic.Series.Add(i.ToString());
                ChartGraphic.Series[i].ChartType = SeriesChartType.Line;
            }
            for (int i = 2 * N * 3; i < 2 * N * 3 + 3; i++)
            {
                ChartGraphic.Series.Add(i.ToString());
                ChartGraphic.Series[i].ChartType = SeriesChartType.Line;
            }
            for (int i = z0; i < z0 + Z * 3; i++)
            {
                ChartGraphic.Series.Add(i.ToString());
                ChartGraphic.Series[i].Color = Color.LightBlue;
            }

            switch (ComboBoxlines.SelectedIndex) // тип изолиний
            {
                case 0:
                case 1: for (int i = z0; i < z0 + Z * 3; i++) ChartGraphic.Series[i].ChartType = SeriesChartType.Line; break;
                case 2: for (int i = z0; i < z0 + Z * 3; i++) ChartGraphic.Series[i].ChartType = SeriesChartType.Spline; break;
            }
            for (int n = 0; n < 3; n++) // цвет изо-линий
                for (int i = 0; i < Z; i++)
                    ChartGraphic.Series[z0 + n * Z + i].Color = Color.FromArgb(255, 255 - i * 255 / Z, 0, i * 255 / Z);
            drawscene();
        }
        #region function, CompileAndRun
        double[,] function(int N)
        {
            string[] code = {
                "using System;" +
                "namespace DynaCore" +
                "{" +
                    "public class DynaCore" +
                    "{" +
                        "static public double[,] Main(int N)" +
                        "{" +
                            "double[,] arr = new double[N, N];" +
                            "double x;" +
                            "double y;" +
                            "for (int X = 0; X < N; X++){" +
                                "for (int Y = 0; Y < N; Y++){" +
                                    "x = X - N / 2;" +
                                    "y = Y - N / 2;" +
                                    "arr[X, Y] = " + TextBoxFormula.Text + ";" +
                                "}" +
                            "}" +
                            "return arr;" +
                        "}" +
                    "}" +
                "}"
            };
            return CompileAndRun(code, N);
        }
        private double[,] CompileAndRun(string[] code, int N)
        {
            CompilerParameters CompilerParams = new CompilerParameters();
            string outputDirectory = Directory.GetCurrentDirectory();
            CompilerParams.GenerateInMemory = true;
            CompilerParams.TreatWarningsAsErrors = false;
            CompilerParams.GenerateExecutable = false;
            CompilerParams.CompilerOptions = "/optimize";
            string[] references = { "System.dll" };
            CompilerParams.ReferencedAssemblies.AddRange(references);
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerResults compile = provider.CompileAssemblyFromSource(CompilerParams, code);
            try
            {
                Module module = compile.CompiledAssembly.GetModules()[0];
                Type mt = null;
                MethodInfo methInfo = null;
                if (module != null)
                {
                    mt = module.GetType("DynaCore.DynaCore");
                }
                if (mt != null)
                {
                    methInfo = mt.GetMethod("Main");
                }

                return (double[,])methInfo.Invoke(null, new object[] { N });
            }
            catch
            {
                return new double[N, N];
            }
        } // +++++++

        #endregion // +++++++

        void drawscene()
        {
            clear();
            drawxyz();
            draw(0);
        } // +++++++

        void draw(int tn)
        {
            bool ip = false;
            if (ComboBoxlines.SelectedIndex > 0) ip = true;

            double[,] a = arr;
            int n = tn * 2 * N;

            double X, Y;

            for (int x = 0; x < N; x++)
            {
                for (int y = 0; y < N; y++)
                {
                    X = l1() * (x - N / 2) + l2() * (y - N / 2) + l3() * a[x, y];
                    Y = m1() * (x - N / 2) + m2() * (y - N / 2) + m3() * a[x, y];
                    ChartGraphic.Series[n].Points.AddXY(X, Y);
                }
                n++;
            }

            for (int y = 0; y < N; y++)
            {
                for (int x = 0; x < N; x++)
                {
                    X = l1() * (x - N / 2) + l2() * (y - N / 2) + l3() * a[x, y];
                    Y = m1() * (x - N / 2) + m2() * (y - N / 2) + m3() * a[x, y];
                    ChartGraphic.Series[n].Points.AddXY(X, Y);
                }
                n++;
            }

            //isolines
            for (int i = 1; i <= Z; i++)
            {
                PointF[] pa = masq.get(a, i, ip);
                for (int j = 0; j < pa.Length; j++)
                {
                    ChartGraphic.Series[z0 + tn * Z + i - 1].Points.AddXY(pa[j].X * l1() + pa[j].Y * l2() + i * l3(), pa[j].X * m1() + pa[j].Y * m2() + i * m3());
                }
            }

        } // +++++++

        #region sin,cos,l1,l2,l3,m1,m2,m3,n1,n2,n3,clear,drawxyz
        double sin(double x)
        {
            return Math.Sin(x * Math.PI / 180);
        }
        double cos(double x)
        {
            return Math.Cos(x * Math.PI / 180);
        }
        double l1()
        {
            return cos(alpha) * cos(gamma) - cos(beta) * sin(alpha) * sin(gamma);
        }
        double m1()
        {
            return sin(alpha) * cos(gamma) + cos(beta) * cos(alpha) * sin(gamma);
        }

        double l2()
        {
            return -cos(alpha) * sin(gamma) + cos(beta) * sin(alpha) * cos(gamma);
        }
        double m2()
        {
            return -sin(alpha) * sin(gamma) + cos(beta) * cos(alpha) * cos(gamma);
        }

        double l3()
        {
            return sin(beta) * sin(alpha);
        }
        double m3()
        {
            return -sin(beta) * cos(alpha);
        }

        void clear()
        {
            for (int i = 0; i < ChartGraphic.Series.Count; i++) ChartGraphic.Series[i].Points.Clear();
        }
        void drawxyz()
        {
            double L = N / 2; //длина оси

            //z
            ChartGraphic.Series[2 * N * 3].Points.AddXY(0, 0);
            ChartGraphic.Series[2 * N * 3].Points.AddXY(l3() * L, m3() * L);
            ChartGraphic.Series[2 * N * 3].Points[1].Label = "Z";
            //x
            ChartGraphic.Series[2 * N * 3 + 1].Points.AddXY(0, 0);
            ChartGraphic.Series[2 * N * 3 + 1].Points.AddXY(l1() * L, m1() * L);
            ChartGraphic.Series[2 * N * 3 + 1].Points[1].Label = "X";
            //y
            ChartGraphic.Series[2 * N * 3 + 2].Points.AddXY(0, 0);
            ChartGraphic.Series[2 * N * 3 + 2].Points.AddXY(l2() * L, m2() * L);
            ChartGraphic.Series[2 * N * 3 + 2].Points[1].Label = "Y";
        }

        #endregion // +++++++

        #region buttons,mouse move,controls



        private void ContourIines_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int tZ = Z;
                if (!int.TryParse(ContourIines.Text, out tZ))
                {
                    ContourIines.Text = Z.ToString();
                    return;
                }
                if (tZ < 1 || tZ > 19)
                {
                    ContourIines.Text = Z.ToString();
                    return;
                }
                Z = tZ;
                ChartGraphic.Series.Clear();
                init();
            }
        } // ------

        private void ButtonColour_Click(object sender, EventArgs e)
        {
        }

        private void ButtonMinus_Click(object sender, EventArgs e)
        {
            if (N < 51)
            {
                N += 2;

                Z = N / 3;
                ContourIines.Text = Z.ToString();

                ChartGraphic.Series.Clear();
                update();
                init();
            }
        } // ------

        private void ButtonPlus_Click(object sender, EventArgs e)
        {
            if (N > 7)
            {
                N -= 2;

                Z = N / 3;
                ContourIines.Text = Z.ToString();

                ChartGraphic.Series.Clear();
                update();
                init();
            }
        } // ------

        private void ButtonApplication_Click(object sender, EventArgs e)
        {
            ChartGraphic.Series.Clear();
            update();
            init();
        } // ------

        private void ChartGraphic_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                onmove = true;
                startpos = e.Location;
            }
        } // ------

        private void ChartGraphic_MouseMove(object sender, MouseEventArgs e)
        {
            if (onmove)
            {
                if ((startpos.Y - e.Y) < 0) beta--;
                if ((startpos.Y - e.Y) > 0) beta++;
                if ((startpos.X - e.X) < 0) gamma--;
                if ((startpos.X - e.X) > 0) gamma++;

                if (beta > 359) beta = 0;
                if (gamma > 359) gamma = 0;
                if (beta < 0) beta = 359;
                if (gamma < 0) gamma = 359;

                drawscene();
            }
        } // ------

        private void ChartGraphic_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) onmove = false;
        } // ------

        private void ComboBoxDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
        } 

        private void ComboBoxlines_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = z0; i < z0 + Z * 3; i++) ChartGraphic.Series[i].ChartType = SeriesChartType.Spline;
            drawscene();
        } // ------

        #endregion
    }

}