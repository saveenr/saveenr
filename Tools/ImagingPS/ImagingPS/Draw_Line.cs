using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SMA = System.Management.Automation;
using ImagingPS.Internal.Extensions;

namespace ImagingPS
{
    [SMA.Cmdlet("Draw","Line")]
    public class Draw_line: SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position = 0)]
        public System.Drawing.Bitmap Bitmap { get; set; }

        [SMA.Parameter(Mandatory = true, Position =1)]
        public int X0{ get; set; }

        [SMA.Parameter(Mandatory = true, Position =2)]
        public int Y0{ get; set; }

        [SMA.Parameter(Mandatory = true, Position =3)]
        public int X1{ get; set; }

        [SMA.Parameter(Mandatory = true, Position =4)]
        public int Y1{ get; set; }

        [SMA.Parameter(Mandatory = false, Position = 5)] public int Color = 0;

        [SMA.Parameter(Mandatory = false, Position = 6)] public float Width = 1.0f;


        protected override void ProcessRecord()
        {
            using (var g = System.Drawing.Graphics.FromImage(this.Bitmap))
            {
                var color = System.Drawing.Color.FromArgb(this.Color);
                using (var pen = new System.Drawing.Pen(color, this.Width))
                {
                    g.DrawLine(pen, this.X0, this.Y0, this.X1, this.Y1);                    
                }
            }
        }
    }
}
