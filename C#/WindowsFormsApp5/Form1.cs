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
using System.Threading;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        Graphics gr;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(256, 256);
            gr = Graphics.FromImage(pictureBox1.Image);
            gr.Clear(Color.White);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        Pen pen1 = new Pen(Color.Black, 20);
        Pen pen2 = new Pen(Color.White, 30);
        Brush brush1 = new SolidBrush(Color.Black);
        Brush brush2 = new SolidBrush(Color.White);
        float old_x = 0;
        float old_y = 0;

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                gr.FillEllipse(brush1, e.X, e.Y,
                   15, 15);
                pictureBox1.Refresh();
            }
            if (e.Button == MouseButtons.Right)
            {
                gr.FillEllipse(brush2, e.X, e.Y,
                    15,15);
                pictureBox1.Refresh();
            }
            old_x = e.X;
            old_y = e.Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap a = new Bitmap(pictureBox1.Image, 64, 64);
            string fileName = "test.txt";
            FileStream aFile = new FileStream(fileName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(aFile);
            aFile.Seek(0, SeekOrigin.End);
            for (int y = 0; y < a.Height; y++)
            {
                for (int x = 0; x < a.Width; x++)
                {
                    sw.Write(1 - a.GetPixel(x, y).GetBrightness() + " ");
                }
                sw.WriteLine();
            }
            sw.Write(textBox1.Text);
            sw.WriteLine();
            sw.Close();
            pictureBox2.Image = new Bitmap(pictureBox1.Image, 64, 64);
            pictureBox2.Refresh();
            gr.Clear(Color.White);
            pictureBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Image a = pictureBox2.Image;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gr.Clear(Color.White);
            pictureBox1.Refresh();

        }

        NeuralNet.NeuronNetwork nn;

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Logs.Text = "";
                FileStream file1 = new FileStream(pathBox.Text, FileMode.OpenOrCreate); //создаем файловый поток
                StreamReader reader = new StreamReader(file1);
                FileStream file2 = new FileStream("logs.txt", FileMode.OpenOrCreate); //создаем файловый поток
                StreamWriter writer = new StreamWriter(file2);
                int[] str = (structBox.Text.Split(' ').Select(s => int.Parse(s)).ToArray());
                nn = new NeuralNet.NeuronNetwork(str);
                List<List<double>> tests = new List<List<double>>();
                while (!reader.EndOfStream)
                {
                    tests.Add(reader.ReadLine().Split(' ').Select(x => double.Parse(x)).ToList());
                }
                reader.Close();
                int c = 0;
                double error = 0;
                long co = 0;
                double E = Convert.ToDouble(eBox.Text);
                double a = Convert.ToDouble(aBox.Text);
                while (true)
                {
                    nn.MORRelearn(tests, E, a);
                    error = nn.getWrong(tests);
                    if (c == 10)
                    {
                        writer.WriteLine("Generation: " + co + " : " +error);
                        c = 0;
                    }
                    if (error < 0.1)
                    {
                        Logs.Text += "НЕЙРОСЕТЬ ОБУЧЕНА!";
                        Logs.Text += "\nОшибка: " + error;
                        Logs.Text += "\nКол-во циклов обучения:" + co;
                        writer.Close();
                        return;
                    }
                    c++;
                    co++;
                }
            }
            catch(Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        void Learn()
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                outBox.Text = "";
                double[] out1 = nn.Go(inBox.Text.Split(' ').Select(x => double.Parse(x)).ToList());
                for (int x = 0; x < out1.Length; x++)
                    outBox.Text += out1[x] + " ";
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
                int[] str = (structBox.Text.Split(' ').Select(s => int.Parse(s)).ToArray());
                nn = new NeuralNet.NeuronNetwork(str);
                nn.LoadWeight(pathBox2.Text);
                nn.SaveWeight("test1.txt");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int[] str = (structBox.Text.Split(' ').Select(s => int.Parse(s)).ToArray());
            nn = new NeuralNet.NeuronNetwork(str);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<double> test = new List<double>();
            Bitmap a = new Bitmap(pictureBox1.Image, 64, 64);
            for (int y = 0; y < a.Height; y++)
            {
                for (int x = 0; x < a.Width; x++)
                {
                    test.Add(1 - a.GetPixel(x, y).GetBrightness());
                }
            }
            nn.Go(test);
            outLabel.Text = "";
            for (int x = 0; x < nn.output.Length; x++)
                outLabel.Text += Convert.ToChar(x + 65) + " : " + nn.output[x].ToString("0.00") + " \n";
        }
    }
}
