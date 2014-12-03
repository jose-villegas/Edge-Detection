using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDI_Tarea2
{
    public partial class Rotation : Form
    {
        Bitmap bitmap;

        public Rotation()
        {
            InitializeComponent();
            DrawRotationCircle();
            bitmap = Cache.GetCurrentBitmap();
        }

        private void DrawRotationCircle()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawEllipse(new Pen(Color.Black), new Rectangle(0, 0, bmp.Width - 1, bmp.Height - 1));
            g.DrawLine(new Pen(Color.Red), new Point((bmp.Width - 1) / 2, (bmp.Width - 1) / 2), new Point(150, (bmp.Width - 1) / 2));
            g.Dispose();
            pictureBox1.Image = bmp;
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawEllipse(new Pen(Color.Black), new Rectangle(0, 0, bmp.Width - 1, bmp.Height - 1));
                // Calculamos el punto de interseccion entre la recta generada por dos puntos
                //Trasladamos las coordenadas a 0,0
                double mouseX = e.X - (bmp.Width - 1) / 2;
                double mouseY = e.Y - (bmp.Width - 1) / 2;
                double calc = Math.Pow((bmp.Width - 1) / 2, 2) / Math.Sqrt(Math.Pow((bmp.Width - 1) / 2, 2) * (Math.Pow(mouseY, 2) + Math.Pow(mouseX, 2)));
                // Calculamos y trasladamos otra vez el origen del circulo
                int finalX = (int) (mouseX * calc + (bmp.Width - 1) / 2);
                int finalY = (int) (mouseY * calc + (bmp.Width - 1) / 2);
                // Dibujamos la linea
                g.DrawLine(new Pen(Color.Red), new Point((bmp.Width - 1) / 2, (bmp.Width - 1) / 2), new Point(finalX, finalY));
                g.Dispose();
                pictureBox1.Image = bmp;
                pictureBox1.Refresh();
                // Calculamos el angulo de rotacion
                double Dy = e.Y - (bmp.Width - 1) / 2;
                double Dx = e.X - (bmp.Width - 1) / 2;
                double angle = Math.Atan2(Dy, Dx);
                angle *= -180 / Math.PI;
                Cache.SetMainformPictureBox(Rotate.FreeRotationNearestNeighbor(bitmap, angle));
                // Mostramos el angulo
                numericUpDown1.Value = (Decimal)angle;
            }
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
