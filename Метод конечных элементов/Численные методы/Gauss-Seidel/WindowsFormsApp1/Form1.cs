using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); // Завершение процесса приложения
        }
        

        static void Division(ref double[,] a, ref double b, double del, int i) // функция приближения значений
        {
            for (int j = 0; j < 3; j++) // цикл по всем элементам
                a[i, j] = -a[i, j] / del;
            b = b / del;
        }
        static double Summary(double[,] c, double f, double[] x, int i) // функция суммы
        {
            double y = f;
            for (int j = 0; j < 3; j++)// цикл по всем элементам
                y += c[i, j] * x[j];
            return y;
        }
        static void Seidel(double[,] A, double[] B, ref double[] X) // метод Зейделя. A - матрица системы, B - правая часть равенства, X - результирующая
        {
            double[] Y = new double[3];
            double N = X[0];
            double Z = 0;
            string res = "";
            int iter = 1;
            do // Цикл будет работать, пока значения не приблизятся к точности в 0,01
            {
                res += "Итерация " + iter + '\n';
                for (int i = 0; i < 3; i++)
                {
                    
                    for (int j = 0; j < 3; j++)
                    {
                        if (i == j)
                        {
                            Z = A[i, j];
                            A[i, j] = 0;
                            Y[i] = Summary(A, B[i], X, i);
                            A[i, j] = Z;
                            if (N > Math.Abs(X[i] - Y[i])) // проверка точности
                                N = Math.Abs(X[i] - Y[i]);
                            X[i] = Y[i];
                        }
                       
                    }
                    res += Convert.ToString(X[i]) + "   ";
                   

                }
                res += "\n";
                iter++;
            }
            while (N > 0.01);
            // Печать итераций в файл
            string writePath = @"RES.txt";            
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(res);
                } 

                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            } // Трай - на случай отказа в правах
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double[,] A = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            double[] B = { 0, 0, 0 };
            double[] X;
            X = new double[3];
            //Матрица А для Левой части выражения, В для правой
            A[0, 0] = Convert.ToDouble(textBox1.Text);
            A[0, 1] = Convert.ToDouble(textBox2.Text);
            A[0, 2] = Convert.ToDouble(textBox3.Text);
            A[1, 0] = Convert.ToDouble(textBox5.Text);
            A[1, 1] = Convert.ToDouble(textBox6.Text);
            A[1, 2] = Convert.ToDouble(textBox7.Text);
            A[2, 0] = Convert.ToDouble(textBox9.Text);
            A[2, 1] = Convert.ToDouble(textBox10.Text);
            A[2, 2] = Convert.ToDouble(textBox11.Text);
            B[0] = Convert.ToDouble(textBox4.Text);
            B[1] = Convert.ToDouble(textBox8.Text);
            B[2] = Convert.ToDouble(textBox12.Text);
            double Del;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    if (i == j)
                    {
                        Del = A[i, j];
                        Division(ref A, ref B[i], Del, i);
                        X[i] = B[i];
                    }
            }
            Seidel(A, B, ref X);     // А и В - входные данные, Х - выходная матрица с корнями
            label10.Text = " " + Convert.ToString(Math.Round(X[0], 3));
            label12.Text = " " + Convert.ToString(Math.Round(X[1], 3));
            label13.Text = " " + Convert.ToString(Math.Round(X[2], 3));

            MessageBox.Show("Инетрационный процесс записан в файл");
        }


        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString("");
            textBox2.Text = Convert.ToString("");
            textBox3.Text = Convert.ToString("");
            textBox4.Text = Convert.ToString("");
            textBox5.Text = Convert.ToString("");
            textBox6.Text = Convert.ToString("");
            textBox7.Text = Convert.ToString("");
            textBox8.Text = Convert.ToString("");
            textBox9.Text = Convert.ToString("");
            textBox10.Text = Convert.ToString("");
            textBox11.Text = Convert.ToString("");
            textBox12.Text = Convert.ToString("");
            label10.Text="";
            label12.Text = "";
            label13.Text = "";

    }

    private void label26_Click(object sender, EventArgs e)
    {

    }
  }
}
