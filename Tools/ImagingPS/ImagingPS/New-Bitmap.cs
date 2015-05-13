using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SMA = System.Management.Automation;
using ImagingPS.Internal.Extensions;

namespace ImagingPS
{
    [SMA.Cmdlet("New","Bitmap")]
    public class New_Bitmap: SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position = 0)]
        public int Width { get; set; }

        [SMA.Parameter(Mandatory = true, Position =1)]
        public int Height{ get; set; }

        [SMA.Parameter(Mandatory = false, Position = 3)] public System.Drawing.Imaging.PixelFormat PixelFormat = System.Drawing.Imaging.PixelFormat.Format32bppRgb;

        [SMA.Parameter(Mandatory = false, Position = 2)]
        public int Color = 0;

        protected override void ProcessRecord()
        {
            var bmp = new System.Drawing.Bitmap(this.Width, this.Height, this.PixelFormat);

            using (var g = System.Drawing.Graphics.FromImage(bmp))
            {
                var color = System.Drawing.Color.FromArgb(this.Color);
                using (var brush = new System.Drawing.SolidBrush(color))
                {
                    this.WriteVerbose("Filling rect {0}", this.Color);
                    g.FillRectangle(brush, 0, 0, this.Width, this.Height);
                }
            }
            this.WriteObject(bmp);
        }
    }
}
