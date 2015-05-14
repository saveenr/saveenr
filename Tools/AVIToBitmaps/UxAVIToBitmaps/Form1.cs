using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UxBitmapsToAVI
{
    public partial class FormBitmapsToAVI : Form
    {
        public FormBitmapsToAVI()
        {
            InitializeComponent();
        }


        private void buttonCreateAVI_Click(object sender, EventArgs e)
        {
            this.Log("Starting");
            string src_folder = this.textBoxBitmapsFolder.Text.Trim();
            string out_filename = this.textBoxAVIFile.Text.Trim();

            this.Log("folder = \"{0}\"", src_folder );


            if (src_folder.Length<1)
            {
                this.Log("folder must enter folder");
                this.LogFailure();
                return;
            }

            if (! System.IO.Directory.Exists(src_folder))
            {
                this.Log("folder does not exist");
                this.LogFailure();
                return;
            }
            if (System.IO.File.Exists(src_folder))
            {
                this.Log("this is a file nor a folder");
                this.LogFailure();
                return;
            }

            this.Log("output file = \"{0}\"", out_filename );
            if (out_filename.Length < 1)
            {
                this.Log("must enter out filename");
                this.LogFailure();
                return;
            }

            string [] bitmap_files = System.IO.Directory.GetFiles(src_folder, "*.png");
            this.Log("Number of bitmaps = {0}", bitmap_files.Length);

            // WORKITEM sort the names
            // WORKITEM print the names?
            // WORKITEM warn about different sizes
            // WORKITEM show dimensions

            Bitmap bmp = (Bitmap)Image.FromFile(bitmap_files[0]);
            AviFile.AviManager aviManager=null;
            try
            {
                aviManager = new AviFile.AviManager(out_filename, false);
            }
            catch (System.Exception ex)
            {
                this.Log(ex.Message);
                this.Log(ex.Source);
                this.Log("Could not create AviManager object");
                this.Log("Sometimes this means the AVI file is open in an app (like WMP). CLose any apps using it and try again.");
                this.LogFailure();
                return;
            }

            bool compress = this.checkBoxUseCompression.Checked;
            this.Log("Compression setting = {0}", compress);

            this.Log("Creating AVIManager");
            AviFile.VideoStream aviStream = aviManager.AddVideoStream( compress, 10, bmp);

            Bitmap bitmap;
            int count = 0;
            for (int n = 1; n < bitmap_files.Length; n++)
            {
                this.Log("Frame {0} Loading bitmap {1} ", n, bitmap_files[n]);
                bitmap = (Bitmap)Bitmap.FromFile(bitmap_files[n]);
                this.Log("Adding bitmap frame");
                aviStream.AddFrame(bitmap);
                bitmap.Dispose();
                count++;
            }

            this.Log("Closing Avi Manger");
            aviManager.Close();
            this.Log("Done.");


        }


        private void Log(string fmt, params object[] a)
        {
            string s = string.Format(fmt, a);
            this.textBox1.AppendText(s+"\r\n");
        }
        private void LogFailure()
        {
            this.Log("FAILED. Stopping");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}