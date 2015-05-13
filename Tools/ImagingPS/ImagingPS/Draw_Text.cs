using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SMA = System.Management.Automation;
using ImagingPS.Internal.Extensions;

namespace ImagingPS
{
    [SMA.Cmdlet("Draw","String")]
    public class Draw_String: SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position = 0)]
        public System.Drawing.Bitmap Bitmap { get; set; }

        [SMA.Parameter(Mandatory = true, Position =1)]
        public int X{ get; set; }

        [SMA.Parameter(Mandatory = true, Position =2)]
        public int Y{ get; set; }

        [SMA.Parameter(Mandatory = true, Position =3)]
        public string Text { get; set; }

        [SMA.Parameter(Mandatory = false, Position = 5)] public int Color = 0;

        [SMA.Parameter(Mandatory = false, Position = 6)]
        public string Font = "Arial";

        [SMA.Parameter(Mandatory = false, Position = 7)] public float Size = 1.0f;

        protected override void ProcessRecord()
        {
            using (var g = System.Drawing.Graphics.FromImage(this.Bitmap))
            {
                var color = System.Drawing.Color.FromArgb(this.Color);
                using (var brush = new System.Drawing.SolidBrush(color))
                using (var font = new System.Drawing.Font(this.Font,this.Size))
                {
                    g.DrawString(this.Text, font, brush, this.X, this.Y);                    
                }
            }
        }
    }
}
