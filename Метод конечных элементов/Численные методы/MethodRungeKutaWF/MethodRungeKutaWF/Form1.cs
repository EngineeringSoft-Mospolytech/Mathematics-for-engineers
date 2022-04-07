using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MethodRungeKutaWF
{
    public partial class Form1 : Form
    {

        private static double
        x = 0.0,
        y = 1.0,
        h=0.001,
        a=-50.0;
        
        
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double hCheck = 2.75 / (-1 * a);
            textBox4.Text = Convert.ToString(Convert.ToDouble(hCheck).ToString("N4"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double  f, g, g1, g2, g3, g4, v, ee;

            int n = 10;            // Количество итераций 
            double Function_f(double c, double b)
            {
                return f = -50 * b;  // Заданная функция, где х=а, у=b
            }
            for (int i = 1; i <= n; i++)
            {
                v = Math.Exp(a * x);
                ee = Math.Abs(v - y) / v;
               // Console.Write($"x={x}; y={y}; v={v}; e={e}\n");

                g1 = h * Function_f(x, y);
                g2 = h * Function_f(x + h / 2, y + (g1 / 2));
                g3 = h * Function_f(x + h / 2, y + (g2 / 2));
                g4 = h * Function_f(x + h, y + g3);
                g = (g1 + 2 * g2 + 2 * g3 + g4) / 6;
                y = y + g;
                textBox5.Text = Convert.ToString(Convert.ToDouble(g1).ToString("N4"));
                textBox6.Text = Convert.ToString(Convert.ToDouble(g2).ToString("N4"));
                textBox7.Text = Convert.ToString(Convert.ToDouble(g3).ToString("N4"));
                textBox8.Text = Convert.ToString(Convert.ToDouble(g4).ToString("N4"));
             
                x = x + h;
              
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            x = 0.0;
            y = 1.0;
            double hCheck = 2.75 / (-1 * a);

            textBox1.Text = Convert.ToString(Convert.ToDouble(x).ToString("N4"));
            textBox2.Text = Convert.ToString(Convert.ToDouble(y).ToString("N4"));
            textBox3.Text = Convert.ToString(Convert.ToDouble(hCheck).ToString("N4"));
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            bool check = true;
            check &= double.TryParse(textBox2.Text, out y);
            if (!check)
            {
                MessageBox.Show("Ошибка! Проверьте формат вводимых значений!");
            }
        }

        

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            bool check = true;
            check &= double.TryParse(textBox3.Text, out h);
            if (!check)
            {
                MessageBox.Show("Ошибка! Проверьте формат вводимых значений!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bool check = true;
            check &= double.TryParse(textBox1.Text, out x);
            if (!check)
            {
                MessageBox.Show("Ошибка! Проверьте формат вводимых значений!");
            }
        }
    }
}
