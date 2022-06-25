using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ZedGraph;

namespace Mhetod_Eiler
{
    public partial class Form1 : Form
    {
        public static int n = 0, k=0;
        public static double h=0.1, x0=0, y0=1, X=2, s;
        public static Array x, y;
        public static Array x1, y1;
       

        private ZedGraphControl ZedGraph1;


        public Form1()
        {
            InitializeComponent();

            //comboBox1.Text = Convert.ToString("f(x, y)");
           // this.comboBox1.Items.AddRange(new object[] { "y+x^2", "5x+3y", "y+e^x", "sin(x)+sin(y)" });


            ZedGraph1 = zedGraphControl1;
            GraphPane pane = ZedGraph1.GraphPane;
            ZedGraph1.RestoreScale(pane);
            pane.XAxis.MajorGrid.IsVisible = true;
            pane.Title.Text = "Метод Эйлера";
            pane.XAxis.Title.Text = "x";
            pane.YAxis.Title.Text = "y";
            pane.XAxis.MajorGrid.DashOn = 10;
            pane.XAxis.MajorGrid.DashOff = 5;
            pane.YAxis.MajorGrid.IsVisible = true;
            pane.YAxis.MajorGrid.DashOn = 10;
            pane.YAxis.MajorGrid.DashOff = 5;
            pane.YAxis.MinorGrid.IsVisible = true;
            pane.YAxis.MinorGrid.DashOn = 1;
            pane.YAxis.MinorGrid.DashOff = 2;
            pane.XAxis.MinorGrid.IsVisible = true;
            pane.XAxis.MinorGrid.DashOn = 1;
            pane.XAxis.MinorGrid.DashOff = 2;

        }

        

        public double func(double x, double y, int k)
        {
            
            if (k==0)
            {
                s= y+Math.Pow(x,2);
            }
            if (k == 1)
            {
                s = 5 * x + 3 * y;
            }
            if (k == 2)
            {
                s = y + Math.Exp(x);
            }
            if (k == 3)
            {
                s= Math.Sin(x)+Math.Sin(y);
            }

            return (s);

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            k=comboBox1.SelectedIndex;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            x0=Convert.ToDouble(textBox2.Text);
            /*bool check = true;
            check &= double.TryParse(textBox2.Text, out a);
            if (!check)
            {
                MessageBox.Show("Ошибка! Проверьте формат вводимых значений!");
            }*/
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            X=Convert.ToDouble(textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            n=Convert.ToInt32(textBox4.Text);
        }

       

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            y0=Convert.ToDouble(textBox6.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            h = (X-x0)/ n;
            
            double[] x = new double[n+1];
            double[] y = new double[n+1];
            double[] x1 = new double[n + 1];
            double[] y1 = new double[n + 1];

            //Метод Эйлера

            x[0] = x0;
            y[0] = y0;

            for (int i = 0; i < n; ++i)// Цикл для заполнения массива значениями по оси Х для графика функции
            {
                x[i + 1] = x[i] + h;
                if (x[i] == X)
                    break;
            }
                       

            for (int i = 1; i < n+1; ++i)// Цикл для заполнения массива значениями по оси Y для графика функции
            {
                y[i] = y[i - 1] + h * func(x[i - 1], y[i - 1], k);
            }

            zedGraphControl1.GraphPane.CurveList.Clear();
            PointPairList list = new PointPairList();

            

            for (int j = 0; j < n+1; ++j)
            {
                list.Add(x[j], y[j]);
               
            }
            
            LineItem MyLine = zedGraphControl1.GraphPane.AddCurve("Метод Эйлера", list, Color.Red, SymbolType.None);
             MyLine.Line.Width = 3;
            // MyLine.Line.Fill = new Fill(Color.AliceBlue);
             MyLine.Symbol.IsVisible = false;
             zedGraphControl1.RestoreScale(zedGraphControl1.GraphPane);

            //Метод Эйлера с перcчетом

            x1[0] = x0;
            y1[0] = y0;

            for (int i = 0; i < n; ++i)// Цикл для заполнения массива значениями по оси Х для графика функции
            {
                x1[i + 1] = x1[i] + h;
                if (x1[i] == X)
                    break;
            }


            for (int i = 1; i < n + 1; ++i)// Цикл для заполнения массива значениями по оси Y для графика функции
            {
               y1[i] = y1[i - 1] + h/2*(func(x1[i-1],y1[i-1], k)+func(x1[i], y1[i-1]+h*func(x1[i-1], y1[i-1], k),k));

            }

            PointPairList list2 = new PointPairList();

            for (int j = 0; j < n + 1; ++j)
            {
                list2.Add(x1[j], y1[j]);

            }

            LineItem MyLine2 = zedGraphControl1.GraphPane.AddCurve("Метод Эйлера с персчетом", list2, Color.Blue, SymbolType.None);
            MyLine2.Line.Width = 3;
            // MyLine.Line.Fill = new Fill(Color.AliceBlue);
            MyLine2.Symbol.IsVisible = false;
            zedGraphControl1.RestoreScale(zedGraphControl1.GraphPane);


        }
    }
}
