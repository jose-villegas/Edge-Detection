using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDI_Tarea2
{
    public partial class Threshold : Form
    {
        int[] hist = new int[256];
        private Bitmap bitmap;
        private Bitmap histogramOrig;

        public Threshold()
        {
            InitializeComponent();
            this.numericUpDown1.DataBindings.Add("Value", this.hScrollBar1, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
            this.bitmap = Colors.ToGrayScale(Cache.GetCurrentBitmap());
            calculateHistogram(Cache.GetByteDataFromBitmap(bitmap));
            drawHistogram();
            updateHistogramDivision(hScrollBar1.Value);
        }

        private void drawHistogram()
        {
            if (bitmap != null)
            {
                Bitmap bmp = new Bitmap(255, 150);
                Graphics orig = Graphics.FromImage(bmp);
                Point P1 = new Point(0, 150);
                Point P2 = new Point(0, 150);
                Pen pen = new Pen(Color.Black);
                int max = hist.Max();

                for (int i = 0; i < 256; i++)
                {
                    P1.X = P2.X = i;
                    P2.Y = 150 - (int)(150 * (float)hist[i] / max);
                    orig.DrawLine(pen, P1, P2);
                }

                orig.Dispose();
                histogramOrig = (Bitmap)bmp.Clone();
                pictureBox1.Image = bmp;
                pictureBox1.Refresh();
            }
        }

        private void updateHistogramDivision(int value)
        {
            if (bitmap != null)
            {
                Bitmap tmp = (Bitmap)histogramOrig.Clone();
                Graphics graph = Graphics.FromImage(histogramOrig);
                Point P1 = new Point(value, 0);
                Point P2 = new Point(value, 150);
                Pen pen = new Pen(Color.Red);
                graph.DrawLine(pen, P1, P2);
                graph.Dispose();
                pictureBox1.Image = histogramOrig;
                pictureBox1.Refresh();
                histogramOrig = tmp;
            }
        }

        private void calculateHistogram(byte[] rgbGreyData)
        {
            if (bitmap != null)
            {
                for (int i = 0; i < rgbGreyData.Length; i++)
                {
                    ++hist[rgbGreyData[i]];
                }
            }
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            updateHistogramDivision(this.hScrollBar1.Value);
            Cache.SetMainformPictureBox(Colors.OtsuThreshold(bitmap, hScrollBar1.Value));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cache.StoreCurrentBitmapData();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cache.ToPreviousBitmapData();
            this.Close();
        }

    }
}
