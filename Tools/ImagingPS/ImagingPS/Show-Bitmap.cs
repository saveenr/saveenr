using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SMA = System.Management.Automation;
using ImagingPS.Internal.Extensions;

namespace ImagingPS
{
    [SMA.Cmdlet("Show","Bitmap")]
    public class Show_Bitmap: SMA.Cmdlet
    {
        [SMA.Parameter(Mandatory = true, Position = 0)]
        public System.Drawing.Bitmap Bitmap { get; set; }


        protected override void ProcessRecord()
        {
            var form = new FormShowBitmap();
            form.Image = this.Bitmap;

            form.ShowDialog();
        }
    }
}
