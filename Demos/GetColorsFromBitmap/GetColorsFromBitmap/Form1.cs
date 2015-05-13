using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GetColorsFromBitmap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.textBoxInputBitmap.Text = Properties.Settings.Default.DefInputImage;
            this.textBoxOutputFile.Text = Properties.Settings.Default.DefOutputFilename;
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            string infile = this.textBoxInputBitmap.Text.Trim();
            string outfile = this.textBoxOutputFile.Text.Trim();
            Properties.Settings.Default.DefInputImage = infile;
            Properties.Settings.Default.DefOutputFilename = outfile;
            Properties.Settings.Default.Save();
            if (infile.Length < 1)
            {
                return;
            }

            if (outfile.Length<1)
            {
                return;
            }

            var bmp = new System.Drawing.Bitmap(infile);

            var hist = new int[256*256*256];

            var fo = System.IO.File.CreateText(outfile);
            foreach (int x in Enumerable.Range(0,bmp.Width))
            {
                foreach (int y in Enumerable.Range(0,bmp.Height))
                {
                    var pixel = bmp.GetPixel(x, y);
                    if (this.checkBoxUseOnlyOpauqePixels.Checked)
                    {
                        if (pixel.A !=255)
                        {
                            continue;
                        }
                    }
                    int index = pixel.R <<  16 | pixel.G << 8 | pixel.B;
                    hist[index] += 1;
                }
                
            }

            fo.WriteLine("{0},{1},{2},{3},{4}", "hex","r","g","b","count");
            foreach (var i in Enumerable.Range(0, hist.Length))
            {
                var n = hist[i];
                if (n>0)
                {
                    fo.WriteLine("#{0},{1},{2},{3},{4}", i.ToString("X6"), n & 0xff0000 >> 16, i & 0xff00>>8, i & 0xff, n);
                }
            }
            fo.Close();
        }

        private void buttonBrowseInput_Click(object sender, EventArgs e)
        {
            var form = new OpenFileDialog();
            form.Filter = "PNG files|*.png|BMP files|*.bmp|All files (*.*)|*.*";
            var res = form.ShowDialog();
            if (res==DialogResult.OK)
            {
                this.textBoxInputBitmap.Text = form.FileName;
            }
        }

        private void buttonBrowseOutput_Click(object sender, EventArgs e)
        {
            var form = new OpenFileDialog();
            form.Filter = "TXT files|*.txt|All files (*.*)|*.*";
            var res = form.ShowDialog();
            if (res == DialogResult.OK)
            {
                this.textBoxOutputFile.Text = form.FileName;
            }
        }
    }
}
