using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


/*
 * 
 * HISTORY
 * -------
 * 
 * 2005-04-29
 * Comments added
 * 
 * 2005-04-29
 * Moved AVI creation to separate method
 * 
 * 2005-04-29
 * AVI creation now uses the frames per second field
 * 
 * 2005-04-28
 * Initial Version
 * 
 *  * 
 * */

namespace UxBitmapsToAVI
{
    public partial class FormBitmapsToAVI : Form
    {
        public FormBitmapsToAVI()
        {
            InitializeComponent();

            string mydocs = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string default_avi_filename = System.IO.Path.Combine( mydocs, "input.avi" );
            string default_output_folder = System.IO.Path.Combine(mydocs, "output-frames");

            this.ucDirectoryPickerOutputFolder.Directory = default_output_folder ;
            this.ucFilePickerInputAviFile.Filename = default_avi_filename;

        }

        private Bitmap load_image(string fname)
        {
            Bitmap bmp = (Bitmap)Image.FromFile(fname);
            return bmp;
        }

        private void buttonCreateAVI_Click(object sender, EventArgs e)
        {
            this.Log("Starting");

            string output_folder = this.ucDirectoryPickerOutputFolder.Directory;
            string input_filename = this.ucFilePickerInputAviFile.Filename;

            this.Log("folder = \"{0}\"", output_folder );


            if (output_folder.Length<1)
            {
                this.Log("folder must enter folder");
                this.LogFailure();
                return;
            }

            if (! System.IO.Directory.Exists(output_folder))
            {
                System.IO.Directory.CreateDirectory(output_folder);
            }

            if (System.IO.File.Exists(output_folder))
            {
                this.Log("this is a file not a folder");
                this.LogFailure();
                return;
            }

            this.Log("input file = \"{0}\"", input_filename );
            if (input_filename.Length < 1)
            {
                this.Log("must enter an input filename");
                this.LogFailure();
                return;
            }

            this.ExtractBitmaps( input_filename , output_folder );

            this.Log("Done.");


        }


        private void ExtractBitmaps( string input_filename, string output_folder )
        {
            AviFile.AviManager aviManager = new AviFile.AviManager(input_filename, true);

            AviFile.VideoStream stream = aviManager.GetVideoStream();
            stream.GetFrameOpen();

            for (int n = 0; n < stream.CountFrames; n++)
            {
                string fname = System.IO.Path.Combine( output_folder , string.Format("frame-{0:0000000000}.bmp",n) );
                stream.ExportBitmap(n, fname);
            }

            stream.GetFrameClose();
            aviManager.Close();


            /*
            this.Log("Number of bitmaps = {0}", bitmap_files.Length);

            // WORKITEM sort the names
            // WORKITEM print the names?
            // WORKITEM warn about different sizes
            // WORKITEM show dimensions


            Bitmap first_bitmap = this.load_image(bitmap_files[0]);
            AviFile.AviManager aviManager = null;
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
            double frames_per_second = (int)this.numericUpDownFrameRate.Value;
            AviFile.VideoStream aviStream = aviManager.AddVideoStream(compress, frames_per_second, first_bitmap);


            Bitmap bitmap;
            int count = 0;
            for (int n = 1; n < bitmap_files.Length; n++)
            {
                this.Log("Frame {0} Loading bitmap {1} ", n, bitmap_files[n]);
                bitmap = this.load_image(bitmap_files[n]);
                this.Log("Adding bitmap frame");
                aviStream.AddFrame(bitmap);
                bitmap.Dispose();
                count++;
            }

            this.Log("Closing Avi Manger");
            aviManager.Close();
             * */

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