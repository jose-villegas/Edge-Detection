
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDI_Tarea2
{
    partial class Histogram : Form
    {
        int[][] hist;
        int max;
        public Histogram()
        {
            InitializeComponent();
            loadRGBHistogram(Colors.GetRGBHistogram(Cache.GetCurrentBitmap()));
            showRGBHistogram();
        }

        public void loadRGBHistogram(int[][] hist)
        {
            this.hist = hist;
            this.max = Math.Max(hist[0].Max(), Math.Max(hist[1].Max(), hist[0].Max()));
        }

        public void showRGBHistogram()
        {
            Bitmap bitmap = new Bitmap(255, 150);
            Graphics graph = Graphics.FromImage(bitmap);
            Point P1 = new Point(0, 150);
            Point P2 = new Point(0, 150);
            Pen pen = new Pen(Color.Black);
            int currentAmount = 0;

            for (int i = 0; i < 256; i++)
            {
                if (radioButton1.Checked)
                {
                    pen.Color = Color.Red;
                    currentAmount =  hist[0][i];
                }

                if (radioButton2.Checked)
                {
                    pen.Color = Color.Green;
                    currentAmount = hist[1][i];
                }

                if (radioButton3.Checked)
                {
                    pen.Color = Color.Blue;
                    currentAmount = hist[2][i];
                }

                P1.X = P2.X = i;
                P2.Y = 150 - (int)(150 * (float)currentAmount / max);
                graph.DrawLine(pen, P1, P2);
            }

            Debug.Write("\n");
            graph.Dispose();
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            showRGBHistogram();
        }
    }
}
