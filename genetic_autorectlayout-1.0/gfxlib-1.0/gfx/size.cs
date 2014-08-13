using System;

namespace Gfx
{

	public struct SizeD 
	{
		public double Width;
		public double Height;

		public SizeD( double w, double h)
		{
			this.Width = w;
			this.Height = h;
		}

		public SizeD( System.Drawing.SizeF size )
		{
			this.Width = size.Width;
			this.Height = size.Height;
		}

		public static SizeD operator + ( SizeD a, SizeD b )
		{
			return new SizeD( a.Width + b.Width , a.Height + b.Height );
		}

		public void Scale( double sx, double sy )
		{
			this.Width *= sx;
			this.Height *= sy;
		}


	};


}
