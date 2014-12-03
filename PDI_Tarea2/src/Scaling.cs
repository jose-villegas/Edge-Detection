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
    public partial  class Scaling : Form
    {
        public Scaling()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            this.groupBox1.DataBindings.Add("Enabled", this.radioButton1, "Checked", false, DataSourceUpdateMode.OnPropertyChanged);
            this.groupBox2.DataBindings.Add("Enabled", this.radioButton2, "Checked", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = null;
            int width = Cache.GetCurrentBitmap().Width;
            int height = Cache.GetCurrentBitmap().Height;

            if (this.radioButton1.Checked)
            {
                width = (int)((this.numericUpDown1.Value / 100) * width);
                height = (int)((this.numericUpDown2.Value / 100) * height);
            }
            else if(this.radioButton2.Checked)
            {
                width = (int)this.numericUpDown3.Value;
                height = (int)this.numericUpDown4.Value;
            }

            if (comboBox1.SelectedIndex == 0)
            {
                bmp = ScalingAlgorithms.NearestNeighbor(Cache.GetCurrentBitmap(), width, height);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                bmp = ScalingAlgorithms.Bilinear(Cache.GetCurrentBitmap(), width, height);
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                bmp = ScalingAlgorithms.Bicubic(Cache.GetCurrentBitmap(), width, height);
            }

            if (bmp != null)
            {
                Cache.StoreCurrentBitmapData();
                ((Form1)this.Owner).setPictureBoxBitmap(bmp);
            }
            this.Hide();
        }

        private void Scaling_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                this.numericUpDown3.Value = Cache.GetCurrentBitmap().Width;
                this.numericUpDown4.Value = Cache.GetCurrentBitmap().Height;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
