using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImagingPS
{
    public partial class FormShowBitmap : Form
    {
        public FormShowBitmap()
        {
            InitializeComponent();
        }

        public System.Drawing.Image Image
        {
            get { return this.pictureBox1.Image;  }
            set { this.pictureBox1.Image = value;  }
        }
    }

}
