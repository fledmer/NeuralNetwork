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
                //NEURONDROW();
            }
            if (e.Button == MouseButtons.Right)
            {
                gr.FillEllipse(brush2, e.X, e.Y,
                    15,15);
                pictureBox1.Refresh();
                //NEURONDROW();
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
         
        }

        void Learn()
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int[] str = (structBox.Text.Split(' ').Select(s => int.Parse(s)).ToArray());
                nn = new NeuralNet.NeuronNetwork(str);
            }
            catch
            {
                MessageBox.Show("Невозможно создать нейросеть");
            }
            try
            {
                
                    nn.LoadWeight(pathBox2.Text);
                    nn.SaveWeight("test1.txt");
                
            }
            catch
            {
                MessageBox.Show("Невозможно загрузить веса");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                int[] str = (structBox.Text.Split(' ').Select(s => int.Parse(s)).ToArray());
                nn = new NeuralNet.NeuronNetwork(str);
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                NEURONDROW();
            }
            catch
            {
                MessageBox.Show("ERROR");
            }
        }

        private void NEURONDROW()
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
            label1.Text = "A: " + nn.output[0].ToString("0.00");
            label4.Text = "B: " + nn.output[1].ToString("0.00");
            label6.Text = "C:" + nn.output[2].ToString("0.00");
            label5.Text = "D: " + nn.output[3].ToString("0.00");
            label10.Text = "E: " + nn.output[4].ToString("0.00");
            label9.Text = "F: " + nn.output[5].ToString("0.00");
            label8.Text = "G: " + nn.output[6].ToString("0.00");
            label7.Text = " H: " + nn.output[7].ToString("0.00");
            label18.Text = "I: " + nn.output[8].ToString("0.00");
            label17.Text = "J: " + nn.output[9].ToString("0.00");
            label16.Text = "K: " + nn.output[10].ToString("0.00");
            label15.Text = "I: " + nn.output[11].ToString("0.00");
            label14.Text = "M: " + nn.output[12].ToString("0.00");
            label13.Text = "N: " + nn.output[13].ToString("0.00");
            label12.Text = "O: " + nn.output[14].ToString("0.00");
            label11.Text = "P: " + nn.output[15].ToString("0.00");
            label26.Text = "Q: " + nn.output[16].ToString("0.00");
            label25.Text = "R: " + nn.output[17].ToString("0.00");
            label24.Text = "S: " + nn.output[18].ToString("0.00");
            label23.Text = "T: " + nn.output[19].ToString("0.00");
            label22.Text = "U: " + nn.output[20].ToString("0.00"); 
            label21.Text = "V: " + nn.output[21].ToString("0.00");
            label20.Text = "W: " + nn.output[22].ToString("0.00");
            label19.Text = "X: " + nn.output[23].ToString("0.00");
            label28.Text = "Y: " + nn.output[24].ToString("0.00");
            label27.Text = "Z: " + nn.output[25].ToString("0.00");

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
