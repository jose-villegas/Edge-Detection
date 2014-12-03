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
    public partial class Brightness_Contrast : Form
    {
        private Bitmap bitmap;

        public Brightness_Contrast()
        {
            InitializeComponent();
            this.numericUpDown1.DataBindings.Add("Value", this.hScrollBar1, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
            this.numericUpDown2.DataBindings.Add("Value", this.hScrollBar2, "Value", false, DataSourceUpdateMode.OnPropertyChanged);
            this.bitmap = Cache.GetCurrentBitmap();
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            Cache.SetMainformPictureBox(Colors.BrigthnessAndContrast(bitmap, hScrollBar1.Value, hScrollBar2.Value));
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
