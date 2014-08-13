using System;

namespace Gfx
{

	public struct RECT
	{
		public double x0,y0,w,h;

		public RECT(double x, double y, double w, double h) 
		{
			this.x0=x;
			this.y0=y;
			this.w=w;
			this.h=h;

			if ( this.w < 0 )
			{
				//throw new Exception( "width is negative" );
			}

			if ( this.h < 0 )
			{
				//throw new Exception( "height is negative" );
			}

		}

		void get_diagonal_points( out double x0, out double y0, out double x1, out double y1 ) 
		{
			x0=this.x0;
			y0=this.y0;
			x1=this.x0+this.w;
			y1=this.y0+this.h;
		}

		public double x1
		{
			get
			{
				return this.x0+this.w;
			}
		}

		public double y1
		{
			get
			{
				return this.y0+this.h;
			}
		}

		public bool overlaps( RECT r1 ) 
		{
			RECT r0=this;

			double x0,x1,y0,y1;
			r0.get_diagonal_points(out x0, out y0, out x1, out y1 );

			double x2,y2,x3,y3;
			r1.get_diagonal_points(out x2, out y2, out x3, out y3 );

			bool b = !(
				( (x0<x2) && (x1<x2))
				|| ((x0>x3) && (x1>x3)) 
				|| ((y0<y2) && (y1<y2)) 
				|| ((y0>y3) && (y1>y3))   );

			return b;

		}

		public bool overlaps_interior( RECT r1 ) 
		{
			RECT r0=this;

			double x0,x1,y0,y1;
			r0.get_diagonal_points(out x0, out y0, out x1, out y1 );

			double x2,y2,x3,y3;
			r1.get_diagonal_points(out x2, out y2, out x3, out y3 );

			bool b = !(   
				( (x0<=x2) && (x1<=x2))
				|| ((x0>=x3) && (x1>=x3)) 
				|| ((y0<=y2) && (y1<=y2)) 
				|| ((y0>=y3) && (y1>=y3))   );

			return b;

		}

		public RECT intersection( RECT r1 )
		{
			double nx0 = System.Math.Max( this.x0, r1.x0 );
			double ny0 = System.Math.Max( this.y0, r1.y0 );
			double nx1 = System.Math.Min( this.x1, r1.x1 );
			double ny1 = System.Math.Min( this.y1, r1.y1 );
			return new RECT( nx0, ny0, nx1-nx0, ny1-ny0 );
		}

		public bool is_valid
		{
			get
			{
				return ( ( this.w >= 0 ) && ( this.h >=0 ) );
			}
		}

		public POINT midpoint()
		{
			return new POINT( this.x0 + (this.w/2.0), this.y0 + (this.h/2.0) );
		}

		public bool contains( POINT P )
		{
			return ( (this.x0<=P.x) && ( this.y0 <=P.y ) && ( P.x<=this.x1 ) && ( P.y<=this.y1) );
		}



		public bool contains( RECT B)
		{
			return ( (this.x0<=B.x0 ) && ( this.y0 <=B.y0 ) && ( B.x1<=this.x1 ) && ( B.y1<=this.y1) );
		}



		public double area 
		{
			get
			{
				return this.w * this.h;
			}
		}
	

		public static RECT FromPoints( double x0, double y0, double x1, double y1 )
		{
			return new RECT( x0,y0, x1-x0,y1-y0 );
		}






	}

}
