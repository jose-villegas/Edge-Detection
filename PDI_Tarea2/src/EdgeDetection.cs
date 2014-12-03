using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDI_Tarea2
{
    public partial class EdgeDetection : Form
    {
        public EdgeDetection()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
            ImageFilters.LoadCurrentBitmap();
            ImageFilters.EdgeDetectionMode();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex == 1)
            {
                this.radioButton1.Enabled = true;
                this.radioButton2.Enabled = false;
                this.radioButton3.Enabled = false;
                this.radioButton4.Enabled = false;
            }

            else if (this.comboBox1.SelectedIndex == 2)
            {
                this.radioButton1.Enabled = true;
                this.radioButton2.Enabled = true;
                this.radioButton3.Enabled = true;
                this.radioButton4.Enabled = false;
            }

            else
            {
                this.radioButton1.Enabled = true;
                this.radioButton2.Enabled = true;
                this.radioButton3.Enabled = true;
                this.radioButton4.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap res = null;
            int size = 0;
            size = radioButton1.Checked ? 3 : size;
            size = radioButton2.Checked ? 5 : size;
            size = radioButton3.Checked ? 7 : size;
            size = radioButton4.Checked ? 9 : size;

            if (this.comboBox1.SelectedIndex == 0)
            {
                res = ImageFilters.ApplySobelFilter(size);
            }

            else if (this.comboBox1.SelectedIndex == 1)
            {
                res = ImageFilters.ApplyRobertsFilter(size);
            }

            else if (this.comboBox1.SelectedIndex == 2)
            {
                res = ImageFilters.ApplyPrewittFilter(size);
            }

            else if (this.comboBox1.SelectedIndex == 3)
            {
                res = ImageFilters.ApplyLaplacianOfGaussianFilter(size);
            }

            if (size != 0 && res != null)
            {
                Cache.SetMainformPictureBox(res);
                Cache.StoreCurrentBitmapData();
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
