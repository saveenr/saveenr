using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SMA = System.Management.Automation;
using ImagingPS.Internal.Extensions;

namespace ImagingPS
{
    [SMA.Cmdlet("Convert","ImageFile")]
    public class Convert_ImageFile: SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position = 0)]
        public string InputFile { get; set; }

        [SMA.Parameter(Mandatory = false)]
        public string OutputFile{ get; set; }

        [SMA.Parameter(Mandatory = false)]
        public ImageFormat ImageFormat { get; set; }

        protected override void ProcessRecord()
        {
            this.WriteVerbose("Infile to {0}", this.InputFile);
            this.WriteVerbose("Format to {0}", this.ImageFormat);
            this.WriteVerbose("Outputfile to {0}", this.OutputFile);
            
            string infile = System.IO.Path.GetFullPath(InputFile);
            if (!System.IO.File.Exists(infile))
            {
                string msg = string.Format("file does not exist");
                throw new System.Exception(msg);
            }

            if (this.OutputFile == null && this.ImageFormat == ImageFormat.Default)
            {
                throw new System.Exception("must either provide an output file or specify a format");
               
            }

            if (this.OutputFile != null && this.ImageFormat!=ImageFormat.Default)
            {
                throw new System.Exception("can't set both an output file and a format");
               
            }

            string outfile = this.OutputFile;

            if (this.ImageFormat != ImageFormat.Default)
            {
                string ext = ".XXX";
                if (this.ImageFormat==ImageFormat.BMP)
                {
                    ext = ".bmp";
                    ext = ".bmp";
                }
                else if (this.ImageFormat==ImageFormat.JPG)
                {
                    ext = ".jpg";
                }
                else if (this.ImageFormat==ImageFormat.PNG)
                {
                    ext = ".png";
                }
                else
                {
                    throw new Exception();
                }
                
                outfile = System.IO.Path.GetDirectoryName(infile) + System.IO.Path.DirectorySeparatorChar + System.IO.Path.GetFileNameWithoutExtension(infile) + ext;
                this.WriteVerbose("outputfile set to {0}", outfile);
            }


            this.WriteVerbose("Loading bitmap");
            using (var inbmp = System.Drawing.Bitmap.FromFile(infile))
            {
                this.WriteVerbose("Saving bitmap");
                inbmp.Save(outfile);
                this.WriteVerbose("Finished saving bitmap");
            }

        }
    }
}
