using System;

namespace Gfx
{

	public struct PointD 
	{
		public double X;
		public double Y;

		public PointD( double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		public void Translate( double dx, double dy )
		{
			this.X += dx;
			this.Y += dy;
		}

		public void Scale( double sx, double sy )
		{
			this.X *= sx;
			this.Y *= sy;
		}

	};


}
