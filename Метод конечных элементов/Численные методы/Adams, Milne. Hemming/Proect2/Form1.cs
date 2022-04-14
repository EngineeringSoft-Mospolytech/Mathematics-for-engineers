using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proect2
{
    public partial class Form1 : Form
    {

        static double G(double X, double Y, double YP) // Функция G
        {
            X = (2 * 3.14159265358979) * (2 * 3.14159265358979) * Y;
            Y = (2 * 3.14159265358979) * (2 * 3.14159265358979) * Y;
            YP = (2 * 3.14159265358979) * (2 * 3.14159265358979) * Y;
            return YP;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // Рассчет
        {
            double A,  E,  H,  T,  V,  X, Y,  OMEGA, OMEGA2, Y0, YP0, Y1 = 0, YS1 = 0, YP1 = 0, YP = 0, YS = 0; // Инициализация всех переменных
            OMEGA = 2 * 3.14159265358979;
            OMEGA2 = OMEGA * OMEGA;
            Y0 = 1;
            YP0 = 0;
            A = 1 / 6;
            T = 1 / 2;
            H = 0.01;
            X = 0;
            Y = Y0;
            YP = YP0;
            YS = G(X, Y, YP);
            for (int I = 1; I <= 10; I++)// Реализация цикла DO
            {
                V = Math.Cos(OMEGA * X);
                E = Math.Abs(V - Y) / V;
                YS1 = YS;
                Y1 = Y + H * YP + 0.5 * H * H * (A * YS1 + (1 - A) * YS);
                YP1 = YP + H * (T * YS1 + (1 - T) * YS);
                YS1 = G(X + H, Y1, YP1);
                Y1 = Y + H * YP + 0.5 * H * H * (A * YS1 + (1 - A) * YS);
                YP1 = YP + H * (T * YS1 + (1 - T) * YS);
                YS1 = G(X + H, Y1, YP1);
                Y1 = Y + H * YP + 0.5 * H * H * (A * YS1 + (1 - A) * YS);
                YP1 = YP + H * (T * YS1 + (1 - T) * YS);
                X = X + H;
                Y = Y1;
                YP = YP1;
                YS = G(X, Y, YP);
            }
            textBox5.Text = Convert.ToString(Y1); // Вывод итогов 1
            textBox6.Text = Convert.ToString(YP1);// Вывод итогов 2
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // Очищаем поля 
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }
    }
}
