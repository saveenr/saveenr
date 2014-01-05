using System.Runtime.InteropServices;

namespace Isotope.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public int Width
        {
            get { return this.Right - this.Left;  }
        }

        public int Height
        {
            get { return this.Bottom  - this.Top ; }
        }

        public System.Drawing.Rectangle ToRectangle()
        {
            return new System.Drawing.Rectangle(this.Left, this.Top, this.Width, this.Height);
        }

    }
}