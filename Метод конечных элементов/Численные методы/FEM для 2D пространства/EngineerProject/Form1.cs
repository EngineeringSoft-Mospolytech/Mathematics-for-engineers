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
using System.Runtime.InteropServices;
using EarcutNet;
using System.Reflection;

namespace EngineerProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public struct PointD
        {
            public double X, Y;

            public PointD(double x, double y)
            {
                X = x;
                Y = y;
            }
        }
        public double[] Points;
        //public PointD[] Points;

        [DllImport("FEM.dll")]
        public extern static void Creat();
        /*
        private void button1_Click(object sender, EventArgs e)
        {
            Creat();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var ss = File.ReadAllLines(openFileDialog1.FileName);
                if (ss.Length > 512)//ДРУГОЕ ЧИСЛО
                {
                    MessageBox.Show("В файле недостаточно параметров.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] datas = ss[0].Replace('.', ',').Split(' ');

                double data1 = double.Parse(datas[0]);
                double data2 = double.Parse(datas[1]);

                double[] Points = new double[int.Parse(ss[1]) * 2];

                int n = 0;
                for (int i = 0; i < int.Parse(ss[1]); i++)
                {
                    datas = ss[i + 2].Replace('.', ',').Split(' ');
                    Points[n] = double.Parse(datas[0]);
                    n++;
                    Points[n] = double.Parse(datas[1]);
                    n++;
                }

            }

        }
        */

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Точка " + numericUpDown3.Value + " тип " + numericUpDown4.Value);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            listBox2.Items.Remove(listBox2.SelectedItem);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add("Точка " + numericUpDown6.Value + " X " + P(numericUpDown5.Value) + " Y " + P(numericUpDown7.Value));
        }

        string P(decimal v)
        {
            string s = v.ToString();
            if (s.IndexOf(',') == -1)
            {
                s += ".0";
            }
            else s = s.Replace(',', '.');
            return s;
        }

        string P2(double v)
        {
            string s = v.ToString();
            if (s.IndexOf(',') == -1)
            {
                s += ".0";
            }
            else s = s.Replace(',', '.');
            return s;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> ss = new List<string>();
            ss.Add(P(numericUpDown1.Value) + " " + P(numericUpDown2.Value) + " " + P(numericUpDown12.Value));
            Points = new double[listBox3.Items.Count * 2];
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                var ss3 = listBox3.Items[i].ToString().Split(' ');
                Points[i * 2] = double.Parse(ss3[1].Replace('.', ','));
                Points[i * 2 + 1] = double.Parse(ss3[3].Replace('.', ','));
            }
            ss.Add((Points.Length / 2).ToString());
            for (int i = 0; i < Points.Length; i += 2) ss.Add(P2(Points[i]) + " " + P2(Points[i + 1]));

            var g = pictureBox1.CreateGraphics();
            Pen p = new Pen(Color.Gray);
            int[] tr = EarcutNet.Earcut.Tessellate(Points, new int[] { 4 }).ToArray();//добавить уточнение в проге
            ss.Add((tr.Length / 3).ToString());
            float SIZE = (float)numericUpDown10.Value;
            float DSIZE = (float)numericUpDown11.Value;
            for (int i = 0; i < tr.Length; i += 3)
            {
                ss.Add(tr[i] + " " + tr[i + 1] + " " + tr[i + 2]);
                g.DrawPolygon(p, new PointF[]
                {
                    new PointF((float)Points[tr[i] * 2] * SIZE + pictureBox1.Width / 4, (float)Points[tr[i] * 2 + 1] * SIZE + pictureBox1.Height / 4),
                    new PointF((float)Points[tr[i + 1] * 2] * SIZE + pictureBox1.Width / 4, (float)Points[tr[i + 1] * 2 + 1] * SIZE + pictureBox1.Height / 4),
                    new PointF((float)Points[tr[i + 2] * 2] * SIZE + pictureBox1.Width / 4, (float)Points[tr[i + 2] * 2 + 1] * SIZE + pictureBox1.Height / 4)
                }
                );
            }
            ss.Add(listBox1.Items.Count.ToString());
            Pen pb = new Pen(Color.Black);
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                var spl = listBox1.Items[i].ToString().Split(' ');
                ss.Add(spl[1] + " " + spl[3]);
                int N = int.Parse(spl[1]);
                var X = (float)Points[N * 2] * SIZE + pictureBox1.Width / 4;
                var Y = (float)Points[N * 2 + 1] * SIZE + pictureBox1.Height / 4;
                g.DrawEllipse(pb, X - 4, Y - 4, 8f, 8f);
                if (spl[3][0] == '1') g.DrawLine(pb, X, Y, X + 5, Y);
                if (spl[3][0] == '2') g.DrawLine(pb, X, Y, X, Y + 5);
            }
            ss.Add(listBox2.Items.Count.ToString());
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                var spl = listBox2.Items[i].ToString().Split(' ');
                ss.Add(spl[1] + " " + spl[3] + " " + spl[5]);
            }
            File.WriteAllLines("MKV.inp", ss.ToArray());

            Creat();
            string[] LinesNew = File.ReadAllLines("MKV.pil");

            double[] Offsets = new double[Points.Length];
            for (int i = 0; i < Points.Length; i++) Offsets[i] = double.Parse(LinesNew[i + Points.Length + 4].Replace('.', ','));

            Pen p2 = new Pen(Color.Red, 0.1f);
            for (int i = 0; i < tr.Length; i += 3)
            {
                g.DrawPolygon(p2, new PointF[]
                {
                    new PointF(
                        (float)Points[tr[i] * 2] * SIZE + DSIZE * (float)Offsets[tr[i] * 2] + pictureBox1.Width / 4,
                        (float)Points[tr[i] * 2 + 1] * SIZE + DSIZE * (float)Offsets[tr[i] * 2 + 1] + pictureBox1.Height / 4
                        ),
                    
                    new PointF(
                        (float)Points[tr[i + 1] * 2] * SIZE + DSIZE * (float)Offsets[tr[i + 1] * 2] + pictureBox1.Width / 4,
                        (float)Points[tr[i + 1] * 2 + 1] * SIZE + DSIZE * (float)Offsets[tr[i + 1] * 2 + 1] + pictureBox1.Height / 4
                        ),
                    
                    new PointF(
                        (float)Points[tr[i + 2] * 2] * SIZE + DSIZE * (float)Offsets[tr[i + 2] * 2] + pictureBox1.Width / 4,
                        (float)Points[tr[i + 2] * 2 + 1] * SIZE + DSIZE * (float)Offsets[tr[i + 2] * 2 + 1] + pictureBox1.Height / 4
                        )
                }
                );
            }

            Pen p3 = new Pen(Color.Aqua, 0.1f);
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                var spl = listBox2.Items[i].ToString().Split(' ');
                var x = (float)Points[int.Parse(spl[1]) * 2] * SIZE + pictureBox1.Width / 4;
                var y = (float)Points[int.Parse(spl[1]) * 2 + 1] * SIZE + pictureBox1.Height / 4;
                g.DrawEllipse(p3, x - 4, y - 4, 8f, 8f);
                g.DrawLine(p3, x, y, x + float.Parse(spl[3].Replace('.', ',')) * SIZE, y + float.Parse(spl[5].Replace('.', ',')) * SIZE);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("Точка 0 тип 3");
            listBox1.Items.Add("Точка 1 тип 2");
            listBox2.Items.Add("Точка 2 X 0.0 Y 1.0");
            listBox2.Items.Add("Точка 3 X 0.0 Y 1.0");
            listBox3.Items.Add("X 0.0 Y 0.0");
            listBox3.Items.Add("X 10.0 Y 0.0");
            listBox3.Items.Add("X 10.0 Y 10.0");
            listBox3.Items.Add("X 0.0 Y 10.0");
            listBox3.Items.Add("X 2.0 Y 2.0");
            listBox3.Items.Add("X 8.0 Y 2.0");
            listBox3.Items.Add("X 8.0 Y 8.0");
            listBox3.Items.Add("X 2.0 Y 8.0");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox3.Items.Add(" X " + P(numericUpDown9.Value) + " Y " + P(numericUpDown8.Value));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox3.Items.Remove(listBox3.SelectedItem);
        }
    }
}
